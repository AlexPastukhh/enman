# Merge risk report

## Risk level

Medium-low.

## Why

This archive updates/creates only one workbench orientation file:

- `planning/thesis/vkr-topic-workbench/03-chapter-2-design/README.md`

If a local README already exists, it should be backed up before applying the archive.

## Safe apply expectation

The apply command should:
1. Expand the archive into a temp folder.
2. Back up the existing target README if it exists.
3. Copy the new README.
4. Copy `_archive-review`.
5. Show `git status`.

## Manual checks after apply

```powershell
git diff -- planning/thesis/vkr-topic-workbench/03-chapter-2-design/README.md
```
