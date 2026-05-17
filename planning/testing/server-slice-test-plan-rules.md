# Server Slice Test Plan Rules

Status: current / DB-state oriented test plan separation for L1 backend slices  
Scope: backend/server slice drafts, API integration tests, command/state-transition slices, regression guards

## 1. Purpose

Server slice drafts must make test responsibilities explicit.

For backend commands that change persisted state, the primary proof is:

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

Mocks are not the primary proof for L1 server behavior that persists state.

## 2. Required Test Plan Buckets

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

## 3. API Boundary / Access Tests

These tests verify endpoint access and rejection behavior:

```text
- unauthenticated -> 401;
- authenticated but missing resource -> documented rejection;
- authenticated but not-owned resource -> documented rejection;
- invalid route/body/query input -> documented validation/problem response.
```

If setup already creates persisted data, assert rejected commands leave DB state unchanged.

## 4. Main DB State Transition Tests

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

## 5. Idempotency / No-op DB State Tests

Use when repeated command is allowed:

```text
- command on already-current/default ApplicantParty succeeds;
- marker remains true;
- no extra ApplicantParty is created;
- stable fields unchanged.
```

## 6. No-mutation Tests

Use these for data-safety rules:

```text
- existing requests are not relinked;
- request status/details/created-at remain unchanged;
- unrelated ApplicantParty rows are unchanged;
- rejected commands leave existing rows unchanged.
```

No-mutation tests can be separate or combined with main transition tests, but the draft must explicitly say which safety rule is protected.

## 7. Same-type / Type-scope Tests

If the slice is type-aware, test current implemented type support now and mark future type coverage explicitly.

Do not expand domain/API only to satisfy a test for a future type.

Example:

```text
Current pass:
  two Individual ApplicantParties prove same-type switching.

Future pass:
  when LegalEntity / IndividualEntrepreneur exists, verify switching Individual default does not affect other type default.
```

## 8. Regression Guards

Use regression guards when new behavior depends on a boundary owned by another slice.

For `SL-APPL-003`:

```text
second same-type create still does not switch default implicitly
```

This may be referenced from `SL-APPL-001` tests if already covered.

## 9. What Not To Test

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
