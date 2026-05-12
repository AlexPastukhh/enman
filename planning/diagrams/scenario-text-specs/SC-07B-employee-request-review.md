# SC-07B — Employee Request Review

## Status

Corrected scenario specification draft split from old SC-07.

## Purpose

Employee reviews an InReview request and approves or rejects it.

## Actor / Screen

Actor: Employee  
Screen: Employee Request Review  
Goal: Process request decision

## Entry Points

Entry A: Employee starts review from Employee Dashboard.

Entry B: Employee starts review while viewing Employee Request Details — SC-07A.

## Preconditions

- Employee is signed in.
- Employee has permission to review requests.
- Request status is InReview.

## DETAIL

Review decision details  
`SC-07B-DETAIL-01`

- request data;
- applicant/client data;
- verification result if available;
- decision type;
- rejection explanation if rejected;
- agreement option/draft data if approved and sent as part of review;
- [VAR:EXPAND]

## Main Flow

1. Employee starts review.
2. System checks that request status is InReview.
3. Employee reviews request data.
4. Employee may use client data verification result if available.
5. Employee chooses approve or reject.
6. Decision is recorded.
7. Request status changes to Approved or Rejected.

## Branches

### Request can enter review

-> request status = InReview
-> review screen/action opens
-> employee reviews request

### Request already processed

-> request status = Approved or Rejected
-> review cannot start
-> processed request message/action state is visible

### Approve request

-> employee approves request
-> decision is recorded
-> request status becomes Approved
-> agreement option/draft can be sent to client

### Reject request

-> employee rejects request
-> rejection explanation is recorded
-> request status becomes Rejected
-> rejected feedback path becomes available to client

## Invariants

Only InReview requests can enter review.

Attach to:

- start review action from dashboard;
- start review action from request details;
- request status check before review screen/action opens;

Approved/Rejected requests cannot be reviewed again.

Attach to:

- request status check before review screen/action opens;

## Step Postconditions

- Review decision is recorded after approve/reject.
- Request status becomes Approved or Rejected after recorded decision.

## Outcomes

- Employee can process InReview requests.
- Approved request becomes visible as Approved to client.
- Rejected request becomes visible as Rejected to client with explanation/details.
- Agreement-related path may become available after approval.

## ADR / Policy Candidates

ADR?: Current implementation requires both employee request details view and review flow.

ADR?: Review can be started from dashboard and from request details.

ADR?: Employee cannot start review for Approved/Rejected request.

## Diagram Notes

- This scenario produces Approved/Rejected decisions.
- Do not keep separate SC-08/SC-09 as standalone client result pages.
- Client viewing result belongs to SC-05.
- Rejected feedback navigation belongs to SC-12.
- Agreement response belongs to SC-13.
