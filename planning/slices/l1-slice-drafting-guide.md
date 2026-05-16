# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: business slices, cross-cutting/helper slices, client sidecars, implementation flow, questions, registers and tests

## 1. Draft-Driven Discovery Gate

Before drafting any slice, read:

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

All slice work uses draft-driven discovery:

```text
source requirements
-> draft
-> open questions/assumptions
-> visual flow maps
-> detailed flow
-> behavior coverage
-> implementation/test planning
-> local/shared register sync
-> next draft or implementation
```

This applies to:

```text
business slices
client sidecars
cross-cutting/helper slices
testing/support slices
documentation/status reconciliation drafts
```

## 2. Default Shortened Slice Draft

By default, slice drafts are created in a shortened working format.

A shortened draft is not the full final slice file.

It is an early working format for discussion, scenario logic checks, slice boundary checks, implementation direction, questions and coverage.

The default shortened draft includes:

```text
1. Visual Scenario Flow
2. Visual Implementation Flow
3. Questions / Decisions
4. Behavior Coverage
5. Test / Verification Plan, when useful
6. Covered Scenario Behavior Items
7. Next Step
```

Generate a full draft file only on explicit request or when the work needs:

```text
- final documentation pass;
- detailed review;
- debugging complex logic;
- transfer into planning docs;
- preparation for implementation by another agent.
```

A shortened draft may use temporary source behavior labels such as:

```text
Source BI TBD
```

only when source behavior item IDs have not been attached yet.

That marker is temporary and must be replaced with real scenario behavior IDs before finalizing the slice draft.

Reference example:

```text
planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
```

## 3. Visual Flow Rule

Visual flow means a diagram-like text map, not only a linear arrow list.

A linear list can be enough for a very early shortened draft, but full slice files should use visual maps when branching, responsibility boundaries or out-of-scope/dependent slices matter.

Visual maps should make it easy to see:

```text
- who acts;
- what the system decides;
- where success and failure branches split;
- what is inside current slice scope;
- what is delegated to dependent slices;
- where the implementation crosses API/application/domain/persistence/client-contract boundaries.
```

## 4. Visual Scenario Flow Rule

Visual Scenario Flow shows the real scenario flow, or the part of the scenario flow, covered by the slice.

It must describe user/system behavior, not controller, handler, repository, mapper or DTO mechanics.

A good Visual Scenario Flow usually includes:

```text
- actor box;
- system boundary box;
- happy path;
- important failure/no-write branches;
- success outcome;
- dependent/out-of-scope slice notes.
```

Bad scenario flow:

```text
[Controller]
Extracts claim
        ↓
[Repository]
Loads entity
        ↓
[DbContext]
Saves entity
```

That is implementation flow, not scenario flow.

## 5. Visual Implementation Flow Rule

Visual Implementation Flow shows how the slice implements the scenario behavior technically.

It should mark layers, folders or boundaries clearly.

For backend slices, include relevant technical boundaries:

```text
API Controller
Application Handler / Query Handler
Domain
Persistence
API Contract / ProblemDetails / Cookie Session
```

When the slice has failure/no-write behavior, show that branch explicitly.

For client sidecars, include client placement boundaries:

```text
Page / Route
Feature UI
Feature Model / Hook
Shared API
Generated Contracts
```

Code, DTO shapes and method names belong in the implementation flow only when they clarify:

```text
- contract boundary;
- disputed behavior;
- non-obvious implementation direction;
- API/client mapping;
- error handling;
- no-write/no-side-effect guarantee;
- testability/checkability.
```

Routine mechanics stay high-level.

## 6. Full Backend Slice File Rule

Full backend slice files must include visual flow maps before detailed flow sections.

Required order for backend parent slice files:

```text
## Visual Scenario Flow
diagram-like scenario map

## Scenario Slice Flow
detailed scenario/source behavior flow

## Visual Implementation Flow
diagram-like technical flow map

## Implementation Flow
detailed layer/API/application/domain/persistence flow
```

The visual sections do not replace the detailed sections.

They provide a quick review map before the detailed flow.

Full slice files should also include:

```text
- source scenario / source behavior items;
- API contract;
- questions and decisions;
- behavior coverage;
- test / verification plan;
- dependent or follow-up slices;
- shared register back-references when local questions are mirrored.
```

Reference example:

```text
planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
```

## 7. Scenario Behavior Items Rule

Behavior items are not invented inside the slice draft.

The slice must select behavior items from scenario specs, scenario behavior registers, DATA/UI specs, validation/security addenda or other accepted source files.

Correct chain:

```text
Scenario specs / source behavior items
        ↓
slice selects relevant behavior items
        ↓
draft describes how slice covers them
        ↓
behavior coverage links required behavior to draft sections
```

If source behavior item IDs are not found yet, an early shortened draft may use:

```text
Source BI TBD
```

Rules for `Source BI TBD`:

```text
- mark it as temporary;
- describe the source behavior in plain language;
- do not treat it as a final ID;
- replace it with real scenario behavior item IDs before finalizing the slice.
```

Client architecture placement is not a behavior item.

For example, choosing `features/create-request` vs `shared/api` belongs to Visual Implementation Flow or component discovery, not to Scenario Behavior Items.

## 8. Behavior Coverage Is Not Test Coverage

Behavior Coverage answers:

```text
Does the draft implementation description cover the required scenario behavior?
```

Test / Verification Plan answers:

```text
How will implemented code be verified later?
```

Do not mix these tables.

Behavior Coverage format:

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|

Test / Verification Plan format:

| Test / check | Verifies | Layer | Status |
|---|---|---|---|

## 9. Questions / Decisions Rule

Questions are not just a local note dump.

They are a review mechanism for unresolved behavior, API, implementation, client, test, security and architecture decisions.

In every `Questions / Decisions` section, list items in this order:

```text
1. open questions;
2. blocked or unresolved behavior / contract / design risks;
3. future-review questions that can affect later work;
4. assumptions / accepted directions that future work must remember;
5. resolved or superseded decisions, only when useful as history.
```

Every non-trivial question should include:

```text
ID
Question status
Question
Assumption / current direction
Impact
Shared register / local-only reason
```

Use these question status values:

| Status | Meaning |
|---|---|
| `open` | Needs an answer before related behavior/contract/design can be finalized. |
| `blocked` | Cannot be answered until another decision/source/implementation is available. |
| `assumption` | A working answer is being used so draft work can continue; user confirmation/refinement is expected. |
| `accepted direction` | Direction is accepted enough for planning and should be remembered by future work. |
| `future review` | Not a current blocker; revisit when the related future slice/hardening/client work starts. |
| `resolved` | Answered and no longer open. Keep only if useful for traceability. |
| `superseded` | Replaced by a newer question/decision/source. |
| `local only` | Intentionally not mirrored globally; local file must say why. |

Assumption rule:

```text
If a draft can proceed without a final answer, write a reasonable assumption/current direction.
The assumption must be explicit enough for the user to confirm, reject or refine.
Do not hide assumptions in prose.
Do not mark an assumption as resolved.
```

For already implemented slices, the assumption can be current repo evidence.

For early drafts, the assumption should be a proposed working answer:

```text
Draft assumes ... until user confirms/refines.
```

Example local question:

```text
### Q-REQ-001 — Should applicant party be verified before request creation?

Question status: future review
Question:
Should request creation require a verified applicant party?

Assumption / current direction:
Current backend does not require verification. Draft assumes verification is a separate policy/slice until user changes the target behavior.

Impact:
Can affect request creation validation, API errors, client UX and E2E coverage.

Shared register:
planning/slices/slice-questions-register.md / Q-SL-REQ-005
```

## 10. Shared Question/Register Rule

Local `Questions / Decisions` sections are required.

They keep local context.

Shared registers keep future work from losing important local discoveries.

Use:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Rules:

```text
- Mirror currently relevant local questions to `slice-questions-register.md`.
- Each mirrored question must have a clear question status.
- Each unresolved/non-final mirrored question must have an assumption/current direction.
- Mirror extension/change pressure to `slice-extension-points-register.md`.
- Mirror concrete future implementation/client/testing notes to `slice-implementation-notes-register.md`.
- If a question remains local only, state why.
- If a shared register row becomes stale, update or supersede it.
```

Local question format should include:

```text
Shared register:
- none, local only because ...
```

or:

```text
Shared register:
- planning/slices/slice-questions-register.md / Q-...
```

## 11. Client Sidecar Shortened Draft Rule

Client sidecars follow the same default shortened draft rule.

A client shortened draft contains:

```text
1. Visual UI / Scenario Flow
2. Visual Client Implementation Flow with layers/folders
3. Questions / Decisions
4. Behavior Coverage
5. Client/component/E2E verification plan
6. Covered Scenario/UI Behavior Items
7. Next Step
```

Client sidecar drafts must still follow generated contract rules:

```text
- use generated OpenAPI types for structure;
- use generated constants for semantic error/field/extension names;
- do not guess routes, DTOs, response shapes or error-code strings;
- record temporary contract gaps explicitly.
```

Do not create `.client.md` in advance.

Create or update it when concrete client work starts.

## 12. Shortened Draft Template

Use this template for early backend, business, helper or cross-cutting slice drafts:

```text
# SLICE-ID — Title — Early Short Draft

Status:
Slice type:
Scope:
Contract direction:
Response direction:
Source behavior items:

## 1. Visual Scenario Flow
## 2. Visual Implementation Flow
## 3. Questions / Decisions
## 4. Behavior Coverage
## 5. Test / Verification Plan
## 6. Covered Scenario Behavior Items
## 7. Next Step
```

The shortened draft `Questions / Decisions` section must still include question status and assumptions/current directions.

Use this template for early client sidecar drafts:

```text
# SLICE-ID.client — Title Client Sidecar — Early Short Draft

Status:
Slice type: client sidecar
Scope:
Source scenario/UI behavior items:
Contract sources:

## 1. Visual UI / Scenario Flow
## 2. Visual Client Implementation Flow
## 3. Questions / Decisions
## 4. Behavior Coverage
## 5. Client / Component / E2E Verification Plan
## 6. Covered Scenario / UI Behavior Items
## 7. Next Step
```

## 13. Full Backend Slice Template

Use this template for full backend/API/persistence slice files:

```text
# SLICE-ID — Title

Status:
Package:
Source scenario:
Slice type:
Current implementation status:

## 1. Slice Overview
## 2. Sources / Source Behavior Items
## 3. Visual Scenario Flow
## 4. Scenario Slice Flow
## 5. Visual Implementation Flow
## 6. Implementation Flow
## 7. API Contract
## 8. Questions / Decisions
## 9. Behavior Coverage
## 10. Test / Verification Plan
## 11. Dependent / Follow-up Slices
## 12. Implementation Checklist
```

The full draft `Questions / Decisions` section must include question status, assumptions/current directions and shared register back-references when practical.

## 14. Business Slice Intake Checklist

```text
1. Read architecture decision notes and ADR candidates.
2. Read draft-driven discovery principles.
3. Read slice questions, extension points and implementation notes registers.
4. Read planning/testing/ if tests/E2E/client test responsibilities are involved.
5. Read planning/api/ if API/client contract work is involved.
6. Read target scenario text spec and DATA file.
7. Read relevant behavior items and scenario UI spec if client-visible behavior is involved.
8. Check scenario questions register.
9. Check planning/slices/cross-cutting/ if the slice uses cross-cutting support.
10. Check planning/client/ and client architecture docs if client work is involved.
11. If scenario/API/constants/testing/security ambiguity exists, stop and resolve or record it first.
12. When a draft proceeds on an assumption, write the assumption explicitly.
```

## 15. Client Sidecar Draft Rule

Client sidecar drafts are created only when concrete client work starts.

A `.client.md` sidecar is the draft-driven discovery file for client implementation.

It must discover and track:

```text
- client behavior to implement;
- client questions/assumptions;
- contract used by client;
- route/page/feature/entity/shared mapping;
- generated OpenAPI types used;
- generated constants/error codes used;
- component placement;
- form/validation behavior;
- ProblemDetails/error mapping;
- client/component tests;
- E2E boundaries;
- shared register updates needed for future work.
```

## 16. API Contract Section

Parent slice API section should include:

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|

Contract status values:

```text
target L1
legacy/current support
temporary compatibility
internal/not client-facing
```

Also include:

```text
- ProblemDetails response statuses;
- client-facing error codes;
- generated constants used;
- OpenAPI generated types used by client;
- whether route is temporary legacy constants route or OpenAPI structural route.
```

## 17. Client Sidecar API Section

`.client.md` should include:

| Client API function | Endpoint | Generated OpenAPI type(s) used | Error constants used | Status |
|---|---|---|---|---|

## 18. Maybe / Optional Repository Result Rule

When a slice uses repository/query reads where absence is a normal possible outcome, read:

```text
planning/slices/shared/maybe-for-optional-results.md
```

Target rule:

```text
Repository/query API returns Maybe<T>.
Application handler maps Maybe.None to the use-case meaning.
```

Use this especially for:

```text
GetByIdAsync
GetByEmailAsync
GetCurrentActive...Async
Find...Async
```

Slice implementation flow should say both parts:

```text
[Repository]
returns Maybe<T> for optional lookup
        ↓
[Application Handler]
maps Maybe.None to invalid credentials / not found / validation error / empty state
```

If the current implementation still uses nullable returns, document it honestly:

```text
Current implementation returns nullable `T?`.
Target convention for new/refactored repository APIs: `Maybe<T>`.
```

Do not overclaim Maybe usage before code has been refactored.

## 19. Cross-Cutting / Helper Slice Intake Checklist

```text
1. Identify source requirement type:
   security / API contract / tooling / testing / infrastructure / client-cross-cutting.
2. Check whether a specification/addendum/source note already exists.
3. Create or update concern-derived behavior items.
4. Ensure every behavior item appears in Concern Slice Flow.
5. Only after the concern flow, write Implementation Flow.
6. Add coverage, test/check plan, consumer rule, local questions and ADR impact.
7. Sync relevant local questions/notes to shared registers.
8. Update cross-cutting index and relevant navigation.
```

## 20. Test Coverage Sections

Parent slice and `.client.md` should separate:

```text
Client/component tests
Server integration/API tests
End-to-end tests
```

Use:

```text
planning/testing/testing-principles.md
planning/testing/e2e-testing-workflow.md
```

## 21. E2E Coverage Table

If E2E is relevant, include:

| E2E test | Scenario/UI items | Cross-layer purpose | Setup | User path | Server communication checked | Expected result | Status |
|---|---|---|---|---|---|---|---|

E2E is relevant when the behavior requires proof of:

```text
browser -> client -> HTTP API -> server/application/domain/persistence/session -> visible outcome
```

Do not use E2E to exhaustively test client-visible UI behavior.

## 22. Client Test Coverage Table

If client-visible UI behavior is involved, include:

| Client/component test | Behavior/UI items | Client behavior covered | Locator/accessibility focus | Status |
|---|---|---|---|---|

Detailed UI validation belongs here, not in happy-path E2E.

## 23. Business Slice Flow Rule

Business slices use:

```text
Scenario-derived behavior items
-> Visual Scenario Flow
-> Scenario Slice Flow
-> Visual Implementation Flow
-> Implementation Flow
```

## 24. Cross-Cutting / Helper Slice Flow Rule

Cross-cutting/helper slices use:

```text
Concern-derived behavior items
-> Concern Slice Flow
-> Implementation Flow
```

Concern-derived behavior item source can be:

```text
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

These items are first-class behavior items and must be covered by the concern flow.

For early shortened drafts, Concern Slice Flow may be represented as a visual concern/scenario flow when that is clearer.

## 25. Cross-Cutting / Helper Slice Template

```text
# CC-XXX — Title

Status:
Slice type: cross-cutting slice / helper slice
Layers:
Depends on:
Used by:

## 1. Purpose
## 2. Why This Is A Cross-Cutting/Helper Slice
## 3. Inputs / Sources
## 4. Concern-Derived Behavior Items
## 5. Coverage Overview
## 6. Concern Slice Flow
## 7. Implementation Flow
## 8. Target Types / Components
## 9. Test / Check Plan
## 10. Consumer Rule For Business Slices
## 11. Local Questions
## 12. ADR Impact
```

For early cross-cutting/helper discovery, use the shortened draft format first unless the user explicitly asks for the full template.

## 26. Implementation Flow Detail Rule

Implementation flow may include involved classes, methods and short code snippets.

Include them when they explain:

```text
- contract boundary;
- non-obvious behavior;
- behavior that was discussed/questioned;
- important trade-off;
- extension/change point;
- error handling;
- testability/checkability;
- no-write/no-side-effect guarantee;
- generated artifact shape;
- API/client boundary.
```

Keep routine mechanics high-level.

If details make the flow noisy, extract them into a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 27. Constants Consumer Rule

If a business slice introduces client-facing error codes, read:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 28. OpenAPI Consumer Rule

If a business/client slice uses server API, read:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

## 29. CSRF Consumer Rule

If a business slice introduces browser unsafe API command, read:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Parent slice API/security note:

```text
Unsafe browser API requests are protected by CC-CSRF-001.
```
