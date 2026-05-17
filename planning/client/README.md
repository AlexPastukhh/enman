# Client Planning Index

Status: current client planning navigation / read-vs-command layering and short-draft rules synchronized  
Scope: client-wide UI/client conventions, cross-cutting client behavior and current L1 client implementation state

## 1. Purpose

This folder contains client-wide planning docs that are broader than a single slice sidecar.

## 2. Required Read Order For Client Work

```text
planning/client/README.md
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

This folder owns:

```text
- client-wide UI conventions;
- client-wide accessibility conventions;
- client-wide styling conventions;
- client-wide form validation conventions;
- client-wide error mapping conventions;
- client-wide feedback/message conventions;
- client-wide command success conventions;
- client layering conventions reused by multiple `.client.md` sidecars.
```

Concrete feature flow and status belongs in the matching `.client.md` slice sidecar.

## 4. Read vs Command Placement

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

## 5. Current L1 Client State Reminders

```text
- Register/login/current-user/logout client flows exist.
- My Requests list exists.
- My Requests filters and details may already be implemented in code; verify current repo before calling them planned.
- ApplicantParty client still needs migration from old current-individual model to flat Applicant Parties read model.
- Request creation UI is future unless current repo code proves otherwise.
```

## 6. Concrete Client Sidecar Rule

Do not create `.client.md` files in advance unless a concrete client slice is being drafted or implemented.

When drafting, use the canonical short-draft shape:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
```

Canonical current example:

```text
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
```


## Request Creation Client Sidecar

Current full draft:

```text
planning/slices/SL-REQ-001-create-connection-request.client.md
```

Direction:

```text
- command/user-action slice;
- page + features + entities + shared/api + generated contracts;
- Existing branch uses saved ApplicantParty selector;
- current/default ApplicantParty is initial selection only;
- New branch submits applicant data inside request creation journey;
- success hands off to My Requests because command success has no required requestId body.
```
