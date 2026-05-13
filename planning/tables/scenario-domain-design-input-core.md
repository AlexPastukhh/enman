# Scenario Domain Design Input Core

Status: first draft / domain design input v1  
Branch: `my-changes`  
Target path: `planning/tables/scenario-domain-design-input-core.md`

## 1. Purpose

This file is the practical bridge from corrected scenario specs and DATA files to domain model / value object / aggregate design.

The goal is not to classify every scenario item by architectural layer.

The goal is to extract the design material needed to build domain candidates:

```text
- value object candidates;
- domain object / entity candidates;
- aggregate owner candidates;
- server-side/domain validation rules;
- state/status values;
- state-transition invariants;
- candidate domain methods;
- unresolved domain decisions.
```

This file supersedes the earlier broad responsibility-table direction.

## 2. Source Set

Use:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-diagram-consistency-report.md
```

Do not use stale generated package summaries or stale `.drawio` files as semantic source of truth.

## 3. Domain Validation Model

The domain model should be the source of truth for validity.

Recommended flow:

```text
client form input
-> optional client-side validation for UX
-> server-side application use case
-> value object construction / domain factory / domain method
-> validation result
-> response errors or successful state change
```

Rules:

```text
- client-side validation is helpful but not trusted;
- server-side validation should not manually duplicate rules that belong to value objects/domain methods;
- invalid value objects should not be created;
- invalid aggregate state transitions should be rejected;
- domain validation errors should be convertible into server validation responses.
```

## 4. Value Object Candidate Catalog

| Candidate | Scenarios | Evidence / DATA | Validation responsibility | Owner candidate | Priority |
|---|---|---|---|---|---|
| EmailAddress | SC-01, SC-02, SC-03A, SC-10, SC-17 | registration email, login email, recovery email, applicant email, follow-up email | valid email-like value; required where scenario requires contact/account email | Account, ApplicantData, FollowUpContact | Core |
| Password / PasswordPolicy | SC-01, SC-02, SC-03B | password, new password, confirmation | password required, policy satisfied, confirmation match at input level | Account/Auth | Core |
| FullName | SC-10 | physical person ФИО, ИП ФИО | non-empty person name; accepted name format | ApplicantData | Core |
| PhoneNumber | SC-10, SC-17 | applicant phone, follow-up contact phone | valid phone/contact value if required | ApplicantData, FollowUpContact | Core / Future depending channel |
| Snils | SC-10 | physical person СНИЛС | valid SNILS-like value | PhysicalPersonApplicantData | Core |
| PassportData | SC-10 | physical person passport data | passport data accepted as structured value | PhysicalPersonApplicantData | Core target / may expand implementation |
| Inn | SC-10 | ИП/юрлицо ИНН | valid INN-like value | EntrepreneurApplicantData, LegalEntityApplicantData | Core |
| Ogrnip | SC-10 | ИП ОГРНИП | valid OGRNIP-like value | EntrepreneurApplicantData | Core |
| Ogrn | SC-10 | юрлицо ОГРН | valid OGRN-like value | LegalEntityApplicantData | Core |
| OrganizationName | SC-10 | legal entity name | non-empty organization display/legal name | LegalEntityApplicantData | Core |
| ApplicantType | SC-10, SC-04 | physical person / entrepreneur / legal entity | supported applicant type | ApplicantData, RequestApplicantData | Core |
| ActualAddress / ResidentialAddress | SC-10 | physical person future address | address-like value if promoted to current target | ApplicantData | Future / decision |
| LegalAddress | SC-10 | legal entity future address | address-like value | LegalEntityApplicantData | Future |
| RegistrationAddress | SC-10 | entrepreneur future address | address-like value | EntrepreneurApplicantData | Future |
| ObjectAddress | SC-04 | request object location | valid non-empty object address | Request / ConnectionRequest | Core |
| RequestSubject / RequestDetails | SC-04, SC-17 | request subject/details/description | required meaningful request description | Request, AnonymousRequest | Core |
| RequestedPowerKw | SC-04 | connection/agreement realism future data | positive/accepted requested power value | Request / ConnectionRequest | Future decision |
| RequestStatus | SC-04, SC-05, SC-06, SC-07A, SC-07B | InReview, Approved, Rejected | supported status; valid transitions only | Request | Core |
| ReviewDecisionType | SC-07B | Approved / Rejected decision | supported review decision | ReviewDecision / Request | Core |
| RejectionFeedback / RejectionExplanation | SC-07B, SC-05 | rejection explanation/details visible to client | required if rejection rule says so; meaningful text | ReviewDecision / Request | Core decision |
| ApprovalMessage | SC-07B, SC-05 | approval message optional | optional/required decision | ReviewDecision / Request | Future / decision |
| DocumentFileRef / AttachmentRef | SC-11, SC-13B, SC-13D | request document, agreement proposal document | accepted file/document reference; not empty when required | RequestDocument, AgreementProposal | Core for proposals; request docs extension |
| DocumentType | SC-11 | future document type | supported document type | RequestDocument | Future |
| AgreementProposalStatus | SC-13A, SC-13B, SC-13C, SC-13D | AwaitingClientConfirmation, SentByClient, Accepted, Rejected | supported status; valid transitions only | AgreementProposalExchange / AgreementProposal | Core |
| AgreementProposalSender | SC-13A, SC-13B, SC-13C, SC-13D | employee/client sender | supported sender; drives action availability | AgreementProposal | Core |
| ProposalComment / TextDetails | SC-13B, SC-13D | text details/comment sent with proposal | accepted text, required/optional decision | AgreementProposal | Core |
| VerificationStatus | SC-14 | passed/failed/unavailable | supported normalized result state | VerificationResult | Future |
| VerificationIssue | SC-14 | issue/reason if failed | accepted failure detail | VerificationResult | Future |
| FollowUpContact | SC-17 | anonymous request follow-up channel | at least one reachable contact channel if required | AnonymousRequest / ContactRequest | Future |
| ClientAccountId / OwnerRef | SC-05, SC-13A, SC-15 | own requests/proposals | non-null owner identity; query scoping | Request, AgreementProposal | Core |
| EmployeeRef / ReviewerRef | SC-07B, SC-13C, SC-13D | employee action identity | authorized employee reference | ReviewDecision, AgreementProposal | Core / future detail |

## 5. State / Invariant Catalog

### Request / ConnectionRequest

| State / data | Invariant | Candidate owner | Candidate method |
|---|---|---|---|
| Created request | Accepted request creation creates Request with status InReview. | Request | `Create(...) -> Result<Request>` |
| ObjectAddress | Request cannot be created without valid ObjectAddress. | Request / ObjectAddress | `ObjectAddress.Create(...)`; `Request.Create(...)` |
| RequestApplicantData | Request uses request-local applicant data, copied or entered inline. | Request | `Create(..., RequestApplicantData applicantData, ...)` |
| Saved ApplicantData vs request-local data | Editing request-local applicant fields does not delete saved ApplicantData. | Request + ApplicantData boundary | `RequestApplicantData.FromApplicantData(...)`; no mutation of ApplicantData |
| InReview | Only InReview request can enter review. | Request | `CanStartReview()` / `StartReview()` |
| Approve | Request can be approved only from InReview. | Request | `Approve(...)` |
| Reject | Request can be rejected only from InReview. | Request | `Reject(...)` |
| Approved/Rejected | Processed requests cannot be reviewed again in core. | Request | guard in `Approve/Reject/StartReview` |
| Rejection feedback | Rejected request has client-visible explanation if rule requires it. | Request / ReviewDecision | `Reject(RejectionFeedback feedback)` |
| Approved | Approved request enables employee-started agreement proposal exchange but does not create proposal automatically. | Request / AgreementProposalExchange | `CanStartAgreementExchange()` |

### ApplicantData

| State / data | Invariant | Candidate owner | Candidate method |
|---|---|---|---|
| ApplicantType | ApplicantData must have supported applicant type. | ApplicantData | `CreatePhysical`, `CreateEntrepreneur`, `CreateLegalEntity` |
| Physical person | Physical person applicant data requires accepted FullName, Snils, PassportData, PhoneNumber, EmailAddress. | ApplicantData / PhysicalPersonApplicantData | `PhysicalPersonApplicantData.Create(...)` |
| Entrepreneur | Entrepreneur data requires accepted FullName, Inn, Ogrnip, PhoneNumber, EmailAddress. | ApplicantData / EntrepreneurApplicantData | `EntrepreneurApplicantData.Create(...)` |
| Legal entity | Legal entity data requires accepted OrganizationName, Inn, Ogrn, PhoneNumber, EmailAddress. | ApplicantData / LegalEntityApplicantData | `LegalEntityApplicantData.Create(...)` |
| Invalid applicant data | Invalid ApplicantData is not saved. | ApplicantData | factory returns validation result |
| Verification policy | Standalone ApplicantData editing does not trigger verification. | ApplicantData / VerificationPolicy | no verification method on standalone ApplicantData |

### AgreementProposalExchange / AgreementProposal

| State / data | Invariant | Candidate owner | Candidate method |
|---|---|---|---|
| Approved request | Agreement exchange starts only from Approved request. | AgreementProposalExchange | `StartByEmployee(approvedRequestRef, ...)` |
| Employee starts exchange | Client cannot start initial exchange. | AgreementProposalExchange | `StartByEmployee`, no `StartByClient` |
| Employee proposal | Employee proposal requires attached document/file and text details/comment. | AgreementProposal | `CreateEmployeeProposal(...)` |
| AwaitingClientConfirmation | Employee-sent proposal waits for client action. | AgreementProposalStatus | set by `StartByEmployee` / employee new version |
| Client accept | Client can accept only employee-sent proposal awaiting confirmation. | AgreementProposalExchange | `ClientAccept(proposalId, clientId)` |
| Client own version | Client can send only one own version in response in core. | AgreementProposalExchange | `ClientSendOwnVersion(...)` |
| Client own version attachment | Client-sent proposal requires document/file and text details/comment. | AgreementProposal | `CreateClientProposal(...)` |
| SentByClient | Client-sent proposal waits for employee follow-up. | AgreementProposalStatus | set by `ClientSendOwnVersion` |
| Employee new version | Employee can respond to client-sent proposal by sending new version. | AgreementProposalExchange | `EmployeeSendNewVersion(...)` |
| Reject previous client proposal | New employee version turns previous client-sent proposal into Rejected. | AgreementProposalExchange | transition inside `EmployeeSendNewVersion` |
| Accepted | Accepted proposal is no longer awaiting response. | AgreementProposalStatus | `ClientAccept(...)` |

### RequestDocument

| State / data | Invariant | Candidate owner | Candidate method |
|---|---|---|---|
| Document attachment | Document cannot be attached without request context. | Request / RequestDocument | `AttachDocument(...)` |
| Invalid document | Rejected/invalid document is not attached. | RequestDocument | attachment factory/result |
| Attached document | Accepted document becomes visible request attachment. | RequestDocument / Request | `AttachDocument(...)` |

### VerificationResult

| State / data | Invariant | Candidate owner | Candidate method |
|---|---|---|---|
| Request context | Verification is available only in request context. | VerificationPolicy / Request | `Request.CanStartVerification()` |
| Standalone ApplicantData | ApplicantData save does not start verification. | ApplicantData / VerificationPolicy | no standalone start method |
| Result state | Verification result must be supported normalized status. | VerificationResult | `VerificationResult.Create(...)` |
| Review use | Review can use result only if related to same request. | Request / ReviewDecision | `ReviewWithVerification(...)` future |

### RecoveryContext

| State / data | Invariant | Candidate owner | Candidate method |
|---|---|---|---|
| Recovery email | Recovery email must create EmailAddress. | EmailAddress / RecoveryRequest | `RequestRecovery(email)` |
| Account existence | UI must not reveal whether account exists. | RecoveryPolicy | neutral result mapping |
| Recovery context | Password update requires valid recovery context. | RecoveryContext | `ValidateRecoveryContext(...)` |
| New password | New password must satisfy password policy. | PasswordPolicy | `Password.Create(...)` |
| Update password | Invalid/expired recovery context cannot update password. | Account / RecoveryContext | `ResetPassword(...)` |

### AnonymousRequest / ContactRequest

| State / data | Invariant | Candidate owner | Candidate method |
|---|---|---|---|
| Follow-up contact | Anonymous submission requires valid reachable follow-up contact. | FollowUpContact | `FollowUpContact.Create(...)` |
| Accepted anonymous submission | Invalid contact/request data is not recorded. | AnonymousRequest / ContactRequest | `Create(...)` |
| Object type unresolved | Full request/contact request/draft must be decided. | Unknown | ADR/domain decision |

## 6. Candidate Domain Methods Summary

### Account / Auth-adjacent

```text
ClientAccount.Register(email, password, confirmation)
AccountCredentials.Authenticate(email, password)
RecoveryRequest.Request(email)
RecoveryContext.Verify(token)
Account.ResetPassword(recoveryContext, newPassword)
```

### ApplicantData

```text
ApplicantData.CreatePhysical(fullName, snils, passportData, phone, email)
ApplicantData.CreateEntrepreneur(fullName, inn, ogrnip, phone, email)
ApplicantData.CreateLegalEntity(organizationName, inn, ogrn, phone, email)
RequestApplicantData.FromSavedApplicantData(applicantData)
RequestApplicantData.CreateInline(applicantType, fields)
```

### Request / ConnectionRequest

```text
Request.Create(clientId, requestDetails, objectAddress, requestApplicantData)
Request.CanStartReview()
Request.Approve(employeeRef, approvalMessage?)
Request.Reject(employeeRef, rejectionFeedback)
Request.CanStartAgreementExchange()
Request.AttachDocument(documentAttachment)
```

### AgreementProposalExchange

```text
AgreementProposalExchange.StartByEmployee(approvedRequestRef, employeeRef, document, textDetails)
AgreementProposalExchange.ClientAccept(clientId, proposalId)
AgreementProposalExchange.ClientSendOwnVersion(clientId, proposalId, document, textDetails)
AgreementProposalExchange.EmployeeSendNewVersion(employeeRef, previousClientProposalId, document, textDetails)
AgreementProposalExchange.CanClientRespond(proposalId, clientId)
AgreementProposalExchange.CanEmployeeSendNewVersion(proposalId, employeeRef)
```

### Verification

```text
VerificationPolicy.CanStartForRequest(request)
VerificationResult.Create(status, issue?)
Request.RecordVerificationResult(result)
```

### Anonymous Request

```text
FollowUpContact.Create(email?, phone?)
AnonymousRequest.Create(requestDetails, followUpContact)
```

## 7. Aggregate Boundary Pressure Map

| Candidate aggregate/root | Why it may be aggregate | Internal candidates | External references | Main invariants |
|---|---|---|---|---|
| Request / ConnectionRequest | Owns lifecycle InReview/Approved/Rejected and review decision. | RequestStatus, RequestApplicantData, ReviewDecision, RejectionFeedback, RequestDocument? | ClientAccountId, ApplicantData? | valid creation, valid review transitions, owner visibility, agreement eligibility. |
| ApplicantData / ApplicantProfile | Reusable client-provided applicant data with type-specific structure. | PhysicalPersonApplicantData, EntrepreneurApplicantData, LegalEntityApplicantData | ClientAccountId | type-specific validity, saved data unchanged by request-local edits. |
| AgreementProposalExchange | Owns multi-step proposal exchange and replacement rules. | AgreementProposal, status, sender, document metadata, comment | Approved Request ref, client id, employee id | employee starts, client responds once, previous client proposal rejected by employee replacement. |
| Account / ClientAccount | Owns identity and resource ownership. | Credentials, recovery context maybe | N/A / Identity framework | valid registration/auth, owner of resources. |
| AnonymousRequest / ContactRequest | Future flow without registered owner. | FollowUpContact, request details | none or future account | reachable contact required. |

Boundary risks:

```text
- Request vs AgreementProposalExchange should not become one large aggregate too early.
- ApplicantData reusable profile and request-local applicant data should not be the same mutable object.
- Document file storage should not be inside domain; domain should hold accepted metadata/reference.
- Auth credentials may remain framework-owned while ClientAccount id participates in domain ownership.
```

## 8. Open Domain Decisions / ADR Candidates

| Topic | Decision needed | Why it matters |
|---|---|---|
| Request applicant data | Reference saved ApplicantData or store snapshot/copy? | Determines Request aggregate data ownership and historical consistency. |
| ApplicantData aggregate | Separate aggregate/profile or value object owned by ClientAccount? | Affects reuse and update behavior. |
| ReviewDecision placement | Part of Request aggregate or separate entity? | Affects review transition consistency and audit. |
| AgreementProposalExchange root | Separate aggregate or part of Request? | Proposal lifecycle can grow independently after approval. |
| Proposal replacement status | Use Rejected now, introduce Superseded later? | Affects AgreementProposalStatus model. |
| Proposal accept meaning | Legal acceptance or lightweight confirmation? | Affects domain language and future signature flow. |
| Rejection feedback required | Must Rejected always include explanation? | Affects Request.Reject signature and validation. |
| Approval message | Is approval message required/optional/not core? | Affects ReviewDecision/Approved details. |
| RequestedPowerKw | Promote to core now or keep future? | Affects Request data model and value object. |
| Verification unavailable | Can review proceed when verification unavailable? | Affects ReviewDecision and VerificationPolicy. |
| Anonymous request object | Full request, contact request, or draft? | Affects future aggregate design. |

## 9. What To Do Next

Next artifact:

```text
planning/tables/domain-discovery-core.md
```

It should use this file to propose domain model options.

Recommended next sections:

```text
1. Current domain concepts confirmed by scenarios.
2. Value object set.
3. Entity candidates.
4. Aggregate root options.
5. Aggregate boundary variants.
6. Recommended first implementation cut.
7. Domain method candidates per aggregate.
8. ADR candidates.
```

Separate later artifact:

```text
planning/tables/ui-page-responsibility-map-core.md
```

The UI page map should not be mixed into domain discovery.
