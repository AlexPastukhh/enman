# Archive Plan

Archive: `archive-merge-workflow-with-originals-v1`  
Review folder: `_archive-review/2026-05-18-archive-merge-workflow-v1/`  
Purpose: introduce safe archive workflow and add navigation to it.

## 1. New files

| File | Purpose | Risk |
|---|---|---|
| `planning/archive-workflow/README.md` | Entry point for archive workflow | low |
| `planning/archive-workflow/SAFE-ARCHIVE-MERGE-WORKFLOW.md` | Core safe merge/original snapshot workflow | low |
| `planning/archive-workflow/ARCHIVE-PLAN-TEMPLATE.md` | Template for archive plans | low |
| `planning/archive-workflow/POST-APPLY-MERGE-REVIEW-WORKFLOW.md` | Step 2 review workflow | low |
| `planning/archive-workflow/ARCHIVE-REVIEW-FOLDER-RULES.md` | Unique review folder/original path rules | low |

## 2. Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SLICE-FOLDER-MAP.md` | Add archive workflow folder/navigation | medium | `_archive-review/2026-05-18-archive-merge-workflow-v1/original-files/planning/slices/SLICE-FOLDER-MAP.md` |
| `planning/slices/SLICE-INDEX.md` | Add archive workflow docs to index | medium | `_archive-review/2026-05-18-archive-merge-workflow-v1/original-files/planning/slices/SLICE-INDEX.md` |
| `planning/slices/SLICE-QUESTIONS.md` | Add accepted archive workflow decisions | medium | `_archive-review/2026-05-18-archive-merge-workflow-v1/original-files/planning/slices/SLICE-QUESTIONS.md` |

## 3. High-risk files

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SLICE-FOLDER-MAP.md` | common navigation file | confirm existing folder map remains and archive workflow section was appended |
| `planning/slices/SLICE-INDEX.md` | common index | confirm legacy/current slice index remains and archive workflow docs were appended |
| `planning/slices/SLICE-QUESTIONS.md` | decision register | confirm existing questions remain and archive workflow questions were appended |

## 4. Original snapshots included

```text
_archive-review/2026-05-18-archive-merge-workflow-v1/original-files/planning/slices/SLICE-FOLDER-MAP.md
_archive-review/2026-05-18-archive-merge-workflow-v1/original-files/planning/slices/SLICE-INDEX.md
_archive-review/2026-05-18-archive-merge-workflow-v1/original-files/planning/slices/SLICE-QUESTIONS.md
```

## 5. Expected post-apply review

```text
1. compare the three replacement files with their archived originals;
2. ensure no existing sections were removed;
3. ensure archive workflow navigation was appended;
4. if any information was lost, create a smaller correction archive.
```
