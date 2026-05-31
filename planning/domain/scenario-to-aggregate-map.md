# Scenario To Aggregate Map

Status: draft / first-pass aggregate-based domain map  
Scope: scenario behavior sources -> domain aggregate/value-object discovery

## 1. Purpose

This file maps scenario-layer behavior sources to domain aggregate candidates, value object candidates, cross-aggregate relations and next domain draft files.

This file is not the final domain model. It is the domain discovery bridge and current first-pass scenario-to-domain map.

## 2. Source Model

Primary scenario/domain discovery sources:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
planning/diagrams/scenario-questions-register.md
```

Scenario-specific DATA source rule:

```text
Primary scenario-specific DATA belongs in the relevant scenario text spec #DATA section.
planning/diagrams/scenario-data/ is for reusable/shared/audited/transitional DATA sidecars.
```

Domain drafting / local source block sources:

```text
planning/source-cascade-sync-workflow.md
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md
planning/domain/aggregate-drafting-workflow.md
planning/domain/aggregate-draft-template.md
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
Domain.EnergyManagement/Accounts/
Domain.EnergyManagement/Applicants/
Domain.EnergyManagement/Employees/
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
Domain source-sync register has not been derived yet.
Full UI sidecar coverage for all domain flows was not audited in these extraction passes.
Full auth/session/EF mapping implementation audit is not done.
Scenario-data sidecars were not promoted to primary scenario-specific DATA unless explicitly named as reusable/shared/audited/transitional sources in local Sources blocks.
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
| `SC-01` Client registration | CMD, VI, auth/account | `Account` / `ClientAccount` | existing Email / PasswordHash implementation VOs | registration orchestration and password hashing are application/auth concerns | Account extracted first-pass |
| `SC-02` Login | auth/session, READ | `Account` | existing Email / PasswordHash implementation VOs | auth/session is application/infrastructure; aggregate only owns account identity/state | Account mapped, not full auth extraction |
| `SC-03B` Account owner verified / activation | LC, IBS, security | `Account` | TBD | activation lifecycle needs later deeper audit | Account first-pass only |
| `SC-04` Client Request Creation | CMD, LC, IBS, VI, UCQ, NW | `ApplicantParty`, `ConnectionRequest`, `Account` | `ObjectAddress`, `ApplicantIdentity`, `ApplicantContact` | new applicant branch creates ApplicantParty + Request in application transaction; existing applicant branch references ApplicantParty | ApplicantParty and Request extracted first-pass |
| `SC-05` My Requests / own request details | READ | `ConnectionRequest` as read source | TBD | list/details are read-model/API/client placement, not aggregate methods | read behavior mapped; not aggregate-owned |
| `SC-06` / `SC-07A` Employee dashboard/details | READ | `ConnectionRequest`, `Account` / `Employee` | TBD | employee list/details and action availability are read/application/API/client placement | read behavior mapped; not aggregate-owned |
| `SC-07B` Employee Request Review | CMD, LC, IBS, NW, UCQ | `ConnectionRequest`, `Account` / `Employee`, `ApplicantParty` | `RejectionFeedback` | approval verifies ApplicantParty by application coordination; rejection stays in request review | Request and Account extracted; Applicant coordination noted |
| `SC-10` Applicant Data | CMD, VI, LC | `ApplicantParty` | `ApplicantIdentity`, `ApplicantContact` | verified edit/versioning remains open | ApplicantParty extracted first-pass |
| `SC-10B` My Applicant Parties | READ, CMD, LC, UCQ | `ApplicantParty` | `ApplicantIdentity`, `ApplicantContact` | current/default uniqueness is application/persistence coordination | ApplicantParty extracted first-pass |
| `SC-13D` Employee Agreement Proposal Create / Send Version | CMD, LC, IBS, VI, UCQ | `AgreementProposalExchange`, `ConnectionRequest`, `Account` / `Employee` | `AgreementProposalVersion`, `AgreementProposalAuthor`, `AgreementDocumentRef`, `ProposalComment` | start exchange requires Approved Request; exchange does not directly mutate Request | exchange extracted |
| `L2-employee-review-agreement` agreement exchange items | CMD, LC, VI, boundary, final refusal | `AgreementProposalExchange`, `ConnectionRequest`, `Account` / `Employee` | `FinalRefusalReason`, agreement proposal VOs | final refusal may coordinate Request AgreementExchangeFailed outside exchange aggregate | exchange/request relation mapped |

## 5. Aggregate Register

| Aggregate | Root | Child entities / concrete types | Source scenarios/items | Draft file | Status |
|---|---|---|---|---|---|
| Account | Account | ClientAccount, Employee | SC-01, SC-02, SC-03B, employee command actor sources | `planning/domain/aggregates/account.md` | prepared with `Doc version: v0.1.0` and local section `Sources:` blocks |
| ApplicantParty | ApplicantParty | IndividualApplicantParty | SC-10, SC-10B, SC-04, old domain drafts | `planning/domain/aggregates/applicant-party.md` | prepared with `Doc version: v0.1.0` and local section `Sources:` blocks |
| ConnectionRequest | ConnectionRequest | RequestReview | SC-04, SC-05, SC-06, SC-07A, SC-07B, SC-13D coordination | `planning/domain/aggregates/connection-request.md` | prepared with `Doc version: v0.1.0` and local section `Sources:` blocks |
| AgreementProposalExchange | AgreementProposalExchange | AgreementProposal | SC-13D, L2 agreement behavior items | `planning/domain/aggregates/agreement-proposal-exchange.md` | prepared with `Doc version: v0.1.0` and local section `Sources:` blocks |

## 6. Value Object Register

| Value object | Source VI items / source area | Used by aggregates | Draft file | Status |
|---|---|---|---|---|
| AgreementProposalVersion | SC-13D / agreement versioning | AgreementProposalExchange | `planning/domain/value-objects/agreement-proposal-version.md` | extracted |
| AgreementProposalAuthor | SC-13D / employee-client proposal authors | AgreementProposalExchange | `planning/domain/value-objects/agreement-proposal-author.md` | extracted |
| AgreementDocumentRef | SC-13D / proposal document reference | AgreementProposalExchange | `planning/domain/value-objects/agreement-document-ref.md` | extracted |
| ProposalComment | SC-13D / optional proposal comment | AgreementProposalExchange | `planning/domain/value-objects/proposal-comment.md` | extracted |
| FinalRefusalReason | L2 agreement final refusal | AgreementProposalExchange | `planning/domain/value-objects/final-refusal-reason.md` | extracted |
| ObjectAddress | SC-04 request object address | ConnectionRequest | `planning/domain/value-objects/object-address.md` | extracted |
| RejectionFeedback | SC-07B rejection feedback | ConnectionRequest / RequestReview | `planning/domain/value-objects/rejection-feedback.md` | extracted |
| ApplicantIdentity | SC-10 applicant identity data | ApplicantParty | `planning/domain/value-objects/applicant-identity.md` | extracted first-pass |
| ApplicantContact | SC-10 applicant contact data | ApplicantParty | `planning/domain/value-objects/applicant-contact.md` | extracted first-pass |
| Email / PasswordHash / FullName / PhoneNumber | common implementation VOs | Account / ApplicantParty | no separate domain draft yet | implementation VO only for now |

## 7. Cross-Aggregate Relations

| From aggregate | To aggregate | Relation | Owner / coordination boundary | Notes |
|---|---|---|---|---|
| ApplicantParty | Account / ClientAccount | belongs to client account | scalar ClientAccountId | Applicant contact email is separate from account auth email. |
| ConnectionRequest | ApplicantParty | request created from selected applicant | scalar ApplicantPartyId | Request does not mutate/relink applicant party when default changes. |
| ConnectionRequest | Account / ClientAccount | client owner | scalar ClientAccountId | Current implementation also stores client account id. |
| ConnectionRequest.RequestReview | Account / Employee | review actor | scalar employee ids | Employee actor is Account subtype. |
| AgreementProposalExchange | ConnectionRequest | exchange for approved request | scalar RequestId + app coordination | Exchange should not directly mutate Request. |
| AgreementProposalExchange | Account / Employee | employee proposal/final refusal actor | scalar employee ids | Employee capability checks live on Employee. |
| AgreementProposalExchange | Account / ClientAccount | client participant/proposal author | scalar client id | Client acceptance/proposal author data stays local to exchange. |

## 8. Application Coordination Notes

Rules that should not be owned by a single aggregate:

```text
- first-of-type ApplicantParty current/default selection;
- switching current/default ApplicantParty per ClientAccountId + ApplicantPartyType;
- creating ApplicantParty and ConnectionRequest in one request creation use case;
- marking ApplicantParty verified after request approval;
- starting AgreementProposalExchange only from Approved ConnectionRequest;
- marking an approved request as AgreementExchangeFailed after exchange final refusal, if that status remains accepted;
- resolving employee actor from authenticated Account.Id.
```

Rules that are likely aggregate-owned:

```text
- ApplicantParty minimum identity/contact data and verification marker;
- ConnectionRequest review lifecycle and request status transitions;
- AgreementProposalExchange proposal version lifecycle and final refusal;
- Account/Employee role, activation and command capability checks.
```

Rules intentionally outside domain:

```text
- dashboard/list/read shape;
- DTO validation and UI field presentation;
- file/blob storage;
- auth token/session mechanics.
```

## 9. Discovery Questions

Open:

```text
- Should ConnectionRequest store immutable applicant snapshot data in addition to ApplicantPartyId?
- What is the final ApplicantParty edit/archive/versioning model after verification or request reference?
- Should Email / PasswordHash / FullName / PhoneNumber receive separate domain value-object docs later?
- How should account activation/suspension/deactivation become explicit domain commands later?
- Which domain sources should receive explicit Doc version markers after the first domain source-sync register is derived?
```

Accepted:

```text
- New target model is aggregate-based.
- Old monolithic domain drafts are historical discovery snapshots until extracted.
- Employee is a concrete Account subtype.
- ApplicantParty is separate from ConnectionRequest.
- AgreementProposalExchange is separate from ConnectionRequest.
```

## 10. Next Domain Work

Priority:

```text
1. Derive `planning/domain/domain-source-sync-register.md` from local aggregate section `Sources:` blocks.
2. Review cross-aggregate dependencies in the domain source-sync register before touching slice drafts.
3. Revisit ApplicantParty edit/archive/versioning questions.
4. Revisit request applicant snapshot question.
5. Audit Account activation/auth-session boundary if needed.
6. Add or refine domain-model-overview.md as aggregate docs evolve.
```

Blocked/deferred:

```text
Slice draft refactor is deferred until after the domain source-sync register is created and reviewed.
```

## 11. Source Delta / Change Log

```text
- AgreementProposalExchange extracted as first pilot.
- ConnectionRequest extracted as second pilot.
- ApplicantParty and Account extracted to complete first-pass aggregate-based domain layer.
- Account/Employee TPH decision extracted into planning/domain/decisions/.
- Domain model overview added as first-pass map.
- Source model cleaned up after F7K-A1..A4: scenario text spec #DATA is primary for scenario-specific DATA; scenario-data sidecars are reusable/shared/audited/transitional by default.
- Aggregate Register statuses updated to show all four active aggregate drafts are prepared with `Doc version: v0.1.0` and local section `Sources:` blocks.
- Next domain work updated to derive `planning/domain/domain-source-sync-register.md` before slice refactor.
```
