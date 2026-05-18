# Archive Plan

Archive: `implemented-slice-draft-sync-workflow-v1`  
Review folder: `_archive-review/2026-05-18-implemented-slice-draft-sync-v1/`  
Purpose: add implemented slice draft sync workflow and navigation.

## 1. New files

| File | Purpose | Risk |
|---|---|---|
| `planning/slices/implemented-slice-sync-workflow.md` | Main workflow for syncing implemented drafts | low |
| `planning/slices/IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md` | Required draft status block | low |
| `planning/slices/IMPLEMENTED-SLICE-SYNC-CHECKLIST.md` | Sync preflight checklist | low |
| `planning/slices/IMPLEMENTED-SLICE-SYNC-REPORT-TEMPLATE.md` | User-facing sync report template | low |

## 2. Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SLICE-FOLDER-MAP.md` | Add implemented slice sync docs to folder navigation | medium | `_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-FOLDER-MAP.md` |
| `planning/slices/SLICE-INDEX.md` | Add implemented slice sync docs to index | medium | `_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-INDEX.md` |
| `planning/slices/SLICE-QUESTIONS.md` | Add implemented slice sync decisions | medium | `_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-QUESTIONS.md` |

## 3. High-risk files

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SLICE-FOLDER-MAP.md` | common navigation | old navigation preserved and implemented-sync section appended |
| `planning/slices/SLICE-INDEX.md` | common index | old slice/cross-cutting/archive sections preserved and implemented-sync section appended |
| `planning/slices/SLICE-QUESTIONS.md` | decision register | old Q-* rows preserved and Q-IMPL-SYNC rows added in same table |

## 4. Original snapshots included

```text
_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-FOLDER-MAP.md
_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-INDEX.md
_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/planning/slices/SLICE-QUESTIONS.md
```

## 5. Expected post-apply review

```text
1. compare replacement files with originals;
2. confirm no old sections/rows were removed;
3. confirm new implemented-sync docs are reachable from navigation;
4. confirm SLICE-QUESTIONS remains one Markdown table.
```
