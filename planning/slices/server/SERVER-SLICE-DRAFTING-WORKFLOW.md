# Server Slice Drafting Workflow

Status: current server workflow / scenario source and test trace synchronized

## Purpose

Server slice drafts define backend/API/domain behavior and generated contract impact.

## Before Drafting

Read:

```text
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/slice-test-plan-workflow.md
```

Then identify scenario sources:

```text
business scenario:
cross-cutting behavior:
data source:
behavior items:
concern umbrella:
```

## Drafting order

```text
1. Identify scenario/behavior source.
2. Define endpoint/command/read purpose.
3. Define auth/session/CSRF boundary.
4. Define request/response contract.
5. Define FluentValidation shape boundary.
6. Define application/domain ownership.
7. Define persistence/read model changes.
8. Define error mapping.
9. Define behavior coverage.
10. Define Behavior-to-Test Trace and test plan.
11. Define OpenAPI/generated artifacts workflow.
```

## Test / Verification Rule

Primary proof should use public boundary and persisted/observable outcome.

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

Direct DB setup is allowed only to arrange scenario preconditions.

Direct DB assertions are allowed to observe persisted behavior outcome.

Repository mocks, handler call order and SaveChanges count are not primary proof.

## Guardrails

```text
FluentValidation validates shape.
Application/domain own authorization, visibility and lifecycle.
Do not manually set domain statuses in handlers when domain methods exist.
Do not add per-command status enums unless explicitly accepted.
Generated artifacts come from repo commands, not manual edits.
```
