# Slice Index

Status: working index / update as slice docs move

This file is the common navigation index for server, client and cross-cutting slice docs.

Use it while old and new paths coexist during migration.

| Slice ID | Title | Type | Status | Related slice | Current path | Notes |
|---|---|---|---|---|---|---|
| SL-EMP-REQ-001 | Employee Request List Read | server read | legacy/current status TBD | L2-EMP-DASH-001.client | `planning/slices/SL-EMP-REQ-001-employee-request-list-read.md` | Historical location; move/update later |
| SL-EMP-REQ-002 | Employee Request Details Read | server read | legacy/current status TBD | L2-EMP-DETAILS-001.client | `planning/slices/SL-EMP-REQ-002-employee-request-details-read.md` | Historical location; move/update later |
| SL-EMP-REQ-003 | Start Request Review | server command | legacy/current status TBD | L2-REVIEW-START-001.client | `planning/slices/SL-EMP-REQ-003-start-request-review.md` | StartReview has dashboard/details entry points |
| SL-EMP-REQ-004 | Approve Request Review | server command | legacy/current status TBD | L2-REVIEW-APPROVE-001.client | `planning/slices/SL-EMP-REQ-004-approve-request-review.md` | Details-only first pass |
| SL-EMP-REQ-005 | Reject Request Review | server command | legacy/current status TBD | L2-REVIEW-REJECT-001.client | `planning/slices/SL-EMP-REQ-005-reject-request-review.md` | Feedback optional |
| L2-EMP-DASH-001.client | Employee Request Dashboard | client read sidecar | legacy/current status TBD | SL-EMP-REQ-001 | `planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md` | Legacy location; update with new template later |
| L2-EMP-DETAILS-001.client | Employee Request Details | client read sidecar | legacy/current status TBD | SL-EMP-REQ-002 | `planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md` | Legacy location; update with new template later |
| L2-REVIEW-START-001.client | Start Request Review | client command sidecar | legacy/current status TBD | SL-EMP-REQ-003 | `planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md` | Legacy location; update with new template later |
| L2-REVIEW-APPROVE-001.client | Approve Request Review | client command sidecar | legacy/current status TBD | SL-EMP-REQ-004 | `planning/slices/l2/L2-REVIEW-APPROVE-001-approve-request-review.client.md` | Legacy location; update with new template later |
| L2-REVIEW-REJECT-001.client | Reject Request Review | client command sidecar | legacy/current status TBD | SL-EMP-REQ-005 | `planning/slices/l2/L2-REVIEW-REJECT-001-reject-request-review.client.md` | Legacy location; update with new template later |
| SL-AGR-EXCH-001 | Start Agreement Exchange With Initial Employee Proposal | server command | legacy/current status TBD | L2-AGR-EXCH-START-001.client | `planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | Creates exchange and initial Employee proposal |
| L2-AGR-EXCH-START-001.client | Start Agreement Exchange With Initial Employee Proposal | client command sidecar | draft/implementation archive exists | SL-AGR-EXCH-001 | legacy path or future `planning/slices/client/` | Employee request details action |
| SL-AGR-EXCH-002 | Send Agreement Counter-Proposal Version | server command | legacy/current status TBD | L2-AGR-EXCH-SEND-PROPOSAL-001.client | `planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | Existing exchange proposal version |
| L2-AGR-EXCH-SEND-PROPOSAL-001.client | Send Agreement Proposal Version | client command sidecar | draft/implementation archive exists | SL-AGR-EXCH-002 | legacy path or future `planning/slices/client/` | Shared Client/Employee details action |
| SL-AGR-EXCH-003 | Agreement Exchange List Page / Read List | server read | legacy/current status TBD | L2-AGR-EXCH-LIST-001.client | `planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md` | Shared list endpoint first pass |
| L2-AGR-EXCH-LIST-001.client | Shared Agreement Exchange List Pages | client read sidecar | draft/implementation archive exists | SL-AGR-EXCH-003 | legacy path or future `planning/slices/client/` | Shared query/model/widget, separate page shells |
| SL-AGR-EXCH-004 | Agreement Exchange Details / Read Details | server read | legacy/current status TBD | L2-AGR-EXCH-DETAILS-001.client | `planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md` | Shared details endpoint first pass |
| L2-AGR-EXCH-DETAILS-001.client | Shared Agreement Exchange Details Pages | client read sidecar | draft/implementation archive exists | SL-AGR-EXCH-004 | legacy path or future `planning/slices/client/` | Shared details widget, separate page shells |
| SL-AGR-EXCH-005 | Client Accept Active Agreement Proposal | server command | legacy/current status TBD | L2-AGR-EXCH-ACCEPT-001.client | `planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md` | Client-only accept |
| L2-AGR-EXCH-ACCEPT-001.client | Client Accept Active Agreement Proposal | client command sidecar | draft/implementation archive exists | SL-AGR-EXCH-005 | legacy path or future `planning/slices/client/` | Client details action only |
| SL-AGR-EXCH-006 | Final Refuse Agreement Exchange | server command | legacy/current status TBD | L2-AGR-EXCH-FINAL-REFUSE-001.client | `planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md` | Employee-only final refusal |
| L2-AGR-EXCH-FINAL-REFUSE-001.client | Employee Final Refuse Agreement Exchange | client command sidecar | draft/implementation archive exists | SL-AGR-EXCH-006 | legacy path or future `planning/slices/client/` | Employee details action only |
| CLIENT-UI-STYLE-WORKFLOW | Client UI / Style Workflow | client rule | current | all client sidecars | `planning/slices/client/CLIENT-UI-STYLE-WORKFLOW.md` | Flow-based header and staged UI cleanup |
| CLIENT-CSS-ARCHITECTURE-RULES | Client CSS Architecture Rules | client rule | current | all client sidecars | `planning/slices/client/CLIENT-CSS-ARCHITECTURE-RULES.md` | CSS as slice ownership |
