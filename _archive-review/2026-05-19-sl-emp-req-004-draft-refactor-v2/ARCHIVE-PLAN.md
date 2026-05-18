# Archive Plan

Archive: `sl-emp-req-004-draft-refactor-v2`  
Review folder: `_archive-review/2026-05-19-sl-emp-req-004-draft-refactor-v2/`  
Purpose: refactor one implemented slice draft to current draft-sync structure.

## Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SL-EMP-REQ-004-approve-request-review.md` | Add source/sync snapshot, implementation sync status and Behavior-to-Test Trace | medium | `_archive-review/2026-05-19-sl-emp-req-004-draft-refactor-v2/original-files/planning/slices/SL-EMP-REQ-004-approve-request-review.md` |

## High-risk notes

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SL-EMP-REQ-004-approve-request-review.md` | Existing slice draft with domain/API/test decisions | Confirm existing Q IDs, privacy guardrail and domain guardrails remain |

## Expected post-apply review

```text
1. confirm draft still says POST /api/employee/requests/{requestId}/review/approve;
2. confirm success response is 204 No Content and no response DTO;
3. confirm Q-008..Q-011 keep their old meanings;
4. confirm Privacy / cross-account data exposure is present;
5. confirm no EmployeeRef / no ReviewDecisionRecord are present;
6. confirm AgreementProposalExchange creation stays out of scope;
7. confirm UI/page redirects are out of scope;
8. confirm Behavior-to-Test Trace exists.
```
