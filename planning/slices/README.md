# Slice Planning Index

Status: canonical slice planning entry point / taxonomy, cross-cutting behavior and test workflow synchronized

This folder owns slice planning navigation for server, client and cross-cutting slice work.

## Start Here

```text
planning/slices/SLICE-FOLDER-MAP.md
planning/slices/SLICE-INDEX.md
planning/slices/SLICE-QUESTIONS.md
planning/slices/slice-test-plan-workflow.md
planning/slices/client/README.md
planning/slices/server/README.md
planning/slices/cross-cutting/README.md
```

## Core Rule

Scenario sources describe required behavior.

Slice drafts describe:

```text
which behavior subset is implemented;
how the implementation is planned;
how the implementation is verified.
```

Data files and behavior item files extract, clarify and classify details from scenarios.

Do not invent behavior locally inside a slice draft when scenario/behavior sources exist.

## Scenario Source Layer

```text
planning/diagrams/scenario-text-specs/
  subject/business scenarios.

planning/diagrams/scenario-ui-specs/
  UI scenarios: visible flow, screen composition, visible states/actions.

planning/diagrams/scenario-cross-cutting/
  cross-cutting behavior sources:
    client-behavior/
    server-behavior/
    server-client/
    security/

planning/diagrams/scenario-data/
  scenario data: what the user enters/sees and data shapes needed by scenarios.

planning/diagrams/scenario-behavior-items/
  behavior items extracted from scenarios.
```

## Slice Planning Layer

```text
planning/slices/client/
  client slice drafts and client implementation conventions.

planning/slices/client/cross-cutting/
  client-side implementation drafts for cross-cutting behavior.

planning/slices/server/
  server slice drafts and server implementation conventions.

planning/slices/server/cross-cutting/
  server-side implementation drafts for cross-cutting behavior.

planning/slices/cross-cutting/
  umbrella/coordination docs for paired server-client/security/cross-cutting concerns.
```

## Slice Shape

A slice is a responsibility boundary, not a guarantee that every change goes vertically through every application layer.

```text
Slice != always vertical.
Same endpoint != same UI slice.
Same DTO != same page.
Different actor journey can justify separate client sidecars.
```

The reverse can also be true: when a first-pass response shape is intentionally common, different page shells may use the same endpoint, query/model and widget.

Client and server drafts can be two implementation parts of one logical slice. They are separated for planning, implementation and testing convenience.

A slice can be:

```text
paired client/server;
client-only;
server-only;
extension/follow-up;
cross-cutting.
```

## Behavior Items vs Slices

Behavior items are smaller than slices.

```text
One scenario can contain many behavior items.
One scenario can be implemented by multiple slices.
One slice can implement an independent chain of behavior items.
One slice can extend behavior already implemented by another slice.
```

A slice draft should list behavior items it covers and behavior items it explicitly leaves out.

## Test / Verification Rule

Every slice draft must include a Behavior-to-Test Trace.

```text
behavior item
  -> scenario outcome being proved
  -> verification layer
  -> implementation mechanism
  -> escape risk
  -> refactor risk
  -> planned/actual test
```

Primary testing rule:

```text
Tests verify behavior items and scenario outcomes.

Implementation details may appear only as:
  setup mechanism;
  action transport;
  observation mechanism;
  persistence/visible-state assertion mechanism.
```

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

Do not make implementation details the reason a test exists.

## New Folder Rule

New slice docs are grouped by implementation responsibility, not by L1/L2 level.

Use:

```text
planning/slices/client/
  for client sidecars, UI workflow, CSS rules and client slice drafts

planning/slices/server/
  for server/backend/API slice drafts and server workflow

planning/slices/cross-cutting/
  for concerns shared across client/server or architecture-level decisions
```

Do not create new `planning/slices/l1` or `planning/slices/l2` files.

Existing historical L1/L2 files may remain during migration and should be indexed from `SLICE-INDEX.md` until they are rewritten or moved.

## Naming Rule For Paired And Single Drafts

If a client/server counterpart is expected or possible, use the same logical ID with side suffix:

```text
planning/slices/server/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.server.md
planning/slices/client/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.client.md
```

If no counterpart is expected, mark it explicitly with `SINGLE-` at the start of the file name:

```text
planning/slices/client/cross-cutting/SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md
planning/slices/server/cross-cutting/SINGLE-CC-SERVER-PROBLEM-DETAILS-001-error-mapping.server.md
```

Rule:

```text
No SINGLE prefix means a paired counterpart may exist or may be added.
SINGLE prefix means this slice draft is intentionally one-sided.
```

## Cross-Cutting Rule

Cross-cutting implementation files are still slice drafts.

They must use the normal client/server slice draft structure, but their scenario source may be a cross-cutting behavior source rather than one subject business scenario.

For server-client/security concerns, use:

```text
1. scenario behavior source
2. umbrella concern doc
3. server slice draft
4. client slice draft
```

`planning/slices/cross-cutting/` is for umbrella/coordination docs, not a dumping place for implementation details.

## Current Client API Placement Rule

```text
Read endpoint wrappers:
  entities/*/api

Command/mutation endpoint wrappers:
  features/*/api

Shared API:
  fetchJson
  ProblemDetails / ApiError
  CSRF/antiforgery helpers
  generated OpenAPI types
  generic transport helpers
```

Existing business-specific wrappers in `shared/api` are transitional compatibility. New client drafts should not copy that shape.

## Client UI/CSS Rule

Client slice drafts must treat UI layout and CSS ownership as part of the slice, not as post-implementation polish.

New `.client.md` drafts must include:

```text
Visual UI / Scenario Flow
Visual Layout / Screen Composition
Visual Client Implementation Flow
Styling / CSS Ownership
Validation / Feedback / Error UI
Accessibility / ARIA Contract
Test / Verification Plan with Behavior-to-Test Trace
```

See:

```text
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-UI-STYLE-WORKFLOW.md
planning/slices/client/CLIENT-CSS-ARCHITECTURE-RULES.md
planning/slices/slice-test-plan-workflow.md
```

## Historical Slice Draft Navigation

Some current slice drafts may still be located in legacy paths such as:

```text
planning/slices/l2/
planning/slices/SL-*.md
```

Those files are not rewritten by this docs workflow update. Use `SLICE-INDEX.md` to find them during migration.

## Current L2 Review Drafts — Legacy Navigation

Server/API slices may still be found in legacy root paths:

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md
planning/slices/SL-EMP-REQ-004-approve-request-review.md
planning/slices/SL-EMP-REQ-005-reject-request-review.md
```

Client sidecars may still be found in legacy paths:

```text
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
planning/slices/l2/L2-REVIEW-APPROVE-001-approve-request-review.client.md
planning/slices/l2/L2-REVIEW-REJECT-001-reject-request-review.client.md
```

Do not add new files to those legacy locations.

## AgreementProposalExchange Slice Family — Legacy Navigation

Canonical server/backend/API slices may still be found in legacy paths:

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md
planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md
planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md
```

Canonical client sidecars may still be found in legacy paths:

```text
planning/slices/l2/L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md
planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md
planning/slices/l2/L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md
planning/slices/l2/L2-AGR-EXCH-SEND-PROPOSAL-001-send-agreement-proposal-version.client.md
planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
planning/slices/l2/L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md
```

Future updated drafts should be created directly under:

```text
planning/slices/client/
planning/slices/server/
```

## Agreement Exchange Guardrails

```text
AgreementProposalExchange and Request are separate aggregates.
ApproveReview does not create AgreementProposalExchange.
Initial exchange creation requires initial Employee proposal document.
AgreementProposalExchange stores ClientAccountId from approved request owner.
Client actions are protected by client.Id == exchange.ClientAccountId.
Do not add ResponsibleEmployeeId as first-pass authorization guard.
Any active Employee can service agreement exchanges first pass.
Proposal author is stored per proposal version through Sender and SenderId.
Counter-proposal replacement is SupersededByCounterProposal, not ordinary Rejected.
AgreementDocumentRef is document metadata reference, not bytes/storage adapter.
Do not use per-command status enums.
```

## StartReview Entry Point Rule

StartReview client has one command sidecar and two entry points:

```text
dashboard/list row
details action area
```

Dashboard/details read pages host feature actions; they do not own command mutations.

## Migration Note

Old client planning docs under `planning/client/` are deprecated. New client planning docs live under:

```text
planning/slices/client/
```

Existing old slice files may remain in their current paths during migration. New client slice drafts should be created directly under:

```text
planning/slices/client/
```

New server slice drafts should be created directly under:

```text
planning/slices/server/
```

Do not mass-move historical drafts without a dedicated cleanup task. Keep `SLICE-INDEX.md` updated while old and new paths coexist.

## Current-State Rule

When asked what exists or is implemented now, inspect the current repository branch.

Do not answer current implementation status from uploaded archives or slice drafts alone.
