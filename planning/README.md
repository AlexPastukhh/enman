# Planning Index

Status: current / validation, My Requests filters, ApplicantParty one-page direction and backend cleanup boundary synchronized

## 1. Start Here

Future chats should be able to start from this file, then follow the relevant read order without relying on a long external prompt.

Core read order:

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/agent-scope-boundaries-and-prompt-safety.md
planning/planning-doc-responsibility-map.md
```

For documentation-only work also read:

```text
planning/documentation/README.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/replacement-file-generation-guide.md
```

For slice/client work read:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

For backend cleanup / legacy-to-L1 boundary work also read:

```text
planning/architecture/README.md
planning/architecture/backend-legacy-and-l1-boundaries.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
```

## 2. Current Applicant/Request Target Direction

```text
ApplicantParty:
- one Applicant Parties page / section is the planning model;
- do not split current docs into two user scenarios such as "Account page applicant section" and "My Applicant Parties page";
- top page area shows current/default ApplicantParty templates by applicant type;
- current/default templates are visually outlined/highlighted;
- below the default/current area, the page shows other saved ApplicantParties that are not selected as default/current;
- the same page owns add ApplicantParty action/form;
- the same page will later own explicit make default/current action;
- many saved ApplicantParties can exist over time;
- one current/default template may exist per applicant type;
- default/current is prefill/default selection only;
- first of type may initialize default/current;
- additional same-type create does not switch default/current silently;
- create is additive, not replacement;
- existing requests do not change when ApplicantParty is created or default/current changes.
```

Current implementation may still contain narrow/current-active names. When docs describe target planning, use the target direction above. When docs describe existing code, label it as current implementation evidence.

Request creation target:

```text
- explicit applicant context: Existing ApplicantPartyId or New applicant data;
- Existing can use any owned saved ApplicantParty;
- New creates ApplicantParty + ConnectionRequest atomically;
- command success has no required body for first cut.
```

## 3. Current My Requests Direction

Backend has implemented L1 read endpoints for:

```text
GET /api/l1/requests
GET /api/l1/requests/{requestId}
```

Client planning is split into sidecars:

```text
planning/slices/l1/L1-MY-REQUESTS-READ-LIST.client.md
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

Important boundary:

```text
Status filter is the first implemented entry in an extensible My Requests filter architecture.
It is not a one-off button.
```

## 4. Server Validation Direction

Use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

Current direction:

```text
FluentValidation owns L1 API request/query shape validation.
Application handlers own ownership/account/transaction behavior.
Domain owns invariants as final guard.
```

## 5. Backend Cleanup Boundary

Use:

```text
planning/architecture/backend-legacy-and-l1-boundaries.md
```

before backend cleanup, legacy removal/isolation, post-FluentValidation handler cleanup, test classification or thesis/diploma architecture writing.

This file explains:

```text
- current L1 runtime surface;
- legacy runtime surface;
- shared/current/future domain primitives;
- PasswordHash vs Password boundary;
- handler value-object creation after FluentValidation;
- current vs legacy test classification.
```

## 6. Agent Scope Rule

Prompts for implementation chats must not allow changing docs, domain code or generated artifacts unless the user explicitly asked for that scope.

Use:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

Reading docs is required. Mutating docs is not allowed unless the task says so.

## 7. Key Navigation

```text
planning/api/README.md
planning/architecture/README.md
planning/client/README.md
planning/slices/README.md
planning/testing/README.md
planning/diagrams/README.md
planning/adr/README.md
```
