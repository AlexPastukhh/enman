# SL-REVIEW-002 — Reject Request

Status: partially implemented slice draft  
Package: `[L1]`  
Source scenario: `SC-07B Employee Request Review`  
Slice type: backend / API / persistence slice depending on request creation, with dependent UI/read/notification slices  
Current implementation status: domain foundation implemented; application/API/persistence slice not confirmed

## 1. Slice Overview

Observable behavior:

```text
Employee rejects an InReview request.
Rejection makes request Rejected, records review decision, and may include optional feedback.
```

Why this is a real slice:

```text
- separate employee command behavior;
- different outcome from approval;
- own failure/no-write rules;
- feedback UX can be separated from domain rule;
- independently testable from approval and agreement proposal flow.
```

Related extension / dependent slices:

```text
[DEPENDENT] SL-REQ-001 — Create connection request
[DEPENDENT][READ] SL-EMP-READ-001 — Employee request dashboard/details
[DEPENDENT][UI] SL-REVIEW-UI-001 — Employee review screen actions
[UI][DEPENDENT] SL-REVIEW-UI-002 — Warning when rejecting without feedback
[READ][DEPENDENT] SL-REQ-READ-001 — Client sees rejected result
[EXTENSION] SL-NOTIF-001 — Notify client about rejection
```

## 2. Questions Overview

| Question | Why it matters | Current answer / candidate | Blocks implementation? |
|---|---|---|---|
| Should feedback be required in domain? | Affects invariant | No, optional in domain | No |
| Should empty feedback warning be blocking? | UI/UX | UI slice decision later | No for backend |
| Should empty string normalize to null? | API/domain consistency | Decide in per-slice implementation | Yes before API |
| Should rejection trigger notification? | Extension behavior | Separate notification slice | No |
| Should client result visibility be part of rejection slice? | Read split | No, read/query slice | No |

## 3. Flow Coverage Overview

| Flow part | Current coverage | Missing / separate slice |
|---|---|---|
| Domain reject transition | Implemented | app/API/persistence flow |
| Optional feedback in domain | Implemented | API normalization question |
| ReviewDecisionRecord rejected | Implemented | persistence integration |
| UI empty feedback warning | Not implemented | SL-REVIEW-UI-002 |
| Client rejected result visibility | Not implemented | read/query slice |
| Notification | Not implemented | extension slice |

## 4. Scenario Slice Flow

### F01 — Employee opens request review context

DATA: request id, request status, request details, applicant summary, employee identity.

### F02 — System confirms request is rejectable

Items: `REQ-LC-003`, `REQ-LC-004`, `REQ-LC-006`.  
Rule: only InReview request can be rejected.  
No-write: failed rejection does not change request status or review decision.

### F03 — Employee provides rejection feedback or leaves it empty

DATA: rejection feedback text, optional.  
Items: `REQ-CMD-REJECT-001`, `REQ-IBS-002`.  
Decision: feedback is optional in domain; empty feedback warning is UI slice.

### F04 — Employee rejects request

Result: request becomes Rejected, ReviewDecisionRecord is recorded, feedback is stored if provided.

### F05 — System persists rejection

### F06 — Result is available

Note: client result visibility belongs to read/query slice.

## 5. Implementation Flow

### I01 — UI blueprint

Current implementation: UI is not implemented in this backend/API/persistence slice.

### I02 — API

Planned employee rejection endpoint or server action. Open question: null vs empty feedback normalization.

### I03 — Application service

Load request, resolve EmployeeRef, normalize feedback if needed, call `request.CanReject(...)`, call `request.Reject(reviewer, feedback)`, persist request, return result.

### I04 — Domain

`ConnectionRequest.CanReject(...)`, `ConnectionRequest.Reject(...)`, `ReviewDecisionRecord.Rejected(...)`, optional `RejectionFeedback`.

### I05 — Persistence

Planned update request status, persist review decision and feedback if provided.

### I06 — Response mapping

Return rejection result and current request status.

## 6. UI Blueprint

Dependent UI blueprint:

```text
- employee opens request details/review screen;
- reject action is available only when request is InReview;
- employee enters optional feedback;
- if feedback is empty, UI shows warning/confirmation;
- employee confirms rejection;
- UI calls rejection API/server action;
- UI shows success or validation/error message;
- rejected request no longer shows review actions.
```

Candidate UI item:

```text
UI-CAND-REVIEW-001: Employee sees warning when rejecting without feedback.
```

## 7. Test Plan / Test Coverage

Expected/current domain unit coverage:

```text
Reject_marks_in_review_request_as_rejected
Reject_allows_null_feedback
Reject_fails_when_request_is_not_in_review
Failed_reject_does_not_change_state
```

Needed application/integration tests:

```text
RejectRequest_PersistsRejectedStatus
RejectRequest_WithFeedback_PersistsFeedback
RejectRequest_WithoutFeedback_Succeeds
RejectRequest_WhenRequestNotInReview_DoesNotChangeState
```

Needed UI tests later:

```text
RejectWithoutFeedback_ShowsWarningBeforeSubmit
```

## 8. Detailed Implementation Notes

This section is intentionally implementation-focused and may include pseudocode or code snippets in later drafts.

## 9. Decisions

```text
Decision:
RejectionFeedback is optional in domain.

Reason:
Empty feedback is undesirable UX, but not accepted as hard business invariant for L1.

Consequence:
Domain allows null feedback.
UI warning belongs to dependent UI slice.
```

## 10. ADR Links / Candidates

ADR candidates should be promoted to `planning/adr/adr-candidates.md` when the decision affects multiple slices or diploma-level architecture explanation.

## 11. Implementation Checklist

```text
[x] Domain request reject method
[x] Domain review decision record with optional feedback
[x] Domain no-repeat reject guard
[ ] Application service orchestration
[ ] EmployeeRef from auth context
[ ] Feedback normalization decision
[ ] Persist rejection decision/status
[ ] Integration tests
[ ] UI warning slice
[ ] Read visibility for rejected result
```
