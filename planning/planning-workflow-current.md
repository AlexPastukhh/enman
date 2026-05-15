# Current Planning Workflow

Status: current workflow

## Current Point

```text
API contract / OpenAPI / client constants generation workflow
and accessibility-as-test-contract hardening have been introduced.
```

## Main Workflow

```text
scenario text specs
+ scenario DATA files
+ validation/security addenda
+ scenario UI specs when client-visible behavior is being planned
-> behavior items / UI behavior items
-> scenario questions register
-> architecture decision notes / ADR candidates when decisions are made
-> domain draft(s) / L1 domain foundation
-> L1 slice boundary draft
-> parent vertical slice files with API/error contract
-> .client.md sidecar when concrete client work starts
-> implement one slice/client layer at a time
```

## API Contract Gate

When a slice exposes or consumes API:

```text
1. Parent slice documents endpoint/request/response/status/Error contract.
2. OpenAPI structural contract is identified.
3. Client-facing error codes are listed.
4. Internal/server-only errors are excluded from client contract.
5. Client sidecar maps DTO fields, error codes and stale/refetch behavior.
6. Generated constants and OpenAPI types are referenced.
```

Use:

```text
planning/api/api-error-contract.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
```

## Deferred Infra

```text
.NET upgrade is deferred.
Do not mix runtime upgrade with API contract / client UI work.
```
