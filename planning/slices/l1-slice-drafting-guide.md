# L1 Slice Drafting Guide

Status: current slice drafting workflow / scope-boundary rule, validation and client sidecar synchronized  
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
```

All slice work uses draft-driven discovery:

```text
source requirements
-> source flow / behavior item intake
-> scope / out-of-scope / related-slice boundary
-> draft
-> open questions/assumptions
-> visual flow maps
-> extension/change point review
-> detailed flow
-> behavior coverage
-> implementation/test planning
-> local/shared register sync
-> next draft or implementation
```

## 2. Scenario Flow / Behavior Item Source Rule

Before writing:

```text
Visual Scenario Flow
Scenario Slice Flow
Visual UI / Scenario Flow
Behavior Coverage
Covered Scenario / UI Behavior Items
```

read:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Then read the linked sources:

```text
[SCENARIO]      scenario text spec
[DATA]          scenario DATA spec
[UI-SCENARIO]   scenario UI spec
[BEHAVIOR]      scenario behavior items file
[SECURITY]      scenario security/validation addendum
[CONCERN]       cross-cutting concern behavior items
[CLARIFICATION] scenario clarification
```

Do not invent behavior items inside slice drafts.

Do not use `slice-questions-register.md`, `slice-extension-points-register.md` or `slice-implementation-notes-register.md` as the source for Scenario Flow or Behavior Items.

If source behavior IDs are missing, mark `[SOURCE-GAP]` and use temporary `Source BI TBD` only in early drafts.

## 3. Default Shortened Slice Draft

The default shortened draft includes:

```text
1. Scope / Out of Scope / Related Slices
2. Visual Scenario Flow
3. Visual Implementation Flow
4. Questions / Decisions
5. Extension / Change Points, when relevant
6. Behavior Coverage
7. Test / Verification Plan, when useful
8. Next Step
9. Scenario Flow / Behavior Items, only when source IDs are not attached yet
```

A shortened draft must still prevent scope creep. If it excludes important work, it must say where that excluded work belongs.

## 4. Scope / Out-of-Scope / Related-Slices Rule

Every non-trivial slice draft must explicitly define:

```text
Scope
Out of scope
Related slices / owners
Future extension points
```

`Scope` says what the slice is responsible for.

`Out of scope` says what the slice must not implement.

`Related slices / owners` maps excluded responsibilities to:

```text
- an existing slice;
- a future slice;
- a cleanup task;
- a client sidecar;
- a cross-cutting/helper slice;
- or explicit "not planned".
```

`Future extension points` records known pressure that may come later, but must not be implemented in the current slice.

Out-of-scope lists must not be vague. For every important out-of-scope item, point to its owner.

Example:

```text
Scope:
- account-level ApplicantParty read endpoint;
- return all owned ApplicantParties;
- expose isCurrentDefault marker;
- include summary/card data.

Out of scope:
- create ApplicantParty -> SL-APPL-001;
- make default/current -> SL-APPL-003;
- request creation applicant context -> SL-REQ-001;
- delete/archive lifecycle -> future ApplicantParty lifecycle slices;
- client UI -> future SL-APPL-002.client or current ApplicantParty page sidecar;
- domain field rename IsCurrentActiveVersion -> cleanup/default-template naming task.
```

Purpose:

```text
Prevent a read slice from expanding into command implementation, client UI, deletion lifecycle,
default switching, domain cleanup, generated/client work beyond required contract checks,
or unrelated scenario cleanup.
```

Implementation prompts derived from a slice draft must preserve the slice `Scope`, `Out of scope`, `Related slices` and `Future extension points`.

## 5. Visual Scenario Flow Rule

Visual Scenario Flow shows user/system behavior, not controller/handler/repository mechanics.

It must be sourced from scenario text, DATA, UI spec and behavior item files.

## 6. Visual Implementation Flow Rule

Visual Implementation Flow shows how the slice implements scenario behavior technically.

For backend slices, include:

```text
API Controller
Request-level validation / FluentValidation, when API input/query validation exists
Application Handler / Query Handler
Domain
Persistence
API Contract / ProblemDetails / Cookie Session
```

For client sidecars, include architecture/folder placement boundaries:

```text
Route / Page
Feature UI
Feature Model / Hook
Feature API Mapper
Shared API
Generated Contracts
Feedback/Error surface when relevant
```

## 7. Server Request Validation Rule

If a backend/API slice introduces or changes request body/query input, read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
planning/api/fluentvalidation-error-code-policy-note.md
```

Slice API Contract sections should separate:

```text
FluentValidation / request-shape validation
Application/domain validation
```

Use FluentValidation for:

```text
- required DTO fields;
- basic request/query shape;
- branch/discriminator validity;
- mutually exclusive fields;
- allowed query values;
- nested DTO required structure.
```

Keep in handlers/domain:

```text
- account existence;
- ownership;
- selected entity belongs to current account;
- domain value object invariants;
- state transitions;
- no-write/atomicity.
```

Visual Implementation Flow should show `[FluentValidation]` before `[Application Handler]` when request-shape rules exist.

For read endpoints without request body/query input, explicitly say that no 422 request-shape validation is expected unless route/query validation is introduced.

## 8. Scenario Behavior Items Rule

Behavior items are selected from source files:

```text
planning/diagrams/scenario-behavior-items/
```

Client sidecars should also consume `[UI-SCENARIO]` behavior items from:

```text
planning/diagrams/scenario-ui-specs/
```

Client architecture placement is not a behavior item.

## 9. Behavior Coverage Is Not Test Coverage

Behavior Coverage answers:

```text
Does the draft implementation description cover the required source behavior?
```

Test / Verification Plan answers:

```text
How will implemented code or UI behavior be verified later?
```

Do not mix these tables.

## 10. Questions / Decisions Rule

Implemented slices can still have open questions.

Question order:

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

## 11. Shared Register Sync Rule

Use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Rules:

```text
- Update source-flow/behavior register when new scenarios, UI specs, behavior items, slices or sidecars are added.
- Mirror currently relevant local questions to `slice-questions-register.md`, including questions from implemented slices.
- Mirror extension/change pressure to `slice-extension-points-register.md`.
- Mirror concrete future implementation/client/testing notes to `slice-implementation-notes-register.md`.
- If a question remains local only, state why.
- If a shared register row becomes stale, update or supersede it.
```

## 12. Client Sidecar Rules

A client sidecar contains:

```text
1. Sidecar Overview
2. Sources / Source Behavior Items
3. Scope / Out of Scope / Related Slices
4. Visual UI / Scenario Flow
5. UI Slice Flow
6. Visual Client Implementation Flow
7. Client Implementation Flow
8. Client API / Generated Contract
9. Questions / Decisions
10. Client Extension / Change Points, when relevant
11. Behavior Coverage
12. Client / Component / E2E Verification Plan
13. Covered Scenario / UI Behavior Items
14. Dependent / Follow-up Slices
15. Implementation Checklist
```

Do not create `.client.md` in advance.

Create or update it when concrete client work starts or when implemented client logic must be documented and reconciled.

## 13. My Requests Filter Sidecar Rule

List filters are not one-off controls.

For My Requests:

```text
Page owns URL query params.
Filter feature owns controls and parse/serialize helpers.
Entity query accepts a filter object.
Shared API maps supported filters to query string.
```

Status is the first supported filter. Future filters extend the filter object only when backend and source behavior support them.

## 14. Full Backend Slice Template

```text
# SLICE-ID — Title

Status:
Package:
Source scenario:
Slice type:
Current implementation status:

## 1. Slice Overview
## 2. Sources / Source Behavior Items
## 3. Scope / Out of Scope / Related Slices
## 4. Visual Scenario Flow
## 5. Scenario Slice Flow
## 6. Visual Implementation Flow
## 7. Implementation Flow
## 8. API Contract
## 9. Questions / Decisions
## 10. Extension / Change Points, when relevant
## 11. Behavior Coverage
## 12. Test / Verification Plan
## 13. Client / Consumer Notes, when relevant
## 14. Dependent / Follow-up Slices
## 15. Implementation Checklist
```

## 15. Full Client Sidecar Template

```text
# SLICE-ID.client — Title

Status:
Parent slice:
Slice type: client sidecar
Source scenario/UI sources:
Current implementation status:

## 1. Sidecar Overview
## 2. Sources / Source Behavior Items
## 3. Scope / Out of Scope / Related Slices
## 4. Visual UI / Scenario Flow
## 5. UI Slice Flow
## 6. Visual Client Implementation Flow
## 7. Client Implementation Flow
## 8. Client API / Generated Contract
## 9. Questions / Decisions
## 10. Client Extension / Change Points, when relevant
## 11. Behavior Coverage
## 12. Client / Component / E2E Verification Plan
## 13. Covered Scenario / UI Behavior Items
## 14. Dependent / Follow-up Slices
## 15. Implementation Checklist
```

## 16. Business Slice Intake Checklist

```text
1. Read planning/README.md.
2. Read planning/agent-scope-boundaries-and-prompt-safety.md.
3. Read scenario source files through `slice-scenario-flow-behavior-register.md`.
4. Read draft-driven discovery and this guide.
5. Read slice questions, extension points and implementation notes registers.
6. Define Scope / Out of scope / Related slices / Future extension points.
7. Read planning/testing/ if tests/E2E/client test responsibilities are involved.
8. Read planning/api/ if API/client contract work is involved.
9. If server API input validation is involved, read CC-VALIDATION-001.
10. If scenario/API/constants/testing/security/extension ambiguity exists, record questions and assumptions first.
```

## 17. Consumer Rules

If a business/client slice uses server API, read:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

If a business slice has request body/query validation, read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

If a client slice needs feedback/messages, read:

```text
planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
```

If a business slice introduces browser unsafe API command, read:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```
