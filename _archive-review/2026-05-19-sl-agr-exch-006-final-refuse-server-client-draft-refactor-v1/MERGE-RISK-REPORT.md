# Merge Risk Report

## Risk level

Low/medium.

## Why

This archive replaces two planning docs only. Runtime code, tests and generated artifacts are intentionally not changed.

## Main risks

```text
- A parallel docs refactor may already have updated the same two files.
- If source registry / derivation map files are later added, these drafts still need map rows/version updates.
- Code evidence came from the uploaded repo snapshot and tests were not executed in this pass.
```

## Review instructions

```text
1. Compare replacement files against originals in _archive-review.
2. Confirm no runtime file is included.
3. Confirm 0.1 / 0.2 / Behavior-to-Test Trace sections are present.
4. Confirm route and DTO names match current implementation.
5. If remote branch has newer docs, merge manually instead of blind overwrite.
```
