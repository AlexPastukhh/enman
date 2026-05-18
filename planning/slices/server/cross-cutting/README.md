# Server Cross-Cutting Slice Drafts

Status: server cross-cutting implementation draft folder

This folder owns server-side slice drafts for cross-cutting behavior.

These are still normal server slice drafts. They must use:

```text
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/slice-test-plan-workflow.md
```

## Source requirement

Server cross-cutting slice drafts should reference behavior sources from:

```text
planning/diagrams/scenario-cross-cutting/server-behavior/
```

or, for paired server-client/security concerns:

```text
planning/diagrams/scenario-cross-cutting/server-client/
planning/diagrams/scenario-cross-cutting/security/
```

## Naming

If no client counterpart is expected:

```text
SINGLE-CC-SERVER-... .server.md
```

If a client counterpart is expected or possible:

```text
CC-... .server.md
```

matching the client draft ID.

## Testing

Every server cross-cutting slice draft must include Behavior-to-Test Trace.

For paired behavior, the server draft must state what is proven on the server side and what remains for client proof.
