# Slice Test Plan Workflow

Status: current workflow for Test / Verification Plan sections inside slice drafts

## 1. Purpose

General testing principles stay in:

```text
planning/testing/
```

This file is the practical bridge for slice authors.

Every slice draft must include a Test / Verification Plan that proves behavior items and scenario outcomes.


## 1.1 Testing Layer Read Selector

This workflow owns the `Test / Verification Plan` shape inside slice drafts.

For testing-layer-specific guidance, read:

```text
planning/testing/testing-responsibility-map.md
```

Then select additional testing docs by need:

```text
Server/API slice behavior
  -> planning/testing/server-slice-test-plan-rules.md

General test-layer boundaries
  -> planning/testing/testing-principles.md

E2E / browser-real-API proof
  -> planning/testing/e2e-testing-workflow.md

Page Object / Component Object pattern
  -> planning/testing/test-object-patterns.md

Screenshot/evidence generation
  -> not ordinary slice proof; use only when evidence/screenshots are explicitly in scope.
```

Do not use `planning/testing/` as a replacement for this workflow. The testing layer provides supporting principles and layer-specific rules after the slice test trace is shaped here.

## 2. Primary Rule

```text
Tests verify behavior items and scenario outcomes.

Implementation details may appear only as:
  setup mechanism;
  action transport;
  observation mechanism;
  persistence/visible-state assertion mechanism.

Implementation details must not become the reason the test exists.
```

Required assertions belong inside the Behavior-to-Test Trace, not in loose lists after it.

## 3. Behavior Coverage vs Test Coverage

Behavior Coverage answers:

```text
which behavior items does this slice implement?
```

Test Coverage answers:

```text
how do we prove those behavior items and scenario outcomes?
```

Do not replace behavior coverage with test names.

Do not replace test planning with a vague "covered by integration tests" note.

## 4. Required Behavior-to-Test Trace

Every slice draft must include:

```markdown
### Behavior-to-Test Trace

| Behavior | Outcome proved | Test layer | Setup/action mechanism | Required assertions | Escape risk | Refactor risk | Planned / actual test |
|---|---|---|---|---|---|---|---|
```

For client UI slices use "Visible scenario outcome" wording when clearer.

For server slices use "Server/system outcome" wording when clearer.

Required assertions must be concrete enough that another chat or reviewer can check the test strength.

Bad:

```text
success assertions
no-mutation assertions
standard validation assertions
```

Good:

```text
HTTP 204; response body empty; proposal count +1; new version = previous +1; previous active proposal is SupersededByCounterProposal; status = AwaitingEmployeeResponse.
```

## 5. Assertion Profiles

Assertion profiles are allowed only as compression.

Example:

```text
COMMAND_204:
  HTTP 204;
  response body empty.

NO_MUTATION_EXCHANGE:
  proposal count unchanged;
  active version unchanged;
  exchange status unchanged;
  active proposal state unchanged;
  no new proposal row created.
```

Even when profiles are used, each trace row must remain understandable:

```text
Required assertions:
  COMMAND_204 + proposal count +1 + author = Client + status = AwaitingEmployeeResponse
```

Do not hide important proof behind a vague profile name.

## 6. Escape Risk Question

For each planned/actual test, answer:

```text
Can a bad vertical implementation pass this test and still produce undesirable scenario behavior?
```

Use:

```text
Low
Medium
High
```

Add a short reason.

Examples:

```text
High:
  Test only checks that handler returns success. Handler can return success without persisting state.

Low:
  Test calls public endpoint and asserts persisted state/read projection.
```

## 7. Refactor Risk Question

For each planned/actual test, answer:

```text
Can a behavior-preserving refactor break this test?
```

Use:

```text
Low
Medium
High
```

Add a short reason.

Examples:

```text
High:
  Test expects repository.GetById to be called once.

Low:
  Test asserts public endpoint result and persisted/visible behavior.
```

## 8. Allowed Implementation Details In Tests

Allowed when used only as mechanisms:

```text
DB fixture setup
auth/session fixture
HTTP endpoint call
API client call
rendered component/page
mock server response for client component test
DB read/assertion
read endpoint call after command
fake timers for deferred validation
test ids only when no semantic query exists
```

## 9. Forbidden Primary Test Goals

Avoid tests whose main purpose is:

```text
handler calls repository
SaveChanges called once
query uses Dapper
React Query key has exact internals
component calls hook with exact object
CSS class name equals exact string
private helper call order
```

These can be acceptable only when the slice explicitly owns that low-level contract.

## 10. Test Layer Selection Guide

### Domain tests

Use when proving pure domain lifecycle/invariant behavior without HTTP/persistence/UI.

Good for:

```text
aggregate state transitions
domain preconditions
no domain mutation on invalid transition
```

### API integration tests

Use when proving server/API behavior through public boundary.

Good for:

```text
401 / 403 / 404 / 422 / 204 / 200
auth/session/role behavior
ownership/visibility
state transition persisted
read projection after command
no-mutation after failed command
```

### Component/page tests

Use when proving visible client behavior for a given server response or command result.

Good for:

```text
button/action visibility
disabled/pending/error/success UI
form validation behavior
accessible labels and roles
page loading/empty/error states
```

### E2E tests

Use when proving a critical user flow through browser and real API.

Good for:

```text
happy path across client/server
role navigation
visible persisted outcome after reload/navigation
```

Do not use E2E for every matrix branch when API/component tests already cover matrix behavior.

### Contract/generated checks

Use for API shape drift prevention.

They support behavior proof but do not replace behavior tests.

## 11. Direct DB Setup Rule

```text
Direct DB setup is allowed only to arrange scenario preconditions.
Behavior proof must still go through public boundary.
Direct DB assertions are allowed to observe persisted outcome.
```

Good:

```text
Arrange:
  insert approved request / started review / existing exchange.

Act:
  call public HTTP endpoint.

Assert:
  DB and/or read endpoint shows expected outcome.
```

Bad primary proof:

```text
directly call handler/repository and assert internal call order.
```

## 12. No-Mutation Rule

For command behavior, ask:

```text
What must not change when this command fails?
```

Examples:

```text
failed command does not create exchange;
failed command does not create proposal;
failed command does not supersede active proposal;
failed command does not change exchange status;
failed command does not alter unrelated request/applicant/address;
failed review command does not overwrite existing review result.
```

A negative test that asserts only `422` may be insufficient for behavior proof.

The no-mutation expectation should appear in the Required assertions cell of the relevant trace row.

## 13. Validation Contract Rule

For validation behavior, at least representative tests should prove external contract:

```text
status = 422
field name
error code/message shape where stable
```

Avoid huge validation matrices unless the slice needs them.

Validation assertions should appear in the Required assertions cell of the relevant trace row.

## 14. CSRF Smoke vs Full Matrix Rule

```text
Each unsafe command family may include one CSRF smoke.

Full CSRF behavior belongs to CC-SEC-CSRF-001 tests.

Do not duplicate the full CSRF matrix in every feature test file.
```

## 15. What Not To Test Here

A slice draft may include a "What not to test here" section after the trace.

That section is a boundary note, not a replacement for the trace.

Use it for behavior owned by other slices/layers, for example:

```text
exchange details/list payload in a command slice;
initial exchange start when current slice only adds a proposal version;
client accept active proposal when current slice only sends a proposal;
binary file upload when current command accepts a document reference;
client UI when current draft is server-only;
full CSRF matrix when current slice only needs a smoke.
```

## 16. Server Command Example

```markdown
### Behavior-to-Test Trace

| Behavior | Outcome proved | Test layer | Setup/action mechanism | Required assertions | Escape risk | Refactor risk | Planned / actual test |
|---|---|---|---|---|---|---|---|
| `REQ-REVIEW-START-001` | Employee starts review for not-started request | API integration + DB assertion | HTTP POST, auth cookie, DB read, details read endpoint | HTTP 204; response body empty; Review exists/started; StartedByEmployeeId = current employee; details read shows started review | Low: catches missing persisted Review.Started state | Low: handler/repository refactor should not affect endpoint/state outcome | `StartRequestReview_StartsReviewAndReturnsNoContent` |
| `REQ-REVIEW-START-002` | Another Employee cannot silently take over started review | API integration + no-mutation DB assertion | DB fixture setup, HTTP POST, DB snapshot after failure | failure response; original StartedByEmployeeId unchanged; review status unchanged; no replacement review created | Low if original StartedByEmployeeId is asserted unchanged | Low/Medium: schema/helper refactor may require helper update | `StartRequestReview_WhenStartedByAnotherEmployee_ReturnsValidationProblemAndDoesNotChangeReview` |
```

## 17. Client UI Example

```markdown
### Behavior-to-Test Trace

| Behavior | Visible scenario outcome | Test layer | Setup/action mechanism | Required assertions | Escape risk | Refactor risk | Planned / actual test |
|---|---|---|---|---|---|---|---|
| `UI-REQ-DETAILS-START-001` | Employee sees Start Review action when review is not started | Component/page test | Mock API response, render route/page, visible button assertion | Start Review button is visible and accessible; action is enabled for not-started review | Medium: proves UI availability only, not server permission | Low: layout refactor should keep accessible button text | `EmployeeRequestDetailsPage_ShowsStartReviewActionForNotStartedReview` |
| `UI-REQ-DETAILS-START-002` | After successful Start Review, UI shows Started by you | Component/E2E | User click, mocked mutation success/refetch or real API in E2E | visible status changes to Started by you; pending state clears; error feedback absent; details/refetch shows started state | Medium/Low depending whether test uses real API | Medium if over-mocked; Low if visible outcome is asserted | `StartReviewButton_ShowsStartedByCurrentEmployeeAfterSuccess` |
```

## 18. Cross-Side Concern Example

```markdown
### Behavior-to-Test Trace

| Behavior | Scenario outcome being proved | Test layer | Setup/action mechanism | Required assertions | Escape risk | Refactor risk | Planned / actual test |
|---|---|---|---|---|---|---|---|
| `CC-SEC-CSRF-001-B01` | Unsafe command without token is rejected | Server integration | POST unsafe endpoint without token, assert rejection and no mutation | request rejected by CSRF/antiforgery boundary; no command mutation occurs; protected endpoint still accepts valid token in companion test if needed | Low for server-side rejection | Low: internal antiforgery wiring can change if behavior remains | `UnsafeCommand_WithoutCsrfToken_ReturnsBadRequestAndDoesNotMutate` |
| `CC-SEC-CSRF-001-B04` | Normal app command sends required protection | Client API/unit/component smoke | command wrapper/fetch boundary sends token/header | unsafe command request includes required CSRF token/header according to current client transport contract | Medium: client test alone does not prove server rejection | Medium: avoid asserting too much internal fetch shape | `unsafeCommand_includesCsrfToken` |
```

For paired concerns:

```text
Server proof alone does not prove client sends token.
Client proof alone does not prove server rejects missing token.
Paired concern needs paired proof.
```
