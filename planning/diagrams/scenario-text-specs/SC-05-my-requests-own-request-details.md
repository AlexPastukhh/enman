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

## DATA
My Requests list visible DATA `SC-05-DATA-01`:
```text
- request summary visible enough to identify the request;
- request status: InReview / Approved / Rejected.
```

Request filter DATA `SC-05-DATA-02`:
```text
- status.
```
Future: request type, text/search, date/period only if UX later needs it.

Own Request Details visible DATA `SC-05-DATA-03`:
```text
- request status;
- submitted request data visible to client;
- status-specific review result/feedback.
```

Status-specific visible DATA:
```text
InReview: under-review state.
Approved: approval result/message if available; agreement-related status/action if available.
Rejected: rejection explanation/details; feedback context for creating a new request.
```

## Main Flow
1. Client opens My Requests page.
2. Own requests list is visible.
3. Client can filter/search by supported criteria.
4. Client selects request.
5. Request Details opens and shows status-specific content.

## Invariants
Client can view only own requests. Client cannot open another client's request details.

## Open Questions
Q: What exact request summary is enough to identify a request?  
Q: Where is “create new request based on feedback” offered?

## Diagram Notes
SC-12 is merged into this scenario plus SC-04.
