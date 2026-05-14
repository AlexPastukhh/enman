# SL-REVIEW-001 — Approve Request And Verify ApplicantParty

Status: partially implemented slice draft  
Package: `[L1]`  
Source scenario: `SC-07B Employee Request Review`  
Slice type: backend / API / persistence slice depending on request creation, with dependent read/UI/agreement slices  
Current implementation status: domain foundation implemented; application/API/persistence slice not confirmed

## 1. Slice Overview

Observable behavior:

```text
Employee approves an InReview request.
Approval makes request Approved, records review decision, and verifies ApplicantParty in current L1 policy.
```

Why this is a real slice:

```text
- separate employee command behavior;
- changes request lifecycle state;
- coordinates Request and ApplicantParty;
- clear success/failure/no-write behavior;
- needs application transaction behavior;
- independently testable from request creation UI and agreement proposal flow.
```

Related extension / dependent slices:

```text
[DEPENDENT] SL-REQ-001 — Create connection request
[DEPENDENT][READ] SL-EMP-READ-001 — Employee request dashboard/details
[DEPENDENT][UI] SL-REVIEW-UI-001 — Employee review screen actions
[DEPENDENT][READ] SL-REQ-READ-001 — Client sees approved result
[DEPENDENT][L2] SL-AGR-001 — Start agreement proposal exchange after approved request
[EXTENSION] SL-NOTIF-001 — Notify client about approval
```

## 2. Questions Overview

| Question | Why it matters | Current answer / candidate | Blocks implementation? |
|---|---|---|---|
| Should approve + verify be one application slice? | Coordinates two domain concepts | Yes for L1 | Yes |
| Should app service precheck both aggregates before mutation? | Avoid partial mutation | Yes, CanDo/precheck | Yes |
| How is EmployeeRef built? | Needed for review decision | From auth/user context in app layer | Yes |
| Should approval create agreement proposal? | Boundary with SC-13D | No, separate dependent L2 slice | No |
| Should client see approved result in this slice? | Read behavior | No, separate read/query slice | No |

## 3. Flow Coverage Overview

| Flow part | Current coverage | Missing / separate slice |
|---|---|---|
| Domain approve transition | Implemented | app/API/persistence flow |
| ReviewDecisionRecord approved | Implemented | persistence integration |
| ApplicantParty MarkVerified | Implemented | application transaction with request |
| Employee auth/permission | Not confirmed | app/auth layer |
| UI approval action | Not in this slice | SL-REVIEW-UI-001 |
| Agreement proposal start | Not in this slice | SL-AGR-001 |
| Client approved result visibility | Not in this slice | read/query slice |

## 4. Scenario Slice Flow

### F01 — Employee opens request review context

DATA: request id, request status, applicant/request details, employee identity.

### F02 — System confirms request is reviewable

Items: `REQ-LC-002`, `REQ-LC-004`, `REQ-LC-005`.  
Rule: only InReview request can be approved.  
No-write: failed approval does not change request status or review decision.

### F03 — System checks ApplicantParty can be verified

Rule: approval verifies ApplicantParty in current implementation policy.  
No-write: if ApplicantParty cannot be verified, approval should not partially mutate request.

### F04 — Employee approves request

Items: `REQ-CMD-APPROVE-001`, `REQ-IBS-001`.  
Result: request becomes Approved and ReviewDecisionRecord is recorded.

### F05 — System marks ApplicantParty verified

Result: ApplicantParty.VerificationStatus becomes Verified.

### F06 — System persists both changes consistently

### F07 — Result is available

Note: approval enables agreement proposal creation but does not create proposal automatically.

## 5. Implementation Flow

### I01 — UI blueprint

Current implementation: UI is not part of current implementation.

### I02 — API

Planned employee approval endpoint or server action.

### I03 — Application service

Load request, load ApplicantParty, resolve EmployeeRef, precheck `request.CanApprove(...)` and `applicantParty.CanMarkVerified()`, then mutate and persist both in one transaction.

### I04 — Domain

`ConnectionRequest.CanApprove(...)`, `ConnectionRequest.Approve(...)`, `ReviewDecisionRecord.Approved(...)`, `ApplicantParty.CanMarkVerified()`, `ApplicantParty.MarkVerified()`.

### I05 — Persistence

Planned request status update, review decision persistence, applicant verification status update, transaction commit.

### I06 — Response mapping

Return approval result and current request status.

## 6. UI Blueprint

Dependent UI blueprint:

```text
- employee opens request details/review screen;
- sees request status, applicant data and request data;
- approve action is available only when request is InReview;
- employee clicks approve;
- UI calls approval API/server action;
- UI shows success or validation/error message;
- approved request no longer shows review actions.
```

## 7. Test Plan / Test Coverage

Expected/current domain unit coverage:

```text
Approve_marks_in_review_request_as_approved
Approve_records_review_decision
Approve_fails_when_request_is_not_in_review
Failed_approve_does_not_change_state
MarkVerified_succeeds_when_minimum_data_present
```

Needed application/integration tests:

```text
ApproveRequest_VerifiesApplicantParty_AndPersistsBoth
ApproveRequest_WhenApplicantCannotBeVerified_DoesNotApproveRequest
ApproveRequest_WhenRequestNotInReview_DoesNotVerifyApplicant
ApproveRequest_DoesNotCreateAgreementProposal
```

## 8. Detailed Implementation Notes

This section is intentionally implementation-focused and may include pseudocode or code snippets in later drafts.

## 9. Decisions

```text
Decision:
Request approval and ApplicantParty verification are coordinated by application service.

Reason:
ConnectionRequest and ApplicantParty are separate consistency concepts.

Consequence:
Domain methods remain local; app service owns orchestration.
```

```text
Decision:
Approval does not create AgreementProposalExchange.
```

## 10. ADR Links / Candidates

ADR candidates should be promoted to `planning/adr/adr-candidates.md` when the decision affects multiple slices or diploma-level architecture explanation.

## 11. Implementation Checklist

```text
[x] Domain request approve method
[x] Domain review decision record
[x] Domain applicant mark verified method
[ ] Application service orchestration
[ ] EmployeeRef from auth context
[ ] Precheck request/applicant before mutation
[ ] Persist both changes transactionally
[ ] Integration tests
[ ] UI review action slice
[ ] Read visibility for approved result
```
