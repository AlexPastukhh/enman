# Client Slice Planning

Status: local client slice folder entry point

This folder contains client slice templates, client-specific handoff docs and client slice drafts.

Reusable client rules, principles and workflows live in the root slice folder:

```text
planning/slices/
```

## Required read order

Before drafting or updating a client slice, start from the root slice docs:

```text
planning/slices/README.md
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/client-implementation-principles.md
planning/slices/client-css-architecture-rules.md
planning/slices/client-form-validation-implementation-principles.md
planning/slices/client-a11y-implementation-principles.md
planning/slices/client-ui-style-workflow.md
planning/slices/slice-test-plan-workflow.md
```

Then use local client docs:

```text
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
