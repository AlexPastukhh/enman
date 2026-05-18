# Derived Decisions

Status: archive-local derived decisions / reflected in docs

## Decisions

```text
L1/L2 are legacy identifiers only.
They are not future folder structure or planning taxonomy.
```

```text
Behavior items are smaller than slices.
A slice implements an independent behavior chain and includes implementation/testing plan.
```

```text
Cross-cutting behavior requires scenario behavior sources too.
Implementation-only convention docs are not enough when behavior is required across scenarios.
```

```text
Add planning/diagrams/scenario-cross-cutting/ with:
  client-behavior
  server-behavior
  server-client
  security
```

```text
Client/server side-specific cross-cutting implementation drafts live under:
  planning/slices/client/cross-cutting/
  planning/slices/server/cross-cutting/
```

```text
planning/slices/cross-cutting/ is for umbrella/coordination docs.
```

```text
Use SINGLE- prefix for intentionally one-sided client-only/server-only slice drafts.
No SINGLE prefix means a paired counterpart may exist or may be added.
```

```text
Deprecate planning/slices/l2/README.md as legacy navigation only,
but preserve its historical guardrails until migrated.
```

```text
Deprecate planning/slices/client-slice-short-draft-rules-and-example.md in favor of CLIENT-SLICE-TEMPLATE.md.
```

```text
Every slice draft must include a Behavior-to-Test Trace.
```

```text
Tests prove behavior items and scenario outcomes.
Implementation details may be used only as setup/action/observation mechanisms.
```

```text
Each test trace must include escape risk and refactor risk.
```

```text
Direct DB setup is allowed for scenario preconditions only.
Behavior proof for API behavior should go through public boundary.
```

```text
No-mutation assertions are required for important failed command behavior.
```
