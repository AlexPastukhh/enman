# Cross-Cutting Slice Planning

Status: cross-cutting slice planning entry point

This folder owns slice concerns that are not purely client or server.

Examples:

```text
CSRF / antiforgery contract
OpenAPI/generated artifacts workflow
auth/session role model
testing workflow shared by client and server
slice folder/index conventions
```

Client-specific cross-cutting rules live in:

```text
planning/slices/client/
```

Server-specific cross-cutting rules live in:

```text
planning/slices/server/
```

New cross-cutting slice docs go directly under:

```text
planning/slices/cross-cutting/
```
