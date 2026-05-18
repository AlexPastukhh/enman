# MANIFEST

Archive: `implemented-slice-draft-sync-workflow-v1`  
Review folder: `_archive-review/2026-05-18-implemented-slice-draft-sync-v1/`

## Purpose

Fix the docs workflow for refactoring existing slice drafts that already have implementation.

The archive adds a workflow for synchronizing:

```text
source/domain/map
  vs
existing draft
  vs
actual implementation
  vs
actual tests
```

## New files

```text
planning/slices/implemented-slice-sync-workflow.md
planning/slices/IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md
planning/slices/IMPLEMENTED-SLICE-SYNC-CHECKLIST.md
planning/slices/IMPLEMENTED-SLICE-SYNC-REPORT-TEMPLATE.md
```

## Replacement files

```text
planning/slices/SLICE-FOLDER-MAP.md
planning/slices/SLICE-INDEX.md
planning/slices/SLICE-QUESTIONS.md
```

Replacement files are additive navigation updates.

## Original snapshots included

```text
_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-FOLDER-MAP.md
_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-INDEX.md
_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-QUESTIONS.md
```

## Not included

```text
- no runtime code
- no tests
- no generated files
- no concrete slice draft rewrites
- no UI refactoring workflow
```
