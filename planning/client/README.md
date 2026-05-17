# Client Planning Index

Status: current client planning navigation / request creation UI implemented and remaining ApplicantParty client gaps synchronized  
Scope: client-wide UI/client conventions, cross-cutting client behavior and current L1 client implementation state

## 1. Purpose

This folder contains client-wide planning docs that are broader than a single slice sidecar.

## 2. Required Read Order For Client Work

```text
planning/client/README.md
planning/l1-current-implementation-status.md
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

## 3. Responsibility

This folder owns client-wide conventions:

```text
- UI/accessibility/styling conventions;
- form validation conventions;
- error/feedback conventions;
- command success conventions;
- read-vs-command layering conventions.
```

Concrete feature flow and status belongs in the matching `.client.md` sidecar.

## 4. Current L1 Client State

Implemented/current:

```text
- Register/login/current-user/logout flows.
- My Requests list, filters and details.
- Request creation route `/requests/create`.
- Request creation page/form with Existing/New applicant context.
- Account ApplicantParties read used by request creation.
```

Remaining L1 client gaps:

```text
- SL-APPL-003.client make default/current button/action.
- Replace old AccountPage single-current/current-individual UI with target flat Applicant Parties page/section.
- Add shared API wrapper/path for make-current-default if still missing during implementation.
```

Future/not-current gaps:

```text
- ApplicantParty delete/archive/edit lifecycle UI.
- LegalEntity / IndividualEntrepreneur ApplicantParty create UI.
```

## 5. Read vs Command Placement

Read slices use:

```text
pages + entities + shared/api + generated contracts
```

Command/user-action slices use:

```text
pages + features + entities + shared/api + generated contracts
```

Read-only UI belongs in:

```text
entities/<entity>/ui
```

Command/action UI belongs in:

```text
features/<action>/ui
```

Shared API wrappers live in `shared/api` even when they call endpoints for different entities, because `shared/api` is the low-level client/server contract boundary.

Detailed rule:

```text
planning/client/client-layering-for-read-and-command-slices.md
```

## 6. Concrete Client Sidecar Rule

Do not create `.client.md` files in advance unless a concrete client slice is being drafted, implemented or reconciled with existing code.

When drafting, use the canonical short-draft shape:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
```

Current implemented command sidecar:

```text
planning/slices/SL-REQ-001-create-connection-request.client.md
```

Current target/read sidecar still needing page replacement work:

```text
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
```

Likely next client sidecar/implementation:

```text
SL-APPL-003.client — Make ApplicantParty Current/Default Action
```
