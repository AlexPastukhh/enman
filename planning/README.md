# Planning Index

Status: current / validation and My Requests filters synchronized

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

## 2. Current Applicant/Request Target Direction

```text
ApplicantParty:
- many saved ApplicantParties over time;
- one current/default template per applicant type;
- default/current is prefill/default selection only;
- first of type may initialize default;
- additional same-type create does not switch default silently;
- create is additive, not replacement.

Request creation target:
- explicit applicant context: Existing ApplicantPartyId or New applicant data;
- Existing can use any owned saved ApplicantParty;
- New creates ApplicantParty + ConnectionRequest atomically;
- command success has no required body for first cut.
```

Current implementation may still contain narrow/current-active names. When docs describe target planning, use the target direction above. When docs describe existing code, label it as current implementation evidence.

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

## 5. Agent Scope Rule

Prompts for implementation chats must not allow changing docs, domain code or generated artifacts unless the user explicitly asked for that scope.

Use:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

Reading docs is required. Mutating docs is not allowed unless the task says so.

## 6. Key Navigation

```text
planning/api/README.md
planning/client/README.md
planning/slices/README.md
planning/testing/README.md
planning/diagrams/README.md
planning/adr/README.md
```
