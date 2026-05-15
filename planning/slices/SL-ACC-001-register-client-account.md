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

## 2. Sources / Source Behavior Items

Sources:

```text
planning/diagrams/scenario-text-specs/SC-01-guest-registration.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
```

Behavior items:

```text
ACC-CMD-REGISTER-001 — Register account
```

## 3. Visual Scenario Flow

```text
┌──────────────────┐
│      Guest       │
└────────┬─────────┘
         │ opens registration screen
         │ enters email + password + confirmation
         ▼
┌──────────────────────────────────────────────┐
│ Client UI                                    │
│ visible validation/correction feedback       │
└────────┬─────────────────────────────────────┘
         │ submits registration
         ▼
┌──────────────────────────────────────────────┐
│ System                                       │
│ checks whether registration data is accepted │
└────────┬─────────────────────────────────────┘
         │
   ┌─────┴─────┐
   │           │
accepted    rejected
   │           │
   ▼           ▼
┌──────────────────────────────┐   ┌─────────────────────────────┐
│ Create client account         │   │ No account is created        │
│ Current L1 core: Active       │   │ validation/business errors   │
└──────────────┬───────────────┘   └──────────────┬──────────────┘
               │                                  │
               ▼                                  ▼
┌──────────────────────────────┐       ┌──────────────────────────┐
│ Registration success visible  │       │ Guest corrects input     │
└──────────────────────────────┘       └──────────────────────────┘

Out of this backend slice:
- registration UI;
- automatic sign-in decision;
- email confirmation / PendingActivation;
- password recovery;
- account hardening.
```

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

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ POST /api/l1/auth/register                   │
│ Body: email + password + passwordConfirm     │
└──────────────┬───────────────────────────────┘
               │ map/validate request shape
               ▼
┌──────────────────────────────────────────────┐
│ Application Handler                          │
│ register client account command              │
└──────────────┬───────────────────────────────┘
               │ check duplicate email /
               │ registration command validity
               ▼
┌──────────────────────────────────────────────┐
│ Registration accepted?                       │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Security boundary    │   │ API error mapping             │
│ hash password        │   │ validation ProblemDetails     │
└──────────┬───────────┘   │ no account write              │
           │               └──────────────────────────────┘
           ▼
┌──────────────────────┐
│ Domain / Account     │
│ ClientAccount.Register│
│ Activation = Active  │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Persistence          │
│ store account row    │
└──────────┬───────────┘
           ▼
┌──────────────────────────────────────────────┐
│ API response                                 │
│ account identity or validation ProblemDetails│
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

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

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/auth/register` | POST | registration DTO | account identity or validation ProblemDetails | 200, 401/403 if protected later, 422, 500 | target L1 | yes |

Contract notes:

```text
- registration input includes email, password and password confirmation;
- raw password is never persisted as domain state;
- duplicate/invalid input returns validation ProblemDetails;
- generated OpenAPI types and constants must match the active contract.
```

## 8. Questions / Decisions

Open questions:

| Question | Why it matters | Current answer / candidate | Blocks implementation? |
|---|---|---|---|
| Should duplicate email be enforced by application precheck, DB unique constraint, or both? | Registration uniqueness | Prefer both eventually; current integration proves validation behavior | No |
| Should PendingActivation be introduced in L2? | Account lifecycle | Separate extension slice | No |
| Where should active-account guard be enforced? | Protected slices | Application service / auth framework, not aggregate duplication | Yes for protected slices |

Accepted decisions:

```text
Decision:
Current L1 registration creates active ClientAccount.

Reason:
L1 goal is to unblock protected client flows without implementing email confirmation.

Consequence:
Email confirmation / PendingActivation becomes an extension slice.
```

## 9. Behavior Coverage

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| ACC-CMD-REGISTER-001 — Register account | Registration command receives accepted input and creates account identity. | Scenario Slice Flow / Implementation Flow | covered |
| Invalid registration input creates no account | Rejected branch returns validation ProblemDetails and no write. | Visual Scenario Flow / Visual Implementation Flow | covered |
| Account created as active in current core | Domain/account step creates active account. | Scenario Slice Flow / Implementation Flow / Decisions | covered |
| Registration UI visible success/errors | UI is identified as dependent slice. | Visual Scenario Flow / UI Blueprint | separate UI slice |
| PendingActivation | Deferred to extension slice. | Slice Overview / Decisions | deferred |

## 10. Test / Verification Plan

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

## 11. Dependent / Follow-up Slices

```text
[UI][DEPENDENT] SL-AUTH-UI-001 — Login / registration / recovery UI
[EXTENSION][L2] SL-AUTH-EMAIL-001 — Email confirmation / PendingActivation flow
[EXTENSION][L2] SL-AUTH-RECOVERY-001 — Password recovery flow
[EXTENSION][L2] SL-AUTH-HARDENING-001 — Rate limiting / lockout / auth hardening
[AUTH/FRAMEWORK][CROSS-CUTTING] AUTH-GUARD-001 — Active account guard for protected actions
```

## 12. Implementation Checklist

```text
[x] Register client account through API
[x] Persist account identity
[x] Account starts active in L1 core
[x] Duplicate email returns validation problem
[ ] Decide DB unique constraint policy
[ ] Plan dependent auth UI slice
[ ] Plan L2 email confirmation slice
```
