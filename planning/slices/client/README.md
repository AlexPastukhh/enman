# Client Slice Planning

Status: canonical client slice planning entry point

This folder owns client slice drafting rules, UI/CSS workflow, client implementation handoff rules, examples and client sidecar drafts.

## Required read order

```text
planning/slices/client/CLIENT-API-PLACEMENT-DECISION.md
planning/slices/client/CLIENT-LAYERING-FOR-READ-AND-COMMAND-SLICES.md
planning/slices/client/CLIENT-UI-STYLE-WORKFLOW.md
planning/slices/client/CLIENT-CSS-ARCHITECTURE-RULES.md
planning/slices/client/CLIENT-FORM-VALIDATION-WORKFLOW.md
planning/slices/client/CLIENT-A11Y-WORKFLOW.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
planning/slices/client/CLIENT-SLICE-IMPLEMENTATION-HANDOFF.md
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

## UI/CSS requirement

Client slice drafts must describe:

```text
Visual UI / Scenario Flow
Visual Layout / Screen Composition
Visual Client Implementation Flow
Styling / CSS Ownership
Validation / Feedback / Error UI
Accessibility / ARIA Contract
Verification Plan
```

CSS is part of client slice implementation ownership.

## Draft locations

New client slice drafts go directly under:

```text
planning/slices/client/
```

Do not create new L1/L2 folders for client slice docs.

Historical client drafts may remain in old folders during migration. Keep `planning/slices/SLICE-INDEX.md` updated until migration is complete.
