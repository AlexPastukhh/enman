# Client Layering For Read And Command Slices

Status: current client architecture clarification / read-vs-command placement rule  
Scope: where pages, entities, features and shared API wrappers belong in client slice drafts

## 1. Purpose

This note explains why client API fetch functions live in `shared/api` even though they call endpoints for different entities, and how client slice drafts should place read UI versus command/user-action UI.

Do not change architecture because `shared/api` contains functions for multiple entity domains.

That is intentional.

## 2. Why API Fetch Functions Live In shared/api

`shared/api` is the low-level client/server boundary.

It owns:

```text
- stable API path constants;
- low-level fetchJson usage;
- generated OpenAPI type aliases;
- thin wrappers around HTTP endpoints;
- transport-level concerns common to all client domains.
```

It is allowed to contain wrappers for different backend areas because it is grouped by architectural layer, not by business entity.

Examples:

```text
shared/api/l1AuthApi.ts
shared/api/l1ApplicantPartyApi.ts
shared/api/l1RequestApi.ts
```

These are not feature modules. They are shared client/server contract wrappers.

## 3. Why Entity API Functions Still Exist

Entity API functions provide domain-facing names and keep React Query / entity model code from depending directly on low-level shared transport names.

Example direction:

```text
entities/applicant-party/api/listAccountApplicantParties.ts
  listAccountApplicantParties()
        ↓
shared/api/l1ApplicantPartyApi.ts
  getAccountApplicantParties()
        ↓
fetchJson(GET /api/l1/applicant-parties)
```

The entity API layer owns:

```text
- entity-level operation names;
- mapping between entity model/read hooks and shared API wrappers;
- future per-entity response mapping if needed.
```

It does not own:

```text
- HTTP path constants;
- fetchJson internals;
- generated OpenAPI alias definitions;
- page UI state.
```

## 4. Read Slice Placement Rule

Read slices map to:

```text
pages + entities + shared/api + generated contracts
```

Read UI belongs in:

```text
entities/<entity>/ui
```

Read model/query belongs in:

```text
entities/<entity>/model
entities/<entity>/api
```

Examples:

```text
entities/request/ui/MyRequestsList.tsx
entities/request/ui/MyRequestDetailsView.tsx
entities/applicant-party/ui/ApplicantPartiesList.tsx
entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
```

Do not put read-only display UI into `features` just because it is visible on a page.

## 5. Command / User-Action Placement Rule

Command or user-action slices map to:

```text
pages + features + entities + shared/api + generated contracts
```

Feature UI belongs in:

```text
features/<business-action>/ui
```

Feature model/mutation code belongs in:

```text
features/<business-action>/model
features/<business-action>/api
```

Examples:

```text
features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm.tsx
features/request/my-requests-filters/ui/MyRequestsFilters.tsx
features/auth/logout/ui/LogoutButton.tsx
```

A filter control is a user action/control feature even if it affects a read list.

A read list is entity display UI.

## 6. Drafting Rule

Client sidecar drafts must explicitly say where each class/function/type lives:

```text
Lives here:
Uses:
Owns:
Does not own:
```

This prevents ambiguity such as:

```text
features/request/my-requests-list/ui/MyRequestsList.tsx
```

when the UI is actually read-only entity display and should be under:

```text
entities/request/ui/MyRequestsList.tsx
```

## 7. Good Layering Example

```text
[Route / Page Layer]
pages/account/AccountPage.tsx
  owns session branch and page composition
  uses useAccountApplicantPartiesQuery()

[Entity Query Layer]
entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
  owns React Query read hook and query key
  uses listAccountApplicantParties()

[Entity API Layer]
entities/applicant-party/api/listAccountApplicantParties.ts
  owns entity-level read operation name
  uses getAccountApplicantParties()

[Shared API Layer]
shared/api/l1ApplicantPartyApi.ts
  owns GET /api/l1/applicant-parties wrapper and generated response type alias

[Generated Contract Layer]
shared/api/generated/openapi-types.ts
  owns generated structural API types

[Entity Display UI Layer]
entities/applicant-party/ui/ApplicantPartiesList.tsx
  owns read-only cards, empty state and current/default grouping UI
```

## 8. Bad Layering Example

```text
[Feature Layer]
features/applicant-party/account-list/ui/ApplicantPartiesList.tsx
  fetches GET /api/l1/applicant-parties directly
  parses session
  groups current/default cards
  renders add form
  calls create mutation
```

Problems:

```text
- read-only display is placed in features;
- HTTP call bypasses shared API wrapper;
- page/session responsibilities are mixed into feature UI;
- read and command behavior are mixed;
- create form belongs to SL-APPL-001.client, not SL-APPL-002.client.
```
