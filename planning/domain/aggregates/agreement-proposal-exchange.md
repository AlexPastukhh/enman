# Domain Aggregate Draft — AgreementProposalExchange

Status: draft / first aggregate extraction pilot  
Doc version: v0.1.0  
Scope: post-approval agreement proposal exchange, proposal versions, final refusal and cross-aggregate coordination with Request

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
  Internal dependencies:
    - none
  Not checked:
    - full scenario-by-scenario domain discovery outside AgreementProposalExchange
    - full source/version/cascade alignment for all agreement/request sources
```

`AgreementProposalExchange` owns post-approval agreement proposal negotiation between Employee and Client.

It owns:

```text
- exchange lifecycle;
- proposal version chain;
- active proposal version;
- proposal child entities;
- employee/client proposal send transitions;
- client acceptance transition;
- final refusal transition.
```

It does not own Request review or Request status transitions.

`AgreementProposalExchange` and `Request / ConnectionRequest` are separate aggregates. Application service coordination is required when exchange decisions affect request state.

## 2. Source Inputs

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-data/README.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
  Internal dependencies:
    - none
  Not checked:
    - full scenario-by-scenario domain discovery outside AgreementProposalExchange
    - full source/version/cascade alignment
```

This section is the aggregate-level reviewed source overview. Section-level `Sources:` blocks below are authoritative for local section work.

Scenario text sources:

```text
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
```

Scenario-specific DATA rule:

```text
Primary SC-13D scenario-specific DATA belongs in the scenario text spec #DATA section.
Use planning/diagrams/scenario-data/ only for reusable/shared/audited/transitional DATA sidecars.
```

Reusable/shared/audited/transitional DATA sidecars checked or referenced by the prior draft:

```text
planning/diagrams/scenario-data/SC-13D-employee-agreement-proposal-create-response-data.md @ Doc version: v0.1.0
planning/diagrams/scenario-data/L2-employee-review-agreement-data.md @ Doc version: v0.1.0
```

Scenario clarification sources:

```text
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
```

Behavior items:

```text
planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
```

Historical domain sources:

```text
planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
```

Existing implementation sources checked in prior archive/source pass:

```text
Domain.EnergyManagement/AgreementProposals/AgreementProposalExchange.cs
Domain.EnergyManagement/AgreementProposals/AgreementProposal.cs
Domain.EnergyManagement/AgreementProposals/AgreementProposalVersion.cs
Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs
Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs
Domain.EnergyManagement/AgreementProposals/ProposalComment.cs
Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs
Tests.EnergyManagement/Domain/AgreementProposals/AgreementProposalExchangeTests.cs
```

Not checked:

```text
Full scenario-by-scenario domain discovery outside AgreementProposalExchange was not audited in this pass.
Full source/version/cascade alignment is still deferred.
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
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
  Internal dependencies:
    - Purpose
  Not checked:
    - current runtime implementation boundary beyond previously checked archive/source pass
```

Aggregate root:
- `AgreementProposalExchange`

Child entities:
- `AgreementProposal`

Owned value-like concepts:
- `AgreementProposalVersion`
- `AgreementProposalAuthor`
- `AgreementDocumentRef`
- `ProposalComment`
- `FinalRefusalReason`

Not part of this aggregate:
- `Request / ConnectionRequest`
- `RequestReview`
- `Employee`
- `ClientAccount`
- file/blob storage adapter
- API DTO validation
- read-model/dashboard/listing queries

External aggregate references:
- `RequestId`
- `ClientAccountId`
- Employee identity through `Employee.Id` / proposal sender id
- Client identity through `ClientAccount.Id` / proposal sender id

## 4. Owned State

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-version.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-author.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-document-ref.md @ Doc version: v0.1.0
    - planning/domain/value-objects/proposal-comment.md @ Doc version: v0.1.0
    - planning/domain/value-objects/final-refusal-reason.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
  Not checked:
    - current persistence mapping / EF configuration
```

Root state:
- `RequestId`
- `ClientAccountId`
- `Status`
- `ActiveProposalVersion`
- `Proposals`
- `FinalRefusedByEmployeeId?`
- `FinalRefusedAt?`
- `FinalRefusalReason?`
- `CreatedAt`

Child state:
- `AgreementProposal.Id`
- `AgreementProposalExchangeId`
- `Version`
- `Author`
- `State`
- `Document`
- `Comment?`
- `CreatedAt`

Derived/read-only state:
- active proposal resolved by `ActiveProposalVersion`
- next proposal version resolved from current proposal versions

Not stored here:
- request status
- review status
- file bytes
- employee/client profile data
- UI presentation state

## 5. Domain Methods / Commands

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-version.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-author.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-document-ref.md @ Doc version: v0.1.0
    - planning/domain/value-objects/proposal-comment.md @ Doc version: v0.1.0
    - planning/domain/value-objects/final-refusal-reason.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
  Not checked:
    - application service orchestration beyond previously checked source pass
    - current implementation code/tests beyond previously checked archive/source pass
```

### `StartByEmployee`

Purpose:
- Create a new exchange from an Approved request with the first Employee proposal.

Input:
- Approved `ConnectionRequest`
- `AgreementDocumentRef`
- optional `ProposalComment`
- `Employee`
- `startedAt`

Preconditions:
- request exists and has valid id;
- request status is `Approved`;
- request has valid `ClientAccountId`;
- document exists;
- employee exists and can start agreement exchange.

State changes:
- creates `AgreementProposalExchange`;
- stores `RequestId` and `ClientAccountId`;
- sets `Status = AwaitingClientConfirmation`;
- sets `ActiveProposalVersion = AgreementProposalVersion.First`;
- adds first `AgreementProposal` authored by Employee.

Domain errors:
- request required;
- approved request required;
- client account required;
- document required;
- employee required;
- employee capability failure.

Observable result:
- one exchange with one active employee proposal.

Source behavior:
- `AGR-CMD-EMP-SEND-001`
- `AGR-LC-001`
- `AGR-UCQ-001`
- `L2-AGR-EMP-START-001`
- `L2-AGR-EMP-START-002`

### `ClientAcceptActiveProposal`

Purpose:
- Let Client accept active Employee proposal while exchange awaits client confirmation.

Input:
- `ClientAccount`
- `acceptedAt`

Preconditions:
- client exists;
- exchange status is `AwaitingClientConfirmation`;
- active proposal can be accepted.

State changes:
- active proposal becomes `Accepted`;
- exchange `Status = Accepted`.

Domain errors:
- client required;
- only awaiting client confirmation can be accepted;
- active proposal cannot be accepted from invalid proposal state.

Observable result:
- accepted exchange keeps accepted active proposal version.

Source behavior:
- `L2-AGR-CLIENT-ACCEPT-001`

### `ClientSendOwnVersion`

Purpose:
- Let Client send a counter proposal version in response to an active Employee proposal.

Input:
- `AgreementDocumentRef`
- optional `ProposalComment`
- `ClientAccount`
- `createdAt`

Preconditions:
- exchange status is `AwaitingClientConfirmation`;
- document exists;
- client exists;
- active proposal is authored by Employee.

State changes:
- active Employee proposal is marked `SupersededByCounterProposal`;
- next local version is generated;
- Client proposal is added;
- `ActiveProposalVersion` moves to new version;
- exchange `Status = AwaitingEmployeeResponse`.

Domain errors:
- wrong exchange status;
- document required;
- client required;
- client can respond only to active Employee proposal.

Observable result:
- one new active client proposal version.

Source behavior:
- `L2-AGR-CLIENT-SEND-001`
- `L2-AGR-VERSION-001`
- `L2-AGR-SUPERSEDE-001`

### `EmployeeSendNewVersion`

Purpose:
- Let Employee send a new proposal version after active Client proposal.

Input:
- `AgreementDocumentRef`
- optional `ProposalComment`
- `Employee`
- `createdAt`

Preconditions:
- exchange status is `AwaitingEmployeeResponse`;
- document exists;
- employee exists and can send agreement proposal;
- active proposal is authored by Client.

State changes:
- active Client proposal is marked `SupersededByCounterProposal`;
- next local version is generated;
- Employee proposal is added;
- `ActiveProposalVersion` moves to new version;
- exchange `Status = AwaitingClientConfirmation`.

Domain errors:
- wrong exchange status;
- document required;
- employee required;
- employee capability failure;
- employee can supersede only client proposal.

Observable result:
- one new active employee proposal version.

Source behavior:
- `AGR-CMD-EMP-NEW-001`
- `AGR-LC-007`
- `L2-AGR-EMP-NEW-001`
- `L2-AGR-EMP-NEW-002`
- `L2-AGR-SUPERSEDE-001`

### `FinalRefuseProposal`

Purpose:
- Let Employee finally refuse an exchange without creating a new proposal version.

Input:
- `Employee`
- optional `FinalRefusalReason`
- `refusedAt`

Preconditions:
- employee exists and can final-refuse agreement;
- exchange is not `Accepted`;
- exchange is not already `FinallyRefused`;
- exchange status is `AwaitingClientConfirmation` or `AwaitingEmployeeResponse`.

State changes:
- exchange `Status = FinallyRefused`;
- stores `FinalRefusedByEmployeeId`;
- stores `FinalRefusedAt`;
- stores optional `FinalRefusalReason`.

Domain errors:
- employee required;
- employee capability failure;
- accepted exchange cannot be refused;
- already refused exchange cannot be refused again;
- exchange cannot be refused from current status.

Observable result:
- exchange is finally refused and no proposal version is created.

Source behavior:
- `L2-AGR-FINAL-001`
- `L2-AGR-FINAL-002`
- `L2-AGR-FINAL-003`

## 6. Invariants

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-version.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-author.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-document-ref.md @ Doc version: v0.1.0
    - planning/domain/value-objects/proposal-comment.md @ Doc version: v0.1.0
    - planning/domain/value-objects/final-refusal-reason.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
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
| Exchange starts only from Approved request | `StartByEmployee` request status precheck | `AGR-UCQ-001`, `L2-AGR-EMP-START-001` | Agreement exchange requires approved request |
| Exchange has ClientAccount reference | `StartByEmployee` reads request `ClientAccountId` | L2/domain direction + current implementation | Client account required |
| First proposal version is 1 | `AgreementProposalVersion.First` | `L2-AGR-EMP-START-002`, `L2-AGR-VERSION-001` | version invariant |
| API/client does not choose version | aggregate generates `GetNextVersion` | `L2-AGR-VERSION-001` | no external setter |
| Proposal has author | `AgreementProposalAuthor` required by `AgreementProposal` factories | `AGR-IBS-001`, `L2-AGR-AUTHOR-001` | proposal cannot be created through valid factory without author |
| Proposal has document ref | proposal factory requires `AgreementDocumentRef` | `AGR-IBS-002`, `AGR-VI-001`, `L2-AGR-DOC-001` | document required |
| Active version points to one proposal | aggregate controls proposal creation and active version update | Domain Draft 02 / implementation | active proposal lookup must resolve one proposal |
| Previous proposal is superseded by counter-proposal, not rejected | `MarkSupersededByCounterProposal` before adding counter proposal | `AGR-LC-007`, `L2-AGR-SUPERSEDE-001` | invalid previous state blocks transition |
| Final refusal does not create proposal version | `FinalRefuseProposal` changes exchange state only | `L2-AGR-FINAL-003` | no proposal added |
| Exchange does not mutate Request directly | aggregate boundary + application coordination | L2 domain direction | request update belongs to Request/application service |

## 7. Lifecycle / State Machine

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Invariants
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - full agreement exchange implementation and tests
```

States:
- `AwaitingClientConfirmation`
- `AwaitingEmployeeResponse`
- `Accepted`
- `FinallyRefused`

Allowed transitions:

```text
No exchange
  -> StartByEmployee
  -> AwaitingClientConfirmation

AwaitingClientConfirmation
  -> ClientAcceptActiveProposal
  -> Accepted

AwaitingClientConfirmation
  -> ClientSendOwnVersion
  -> AwaitingEmployeeResponse

AwaitingEmployeeResponse
  -> EmployeeSendNewVersion
  -> AwaitingClientConfirmation

AwaitingClientConfirmation
  -> Employee FinalRefuseProposal
  -> FinallyRefused

AwaitingEmployeeResponse
  -> Employee FinalRefuseProposal
  -> FinallyRefused
```

Forbidden transitions:
- create exchange from non-Approved request;
- Client sends version when active proposal is not Employee-authored;
- Employee sends new version when active proposal is not Client-authored;
- final refusal after `Accepted`;
- final refusal after `FinallyRefused`;
- final refusal creating a proposal version.

## 8. Impossible States Prevented

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-version.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-author.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-document-ref.md @ Doc version: v0.1.0
    - planning/domain/value-objects/proposal-comment.md @ Doc version: v0.1.0
    - planning/domain/value-objects/final-refusal-reason.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
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
| Exchange exists for non-Approved request | `StartByEmployee` approved-request precheck | `AGR-UCQ-001` |
| Proposal without sender | `AgreementProposalAuthor` required | `AGR-IBS-001` |
| Proposal with ambiguous sender id | `AgreementProposalAuthor(Sender, SenderId)` | `L2-AGR-AUTHOR-001`, naming guardrails |
| Proposal without document/file reference | `AgreementDocumentRef` required | `AGR-IBS-002`, `AGR-VI-001`, `L2-AGR-DOC-001` |
| Invalid proposal version | `AgreementProposalVersion` positive local value | `L2-AGR-VERSION-001` |
| Employee counter-proposal replaces employee proposal | active author check | `AGR-CMD-EMP-NEW-001` |
| Client counter-proposal replaces client proposal | active author check | `L2-AGR-CLIENT-SEND-001` |
| Superseded proposal treated as final rejection | separate proposal state and exchange final refusal state | `AGR-LC-007`, `L2-AGR-SUPERSEDE-001` |
| Final refusal represented as proposal version | `FinalRefuseProposal` changes exchange state only | `L2-AGR-FINAL-003` |
| Exchange directly mutates Request | application coordination boundary | L2 domain direction |

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
    - planning/domain/value-objects/agreement-proposal-version.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-author.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-document-ref.md @ Doc version: v0.1.0
    - planning/domain/value-objects/proposal-comment.md @ Doc version: v0.1.0
    - planning/domain/value-objects/final-refusal-reason.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
  Internal dependencies:
    - Owned State
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - whether future agreement exchange metadata needs additional value-object docs
```

| Value object | File | Purpose in this aggregate |
|---|---|---|
| `AgreementProposalVersion` | `planning/domain/value-objects/agreement-proposal-version.md` | Local per-exchange proposal version number. |
| `AgreementProposalAuthor` | `planning/domain/value-objects/agreement-proposal-author.md` | Sender type + sender id for proposal author. |
| `AgreementDocumentRef` | `planning/domain/value-objects/agreement-document-ref.md` | Metadata reference to accepted/stored proposal document. |
| `ProposalComment` | `planning/domain/value-objects/proposal-comment.md` | Optional proposal text/comment. |
| `FinalRefusalReason` | `planning/domain/value-objects/final-refusal-reason.md` | Optional final refusal explanation. |

## 10. Cross-Aggregate Relations

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
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
- `Request / ConnectionRequest` by `RequestId`.
- `ClientAccount` by `ClientAccountId` and proposal `SenderId`.
- `Employee` by proposal `SenderId` and `FinalRefusedByEmployeeId`.

Rules not owned here:
- Request review approval.
- Request status transition to `AgreementExchangeFailed`.
- Employee authentication and capability resolution.
- Client authorization for exchange visibility/actions.
- File upload/storage.

Application coordination needed:
- Start exchange only after application loads Approved request and passes it to aggregate.
- Final refusal orchestration:
  - `exchange.FinalRefuseProposal(employee, reason, refusedAt)`
  - `request.MarkAgreementExchangeFailed(exchange.Id, failedAt)`
  - save transaction.
- Document upload/storage must occur outside domain before creating `AgreementDocumentRef`.

## 11. Behavior Coverage

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
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
    - behavior items from unrelated scenario branches unless included by mapping
    - all agreement proposal exchange tests in current repo state
```

| Source item | Covered by | Status | Notes |
|---|---|---|---|
| `AGR-CMD-EMP-SEND-001` | `StartByEmployee` | covered | Starts exchange from Approved request with first Employee proposal. |
| `AGR-CMD-EMP-NEW-001` | `EmployeeSendNewVersion` | covered | Employee response to active Client proposal. |
| `AGR-LC-001` | `StartByEmployee` state transition | covered | First proposal creates `AwaitingClientConfirmation`. |
| `AGR-LC-007` | `EmployeeSendNewVersion` + supersede active client proposal | covered with naming note | Uses `SupersededByCounterProposal`, not Rejected. |
| `AGR-IBS-001` | `AgreementProposalAuthor` | covered | Proposal author uses `Sender + SenderId`. |
| `AGR-IBS-002` | `AgreementDocumentRef` required | covered | Proposal cannot be created without document ref. |
| `AGR-VI-001` | `AgreementDocumentRef` | covered | Metadata reference, not bytes/storage adapter. |
| `AGR-VI-002` | `ProposalComment` | partial / source question | Current direction: optional; if present, non-empty/max length. |
| `AGR-UCQ-001` | `StartByEmployee` + application coordination | covered | Exchange only for Approved request. |
| `L2-AGR-CLIENT-ACCEPT-001` | `ClientAcceptActiveProposal` | covered | Client acceptance while awaiting client confirmation. |
| `L2-AGR-CLIENT-SEND-001` | `ClientSendOwnVersion` | covered | Client counter proposal in response to employee proposal. |
| `L2-AGR-EMP-START-001` | `StartByEmployee` | covered | Employee starts exchange for Approved request. |
| `L2-AGR-EMP-START-002` | `AgreementProposalVersion.First` + status | covered | First proposal version is 1. |
| `L2-AGR-EMP-NEW-001` | `EmployeeSendNewVersion` | covered | Employee sends new version when awaiting employee response. |
| `L2-AGR-EMP-NEW-002` | `EmployeeSendNewVersion` transition | covered | Returns to awaiting client confirmation. |
| `L2-AGR-VERSION-001` | aggregate-generated version | covered | API/client does not choose version. |
| `L2-AGR-SUPERSEDE-001` | proposal state transition | covered | Previous proposal superseded by counter proposal. |
| `L2-AGR-AUTHOR-001` | `AgreementProposalAuthor` | covered | Sender + SenderId. |
| `L2-AGR-BOUNDARY-001` | aggregate boundary | covered | Exchange and Request are separate aggregates. |
| `L2-AGR-FINAL-001` | `FinalRefuseProposal` | covered | Employee can final-refuse from active negotiation states. |
| `L2-AGR-FINAL-002` | final refusal state fields | covered | Stores employee/time/reason. |
| `L2-AGR-FINAL-003` | `FinalRefuseProposal` | covered | No new proposal version. |
| `L2-REQ-AGR-FAIL-001` | application coordination with Request | outside aggregate / covered by placement | Request marks agreement exchange failed, not exchange. |
| `L2-AGR-DOC-001` | `AgreementDocumentRef` | covered | Proposal version requires document ref. |
| `L2-AGR-DOC-002` | value object boundary | covered | Metadata only, not bytes/storage. |
| `L2-AGR-COMMENT-001` | `ProposalComment` | covered with optionality note | Optional; if present non-empty. |

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
    - AgreementProposal child entity
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Value Objects Used
  Not checked:
    - current EF configuration unless explicitly reviewed
```

Only where needed:
- `AgreementProposal` is an owned child entity of `AgreementProposalExchange`.
- `AgreementDocumentRef`, `ProposalComment`, `AgreementProposalAuthor`, `AgreementProposalVersion` can be persisted as owned/value-like data.
- `ActiveProposalVersion` is the domain active pointer; avoid domain logic based on DB child id.
- `RequestId` and `ClientAccountId` are scalar references; no Request navigation is needed in domain behavior.
- File bytes are not stored in the domain aggregate.

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
- loads Request / Employee / ClientAccount;
- handles authorization;
- coordinates final refusal with Request status update;
- handles file upload/storage before creating `AgreementDocumentRef`.

API:
- validates DTO/form shape and required transport fields;
- must not let client choose proposal version;
- maps upload metadata to application input.

Client:
- displays active version, status, proposal history and final refusal state;
- does not define domain version numbers.

Testing:
- domain unit tests should cover aggregate transitions, value object invariants and no-write failure cases;
- integration tests should cover application orchestration, document upload/download and transaction boundaries;
- E2E tests should cover user-visible flow only after domain/API/client are stable.

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
    - planning/domain/value-objects/agreement-proposal-version.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-author.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-document-ref.md @ Doc version: v0.1.0
    - planning/domain/value-objects/proposal-comment.md @ Doc version: v0.1.0
    - planning/domain/value-objects/final-refusal-reason.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
  Internal dependencies:
    - Source Inputs
    - Behavior Coverage
    - Cross-Aggregate Relations
  Not checked:
    - sources needed to resolve SC-13D source questions Q-SC-13D-001 / Q-SC-13D-002
```

Open:
- `Q-SC-13D-001`: scenario question still asks whether replaced proposal should be named Superseded/Replaced instead of Rejected. Current domain direction uses `SupersededByCounterProposal`; source question should be resolved/synchronized later.
- `Q-SC-13D-002`: source question asks whether proposal text details/comment are required or optional. Current domain direction treats comment as optional; if present, it must be non-empty and max-length constrained.

Accepted:
- `AgreementProposalExchange` is separate aggregate from Request.
- `AgreementProposal` is a child entity, not aggregate.
- `AgreementProposalVersion` is local per exchange and generated by the exchange.
- `AgreementProposalAuthor` uses `Sender + SenderId`; do not use `EmployeeRef`, `ClientRef` or `AggregateId`.
- `AgreementDocumentRef` is metadata reference only.
- Final refusal belongs to exchange state and is not a separate entity/class.
- Final refusal does not create proposal version.
- Cross-aggregate final refusal effect on Request is application coordination.

Deferred:
- Full source/version/cascade metadata.
- Full extraction of Request / ConnectionRequest aggregate.
- `domain-model-overview.md`.

## 15. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/source-cascade-sync-workflow.md @ version not confirmed in this batch
  Content:
    - changed sections in this AgreementProposalExchange aggregate draft
    - planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.1.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ version not confirmed in this batch
  Internal dependencies:
    - all changed sections in this draft
  Not checked:
    - downstream slices not reviewed in this pass
```

What changed:
- Extracted first aggregate draft from monolithic `domain-draft-02.md`, SC-13D behavior items, L2 agreement behavior items and current AgreementProposal implementation sources.
- Added separate value object draft links for the value concepts used by this aggregate.
- Kept old monolithic drafts as historical source snapshots rather than moving them.
- Added `Doc version: v0.1.0`.
- Added local section-level fenced `Sources:` blocks for aggregate draft work.
- Reclassified `## 2. Source Inputs` as overview; section-level `Sources:` blocks are authoritative for local section work.
- Clarified SC-13D DATA rule: primary scenario-specific DATA is scenario text spec #DATA; scenario-data sidecars are reusable/shared/audited/transitional unless explicitly promoted.
- No AgreementProposalExchange domain behavior semantics changed in this pass.
