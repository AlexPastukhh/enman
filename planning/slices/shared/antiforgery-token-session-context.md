# Shared Support — Antiforgery Token / Session Context

Status: support note  
Marker: `[SHARED SUPPORT][AUTH/FRAMEWORK][CROSS-SLICE]`

## Purpose

Document cross-slice Client/Server support for ASP.NET Core cookie authentication and antiforgery protection.

This is important for unsafe requests from the client when using cookie auth.

## Why This Is Not A Business Slice

Antiforgery token handling is not independent user-visible business behavior.

It supports many unsafe client requests.

## Client Responsibility

```text
- fetch antiforgery request token;
- store request token in client runtime state;
- attach request token to unsafe requests;
- refetch token when auth/session state changes;
- usually refetch after login/logout because the token pair is tied to the current security context;
- handle missing/expired token by refetching or showing recoverable error depending on request state.
```

## Server Responsibility

```text
- use ASP.NET Core IAntiforgery;
- create/write antiforgery cookie;
- provide request token to client through a safe endpoint or response flow;
- validate unsafe requests with current cookie/session context.
```

## Mental Model

```text
Before login/logout:
token pair belongs to previous security/session state.

After login/logout:
cookie/session context changed.

Therefore:
client should fetch a fresh antiforgery token pair for the new state before unsafe requests.
```

## Used By

```text
SL-REQ-UI-001 — create request submit
SL-APPL-UI-001 — save applicant data
SL-REVIEW-UI-001 — approve/reject actions
SL-AUTH-UI-001 — auth flows that make unsafe requests
```

## Tests

```text
- client helper tests for token storage/attach/refetch behavior;
- server integration tests for token issue endpoint if implemented;
- per-slice client tests verify unsafe requests use token helper;
- E2E tests can cover login -> unsafe request and logout -> fresh anonymous token flow later.
```

## Diploma Note

This support can be explained as part of the security architecture:

```text
ASP.NET Core cookie authentication protects session identity,
while antiforgery protects unsafe browser requests from cross-site request forgery.
The client must keep the request token aligned with the current auth/session context.
```
