# Manifest — L2 Agreement Exchange Invariants / Draft Rules Sync

## New / replacement files

```text
planning/slices/l2/L2-agreement-exchange-domain-invariants-and-actor-access.md
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md
planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md
planning/slices/drafting-current-state-source-rule.md
planning/slices/drafting-implementation-checklist-rule.md
planning/slices/slice-draft-file-naming-and-placement.md
```

## Apply script updates navigation/registers

```text
planning/README.md
planning/planning-agent-protocol.md
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/l2/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

## Key decisions

```text
- Current-state questions use GitHub/current repo, not handoff archives.
- Full server/client drafts require near-end Implementation Checklist.
- SL-AGR-EXCH-001 has concrete implementation checklist with UnitResult, no per-command enum, separate controller, repository, CSRF and OpenAPI generation.
- AgreementProposalExchange stores ClientAccountId.
- Client actions check client.Id == ClientAccountId.
- No ResponsibleEmployeeId guard first pass.
- Any active Employee can service exchange first pass.
- Proposal authors are tracked per proposal version.
- Handler/controller stay thin.
- Application service branches by actor and calls actor-specific domain methods.
- Read services use actor-specific access filters.
```
