# Slice Index

Status: working index / taxonomy and testing workflow synchronized

This file is the common navigation index for server, client and cross-cutting slice docs.

Use it while old and new paths coexist during migration.

## Current file-location rule

```text
New client drafts:
  planning/slices/client/

New server drafts:
  planning/slices/server/

New client-side cross-cutting drafts:
  planning/slices/client/cross-cutting/

New server-side cross-cutting drafts:
  planning/slices/server/cross-cutting/

Umbrella cross-cutting docs:
  planning/slices/cross-cutting/
```

Historical L1/L2 paths are legacy only.

## Workflow docs

| ID | Title | Type | Status | Current path | Notes |
|---|---|---|---|---|---|
| SLICE-TEST-PLAN-WORKFLOW | Slice Test Plan Workflow | workflow | current | `planning/slices/slice-test-plan-workflow.md` | Required Behavior-to-Test Trace for slice drafts |
| CLIENT-SLICE-TEMPLATE | Client Slice Template | template | current | `planning/slices/client/CLIENT-SLICE-TEMPLATE.md` | Includes scenario sources and test trace |
| SERVER-SLICE-TEMPLATE | Server Slice Template | template | current | `planning/slices/server/SERVER-SLICE-TEMPLATE.md` | Includes scenario sources and test trace |
| CROSS-CUTTING-UMBRELLA-TEMPLATE | Cross-Cutting Umbrella Template | template | current | `planning/slices/cross-cutting/CROSS-CUTTING-UMBRELLA-TEMPLATE.md` | Includes cross-side test trace |

## Legacy/current slice index

| Slice ID | Title | Type | Status | Related source | Current path | Notes |
|---|---|---|---|---|---|---|
| SL-EMP-REQ-001 | Employee Request List Read | server read | legacy/current status TBD | SC-06 + missing UI spec | `planning/slices/SL-EMP-REQ-001-employee-request-list-read.md` | Historical location; move/update later |
| SL-EMP-REQ-002 | Employee Request Details Read | server read | legacy/current status TBD | SC-07A + missing UI spec | `planning/slices/SL-EMP-REQ-002-employee-request-details-read.md` | Historical location; move/update later |
| SL-EMP-REQ-003 | Start Request Review | server command | legacy/current status TBD | SC-07B + missing review actions UI spec | `planning/slices/SL-EMP-REQ-003-start-request-review.md` | StartReview has dashboard/details entry points |
| SL-EMP-REQ-004 | Approve Request Review | server command | legacy/current status TBD | SC-07B + missing review actions UI spec | `planning/slices/SL-EMP-REQ-004-approve-request-review.md` | Details-only first pass |
| SL-EMP-REQ-005 | Reject Request Review | server command | legacy/current status TBD | SC-07B + missing review actions UI spec | `planning/slices/SL-EMP-REQ-005-reject-request-review.md` | Feedback optional |
| L2-EMP-DASH-001.client | Employee Request Dashboard | client read sidecar | legacy/current status TBD | SC-06 + missing UI spec | `planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md` | Legacy location; update with new template later |
| L2-EMP-DETAILS-001.client | Employee Request Details | client read sidecar | legacy/current status TBD | SC-07A + missing UI spec | `planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md` | Legacy location; update with new template later |
| L2-REVIEW-START-001.client | Start Request Review | client command sidecar | legacy/current status TBD | SC-07B + missing UI spec | `planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md` | Legacy location; update with new template later |
| L2-REVIEW-APPROVE-001.client | Approve Request Review | client command sidecar | legacy/current status TBD | SC-07B + missing UI spec | `planning/slices/l2/L2-REVIEW-APPROVE-001-approve-request-review.client.md` | Legacy location; update with new template later |
| L2-REVIEW-REJECT-001.client | Reject Request Review | client command sidecar | legacy/current status TBD | SC-07B + missing UI spec | `planning/slices/l2/L2-REVIEW-REJECT-001-reject-request-review.client.md` | Legacy location; update with new template later |
| SL-AGR-EXCH-001 | Start Agreement Exchange With Initial Employee Proposal | server command | legacy/current status TBD | SC-13D + missing UI spec | `planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | Creates exchange and initial Employee proposal |
| L2-AGR-EXCH-START-001.client | Start Agreement Exchange With Initial Employee Proposal | client command sidecar | draft/implementation archive exists | SC-13D + missing UI spec | legacy path or future `planning/slices/client/` | Employee request details action |
| SL-AGR-EXCH-002 | Send Agreement Counter-Proposal Version | server command | legacy/current status TBD | SC-13B/13D + missing UI spec | `planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | Existing exchange proposal version |
| L2-AGR-EXCH-SEND-PROPOSAL-001.client | Send Agreement Proposal Version | client command sidecar | draft/implementation archive exists | SC-13B + missing UI spec | legacy path or future `planning/slices/client/` | Shared Client/Employee details action |
| SL-AGR-EXCH-003 | Agreement Exchange List Page / Read List | server read | legacy/current status TBD | SC-13A/13C + missing UI spec | `planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md` | Shared list endpoint first pass |
| L2-AGR-EXCH-LIST-001.client | Shared Agreement Exchange List Pages | client read sidecar | draft/implementation archive exists | SC-13A/13C + missing UI spec | legacy path or future `planning/slices/client/` | Shared query/model/widget, separate page shells |
| SL-AGR-EXCH-004 | Agreement Exchange Details / Read Details | server read | legacy/current status TBD | SC-13B/13C/13D/13E + missing UI spec | `planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md` | Shared details endpoint first pass |
| L2-AGR-EXCH-DETAILS-001.client | Shared Agreement Exchange Details Pages | client read sidecar | draft/implementation archive exists | SC-13B + missing UI spec | legacy path or future `planning/slices/client/` | Shared details widget, separate page shells |
| SL-AGR-EXCH-005 | Client Accept Active Agreement Proposal | server command | legacy/current status TBD | SC-13B + missing UI spec | `planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md` | Client-only accept |
| L2-AGR-EXCH-ACCEPT-001.client | Client Accept Active Agreement Proposal | client command sidecar | draft/implementation archive exists | SC-13B + missing UI spec | legacy path or future `planning/slices/client/` | Client details action only |
| SL-AGR-EXCH-006 | Final Refuse Agreement Exchange | server command | legacy/current status TBD | SC-13E + missing UI spec | `planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md` | Employee-only final refusal |
| L2-AGR-EXCH-FINAL-REFUSE-001.client | Employee Final Refuse Agreement Exchange | client command sidecar | draft/implementation archive exists | SC-13E + missing UI spec | legacy path or future `planning/slices/client/` | Employee details action only |

## Cross-cutting scenario and slice index

| ID | Title | Source/Draft type | Status | Current path | Notes |
|---|---|---|---|---|---|
| CC-CLIENT-FORM-VALIDATION-001 | Deferred Form Validation | cross-cutting client behavior source | current first pass | `planning/diagrams/scenario-cross-cutting/client-behavior/CC-CLIENT-FORM-VALIDATION-001-deferred-validation.behavior.md` | Source for future `SINGLE-...client.md` draft |
| CC-CLIENT-FEEDBACK-001 | Client Error / Feedback Visibility | cross-cutting client behavior source | current first pass | `planning/diagrams/scenario-cross-cutting/client-behavior/CC-CLIENT-FEEDBACK-001-error-feedback.behavior.md` | Source for feedback behavior used by many UI scenarios |
| CC-SEC-CSRF-001 | Unsafe Command Protection | security/server-client behavior source | current first pass | `planning/diagrams/scenario-cross-cutting/security/CC-SEC-CSRF-001-unsafe-command-protection.behavior.md` | Source for umbrella + server/client slices |
| CC-SEC-CSRF-001 | Unsafe Command Protection | umbrella concern | draft | `planning/slices/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.md` | Coordinates server/client slice drafts and cross-side proof |
| CC-SEC-CSRF-001.server | Unsafe Command Protection Server Slice | server cross-cutting draft | future | `planning/slices/server/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.server.md` | Not created in this archive |
| CC-SEC-CSRF-001.client | Unsafe Command Protection Client Slice | client cross-cutting draft | future | `planning/slices/client/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.client.md` | Not created in this archive |
