# SC-06 — Employee Request Dashboard

## Status

Corrected scenario specification draft.

## Purpose

Employee finds requests awaiting review and chooses whether to open details or start review.

## Actor / Screen

Actor: Employee  
Screen: Employee Request Dashboard  
Goal: Find requests and start work on them

## Entry Points

Entry A: Employee opens dashboard.

## Preconditions

- Employee is signed in.
- Employee has permission to access employee dashboard.

## DETAIL

Employee dashboard filter criteria  
`SC-06-DETAIL-01`

- status;
- date/period;
- request type;
- applicant/client;
- priority if exists;
- assigned/unassigned if exists;
- [VAR:EXPAND]

## Main Flow

1. Employee opens dashboard.
2. InReview queue is visible.
3. Employee can filter/search requests.
4. Employee can select/open request details.
5. Employee can start review directly from dashboard for an InReview request.

## Branches

### Open request details

-> employee selects request details
-> Employee Request Details — SC-07A opens

### Start review from dashboard

-> employee starts review from dashboard
-> request status is checked
-> if request is InReview, Employee Request Review — SC-07B starts
-> if request is Approved/Rejected, review cannot start

## Invariants

Employee can access only employee-authorized request dashboard.

Attach to:

- dashboard loaded;

Only InReview requests can be sent into review from dashboard.

Attach to:

- start review action from dashboard;

## Outcomes

- Employee sees review queue.
- Employee can find requests using filters/search.
- Employee can open request details.
- Employee can start review only for InReview requests.

## Open Questions

Q: Does the dashboard show only InReview requests by default, or all requests with filters?

Q: Are requests assigned to employees in the current implementation, or is assignment future behavior?

## Diagram Notes

- Do not collapse filter/search/select/open/review into one node.
- Use separate steps for search/filter, opening details and starting review.
