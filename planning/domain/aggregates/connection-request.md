# Domain Aggregate Draft — ConnectionRequest

Status: draft / second extraction pilot  
Doc version: v0.1.0  
Scope: connection request creation, request review lifecycle and request-to-agreement-exchange coordination

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
  Internal dependencies:
    - none
  Not checked:
    - full UI sidecar coverage for request creation/review
    - full source/version/cascade alignment for all request/review/agreement sources
```

`ConnectionRequest` owns the lifecycle of a client connection request.

It covers:

```text
- request creation from one ApplicantParty context;
- persisted request details and object address;
- initial InReview status;
- employee review start/approve/reject behavior;
- RequestReview child entity state;
- transition to Approved / Rejected;
- marking an approved request as AgreementExchangeFailed when agreement exchange final refusal is coordinated by the application layer.
```

It does not own ApplicantParty creation, AgreementProposalExchange creation, file storage, dashboard/list read models or UI navigation.

## 2. Source Inputs

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-data/README.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-06-employee-request-dashboard-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07A-employee-request-details-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
  Internal dependencies:
    - none
  Not checked:
    - full UI sidecar coverage for request creation/review
    - full source/version/cascade model for all request/review/agreement sources
```

This section is the aggregate-level reviewed source overview. Section-level `Sources:` blocks below are authoritative for local section work.

Scenario text sources:

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
```

Scenario-specific DATA rule:

```text
Primary scenario-specific DATA belongs in the scenario text spec #DATA section.
Use planning/diagrams/scenario-data/ only for reusable/shared/audited/transitional DATA sidecars.
```

Reusable/shared/audited/transitional DATA sidecars checked or referenced by the prior draft:

```text
planning/diagrams/scenario-data/SC-04-request-creation-data.md @ Doc version: v0.1.0
planning/diagrams/scenario-data/SC-07B-employee-request-review-data.md @ Doc version: v0.1.0
```

Behavior items:

```text
planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-06-employee-request-dashboard-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-07A-employee-request-details-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
```

Existing domain sources:

```text
planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
```

Current implementation sources checked in prior archive/source pass:

```text
Domain.EnergyManagement/Requests/ClientRequest.cs
Domain.EnergyManagement/Requests/ConnectionRequest.cs
Domain.EnergyManagement/Requests/RequestReview.cs
Domain.EnergyManagement/Requests/RejectionFeedback.cs
Domain.EnergyManagement/Requests/RequestStatus.cs
Domain.EnergyManagement/Requests/RequestReviewStatus.cs
Domain.EnergyManagement/DocumentManaging/Address.cs
Tests.EnergyManagement/Domain/Requests/
Tests.EnergyManagement/Integration/App/Requests/CreateConnectionRequestIntegrationTests.cs
Tests.EnergyManagement/Integration/App/EmployeeRequests/
```

Not checked:

```text
Full UI sidecar coverage for request creation/review was not audited in this pass.
Full source/version/cascade model is still deferred.
Current implementation files and tests are not treated as freshly rechecked evidence unless explicitly reviewed in a later implementation-sync pass.
```

## 3. Aggregate Boundary

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
  Internal dependencies:
    - Purpose
  Not checked:
    - current runtime implementation boundary beyond previously checked archive/source pass
```

Aggregate root:

```text
ConnectionRequest
```

Base class / type hierarchy:

```text
ClientRequest
  -> ConnectionRequest
```

Child entities:

```text
RequestReview
```

Value objects used:

```text
ObjectAddress / Address
RejectionFeedback
```

Not part of this aggregate:

```text
ApplicantParty
AgreementProposalExchange
Employee
ClientAccount
File/blob storage
request dashboard/list/read models
```

External aggregate references:

```text
ApplicantPartyId
ClientAccountId
Employee ids on RequestReview
AgreementProposalExchangeId used only when marking AgreementExchangeFailed
```

## 4. Owned State

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/object-address.md @ Doc version: v0.1.0
    - planning/domain/value-objects/rejection-feedback.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
  Not checked:
    - current persistence mapping / EF configuration
```

Root state:

```text
ApplicantPartyId
ClientAccountId
RequestType
Status
Details
ObjectAddress
CreatedAt
```

Child state:

```text
Review?
  RequestId
  Status
  StartedByEmployeeId
  StartedAt
  CompletedByEmployeeId?
  CompletedAt?
  RejectionFeedback?
```

Derived/read-only state:

```text
Request is actively started for review when Review.Status == Started.
Request is approved when Status == Approved and Review.Status == Approved.
Request is rejected when Status == Rejected and Review.Status == Rejected.
```

Not stored here:

```text
ApplicantParty details snapshot, unless a future scenario requires request-local immutable applicant data.
AgreementProposalExchange contents.
Employee profile data.
Agreement proposal document bytes.
```

## 5. Domain Methods / Commands

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/object-address.md @ Doc version: v0.1.0
    - planning/domain/value-objects/rejection-feedback.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
  Not checked:
    - application service orchestration beyond previously checked source pass
    - current implementation code/tests beyond previously checked archive/source pass
```

### `Create(applicantParty, details, objectAddress)`

Purpose:

```text
Create a connection request from one persisted ApplicantParty context.
```

Input:

```text
ApplicantParty applicantParty
string details
Address objectAddress
```

Preconditions:

```text
ApplicantParty is required and persisted.
Details are required and within max length.
Object address is required and structurally valid before being passed in.
```

State changes:

```text
Creates ConnectionRequest with:
- ApplicantPartyId from selected ApplicantParty;
- ClientAccountId from selected ApplicantParty;
- RequestType = Connection;
- Status = InReview;
- Details;
- ObjectAddress;
- CreatedAt.
```

Domain errors:

```text
ApplicantPartyIsRequired
ApplicantPartyMustBePersisted
ClientRequestTextIsRequired
ClientRequestTextIsTooLong
RequestObjectAddressIsRequired
```

Source behavior:

```text
REQ-CMD-CREATE-001
REQ-LC-001
SC-04-BI-001..SC-04-BI-012
```

### `StartReview(employee, startedAt)`

Purpose:

```text
Start an employee review for an InReview request.
```

Preconditions:

```text
Request is persisted.
Employee exists and can review.
Request status is InReview.
There is no active started review.
```

State changes:

```text
Creates owned RequestReview child with Status = Started, StartedByEmployeeId and StartedAt.
Request.Status remains InReview.
```

Domain errors:

```text
RequestIsRequired
EmployeeIsRequired
OnlyInReviewRequestCanStartReview
RequestReviewAlreadyStarted
employee capability errors
```

Source behavior:

```text
L2-REVIEW-START-001
L2-REVIEW-START-002
L2-REVIEW-START-003
```

### `ApproveReview(employee, decidedAt)`

Purpose:

```text
Complete a started review as approved.
```

Preconditions:

```text
Review must have been started.
Request status must still be InReview.
Employee must be the review starter and must still be able to review.
```

State changes:

```text
RequestReview.Status = Approved
RequestReview.CompletedByEmployeeId = employee.Id
RequestReview.CompletedAt = decidedAt
RequestReview.RejectionFeedback = null
Request.Status = Approved
```

Domain errors:

```text
RequestReviewMustBeStarted
OnlyInReviewRequestCanBeApproved
OnlyStartedReviewCanBeCompleted
RequestReviewStartedByAnotherEmployee
employee capability errors
```

Source behavior:

```text
REQ-CMD-APPROVE-001
REQ-LC-002
REQ-LC-004
REQ-LC-005
REQ-IBS-001
L2-REVIEW-APPROVE-001
L2-REVIEW-APPROVE-002
L2-REVIEW-APPROVE-003
AGR-UCQ-002
```

### `RejectReview(employee, feedback, decidedAt)`

Purpose:

```text
Complete a started review as rejected, optionally storing rejection feedback.
```

Preconditions:

```text
Review must have been started.
Request status must still be InReview.
Employee must be the review starter and must still be able to review.
Feedback is optional in current domain/API direction; if present, it must be a valid RejectionFeedback value object.
```

State changes:

```text
RequestReview.Status = Rejected
RequestReview.CompletedByEmployeeId = employee.Id
RequestReview.CompletedAt = decidedAt
RequestReview.RejectionFeedback = feedback or null
Request.Status = Rejected
```

Domain errors:

```text
RequestReviewMustBeStarted
OnlyInReviewRequestCanBeRejected
OnlyStartedReviewCanBeCompleted
RequestReviewStartedByAnotherEmployee
RejectionFeedbackIsTooLong when feedback is present and invalid
employee capability errors
```

Source behavior:

```text
REQ-CMD-REJECT-001
REQ-LC-003
REQ-LC-006
REQ-IBS-002
L2-REVIEW-REJECT-001
L2-REVIEW-REJECT-002
```

### `MarkAgreementExchangeFailed(agreementProposalExchangeId, failedAt)`

Purpose:

```text
Let application coordination mark an approved request as AgreementExchangeFailed after final refusal of its related AgreementProposalExchange.
```

Preconditions:

```text
AgreementProposalExchangeId must be present.
Request status must be Approved.
```

State changes:

```text
Request.Status = AgreementExchangeFailed
```

Domain errors:

```text
AgreementProposalExchangeIsRequired
OnlyApprovedRequestCanBeMarkedAgreementExchangeFailed
```

Source behavior:

```text
L2 agreement final refusal direction
planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
```

## 6. Invariants

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/object-address.md @ Doc version: v0.1.0
    - planning/domain/value-objects/rejection-feedback.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Owned State
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Domain Methods / Commands
  Not checked:
    - runtime enforcement / tests unless explicitly reviewed
```

| Invariant | Protected by | Source | Failure/error |
|---|---|---|---|
| Request cannot be created without persisted ApplicantParty. | `Create` validation | SC-04 / current implementation | `ApplicantPartyIsRequired`, `ApplicantPartyMustBePersisted` |
| Request cannot be created without details. | `Create` validation | SC-04 | `ClientRequestTextIsRequired` |
| Request cannot be created without object address. | `Create` validation | REQ-IBS-003, REQ-VI-001 | `RequestObjectAddressIsRequired` |
| New request starts InReview. | `Create` factory | REQ-LC-001, SC-04-BI-010 | n/a |
| Review can start only for persisted InReview request. | `StartReview` | L2 review items | `RequestIsRequired`, `OnlyInReviewRequestCanStartReview` |
| At most one active started review exists. | `StartReview` | L2 review items | `RequestReviewAlreadyStarted` |
| Approve/reject requires started review. | `ApproveReview`, `RejectReview` | L2-REVIEW-BLOCK-001 | `RequestReviewMustBeStarted` |
| Only the employee who started review can complete it. | `RequestReview.CanComplete` | L2-REVIEW-BLOCK-002 | `RequestReviewStartedByAnotherEmployee` |
| Approve changes request and review together. | `ApproveReview` | REQ-CMD-APPROVE-001 | no partial mutation if child approval fails |
| Reject changes request and review together. | `RejectReview` | REQ-CMD-REJECT-001 | no partial mutation if child rejection fails |
| Approved request cannot be reviewed again in core. | status guards | REQ-LC-004 | `OnlyInReview...` errors |
| Rejected request cannot be approved/rejected again in core. | status guards | REQ-LC-005, REQ-LC-006 | `OnlyInReview...` errors |
| AgreementExchangeFailed can only follow Approved. | `MarkAgreementExchangeFailed` | L2 agreement final refusal direction | `OnlyApprovedRequestCanBeMarkedAgreementExchangeFailed` |

## 7. Lifecycle / State Machine

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Invariants
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - full request/review/agreement final-refusal implementation and tests
```

Request statuses:

```text
InReview
Approved
Rejected
AgreementExchangeFailed
```

Review statuses:

```text
Started
Approved
Rejected
```

Allowed transitions:

```text
Create -> Request.Status = InReview
InReview + StartReview -> Review.Status = Started, Request.Status remains InReview
InReview + Started review + ApproveReview -> Request.Status = Approved, Review.Status = Approved
InReview + Started review + RejectReview -> Request.Status = Rejected, Review.Status = Rejected
Approved + final-refusal application coordination -> Request.Status = AgreementExchangeFailed
```

Forbidden transitions:

```text
StartReview on non-InReview request.
StartReview when active started review already exists.
Approve/reject without started review.
Approve/reject by another employee.
Approve/reject after request left InReview.
MarkAgreementExchangeFailed from InReview or Rejected.
```

## 8. Impossible States Prevented

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/object-address.md @ Doc version: v0.1.0
    - planning/domain/value-objects/rejection-feedback.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - Invariants
    - Lifecycle / State Machine
    - Domain Methods / Commands
  Internal dependencies:
    - Invariants
    - Lifecycle / State Machine
    - Domain Methods / Commands
  Not checked:
    - current runtime code/tests unless explicitly reviewed
```

| Impossible state | Prevented by | Source |
|---|---|---|
| Request without object address. | `Create` validation + `Address` value object validation | REQ-IBS-003 / REQ-VI-001 |
| Request created without ApplicantParty reference. | `Create` validation | SC-04 applicant context model |
| Approved request without review decision. | `ApproveReview` changes Review and Request in one method | REQ-IBS-001 |
| Rejected request with invalid feedback value. | `RejectionFeedback.Create` when feedback is present | REQ-IBS-002 / SC-07B |
| Review completed by employee who did not start it. | `RequestReview.CanComplete` | L2-REVIEW-BLOCK-002 |
| Proposal/exchange created automatically by approval. | coordination separation; `ApproveReview` does not create exchange | AGR-UCQ-002 |
| Request marked exchange failed before approval. | `MarkAgreementExchangeFailed` status guard | L2 agreement final refusal direction |

## 9. Value Objects Used

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/domain/value-objects/object-address.md @ Doc version: v0.1.0
    - planning/domain/value-objects/rejection-feedback.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
  Internal dependencies:
    - Owned State
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - whether future request snapshot/refusal metadata needs additional value-object docs
```

| Value object | File | Purpose in this aggregate |
|---|---|---|
| ObjectAddress / Address | `planning/domain/value-objects/object-address.md` | Required object address for request creation. |
| RejectionFeedback | `planning/domain/value-objects/rejection-feedback.md` | Optional feedback value when an employee rejects review. |

## 10. Cross-Aggregate Relations

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - Aggregate Boundary
    - Domain Methods / Commands
  Internal dependencies:
    - Aggregate Boundary
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - application service implementation beyond prior source pass
```

References to other aggregates:

```text
ApplicantPartyId
ClientAccountId
Employee ids in RequestReview
AgreementProposalExchangeId for agreement exchange failure coordination
```

Rules not owned here:

```text
ApplicantParty creation/edit/current-default selection.
Employee account/role/capability lifecycle.
AgreementProposalExchange creation and proposal lifecycle.
```

Application coordination needed:

```text
- Existing ApplicantParty branch: load and authorize owned saved ApplicantParty, then call ConnectionRequest.Create.
- New applicant branch: create ApplicantParty and ConnectionRequest as one atomic user intent/transaction.
- Employee review commands: resolve Employee from auth/account context before calling domain methods.
- Agreement final refusal: call AgreementProposalExchange final-refusal behavior and then mark approved Request as AgreementExchangeFailed where appropriate.
```

## 11. Behavior Coverage

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-06-employee-request-dashboard-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07A-employee-request-details-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Invariants
    - Lifecycle / State Machine
    - Impossible States Prevented
    - Cross-Aggregate Relations
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
    - Lifecycle / State Machine
    - Impossible States Prevented
  Not checked:
    - UI sidecar behavior not audited in this pass
    - all request/review tests in current repo state
```

| Source item | Covered by | Status | Notes |
|---|---|---|---|
| REQ-CMD-CREATE-001 | `Create` | covered | Creates request from valid ApplicantParty/details/address. |
| REQ-LC-001 / SC-04-BI-010 | `Create` | covered | New request starts InReview. |
| REQ-IBS-003 / REQ-VI-001 | `ObjectAddress` + `Create` | covered | Address integrity mostly lives in Address value object. |
| REQ-UCQ-001 | application coordination + `ApplicantPartyId` reference | partial | Aggregate stores reference; app layer ensures no saved ApplicantParty mutation. |
| SC-04-BI-007 / SC-04-BI-008 | application transaction | partial / outside aggregate | New ApplicantParty + request atomicity is application/persistence coordination. |
| REQ-CMD-APPROVE-001 | `ApproveReview` | covered | Request and Review both approved. |
| REQ-CMD-REJECT-001 | `RejectReview` | covered | Feedback optional current direction. |
| REQ-LC-002..REQ-LC-006 | review methods/status guards | covered | Core review lifecycle. |
| REQ-IBS-001 | `ApproveReview` | covered | Approved request has review decision. |
| REQ-IBS-002 | `RejectionFeedback` when present + optional policy | covered / policy-sensitive | Current direction: feedback optional. |
| AGR-UCQ-002 | no exchange creation in `ApproveReview` | covered | Approval enables but does not create exchange. |
| AGR-UCQ-001 | cross-aggregate precondition | outside this aggregate | Exchange creation verifies Approved request; see exchange draft. |

## 12. Persistence / EF Notes

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - Owned State
    - Value Objects Used
    - Aggregate Boundary
    - Cross-Aggregate Relations
    - RequestReview child entity
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Value Objects Used
  Not checked:
    - current EF configuration unless explicitly reviewed
```

```text
RequestReview is an owned/child entity of ConnectionRequest for domain purposes.
Review has no repository.
Request stores scalar ids for external aggregate/account/employee references.
Application/repository layer is responsible for loading related ApplicantParty, Employee and AgreementProposalExchange when coordinating behavior.
```

## 13. Cross-Layer Placement Notes

```text
Sources:
  Format/process:
    - planning/domain/domain-responsibility-map.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
  Content:
    - Aggregate Boundary
    - Domain Methods / Commands
    - Cross-Aggregate Relations
    - Behavior Coverage
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
    - Cross-Aggregate Relations
    - Behavior Coverage
  Not checked:
    - API/client/slice implementation unless explicitly reviewed
```

Application layer:

```text
- owns ApplicantParty selection/creation + request creation transaction;
- owns employee auth/account lookup before review commands;
- owns final-refusal coordination between AgreementProposalExchange and Request.
```

API:

```text
- owns DTO branch validation for Existing vs New applicant context;
- owns transport-level validation and response shape.
```

Client:

```text
- owns form branch UX, prefill/default applicant behavior and validation feedback display.
```

Testing:

```text
- domain tests should cover lifecycle/invariant/no-mutation behavior;
- integration tests should cover atomic new applicant + request creation and auth/authorization behavior.
```

## 14. Questions / Decisions

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
  Content:
    - Source Inputs
    - Behavior Coverage
    - Cross-Layer Placement Notes
    - planning/domain/value-objects/object-address.md @ Doc version: v0.1.0
    - planning/domain/value-objects/rejection-feedback.md @ Doc version: v0.1.0
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
  Internal dependencies:
    - Source Inputs
    - Behavior Coverage
    - Cross-Aggregate Relations
  Not checked:
    - sources needed to resolve open snapshot/failure-metadata/feedback policy questions
```

Open:

```text
- Should request store immutable applicant snapshot data, or is ApplicantPartyId reference sufficient for all current read/detail scenarios?
- Should AgreementExchangeFailed store AgreementProposalExchangeId/failedAt as value object later, or remain status-only for now?
- Should RejectionFeedback stay optional long-term, or should scenario/API direction require it later?
- Should current/default ApplicantParty selection remain pure application/client behavior with no Request aggregate rule?
```

Accepted:

```text
- RequestReview is not an aggregate and has no repository.
- ConnectionRequest owns RequestReview.
- ApproveReview does not create AgreementProposalExchange.
- AgreementProposalExchange and ConnectionRequest are separate aggregates.
- Feedback is optional in current synchronized direction; if provided, it must be a valid RejectionFeedback value.
```

Deferred:

```text
- Future request types beyond ConnectionRequest.
- Assignment/department rules for employee dashboard/review access.
- Immutable applicant snapshot policy.
```

## 15. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/source-cascade-sync-workflow.md @ version not confirmed in this batch
  Content:
    - changed sections in this ConnectionRequest aggregate draft
    - planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.1.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ version not confirmed in this batch
  Internal dependencies:
    - all changed sections in this draft
  Not checked:
    - downstream aggregates/slices not reviewed in this pass
```

```text
- Extracted as second aggregate draft after AgreementProposalExchange pilot.
- Added ObjectAddress and RejectionFeedback value-object draft references.
- Mapped SC-04 and SC-07B behavior items to the Request / ConnectionRequest aggregate boundary.
- Added `Doc version: v0.1.0`.
- Added local section-level fenced `Sources:` blocks for aggregate draft work.
- Reclassified `## 2. Source Inputs` as overview; section-level `Sources:` blocks are authoritative for local section work.
- Clarified scenario-specific DATA rule: prefer scenario text spec #DATA; use `planning/diagrams/scenario-data/` only for reusable/shared/audited/transitional sidecars.
- No ConnectionRequest domain behavior semantics changed in this pass.
```
