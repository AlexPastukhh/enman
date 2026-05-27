# Domain Drafts Index

Status: historical / transitional monolithic domain discovery snapshots  
Scope: old whole-domain draft files used as source material for aggregate-based extraction

## 1. Purpose

This folder stores older monolithic domain drafts.

A monolithic domain draft is a complete snapshot of domain understanding at the time it was written.

The current target model is no longer one large domain draft per iteration. New domain work should use:

```text
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/domain-discovery-workflow.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/domain-model-overview.md
planning/domain/aggregate-drafting-workflow.md
planning/domain/value-object-drafting-workflow.md
```

## 2. How To Use These Files

Use these files as:

```text
historical discovery snapshots;
source material for aggregate extraction;
cross-checks for scenario-to-aggregate mapping;
source material for value object candidates;
source material for domain decisions.
```

Do not treat them as the current target shape for new domain docs.

## 3. Inputs Used By Historical Drafts

Historical drafts used sources such as:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
planning/domain-draft-generation-guide.md
```

Current domain discovery should prefer per-scenario behavior items where available.

## 4. Current Files

| File | Status | Purpose |
|---|---|---|
| `domain-draft-01.md` | historical monolithic discovery snapshot / partially extracted | ApplicantParty-centric / request-related domain model snapshot. Useful content is now partially extracted into `planning/domain/aggregates/applicant-party.md`, `planning/domain/aggregates/connection-request.md`, applicant value-object drafts and scenario-to-aggregate map. |
| `domain-draft-02.md` | historical monolithic discovery snapshot / partially extracted | Later domain model snapshot with account/employee, request and agreement proposal exchange direction. Useful content is now partially extracted into `planning/domain/aggregates/account.md`, `planning/domain/aggregates/connection-request.md`, `planning/domain/aggregates/agreement-proposal-exchange.md` and related value-object drafts. |
| `domain-draft-02-account-employee-tph-decision.md` | extracted historical decision source | Extracted into `planning/domain/decisions/account-employee-tph-decision.md`; keep source file for traceability. |

## 5. Migration Direction

Current direction:

```text
old monolithic drafts
  -> scenario-to-aggregate-map.md
  -> one aggregate draft per aggregate boundary
  -> value object drafts for reusable/non-trivial value concepts
  -> decisions/ for accepted domain decisions
  -> domain-model-overview.md for high-level current map
```

Do not delete or move historical drafts until their useful content is extracted or linked from current domain files.

## 6. Future Cleanup

After aggregate extraction stabilizes, decide whether to:

```text
- keep this folder as historical archive;
- move old drafts into an archive subfolder;
- extract more accepted decisions into planning/domain/decisions/;
- update or remove stale references from old drafts.
```
