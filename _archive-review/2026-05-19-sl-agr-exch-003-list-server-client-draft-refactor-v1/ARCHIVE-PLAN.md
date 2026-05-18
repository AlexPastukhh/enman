# Archive Plan

Archive: `sl-agr-exch-003-list-server-client-draft-refactor-v1`  
Review folder: `_archive-review/2026-05-19-sl-agr-exch-003-list-server-client-draft-refactor-v1/`  
Purpose: refactor one paired server/client agreement exchange list draft set.

## Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md` | Add source/sync snapshot, implementation sync status and Behavior-to-Test Trace | medium | `_archive-review/2026-05-19-sl-agr-exch-003-list-server-client-draft-refactor-v1/original-files/planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md` |
| `planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md` | Add source/sync snapshot, implementation sync status, visual implementation flow and Behavior-to-Test Trace | medium | `_archive-review/2026-05-19-sl-agr-exch-003-list-server-client-draft-refactor-v1/original-files/planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md` |

## High-risk notes

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md` | Server read slice with shared endpoint/visibility decisions | Confirm Q001..Q013, shared endpoint, ClientAccountId filter, no ResponsibleEmployeeId and no full history remain |
| `planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md` | Client sidecar with API placement/page shell decisions | Confirm Q001..Q010, shared entity wrapper, separate page shells, no actor API wrappers and no shared/api business wrapper remain |

## Expected post-apply review

```text
1. confirm server endpoint remains GET /api/agreement-exchanges;
2. confirm server Q001..Q013 keep old meanings;
3. confirm client Q001..Q010 keep old meanings;
4. confirm ClientAccountId server filtering remains;
5. confirm no ResponsibleEmployeeId guard is introduced;
6. confirm one shared frontend entity API/query/list widget remains;
7. confirm Client and Employee page shells remain separate;
8. confirm UI/page redirects are out of scope;
9. confirm Behavior-to-Test Trace exists in both drafts.
```
