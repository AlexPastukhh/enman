# Client Planning Index

Status: current client planning navigation / entity-feature API ownership synchronized  
Scope: client-wide UI/client conventions, cross-cutting client behavior and client implementation mapping

## 1. Required Read Order For Client Work

```text
planning/client/README.md
planning/client/client-api-placement-decision.md
planning/client/client-layering-for-read-and-command-slices.md
planning/client/cross-cutting/README.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
planning/api/generated-artifact-check-workflow.md
```

## 2. Read vs Command Placement

Read slices use:

```text
pages + entities + shared transport/generated infrastructure
```

Command/user-action slices use:

```text
pages + features + entities + shared transport/generated infrastructure
```

Read-only UI belongs in:

```text
entities/<entity>/ui
```

Read endpoint API functions belong in:

```text
entities/<entity>/api
```

Command/action UI belongs in:

```text
features/<action>/ui
```

Command/mutation endpoint API functions belong in:

```text
features/<action>/api
```

`shared/api` owns true shared infrastructure only:

```text
fetchJson
ProblemDetails / ApiError
antiforgery/CSRF transport helpers
generated OpenAPI types
generic request/query helpers
```

## 3. Transitional Compatibility

Some existing client runtime/docs may still have business endpoint wrappers under:

```text
shared/api/<businessArea>Api.ts
```

Treat those as transitional compatibility.

Do not mass-migrate without a concrete slice implementation or cleanup task.

For new L2 work and new client drafts:

```text
entity read wrappers in entities/*/api;
feature command wrappers in features/*/api;
shared/api only for transport/generated infrastructure.
```

## 4. Drafting Rule

Do not create `.client.md` files in advance unless a concrete client slice is being drafted or implemented.

When drafting, use:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
```

Full sidecars and implementation handoffs must follow the current API placement rule, even if older examples still show a business-specific shared API wrapper.
