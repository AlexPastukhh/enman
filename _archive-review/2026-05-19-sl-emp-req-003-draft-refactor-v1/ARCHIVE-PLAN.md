# Archive Plan

Archive: `sl-emp-req-003-draft-refactor-v1`  
Review folder: `_archive-review/2026-05-19-sl-emp-req-003-draft-refactor-v1/`  
Purpose: refactor one implemented/legacy slice draft without runtime implementation inspection.

## 1. New files

None outside archive-review material.

## 2. Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SL-EMP-REQ-003-start-request-review.md` | Refactor draft to current structure/contract/test trace | medium | `_archive-review/2026-05-19-sl-emp-req-003-draft-refactor-v1/original-files/planning/slices/SL-EMP-REQ-003-start-request-review.md` |

## 3. High-risk notes

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SL-EMP-REQ-003-start-request-review.md` | Existing long draft contains decisions and assumptions | confirm key decisions remain; confirm old 200/DTO contract is removed; confirm UI remains out of scope |

## 4. Expected post-apply review

```text
1. compare replacement draft with original snapshot;
2. confirm no required guardrails were lost;
3. confirm draft states implementation was not rechecked;
4. continue with next single-slice draft refactor.
```
