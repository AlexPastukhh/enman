# Archive Plan

Archive: `sl-emp-req-005-draft-refactor-v1`  
Review folder: `_archive-review/2026-05-19-sl-emp-req-005-draft-refactor-v1/`  
Purpose: refactor one implemented slice draft to current draft-sync structure.

## Replacement files

| Project file | Replacement purpose | Risk | Original snapshot path |
|---|---|---|---|
| `planning/slices/SL-EMP-REQ-005-reject-request-review.md` | Add source/sync snapshot, implementation sync status and Behavior-to-Test Trace | medium | `_archive-review/2026-05-19-sl-emp-req-005-draft-refactor-v1/original-files/planning/slices/SL-EMP-REQ-005-reject-request-review.md` |

## High-risk notes

| File | Why high risk | Required post-apply check |
|---|---|---|
| `planning/slices/SL-EMP-REQ-005-reject-request-review.md` | Existing slice draft with API feedback/domain/client notes | Confirm existing Q001..Q009, feedback requirement and no agreement flow remain |

## Expected post-apply review

```text
1. confirm draft still says POST /api/employee/requests/{requestId}/review/reject;
2. confirm success response is 204 No Content and no response DTO;
3. confirm Q001..Q009 keep their old meanings;
4. confirm feedback is required at API boundary;
5. confirm domain optional feedback policy is not changed by this slice;
6. confirm AgreementProposalExchange creation stays out of scope;
7. confirm agreement final refusal stays out of scope;
8. confirm UI/page redirects are out of scope;
9. confirm historical client notes are preserved as future client sidecar ownership;
10. confirm Behavior-to-Test Trace exists.
```
