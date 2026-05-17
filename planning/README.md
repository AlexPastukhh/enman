# Planning Index

Status: current / L1 baseline and near-final L2 Employee Review + Agreement Exchange planning synchronized

## 1. Start Here

Future chats should be able to start from this file, then follow the relevant read order without relying on a long external prompt.

Core read order:

```text
planning/README.md
planning/l1-current-implementation-status.md
planning/l2-current-planning-status.md
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

For scenario/diagram work read:

```text
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/diagrams/scenario-clarifications/README.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

For slice/client/server work read:

```text
planning/slices/README.md
planning/slices/l2/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

For backend cleanup / legacy-to-L1/L2 boundary work also read:

```text
planning/architecture/README.md
planning/architecture/backend-legacy-and-l1-boundaries.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
```

For non-canonical thesis/diploma recovery notes:

```text
planning/dirty-drafts/README.md
```

Dirty drafts are not source of truth. Use them only to recover explanations or thesis wording after canonical docs are checked.

## 2. Source-of-Truth Rules

```text
Scenario specs are source of truth for scenario behavior.
Slice docs map a scenario portion to one implementation slice.
Domain drafts are domain-design input, not a replacement for scenario specs.
GitHub/current branch is source of truth for current implementation state.
Archives are handoff artifacts, not current-state truth.
Dirty drafts are non-canonical scratch/recovery/thesis notes.
```

When the user asks about what is implemented now, inspect GitHub/current branch directly.

## 3. Current L1 Snapshot

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

## 4. Current Applicant/Request Target Direction

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

## 5. Current L2 Snapshot

L2 planning is almost complete for the Employee Review and Agreement Exchange cut.

Use:

```text
planning/l2-current-planning-status.md
planning/slices/l2/README.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
```

Canonical L2 server slice set:

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
SL-EMP-REQ-003 — Start Request Review
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review

SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Canonical L2 client sidecar set:

```text
L2-EMP-DASH-001.client
L2-EMP-DETAILS-001.client
L2-REVIEW-START-001.client
L2-REVIEW-APPROVE-001.client
L2-REVIEW-REJECT-001.client

L2-AGR-EXCH-START-001.client
L2-AGR-EXCH-LIST-001.client
L2-AGR-EXCH-DETAILS-001.client
L2-AGR-EXCH-SEND-PROPOSAL-001.client
L2-AGR-EXCH-ACCEPT-001.client
L2-AGR-EXCH-FINAL-REFUSE-001.client
```

Key L2 guardrails:

```text
Employee : Account / TPH target.
No EmployeeRef.
Review is owned by Request.
RejectReview feedback is optional.
ApproveReview does not create AgreementProposalExchange.
AgreementProposalExchange stores ClientAccountId.
No ResponsibleEmployeeId guard first pass.
Counter-proposal replacement is SupersededByCounterProposal.
AgreementDocumentRef is metadata reference, not bytes/storage adapter.
No per-command status enums.
```

## 6. Server Validation Direction

Use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

Current direction:

```text
FluentValidation owns API request/query shape validation.
Application handlers/services own orchestration, loading, ownership and transaction behavior.
Domain owns lifecycle, participant and invariant checks as final guard.
```

Do not put ownership, lifecycle, current turn, active proposal author or exchange status rules into FluentValidation.

## 7. Backend Cleanup Boundary

Use:

```text
planning/architecture/backend-legacy-and-l1-boundaries.md
```

before backend cleanup, legacy removal/isolation, post-FluentValidation handler cleanup, test classification or thesis/diploma architecture writing.

## 8. Agent Scope Rule

Prompts for implementation chats must not allow changing docs, domain code or generated artifacts unless the user explicitly asked for that scope.

Use:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

Implementation prompts generated from slice drafts must preserve the slice `Scope`, `Out of scope`, `Related slices` and `Future extension points`.

## 9. Key Navigation

```text
planning/api/README.md
planning/architecture/README.md
planning/client/README.md
planning/slices/README.md
planning/slices/l2/README.md
planning/testing/README.md
planning/diagrams/README.md
planning/adr/README.md
planning/dirty-drafts/README.md
```
