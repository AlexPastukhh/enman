# Scenario To Aggregate Map

Status: draft / two aggregate extractions applied  
Scope: scenario behavior sources -> domain aggregate/value-object discovery

## 1. Purpose

This file maps scenario-layer behavior sources to domain aggregate candidates, value object candidates, cross-aggregate relations and next domain draft files.

This file is not the final domain model. It is the domain discovery bridge.

## 2. Source Model

Primary sources:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
planning/diagrams/scenario-questions-register.md
```

Historical / cross-check sources:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/domain-drafts/domain-draft-01.md
planning/tables/domain-drafts/domain-draft-02.md
planning/domain-model.md
planning/domain-design-input-navigation-notes.md
planning/scenario-domain-validation-principles.md
```

Existing implementation sources checked for extraction passes:

```text
Domain.EnergyManagement/AgreementProposals/
Domain.EnergyManagement/Requests/
Domain.EnergyManagement/DocumentManaging/Address.cs
Tests.EnergyManagement/Domain/AgreementProposals/
Tests.EnergyManagement/Domain/Requests/
Tests.EnergyManagement/Integration/App/Requests/
Tests.EnergyManagement/Integration/App/EmployeeRequests/
```

Not checked:

```text
Full scenario-by-scenario discovery pass is not done yet.
Full source/version/cascade alignment is deferred.
Full UI sidecar coverage for request creation/review was not audited in the Request extraction pass.
```

## 3. Behavior Item Category Mapping

| Category | Domain interpretation | Output |
|---|---|---|
| CMD | Command behavior | Candidate aggregate method / domain command / application command |
| LC | Lifecycle / state / condition behavior | State machine / lifecycle rule |
| IBS | Impossible business state | Invariant / impossible state |
| VI | Value integrity | Value object candidate / value validation rule |
| UCQ | Use-case coordination | Cross-aggregate relation / application coordination |
| READ | Read/list/access behavior | Read model / query / access placement |
| INT | Infrastructure/integration expectation | Out-of-domain note / app/infrastructure/testing note |
| FUT | Future/deferred behavior | Deferred note |
| NW | No-write / failure preservation | Failure/no-write proof requirement |

## 4. Scenario-To-Aggregate Matrix

| Scenario / behavior source | Key behavior categories | Aggregate candidates | Value object candidates | Coordination notes | Status |
|---|---|---|---|---|---|
| `SC-04` Client Request Creation | CMD, LC, IBS, VI, UCQ, NW | `ConnectionRequest`, `ApplicantParty` | `ObjectAddress` | existing ApplicantParty branch references saved ApplicantParty; new applicant branch is application transaction creating ApplicantParty + Request | Request extracted; ApplicantParty remains candidate |
| `SC-07B` Employee Request Review | CMD, LC, IBS, NW, UCQ | `ConnectionRequest` | `RejectionFeedback` | approval enables but does not create AgreementProposalExchange | Request extracted |
| `SC-05` My Requests / own request details | READ | `ConnectionRequest` as read source | TBD | list/details are read-model/API/client placement, not aggregate methods | read behavior mapped; not aggregate-owned |
| `SC-06` / `SC-07A` Employee dashboard/details | READ | `ConnectionRequest` as read source | TBD | employee list/details and action availability are read/application/API/client placement | read behavior mapped; not aggregate-owned |
| `SC-13D` Employee Agreement Proposal Create / Send Version | CMD, LC, IBS, VI, UCQ | `AgreementProposalExchange` | `AgreementProposalVersion`, `AgreementProposalAuthor`, `AgreementDocumentRef`, `ProposalComment` | start exchange requires Approved Request; exchange does not mutate Request | exchange extracted |
| `L2-employee-review-agreement` agreement exchange items | CMD, LC, VI, boundary, final refusal | `AgreementProposalExchange`, `ConnectionRequest` | `AgreementDocumentRef`, `ProposalComment`, `FinalRefusalReason`, `RejectionFeedback` | final refusal requires application coordination with Request | exchange + request extracted for current scope |
| `SC-13B` Client proposal response | CMD, LC, IBS, VI | `AgreementProposalExchange` | `AgreementDocumentRef`, `ProposalComment` | Client can send counter proposal only against active Employee proposal | related source, not fully extracted in this pass |

## 5. Aggregate Candidate Register

| Aggregate candidate | Root | Child entities | Source scenarios/items | Draft file | Status |
|---|---|---|---|---|---|
| AgreementProposalExchange | AgreementProposalExchange | AgreementProposal | SC-13D, L2 agreement items, Domain Draft 02, current AgreementProposal implementation | `planning/domain/aggregates/agreement-proposal-exchange.md` | extracted pilot draft |
| Request / ConnectionRequest | ConnectionRequest | RequestReview | SC-04, SC-07B, SC-05/06/07A read sources, L2 agreement coordination, Domain Draft 02, current Request implementation | `planning/domain/aggregates/connection-request.md` | extracted draft |
| ApplicantParty | ApplicantParty / concrete applicant party type | IndividualApplicantParty and future concrete applicant parties | historical Domain Draft 01/02, SC-04, SC-10/10B, request creation sources | TBD | candidate / next extraction |
| Account / ClientAccount / Employee | Account / ClientAccount / Employee | TBD | L2 account/employee clarification, domain decision candidate | TBD | candidate / decision-dependent |

## 6. Value Object Candidate Register

| Value object candidate | Source VI/items | Used by aggregates | Draft file | Status |
|---|---|---|---|---|
| AgreementProposalVersion | `L2-AGR-VERSION-001`, `L2-AGR-EMP-START-002` | AgreementProposalExchange | `planning/domain/value-objects/agreement-proposal-version.md` | extracted pilot draft |
| AgreementProposalAuthor | `AGR-IBS-001`, `L2-AGR-AUTHOR-001` | AgreementProposalExchange | `planning/domain/value-objects/agreement-proposal-author.md` | extracted pilot draft |
| AgreementDocumentRef | `AGR-VI-001`, `AGR-IBS-002`, `L2-AGR-DOC-001`, `L2-AGR-DOC-002` | AgreementProposalExchange | `planning/domain/value-objects/agreement-document-ref.md` | extracted pilot draft |
| ProposalComment | `AGR-VI-002`, `L2-AGR-COMMENT-001` | AgreementProposalExchange | `planning/domain/value-objects/proposal-comment.md` | extracted pilot draft |
| FinalRefusalReason | `L2-AGR-FINAL-002` | AgreementProposalExchange | `planning/domain/value-objects/final-refusal-reason.md` | extracted pilot draft |
| ObjectAddress / Address | `REQ-IBS-003`, `REQ-VI-001`, `SC-04-BI-002` | ConnectionRequest | `planning/domain/value-objects/object-address.md` | extracted draft |
| RejectionFeedback | `REQ-IBS-002`, `REQ-CMD-REJECT-001`, `L2-REVIEW-REJECT-002` | ConnectionRequest / RequestReview | `planning/domain/value-objects/rejection-feedback.md` | extracted draft |

## 7. Cross-Aggregate Relations

| From aggregate | To aggregate | Relation | Owner / coordination boundary | Notes |
|---|---|---|---|---|
| ConnectionRequest | ApplicantParty | Request uses one ApplicantParty context | Application loads/creates ApplicantParty; Request stores scalar ids | New ApplicantParty + request is one app transaction; Request does not own ApplicantParty creation. |
| ConnectionRequest | Employee | Employee starts/completes review | Application/auth resolves Employee; RequestReview stores scalar ids | Employee capability is checked through Employee object. |
| AgreementProposalExchange | Request / ConnectionRequest | Exchange exists for approved request | Application coordination + exchange start precheck | Exchange stores `RequestId`; Request does not own exchange. |
| AgreementProposalExchange | Request / ConnectionRequest | Final refusal can mark approved request as AgreementExchangeFailed | Application service coordinates; Request owns its status change | Exchange does not directly mutate Request. |
| AgreementProposalExchange | Employee | Employee starts exchange, sends employee versions and final-refuses | Application/auth resolves Employee; exchange stores scalar ids | Do not use EmployeeRef in proposal author. |
| AgreementProposalExchange | ClientAccount | Client accepts/sends proposal; exchange stores `ClientAccountId` | Application/auth resolves ClientAccount; exchange stores scalar ids | Client visibility/query remains outside aggregate. |
| AgreementProposalExchange | File/blob storage | Proposal document file metadata becomes AgreementDocumentRef | Application/infrastructure owns storage; value object owns metadata integrity | Domain does not upload/read/delete bytes. |

## 8. Application Coordination Notes

Rules that should not be owned by a single aggregate:
- creating a new ApplicantParty and ConnectionRequest atomically;
- resolving/authorizing existing ApplicantParty ownership before request creation;
- resolving Employee from auth/account context before review commands;
- creating AgreementProposalExchange after request approval;
- final refusal impact on Request status;
- loading current Employee/ClientAccount from auth context;
- file upload/storage before agreement document value object creation;
- read/list/dashboard filtering.

Rules that are likely aggregate-owned:
- request creation validation for persisted ApplicantParty reference, details and object address presence;
- request review lifecycle status transitions;
- review starter/completer constraints;
- request Approved/Rejected status changes;
- AgreementExchangeFailed transition guard;
- exchange lifecycle status transitions;
- active proposal version changes;
- proposal supersede/accept state transitions;
- proposal author/document/value invariants;
- final refusal exchange state.

Rules intentionally outside domain:
- physical file storage;
- UI step ordering;
- DTO/multipart transport validation;
- dashboard/read model joins;
- client-side branch form state.

## 9. Discovery Questions

Open:
- `Q-SC-13D-001`: source question still asks whether replaced proposal version should be named Superseded/Replaced instead of Rejected. Current extracted domain direction uses `SupersededByCounterProposal`; scenario question should be resolved/synchronized later.
- `Q-SC-13D-002`: source question asks whether proposal text details/comment are required or optional. Current extracted domain direction treats comment as optional; if present, it must be non-empty and max-length constrained.
- Should Request store immutable applicant snapshot data, or is `ApplicantPartyId` reference sufficient for all current read/detail scenarios?
- Should `AgreementExchangeFailed` store `AgreementProposalExchangeId` / failed-at metadata in a future value object, or remain status-only for now?
- Which old domain draft decisions should move into `planning/domain/decisions/` first? Current candidate: account/employee TPH decision.
- Whether `domain-model-overview.md` should be created after ApplicantParty extraction or after 2-3 aggregate drafts.

Accepted:
- New target model is aggregate-based.
- Old monolithic domain drafts are historical discovery snapshots until extracted.
- `AgreementProposalExchange` was the first pilot aggregate extraction.
- `ConnectionRequest` was the second aggregate extraction.
- `AgreementProposalExchange` and Request are separate aggregates.
- `RequestReview` is child entity of `ConnectionRequest`, not aggregate.
- `AgreementProposal` is child entity of `AgreementProposalExchange`, not aggregate.
- Agreement proposal document/comment/version/author/final refusal reason are extracted as value object drafts for the exchange pilot.
- Object address and rejection feedback are extracted as value object drafts for Request.

## 10. Next Aggregate Drafts To Create

Priority:
1. ApplicantParty
2. Account / ClientAccount / Employee

Blocked / deferred:
- Full source/version/cascade alignment.
- `domain-model-overview.md` until the map has more accepted aggregate coverage.
- Immutable applicant snapshot decision until ApplicantParty extraction/source review.

## 11. Source Delta / Change Log

```text
- Added first pilot extraction for AgreementProposalExchange.
- Added value object draft references for AgreementProposalVersion, AgreementProposalAuthor, AgreementDocumentRef, ProposalComment and FinalRefusalReason.
- Added second extraction for ConnectionRequest / Request and RequestReview child entity.
- Added ObjectAddress and RejectionFeedback value object draft references.
- Kept old monolithic domain drafts as historical source snapshots.
```
