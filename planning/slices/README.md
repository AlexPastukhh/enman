# Slice Planning Index

Status: current slice-planning navigation index  
Scope: business slices, client sidecars, cross-cutting/helper slices, examples, shared registers, test planning, API contract artifacts, client architecture, extension/change points and implementation notes

## 1. Purpose

This folder documents how to derive implementation slices from scenarios/concerns and how to plan implementation one slice at a time.

It is also the entry point for slice-wide shared registers.

## 2. Draft-Driven Discovery

All slice families use draft-driven discovery:

```text
planning/slices/draft-driven-discovery-principles.md
```

This applies to:

```text
business slices
client sidecars
cross-cutting/helper slices
testing/support slices
documentation/status reconciliation drafts
```

## 3. Slice Types

### Business slice

Scenario-derived business behavior slice.

Full backend/API/persistence business slice files should include:

```text
Visual Scenario Flow
-> Scenario Slice Flow
-> Visual Implementation Flow
-> Implementation Flow
```

Visual flow sections should be diagram-like maps, not only linear arrow lists.

### Client sidecar

Client implementation file for a concrete business slice.

Client sidecars are not created in advance. They are created or updated when concrete client work starts and then used as draft/discovery/status files.

Full client sidecars should include architecture/folder-based visual implementation flow, for example:

```text
[Route: app/router/router.tsx]
-> [Page: pages/...]
-> [Feature UI: features/.../ui]
-> [Feature Model: features/.../model]
-> [Feature API Mapper: features/.../api]
-> [Shared API: shared/api/...]
-> [Generated Contracts: shared/api/generated/openapi-types.ts]
```

### Cross-cutting slice

Technical/support slice with observable behavior, concern-derived behavior items, concern flow, implementation flow and tests/checks, used by multiple business slices.

### Helper slice

Smaller reusable helper/support behavior with implementation and tests.

### Example slice draft

Example files live in:

```text
planning/slices/examples/
```

They are examples only and are not current implementation evidence unless copied into an active slice and reconciled with current repo state.

## 4. Current Backend Slice Files

Current implemented L1 backend/API/persistence/session slice docs:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-AUTH-001-login-client-account.md
planning/slices/SL-AUTH-002-current-user.md
planning/slices/SL-AUTH-003-logout.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
```

Status summary:

| Slice | Backend/API status | Client/UI status |
|---|---|---|
| `SL-ACC-001` | implemented register endpoint, persistence and tests | first-stage registration UI implemented; tests pending |
| `SL-AUTH-001` | implemented login endpoint, L1 cookie session and tests | first-stage login UI implemented; tests pending |
| `SL-AUTH-002` | implemented current-user endpoint/query/session validation and tests | first-stage session bootstrap implemented; route guard/global error policy pending |
| `SL-AUTH-003` | implemented logout endpoint/session clearing and tests | shared logout API wrapper exists; concrete logout UI/cache/navigation flow not confirmed |
| `SL-APPL-001` | implemented applicant create endpoint, persistence and tests | first-stage Account page applicant create UI implemented; read-current slice missing; tests pending |
| `SL-REQ-001` | implemented request create endpoint, server-selected applicant, no required body and tests | request form UI, My Requests read screens and E2E are future client/read work |

These files are current backend/API/persistence/session slice docs and should follow the full backend slice flow rule.

## 5. Current Client Sidecar Files

Current implemented/first-stage client sidecars:

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
```

Current sidecar status:

| Sidecar | Parent slice | Status | Notes |
|---|---|---|---|
| `SL-ACC-001-register-client-account.client.md` | `SL-ACC-001` | implemented first-stage client feature flow / tests pending | `/register`, password confirmation, DTO mapping to email/password, success -> `/login` |
| `SL-AUTH-001-login-client-account.client.md` | `SL-AUTH-001` | implemented first-stage client feature flow / tests pending | `/login`, login mutation, ProblemDetails mapping, session query invalidation, success -> home |
| `SL-AUTH-002-current-user.client.md` | `SL-AUTH-002` | first-stage implemented session bootstrap / route guard pending | `SessionProvider`, current-user query, 401 -> null session, `useSession` consumers |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `SL-APPL-001` | first-stage implemented client feature flow / current applicant read missing / tests pending | Account page form, local read-only success state, Edit action, self-dismissing notification |

Client-support-only implementation that does not yet get an implemented full sidecar:

| Support | Current status | Why no implemented sidecar yet |
|---|---|---|
| `logoutClientAccount()` shared API wrapper | shared API support implemented | concrete logout UI/cache/navigation flow not confirmed |
| request creation generated path/type support | generated contract support exists | request creation feature UI is not implemented |
| current applicant read | not implemented | future read-current endpoint/client slice needed |

## 6. Slice Support Files

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/shared/README.md
planning/slices/shared/maybe-for-optional-results.md
planning/slices/cross-cutting/README.md
planning/slices/examples/README.md
```

## 7. Slice Registers

Use shared registers to keep local slice discoveries visible across time and chats.

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

### Slice questions register

`slice-questions-register.md` is the shared overview of currently relevant local slice/client/cross-cutting questions.

Local `Questions / Decisions` sections remain the source of detailed context.

The register makes important local questions discoverable from one place.

### Extension points register

`slice-extension-points-register.md` owns extension points, change pressure, anti-coupling decisions and extension-related questions.

### Implementation notes register

`slice-implementation-notes-register.md` owns concrete future implementation/client/testing notes that are not yet assigned to an active slice or sidecar.

## 8. Question Sync Rule

When a local slice/client/cross-cutting file has a question that remains relevant after the local draft, mirror it into:

```text
planning/slices/slice-questions-register.md
```

Also update specialized registers when needed:

```text
extension/change pressure -> slice-extension-points-register.md
future implementation/client/testing note -> slice-implementation-notes-register.md
scenario/domain ambiguity -> planning/diagrams/scenario-questions-register.md
```

Open questions and unresolved risks should appear before accepted decisions in local files and registers.

If a question remains local only, state why.

## 9. Examples

Use:

```text
planning/slices/examples/
```

Current examples:

```text
planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
```

The early short example shows the valid shortened working format.

The full backend slice example shows visual maps before detailed scenario/implementation flows.

## 10. Cross-Cutting / Helper Slices

Use:

```text
planning/slices/cross-cutting/
```

Current cross-cutting slices:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Cross-cutting/helper slices must have:

```text
- observable/support behavior;
- concern-derived behavior items;
- concern slice flow;
- implementation flow;
- tests/checks;
- consumers / used-by slices;
- local questions;
- ADR impact.
```

## 11. API Contract Support

API contract docs live in:

```text
planning/api/
```

Use them before client slice implementation.

For current L1 client work, generated OpenAPI TypeScript types already include L1 auth/applicant/request paths and DTOs.

Generated types are support artifacts, not completed feature UI.

## 12. Shared Notes

Use:

```text
planning/slices/shared/
```

for reusable notes/helpers that do not have a full slice behavior/test flow.

Current shared notes:

```text
planning/slices/shared/maybe-for-optional-results.md
planning/slices/shared/antiforgery-token-session-context.md
```

`maybe-for-optional-results.md` is the target convention for repository/query APIs where no resulting object is a normal outcome. It is especially relevant for future read slices and repository refactors.

## 13. Repository / Optional Result Convention

For new/refactored repository and query APIs, use:

```text
planning/slices/shared/maybe-for-optional-results.md
```

Core rule:

```text
If absence of the resulting object is normal, return `Maybe<T>` from repository/query APIs and let the application handler map `Maybe.None` to the use-case result.
```

Current implementation note:

```text
Existing L1 repositories still use nullable returns in several places.
That is current repo evidence, not the target convention for new/refactored repository APIs.
```

## 14. Testing Support

Testing workflow lives in:

```text
planning/testing/
```

Current L1 backend slices are covered primarily by:

```text
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Tests.EnergyManagement/Domain/**
```

Client/component/E2E test evidence for current L1 client sidecars was not found during this documentation reconciliation; mark those checks as planned/gap until tests are added or located.

Browser E2E for applicant/request flows should wait until the corresponding client/read UI exists.

## 15. Parent Business Slice Files

Parent business slice files own:

```text
- vertical behavior;
- Visual Scenario Flow;
- Scenario Slice Flow;
- behavior item coverage summary;
- Visual Implementation Flow;
- cross-layer Implementation Flow;
- API contract;
- local Questions / Decisions;
- links/back-references to shared registers when questions are mirrored;
- extension/change/pressure decisions;
- application/domain/persistence responsibilities;
- server/integration tests;
- end-to-end test coverage summary when relevant;
- links to cross-cutting/helper slices when used;
- link to `.client.md` sidecar when client work starts.
```

## 16. Client Sidecar Files

A `.client.md` file is created only when concrete client work starts.

It owns detailed client implementation planning, current client implementation status and client/component tests.

It should include E2E coverage only for cross-layer behavior that truly needs browser-client-server wiring.

Client sidecars also follow the local/global question sync rule.

Current remaining client order:

```text
Logout UI/cache/navigation, if needed before protected flows
-> Current applicant read after refresh
-> Request Creation UI
-> My Requests read/list/detail
-> Browser E2E happy paths
```
