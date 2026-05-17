# Agreement Exchange Start Client Sidecar Sync

Status: docs sync note / non-runtime  
Scope: adds the missing client command sidecar for starting an agreement exchange from Employee request details.

## Added slice

```text
planning/slices/l2/L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md
```

## Parent server slice

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
```

## Placement rule

```text
Start Agreement Exchange belongs on Employee request details because exchange does not exist yet.

After exchange exists, proposal negotiation belongs on Agreement Exchange details.
```

## Contract alignment guardrail

Current parent server slice and implementation prompt use:

```text
POST /api/employee/requests/{requestId}/agreement-exchange/start
success: 204 No Content
```

The new client sidecar records a preferred UX contract candidate:

```text
POST /api/agreement-exchanges
success: 200/201 with exchangeId
```

This is intentionally marked blocked until generated OpenAPI exists. Do not implement the client from the preferred candidate unless the server slice/OpenAPI is changed to match it.

## Client ownership

```text
features/agreement-exchange/start-exchange/*
```

Do not add business-specific wrappers to `shared/api`.
