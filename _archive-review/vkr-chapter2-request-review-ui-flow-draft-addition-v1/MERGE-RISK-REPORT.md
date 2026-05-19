# Merge risk report

## Risk level

Low to medium.

## Why

The archive adds one topic-draft file in the Chapter 2 user interface workbench area.

## Possible conflict

If the target file already exists locally, applying this archive will replace it. The provided command backs up the previous local version first.

## Recommended check after applying

```powershell
git diff -- planning/thesis/vkr-topic-workbench/03-chapter-2-design/06-user-interface/02-request-and-review-ui-flow.topic.md
git status
```
