# Cross-Cutting Slice Planning

Status: cross-cutting umbrella / coordination folder

This folder owns umbrella/coordination docs for concerns that connect client/server/security/server-wide behavior.

It is not a dump for implementation details.

## Use this folder for

```text
concern overview
scenario behavior source links
server slice draft links
client slice draft links
shared questions/decisions
status across sides
cross-side Behavior-to-Test Trace
```

## Do not use this folder for

```text
detailed client implementation draft
detailed server implementation draft
React/client CSS implementation plan
server handler/domain implementation plan
```

Side-specific implementation drafts belong in:

```text
planning/slices/client/cross-cutting/
planning/slices/server/cross-cutting/
```

## Scenario behavior sources

Cross-cutting behavior sources live in:

```text
planning/diagrams/scenario-cross-cutting/
```

Examples:

```text
security/CC-SEC-CSRF-001-unsafe-command-protection.behavior.md
client-behavior/CC-CLIENT-FORM-VALIDATION-001-deferred-validation.behavior.md
server-behavior/SINGLE-CC-SERVER-PROBLEM-DETAILS-001-error-mapping.behavior.md
```

## Example shape for paired security concern

```text
Scenario source:
  planning/diagrams/scenario-cross-cutting/security/CC-SEC-CSRF-001-unsafe-command-protection.behavior.md

Umbrella concern:
  planning/slices/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.md

Server slice:
  planning/slices/server/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.server.md

Client slice:
  planning/slices/client/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.client.md
```

## Cross-side testing

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

Umbrella docs for paired concerns should include:

```markdown
## Cross-Side Behavior-to-Test Trace

| Behavior item | Server proof | Client proof | E2E/user proof if needed | Gap |
|---|---|---|---|---|
```
