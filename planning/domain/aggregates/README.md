# Domain Aggregates Index

Status: current aggregate draft folder index  
Scope: one file per aggregate boundary

## 1. Purpose

This folder contains one draft per aggregate boundary.

An aggregate draft owns:

```text
aggregate root;
child entities;
owned state;
domain methods/commands;
invariants;
lifecycle/state machine;
impossible states;
value objects used;
cross-aggregate references;
behavior coverage.
```

## 2. Current Drafts

| File | Aggregate root | Status | Notes |
|---|---|---|---|
| `account.md` | `Account` | draft / first-pass extraction | Extracted from account/client/employee hierarchy decision and current Account/ClientAccount/Employee implementation sources. |
| `applicant-party.md` | `ApplicantParty` | draft / first-pass extraction | Extracted from ApplicantParty-centric domain drafts, applicant scenarios and current ApplicantParty implementation sources. |
| `connection-request.md` | `ConnectionRequest` | draft / extraction pilot | Extracted from SC-04 request creation, SC-07B request review, Domain Draft 02 and current Request implementation sources. |
| `agreement-proposal-exchange.md` | `AgreementProposalExchange` | draft / extraction pilot | Extracted from SC-13D/L2 agreement behavior, Domain Draft 02 and current AgreementProposal implementation sources. |

## 3. Historical Source Snapshots

Use historical source snapshots while validating or extending aggregate drafts:

```text
planning/tables/domain-drafts/domain-draft-01.md
planning/tables/domain-drafts/domain-draft-02.md
planning/domain/scenario-to-aggregate-map.md
```

## 4. Drafting

Use:

```text
planning/domain/aggregate-drafting-workflow.md
planning/domain/aggregate-draft-template.md
planning/domain/domain-modeling-principles.md
```

## 5. Guardrails

```text
One file should describe one aggregate boundary.
Child entities stay in the owning aggregate draft.
External aggregates are referenced, not owned.
Do not create aggregate drafts without source-backed behavior evidence.
```
