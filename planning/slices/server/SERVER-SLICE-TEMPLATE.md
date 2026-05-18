# Server Slice Template

Status: canonical template for server/backend/API slice drafts

```markdown
# <SLICE-ID> — <Title>

Status:
Logical slice:
Package:
Slice type:
Primary purpose:

Depends on:

## 0. Scenario Sources

Business scenario:
Cross-cutting behavior:
Data source:
Behavior items:
Concern umbrella:

## 1. Slice Overview

## 2. Scope

## 3. Out of Scope

## 4. Scenario Flow

Describe only the scenario portion relevant to this slice.

## 5. Implementation Flow

Describe domain/application/API/persistence responsibility.

## 6. API Contract

Endpoint:
Auth:
CSRF:
Request:
Success response:
Failure responses:
OpenAPI impact:

## 7. Validation / FluentValidation

Shape validation:
Domain/application validation:
Error mapping:

## 8. Domain Rules

## 9. Application / Handler Direction

## 10. Security / Protection

Auth/session/visibility/ownership/CSRF concerns.

## 11. Behavior Coverage

| Behavior item | Scenario/source meaning | Covered by this slice? | Notes |
|---|---|---|---|

## 12. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

### Behavior-to-Test Trace

| Behavior item | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|

Required considerations:

```text
public boundary proof;
persisted/read-model outcome;
no-mutation on failed command;
validation contract;
auth/ownership/visibility;
CSRF smoke for unsafe command family if relevant;
OpenAPI/generated checks if API shape changes.
```

Direct DB setup is allowed only for scenario preconditions.

Direct DB assertions are allowed to observe persisted behavior.

Repository mocks, handler call order and SaveChanges count are not primary proof.

## 13. OpenAPI / Generated Artifacts

## 14. Implementation Checklist

## 15. Guardrail Summary
```
