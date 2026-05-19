# CC-AUTH-ACT-001 — Email Activation Flow

Status: draft / cross-cutting coordination  
Logical slice family: account email activation after registration  
Package: auth/account/security  
Slice type: cross-cutting umbrella  
Primary purpose: coordinate minimal server and client slices required to make account activation real instead of a future note.

Depends on:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/slice-test-plan-workflow.md
```

## 0. Scenario Sources

Business scenario:

```text
Guest Registration / Login / protected client functionality.
System/security pages include account activation required state.
```

Cross-cutting behavior:

```text
Account activation marker exists.
Protected functionality requires activated account.
Current core creates Active immediately; this slice family changes that.
```

Data source:

```text
ClientAccount.Email is auth/login/recovery/activation email.
ApplicantParty.Email is applicant contact email and is not used for account activation.
```

Behavior items:

```text
AUTH-ACT-BI-001 — registration creates pending activation account.
AUTH-ACT-BI-002 — activation email contains one-use activation link.
AUTH-ACT-BI-003 — activation link activates account.
AUTH-ACT-BI-004 — inactive account cannot receive full application session.
AUTH-ACT-BI-005 — protected commands/pages require active account.
AUTH-ACT-BI-006 — client shows check-email / activation-required states.
```

Concern umbrella:

```text
CC-AUTH-ACT-001
```

Stable source behavior item IDs are pending scenario/source registry.

## 1. Scope

This umbrella coordinates minimal activation implementation:

```text
Server:
- register creates PendingActivation instead of Active;
- activation token is generated and persisted;
- activation email is sent;
- activate endpoint consumes token and activates account;
- login rejects non-active account with activation-required ProblemDetails;
- protected commands continue to require active account.

Client:
- registration success moves user to check-email page/state;
- activation page handles activation link result;
- login shows activation-required error for inactive account;
- protected client nav/actions are hidden/blocked when account is not active.
```

Chosen minimal UX direction for first implementation:

```text
Reject login for non-active Client account with activation-required ProblemDetails.

Do not implement limited session in this pass.
```

Reason:

```text
Limited session requires additional session type, route guard behavior, shell state, and refresh rules.
Rejecting login is simpler, safer, and sufficient for "activate before use".
```

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Password recovery | Future auth slice |
| Resend activation email | Future `SL-AUTH-ACT-004` unless explicitly added |
| Activation for Employee Windows accounts | Future auth/security slice |
| Limited inactive session | Future UX/security slice if chosen later |
| Background email outbox/retry | Future notification reliability slice |
| Email template styling | Future notification/content polish |
| Admin manual activation | Future admin/support slice |
| Changing ApplicantParty.Email semantics | Not part of activation; ApplicantParty.Email remains applicant contact |

## 3. Related Slices / Owners

| Slice | Role |
|---|---|
| `SL-AUTH-ACT-001.server` | registration creates pending account and activation token/email |
| `SL-AUTH-ACT-002.server` | activation endpoint consumes token |
| `SL-AUTH-ACT-003.server` | login/current-user/protected active-account guard |
| `SL-AUTH-ACT-001.client` | check-email/activation UI and protected gate |
| `SL-ACC-001` | existing registration behavior changed by this slice family |
| `SL-AUTH-001` | login behavior changed for inactive accounts |
| `SL-AUTH-002` | current-user/session active-state semantics |
| `SL-APPL-*`, `SL-REQ-*`, `SL-AGR-*` | protected use cases must rely on active account guard |

## 4. Scenario Flow

```text
Guest registers
      ↓
System validates registration
      ↓
System creates PendingActivation account
      ↓
System creates one-use activation token
      ↓
System sends activation email to ClientAccount.Email
      ↓
Guest sees "check your email"
      ↓
Guest opens activation link
      ↓
System validates token
      ↓
System activates account
      ↓
Guest can sign in
      ↓
Protected client functionality is available
```

Inactive login branch:

```text
Guest tries to login before activation
      ↓
Credentials are valid, account is pending
      ↓
System does not issue session cookie
      ↓
Client shows activation-required message
```

## 5. Slice Breakdown

| Order | Slice | Why first-pass needed |
|---:|---|---|
| 1 | `SL-AUTH-ACT-001.server` | Registration must stop creating immediately usable accounts. |
| 2 | `SL-AUTH-ACT-002.server` | User needs a public activation link endpoint. |
| 3 | `SL-AUTH-ACT-003.server` | Auth/session must reject pending accounts and protected commands must stay safe. |
| 4 | `SL-AUTH-ACT-001.client` | UI must show check-email, activation results and gated protected app state. |

## 6. Cross-Cutting Decisions

| ID | Status | Decision | Impact |
|---|---|---|---|
| CC-AUTH-ACT-Q001 | accepted direction | Register creates PendingActivation for Client accounts. | Changes server registration and E2E assumptions. |
| CC-AUTH-ACT-Q002 | accepted direction | First pass rejects login for pending Client accounts. | Avoids limited session complexity. |
| CC-AUTH-ACT-Q003 | accepted direction | Activation token is persisted in DB and delivered via email link. | Requires token table/entity and repository. |
| CC-AUTH-ACT-Q004 | accepted direction | Token value is generated with ASP.NET Data Protection. | Avoids raw random token construction in app code. |
| CC-AUTH-ACT-Q005 | accepted direction | Persist token hash, not raw token, if practical. | Reduces DB token exposure risk. |
| CC-AUTH-ACT-Q006 | accepted direction | Token is one-use and expires. | Prevents replay. |
| CC-AUTH-ACT-Q007 | future review | Resend activation email is not required for first pass. | Can be added later without changing activation core. |
| CC-AUTH-ACT-Q008 | future review | AccountActivated authorization policy/claim may replace per-use DB guard later. | Not needed in minimal pass. |

## 7. Data / Persistence Direction

Minimal persistence model:

```text
AccountActivationTokens
  Id bigint identity / Guid
  AccountId bigint
  TokenHash nvarchar(128)
  Purpose nvarchar(100) = "account-email-activation"
  CreatedAt datetimeoffset
  ExpiresAt datetimeoffset
  ConsumedAt datetimeoffset null
```

Token delivery:

```text
email link:
  /activate-account?token=<url-encoded-protected-token>
```

Protected token payload:

```text
purpose
accountId
tokenId or nonce
createdAt
expiresAt
```

Validation:

```text
1. Unprotect token with Data Protection purpose.
2. Verify payload purpose.
3. Hash full token string and find matching unconsumed DB row.
4. Verify AccountId matches payload.
5. Verify ExpiresAt not passed.
6. Activate account and mark token consumed in same transaction.
```

## 8. Security / Privacy Guardrails

```text
- Do not disclose whether email exists on registration duplicate/login error beyond existing contract.
- Do not issue full auth cookie for pending accounts.
- Do not allow activation token reuse.
- Do not allow activation token to activate another account.
- Do not put raw token in logs.
- Do not use ApplicantParty.Email for account activation.
- Do not add client-controlled accountId to activation request body.
```

## 9. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item | System outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned / actual test |
|---|---|---|---|---|---|---|
| AUTH-ACT-BI-001 | Registration creates pending account and no full session is issued | API integration + DB assertion | POST register, DB read | Low if state and no-cookie behavior asserted | Low | planned |
| AUTH-ACT-BI-002 | Activation email contains usable link | API integration with fake email sender | Register, capture email body | Medium if only string asserted | Medium | planned |
| AUTH-ACT-BI-003 | Activation link activates account | API integration + DB/read assertion | GET/POST activate, DB read, login after | Low | Low | planned |
| AUTH-ACT-BI-004 | Pending account cannot login | API integration | Register pending, POST login | Low | Low | planned |
| AUTH-ACT-BI-005 | Protected commands reject pending account | API integration | Auth fixture/session or direct setup | Medium if login is impossible; use handler/fixture carefully | Medium | planned |
| AUTH-ACT-BI-006 | Client shows check-email / activation-required states | Component/page + E2E | mocked/real API responses | Medium | Medium | planned |

## 10. Implementation Handoff Order

```text
1. Implement server register pending + token/email.
2. Implement server activate endpoint.
3. Implement login/current-user active gate.
4. Regenerate OpenAPI/types/client constants.
5. Implement client check-email and activation pages.
6. Update Playwright: registration test expects check-email; business-flow E2E uses activated seeded user or test activation helper.
```

## 11. Guardrail Summary

```text
- ClientAccount.Email is activation email.
- ApplicantParty.Email is not activation email.
- First pass rejects inactive login.
- No limited session.
- No resend endpoint first pass.
- Token is one-use, expiring, server-generated, and not logged.
- Runtime implementation not changed by this draft archive.
```
