# Archive Plan Template

Status: template used before creating archives that replace files

```markdown
# Archive Plan

Archive:
Review folder:
Purpose:

## 1. New files

| File | Purpose | Risk |
|---|---|---|

## 2. Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|

## 3. Deprecated redirect files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|

## 4. High-risk files

| File | Why high risk | Required post-apply check |
|---|---|---|

## 5. Original snapshots to include

```text
_archive-review/<slug>/original-files/...
```

## 6. Expected post-apply review

```text
1. compare replacement files with archived originals
2. list lost sections if any
3. decide whether correction archive is needed
4. create smaller correction archive only for problematic files
```

## 7. Not included

```text
runtime code
generated files
OpenAPI artifacts
mass cleanup/delete
```
```
