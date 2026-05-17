# Slice Planning Index

Status: current slice-planning navigation index / client API placement synchronized

## 1. Core Rule

Scenario Flow and Behavior Items come from:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Client drafters must use:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
planning/client/client-api-placement-decision.md
planning/client/client-layering-for-read-and-command-slices.md
```

## 2. Current Client API Placement Rule

```text
Read endpoint wrappers:
  entities/*/api

Command/mutation endpoint wrappers:
  features/*/api

Shared API:
  fetchJson
  ProblemDetails / ApiError
  CSRF/antiforgery helpers
  generated OpenAPI types
  generic transport helpers
```

Existing business-specific wrappers in `shared/api` are transitional compatibility. New drafts should not copy that shape.

## 3. Drafting Rules

```text
- Draft by examples, not by improvisation.
- Scenario Flow is user/system behavior from scenario sources.
- Implementation Flow is code/layer responsibility.
- Behavior items are not implementation details.
- Read-only UI belongs in entities.
- Command/user-action UI belongs in features.
- Read endpoint wrappers belong in entities/*/api.
- Command endpoint wrappers belong in features/*/api.
- shared/api is only transport/generated infrastructure.
- One draft covers one slice; extension slices are named but not implemented.
```
