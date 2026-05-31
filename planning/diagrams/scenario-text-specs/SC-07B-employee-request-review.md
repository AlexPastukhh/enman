# SC-07B вЂ” Employee Request Review

Status: L2 scenario draft / StartReview two entry points and RejectReview optional feedback synchronized  
Doc version: v0.1.0  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee starts, approves or rejects a request review through Request-owned review behavior.

## 2. Start Review Flow

```text
Employee opens an InReview request with no active started review
from one of two entry points:
  dashboard/list row
  request details action area
        в†“
Employee starts review
        в†“
System calls Request.StartReview(Employee, startedAt)
        в†“
Request owns new RequestReview with Status Started
        в†“
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
        в†“
Employee approves review
        в†“
System calls Request.ApproveReview(Employee, decidedAt)
        в†“
RequestReview becomes Approved
        в†“
Request status becomes Approved
        в†“
AgreementProposalExchange can be started later by explicit Employee proposal workflow
```

ApproveReview does not create AgreementProposalExchange.

## 4. Reject Review Flow

```text
Employee opens request with Review Started by same Employee
        в†“
Employee rejects review, optionally providing RejectionFeedback
        в†“
System calls Request.RejectReview(Employee, feedback, decidedAt)
        в†“
RequestReview becomes Rejected
        в†“
Request status becomes Rejected
```

Feedback/body direction:

```text
RejectionFeedback is optional in current direction.
Missing/blank feedback is allowed unless current server/OpenAPI intentionally changes the contract.
Client may show a non-blocking warning in the future, but must not block submit only because feedback is empty by default.
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
L2-REVIEW-START-001 вЂ” Employee can start review for InReview request with no active started review.
L2-REVIEW-START-002 вЂ” Starting review stores StartedByEmployeeId and StartedAt.
L2-REVIEW-START-003 вЂ” StartReview can be initiated from dashboard row or request details action area through the same command behavior.
L2-REVIEW-APPROVE-001 вЂ” Employee who started review can approve it.
L2-REVIEW-APPROVE-002 вЂ” ApproveReview changes Request status to Approved.
L2-REVIEW-APPROVE-003 вЂ” ApproveReview does not create AgreementProposalExchange.
L2-REVIEW-REJECT-001 вЂ” Employee who started review can reject it.
L2-REVIEW-REJECT-002 вЂ” RejectReview changes Request status to Rejected and may store optional feedback.
L2-REVIEW-BLOCK-001 вЂ” Approve/reject without started review is blocked.
L2-REVIEW-BLOCK-002 вЂ” Another Employee cannot start/approve/reject review already started by someone else.
L2-REVIEW-NW-001 вЂ” Failed review command does not change request/review state.
```

## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| StartReview command behavior | `[PLANNED]` | Current L2 planned/implemented candidate; diagram preflight must verify code/OpenAPI before marking `[IMPLEMENTED]`. |
| StartReview two entry points | `[PLANNED]` | Dashboard row and details action area host one command feature, not two scenarios. |
| ApproveReview command behavior | `[PLANNED]` | Details-only review completion command; does not create AgreementProposalExchange. |
| RejectReview command behavior | `[PLANNED]` | Details-only review rejection; feedback is optional in current direction. |
| Agreement exchange start after approval | `[PLANNED]` | Enabled by approval but owned by `SC-13D` / `SL-AGR-EXCH-001`, not by ApproveReview. |
| Employee assignment/department rules | `[DEFERRED]` | Future visibility/assignment extension outside current review command scenarios. |

