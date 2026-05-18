# Client Cross-Cutting Slice Drafts

Status: client cross-cutting implementation draft folder

This folder owns client-side slice drafts for cross-cutting behavior.

These are still normal client slice drafts. They must use:

```text
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/slice-test-plan-workflow.md
```

## Source requirement

Client cross-cutting slice drafts should reference behavior sources from:

```text
planning/diagrams/scenario-cross-cutting/client-behavior/
```

or, for paired server-client/security concerns:

```text
planning/diagrams/scenario-cross-cutting/server-client/
planning/diagrams/scenario-cross-cutting/security/
```

## Naming

If no server counterpart is expected:

```text
SINGLE-CC-CLIENT-... .client.md
```

If a server counterpart is expected or possible:

```text
CC-... .client.md
```

matching the server draft ID.

## Testing

Every client cross-cutting slice draft must include Behavior-to-Test Trace.

For client-only behavior, tests usually prove visible/UI behavior or helper behavior.

For paired behavior, the client draft must state what is proven on the client side and what remains for server proof.
