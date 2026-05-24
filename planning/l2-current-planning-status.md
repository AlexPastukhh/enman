# L2 Current Planning Status

> Historical/internal status note.
> This file is not implementation truth and may be stale.
> Do not use it as proof that a feature is implemented.
> For current implementation state, inspect the current branch, code, tests, migrations, generated API contracts and runtime screenshots.
> For VKR/thesis wording, use `planning/vkr-clean-reference.md`.

Status: current / L2 Employee Review and Agreement Exchange planning nearly complete  
Scope: L2 planning status, canonical slice navigation, remaining draft/implementation gaps

## 1. Purpose

This file gives future chats a compact current-state entry point for the L2 planning cut.

Use it after the central planning read order:

```text
planning/README.md
planning/planning-workflow-current.md
planning/slices/README.md
planning/slices/l2/README.md
planning/diagrams/README.md
```

## 2. Current L2 Planning State

L2 planning is now close to complete for the Employee Review and Agreement Exchange cut.

Completed / synchronized planning areas:

```text
- Employee request dashboard/list read;
- Employee request details read;
- StartReview backend/client command direction;
- ApproveReview backend command direction;
- RejectReview backend command direction with optional feedback;
- Agreement Exchange server slice family 001..006;
- Agreement Exchange client sidecar family for start/list/details/send/accept/final-refuse;
- Employee : Account / TPH identity direction;
- AgreementProposalExchange.ClientAccountId participant/ownership rule;
- no ResponsibleEmployeeId guard first pass;
- proposal replacement uses SupersededByCounterProposal, not ordinary Rejected;
- AgreementDocumentRef document metadata reference direction;
- slice drafting / implementation checklist rules;
- diagram prompt generation workflow and three-part diagram batch plan;
- dirty-drafts area for non-canonical thesis/diploma recovery notes.
```

Current remaining planning/docs work is mostly cleanup:

```text
- apply outstanding docs-only sync archives if not applied locally;
- run grep checks for stale L2 terms;
- keep central README/status files synchronized after each implementation archive;
- update implementation status from GitHub/current branch after each actual merge.
```

Current remaining implementation work must be verified from the GitHub branch, not from archives.

## 3. Canonical L2 Server Slice Set

Employee request/review slices:

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
SL-EMP-REQ-003 — Start Request Review
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review
```

Agreement exchange slices:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Document/reference future family:

```text
SL-DOC-* — AgreementDocumentRef / document metadata/storage follow-up work
```

## 4. Canonical L2 Client Sidecar Set

Employee request/review client sidecars:

```text
L2-EMP-DASH-001.client — Employee Request Dashboard
L2-EMP-DETAILS-001.client — Employee Request Details
L2-REVIEW-START-001.client — Start Request Review
L2-REVIEW-APPROVE-001.client — Approve Request Review
L2-REVIEW-REJECT-001.client — Reject Request Review
```

Agreement exchange client sidecars:

```text
L2-AGR-EXCH-START-001.client — Start Agreement Exchange With Initial Employee Proposal
L2-AGR-EXCH-LIST-001.client — Agreement Exchange List
L2-AGR-EXCH-DETAILS-001.client — Agreement Exchange Details
L2-AGR-EXCH-SEND-PROPOSAL-001.client — Send Agreement Proposal Version
L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal
L2-AGR-EXCH-FINAL-REFUSE-001.client — Employee Final Refuse Agreement Exchange
```

## 5. Current Source-of-Truth Rules

```text
Scenario specs are source of truth for Scenario Flow and Behavior Coverage.
Slice docs map scenario portions to implementation slices.
Domain drafts provide domain-design input, not replacement scenario behavior.
GitHub/current branch is source of truth for current implementation state.
Dirty drafts are non-canonical recovery/thesis notes only.
Archives are handoff artifacts, not current-state truth.
```

## 6. L2 Guardrails

```text
Use Employee, not Worker.
Use Start/Started, not Open/Opened.
Do not use EmployeeRef in L2 target.
Employee derives from Account in target model.
ClaimTypes.NameIdentifier stores Account.Id; for Employee sessions Account.Id == Employee.Id.
Review is owned by Request; no Review repository.
ApproveReview does not create AgreementProposalExchange.
RejectReview feedback is optional unless implementation intentionally changes it.
AgreementProposalExchange stores ClientAccountId.
Do not add ResponsibleEmployeeId as first-pass authorization guard.
Any active Employee can service agreement exchanges first pass.
Proposal sender is tracked per proposal version with Sender and SenderId.
Counter-proposal replacement is SupersededByCounterProposal, not Rejected.
AgreementDocumentRef is metadata reference, not file bytes/storage adapter.
Do not add per-command status enums.
Command success usually returns 204 No Content unless the slice explicitly needs response data such as exchangeId.
```

## 7. After Applying Implementation Archives

When backend/API shape changes:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
```

Include generated artifacts in the same handoff/commit when the API contract changed:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Run local checks appropriate to the implementation slice.

## 8. Stale Term Check

Use these checks after large docs syncs:

```powershell
git grep -n "scenario-server-domain-validation-addendum"
git grep -n "DocumentFileRef"
git grep -n "ReviewerRef"
git grep -n "ProposalAttachment"
git grep -n "EmployeeRef"
git grep -n "ResponsibleEmployeeId"
git grep -n "Client Data Verification"
git grep -n "SL-AGR-EXCH-003.*Read Agreement Exchange"
git grep -n "SL-AGR-EXCH-004.*Accept Active"
git grep -n "SL-AGR-EXCH-005.*Final Refuse"
```

If results appear in deprecated/dirty-draft files, they may be historical. If they appear in active scenario/slice docs, update or mark them as deprecated.
