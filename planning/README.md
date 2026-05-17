# Planning Index

Status: current / L1 implementation status, ApplicantParty read/default command, request creation UI and remaining L1 gaps synchronized

## 1. Start Here

Future chats should be able to start from this file, then follow the relevant read order without relying on a long external prompt.

Core read order:

```text
planning/README.md
planning/l1-current-implementation-status.md
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

## 2. Current L1 Snapshot

Implemented/current:

```text
Backend:
- auth register/login/current-user/logout;
- create individual ApplicantParty;
- GET /api/l1/applicant-parties flat account ApplicantParties read;
- POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default;
- create connection request with Existing/New applicant context;
- My Requests list/filter/details.

Client:
- auth/register/login/current-user/logout flows;
- My Requests list/filter/details;
- request creation page /requests/create;
- Existing/New applicant context request creation form;
- account ApplicantParties read used by request creation.
```

Remaining L1 finish items:

```text
- SL-APPL-003.client make default/current button/action;
- replace old AccountPage single-current/current-individual UI with target flat Applicant Parties page/section;
- confirm/add make-current-default API integration tests;
- later compatibility decision for old current-individual endpoint.
```

Use:

```text
planning/l1-current-implementation-status.md
```

## 3. Current Applicant/Request Target Direction

```text
ApplicantParty:
- one Applicant Parties page / section is the planning model;
- top page area shows current/default ApplicantParty templates by applicant type;
- current/default templates are visually outlined/highlighted;
- below the default/current area, the page shows other saved ApplicantParties;
- create is additive, not replacement;
- first account+ApplicantPartyType initializes current/default;
- additional same-type create does not switch default/current silently;
- explicit make default/current backend command is implemented;
- client make default/current action is still remaining;
- existing requests do not change when ApplicantParty is created or default/current changes.
```

ApplicantParty account read model:

```text
GET /api/l1/applicant-parties
returns one flat applicantParties[] list.
Each item has isCurrentDefault.
Client groups current/default vs other saved cards by isCurrentDefault.
API does not return currentDefaults / otherApplicantParties layout arrays.
```

Request creation current state:

```text
- explicit applicant context: Existing ApplicantPartyId or New applicant data;
- Existing can use any owned saved ApplicantParty;
- New creates ApplicantParty + ConnectionRequest atomically;
- client /requests/create route and form exist;
- command success hands off to My Requests.
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

## 6. Agent Scope Rule

Prompts for implementation chats must not allow changing docs, domain code or generated artifacts unless the user explicitly asked for that scope.

Use:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

Implementation prompts generated from slice drafts must preserve the slice `Scope`, `Out of scope`, `Related slices` and `Future extension points`.

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
