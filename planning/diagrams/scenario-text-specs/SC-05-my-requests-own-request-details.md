# SC-05 — My Requests / Own Request Details

## Status

Corrected scenario specification draft.

## Purpose

Client views own requests and details of a selected request.

## Actor / Screen

Actor: Client  
Screen: My Requests / Request Details  
Goal: View own request status and details

## Entry Points

Entry A: Client selects request from My Requests list.

Entry B: Client opens processed/rejected request after review feedback.

## Preconditions

- Client is signed in.
- Client has at least one request, or empty state is shown.

## DETAIL

Request filter criteria  
`SC-05-DETAIL-01`

- status;
- request type;
- date/period;
- address/object;
- applicant if relevant;
- [VAR:EXPAND]

## Main Flow

1. Client opens My Requests page.
2. Own requests list is visible.
3. Each request shows status: InReview / Approved / Rejected.
4. Client can filter/search/sort requests.
5. Client selects a request.
6. Request Details opens.
7. Request Details shows status-specific content.

## Branches

### InReview request details

-> request status = InReview
-> request is under review
-> submitted data is visible

### Approved request details

-> request status = Approved
-> approval result is visible
-> agreement-related status/action may be visible

### Rejected request details

-> request status = Rejected
-> rejection explanation/details visible
-> client can continue from rejected request feedback
-> client can start new request based on feedback

## Invariants

Client can view only own requests.

Attach to:

- My Requests list is loaded;
- Request Details opens;

Client cannot open another client's request details.

Attach to:

- Request Details opens;

## Outcomes

- Client sees own request list.
- Client sees selected own request details.
- Client sees current status and status-specific information.

## Open Questions

Q: When review feedback is available, should UX lead to Request Details, My Agreements, Agreement Details, or multiple destinations?

Q: Where is “create new request based on feedback” offered: rejected Request Details, review feedback notification, both, or future update existing request flow?

## Diagram Notes

- Do not create separate client-view scenarios for InReview, Approved and Rejected.
- These are branches/states inside one own Request Details scenario.
- Avoid default wording like email link; use review-feedback UX wording unless implementation is explicitly being discussed as Q/ADR.
