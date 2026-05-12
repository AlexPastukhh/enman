# SC-07B — Employee Request Review

## Status

Corrected scenario specification draft.

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

## DATA

Review decision DATA  
`SC-07B-DATA-01`

Input DATA:

```text
- decision: Approved or Rejected;
- rejection explanation when decision is Rejected.
```

Visible DATA:

```text
- request data reviewed by employee;
- applicant/client DATA reviewed by employee.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- approval message, if needed;
- verification result, if verification flow is enabled.
```

## Main Flow

1. Employee starts review.
2. System checks that request status is InReview.
3. Employee reviews request data.
4. Employee may use client data verification result if available.
5. Employee chooses approve or reject.
6. Decision is recorded.
7. Request status changes to Approved or Rejected.

## Branches

### Approve request

-> employee approves request  
-> decision is recorded  
-> request status becomes Approved  
-> employee can later create agreement proposal from Approved request  
-> agreement proposal is not created automatically

### Reject request

-> employee rejects request  
-> rejection explanation is recorded  
-> request status becomes Rejected  
-> rejected feedback is visible to client in own Request Details

## Invariants

Only InReview requests can enter review.

Approved/Rejected requests cannot be reviewed again.

Agreement proposal creation is available only for Approved requests.

Approval does not automatically create agreement proposal.

## Outcomes

- Employee can process InReview requests.
- Approved request becomes visible as Approved to client.
- Rejected request becomes visible as Rejected to client with explanation/details.
- Approved request can later be used by employee to start agreement proposal exchange.
