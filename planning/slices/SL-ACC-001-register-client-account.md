# SL-ACC-001 — Register Client Account

Status: implemented slice draft  
Package: `[L1]`  
Source scenario: `SC-01 Guest Registration`  
Slice type: backend / API / persistence slice with dependent auth/UI extension slices  
Current implementation status: implemented and integration-tested

## 1. Slice Overview

Observable behavior:

```text
Guest registers a client account.
Accepted registration creates persisted client account identity.
```

Why this is a real slice:

```text
- separate observable behavior;
- creates account identity required by later L1 slices;
- clear success/failure outcomes;
- independently testable through API/persistence integration tests;
- does not require ApplicantParty, Request, UI, password recovery or email confirmation.
```

Related extension / dependent slices:

```text
[EXTENSION][L2] SL-AUTH-EMAIL-001 — Email confirmation / PendingActivation flow
[EXTENSION][L2] SL-AUTH-RECOVERY-001 — Password recovery flow
[EXTENSION][L2] SL-AUTH-HARDENING-001 — Rate limiting / lockout / auth hardening
[UI][DEPENDENT] SL-AUTH-UI-001 — Login / registration / recovery UI
[AUTH/FRAMEWORK][CROSS-CUTTING] AUTH-GUARD-001 — Active account guard for protected actions
```

## 2. Questions Overview

| Question | Why it matters | Current answer / candidate | Blocks implementation? |
|---|---|---|---|
| Should duplicate email be enforced by application precheck, DB unique constraint, or both? | Registration uniqueness | Prefer both eventually; current integration proves validation behavior | No |
| Should PendingActivation be introduced in L2? | Account lifecycle | Separate extension slice | No |
| Where should active-account guard be enforced? | Protected slices | Application service / auth framework, not aggregate duplication | Yes for protected slices |

## 3. Flow Coverage Overview

| Flow part | Current coverage | Missing / separate slice |
|---|---|---|
| Registration input accepted | Implemented through API/integration path | UI separate |
| Account created as active | Implemented | PendingActivation extension |
| Duplicate email rejected | Integration-tested | DB constraint decision later |
| Registration UI | Not in this slice | SL-AUTH-UI-001 |
| Email confirmation | Not in this slice | SL-AUTH-EMAIL-001 |

## 4. Scenario Slice Flow

### F01 — Guest provides registration data

DATA: email, password, password confirmation.  
Items: `ACC-CMD-REGISTER-001`.

### F02 — System validates registration input

Rules/no-write: invalid input does not create account; duplicate email does not create second valid account.

### F03 — System creates client account

DATA: email, password hash, role/type = Client.  
Items: `ACC-CMD-REGISTER-001`, `ACC-LC-001`.  
Expected result: client account is created and active in current L1 core.

### F04 — System persists account

Expected result: persisted account identity exists and can be referenced by later slices.

### F05 — API returns registration result

DATA: AccountId, Email.

## 5. Implementation Flow

### I01 — UI blueprint

Current implementation: UI is not part of this implemented backend/API/persistence slice.

### I02 — API

`POST /api/l1/auth/register` receives registration DTO and returns account identity or validation problem.

### I03 — Application service

High-level responsibility: accept registration command, validate uniqueness / handle duplicate email, hash password, call `ClientAccount.Register(...)`, persist account, return account identity.

### I04 — Domain

`ClientAccount.Register(...)`, `Account.IsActive`, `Account.ActivationState`, `Account.EnsureActivated()`.

### I05 — Persistence

L1 account row is created with account type/role/email/password hash/is-active state.

### I06 — Response mapping

Response exposes AccountId and Email.

## 6. UI Blueprint

UI is handled by `[UI][DEPENDENT] SL-AUTH-UI-001`.

Blueprint:

```text
- user opens registration screen;
- enters email/password/password confirmation;
- submits;
- sees validation errors or registration success;
- after success user can continue to authenticated flow depending on auth UX.
```

## 7. Test Plan / Test Coverage

Current integration coverage:

```text
RegisterClientAccount_CreatesL1Account
RegisterClientAccount_WithDuplicateEmail_ReturnsValidationProblem
```

Expected/current domain unit coverage from L1 implementation report:

```text
Register_creates_active_account
Register_rejects_missing_email
EnsureActivated_succeeds_for_active_account
EnsureActivated_fails_for_non_active_account
```

Missing/later: UI tests, email confirmation tests, stronger DB uniqueness tests if DB constraint is introduced.

## 8. Detailed Implementation Notes

This section is intentionally implementation-focused and may include pseudocode or code snippets in later drafts.

## 9. Decisions

```text
Decision:
Current L1 registration creates active ClientAccount.

Reason:
L1 goal is to unblock protected client flows without implementing email confirmation.

Consequence:
Email confirmation / PendingActivation becomes an extension slice.
```

## 10. ADR Links / Candidates

ADR candidates should be promoted to `planning/adr/adr-candidates.md` when the decision affects multiple slices or diploma-level architecture explanation.

## 11. Implementation Checklist

```text
[x] Register client account through API
[x] Persist account identity
[x] Account starts active in L1 core
[x] Duplicate email returns validation problem
[ ] Decide DB unique constraint policy
[ ] Plan dependent auth UI slice
[ ] Plan L2 email confirmation slice
```
