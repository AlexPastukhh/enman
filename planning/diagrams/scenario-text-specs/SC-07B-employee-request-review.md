# SC-07B — Employee Request Review

## Status
Corrected scenario specification draft.

## Purpose
Employee reviews an InReview request and approves or rejects it.

## DATA
Review decision DATA `SC-07B-DATA-01`

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
Future: approval message, verification result, agreement-related action/DATA pending agreement discussion.

## Main Flow
1. Employee starts review from dashboard or details.
2. System checks request status is InReview.
3. Employee reviews request data.
4. Employee chooses approve or reject.
5. Decision is recorded.
6. Request status changes to Approved or Rejected.

## Invariants
Only InReview requests can enter review. Approved/Rejected requests cannot be reviewed again.

## Open Questions
Q: Is rejection explanation always required?  
Q: Does approval directly create/send agreement proposal, or is that separate?
