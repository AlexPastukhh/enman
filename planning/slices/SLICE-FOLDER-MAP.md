# Slice Folder Map

Status: transitional folder ownership map / L1-L2 legacy, cross-cutting taxonomy and testing workflow synchronized

Authority note:

```text
This file is transitional.
Canonical slice-layer placement and responsibility routing now lives in:
planning/slices/slice-responsibility-map.md

Keep this file during migration because it still preserves folder placement, legacy path and archive-workflow references.
Do not delete it until README/index links and legacy migration references are synchronized.
```

## Scenario source folders

```text
planning/diagrams/scenario-text-specs/
  subject/business scenarios.

planning/diagrams/scenario-ui-specs/
  UI scenario requirements: visible screens, actions, states and visual flow.

planning/diagrams/scenario-cross-cutting/client-behavior/
  cross-cutting client behavior used across multiple UI/client scenarios
  without a required server implementation.

planning/diagrams/scenario-cross-cutting/server-behavior/
  cross-cutting server behavior without a required client implementation.

planning/diagrams/scenario-cross-cutting/server-client/
  behavior where both server and client participate.

planning/diagrams/scenario-cross-cutting/security/
  security/protection/abuse scenarios such as CSRF protection.

planning/diagrams/scenario-data/
  scenario data details.

planning/diagrams/scenario-behavior-items/
  behavior items extracted from scenarios.
```

## Slice planning folders

```text
planning/slices/client/
  client rules, workflow, templates, examples and client sidecar drafts.

planning/slices/client/cross-cutting/
  client-side slice drafts for cross-cutting behavior.

planning/slices/server/
  server rules, workflow, templates, examples and server/API/backend drafts.

planning/slices/server/cross-cutting/
  server-side slice drafts for cross-cutting behavior.

planning/slices/cross-cutting/
  umbrella/coordination docs for paired server-client/security/cross-cutting concerns.
```

## Common navigation

```text
planning/slices/README.md
  main entry point

planning/slices/slice-responsibility-map.md
  canonical slice-layer placement and responsibility routing

planning/slices/SLICE-INDEX.md
  list of known slices and docs

planning/slices/SLICE-QUESTIONS.md
  transitional register of decisions, blocked questions, assumptions and future-review items

planning/slices/slice-test-plan-workflow.md
  rules for writing Test / Verification Plan inside slice drafts
```

## New slice file placement

New client drafts go directly to:

```text
planning/slices/client/
```

New server drafts go directly to:

```text
planning/slices/server/
```

New client-side cross-cutting implementation drafts go to:

```text
planning/slices/client/cross-cutting/
```

New server-side cross-cutting implementation drafts go to:

```text
planning/slices/server/cross-cutting/
```

Umbrella cross-cutting concern docs go to:

```text
planning/slices/cross-cutting/
```

Do not create new L1/L2 folders for future slice docs.

For canonical placement/routing, read:

```text
planning/slices/slice-responsibility-map.md
```

## Migration rule

Do not mass-move existing historical slice drafts unless a specific cleanup task does that.

Historical files under old paths may remain temporarily:

```text
planning/slices/l1/
planning/slices/l2/
planning/slices/SL-*.md
planning/client/
```

Use `SLICE-INDEX.md` to point to both legacy and new locations until migration is complete.

## Deprecated compatibility folders

```text
planning/client/
  deprecated compatibility entry point; use planning/slices/client/

planning/slices/l1/
planning/slices/l2/
  legacy historical slice draft grouping; do not add new files
```


## Archive workflow folders

```text
planning/archive-workflow/
  rules for safe docs archives, original snapshots, post-apply merge review and second-step correction archives.

_archive-review/<unique-archive-slug>/
  archive-local review material created by archives that replace existing files:
    ORIGINALS-INDEX.md
    MERGE-RISK-REPORT.md
    ARCHIVE-PLAN.md
    original-files/
    raw-author-message-log.md
    derived-decisions.md
```

Archive workflow docs are not slice drafts. They are documentation/change-management workflow.


## Implemented slice sync docs

```text
planning/slices/implemented-slice-sync-workflow.md
  transitional workflow for refactoring/synchronizing slice drafts that already have implementation.

planning/slices/IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md
  transitional draft block for implemented slice status and known drift.

planning/slices/IMPLEMENTED-SLICE-SYNC-CHECKLIST.md
  transitional checklist for source/domain/map/code/test inspection before updating an implemented draft.

planning/slices/IMPLEMENTED-SLICE-SYNC-REPORT-TEMPLATE.md
  transitional user-facing report template for implemented slice sync audit.
```

These docs are for existing implemented slice drafts. New draft-only slices still use the normal client/server templates.

Their future shape is tracked in:

```text
planning/planning-maintenance-register.md
```
