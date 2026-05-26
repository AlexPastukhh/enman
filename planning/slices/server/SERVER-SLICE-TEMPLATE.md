# Server Slice Template

Status: canonical template for server/backend/API slice drafts  
Scope: copyable structure for server slice drafts using current slice authoring and server implementation principles

Use with:

```text
planning/slices/slice-draft-authoring-principles.md
planning/slices/server/server-implementation-principles.md
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/slice-test-plan-workflow.md
```

````markdown
# <SLICE-ID> — <Title>

Status:
Logical slice:
Package:
Slice type:
Primary purpose:
Implementation status:
Runtime implementation checked: yes/no

Depends on:

## 0. Scenario Sources / Source Sync

Scenario/source files:
  -

Slice source mapping:
  - planning/slices/slice-scenario-flow-behavior-register.md

Domain files:
  -

Behavior items:
  -

Cross-cutting behavior / concern sources:
  -

Source status:
  current / provisional / pending source sync / blocked

Notes:

## 1. Scope

This slice implements:

```text
<short behavior-first scope>
```

Scope rules:

```text
- Scope comes from scenario/source mapping, not from a random file list.
- Scope must be a verifiable behavior subset.
- This draft should not become a full scenario dependency map.
```

## 2. Scenario Scope / Slice Boundary

### 2.1 Scenario Identity

Scenario:

Scenario purpose:

Scenario source status:

### 2.2 This Slice Scope Inside The Scenario

This slice implements this part of the scenario:

```text
-
```

### 2.3 Scenario Behavior Not Implemented By This Slice

| Scenario/source behavior not in this slice | Owner / destination | Reason |
|---|---|---|

### 2.4 Cross-Cutting Concerns For This Scenario

| Cross-cutting concern | Applies to scenario? | Applies to this slice? | Owner / rule |
|---|---:|---:|---|

## 3. Slice Relations And Future-Change Check

### 3.1 Slice Prerequisites

This slice depends on:

```text
-
```

### 3.2 Planned Follow-Up Slices

Known planned follow-ups:

```text
-
```

### 3.3 Future-Change Check

| Possible future change | Type | What to account for now |
|---|---|---|

## 4. Domain Methods / Domain Behavior Contract

Domain aggregate / model:

Domain methods used:

```text
-
```

Expected domain behavior:

```text
-
```

Domain must own:

```text
ownership / participant rules;
lifecycle / current turn / status rules;
state transitions;
impossible-state protection;
no partial mutation on failed transition.
```

Domain/application behavior that must not be moved into controller/validator:

```text
-
```

## 5. Implementation Components Overview

Component/class/method names in this draft are semantic first-pass names.

```text
If final runtime names differ but responsibilities and behavior stay equivalent,
that is not implementation drift.
```

| Component | Semantic name | Responsibility |
|---|---|---|
| Controller endpoint / method |  | HTTP boundary, auth/role guard, CSRF attribute, route/DTO binding, current session extraction, response mapping |
| DTO / request model |  | Request shape |
| DTO validator |  | Shape validation only |
| Application service / handler |  | Orchestration, current actor/load/save, domain calls, error mapping |
| Repository / load method |  | Load aggregate/read model with required state |
| Domain aggregate/model |  | Invariants, lifecycle, state transitions |
| Persistence / unit of work |  | Atomic persistence boundary |

## 6. Implementation Flow

Describe responsibility by component.

```text
[Controller endpoint / <semantic name>]
-

[DTO validator / <semantic name>]
-

[Application service / handler / <semantic name>]
-

[Domain aggregate / <semantic name>]
-

[Persistence / <semantic name>]
-

[Response]
-
```

## 7. API Contract

Endpoint:

Auth:

CSRF:

Request:

Success response:

Failure responses:

OpenAPI impact:

Generated artifacts impact:

## 8. Application Result Model

Application result shape:

```text
Result / UnitResult / Maybe / error mapping / 204 / DTO
```

Absence / not found / forbidden / validation / domain failure mapping:

```text
-
```

## 9. DTO Validation

Shape validation:

```text
-
```

Domain/application validation:

```text
-
```

Rule:

```text
DTO validators own request/query shape only.
Ownership, lifecycle, current turn, status and visibility belong to application/domain.
```

## 10. Repository / Persistence Notes

Load pattern:

Persistence boundary:

No-partial-write rule:

Read/write split notes:

Current implementation evidence checked:

## 11. Cross-Cutting Concerns / Considerations

Implementation-level concerns only. Scenario-level cross-cutting concern applicability belongs in section 2.4.

| Concern | Applies? | Rule for this slice | Owner/source |
|---|---:|---|---|
| Auth/session |  |  |  |
| CSRF / unsafe request protection |  |  |  |
| ProblemDetails / validation mapping |  |  |  |
| Transaction / atomicity |  |  |  |
| No partial write |  |  |  |
| OpenAPI/generated artifacts |  |  |  |

## 12. Questions / Decisions

| ID | Question / Decision | Status | Current direction | Impact |
|---|---|---|---|---|

## 13. Behavior Coverage

Behavior Coverage is not Scope and not Test Plan.

| Behavior | Status | Covered by / delegated to | Notes |
|---|---|---|---|

Status examples:

```text
covered
covered boundary
out of scope
future
delegated
blocked
```

## 14. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
Required assertions live inside Behavior-to-Test Trace.
```

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

### 14.1 Testing Boundary

What this slice must prove:

```text
-
```

What not to test here:

```text
-
```

### 14.2 Behavior-to-Test Trace

| Behavior | Outcome proved | Test layer | Setup/action mechanism | Required assertions | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|---|

### 14.3 Generated/API Verification

```text
OpenAPI generation/check:
TypeScript client generation/check:
Constants/error codes generation/check:
check:api or equivalent:
```

## 15. Backend Implementation Direction / Sync Checklist

Use only when preparing for implementation or syncing with already implemented code.

```text
[ ] Confirm endpoint/route is present or planned.
[ ] Confirm success response matches draft.
[ ] Confirm auth/session/role boundary.
[ ] Confirm CSRF policy if unsafe command.
[ ] Confirm controller does not own lifecycle/domain state rules.
[ ] Confirm DTO validator checks shape only.
[ ] Confirm aggregate/load path includes required state.
[ ] Confirm application/domain responsibilities match draft.
[ ] Confirm persistence boundary and no-partial-write behavior.
[ ] Confirm tests assert required outcomes.
[ ] Mark runtime implementation checked yes/no.
```

## 16. Guardrail Summary

```text
- Do not broaden scope silently.
- Do not implement out-of-scope scenario behavior.
- Do not invent behavior items locally.
- Do not use UI visibility as a security boundary.
- Do not move lifecycle/domain rules into DTO validator/controller.
- Do not treat harmless naming differences as drift.
- Do not leave required assertions outside Behavior-to-Test Trace.
```
````
