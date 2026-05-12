# SC-04 — Client Request Creation

## Status
Corrected scenario specification draft.

## Purpose
Client creates and submits a connection/request for review.

## Actor / Screen
Actor: Client  
Screen: Request creation page  
Goal: Create request

## Entry Points
Entry A: Client opens request creation page.  
Entry B: Client starts a new request after rejected request feedback.

## Preconditions
- Client is signed in.
- Client can access request creation page.

## DATA
Request creation DATA `SC-04-DATA-01`

Input DATA:
```text
- requested service / request subject information;
- request object/location information, exact fields pending;
- applicant DATA reference or inline applicant DATA, if needed.
```

Visible DATA after accepted submit:
```text
- request status = InReview;
- request appears in My Requests;
- request appears in employee review queue.
```

Extension / Future DATA:
```text
[VAR:EXPAND]
- data prefilled from rejected request feedback;
- document references if document flow becomes part of request creation.
```

## Main Flow
1. Client opens request creation page.
2. Client enters request creation data.
3. Client-side validation runs automatically.
4. Client corrects request data if client-side validation fails.
5. Client submits request.
6. If accepted, request is created with status InReview.
7. Request appears in My Requests and employee review queue.

## Invariants
Invalid request is not accepted.

## Open Questions
Q: Exact request object/location fields need domain confirmation.  
Q: Is requested service type a fixed list or free description?  
Q: Is applicant DATA always required before submit?
