# Client Slice Short Draft Rules And Canonical Example

Status: current / canonical short-draft form for client sidecar drafters  
Scope: client sidecar short drafts, scenario flow vs implementation flow, behavior coverage discipline, extension-slice boundaries

## 1. Non-Negotiable Rule

Client slice drafters must follow existing examples and this canonical shape.

Do not improvise a new structure unless the user explicitly asks for a different format.

The goal is boring consistency:

```text
same section names;
same scope/out-of-scope discipline;
same Scenario Flow vs Implementation Flow separation;
same Questions table shape;
same Behavior Coverage discipline;
same Testing style.
```

When in doubt, copy the shape and wording style from the canonical example below.

## 2. Short Draft Purpose

A short draft is not an unstructured sketch.

A short draft is a compact planning artifact for one client slice.

It must:

```text
- define exactly what this slice owns;
- define what this slice does not own;
- identify related slices / extension slices;
- show only the scenario slice flow relevant to this slice;
- show the client implementation layering;
- capture questions/decisions;
- cover source behavior items;
- include a verification plan.
```

Do not omit Scope / Out of Scope just because the draft is short.

## 3. Draft One Slice At A Time

A scenario can be implemented by multiple slices.

A client draft is for one slice only.

Example:

```text
Scenario: Applicant Parties page
        ↓
SL-APPL-002.client reads and displays saved ApplicantParties.
SL-APPL-001.client owns add Individual ApplicantParty action/form.
SL-APPL-003 owns future explicit make default/current action.
Future lifecycle slice owns edit/delete/archive.
```

Do not include all scenario actions in one sidecar just because they happen on the same page.

## 4. Scenario Flow Source Rule

Scenario Flow must come from:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

The register points to scenario text, DATA, UI and behavior item source files.

Keep the register up to date when scenarios, UI specs, behavior files or slices change.

Scenario Flow is the part of the scenario that belongs to the current slice.

It is not automatically the whole scenario.

## 5. Scenario Flow vs Implementation Flow

Scenario Flow is user/system behavior from scenario specs.

Implementation Flow is how the client implements that behavior.

### Bad Scenario Flow

```text
[Route / Page]
AccountPage.tsx calls useQuery
        ↓
[Entity API]
listAccountApplicantParties()
        ↓
[Shared API]
fetchJson()
        ↓
[React Query]
caches response
```

Why bad:

```text
- implementation details are presented as scenario behavior;
- scenario source did not require React Query or fetchJson;
- user-visible/system behavior is missing.
```

### Good Scenario Flow

```text
[Signed-in Client]
opens Applicant Parties page / section
        ↓
[Page]
shows Applicant Parties read area
        ↓
Client sees current/default ApplicantParty templates highlighted
        ↓
Client sees other saved ApplicantParties below
        ↓
If none exist, client sees empty read state
```

### Bad Implementation Flow

```text
Client sees current/default templates
        ↓
Client sees other saved ApplicantParties
        ↓
Client sees empty state
```

Why bad:

```text
- this repeats scenario flow;
- it does not tell implementers where functions/classes/types live;
- it does not show page/entity/shared/generated layering.
```

### Good Implementation Flow

```text
[Route / Page Layer]
pages/account/AccountPage.tsx
  Lives here: AccountPage
  Uses: useSession(), useAccountApplicantPartiesQuery()
  Owns: session branch, page layout, read-state composition
  Does not own: fetchJson, generated DTO aliases, query keys
        ↓
[Entity Query Layer]
entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
  Lives here: useAccountApplicantPartiesQuery(), applicantPartyQueryKeys
  Uses: useQuery(), listAccountApplicantParties()
  Owns: query key, enabled guard, read hook
  Does not own: route/session decision, HTTP path string, command mutation
```

## 6. Behavior Items Rule

Behavior items are not implementation details.

Behavior items must come from scenario/UI/behavior source files or cross-cutting concern sources.

Do not invent behavior items such as:

```text
- query key includes filters;
- listAccountApplicantParties() exists;
- getAccountApplicantParties() uses fetchJson;
- generated OpenAPI type exists;
- cache invalidation happens;
- route param is parsed;
- API wrapper is shared.
```

These belong in implementation flow, API contract, implementation notes or test plan.

If source behavior item IDs are missing, write:

```text
Source BI TBD
```

and treat it as a source gap.

## 7. Canonical Short Draft Shape

Use this exact shape for short client drafts.

```text
# SLICE-ID.client — Title

Status:
Parent slice:
Slice type:
Architecture direction:
Backend/API contract evidence, when relevant:

## 1. Scope
## 2. Out of Scope
## 3. Related Slices / Owners
## 4. Visual UI / Scenario Flow
## 5. Visual Client Implementation Flow
## 6. Client API / Server Contract
## 7. Questions / Decisions
## 8. Extension / Change Points
## 9. Behavior Coverage
## 10. Client / Component / E2E Verification Plan
## 11. Implementation Checklist
## 12. Next Step
```

## 8. Canonical Implementation Flow Block Shape

Every implementation layer should use:

```text
[Layer Name]
path/to/file.tsx
path/to/file.ts

Lives here:
  functions/classes/types/components/constants that live in this layer

Uses:
  dependencies called/imported/consumed

Owns:
  responsibilities belonging to this layer

Does not own:
  responsibilities that must stay in another layer/slice
```

This is mandatory for new client drafts.

## 9. Canonical Short Draft Example To Copy

Copy this form for future short drafts. Replace names and source items, but keep the section order and table shapes.

```text
# SLICE-ID.client — Human Title

Status: short client <read|command> sidecar draft / <implementation status or blocker>
Parent slice: SL-XXX-000 — Parent Slice Title
Slice type: client <read|command|filter|details> sidecar
Architecture direction: <read slice maps to pages + entities; command/user action maps to pages + features + entities>
Backend/API contract evidence:
  <endpoint / contract direction / blocker>

## 1. Scope

This sidecar owns:

- <visible/read/command responsibility>;
- <client state responsibility>;
- <API consumption responsibility>;
- <component placement responsibility>.

## 2. Out of Scope

- <excluded action> -> <owning slice>;
- <backend implementation> -> <parent backend slice>;
- <generated artifacts> -> <generation/check workflow>;
- <future lifecycle> -> <future slice>;
- <cleanup/removal> -> <cleanup task>.

## 3. Related Slices / Owners

SL-XXX-001
  owns ...

Future SL-XXX-002
  owns ...

## 4. Visual UI / Scenario Flow

Source UI model:
  <scenario/UI source and relevant lines or source item ids>

[Actor]
visible user/system behavior
        ↓
[Page/Area]
visible result
        ↓
[Branch]
visible success/empty/error state

In ordinary words:
  <short user-facing scenario explanation>

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | ... | ... |

## 5. Visual Client Implementation Flow

[Route / Page Layer]
path/to/page.tsx

Lives here:
  ...

Uses:
  ...

Owns:
  ...

Does not own:
  ...
        ↓

[Entity / Feature / Shared / Generated Layer]
path/to/file.ts

Lives here:
  ...

Uses:
  ...

Owns:
  ...

Does not own:
  ...

In ordinary words:
  <implementation layering explanation, not scenario behavior>

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | ... | ... |

## 6. Client API / Server Contract

Endpoint / generated contract:
  ...

Client rule:
  ...

Do not invent generated operation ids before generation exists.

## 7. Questions / Decisions

| ID | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| Q-... | accepted direction | ... | ... | ... |

## 8. Extension / Change Points

- <future behavior> -> <future slice>;
- <cleanup> -> <cleanup task>.

## 9. Behavior Coverage

| Source behavior item | How sidecar covers it | Status |
|---|---|---|
| SC-... | ... | covered |
| SC-... | owned by related slice | out of scope |

## 10. Client / Component / E2E Verification Plan

Component/client tests:

| Test / check | Verifies |
|---|---|
| ... | ... |

Shared API/entity tests:

| Test / check | Verifies |
|---|---|
| ... | ... |

E2E:
  visible user outcome only; do not assert React Query internals or refetch mechanics.

## 11. Implementation Checklist

[ ] ...

## 12. Next Step

<full sidecar / implementation handoff / source gap / backend blocker>
```

## 10. Extension Slices

An extension slice is a later slice that completes more of the same scenario without being part of this slice.

Example:

```text
SL-APPL-002.client:
  read/display ApplicantParties.

SL-APPL-001.client:
  add Individual ApplicantParty action/form.

SL-APPL-003:
  explicit make default/current action.

Future lifecycle slice:
  edit/delete/archive.
```

Extension slices should appear in:

```text
Out of Scope
Related Slices / Owners
Extension / Change Points
Behavior Coverage as related/out of scope where relevant
```

Do not implement extension behavior in the current draft.

## 11. Current Full Draft Example

The current full read sidecar example is:

```text
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
```

Use its detailed implementation-flow style for future full sidecars.

Important distinction:

```text
The full sidecar can be longer.
The short draft still uses the same section order and same Lives here / Uses / Owns / Does not own style.
```

## 12. Drafting Checklist

Before finalizing a client short draft, verify:

```text
[ ] I copied the canonical short draft section structure.
[ ] I read slice-scenario-flow-behavior-register.md.
[ ] Scenario Flow is from scenario/UI specs and only covers this slice.
[ ] Implementation Flow includes concrete files/functions/types per layer.
[ ] I did not call implementation details behavior items.
[ ] I separated Behavior Coverage from Test/Verification Plan.
[ ] I named extension slices and owners.
[ ] I kept read UI in entities and command/user-action UI in features.
[ ] I used generated OpenAPI types only through shared/entity API boundaries.
[ ] I did not invent generated operation ids before generation exists.
```
