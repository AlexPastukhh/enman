# CC-CSRF-001 — Antiforgery Behavior Items

Status: current security-derived cross-cutting behavior items  
Doc version: v0.1.0  
Source type: cross-cutting security requirement, not business scenario  
Primary slice: `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md`

## 1. Purpose

These behavior items are derived from browser security requirements and antiforgery support notes.

They are first-class behavior items even though they are not scenario-derived.

Decision:

```text
Precise cross-cutting security concerns may still get behavior items
to keep traceability from requirements to implementation/tests.
```

Trade-off:

```text
This adds documentation overhead,
but prevents important security behavior from living only in implementation notes.
```

## 2. Sources

```text
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
planning/diagrams/scenario-text-specs/SC-15-security-text-specification.md
planning/slices/shared/antiforgery-token-session-context.md
planning/api/api-error-contract.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## 3. Behavior Items

| ID | Source | Behavior | Covered by concern flow |
|---|---|---|---|
| CC-CSRF-SRV-001 | browser security addendum / shared note | Server provides a same-origin way for the browser client to obtain an antiforgery request token. | F01 |
| CC-CSRF-SRV-002 | browser security addendum / antiforgery validation | Server validates unsafe browser API requests before business action logic. | F03 |
| CC-CSRF-SRV-003 | browser security addendum | Safe/read requests do not require antiforgery request token by default. | F03 |
| CC-CSRF-SRV-004 | browser security addendum | Token endpoint must not become uncontrolled credentialed cross-site token bootstrap. | F01 |
| CC-CSRF-CL-001 | shared note | Client fetches and stores antiforgery request token. | F01 |
| CC-CSRF-CL-002 | shared note | Client attaches antiforgery token to unsafe API requests through shared request helper. | F02 |
| CC-CSRF-CL-003 | session-context note | Client refetches or resets token after login/logout/session context changes. | F05 |
| CC-CSRF-ERR-001 | API error contract / browser security addendum | Antiforgery failure is returned as distinguishable ProblemDetails with stable client-facing error code. | F04 |
| CC-CSRF-ERR-002 | client recovery requirement | Client treats antiforgery failure as recoverable security/session failure. | F06 |
| CC-CSRF-NW-001 | security decision | Client must not blindly auto-replay unsafe commands after token refresh. | F06 |
| CC-CSRF-TEST-001 | test requirement | Server integration tests cover unsafe request without token. | F07 |
| CC-CSRF-TEST-002 | test requirement | Server integration tests cover unsafe request with invalid token. | F07 |
| CC-CSRF-TEST-003 | test requirement | Server integration tests prove ordinary DTO validation is not mislabeled as antiforgery failure. | F07 |
| CC-CSRF-TEST-004 | test requirement | Client tests cover token fetch/store/attach/refetch helper behavior. | F07 |
| CC-CSRF-TEST-005 | test requirement | Later E2E covers login/session plus at least one unsafe command success path. | F07 |

## 4. Coverage Rule

Every item above must be reflected in the cross-cutting slice Concern Flow before Implementation Flow.

Implementation may be staged, but missing coverage must be visible in the slice coverage table.
