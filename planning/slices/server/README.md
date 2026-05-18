# Server Slice Planning

Status: server slice planning entry point / cross-cutting behavior and testing workflow synchronized

This folder owns server/backend/API slice drafting rules, templates, examples and server slice drafts.

New server drafts should go directly under:

```text
planning/slices/server/
```

Do not create new L1/L2 folders for server slice docs.

Until migration is complete, some existing server drafts may still live in older `planning/slices/*` paths.

## Required read order

```text
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/slice-test-plan-workflow.md
```

## Scenario source requirement

Before drafting or updating a server slice, identify scenario sources:

```text
business scenario
cross-cutting behavior
data source
behavior items
concern umbrella if applicable
```

If the slice implements common server/security/server-client behavior, check:

```text
planning/diagrams/scenario-cross-cutting/server-behavior/
planning/diagrams/scenario-cross-cutting/server-client/
planning/diagrams/scenario-cross-cutting/security/
```

## Cross-cutting server drafts

Server-side cross-cutting implementation drafts go here:

```text
planning/slices/server/cross-cutting/
```

If no client counterpart is expected, use `SINGLE-` prefix:

```text
SINGLE-CC-SERVER-PROBLEM-DETAILS-001-error-mapping.server.md
```

If a client counterpart is expected or possible, use paired logical ID and `.server.md` suffix:

```text
CC-SEC-CSRF-001-unsafe-command-protection.server.md
```

## Server slice docs should cover

```text
domain behavior
application handler/service boundary
FluentValidation boundary
persistence/read model
API contract
OpenAPI generation
integration tests
generated artifacts
Test / Verification Plan with Behavior-to-Test Trace
```
