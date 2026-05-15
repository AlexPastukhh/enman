# Shared Support — Antiforgery Token / Session Context

Status: support note  
Marker: `[SHARED SUPPORT][AUTH/FRAMEWORK][CROSS-SLICE]`

## Purpose

Document cross-slice Client/Server support for ASP.NET Core cookie authentication and antiforgery protection.

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

## Tests

```text
- client helper tests for token storage/attach/refetch behavior;
- server integration tests for token issue endpoint if implemented;
- per-slice client tests verify unsafe requests use token helper;
- E2E tests can cover login -> unsafe request and logout -> fresh token flow later.
```

## Diploma Note

ASP.NET Core cookie authentication protects session identity, while antiforgery protects unsafe browser requests from cross-site request forgery.
