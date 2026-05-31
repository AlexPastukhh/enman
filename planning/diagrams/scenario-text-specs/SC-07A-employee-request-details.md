# SC-07A вЂ” Employee Request Details

Status: L2 scenario draft / StartReview details entry point synchronized  
Doc version: v0.1.0  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee opens a request and sees request details, applicant information and review state needed before starting, approving or rejecting review.

The details page is a read/details surface. It can host review action entry points, including Start Review, but command persistence/lifecycle behavior belongs to `SC-07B`.

## 2. Scenario Flow

```text
Employee opens request details
        в†“
System shows request details and applicant/request data
        в†“
System shows review state:
  no review started
  started by current Employee
  started by another Employee
  completed approved/rejected
        в†“
System enables or disables review actions according to domain state
        в†“
If StartReview command sidecar is wired,
details action area may host Start Review when available
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
L2-EMP-DETAIL-001 вЂ” Employee request details show review state.
L2-EMP-DETAIL-002 вЂ” If another Employee started review, current Employee cannot start/approve/reject it.
L2-EMP-DETAIL-003 вЂ” Approve/reject actions require started review.
L2-EMP-DETAIL-004 вЂ” Details action area may host StartReview entry point when no active review exists and request is InReview.
```

## 6. Out of Scope

```text
- review command persistence/API details -> SC-07B and review command slices;
- employee assignment/claiming beyond review start -> future slice;
- proposal exchange after approval -> SC-13*.
```

## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Employee request details read surface | `[PLANNED]` | L2 details surface for request/applicant/review state. Promote to `[IMPLEMENTED]` only with current repo evidence. |
| Review action availability display | `[PLANNED]` | Details page shows whether Start/Approve/Reject are available or blocked. |
| StartReview details entry point | `[PLANNED]` | Details action area can host the same StartReview feature sidecar as dashboard row. |
| Approve/Reject action area | `[PLANNED]` | Details-only first pass for approve/reject command sidecars. |
| Employee assignment/claiming beyond review start | `[DEFERRED]` | Future workflow; not part of current details scenario. |
| Agreement exchange after approval | `[DEFERRED]` | Belongs to `SC-13D` and related agreement exchange slices. |

