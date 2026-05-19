# SL-AUTH-ACT-001.server — Register Pending Account And Send Activation Email

Status: draft / ready for implementation planning  
Logical slice: `CC-AUTH-ACT-001`  
Package: server/auth/account  
Slice type: server/backend/API slice  
Primary purpose: change registration so a new Client account requires email activation before using protected functionality.

Depends on:

```text
planning/slices/cross-cutting/CC-AUTH-ACT-001-email-activation-flow.md
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/slice-test-plan-workflow.md
```

## 0. Scenario Sources

Business scenario:

```text
Guest registers account.
System sends activation email.
User must activate account before protected app usage.
```

Cross-cutting behavior:

```text
Protected functionality requires activated account.
Current core created Active directly; this slice changes that first-pass shortcut.
```

Data source:

```text
ClientAccount.Email is auth/login/recovery/activation email.
```

Behavior items:

```text
AUTH-ACT-REGISTER-001 — Valid registration creates PendingActivation account.
AUTH-ACT-REGISTER-002 — Invalid registration creates no account and no token.
AUTH-ACT-REGISTER-003 — Registration creates one activation token row.
AUTH-ACT-REGISTER-004 — Registration sends activation email with activation link.
AUTH-ACT-REGISTER-005 — Registration response does not authenticate user.
```

Concern umbrella:

```text
CC-AUTH-ACT-001
```

Stable source behavior item IDs are pending scenario/source registry.

## 1. Slice Overview

Registration stops creating an immediately usable Active account.

Target first-pass behavior:

```text
POST /api/auth/register
      ↓
validate request
      ↓
create ClientAccount in PendingActivation
      ↓
create activation token
      ↓
send activation email to ClientAccount.Email
      ↓
return registration accepted/success response
      ↓
client shows check-email page
```

## 2. Scope

In scope:

```text
- ClientAccount activation lifecycle minimal support.
- AccountActivationToken domain/persistence representation.
- Registration command creates PendingActivation account.
- Activation token generated with ASP.NET Data Protection.
- Token row persisted in DB.
- Activation email sent after account/token creation.
- Registration response remains non-authenticated.
```

Suggested minimal domain additions:

```text
AccountActivationState value object or enum:
  PendingActivation
  Active

ClientAccount.RegisterPendingActivation(...)

ClientAccount.CompleteActivation(...)

AccountActivationToken:
  AccountId
  TokenHash
  Purpose
  CreatedAt
  ExpiresAt
  ConsumedAt
```

## 3. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Activation endpoint execution | `SL-AUTH-ACT-002.server` |
| Login/current-user active guard | `SL-AUTH-ACT-003.server` |
| Client check-email page | `SL-AUTH-ACT-001.client` |
| Resend activation email | Future `SL-AUTH-ACT-004` |
| Background outbox/retry | Future notification reliability |
| Email template visual polish | Future notification/UI content |
| Employee activation | Future employee auth slice |

## 4. Scenario Flow

```text
Guest submits registration form
      ↓
System validates email/password
      ↓
Duplicate/invalid?
      ├─ yes → validation/problem response, no account/token/email
      └─ no
          ↓
System creates Client account as PendingActivation
          ↓
System creates activation token
          ↓
System saves account + token atomically
          ↓
System sends activation email
          ↓
System returns success response
```

## 5. Implementation Flow

Target flow:

```text
RegisterClientAccountHandler
  -> validate DTO
  -> ClientAccount.RegisterPendingActivation(email, passwordHash, createdAt)
  -> activation token service creates protected token + hash
  -> account repository add
  -> token repository add
  -> SaveChanges transaction
  -> registration email notification service sends activation link
  -> response indicates check-email
```

Transaction direction:

```text
Account + token persistence should be atomic.
Email sending can happen after commit in first pass.
If email sending fails, first pass may return 500 and leave pending account/token,
or return success with "try resend later" only if resend exists.
Because resend is out of scope, prefer fail visibly and log.
```

## 6. API Contract

Endpoint:

```text
POST /api/auth/register
```

Auth:

```text
Guest / anonymous.
```

CSRF:

```text
Unsafe command. Existing antiforgery rule applies if registration currently requires it.
```

Request:

```text
RegisterClientAccountDto
  email
  password
```

Success response:

```text
200 OK or 201/202 if current registration contract is intentionally changed.
Recommended minimal: preserve current success status/body if possible, but do not issue auth cookie.
Response should allow client to show check-email state.
```

Failure responses:

```text
400/422 validation problem
409/422 duplicate email if current contract already exposes it
500 email/token persistence errors
```

OpenAPI impact:

```text
Register response schema may need a checkEmail/activationRequired marker only if current response cannot support client redirect.
If response changes, regenerate Shared/openapi.json and client generated types.
```

## 7. Validation / FluentValidation

Shape validation:

```text
email required/format
password required/rules
```

Domain/application validation:

```text
duplicate account email
password hash creation
activation token creation failure
```

Error mapping:

```text
invalid input -> validation ProblemDetails
duplicate email -> existing registration duplicate behavior
email/token failure -> safe ProblemDetails without raw token
```

## 8. Domain Rules

```text
- Pending account is not active.
- Pending account cannot use protected client functionality.
- Pending account can be activated once.
- Activation state belongs to Account/ClientAccount, not ApplicantParty.
- Business aggregates should not duplicate activation logic.
```

## 9. Application / Handler Direction

New/updated services:

```text
IAccountActivationTokenService
  CreateToken(accountId, email, now)

IAccountActivationTokenRepository
  Add(token)
  GetUnconsumedByHash(hash)

IRegistrationEmailNotificationService
  SendActivationEmail(email, activationLink)
```

Data Protection direction:

```text
IDataProtector purpose: "EnMan.AccountActivation.v1"
Payload includes accountId/tokenId/purpose/expiresAt.
Store hash of emitted token string in DB.
```

## 10. Security / Protection

```text
- Token value must not be stored raw if practical.
- Token value must not be logged.
- Token expiry should be short but reasonable, e.g. 24 hours.
- Registration must not issue full auth cookie.
- Email is sent to ClientAccount.Email only.
```

## 11. Behavior Coverage

| Behavior item | Scenario/source meaning | Covered by this slice? | Notes |
|---|---|---:|---|
| AUTH-ACT-REGISTER-001 | Valid registration creates pending account | yes | replaces Active shortcut |
| AUTH-ACT-REGISTER-002 | Invalid registration creates no account/token | yes | no partial write |
| AUTH-ACT-REGISTER-003 | Activation token row exists | yes | token consumed by activate slice |
| AUTH-ACT-REGISTER-004 | Activation email is sent | yes | minimal synchronous send |
| AUTH-ACT-REGISTER-005 | Registration does not authenticate user | yes | client goes check-email |

## 12. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| AUTH-ACT-REGISTER-001 | Account row state is PendingActivation after valid register | API integration + DB assertion | POST register, DB read | Low | Low | `Register_CreatesPendingActivationAccount` |
| AUTH-ACT-REGISTER-002 | Invalid register creates no account/token/email | API integration + DB/email assertion | invalid POST, DB count, fake email sender | Low | Low | `Register_InvalidInput_DoesNotCreateAccountTokenOrEmail` |
| AUTH-ACT-REGISTER-003 | Exactly one unconsumed token row is created | API integration + DB assertion | valid POST, token table read | Low | Medium | `Register_CreatesSingleActivationToken` |
| AUTH-ACT-REGISTER-004 | Activation email sent to account email and contains activation link | API integration with fake email sender | capture message | Medium: link may be malformed unless later activate test uses it | Medium | `Register_SendsActivationEmail` |
| AUTH-ACT-REGISTER-005 | Register does not issue full auth session | API integration | response cookies/current-user | Low | Low | `Register_DoesNotAuthenticatePendingAccount` |

Required negative tests:

```text
- duplicate email behavior remains safe;
- invalid email/password creates no token;
- email send failure behavior is explicit and safe.
```

## 13. OpenAPI / Generated Artifacts

If registration response DTO changes:

```text
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix .\energymanagement.client run generate:api-types
```

If response body is preserved and only semantics change, generated artifacts may be unchanged.

## 14. Implementation Checklist

```text
[ ] Add AccountActivationState / PendingActivation domain state.
[ ] Add ClientAccount.RegisterPendingActivation.
[ ] Add ClientAccount.CompleteActivation placeholder if needed by activate slice.
[ ] Add token entity/table mapping.
[ ] Add token service using IDataProtectionProvider.
[ ] Add token repository.
[ ] Update register handler to create pending account + token.
[ ] Update registration email to activation email.
[ ] Confirm no auth cookie is issued.
[ ] Add integration tests.
[ ] Regenerate OpenAPI/types only if contract changes.
```

This pass did not perform implementation verification.

## 15. Guardrail Summary

```text
- No full session after registration.
- ClientAccount.Email receives activation.
- ApplicantParty.Email is irrelevant here.
- Account + token persistence must be atomic.
- Token is one-use and expiring.
- Resend is not first-pass scope.
```
