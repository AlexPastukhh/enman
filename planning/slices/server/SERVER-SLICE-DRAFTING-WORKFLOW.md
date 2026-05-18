# Server Slice Drafting Workflow

Status: initial server workflow navigation

## Purpose

Server slice drafts define backend/API/domain behavior and generated contract impact.

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
9. Define integration tests and DB assertions.
10. Define OpenAPI/generated artifacts workflow.
```

## Guardrails

```text
FluentValidation validates shape.
Application/domain own authorization, visibility and lifecycle.
Do not manually set domain statuses in handlers when domain methods exist.
Do not add per-command status enums unless explicitly accepted.
Generated artifacts come from repo commands, not manual edits.
```
