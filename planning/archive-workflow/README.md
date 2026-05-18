# Archive Workflow

Status: canonical workflow entry point for docs archives and replacement safety

This folder owns the workflow for creating, applying and reviewing archives that add or replace project documentation.

## Why this exists

Large docs archives can accidentally replace existing files with shorter or incomplete versions.

To avoid losing guardrails, navigation or decisions, replacement archives must preserve originals and include a post-apply review plan.

## Required docs

```text
planning/archive-workflow/SAFE-ARCHIVE-MERGE-WORKFLOW.md
planning/archive-workflow/ARCHIVE-PLAN-TEMPLATE.md
planning/archive-workflow/POST-APPLY-MERGE-REVIEW-WORKFLOW.md
planning/archive-workflow/ARCHIVE-REVIEW-FOLDER-RULES.md
```

## Core rule

If an archive replaces an existing file, the archive must also include the original file copy under:

```text
_archive-review/<unique-archive-slug>/original-files/<same-project-path>
```

The archive must also include:

```text
_archive-review/<unique-archive-slug>/ORIGINALS-INDEX.md
_archive-review/<unique-archive-slug>/MERGE-RISK-REPORT.md
_archive-review/<unique-archive-slug>/ARCHIVE-PLAN.md
```

## Two-step model

```text
Step 1:
  safe merge archive:
    new/replacement files + originals + risk report

Step 2:
  post-apply review:
    compare applied files against originals
    produce smaller correction archive if information was lost
```

Do not treat a large replacement archive as final without post-apply review.
