# SL-AUTH-ACT-002.server — Activate Client Account From Email Token

Status: draft / ready for implementation planning  
Logical slice: `CC-AUTH-ACT-001`  
Package: server/auth/account  
Slice type: server/backend/API slice  
Primary purpose: expose activation endpoint that consumes a one-use email token and activates the Client account.

Depends on:

```text
planning/slices/cross-cutting/CC-AUTH-ACT-001-email-activation-flow.md
planning/slices/server/SL-AUTH-ACT-001-register-pending-and-send-activation-email.server.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/slice-test-plan-workflow.md
```

## 0. Scenario Sources

Business scenario:

```text
User opens activation link from email after registration.
```

Cross-cutting behavior:

```text
Protected functionality requires activated account.
```

Data source:

```text
Activation token belongs to ClientAccount.Email registration flow.
```

Behavior items:

```text
AUTH-ACT-ACTIVATE-001 — Valid activation token activates pending account.
AUTH-ACT-ACTIVATE-002 — Activation token is one-use.
AUTH-ACT-ACTIVATE-003 — Expired/invalid token does not activate account.
AUTH-ACT-ACTIVATE-004 — Activation is idempotent only for already consumed same-account success page if explicitly chosen; first pass can return safe already-used problem.
AUTH-ACT-ACTIVATE-005 — Activation does not issue full session in server first pass unless client/server chooses auto-login later.
```

Concern umbrella:

```text
CC-AUTH-ACT-001
```

Stable source behavior item IDs are pending scenario/source registry.

## 1. Slice Overview

Target behavior:

```text
GET or POST /api/auth/activate?token=<token>
      ↓
unprotect token
      ↓
find matching unconsumed token row
      ↓
verify account is pending and token is not expired
      ↓
activate account and consume token in one transaction
      ↓
return success
```

Recommended first-pass endpoint:

```text
POST /api/auth/activate
body: { token: string }
```

Reason:

```text
API command remains explicit and easy for SPA activation page.
The email link can open `/activate-account?token=...`, and the client page posts token to API.
```

## 2. Scope

In scope:

```text
- Activate pending Client account from token.
- Consume token.
- Reject invalid/expired/already-used tokens.
- Keep no-session / explicit login-after-activation first pass.
- Provide safe ProblemDetails codes for client UI.
```

## 3. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Token creation/email send | `SL-AUTH-ACT-001.server` |
| Login active-account gate | `SL-AUTH-ACT-003.server` |
| Client activation page | `SL-AUTH-ACT-001.client` |
| Auto-login after activation | Future UX/security decision |
| Resend activation email | Future slice |
| Admin/support activation | Future admin slice |

## 4. Scenario Flow

```text
User clicks activation email link
      ↓
SPA opens activation page with token
      ↓
Client posts token to API
      ↓
Server validates token
      ↓
Token valid?
  ├─ no → safe activation error
  └─ yes
      ↓
Server activates account
      ↓
Server marks token consumed
      ↓
Client shows activation success and link to login
```

## 5. Implementation Flow

```text
ActivateClientAccountHandler
  -> unprotect token with IDataProtector
  -> validate payload purpose/accountId/expiresAt
  -> hash full token string
  -> load token row by hash
  -> load account
  -> ensure pending
  -> ensure token not consumed/expired
  -> account.CompleteActivation(now)
  -> token.MarkConsumed(now)
  -> SaveChanges transaction
  -> return 204 No Content or 200 result
```

Recommended success:

```text
204 No Content
```

Reason:

```text
Client only needs visible success state.
No response DTO required for first pass.
```

## 6. API Contract

Endpoint:

```text
POST /api/auth/activate
```

Auth:

```text
Anonymous.
```

CSRF:

```text
Decision needed:
- If using cookie-auth CSRF policy for unsafe requests, public activation POST may require antiforgery token fetched by SPA.
- Alternative: GET activation API with token only, but token appears in logs/history.
Recommended first pass: SPA page fetches antiforgery token and POSTs token.
```

Request:

```ts
type ActivateAccountDto = {
  token: string;
};
```

Success response:

```text
204 No Content
response body: none
no response DTO
```

Failure responses:

```text
400/422 token missing/invalid shape
400/422 token invalid/expired/already used
404 should be avoided if it leaks account existence; prefer safe activation problem
500 unexpected
```

ProblemDetails codes:

```text
auth.activation.token_missing
auth.activation.token_invalid
auth.activation.token_expired
auth.activation.token_already_used
auth.activation.account_not_pending
```

OpenAPI impact:

```text
New endpoint and DTO.
Regenerate OpenAPI and TypeScript types.
```

## 7. Validation / FluentValidation

Shape validation:

```text
token required
token max length if needed
```

Domain/application validation:

```text
unprotect failure
purpose mismatch
hash lookup mismatch
expired token
consumed token
account missing
account not pending
```

Error mapping:

```text
Do not expose account/email existence.
Return generic activation failure title with specific safe code only where acceptable.
```

## 8. Domain Rules

```text
- PendingActivation -> Active is allowed.
- Active -> Active through token is not a normal transition.
- Consumed token cannot be reused.
- Expired token cannot activate.
- Activation state changes only for token account owner.
```

## 9. Application / Handler Direction

Domain methods:

```text
ClientAccount.CompleteActivation(now)
AccountActivationToken.MarkConsumed(now)
```

Repository methods:

```text
GetUnconsumedByTokenHash(hash)
GetAccountById(accountId)
```

Transaction:

```text
Account activation and token consume must be saved atomically.
```

## 10. Security / Protection

```text
- Do not accept accountId from request body.
- Do not log raw token.
- Do not reveal whether token maps to real account.
- Do not activate if token payload accountId and stored row accountId differ.
- Use constant-time token hash comparison if manual comparison is needed.
```

## 11. Behavior Coverage

| Behavior item | Scenario/source meaning | Covered by this slice? | Notes |
|---|---|---:|---|
| AUTH-ACT-ACTIVATE-001 | Valid token activates pending account | yes | core |
| AUTH-ACT-ACTIVATE-002 | Token one-use | yes | consumed marker |
| AUTH-ACT-ACTIVATE-003 | Invalid/expired token rejected | yes | no mutation |
| AUTH-ACT-ACTIVATE-004 | Already-used behavior safe | yes | problem first pass |
| AUTH-ACT-ACTIVATE-005 | No auto-login first pass | yes | explicit login after |

## 12. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| AUTH-ACT-ACTIVATE-001 | Pending account becomes Active | API integration + DB assertion | seed pending+token, POST activate, DB read | Low | Low | `Activate_WithValidToken_ActivatesAccountAndConsumesToken` |
| AUTH-ACT-ACTIVATE-002 | Same token cannot be reused | API integration + DB assertion | POST twice, DB read | Low | Low | `Activate_WithConsumedToken_ReturnsProblemAndDoesNotMutate` |
| AUTH-ACT-ACTIVATE-003 | Expired token fails and account stays pending | API integration + DB assertion | seed expired token | Low | Low | `Activate_WithExpiredToken_ReturnsProblemAndKeepsPending` |
| AUTH-ACT-ACTIVATE-003 | Malformed token fails safely | API integration | POST bad token | Medium: no DB assertion if malformed never maps | Low | `Activate_WithMalformedToken_ReturnsSafeProblem` |
| AUTH-ACT-ACTIVATE-005 | Activation does not issue auth cookie | API integration | response cookies/current-user | Low | Low | `Activate_DoesNotAuthenticateAutomatically` |

## 13. OpenAPI / Generated Artifacts

Expected generated additions:

```text
POST /api/auth/activate
ActivateAccountDto
```

Commands:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix .\energymanagement.client run generate:api-types
```

## 14. Implementation Checklist

```text
[ ] Add ActivateAccountDto + validator.
[ ] Add endpoint POST /api/auth/activate.
[ ] Add activation handler/service.
[ ] Add token unprotect/hash lookup.
[ ] Add domain transition PendingActivation -> Active.
[ ] Consume token in same transaction.
[ ] Add integration tests for success/reuse/expired/malformed.
[ ] Regenerate OpenAPI/types.
```

This pass did not perform implementation verification.

## 15. Guardrail Summary

```text
- No accountId in request body.
- No raw token logging.
- One-use token.
- Expiring token.
- 204 No Content recommended.
- No auto-login first pass.
```
