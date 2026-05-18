# Safe Archive Merge Workflow

Status: current workflow for archives that replace existing docs

## 1. Purpose

Prevent accidental loss of existing documentation when archives replace many files.

## 2. File categories

Before creating an archive, classify files:

```text
New files:
  safe to add; no original snapshot required.

Replacement files:
  existing project files that will be overwritten; original snapshot required.

Deprecated redirect files:
  existing files replaced by redirect/deprecation notice; original snapshot required.

Generated/runtime files:
  do not include in docs archives unless explicitly requested.

Cleanup/delete:
  do not perform through ordinary zip replacement; use separate explicit cleanup step.
```

## 3. Required archive plan

Before creating archive, prepare:

```text
Archive purpose
New files
Replacement files
Original snapshots to include
High-risk files
Expected post-apply review
```

Use:

```text
planning/archive-workflow/ARCHIVE-PLAN-TEMPLATE.md
```

## 4. Original snapshot rule

If archive replaces:

```text
planning/slices/CLIENT-SLICE-TEMPLATE.md
```

it must include:

```text
_archive-review/<unique-archive-slug>/original-files/planning/slices/CLIENT-SLICE-TEMPLATE.md
```

Use the same relative project path under `original-files/`.

## 5. High-risk replacement files

Always treat these as high-risk:

```text
README.md files
SLICE-INDEX.md
SLICE-FOLDER-MAP.md
SLICE-QUESTIONS.md
client/server slice templates
drafting workflows
testing workflows
source-sync registries/maps
cross-cutting umbrella docs
legacy navigation files
```

High-risk files require explicit review notes in:

```text
_archive-review/<unique-archive-slug>/MERGE-RISK-REPORT.md
```

## 6. Archive-local logs

Raw author messages and derived decisions must be archive-local:

```text
_archive-review/<unique-archive-slug>/raw-author-message-log.md
_archive-review/<unique-archive-slug>/derived-decisions.md
```

Do not use one shared project-level raw log.

## 7. Post-apply review

After applying, compare:

```text
project file after apply
vs
_archive-review/<unique-archive-slug>/original-files/<same project path>
```

Use:

```text
planning/archive-workflow/POST-APPLY-MERGE-REVIEW-WORKFLOW.md
```

If information was lost, create a smaller correction archive.
