# SC-07A — Employee Request Details

Status: L2 scenario draft / derived from Domain Draft 02  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee opens a request and sees request details, applicant information and review state needed before starting, approving or rejecting review.

## 2. Scenario Flow

```text
Employee opens request details
        ↓
System shows request details and applicant/request data
        ↓
System shows review state:
  no review started
  started by current Employee
  started by another Employee
  completed approved/rejected
        ↓
System enables or disables review actions according to domain state
```

## 3. Review Action Visibility

```text
No Review + Request InReview:
  Start review action can be available.

Review Started by current Employee:
  Approve/Reject can be available.

Review Started by another Employee:
  Start/Approve/Reject are blocked/unavailable.

No Review:
  Approve/Reject are blocked.

Request Approved/Rejected/AgreementExchangeFailed:
  Start review is blocked.
```

## 4. Domain Direction

```text
ConnectionRequest owns RequestReview.
Public review API lives on ConnectionRequest:
  StartReview(employee, startedAt)
  ApproveReview(employee, decidedAt)
  RejectReview(employee, feedback, decidedAt)

RequestReview internal methods:
  StartForRequest
  Approve
  Reject
```

## 5. Behavior Items

```text
L2-EMP-DETAIL-001 — Employee request details show review state.
L2-EMP-DETAIL-002 — If another Employee started review, current Employee cannot start/approve/reject it.
L2-EMP-DETAIL-003 — Approve/reject actions require started review.
```

## 6. Out of Scope

```text
- review command persistence/API details -> future server slice;
- employee assignment/claiming beyond review start -> future slice;
- proposal exchange after approval -> SC-13*.
```
