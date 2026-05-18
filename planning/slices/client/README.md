# Client Slice Planning

Status: canonical client slice planning entry point / cross-cutting behavior and testing workflow synchronized

This folder owns client slice drafting rules, UI/CSS workflow, client implementation handoff rules, examples and client sidecar drafts.

## Required read order

```text
planning/slices/client/CLIENT-API-PLACEMENT-DECISION.md
planning/slices/client/CLIENT-LAYERING-FOR-READ-AND-COMMAND-SLICES.md
planning/slices/client/CLIENT-UI-STYLE-WORKFLOW.md
planning/slices/client/CLIENT-CSS-ARCHITECTURE-RULES.md
planning/slices/client/CLIENT-FORM-VALIDATION-WORKFLOW.md
planning/slices/client/CLIENT-A11Y-WORKFLOW.md
planning/slices/slice-test-plan-workflow.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
planning/slices/client/CLIENT-SLICE-IMPLEMENTATION-HANDOFF.md
```

## Scenario source requirement

Before drafting or updating a client slice, identify scenario sources:

```text
business scenario
UI scenario
cross-cutting behavior
data source
behavior items
concern umbrella if applicable
```

If the slice changes meaningful UI behavior, check:

```text
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-ui-specs/UI-SCENARIO-READINESS.md
```

If the slice implements common client behavior used across scenarios, check:

```text
planning/diagrams/scenario-cross-cutting/client-behavior/
```

## Client slice placement

Read slices use:

```text
pages + entities + shared transport/generated infrastructure
```

Command/user-action slices use:

```text
pages + features + entities + shared transport/generated infrastructure
```

Widgets may be used for larger reusable composed UI blocks shared across pages.

## API wrapper ownership

```text
entities/*/api
  read endpoint wrappers

features/*/api
  command endpoint wrappers

shared/api
  fetchJson, ProblemDetails/ApiError, CSRF helpers, generated OpenAPI types only
```

Do not add business-specific endpoint wrappers to `shared/api`.

## Cross-cutting client drafts

Client-side cross-cutting implementation drafts go here:

```text
planning/slices/client/cross-cutting/
```

If no server counterpart is expected, use `SINGLE-` prefix:

```text
SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md
```

If a server counterpart is expected or possible, use paired logical ID and `.client.md` suffix:

```text
CC-SEC-CSRF-001-unsafe-command-protection.client.md
```

## UI/CSS requirement

Client slice drafts must describe:

```text
Visual UI / Scenario Flow
Visual Layout / Screen Composition
Visual Client Implementation Flow
Styling / CSS Ownership
Validation / Feedback / Error UI
Accessibility / ARIA Contract
Test / Verification Plan with Behavior-to-Test Trace
```

CSS is part of client slice implementation ownership.

## Draft locations

New client slice drafts go directly under:

```text
planning/slices/client/
```

Client-side cross-cutting slice drafts go under:

```text
planning/slices/client/cross-cutting/
```

Do not create new L1/L2 folders for client slice docs.

Historical client drafts may remain in old folders during migration. Keep `planning/slices/SLICE-INDEX.md` updated until migration is complete.
