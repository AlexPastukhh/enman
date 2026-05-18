# Server Slice Planning

Status: server slice planning entry point

This folder owns server/backend/API slice drafting rules, templates, examples and server slice drafts.

New server drafts should go directly under:

```text
planning/slices/server/
```

Do not create new L1/L2 folders for server slice docs.

Until migration is complete, some existing server drafts may still live in older `planning/slices/*` paths.

Server slice docs should cover:

```text
domain behavior
application handler/service boundary
FluentValidation boundary
persistence/read model
API contract
OpenAPI generation
integration tests
generated artifacts
```

Read:

```text
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
```
