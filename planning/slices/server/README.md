# Server Slice Planning

Status: local server slice folder entry point

This folder contains server slice templates and server/backend/API slice drafts.

Reusable server rules and principles live in the root slice folder:

```text
planning/slices/
```

New server drafts should go directly under:

```text
planning/slices/server/
```

Do not create new L1/L2 folders for server slice docs.

Until migration is complete, some existing server drafts may still live in older `planning/slices/*` paths.

## Required read order

Before drafting or updating a server slice, start from the root slice docs:

```text
planning/slices/README.md
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/server-implementation-principles.md
planning/slices/slice-test-plan-workflow.md
```

Then use local server docs:

```text
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
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

## Draft locations

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
