# Client Planning Index

Status: current client planning navigation / read-vs-command layering, request creation UI and make-current/default sidecar synchronized  
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
- My Requests list, filters and details exist.
- Request creation UI exists.
- Account ApplicantParties flat read is available for client consumption.
- ApplicantParty client still needs final migration from old current-individual AccountPage model to target Applicant Parties page/section if current UI has not been fully replaced.
- Make current/default client action is now documented by full sidecar and is the next likely client implementation gap.
```

## 6. Concrete Client Sidecars

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-AUTH-003-logout.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.client.md
planning/slices/SL-REQ-001-create-connection-request.client.md
planning/slices/l1/L1-MY-REQUESTS-READ-LIST.client.md
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

## 7. Drafting Rule

Do not create `.client.md` files in advance unless a concrete client slice is being drafted or implemented.

When drafting, use the canonical short-draft shape:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
```

For full sidecar examples, use:

```text
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
planning/slices/SL-REQ-001-create-connection-request.client.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.client.md
```
