# Post-Apply Merge Review Workflow

Status: current workflow after applying safe merge archives

## 1. Purpose

After applying a large archive, verify that replacement files did not lose useful information from originals.

## 2. Required inputs

```text
applied project files
_archive-review/<unique-archive-slug>/original-files/
_archive-review/<unique-archive-slug>/ORIGINALS-INDEX.md
_archive-review/<unique-archive-slug>/MERGE-RISK-REPORT.md
```

## 3. Review algorithm

For every replacement file:

```text
1. Open applied project file.
2. Open archived original file.
3. Check whether replacement preserved required old sections.
4. Check whether new workflow/nav sections were added.
5. Record lost/changed/missing information.
6. Decide if correction archive is needed.
```

## 4. Review output

Produce a report:

```markdown
# Post-Apply Merge Review

Archive:
Review folder:

## Files checked

| Project file | Original snapshot | Result | Notes |
|---|---|---|---|

## Lost information

| Project file | Lost section/info | Severity | Fix |
|---|---|---|---|

## Correction archive recommendation

```text
needed / not needed
```
```

## 5. Correction archive rule

If correction is needed, create a second smaller archive.

The second archive should:

```text
touch only problematic files;
preserve originals for files it replaces;
include its own unique _archive-review/<slug>/ folder;
update review report status if appropriate.
```

Do not re-ship the whole first archive unless necessary.
