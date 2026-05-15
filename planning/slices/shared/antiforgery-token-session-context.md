# Antiforgery Token / Session Context Note

Status: shared support note; superseded as primary implementation plan  
Scope: quick reference for antiforgery token/session context concerns

## 1. Primary Slice

Primary implementation-ready cross-cutting slice:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Security requirements source:

```text
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
```

Behavior items:

```text
planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
```

## 2. Purpose

This file remains a short shared support note.

Do not treat it as the source of truth for implementation flow.

## 3. Core Model

```text
Server:
- uses antiforgery services;
- issues request token/cookie/context for browser client;
- validates unsafe browser API requests;
- normalizes antiforgery failure into project ProblemDetails.

Client:
- fetches and stores request token;
- attaches token to unsafe requests;
- refetches/resets token after login/logout/session context change;
- does not blindly replay unsafe commands after token refresh.
```

## 4. Session Context

Token validity is tied to the current security/session context.

After login/logout/session reset, token should be refetched or reset.

## 5. Tests

Detailed test plan lives in:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Short summary:

```text
- server integration tests for missing/invalid/valid token;
- server integration test that ordinary DTO validation is not mislabeled as antiforgery failure;
- client tests for fetch/store/attach/refetch/no-blind-replay;
- later E2E smoke for login/session + one unsafe command.
```
