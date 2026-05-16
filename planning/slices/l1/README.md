# L1 Client Sidecar Index

Status: current L1 client sidecar navigation / canonical short-draft rules, filters and details implementation status synchronized

## 1. Purpose

This folder contains L1 client sidecars for concrete client implementation work and implemented-client status reconciliation.

Read this together with:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/client/README.md
planning/client/client-layering-for-read-and-command-slices.md
planning/api/client-server-contract-principles.md
```

## 2. Current Sidecars

| File | Scope | Status |
|---|---|---|
| `L1-MY-REQUESTS-READ-LIST.client.md` | My Requests list page/read state | first-stage implemented client sidecar / normalized compact status note |
| `L1-MY-REQUESTS-LIST-FILTERS.client.md` | My Requests filter architecture; status is first filter | implemented first-stage client sidecar / normalized to canonical draft style |
| `L1-MY-REQUEST-DETAILS.client.md` | Own Request Details page/read state | implemented first-stage client sidecar / normalized compact status note |

ApplicantParty client sidecar currently lives at parent slice level:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
```

`SL-APPL-002.client` is a full read sidecar draft for the target Applicant Parties read model. It is blocked until the backend endpoint and generated types exist.

## 3. Boundary Rules

```text
List sidecar owns list page/read behavior.
Filters sidecar owns filter architecture and status filter UI.
Details sidecar owns /requests/:requestId read context.
```

For filters:

```text
Page owns URL query params.
Filter UI is controlled by page state.
Entity query accepts a filter object.
Shared API maps supported filters to query string.
```

For read-vs-command client layering:

```text
Read-only display UI belongs under entities/<entity>/ui.
Command/user-action UI belongs under features/<entity-or-flow>/.
Shared API owns low-level HTTP wrappers and generated DTO aliases.
Entity API/model owns domain-facing read operation names, query hooks and query keys.
Pages own route/session/URL/read-state composition.
```

## 4. Drafting Rule

Client sidecar drafters must copy the canonical short-draft form from:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
```

Do not invent a new draft shape without explicit user instruction.

Keep Scenario Flow separate from Implementation Flow:

```text
Scenario Flow:
  user/system behavior from scenario specs for this slice.

Implementation Flow:
  page/entity/feature/shared/generated layers, files, functions, classes, types and ownership.
```

Implementation details are not behavior items.

Implementation chats may read these docs freely, but must not edit docs unless the task explicitly includes documentation updates.
