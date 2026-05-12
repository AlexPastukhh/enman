# SC-07A — Employee Request Details

## Status
Corrected scenario specification draft.

## Purpose
Employee views details of a request before deciding whether to review it.

## DATA
Employee request details visible DATA `SC-07A-DATA-01`:
```text
- request status;
- request data;
- applicant/client DATA needed for review;
- review action availability.
```
Future: submitted documents, verification result, review/decision history, assignment/lock.

## Main Flow
1. Employee opens request details.
2. Request details and status are visible.
3. If request is InReview, review action is available.
4. If request is Approved or Rejected, review action is not available.

## Invariants
Employee cannot start review for already processed request.

## Open Questions
Q: Does opening details assign or lock request? Current assumption: no.
