# Scenario Cross-Cutting Behavior

Status: scenario source area for cross-cutting behavior

This folder owns required behavior that applies across multiple scenarios or across client/server boundaries.

These are scenario/behavior sources, not implementation drafts.

## Subfolders

```text
client-behavior/
  common client behavior required by multiple UI/client scenarios
  and not inherently requiring server implementation.

server-behavior/
  common server behavior required by multiple server/API/domain slices.

server-client/
  common behavior where server and client both participate.

security/
  security/protection/abuse scenarios, including dangerous flows to prevent.
```

## Relationship to slices

Cross-cutting behavior sources feed slice drafts.

Examples:

```text
client-behavior/CC-CLIENT-FORM-VALIDATION-001-deferred-validation.behavior.md
        ↓
planning/slices/client/cross-cutting/SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md
```

```text
security/CC-SEC-CSRF-001-unsafe-command-protection.behavior.md
        ↓
planning/slices/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.md
        ↓
planning/slices/server/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.server.md
planning/slices/client/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.client.md
```

## Rule

A cross-cutting slice draft must cite a scenario-cross-cutting source when it implements behavior that is not owned by one subject scenario.
