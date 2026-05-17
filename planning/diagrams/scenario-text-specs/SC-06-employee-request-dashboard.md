# SC-06 — Employee Request Dashboard

Status: L2 scenario draft / derived from Domain Draft 02  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee sees requests that may need review and can understand whether a request review is not started, started by them, or started by another Employee.

This scenario prepares Employee-side work after the current L1 client foundation.

## 2. Actors

```text
Employee
System
```

Use `Employee`, not `Worker`.

## 3. Scenario Flow

```text
Employee opens request dashboard
        ↓
System lists requests relevant to Employee review work
        ↓
For each request, System shows review state:
  no review started
  review started by current Employee
  review started by another Employee
        ↓
Employee opens request details for a selected request
```

## 4. Domain Direction

```text
RequestReview is owned by ConnectionRequest.
Review is not an aggregate.
There is no Review repository.
Started review state is derived from Request.Review.Status == Started.
Started employee identity is stored as StartedByEmployeeId.
```

## 5. Data Shown

```text
request id / display number if available
request type
request status
created at
applicant/request summary
review state marker
StartedByEmployeeId when needed for current-vs-other Employee display
```

## 6. Behavior Items

```text
L2-EMP-DASH-001 — Employee can see review-relevant requests.
L2-EMP-DASH-002 — Dashboard distinguishes not-started review, started-by-current-employee review and started-by-another-employee review.
L2-EMP-DASH-003 — Dashboard uses Employee terminology, not Worker terminology.
```

## 7. Out of Scope

```text
- starting review command -> SC-07B;
- approve/reject command -> SC-07B;
- employee assignment/queue policy -> future slice;
- department/permission model -> future slice;
- agreement proposal exchange -> SC-13*.
```
