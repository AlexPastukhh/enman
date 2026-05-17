# L1 Slice Drafting Guide

Status: current / flow separation, cross-cutting concerns and client API placement synchronized

## 1. Read Before Drafting

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/client/client-api-placement-decision.md
planning/client/client-layering-for-read-and-command-slices.md
```

## 2. Scenario Flow vs Implementation Flow

Scenario Flow shows user/system behavior from scenario specs.

Implementation Flow shows code/layer responsibilities.

Implementation details are not behavior items.

## 3. Client API Placement Rule

Read client sidecar:

```text
pages + entities + shared transport/generated infrastructure
```

Command client sidecar:

```text
pages + features + entities + shared transport/generated infrastructure
```

Read endpoint wrappers belong in:

```text
entities/<entity>/api
```

Command/mutation endpoint wrappers belong in:

```text
features/<business-action>/api
```

`shared/api` owns:

```text
fetchJson
ProblemDetails / ApiError
CSRF/antiforgery transport helpers
generated OpenAPI types
generic transport helpers
```

`shared/api` does not own new business-specific endpoint wrappers.

## 4. Client Short Draft Template

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
## 7. Cross-Cutting Concerns / Considerations
## 8. Questions / Decisions
## 9. Extension / Change Points
## 10. Behavior Coverage
## 11. Client / Component / E2E Verification Plan
## 12. Implementation Checklist
## 13. Next Step
```

## 5. Server Test Rule

For backend reads, primary proof is API/read integration tests.

For backend state-changing commands, primary proof is API/integration + DB state assertions.

Do not use repository/handler mocks as primary behavior proof.
