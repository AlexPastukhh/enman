# Archive Plan

Archive: `archive-merge-workflow-qfix-v1`  
Review folder: `_archive-review/2026-05-18-archive-merge-workflow-qfix-v1/`  
Purpose: fix Markdown table formatting in `SLICE-QUESTIONS.md`.

## 1. New files

None.

## 2. Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SLICE-QUESTIONS.md` | Remove blank line that splits the questions table | low | `_archive-review/2026-05-18-archive-merge-workflow-qfix-v1/original-files/planning/slices/SLICE-QUESTIONS.md` |

## 3. High-risk files

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SLICE-QUESTIONS.md` | decision register | confirm all old Q-* rows remain and Q-ARCHIVE rows are in the same Markdown table |

## 4. Original snapshots included

```text
_archive-review/2026-05-18-archive-merge-workflow-qfix-v1/original-files/planning/slices/SLICE-QUESTIONS.md
```

## 5. Expected post-apply review

```text
1. open planning/slices/SLICE-QUESTIONS.md;
2. confirm there is no blank line between Q-TEST-004 and Q-ARCHIVE-001;
3. confirm no rows were removed.
```
