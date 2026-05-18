# Merge risk report

## Risk level

Medium-low.

## Why

The archive adds/replaces one VKR topic draft under Chapter 2 architecture workbench. It does not modify source code.

## Main risk

If a local version of the same topic file already exists, applying this archive will overwrite it after backing it up to `_archive-review`.

## Review after applying

Run:

```powershell
git diff -- planning/thesis/vkr-topic-workbench/03-chapter-2-design/03-architecture/02-frontend-backend-responsibilities.topic.md
```
