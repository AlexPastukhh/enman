# L2 Agreement Scenario / Slice Follow-up Cleanup

Status: current clarification / follow-up after validation source cleanup  
Doc version: v0.1.0  
Scope: remaining agreement scenario and slice navigation drift after the old validation addendum was removed from active source-of-truth use

## 1. Purpose

This file records the follow-up cleanup required so scenario, slice and diagram-prompt sources agree on current L2 Agreement Exchange direction.

It is a clarification guardrail for diagram/documentation work and should be retired once source docs no longer contain stale wording.

## 2. Canonical Agreement Slice Numbering

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Do not use the older numbering where:

```text
003 = generic read
004 = accept
005 = final refusal
```

## 3. Scenario Corrections

```text
SC-13A — Client Agreements:
  Client sees existing agreement exchanges filtered by ClientAccountId.

SC-13B — Client Proposal Details / Response:
  Client actions require client.Id == exchange.ClientAccountId.
  Counter-proposal replacement is SupersededByCounterProposal, not Rejected.

SC-13C — Employee Agreements:
  Employee agreement list shows existing exchanges first pass.
  Approved requests without exchange are started from Employee request details, not from agreement list first pass.

SC-13D — Employee Proposal Create / Send Version:
  Start exchange happens from Employee request details.
  Start stores ClientAccountId from approved request owner.
  Employee new version supersedes active Client proposal with SupersededByCounterProposal.

SC-13E — Final Refusal:
  Employee-only first pass.
  Reason optional; blank provided reason invalid.
  Exchange and Request are separate aggregates orchestrated by application service.

SC-14 — Agreement Documents:
  Current SC-14 means Agreement Documents / AgreementDocumentRef.
  Old Client Data Verification use of SC-14 is stale/deprecated/deferred.
```

## 4. Validation Placement

DTO/request-shape validation belongs in concrete scenario/slice docs:

```text
document/reference required fields;
comment/reason max length;
blank/whitespace handling;
route/query shape.
```

Domain/application invariants stay outside FluentValidation:

```text
client ownership via ClientAccountId;
current exchange status;
whose turn;
active proposal author;
accepted/finally refused cannot continue;
request lifecycle;
cross-aggregate final refusal orchestration.
```

## 5. Diagram Prompt Impact

Diagram chats must read scenario specs, scenario clarifications, scenario questions, slice README and slice source register.

If a stale source conflicts with this file:

```text
1. use the current accepted clarification;
2. mark the affected diagram element with a note or [QUESTION] if not fully resolved;
3. do not silently draw old numbering or stale terminology.
```

## 6. Stale Terms To Search

```text
DocumentFileRef
ReviewerRef
ProposalAttachment
EmployeeRef
ResponsibleEmployeeId
SC-14 Client Data Verification
Rejected as counter-proposal replacement
```
