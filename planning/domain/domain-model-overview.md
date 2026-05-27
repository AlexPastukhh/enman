# Domain Model Overview

Status: draft / first-pass aggregate-based domain overview  
Scope: current extracted aggregate/value-object map and known cross-aggregate coordination

## 1. Purpose

This file gives a high-level view of the current first-pass aggregate-based domain model.

It does not replace detailed aggregate/value-object drafts.

Use this file after reading:

```text
planning/domain/scenario-to-aggregate-map.md
```

## 2. Current Aggregate Map

| Aggregate | Root | Child entities / concrete types | File | Status |
|---|---|---|---|---|
| Account | Account | ClientAccount, Employee | `planning/domain/aggregates/account.md` | draft / first-pass extracted |
| ApplicantParty | ApplicantParty | IndividualApplicantParty | `planning/domain/aggregates/applicant-party.md` | draft / first-pass extracted |
| ConnectionRequest | ConnectionRequest | RequestReview | `planning/domain/aggregates/connection-request.md` | draft / extracted |
| AgreementProposalExchange | AgreementProposalExchange | AgreementProposal | `planning/domain/aggregates/agreement-proposal-exchange.md` | draft / extracted |

## 3. Value Object Map

| Value object | File | Used by | Status |
|---|---|---|---|
| AgreementProposalVersion | `planning/domain/value-objects/agreement-proposal-version.md` | AgreementProposalExchange | draft / extracted |
| AgreementProposalAuthor | `planning/domain/value-objects/agreement-proposal-author.md` | AgreementProposalExchange | draft / extracted |
| AgreementDocumentRef | `planning/domain/value-objects/agreement-document-ref.md` | AgreementProposalExchange | draft / extracted |
| ProposalComment | `planning/domain/value-objects/proposal-comment.md` | AgreementProposalExchange | draft / extracted |
| FinalRefusalReason | `planning/domain/value-objects/final-refusal-reason.md` | AgreementProposalExchange | draft / extracted |
| ObjectAddress | `planning/domain/value-objects/object-address.md` | ConnectionRequest | draft / extracted |
| RejectionFeedback | `planning/domain/value-objects/rejection-feedback.md` | ConnectionRequest / RequestReview | draft / extracted |
| ApplicantIdentity | `planning/domain/value-objects/applicant-identity.md` | ApplicantParty | draft / first-pass extracted |
| ApplicantContact | `planning/domain/value-objects/applicant-contact.md` | ApplicantParty | draft / first-pass extracted |

Existing implementation value objects without separate domain docs yet:

```text
Email
PasswordHash
FullName
PhoneNumber
```

## 4. Aggregate Relationships

| From | To | Relation | Allowed dependency | Notes |
|---|---|---|---|---|
| ApplicantParty | Account / ClientAccount | belongs to client account | scalar ClientAccountId | Applicant contact email is not account auth email. |
| ConnectionRequest | ApplicantParty | request is created from selected applicant party | scalar ApplicantPartyId | Request does not own ApplicantParty mutation. |
| ConnectionRequest | Account / ClientAccount | request belongs to client account | scalar ClientAccountId | Client ownership can also be resolved through ApplicantParty. |
| ConnectionRequest.RequestReview | Account / Employee | review actors | scalar employee ids | Employee actor is Account-derived Employee. |
| AgreementProposalExchange | ConnectionRequest | exchange starts for approved request | scalar RequestId | exchange does not directly mutate request. |
| AgreementProposalExchange | Account / ClientAccount | client participant | scalar ClientAccountId | proposal author can be client. |
| AgreementProposalExchange | Account / Employee | employee participant/final refusal actor | scalar employee ids | employee capability checks live on Employee. |

## 5. Cross-Aggregate Coordination

Application-coordinated rules:

```text
- first-of-type ApplicantParty current/default selection;
- switching ApplicantParty current/default marker;
- creating ConnectionRequest from selected/current ApplicantParty;
- marking ApplicantParty verified after request approval;
- starting AgreementProposalExchange only for Approved ConnectionRequest;
- marking approved request AgreementExchangeFailed after final refusal when applicable;
- resolving Employee actor from authenticated Account.Id.
```

Aggregate-owned rules:

```text
- ApplicantParty minimum identity/contact data and verification marker;
- ConnectionRequest review lifecycle and request status transitions;
- AgreementProposalExchange proposal version lifecycle and final refusal status;
- Account/Employee account role, activation and employee capability checks.
```

Intentionally not domain-owned here:

```text
- dashboard/list read models;
- DTO/input formatting;
- file/blob storage mechanics;
- auth token/session implementation;
- UI navigation state.
```

## 6. Scenario / Behavior Coverage Overview

| Scenario / behavior source | Aggregates involved | Notes |
|---|---|---|
| SC-01 Client registration | Account | ClientAccount first-pass extracted; auth details not fully audited. |
| SC-02 Login | Account | Session/auth behavior mostly application/infrastructure. |
| SC-03B Account owner verified / activation | Account | Activation lifecycle first-pass only. |
| SC-04 Request creation | ApplicantParty, ConnectionRequest, Account | request creation references selected/current applicant party. |
| SC-05 My requests/details | ConnectionRequest | read/API/client behavior, not aggregate methods. |
| SC-06/SC-07A Employee dashboard/details | ConnectionRequest, Account/Employee | read/API/client behavior, actor resolution. |
| SC-07B Employee review | ConnectionRequest, Account/Employee, ApplicantParty | approval/rejection lifecycle; applicant verification is application coordination. |
| SC-10/SC-10B Applicant data | ApplicantParty | applicant creation/default/edit behavior first-pass extracted. |
| SC-13D Agreement proposal exchange | AgreementProposalExchange, ConnectionRequest, Account/Employee | exchange starts after approved request; request mutation is application coordination. |

## 7. Current Decisions

```text
Employee is a concrete Account subtype.
Employee.Id == Account.Id for authenticated employee sessions.
ApplicantParty is separate from ConnectionRequest.
AgreementProposalExchange is separate from ConnectionRequest.
Cross-aggregate state changes are coordinated by application services unless one aggregate clearly owns the rule.
```

Detailed decision:

```text
planning/domain/decisions/account-employee-tph-decision.md
```

## 8. Open Questions

```text
- Should request store immutable applicant snapshot data in addition to ApplicantPartyId?
- What is the final edit/archive/versioning model for verified or request-referenced ApplicantParty?
- How should account activation/suspension/deactivation be represented as explicit domain commands later?
- Which common implementation value objects deserve separate domain docs later?
- How should source/version/cascade metadata be added after the source/version model stabilizes?
```

## 9. Read Order

```text
1. planning/domain/scenario-to-aggregate-map.md
2. planning/domain/domain-model-overview.md
3. planning/domain/aggregates/account.md
4. planning/domain/aggregates/applicant-party.md
5. planning/domain/aggregates/connection-request.md
6. planning/domain/aggregates/agreement-proposal-exchange.md
7. planning/domain/value-objects/
8. planning/domain/decisions/
```
