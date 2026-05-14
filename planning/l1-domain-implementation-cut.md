# L1 Domain Implementation Cut

Status: current implementation-readiness guide  
Scope: first narrow L1 domain implementation step after `domain-draft-01.md`

## 1. Purpose

This file defines the safe first L1 implementation cut for an implementation agent.

Use it after reading:

```text
planning/tables/domain-drafts/domain-draft-01.md
planning/l1-domain-testing-rules.md
planning/current-state.md
planning/domain-model.md
```

This file exists because `domain-draft-01.md` is broader than the first implementation step.

Do not ask an implementation agent to implement the whole draft at once.

This file does not replace slice planning.

The first L1 cut is a domain-foundation cut, not a full scenario/application slice.

## 2. Source Of Truth

Target domain direction:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

Implementation/testing guides:

```text
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
```

Implementation/background context:

```text
planning/current-state.md
planning/domain-model.md
```

Slice planning after the first green domain foundation:

```text
planning/slices/l1-slice-drafting-guide.md
```

Current repo implementation may still contain older L1 names/statuses such as:

```text
RequestStatus.Submitted
```

Target request statuses for the next domain direction are:

```text
InReview
Approved
Rejected
```

`Submitted` is legacy/current implementation background unless explicitly reintroduced with separate meaning.

## 3. First L1 Domain Implementation Cut

Implement only domain classes and unit tests for this cut:

```text
Account / ClientAccount activation marker
Account.EnsureActivated

ApplicantParty
IndividualApplicantParty
ApplicantPartyType
ApplicantPartyVerificationStatus: Unverified / Verified
ApplicantParty current-active/version-like marker

ConnectionRequest
RequestStatus: InReview / Approved / Rejected
ReviewDecisionRecord
Approve / Reject
optional RejectionFeedback
```

Unit tests should cover:

```text
- local domain invariants;
- no-write behavior on failed commands;
- state transitions;
- optional rejection feedback;
- applicant verification rules;
- account activation guard behavior.
```

Detailed testing rules are in:

```text
planning/l1-domain-testing-rules.md
```

## 4. Explicitly Out Of Scope For First Cut

Do not implement unless explicitly requested:

```text
persistence
EF mapping
Dapper read models
API/controllers
UI
Playwright/E2E
AgreementProposalExchange
AgreementProposal
AgreementProposalVersion
file/blob storage
request document upload
verification provider
anonymous request
future applicant types
email confirmation flow
password recovery flow
notification sending
request public number generation
agreement proposal public number generation
```

## 5. Account Activation Placement

Current direction:

```text
Current core registration creates Active account.
Protected use cases require active account.
```

Implementation rule:

```text
Account.EnsureActivated belongs to Account/ClientAccount.
Application service or future auth policy calls EnsureActivated before protected use cases.
Business aggregates should not duplicate activation checks internally.
```

Good application service shape:

```csharp
var account = await accounts.GetById(currentAccountId);

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

Avoid inside `IndividualApplicantParty.Create(...)`:

```csharp
clientAccount.EnsureActivated();
```

## 6. ApplicantParty Implementation Decisions

Current decisions:

```text
- ApplicantParty inheritance is accepted.
- Current core type is IndividualApplicantParty.
- Future types are EntrepreneurApplicantParty and LegalEntityApplicantParty.
- ApplicantPartyVerificationStatus is only Unverified / Verified.
- New client-entered ApplicantParty starts Unverified.
- Request approval verifies ApplicantParty through application service.
```

Applicant editing/version-like policy:

```text
Changing applicant data creates a new version-like ApplicantParty record.
Preserve verified records.
Preserve request-referenced records.
Preserve current active record.
Irrelevant inactive unverified records may be removed or archived later.
```

For the first L1 cut, implement only what is needed to express the current active marker and local invariants.

Do not implement cleanup/archive yet unless explicitly requested.

## 7. ConnectionRequest Implementation Decisions

Current target statuses:

```text
InReview
Approved
Rejected
```

Creation:

```text
ConnectionRequest.Create(...) creates InReview request.
```

Approval:

```text
Only InReview request can be approved.
Approve records ReviewDecisionRecord.
Approve does not create AgreementProposalExchange.
Approve does not mutate ApplicantParty internally.
Application service coordinates request approval + applicant verification.
```

Rejection:

```text
Only InReview request can be rejected.
RejectionFeedback is optional in domain.
UI should warn when rejecting without feedback.
```

## 8. Unit Test Scope

Read first:

```text
planning/l1-domain-testing-rules.md
```

Technical baseline:

```text
Tests.EnergyManagement
xUnit
FluentAssertions
```

Existing tests under `Tests.EnergyManagement/` are the valid local style examples.

If exact existing test class/method names are needed, inspect the local checkout before implementation.

Recommended test groups:

```text
ClientAccountTests
  Register_CreatesActiveAccount
  Register_RejectsMissingEmail
  EnsureActivated_FailsForNonActiveAccount
  EnsureActivated_SucceedsForActiveAccount

IndividualApplicantPartyTests
  Create_CreatesUnverifiedCurrentActiveApplicant
  Create_FailsForInvalidApplicantData
  MarkVerified_SucceedsWhenMinimumDataPresent
  MarkInactiveVersion_MarksCurrentFlagFalse

ConnectionRequestCreationTests
  Create_CreatesInReviewRequest
  Create_FailsWithoutDetails
  Create_FailsWithoutObjectAddress
  Create_GuardsTransientApplicantParty

ConnectionRequestReviewTests
  Approve_OnlyInReview
  Approve_RecordsReviewDecision
  Reject_OnlyInReview
  Reject_AllowsNullFeedback
  FailedApproveOrReject_DoesNotChangeState
```

These names are target/recommended tests, not claims that they already exist.

## 9. Unit vs Integration Testing Rule

Unit tests come first.

Integration tests start after the first L1 domain implementation is green.

Do not mix these into the first domain unit-test cut:

```text
database tests
API tests
WebApplicationFactory tests
EF mapping tests
Dapper query tests
Playwright/E2E tests
```

Integration tests later should verify:

```text
- application/API + persistence integration;
- transaction behavior;
- DB mapping and constraints;
- failed command does not partially persist state;
- external adapters through fakes/test doubles where needed.
```

## 10. Progressive File Splitting Strategy

Use progressive file splitting.

Do not implement the whole L1 domain in one giant file until the end.

Do not create excessive micro-files before behavior stabilizes.

Recommended approach:

```text
1. Implement one mini-cut at a time.
2. Keep related code close enough for agent readability.
3. Add unit tests for the mini-cut.
4. Get tests green.
5. Split/normalize files into production structure.
6. Run tests again.
```

Suggested mini-cuts:

```text
Mini-cut 1:
  Account / ClientAccount / AccountActivationState + tests

Mini-cut 2:
  ApplicantParty / IndividualApplicantParty / applicant value objects + tests

Mini-cut 3:
  ConnectionRequest / ReviewDecisionRecord / RequestStatus + tests

Mini-cut 4 later:
  AgreementProposalExchange, only after core request/applicant model is stable
```

Suggested production structure after green mini-cuts:

```text
Domain.EnergyManagement.L1/
  Accounts/
    ClientAccount.cs
    AccountActivationState.cs

  Applicants/
    ApplicantParty.cs
    IndividualApplicantParty.cs
    ApplicantPartyType.cs
    ApplicantPartyVerificationStatus.cs

  Requests/
    ConnectionRequest.cs
    RequestStatus.cs
    ReviewDecisionRecord.cs

Tests.EnergyManagement/
  Domain/
    Accounts/
      ClientAccountTests.cs

    Applicants/
      IndividualApplicantPartyTests.cs

    Requests/
      ConnectionRequestCreationTests.cs
      ConnectionRequestReviewTests.cs
```

A temporary grouped file per mini-cut is acceptable while stabilizing behavior:

```text
Accounts/AccountDomain.cs
Applicants/ApplicantDomain.cs
Requests/RequestDomain.cs
```

But split after the mini-cut is green.

## 11. After Green L1 Domain Foundation: Slice Drafting

After the first L1 domain classes compile and unit tests are green, do not jump directly to controllers, persistence or UI.

Next planning step:

```text
planning/slices/l1-slice-drafting-guide.md
```

The goal is to create L1 slice drafts before application/API/persistence/UI implementation.

Slice definition:

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

Slice drafts should:

```text
- start from scenarios;
- list scenario-derived behavior items;
- include relevant DATA facts;
- mark missing UI/read/UX candidate items;
- derive testable behavior slices;
- map each slice to domain/application/persistence/API/UI/auth/infra/tests;
- include per-slice coverage;
- include behavior item coverage;
- include test coverage;
- collect local and consolidated questions;
- collect ADR candidates.
```

## 12. AgreementProposalExchange Position

`AgreementProposalExchange` is in `domain-draft-01.md`, but it is not part of the first implementation cut.

Reason:

```text
Agreement exchange has additional decisions around proposal versions, public numbers, and final refusal semantics.
The first L1 implementation should stabilize account/applicant/request review behavior first.
```

## 13. Implementation Agent Prompt Shape

Use this shape when asking an implementation agent:

```text
Implement only the L1 domain classes and unit tests described in planning/l1-domain-implementation-cut.md and planning/l1-domain-testing-rules.md.

Do not implement persistence/API/UI.

Do not implement AgreementProposalExchange yet.

Keep current old implementation working in parallel unless explicitly told to replace it.

Use domain-draft-01.md as target domain direction, but follow the narrower L1 cut.

Add/adjust unit tests for local domain invariants and no-write behavior.

Use existing Tests.EnergyManagement as the local test style baseline.

Use progressive file splitting:
- stabilize each mini-cut;
- get tests green;
- split/normalize files;
- run tests again.

After green L1 domain foundation, stop before application/API/persistence/UI work and create L1 slice drafts using planning/slices/l1-slice-drafting-guide.md.
```

## 14. Done Criteria

The first L1 domain implementation cut is done when:

```text
- domain classes compile;
- unit tests cover local invariants and state transitions;
- unit tests cover no-write behavior for failed commands;
- no persistence/API/UI work is mixed into the same step;
- target RequestStatus uses InReview / Approved / Rejected for the new/refined L1 domain direction;
- rejection feedback is optional in domain;
- account activation guard is not duplicated inside ApplicantParty factory;
- existing old flow is not accidentally broken unless migration is explicitly requested;
- next step is slice drafting before application/API/persistence/UI implementation.
```
