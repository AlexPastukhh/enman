# Server Slice Test Plan Rules

Status: current / command DB-state and read integration test-plan separation synchronized  
Scope: backend/server slice drafts, API integration tests, command/state-transition slices, read slices, regression guards, no-unit-by-default rule

## 1. Purpose

Server slice drafts must make test responsibilities explicit.

The primary proof depends on slice type:

```text
State-changing command slice:
  API/integration boundary + DB state before/after assertions.

Read slice:
  API/read integration boundary + response shape/filter/visibility assertions.
```

Mocks are not the primary proof for L1/L2 server behavior.

Unit tests are not added by default.

## 2. State-Changing Command Slice Buckets

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

### API boundary / access tests

```text
- unauthenticated -> 401;
- authenticated but missing resource -> documented rejection;
- authenticated but not-owned resource -> documented rejection;
- invalid route/body/query input -> documented validation/problem response.
```

If setup already creates persisted data, assert rejected commands leave DB state unchanged.

### Main DB state transition tests

Reload rows from DB after the command and assert persisted state:

```text
- selected row changed as expected;
- previous row changed as expected;
- both rows still exist when deletion is out of scope;
- stable fields were not rewritten;
- ownership fields did not change;
- status/marker fields are correct.
```

### Idempotency / no-op tests

Use when repeated command is allowed:

```text
- repeated command succeeds or returns documented no-op;
- no extra rows are created;
- stable fields remain unchanged.
```

### No-mutation tests

Use these for data-safety rules:

```text
- existing requests are not relinked;
- unrelated rows are unchanged;
- rejected commands leave rows unchanged;
- future/lifecycle behavior outside the slice is not triggered.
```

## 3. Read Slice Test Plan Rule

For backend/API read slices, primary verification is:

```text
API/read integration tests.
```

Do not add unit tests by default.

Unit tests are allowed only if the slice introduces reusable helper logic with non-trivial branching, for example:

```text
- reviewState derivation helper;
- filter parsing helper;
- field-name mapping helper;
- reusable Dapper row mapper with meaningful conditional logic.
```

Even then:

```text
- keep unit tests focused on that helper only;
- do not unit-test every DTO property;
- do not add query-handler mock tests as primary proof;
- do not add validator unit tests by default.
```

Endpoint behavior, auth, validation, visibility, filtering and response shape are verified through integration tests.

## 4. Read Slice Buckets

Use these buckets for non-trivial server read slices:

```text
1. API boundary / access tests
2. Query validation tests, when query filters exist
3. Read correctness / response shape tests
4. Filtering tests, when filters exist
5. Visibility / privacy tests
6. Optional no-mutation smoke test, only if cheap
7. What not to test
```

### API boundary / access tests

```text
- unauthenticated -> 401;
- wrong role/account type -> documented rejection;
- authenticated allowed actor -> 200;
- missing/not-visible resource -> documented not-found/visibility response.
```

### Query validation tests

Keep these small.

For a filtered list endpoint:

```text
- one unknown status -> 422;
- one unknown reviewState -> 422;
- one valid combined filter -> 200.
```

Do not create many tiny tests for every DTO property if integration tests already cover the boundary.

### Read correctness tests

Prefer mixed dataset tests when they prove projection behavior compactly.

Example for employee request list:

```text
Arrange:
- request with no Review;
- request with Review started by current Employee;
- request with Review started by another Employee;
- approved reviewed request;
- rejected reviewed request.

Assert response contains:
- NotStarted;
- StartedByCurrentEmployee;
- StartedByAnotherEmployee;
- Approved;
- Rejected.
```

### Filtering tests

```text
- one status filter test narrows rows;
- one reviewState filter test narrows rows.
```

### Visibility / privacy tests

```text
- list/details returns only employee-visible request data;
- started-by-other state does not leak private employee/auth data;
- cross-account/private client data is not exposed beyond the DTO contract.
```

### Optional no-mutation smoke

Only if cheap with existing helpers:

```text
- GET does not create Review;
- GET does not change Request.Status;
- GET does not change Review.Status.
```

Do not make no-mutation smoke test expensive for every read slice.

## 5. Regression Guards

Use regression guards when new behavior depends on a boundary owned by another slice.

Example:

```text
SL-APPL-003 explicit make-default command may reference SL-APPL-001 regression:
second same-type create still does not switch default implicitly.
```

This may be referenced from owner slice tests if already covered.

## 6. What Not To Test

Do not use these as primary behavior proof:

```text
- repository mocks;
- handler call order;
- SaveChanges call count;
- generated OpenAPI artifacts;
- generated TypeScript types;
- React Query invalidation;
- client button rendering;
- validator unit tests by default;
- query-handler unit tests with mocks as primary proof;
- helper unit tests unless reusable helper has meaningful branching;
- future lifecycle behavior outside current slice.
```

Generated artifacts are checked through API generation/check workflow, not as behavior proof.

Client behavior is verified in client sidecars, not server slice tests.
