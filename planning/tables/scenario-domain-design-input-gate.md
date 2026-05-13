# Scenario Domain Design Input Gate

Status: current workflow gate  
Scope: bridge from corrected scenario specs / DATA specs to domain discovery

## 1. Purpose

This gate defines the required artifact created after scenario text specs and scenario DATA files are ready:

```text
planning/tables/scenario-domain-design-input-core.md
```

This artifact is required before:

```text
planning/tables/domain-discovery-core.md
planning/tables/aggregate-boundary-candidates-core.md
planning/tables/domain-model-options-core.md
```

## 2. Why This Gate Exists

Scenario specs and DATA files are necessary, but they are not enough to design domain models and aggregates directly.

Scenario specs answer:

```text
what happens
what actor sees
what actor enters/selects/attaches
what observable outcome happens
```

DATA files answer:

```text
what data is entered / visible / selected / filtered / attached
```

Domain design still needs a focused extraction step:

```text
what data should become value objects
what persisted states exist
what state transitions are allowed
what invariants depend on those states
what methods should guard those transitions
what candidate aggregate owns those rules
```

That extraction step is:

```text
scenario-domain-design-input-core.md
```

## 3. Required Contents

`scenario-domain-design-input-core.md` must contain at least:

```text
1. Value Object Candidate Catalog
2. State / Invariant Catalog
3. Candidate Domain Methods Summary
4. Aggregate Boundary Pressure Map
5. Open Domain Decisions / ADR Candidates
```

## 4. Value Object Candidate Catalog

The value object catalog should be derived from scenario DATA files and validation addendum.

It should include:

```text
- candidate name;
- scenarios where it appears;
- evidence / DATA refs;
- validation responsibility;
- owner candidate;
- priority: Core / Future / Decision.
```

Examples:

```text
EmailAddress
PhoneNumber
Snils
PassportData
Inn
Ogrn
Ogrnip
ObjectAddress
RequestStatus
AgreementProposalStatus
AgreementProposalSender
ProposalComment / TextDetails
DocumentFileRef / AttachmentRef
FollowUpContact
```

## 5. State / Invariant Catalog

This is the most important part of the artifact.

It should collect state that will likely be persisted and methods/invariants depending on it.

Use this pattern:

```text
persisted state / data
-> invariant
-> candidate owner
-> candidate method
```

Examples:

```text
Request.status = InReview
-> only InReview request can enter review
-> Request
-> CanStartReview() / StartReview()

Request.status = InReview
-> approve transition is valid only from InReview
-> Request
-> Approve(...)

AgreementProposal.status = AwaitingClientConfirmation
-> client can respond only to employee-sent awaiting proposal
-> AgreementProposalExchange
-> ClientAccept(...) / ClientSendOwnVersion(...)

AgreementProposal.status = SentByClient
-> employee can send new version and previous client proposal becomes Rejected
-> AgreementProposalExchange
-> EmployeeSendNewVersion(...)
```

## 6. Candidate Domain Methods Summary

Candidate methods should be extracted from state-transition invariants and validation rules.

They are not final implementation signatures.

They are design pressure signals.

Examples:

```text
Request.Create(...)
Request.Approve(...)
Request.Reject(...)
Request.CanStartAgreementExchange()

ApplicantData.CreatePhysical(...)
ApplicantData.CreateEntrepreneur(...)
ApplicantData.CreateLegalEntity(...)

AgreementProposalExchange.StartByEmployee(...)
AgreementProposalExchange.ClientAccept(...)
AgreementProposalExchange.ClientSendOwnVersion(...)
AgreementProposalExchange.EmployeeSendNewVersion(...)
```

## 7. Aggregate Boundary Pressure Map

Aggregate boundary pressure should identify candidate owners of invariants.

Use this question:

```text
which object must be consistent when this state changes?
```

Examples:

```text
Request owns RequestStatus and review transition invariants.
ApplicantData owns type-specific applicant data validity.
AgreementProposalExchange owns proposal exchange status transitions.
Account / ClientAccount owns identity/resource ownership boundary.
AnonymousRequest / ContactRequest owns future anonymous follow-up requirement.
```

## 8. Validation Rule

The validation source of truth is:

```text
value objects + domain methods + state-transition methods
```

Client-side validation is UX feedback only.

Server-side validation should use domain/value-object results and convert them into validation responses.

Recommended flow:

```text
input payload
-> application use case
-> value object creation / domain method
-> Result/errors
-> server validation response or successful state change
```

## 9. Completion Criteria

This gate is complete when `scenario-domain-design-input-core.md` contains enough information to produce `domain-discovery-core.md`.

Minimum completion checklist:

```text
- value object candidates are listed;
- persisted state/status values are listed;
- state-transition invariants are listed;
- candidate domain methods are listed;
- aggregate owner candidates are listed;
- open domain decisions are listed;
- stale responsibility tables are not used as source of truth.
```

## 10. Not This Gate

This gate must not produce:

```text
- database schema;
- controllers/endpoints;
- repositories;
- handlers;
- React components;
- final aggregate implementation;
- ports/adapters;
- test plan.
```

Those come later.

## 11. Next Artifact

After this gate:

```text
planning/tables/domain-discovery-core.md
```

That file should use the design input to propose:

```text
- confirmed domain concepts;
- value object set;
- entity candidates;
- aggregate root options;
- aggregate boundary variants;
- recommended first implementation cut;
- domain method candidates per aggregate;
- ADR candidates.
```
