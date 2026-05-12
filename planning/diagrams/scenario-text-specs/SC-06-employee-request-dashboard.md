# SC-06 — Employee Request Dashboard

## Status
Corrected scenario specification draft.

## Purpose
Employee finds requests awaiting review and chooses whether to open details or start review.

## Actor / Screen
Actor: Employee  
Screen: Employee Request Dashboard  
Goal: Find requests and start work on them

## DATA
Employee dashboard row visible DATA `SC-06-DATA-01`:
```text
- request summary visible enough for employee to identify request;
- request status, especially InReview;
- applicant/client summary if needed to distinguish requests.
```

Employee dashboard filter DATA `SC-06-DATA-02`:
```text
- status.
```
Future: request type, applicant/client search, assigned/unassigned, priority, date/period only if UX later needs it.

## Main Flow
1. Employee opens dashboard.
2. InReview queue is visible.
3. Employee can filter/search requests.
4. Employee can select/open request details.
5. Employee can start review directly from dashboard for InReview request.

## Invariants
Employee can access only employee-authorized dashboard. Only InReview requests can be sent into review from dashboard.
