# Domain Model

Status: background / implementation compatibility note  
Current target domain direction: `planning/tables/domain-drafts/domain-draft-01.md`  
Current L1 implementation cut: `planning/l1-domain-implementation-cut.md`

## 1. Purpose Of This File

This file preserves useful older L1 domain rules and implementation compatibility notes.

It is not the current target domain draft.

The current target domain model direction is:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

The current narrow implementation task boundary is:

```text
planning/l1-domain-implementation-cut.md
```

If this file conflicts with `domain-draft-01.md`, prefer the draft for target domain direction.

## 2. Main Architecture Idea

ФЛ / ИП / ЮЛ — это не разные аккаунты, а разные типы заявителя.

```text
Account         — кто входит в систему
ApplicantParty  — от чьего имени подается заявка
Employee        — кто обрабатывает заявку
ConnectionRequest — сама заявка
AgreementProposalExchange — future post-approval proposal exchange
```

## 3. Current Implemented Snapshot vs Target Direction

Previous implemented parallel L1 subset included:

```text
RequestStatus.Submitted
```

Target request lifecycle in `domain-draft-01.md` is:

```text
InReview
Approved
Rejected
```

`Submitted` is legacy implementation background and is not used by the current L1 request creation target unless explicitly reintroduced with separate meaning.

## 4. L1 Aggregate Boundaries

Current implemented aggregate roots:

```text
ClientAccount
IndividualApplicantParty
ConnectionRequest
```

Target/refined aggregate candidates from draft:

```text
ClientAccount
ApplicantParty / IndividualApplicantParty
ConnectionRequest
AgreementProposalExchange later
```

Agreement proposal exchange is not part of the first implementation cut unless explicitly requested.

## 5. Aggregate Root Creation Rules

- Aggregate roots are created through their own static `Create` methods or through application services/factories dedicated to that aggregate.
- One aggregate root must not create another aggregate root.
- Aggregate root constructors should stay private/protected where possible.
- Application services may coordinate multiple aggregates, load required state, check cross-aggregate rules, and then call the target aggregate `Create` method or domain method.

## 6. Child Entity Creation Rules

- Child entities are created only by their owning aggregate root.
- Child entities should not have independent repositories.
- Child entity `Create`/factory methods should be internal/private if they exist.
- External code should not create child entities and then attach them manually.

## 7. Inter-Aggregate References

- Store scalar `long` IDs for inter-aggregate references for now.
- Do not introduce typed IDs yet unless explicitly requested later.
- Examples:
  - `IndividualApplicantParty.ClientAccountId: long`;
  - `ConnectionRequest.ApplicantPartyId: long`.
- Domain behavior must not rely on traversing public navigation graphs across aggregate boundaries.
- Read/query convenience should not change write aggregate shape.

## 8. Inter-Aggregate Creation Context

Current implementation may pass an existing aggregate object as creation context when it improves type safety.

Target draft refinement:

```text
Account activation guard belongs in application/auth layer.
ApplicantParty factory should not duplicate activation checks internally.
```

Preferred current target shape for applicant creation:

```csharp
var account = await accounts.GetById(currentClientAccountId);

var activated = account.EnsureActivated();
if (activated.IsFailure)
    return activated;

var applicant = IndividualApplicantParty.Create(
    account.Id,
    fullName,
    applicantContactEmail,
    phoneNumber,
    clock.UtcNow);
```

## 9. Domain Error Policy

- Use `Result` for expected business/user validation failures.
- Use exceptions/guards for:
  - null required domain objects/value objects;
  - method contract violations;
  - impossible states;
  - transient aggregate passed where a persisted aggregate reference is required.
- Raw user input may be null/empty and should be handled as validation failure when it reaches a domain factory as raw primitive data.
- Required domain objects and value objects should not be null.
- A referenced aggregate with `Id <= 0` is not normal business validation. It means the application tried to use a transient aggregate where an existing persisted aggregate was required.

## 10. Optional Values Policy

- Use nullable `T?` for simple persisted optional state, DTO/API fields, EF nullable columns, private backing fields and UI/display-only optional values.
- Use `Maybe<T>` for operation results where absence is an expected outcome, especially repository lookups.
- Use `Result<T>` when failure needs an explicit reason.
- Do not pass `Maybe<T>` into aggregate factories for required dependencies.
- Resolve `Maybe<T>` in the application service before calling the domain method.

## 11. EF Relationship And Navigation Rule

- For one-to-many relationships between aggregate roots, model the relationship from the many side with an FK.
- Do not add a collection navigation on the one/principal side just to express one-to-many.
- Example:
  - `ApplicantParty` has `ClientAccountId`;
  - `ClientAccount` does not need an `ApplicantParties` collection as a domain navigation.
- Optional EF navigation properties between aggregate roots are allowed only as persistence/read convenience, not as domain ownership.
- Domain methods/factories should not require those navigation properties to be loaded.

## 12. Primitive Collections

- Do not model aggregate relationships as primitive collections like `List<long> ApplicantPartyIds` or `List<long> DocumentIds`.
- If the application needs "all applicant parties for account", use a repository/query by `ClientAccountId`.
- If an aggregate needs a collection inside it, it should usually be a child entity collection owned by that aggregate, not a primitive ID collection.

## 13. Cross-Aggregate Invariants

If a command needs data from another aggregate, the application service loads that aggregate and checks the rule before calling the target aggregate.

Example:

```text
load ApplicantParty by applicantPartyId
check ApplicantParty.ClientAccountId == currentClientAccountId
call ConnectionRequest.Create(applicantParty, details, address)
ConnectionRequest stores only applicantParty.Id
```

Do not push current-user ownership checks into `ConnectionRequest`.

## 14. Current First L1 Implementation Cut

Use:

```text
planning/l1-domain-implementation-cut.md
```

First implementation cut:

```text
Account / ClientAccount activation marker
Account.EnsureActivated
ApplicantParty / IndividualApplicantParty
ApplicantPartyVerificationStatus: Unverified / Verified
ConnectionRequest
RequestStatus: InReview / Approved / Rejected
ReviewDecisionRecord
Approve / Reject
optional RejectionFeedback
domain unit tests
```

Out of first cut:

```text
AgreementProposalExchange
VerificationResult
AnonymousSubmission
future applicant types
documents/file storage
persistence/API/UI/read models
```

## 15. Identity / Accounts

`ClientAccount` owns account/auth identity.

Current core registration creates Active account.

Protected use cases require active account.

Important rule:

```text
Account.EnsureActivated is called by application service/auth policy before protected use cases.
Business aggregates should not duplicate activation checks internally.
```

Password storage rule:

- `Account` stores `PasswordHash`, not a raw password.
- Raw password exists only at the DTO/command/application-service boundary.
- Hashing and password verification belong to password hasher/auth service, not to the account aggregate itself.

## 16. Applicant Parties

Target current direction:

```text
ApplicantParty is persisted applicant domain object.
ApplicantParty belongs to ClientAccount by ClientAccountId.
ApplicantParty uses inheritance for Individual / Entrepreneur / LegalEntity.
```

Current core:

```text
IndividualApplicantParty
ApplicantPartyVerificationStatus.Unverified
ApplicantPartyVerificationStatus.Verified
```

Editing/version-like policy:

```text
Changing applicant data creates a new version-like ApplicantParty record.
Verified records are preserved.
Request-referenced records are preserved.
Current active record is preserved.
Irrelevant inactive unverified records may be removed or archived.
```

## 17. Requests

Target current direction:

```text
ConnectionRequest stores ApplicantPartyId.
ConnectionRequest does not store ClientAccountId.
ConnectionRequest starts as InReview.
ConnectionRequest can be Approved or Rejected.
```

Current target statuses:

```text
InReview
Approved
Rejected
```

Legacy/current implementation status:

```text
Submitted
```

Request number policy:

- `Id` is the technical primary key/FK.
- Request number is a future public/business identifier, not the primary key.
- Request number is not part of the first implementation cut.
- Do not generate request numbers inside the domain entity with timestamps.

## 18. Review

Target current direction:

```text
ReviewDecisionRecord is child state of ConnectionRequest.
Approval records review decision.
Rejection records review decision with optional RejectionFeedback.
RejectionFeedback is optional in domain.
UI should warn if employee rejects without feedback.
```

## 19. Agreement Proposal Exchange

Agreement proposal exchange is in `domain-draft-01.md` but outside the first implementation cut.

Current draft decisions:

```text
AgreementProposalExchange has its own status.
AgreementProposalExchange stores ActiveProposalVersion, not ActiveProposalId.
AgreementProposal Id is DB technical identity.
AgreementProposalVersion is aggregate-generated domain version.
AgreementProposalNumber is optional/future public number.
SupersededByCounterProposal is used for replacement, not Rejected.
Final agreement refusal is not current core.
```

Final agreement refusal likely requires request-level post-approval outcome such as:

```text
agreement not concluded
failed agreement flow
closed without agreement
```

## 20. Documents / Notifications / Verification / Audit

These are outside the first implementation cut.

Future/deferred concepts:

```text
RequestDocument
GeneratedDocument
NotificationMessage
EmailNotification
OutboxMessage
VerificationResult
VerificationProvider
AuditLogEntry
```

## 21. EF Core Mapping Strategy

Historical mapping ideas remain background only.

Do not start persistence mapping from this file for the first L1 implementation cut.

First L1 cut should be:

```text
domain classes + domain unit tests
```

Persistence/API/UI should be planned after the domain cut is stable.
