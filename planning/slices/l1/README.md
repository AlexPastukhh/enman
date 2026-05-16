# L1 Client Sidecar Index

Status: current L1 client sidecar navigation

## 1. Purpose

This folder contains L1 client sidecars for concrete client implementation work and implemented-client status reconciliation.

Read this together with:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/client/README.md
planning/api/client-server-contract-principles.md
```

## 2. Current Sidecars

| File | Scope | Status |
|---|---|---|
| `L1-MY-REQUESTS-READ-LIST.client.md` | My Requests list page/read state | first-stage implemented client sidecar |
| `L1-MY-REQUESTS-LIST-FILTERS.client.md` | My Requests filter architecture; status is first filter | implementation-ready client sidecar |
| `L1-MY-REQUEST-DETAILS.client.md` | Own Request Details page/read state | implementation-ready client sidecar |

## 3. Boundary Rules

```text
List sidecar owns list page/read behavior.
Filters sidecar owns filter architecture and status filter UI.
Details sidecar owns /requests/:requestId read context.
```

Implementation chats may read these docs freely, but must not edit docs unless the task explicitly includes documentation updates.
