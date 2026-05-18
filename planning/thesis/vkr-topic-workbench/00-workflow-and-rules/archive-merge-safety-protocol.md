# Archive Merge Safety Protocol

Status: active / v1

Purpose:
large documentation archives must be safe to apply and review. A large archive is not treated as the final merge. It is treated as a staged update with preserved originals and explicit post-apply review tasks.

## When this protocol is required

Use this protocol when an archive:

- replaces existing README/index/workflow/template files;
- changes navigation or topic structure;
- touches many files;
- may remove or compress previous guardrails;
- introduces a refactoring of documentation structure.

Small additive archives may skip original snapshots if they add only new files and do not replace existing files.

## Required archive plan before generation

Before creating an archive, state in chat:

```text
Archive plan

New files:
- ...

Files to replace:
- ...

Original snapshots to include:
- ...

High-risk files:
- ...

Expected post-apply review:
- ...

Author messages to capture:
- ...
```

## Safe merge archive structure

If files are replaced, the archive must contain preserved originals:

```text
APPLY.md
MANIFEST.md

<new or replacement project files>

_archive-review/<unique-archive-slug>/
  ORIGINALS-INDEX.md
  MERGE-RISK-REPORT.md
  original-files/
    <repo-relative original file copies>
  raw-author-message-log.md
  derived-decisions.md
```

## Original snapshots

For every replacement file, preserve the pre-replacement version in:

```text
_archive-review/<archive-slug>/original-files/<repo-relative-path>
```

If the exact local current file cannot be read, state the baseline used for the original snapshot, for example:

```text
Original snapshot source: previous generated archive / GitHub branch / uploaded project zip.
```

## Merge correction archive

After applying the first archive, compare:

```text
new project file
vs
_archive-review/<archive-slug>/original-files/<same project file>
```

Then create a smaller correction archive if needed:

- restore lost guardrails;
- fix navigation conflicts;
- merge useful old sections;
- update merge risk status;
- avoid re-shipping all files.

## Cleanup/delete rule

Do not delete or clean up old files through a large mixed archive. Cleanup is a separate explicit step with its own archive plan.

## Author message capture

Before archive generation, list in chat which user messages will be captured. Raw messages go only to `raw-author-message-log.md` or archive-review raw logs. They are not final thesis text.
