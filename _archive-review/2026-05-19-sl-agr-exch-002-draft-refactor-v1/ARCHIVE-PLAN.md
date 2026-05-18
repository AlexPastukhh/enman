# Archive Plan

Archive: `sl-agr-exch-002-draft-refactor-v1`  
Review folder: `_archive-review/2026-05-19-sl-agr-exch-002-draft-refactor-v1/`  
Purpose: refactor one implemented slice draft to current draft-sync structure.

## Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | Add source/sync snapshot, implementation sync status and Behavior-to-Test Trace | medium | `_archive-review/2026-05-19-sl-agr-exch-002-draft-refactor-v1/original-files/planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` |

## High-risk notes

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | Existing slice draft with shared Client/Employee endpoint decisions | Confirm Q001..Q020, shared route, ClientAccountId, no ResponsibleEmployeeId, no actor abstraction and no per-command status enum remain |

## Expected post-apply review

```text
1. confirm draft still says POST /api/requests/{requestId}/agreement-exchange/proposals;
2. confirm success response is 204 No Content and no proposal DTO;
3. confirm Q001..Q020 keep their old meanings;
4. confirm one shared endpoint first pass remains;
5. confirm ClientAccountId ownership check remains;
6. confirm no ResponsibleEmployeeId guard is introduced;
7. confirm no AgreementExchangeActor abstraction is introduced;
8. confirm UI/page redirects are out of scope;
9. confirm Behavior-to-Test Trace exists.
```
