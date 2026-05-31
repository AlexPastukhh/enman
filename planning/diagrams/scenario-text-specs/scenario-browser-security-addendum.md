# Scenario Browser Security Addendum

Status: cross-cutting companion addendum to scenario text specifications  
Doc version: v0.1.0  
Scope: browser security requirements for cookie-authenticated client/API flows

## 1. Purpose

This addendum records browser-security requirements without editing every protected scenario file.

Use it together with:

```text
SC-01-guest-registration.md
SC-02-login.md
SC-15-security-text-specification.md
scenario-account-activation-security-addendum.md
scenario-server-domain-validation-addendum.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## 2. Source Type

This file is not a business scenario.

It is a cross-cutting security requirements source.

Behavior items derived from this file are:

```text
security-derived cross-cutting behavior items
```

not ordinary scenario-derived behavior items.

## 3. Core Decision

Cookie-authenticated browser unsafe API requests require antiforgery protection.

Unsafe browser requests include:

```text
POST
PUT
PATCH
DELETE
```

Safe/read requests do not need the request token by default.

## 4. Browser Client Token Requirement

The browser client must have access to an antiforgery request token before executing unsafe API commands.

Current direction:

```text
client fetches antiforgery token from a same-origin API endpoint;
client stores the request token in runtime client state;
client attaches the token to unsafe API requests through a shared request helper.
```

The client must not require each feature/business slice to manually attach antiforgery token headers.

## 5. Session Context Requirement

Antiforgery token validity is tied to the current security/session context.

The client must refetch or reset antiforgery token after session context changes, including:

```text
login
logout
session reset
```

Current direction:

```text
- anonymous browser session may fetch a token before register/login unsafe requests;
- after login, client refetches token for the authenticated session context;
- after logout, client clears/refetches token for the anonymous/new session context.
```

## 6. Failure Requirement

Antiforgery validation failure must be distinguishable from ordinary validation errors.

Current API direction:

```text
native ProblemDetails
+ shared errors extension
+ stable client-facing error code
```

Candidate error code:

```text
security.antiforgery.validation.failed
```

The API must not expose internal framework exception details to normal client behavior.

## 7. Failure Normalization Requirement

Current implementation direction:

```text
Use an always-run result filter to normalize antiforgery validation failure into project ProblemDetails.
```

Important constraint:

```text
The filter must detect antiforgery validation failure by marker/result type,
not by generic HTTP 400 status.
```

Reason:

```text
ordinary DTO validation and other bad-request failures must not be mislabeled as CSRF/antiforgery failure.
```

## 8. Client Recovery Requirement

When antiforgery validation fails, the client may refetch token.

However:

```text
The client must not blindly auto-replay unsafe commands after token refresh.
```

Reason:

```text
unsafe commands may have side effects, and automatic replay can hide a security/session problem from the user.
```

Current direction:

```text
show recoverable session/security message;
refetch token;
require explicit user retry for unsafe command.
```

## 9. Same-Origin / Token Bootstrap Requirement

The token endpoint is intended for the browser client using the frontend origin / same-origin proxy model.

Current assumption:

```text
no credentialed cross-site token bootstrap;
browser client uses relative /api requests through frontend dev proxy in local E2E;
production origin/CORS policy must keep token issuance same-origin or explicitly controlled.
```

## 10. Protected Scenarios Affected

This cross-cutting requirement affects browser unsafe commands across protected and auth-related scenarios, including:

```text
SC-01 Guest Registration
SC-02 Login
SC-04 Client Request Creation
SC-07B Employee Request Review
SC-10 Applicant Data
SC-11 Request Documents
SC-13B Agreement Proposal Response
SC-13D Agreement Proposal Create / Send Version
SC-14 Client Data Verification
```

Exact endpoint coverage is owned by the corresponding parent slice files and `.client.md` sidecars.

## 11. Scenario / Implementation Questions

| ID | Question | Current assumption / direction | Where to resolve |
|---|---|---|---|
| Q-BSEC-CSRF-001 | Exact token endpoint path? | `/api/antiforgery/token` | CC-CSRF-001 implementation |
| Q-BSEC-CSRF-002 | Exact token header name? | explicit shared constant, e.g. `X-CSRF-TOKEN` | CC-CSRF-001 / generated constants |
| Q-BSEC-CSRF-003 | Does register/login require antiforgery? | yes for browser unsafe API requests | auth/API slice planning |
| Q-BSEC-CSRF-004 | Validation mechanism? | ASP.NET antiforgery + global validation policy/filter | CC-CSRF-001 |
| Q-BSEC-CSRF-005 | Failure normalization? | always-run result filter checks antiforgery failure marker/result, not HTTP 400 | CC-CSRF-001 |
| Q-BSEC-CSRF-006 | Auto-retry unsafe command after refetch? | no blind replay | client helper / CC-CSRF-001 |
| Q-BSEC-CSRF-007 | Cross-tab/session mismatch? | recoverable failure + refetch + explicit user retry | future client hardening |
