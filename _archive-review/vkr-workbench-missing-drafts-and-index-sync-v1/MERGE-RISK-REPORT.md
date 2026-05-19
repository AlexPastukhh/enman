# Merge risk report

## Risk level

Medium.

## Why

The archive adds missing Chapter 3/index/slice files and replaces two existing navigation/index files.

## Safe behavior

The apply command backs up existing local versions before copying replacements.

## Recommended check after applying

```powershell
git diff -- planning/thesis/vkr-topic-workbench/README.md
git diff -- planning/thesis/vkr-topic-workbench/03-chapter-2-design/topic-index.md
git diff -- planning/thesis/vkr-topic-workbench/04-chapter-3-implementation
git diff -- planning/slices/L2-APPL-VER-RUN-001.client-applicant-party-verification-panel-run-action.md
git status
```
