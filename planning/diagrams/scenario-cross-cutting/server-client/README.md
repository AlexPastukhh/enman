# Cross-Cutting Server-Client Behavior Sources

Status: current source folder
Doc version: v0.1.0  

This folder owns common behavior where both server and client participate.

Examples:

```text
current user/session contract
generated API contract refresh workflow
shared error response expectations consumed by client
```

These files are not implementation drafts.

Use paired implementation drafts when both sides need work:

```text
planning/slices/server/cross-cutting/<ID>.server.md
planning/slices/client/cross-cutting/<ID>.client.md
```
