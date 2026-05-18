# Archive Review Folder Rules

Status: current rule for archive-local review folders

## 1. Rule

Every archive that replaces existing files must use a unique review folder:

```text
_archive-review/<unique-archive-slug>/
```

Do not use a shared folder like:

```text
_archive-review/current/
_archive-review/latest/
_archive-notes/raw-author-message-log.md
```

## 2. Recommended slug format

```text
YYYY-MM-DD-short-purpose-vN
```

Example:

```text
_archive-review/2026-05-18-archive-merge-workflow-v1/
```

## 3. Required contents

```text
ARCHIVE-PLAN.md
ORIGINALS-INDEX.md
MERGE-RISK-REPORT.md
original-files/
raw-author-message-log.md
derived-decisions.md
```

## 4. Original file path rule

Preserve the same project-relative path under `original-files/`.

Example:

```text
Project file:
  planning/slices/SLICE-INDEX.md

Original snapshot:
  _archive-review/<slug>/original-files/planning/slices/SLICE-INDEX.md
```

## 5. Why this matters

Multiple chats/archives can work on overlapping files.

Unique archive review folders prevent archives from overwriting each other's audit notes, raw author logs and original snapshots.
