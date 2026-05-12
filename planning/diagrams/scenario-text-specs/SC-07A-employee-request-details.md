# SC-07A — Employee Request Details

## Status

New corrected scenario specification draft split from old SC-07.

## Purpose

Employee views details of a request before deciding whether to review it.

## Actor / Screen

Actor: Employee  
Screen: Employee Request Details  
Goal: View request details

## Entry Points

Entry A: Employee opens request details from Employee Dashboard — SC-06.

## Preconditions

- Employee is signed in.
- Employee has permission to view employee request details.

## DETAIL

Employee request detail sections  
`SC-07A-DETAIL-01`

- request status;
- request data;
- applicant/client data;
- submitted documents if present;
- review/decision history if visible;
- verification result if available;
- [VAR:EXPAND]

## Main Flow

1. Employee opens request details.
2. Request details are visible.
3. Current request status is visible.
4. System determines whether review action is available.
5. If request is InReview, review action is available.
6. If request is Approved or Rejected, review action is not available.

## Branches

### Request is InReview

-> status = InReview
-> review action is visible/available
-> employee may start Employee Request Review — SC-07B

### Request is Approved

-> status = Approved
-> details are visible readonly
-> review action is not available

### Request is Rejected

-> status = Rejected
-> details and rejection explanation are visible
-> review action is not available

## Invariants

Employee cannot start review for already processed request.

Attach to:

- review action visibility;
- start review action;

## Outcomes

- Employee can view request details.
- Employee can start review only if request is InReview.
- Approved/Rejected requests are not reviewable again.

## Open Questions

Q: Does opening details assign or lock the request? Current assumption: no.

Q: Is review history visible in current implementation or future behavior?

## Diagram Notes

- Keep viewing details separate from reviewing.
- This scenario should not approve/reject; it only opens details and exposes available actions.
