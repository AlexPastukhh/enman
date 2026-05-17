# SC-06 — Employee Request Dashboard

Status: L2 scenario draft / StartReview entry point synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee sees requests that may need review and can understand whether a request review is not started, started by them, or started by another Employee.

This scenario prepares Employee-side work after the current L1 client foundation.

The dashboard is a read/list surface. It may host a Start Review entry point for rows whose read state says Start Review is available, but StartReview command behavior belongs to `SC-07B`.

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
If StartReview command sidecar is wired,
dashboard row may show a Start Review entry point
for a request whose read state allows it
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

Optional hosted action slot:

```text
Start Review entry point may appear on dashboard/list row,
but command persistence and lifecycle behavior remain in SC-07B.
```

## 6. Behavior Items

```text
L2-EMP-DASH-001 — Employee can see review-relevant requests.
L2-EMP-DASH-002 — Dashboard distinguishes not-started review, started-by-current-employee review and started-by-another-employee review.
L2-EMP-DASH-003 — Dashboard uses Employee terminology, not Worker terminology.
L2-EMP-DASH-004 — Dashboard may host StartReview entry point when command sidecar is wired; dashboard itself does not define command persistence.
```

## 7. Out of Scope

```text
- start review command behavior -> SC-07B;
- approve/reject command -> SC-07B;
- employee assignment/queue policy -> future slice;
- department/permission model -> future slice;
- agreement proposal exchange -> SC-13*.
```
