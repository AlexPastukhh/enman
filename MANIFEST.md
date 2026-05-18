# MANIFEST

Docs-only archive for client slice planning reorganization and UI/CSS workflow rules.

## Purpose

Create a canonical slice planning structure organized by responsibility:

```text
planning/slices/client/
planning/slices/server/
planning/slices/cross-cutting/
```

This archive removes L1/L2 as the future folder model. Historical `planning/slices/l1` / `planning/slices/l2` references may remain only as migration legacy and are tracked through `SLICE-INDEX.md`.

## Important guardrails

```text
- does not update existing slice draft bodies;
- does not move all historical drafts;
- does not create new l1/l2 folders;
- does not change runtime UI code;
- does not change CSS implementation;
- does not change routes;
- does not change OpenAPI/generated files.
```

## Files included

```text
planning/slices/README.md
planning/slices/SLICE-FOLDER-MAP.md
planning/slices/SLICE-INDEX.md
planning/slices/SLICE-QUESTIONS.md

planning/slices/client/README.md
planning/slices/client/CLIENT-API-PLACEMENT-DECISION.md
planning/slices/client/CLIENT-LAYERING-FOR-READ-AND-COMMAND-SLICES.md
planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-UI-STYLE-WORKFLOW.md
planning/slices/client/CLIENT-CSS-ARCHITECTURE-RULES.md
planning/slices/client/CLIENT-FORM-VALIDATION-WORKFLOW.md
planning/slices/client/CLIENT-A11Y-WORKFLOW.md
planning/slices/client/CLIENT-SLICE-IMPLEMENTATION-HANDOFF.md
planning/slices/client/examples/CLIENT-READ-SLICE-WITH-LAYOUT-EXAMPLE.md
planning/slices/client/examples/CLIENT-COMMAND-FORM-SLICE-WITH-CSS-EXAMPLE.md

planning/slices/server/README.md
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/server/examples/README.md

planning/slices/cross-cutting/README.md
planning/client/README.md
```
