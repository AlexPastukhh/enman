# Cross-Cutting Server Behavior Sources

Status: current source folder
Doc version: v0.1.0  

This folder owns common server behavior required by multiple server/API/domain slices.

Examples:

```text
ProblemDetails mapping
validation error response shape
common authorization visibility behavior
audit timestamp behavior
```

These files are not implementation drafts.

Server implementation drafts go to:

```text
planning/slices/server/cross-cutting/
```

If the behavior is server-only, the implementation draft should use `SINGLE-` prefix.
