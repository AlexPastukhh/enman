# L1 Slice Drafting Guide

Status: current slice drafting workflow / validation and client sidecar synchronized  
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
1. Visual Scenario Flow
2. Visual Implementation Flow
3. Questions / Decisions
4. Extension / Change Points, when relevant
5. Behavior Coverage
6. Test / Verification Plan, when useful
7. Next Step
8. Scenario Flow / Behavior Items, only when source IDs are not attached yet
```

## 4. Visual Scenario Flow Rule

Visual Scenario Flow shows user/system behavior, not controller/handler/repository mechanics.

It must be sourced from scenario text, DATA, UI spec and behavior item files.

## 5. Visual Implementation Flow Rule

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

## 6. Server Request Validation Rule

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

## 7. Scenario Behavior Items Rule

Behavior items are selected from source files:

```text
planning/diagrams/scenario-behavior-items/
```

Client sidecars should also consume `[UI-SCENARIO]` behavior items from:

```text
planning/diagrams/scenario-ui-specs/
```

Client architecture placement is not a behavior item.

## 8. Behavior Coverage Is Not Test Coverage

Behavior Coverage answers:

```text
Does the draft implementation description cover the required source behavior?
```

Test / Verification Plan answers:

```text
How will implemented code be verified later?
```

Do not mix these tables.

## 9. Questions / Decisions Rule

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

## 10. Shared Register Sync Rule

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

## 11. Client Sidecar Rules

A client sidecar contains:

```text
1. Sidecar Overview
2. Sources / Source Behavior Items
3. Visual UI / Scenario Flow
4. UI Slice Flow
5. Visual Client Implementation Flow
6. Client Implementation Flow
7. Client API / Generated Contract
8. Questions / Decisions
9. Client Extension / Change Points, when relevant
10. Behavior Coverage
11. Client / Component / E2E Verification Plan
12. Covered Scenario / UI Behavior Items
13. Dependent / Follow-up Slices
14. Implementation Checklist
```

Do not create `.client.md` in advance.

Create or update it when concrete client work starts or when implemented client logic must be documented and reconciled.

## 12. My Requests Filter Sidecar Rule

List filters are not one-off controls.

For My Requests:

```text
Page owns URL query params.
Filter feature owns controls and parse/serialize helpers.
Entity query accepts a filter object.
Shared API maps supported filters to query string.
```

Status is the first supported filter. Future filters extend the filter object only when backend and source behavior support them.

## 13. Full Backend Slice Template

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
## 9. Extension / Change Points, when relevant
## 10. Behavior Coverage
## 11. Test / Verification Plan
## 12. Dependent / Follow-up Slices
## 13. Implementation Checklist
```

## 14. Full Client Sidecar Template

```text
# SLICE-ID.client — Title

Status:
Parent slice:
Slice type: client sidecar
Source scenario/UI sources:
Current implementation status:

## 1. Sidecar Overview
## 2. Sources / Source Behavior Items
## 3. Visual UI / Scenario Flow
## 4. UI Slice Flow
## 5. Visual Client Implementation Flow
## 6. Client Implementation Flow
## 7. Client API / Generated Contract
## 8. Questions / Decisions
## 9. Client Extension / Change Points, when relevant
## 10. Behavior Coverage
## 11. Client / Component / E2E Verification Plan
## 12. Covered Scenario / UI Behavior Items
## 13. Dependent / Follow-up Slices
## 14. Implementation Checklist
```

## 15. Business Slice Intake Checklist

```text
1. Read planning/README.md.
2. Read planning/agent-scope-boundaries-and-prompt-safety.md.
3. Read scenario source files through `slice-scenario-flow-behavior-register.md`.
4. Read draft-driven discovery and this guide.
5. Read slice questions, extension points and implementation notes registers.
6. Read planning/testing/ if tests/E2E/client test responsibilities are involved.
7. Read planning/api/ if API/client contract work is involved.
8. If server API input validation is involved, read CC-VALIDATION-001.
9. If scenario/API/constants/testing/security/extension ambiguity exists, record questions and assumptions first.
```

## 16. Consumer Rules

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
