# Slice Planning Index

Status: canonical slice planning entry point / current root navigation

This folder owns slice-layer navigation for server, client and cross-cutting slice work.

This README is the entry point and read order. It is not the full ownership map, not the concrete file catalog and not a principles file.

Use:

```text
planning/slices/slice-responsibility-map.md
```

for placement/routing decisions.

Use:

```text
planning/slices/SLICE-INDEX.md
```

for the concrete catalog of current, transitional and legacy slice files.

## Start Here

```text
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/SLICE-INDEX.md
planning/slices/slice-questions-register.md
planning/slices/slice-test-plan-workflow.md
```

For server/backend/API slice work, also read:

```text
planning/slices/server-implementation-principles.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
```

For client slice work, also read:

```text
planning/slices/client-implementation-principles.md
planning/slices/client-css-architecture-rules.md
planning/slices/client-form-validation-implementation-principles.md
planning/slices/client-a11y-implementation-principles.md
planning/slices/client-ui-style-workflow.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
```

For cross-cutting/paired concerns, also read:

```text
planning/slices/cross-cutting/README.md
planning/slices/cross-cutting/CROSS-CUTTING-UMBRELLA-TEMPLATE.md
```

## Core Rule

Scenario sources describe required behavior.

Slice drafts describe:

```text
which behavior subset is implemented;
how the implementation is planned;
how the implementation is verified.
```

Data files and behavior item files extract, clarify and classify details from scenarios.

Do not invent behavior locally inside a slice draft when scenario/behavior sources exist.

## Responsibility Routing Rule

Before adding new slice-layer information, first use:

```text
planning/slices/slice-responsibility-map.md
```

Use it to decide whether the information belongs in:

```text
README / read order
SLICE-INDEX.md catalog
source mapping register
questions register
extension points register
implementation notes register
workflow
principles/rules file
template
concrete slice draft
transitional legacy file
```

## Root Principles / Rules

Root `planning/slices/` owns reusable slice-layer principles, rules and workflows.

Important root files:

```text
slice-draft-authoring-principles.md
  general slice draft authoring: scope, boundary, coverage, drift, trace

client-implementation-principles.md
  client layering and read/command/API ownership

client-css-architecture-rules.md
  client CSS ownership and styling boundaries

client-form-validation-implementation-principles.md
  applying deferred form validation behavior in client implementation

client-a11y-implementation-principles.md
  accessibility/ARIA implementation and test contract

client-ui-style-workflow.md
  client UI/style implementation process

server-implementation-principles.md
  server/backend implementation boundaries

implementation-principles.md
  common slice implementation principles

change-extension-points-principles.md
  extension/change pressure principles

draft-driven-discovery-principles.md
  slice-layer discovery loop
```

## Folder Roles

```text
planning/slices/client/
  client templates, client-specific handoff docs and client slice drafts.

planning/slices/server/
  server templates and server slice drafts.

planning/slices/cross-cutting/
  umbrella/coordination docs for paired server-client/security/cross-cutting concerns.
```

General reusable rules/principles should live in the root `planning/slices/` folder, not hidden inside `client/` or `server/`.

## Current File-Location Rule

New slice docs are grouped by implementation responsibility, not by old L1/L2 level.

```text
New client drafts:
  planning/slices/client/

New server drafts:
  planning/slices/server/

New client-side cross-cutting drafts:
  planning/slices/client/cross-cutting/

New server-side cross-cutting drafts:
  planning/slices/server/cross-cutting/

Umbrella cross-cutting docs:
  planning/slices/cross-cutting/
```

Do not create new `planning/slices/l1` or `planning/slices/l2` files.

Existing historical L1/L2 files may remain during migration and should be indexed from `SLICE-INDEX.md` until they are rewritten or moved.

## Current-State Rule

When asked what exists or is implemented now, inspect the current repository branch.

Do not answer current implementation status from uploaded archives or slice drafts alone.
