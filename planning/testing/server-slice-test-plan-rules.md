# Server Slice Test Plan Rules

Status: current / read-slice and state-changing command test-plan separation synchronized  
Scope: backend/server slice drafts, API integration tests, read/query slices, command/state-transition slices, regression guards

## 1. Purpose

Server slice drafts must make test responsibilities explicit.

The test plan should say what each test bucket proves and what should not be tested in this slice.

Do not turn implementation details into test goals.

## 2. Primary Proof By Slice Type

### Read/query slices

Primary proof:

```text
API/read integration tests
```

Read slices verify:

```text
- access/auth boundary;
- query/route validation where applicable;
- visibility rules;
- response shape;
- projection correctness;
- filtering correctness;
- no-mutation safety when important/cheap to verify.
```

Do not add unit tests by default for read slices.

Unit tests are allowed only when the slice introduces reusable helper logic with non-trivial branching, for example:

```text
- reviewState derivation helper;
- filter parsing helper;
- field-name mapping helper.
```

Even then, keep unit tests focused on that helper only.

Endpoint behavior, auth, validation, visibility, filtering and response shape are verified through integration tests.

### State-changing command slices

Primary proof:

```text
API/integration test
        ↓
state before command
        ↓
call endpoint/handler through public boundary
        ↓
state after command
        ↓
DB assertions
```

Mocks are not the primary proof for L1/L2 server behavior that persists state.

## 3. Read Slice Test Plan Buckets

Use these buckets for backend read/query slices:

```text
1. API boundary / access tests
2. Query/route validation tests, if applicable
3. Read/projection correctness tests
4. Filtering tests, if applicable
5. No-mutation safety tests, optional when cheap/important
6. What not to test
```

### API boundary / access

```text
- unauthenticated -> 401;
- wrong role/account type -> documented rejection;
- authenticated allowed actor -> 200;
- missing/not-visible resource -> documented not-found/visibility response.
```

### Query/route validation

Keep this small.

For list filters:

```text
- one unknown status -> 422;
- one unknown reviewState -> 422;
- one positive valid-filter case -> 200.
```

Do not create many tiny tests for every DTO property if the integration boundary already covers the relevant risk.

### Read/projection correctness

Prefer mixed dataset tests when possible.

For employee request list review state:

```text
Arrange:
- request with no Review -> NotStarted;
- request started by current Employee -> StartedByCurrentEmployee;
- request started by another Employee -> StartedByAnotherEmployee;
- approved reviewed request -> Approved;
- rejected reviewed request -> Rejected.
```

This proves projection logic without a large test matrix.

### Filtering

```text
- one status filter test;
- one reviewState filter test.
```

### No-mutation safety

Optional smoke only if cheap with existing helpers:

```text
- GET does not create Review;
- GET does not change Request.Status;
- GET does not change Review.Status.
```

### What not to test for read slices

```text
- no unit tests by default;
- no validator unit tests by default;
- no query-handler unit tests with mocks as primary proof;
- no helper unit tests unless a reusable helper has meaningful branching;
- no repository mock call-order as primary proof;
- no generated TypeScript as behavior proof;
- no command behavior;
- no client UI/cache behavior.
```

## 4. State-Changing Command Test Plan Buckets

Use these buckets when a server slice changes state:

```text
1. API boundary / access tests
2. Main DB state transition tests
3. Idempotency / no-op DB state tests, when relevant
4. DB no-mutation / unrelated-record safety tests
5. Same-type / type-scope tests, when relevant
6. Regression guards
7. What not to test
```

## 5. API Boundary / Access Tests For Commands

These tests verify endpoint access and rejection behavior:

```text
- unauthenticated -> 401;
- authenticated but missing resource -> documented rejection;
- authenticated but not-owned resource -> documented rejection;
- invalid route/body/query input -> documented validation/problem response.
```

If setup already creates persisted data, assert rejected commands leave DB state unchanged.

## 6. Main DB State Transition Tests

These tests prove the behavior that the slice owns.

For state-changing commands, reload rows from DB after the command and assert persisted state:

```text
- selected row changed as expected;
- previous row changed as expected;
- both rows still exist when deletion is out of scope;
- stable fields were not rewritten;
- ownership fields did not change;
- status/marker fields are correct.
```

For `SL-APPL-003`, this means:

```text
- selected ApplicantParty becomes IsCurrentActiveVersion == true;
- previous same-type ApplicantParty becomes IsCurrentActiveVersion == false;
- both ApplicantParties still exist;
- ClientAccountId / VerificationStatus / identity/contact fields are unchanged.
```

## 7. Idempotency / No-op DB State Tests

Use when repeated command is allowed:

```text
- command on already-current/default ApplicantParty succeeds;
- marker remains true;
- no extra ApplicantParty is created;
- stable fields unchanged.
```

## 8. No-mutation Tests

Use these for data-safety rules:

```text
- existing requests are not relinked;
- request status/details/created-at remain unchanged;
- unrelated ApplicantParty rows are unchanged;
- rejected commands leave existing rows unchanged.
```

No-mutation tests can be separate or combined with main transition tests, but the draft must explicitly say which safety rule is protected.

## 9. Same-type / Type-scope Tests

If the slice is type-aware, test current implemented type support now and mark future type coverage explicitly.

Do not expand domain/API only to satisfy a test for a future type.

Example:

```text
Current pass:
  two Individual ApplicantParties prove same-type switching.

Future pass:
  when LegalEntity / IndividualEntrepreneur exists, verify switching Individual default does not affect other type default.
```

## 10. Regression Guards

Use regression guards when new behavior depends on a boundary owned by another slice.

For `SL-APPL-003`:

```text
second same-type create still does not switch default implicitly
```

This may be referenced from `SL-APPL-001` tests if already covered.

## 11. What Not To Test For Commands

Do not use these as primary behavior proof:

```text
- repository mocks;
- handler call order;
- SaveChanges call count;
- generated OpenAPI artifacts;
- generated TypeScript types;
- React Query invalidation;
- client button rendering;
- future lifecycle behavior outside current slice.
```

These may appear in lower-level checks only when the project already has that pattern, but behavior proof must come from public boundary + DB state.
