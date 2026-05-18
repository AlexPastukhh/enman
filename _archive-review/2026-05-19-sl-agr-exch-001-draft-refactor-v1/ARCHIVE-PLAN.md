# Archive Plan

Archive: `sl-agr-exch-001-draft-refactor-v1`  
Review folder: `_archive-review/2026-05-19-sl-agr-exch-001-draft-refactor-v1/`  
Purpose: refactor one implemented slice draft to current draft-sync structure.

## Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | Add source/sync snapshot, implementation sync status and Behavior-to-Test Trace | medium | `_archive-review/2026-05-19-sl-agr-exch-001-draft-refactor-v1/original-files/planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` |

## High-risk notes

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | Existing slice draft with many accepted decisions | Confirm Q001..Q023, ClientAccountId, no ResponsibleEmployeeId, no per-command status enum, no binary upload and explicit start command remain |

## Expected post-apply review

```text
1. confirm draft still says POST /api/employee/requests/{requestId}/agreement-exchange/start;
2. confirm success response is 204 No Content and no response DTO;
3. confirm Q001..Q023 keep their old meanings;
4. confirm ClientAccountId is stored from approved request owner;
5. confirm no ResponsibleEmployeeId guard is introduced;
6. confirm ApproveReview does not create exchange;
7. confirm documentRef is required and no binary upload is in scope;
8. confirm UI/page redirects are out of scope;
9. confirm Behavior-to-Test Trace exists.
```
