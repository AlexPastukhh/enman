# SC-07B — Employee Request Review

Status: L2 scenario draft / StartReview two entry points synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee starts, approves or rejects a request review through Request-owned review behavior.

## 2. Start Review Flow

```text
Employee opens an InReview request with no active started review
from one of two entry points:
  dashboard/list row
  request details action area
        ↓
Employee starts review
        ↓
System calls Request.StartReview(Employee, startedAt)
        ↓
Request owns new RequestReview with Status Started
        ↓
Dashboard/details show review started by this Employee
```

Entry point rule:

```text
Dashboard and details may both host StartReview.
This is one scenario command behavior and one client command sidecar, not two separate StartReview scenarios.
```

## 3. Approve Review Flow

```text
Employee opens request with Review Started by same Employee
        ↓
Employee approves review
        ↓
System calls Request.ApproveReview(Employee, decidedAt)
        ↓
RequestReview becomes Approved
        ↓
Request status becomes Approved
        ↓
AgreementProposalExchange can be started later by Employee proposal workflow
```

## 4. Reject Review Flow

```text
Employee opens request with Review Started by same Employee
        ↓
Employee rejects review with RejectionFeedback required by the API endpoint
        ↓
System calls Request.RejectReview(Employee, feedback, decidedAt)
        ↓
RequestReview becomes Rejected
        ↓
Request status becomes Rejected
```

## 5. Blocked Flows

```text
Approve/reject without started review -> blocked.
Another Employee tries start/approve/reject after review was started by someone else -> blocked.
Request not InReview -> start review blocked.
Failed review command does not change Request status or Review state.
```

## 6. Domain Direction

```text
Review is not aggregate.
Request owns Review.
Review has no repository.
ReviewDecisionRecord is removed.
Decision/result fields live inside RequestReview.
Domain methods receive Employee object when employee capability matters.
Owned state stores scalar ids: StartedByEmployeeId and CompletedByEmployeeId.
```

## 7. Behavior Items

```text
L2-REVIEW-START-001 — Employee can start review for InReview request with no active started review.
L2-REVIEW-START-002 — Starting review stores StartedByEmployeeId and StartedAt.
L2-REVIEW-START-003 — StartReview can be initiated from dashboard row or request details action area through the same command behavior.
L2-REVIEW-APPROVE-001 — Employee who started review can approve it.
L2-REVIEW-APPROVE-002 — ApproveReview changes Request status to Approved.
L2-REVIEW-REJECT-001 — Employee who started review can reject it.
L2-REVIEW-REJECT-002 — RejectReview changes Request status to Rejected and stores feedback required by the API.
L2-REVIEW-BLOCK-001 — Approve/reject without started review is blocked.
L2-REVIEW-BLOCK-002 — Another Employee cannot start/approve/reject review already started by someone else.
L2-REVIEW-NW-001 — Failed review command does not change request/review state.
```
