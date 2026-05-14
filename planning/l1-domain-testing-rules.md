# L1 Domain Testing Rules

Status: current testing rules  
Scope: first L1 domain implementation cut and later integration-test planning

## 1. Purpose

This file defines how to test the first L1 domain implementation cut.

Use it together with:

```text
planning/tables/domain-drafts/domain-draft-01.md
planning/l1-domain-implementation-cut.md
planning/current-state.md
planning/domain-model.md
```

This file is not a replacement for `l1-domain-implementation-cut.md`.

It clarifies how to write tests for that cut.

## 2. Existing Test Project Baseline

The repository already contains a test project:

```text
Tests.EnergyManagement
```

The project is already referenced from:

```text
EnergyManagement.sln
```

The current test stack is:

```text
xUnit
FluentAssertions
Moq
Microsoft.AspNetCore.Mvc.Testing
coverlet.collector
```

Use this existing test project and stack as the technical baseline.

Do not introduce another test framework unless explicitly decided.

## 3. Valid Local Examples

Valid local examples are the existing tests under:

```text
Tests.EnergyManagement/
```

Use them as the local style reference for:

```text
- xUnit attributes;
- FluentAssertions assertion style;
- test class layout;
- integration fixture style;
- WebApplicationFactory usage if present;
- naming conventions already used in the repository.
```

Important:

```text
Do not invent existing test class or method names.
If an implementation agent needs exact example names, it should inspect the local checkout first.
```

Recommended local command to list existing test files:

```powershell
Get-ChildItem .\Tests.EnergyManagement -Recurse -Filter *.cs | Select-Object FullName
```

Recommended local command to list classes and xUnit tests:

```powershell
Get-ChildItem .\Tests.EnergyManagement -Recurse -Filter *.cs |
  ForEach-Object {
    $_.FullName
    Select-String -Path $_.FullName -Pattern "\[Fact\]|\[Theory\]|class "
  }
```

If the local checkout contains existing unit/integration examples, cite their concrete class and method names in the implementation prompt before coding.

## 4. Testing Philosophy

Testing rules are inspired by Vladimir Khorikov's unit-testing principles and adapted to this project.

Good tests should:

```text
- protect against bugs;
- resist refactoring;
- provide fast feedback;
- remain easy to maintain.
```

Use tests to verify observable behavior and business rules, not incidental implementation details.

Do not chase a coverage percentage as the goal.

Coverage can help detect obviously missing tests, but high coverage does not prove test quality.

## 5. Unit Tests First

For the first L1 domain cut, write domain unit tests first.

The first implementation step is:

```text
domain classes + domain unit tests
```

Do not start with:

```text
integration tests
API tests
EF mapping tests
Dapper query tests
Playwright / E2E tests
```

Unit tests should be fast, deterministic, and independent of external infrastructure.

## 6. What To Unit Test In L1

Unit tests should verify:

```text
- local domain invariants;
- state transitions;
- failed command behavior;
- no-write behavior after failures;
- Result / error outcomes;
- optional RejectionFeedback behavior;
- ApplicantParty verification behavior;
- Account.EnsureActivated behavior;
- ConnectionRequest create / approve / reject behavior.
```

Current L1 target areas:

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

## 7. What Not To Unit Test In L1

Do not unit test infrastructure or cross-process behavior in the L1 domain cut:

```text
EF mappings
repositories
Dapper queries
controllers
HTTP pipeline
database constraints
email sender
file storage
verification provider
Playwright UI flow
```

Do not unit test private methods directly.

Do not assert internal call order between domain methods.

Do not mock domain entities or value objects.

## 8. Naming Rules

Use readable behavior-style names.

Good examples:

```csharp
[Fact]
public void Register_creates_active_client_account()

[Fact]
public void Approve_marks_in_review_request_as_approved()

[Fact]
public void Approve_fails_when_request_is_already_approved()

[Fact]
public void Failed_approve_does_not_change_request_state()

[Fact]
public void Reject_allows_null_feedback()
```

Avoid names tied to implementation details:

```csharp
[Fact]
public void Approve_sets_status_property_to_2()
```

Rule:

```text
Test name should describe observable behavior / business rule.
```

## 9. Test Structure

Use Arrange / Act / Assert structure.

Keep each test focused on one behavior.

Avoid:

```text
- multiple unrelated Act sections;
- conditionals inside tests;
- hidden setup that makes the test hard to read;
- large shared fixtures for simple domain tests;
- assertion-free tests.
```

Prefer explicit setup in the test or small helper/builder methods.

## 10. No-Write Behavior

No-write behavior is mandatory for failed commands.

Definition:

```text
If a command/method fails, aggregate state must remain unchanged.
```

Examples:

```text
Given request is Rejected
When Approve(...) is called
Then result is failure
And request remains Rejected
And original review decision is not replaced
```

```text
Given request is Approved
When Reject(...) is called
Then result is failure
And request remains Approved
And original review decision remains unchanged
```

```text
Given applicant cannot satisfy verification requirements
When MarkVerified(...) fails
Then applicant remains Unverified
```

No-write tests help detect missing CanDo/precheck logic and accidental partial mutation.

## 11. Result / Error Assertions

When a method returns `Result`, tests should verify:

```text
- success/failure outcome;
- relevant error when failure is meaningful;
- state after success;
- state after failure.
```

Do not overfit tests to exact error message strings unless the message itself is user-facing contract.

Prefer stable error identity if the codebase has typed/domain errors.

## 12. Mocking Rules

For L1 domain unit tests:

```text
mocks are usually not needed.
```

Prefer:

```text
- output assertions;
- state assertions;
- Result assertions.
```

Do not mock:

```text
domain entities
value objects
private methods
in-process domain collaborators
```

Moq exists in the test project, but should be reserved for later tests where there is a real boundary:

```text
email sender
file storage
verification provider
clock/time provider
external gateway
```

Interaction assertions are appropriate mainly at system boundaries, not inside the domain model.

## 13. Test Data Builders / Helpers

Small builders/helpers are allowed when they improve readability.

Rules:

```text
- keep defaults obvious;
- do not hide the business-relevant part of the test;
- do not put assertions in builders;
- do not create large shared object graphs for simple domain tests.
```

Good helper shape:

```csharp
private static ConnectionRequest CreateInReviewRequest()
```

Avoid helpers that hide the rule under test.

## 14. Recommended L1 Unit Test Groups

Recommended test groups from the L1 implementation cut:

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

These names are recommended target tests, not claims that they already exist.

Existing tests under `Tests.EnergyManagement/` remain the valid local style examples.

## 15. Integration Tests Later

Integration tests start only after the first domain implementation is green with unit tests.

Entry criteria:

```text
- L1 domain classes compile;
- L1 unit tests are green;
- mini-cut files are split/normalized;
- domain behavior is stable enough to wire through application/persistence/API.
```

Integration tests should verify system wiring and cross-boundary behavior, not repeat every domain unit test.

## 16. Integration Test Scope

Later integration tests should cover:

```text
- application/API + persistence integration;
- transaction behavior;
- DB mapping and constraints;
- request/applicant state persistence;
- failed command does not partially persist state;
- external adapters through fakes/test doubles where needed.
```

Examples for later phases:

```text
create applicant party persists expected state
create request persists InReview request
approve request changes request + applicant in one transaction
failed approve does not partially persist changes
reject request persists optional feedback behavior
API returns expected response for happy/failure paths
```

## 17. Integration Test Infrastructure Notes

`current-state.md` warns that integration tests may require SQL Server LocalDB/test database.

Do not make LocalDB/test DB a blocker for L1 domain unit tests.

Do not mix Playwright/E2E into the first domain cut.

Playwright/E2E can be revisited after backend/API/UI flows are stable.

## 18. Anti-Patterns To Avoid

Avoid:

```text
- testing private methods directly;
- asserting implementation details instead of observable behavior;
- mocking domain entities;
- interaction-heavy unit tests inside the domain model;
- assertion-free tests;
- tests with conditionals;
- tests that pass only because they mirror the implementation;
- large shared fixtures that hide the scenario;
- chasing code coverage percentage as a goal.
```

## 19. Done Criteria For L1 Testing

L1 domain testing is acceptable when:

```text
- target domain classes compile;
- unit tests cover local invariants;
- unit tests cover state transitions;
- unit tests cover no-write behavior for failed commands;
- unit tests cover optional rejection feedback behavior;
- unit tests cover applicant verification behavior;
- unit tests cover account activation guard behavior;
- tests use existing Tests.EnergyManagement stack;
- no persistence/API/UI tests are mixed into the first domain cut.
```
