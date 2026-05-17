# L1 Slice Drafting Guide

Status: current / strict example-driven drafting, cross-cutting concerns and server test-plan separation synchronized  
Scope: business slices, cross-cutting/helper slices, client sidecars, scenario source intake, implementation flow, extension/change points, questions, registers and tests

## 1. Draft-Driven Discovery Gate

Before drafting any slice, read:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
planning/slices/draft-driven-discovery-principles.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/change-extension-points-principles.md
planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md
```

Client sidecar drafters must also read:

```text
planning/client/README.md
planning/client/client-layering-for-read-and-command-slices.md
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/api/client-server-contract-principles.md
```

Server/API drafts that introduce browser unsafe requests must also read:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## 2. Example-Driven Drafting Rule

New chats must draft by existing examples.

Do not invent a new form, new section order or new terminology just because it seems nicer.

For client short drafts, copy the canonical shape from:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
```

Consistency is more important than creativity.

## 3. Scenario Flow Source Rule

Before writing Scenario Flow or Behavior Coverage, read:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Then read the linked scenario text, DATA, UI and behavior files.

Scenario Flow is the part of the scenario that belongs to the current slice.

It is not necessarily the whole scenario.

A scenario can be implemented by several slices and extension slices.

## 4. Scenario Flow vs Implementation Flow

Scenario Flow shows user/system behavior from scenario specs.

Implementation Flow shows code/layer responsibilities.

Bad Scenario Flow:

```text
[Controller]
Extracts account id
        ↓
[Repository]
Loads ApplicantParties
        ↓
[React Query]
Caches response
```

Good Scenario Flow:

```text
[Signed-in Client]
opens Applicant Parties page / section
        ↓
Client sees current/default ApplicantParty templates highlighted
        ↓
Client sees other saved ApplicantParties below
```

Good Client Implementation Flow must show:

```text
[Layer]
path/to/file

Lives here:
Uses:
Owns:
Does not own:
```

## 5. Behavior Items Rule

Behavior items are not implementation details.

Do not treat these as behavior items:

```text
- query key includes filters;
- fetchJson is called;
- generated type exists;
- route param is parsed;
- cache invalidation happens;
- ApplicantPartyId is returned;
- repository method exists;
- antiforgery filter class exists.
```

They may be implementation notes, API contract notes or test plan items.

Behavior items must come from scenario/UI/behavior sources or cross-cutting concern sources.

If source IDs are missing, write `Source BI TBD` and mark it as a source gap.

## 6. Scope / Out-of-Scope / Related-Slices Rule

Every non-trivial slice draft must explicitly define:

```text
Scope
Out of scope
Related slices / owners
Future extension points
```

Out-of-scope items must point to an owner:

```text
- existing slice;
- future slice;
- cleanup task;
- client sidecar;
- cross-cutting/helper slice;
- explicit not planned.
```

## 7. Cross-Cutting Concerns / Considerations Rule

Every non-trivial slice draft must include:

```text
## Cross-Cutting Concerns / Considerations
```

Use the checklist:

```text
planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md
```

Minimum concern scan:

```text
- Auth/session/actor context
- Authorization/ownership
- Antiforgery / browser unsafe requests
- Request validation / ProblemDetails
- OpenAPI / generated artifacts
- Generated constants / error codes
- Transaction / atomicity / no partial write
- No-mutation / existing data safety
- Idempotency / retry / double-submit
- Concurrency / stale state
- File/document boundary
- Clock/audit actor fields
- Privacy / cross-account data exposure
- Client feedback / accessibility
- Testing responsibility split
```

Important:

```text
Cross-cutting concerns are not Scenario Flow.
Cross-cutting concerns are not automatically Behavior Items.
```

If a cross-cutting source defines behavior items, reference those items.

Otherwise, write concerns as considerations/constraints and owners.

### Antiforgery marker rule

For browser unsafe request slices, distinguish:

```text
Internal server marker:
  antiforgery-specific ASP.NET framework result marker/type,
  preferably IAntiforgeryValidationFailedResult.

Public client marker:
  ProblemDetails.Extensions["code"] = "security.antiforgery.validation.failed".
```

Do not infer antiforgery failure from generic 400/BadRequest.

Do not use FluentValidation, legacy ServerValidationError or `errors[]` as the CSRF public marker.

## 8. Client Sidecar Short Draft Template

Use exactly:

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
## 10. Cross-Cutting Concerns / Considerations
## 11. Client / Component / E2E Verification Plan
## 12. Implementation Checklist
## 13. Next Step
```

## 9. Client Read vs Command Placement

Read slices:

```text
pages + entities + shared/api + generated contracts
```

Command/user-action slices:

```text
pages + features + entities + shared/api + generated contracts
```

Read-only UI belongs in `entities/<entity>/ui`.

Command/action UI belongs in `features/<action>/ui`.

## 10. Behavior Coverage Is Not Test Coverage

Behavior Coverage answers:

```text
Does the draft cover source behavior?
```

Test / Verification Plan answers:

```text
How will implementation be verified?
```

Do not mix these tables.

## 11. Questions / Decisions Rule

Every non-trivial question should include:

```text
ID
Status
Question
Assumption / current direction
Impact
Shared register / local-only reason
```

Order:

```text
open -> blocked -> assumptions -> future review -> accepted -> resolved/superseded
```

## 12. Shared Register Sync Rule

Use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Update them when local slice changes affect source mapping, future work, extension pressure, implementation notes or shared questions.

## 13. OpenAPI / Generated Artifact Rule

If a slice changes API contract, generated artifacts must come from repo commands, not manual edits.

Read:

```text
planning/api/generated-artifact-check-workflow.md
planning/api/openapi-contract-generation.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

## 14. Server Slice Test Plan Separation Rule

Server/backend slice drafts must separate test responsibilities by what they prove.

Use this structure for non-trivial backend command slices:

```text
### API boundary / access tests
### Main DB state transition tests
### Idempotency / no-op DB state tests
### No-mutation / unrelated-record safety tests
### Same-type / type-scope tests, when applicable
### Regression guards
### What not to test
```

Do not write one flat list when the slice changes persisted state.

### API boundary / access tests

Use these to verify the endpoint boundary:

```text
- unauthenticated -> 401;
- missing resource -> documented rejection response;
- not-owned resource -> documented rejection response;
- invalid route/body/query input -> documented validation/problem response.
```

For unsafe browser request slices, CSRF missing/invalid token behavior is owned by `CC-CSRF-001`.

A business slice may reference CSRF coverage but should not duplicate the full cross-cutting matrix unless it has special security behavior.

### DB state transition tests

Use these as the primary proof for backend command behavior.

For state-changing slices, assert persisted rows before and after the command:

```text
- selected row changed as expected;
- previous row changed as expected;
- required rows still exist;
- stable fields are unchanged;
- status/marker values are persisted correctly.
```

### No-mutation tests

Use these to protect important data-safety rules:

```text
- existing requests remain linked to the same ApplicantParty;
- unrelated ApplicantParties are not deleted/hidden/replaced;
- other rows/types remain unchanged;
- rejected commands do not partially update state.
```

When setup is expensive, no-mutation assertions may be combined with the main DB transition test, but the draft must name what safety rule is being protected.

### Regression guards

Use regression guards for behavior owned by another slice but critical to the new slice boundary.

Do not duplicate heavy tests if an existing stable test already covers the guard. Reference the owner slice when appropriate.

### What not to test

Do not use mocks as primary proof for server behavior that persists state.

Do not add tests for:

```text
- repository mock call order;
- handler mock call order;
- exact SaveChanges call count, unless existing project style requires it;
- generated TypeScript as behavior proof;
- OpenAPI generation as behavior proof;
- React Query invalidation;
- client button rendering;
- lifecycle behavior outside this slice.
```

Mocks can be helper-level/unit-level support, but the primary proof for L1 backend command behavior is API/integration + DB state assertions.

## 15. Server Test Plan Example For State-Changing Command

Good test-plan shape:

```text
## Test / Verification Plan

Primary verification: API integration tests with direct DB state assertions.

### API boundary / access tests
- unauthenticated request returns 401;
- missing selected entity returns documented rejection and leaves DB unchanged;
- not-owned selected entity returns documented rejection and leaves DB unchanged.

### Main DB state transition test
- arrange current/default entity and non-default entity;
- call command on non-default entity;
- reload rows from DB;
- assert selected is current/default;
- assert previous same-type current/default is unset;
- assert both rows still exist;
- assert stable fields unchanged.

### No-mutation test
- arrange existing request linked to old ApplicantParty;
- switch default/current;
- reload request row;
- assert request ApplicantPartyId/status/details unchanged.

### Cross-cutting concerns
- unsafe command uses CC-CSRF-001;
- generic CSRF failure branches are covered by cross-cutting tests;
- no local CSRF domain/application logic.

### What not to test
- no repository mock assertions;
- no handler call-order assertions;
- no client/UI/cache assertions.
```

Bad test-plan shape:

```text
- handler calls repository;
- repository method exists;
- SaveChanges is called;
- generated OpenAPI type exists;
- React Query invalidates cache.
```

Those are implementation details or client concerns, not proof of server slice behavior.
