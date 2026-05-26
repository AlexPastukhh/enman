# Scenario To Aggregate Map

Status: draft scaffold / domain discovery bridge  
Scope: scenario behavior sources -> domain aggregate/value-object discovery

## 1. Purpose

This file maps scenario-layer behavior sources to domain aggregate candidates, value object candidates, cross-aggregate relations and next domain draft files.

This file is not the final domain model. It is the domain discovery bridge.

## 2. Source Model

Primary sources:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
planning/diagrams/scenario-questions-register.md
```

Historical / cross-check sources:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/domain-drafts/domain-draft-01.md
planning/tables/domain-drafts/domain-draft-02.md
planning/domain-model.md
planning/domain-design-input-navigation-notes.md
planning/scenario-domain-validation-principles.md
```

Not checked:

```text
Full scenario-by-scenario discovery pass is not done yet.
```

## 3. Behavior Item Category Mapping

| Category | Domain interpretation | Output |
|---|---|---|
| CMD | Command behavior | Candidate aggregate method / domain command / application command |
| LC | Lifecycle / state / condition behavior | State machine / lifecycle rule |
| IBS | Impossible business state | Invariant / impossible state |
| VI | Value integrity | Value object candidate / value validation rule |
| UCQ | Use-case coordination | Cross-aggregate relation / application coordination |
| READ | Read/list/access behavior | Read model / query / access placement |
| INT | Infrastructure/integration expectation | Out-of-domain note / app/infrastructure/testing note |
| FUT | Future/deferred behavior | Deferred note |
| NW | No-write / failure preservation | Failure/no-write proof requirement |

## 4. Scenario-To-Aggregate Matrix

| Scenario / behavior source | Key behavior categories | Aggregate candidates | Value object candidates | Coordination notes | Status |
|---|---|---|---|---|---|
| TBD | TBD | TBD | TBD | TBD | discovery pending |

## 5. Aggregate Candidate Register

| Aggregate candidate | Root | Child entities | Source scenarios/items | Draft file | Status |
|---|---|---|---|---|---|
| AgreementProposalExchange | AgreementProposalExchange | AgreementProposal, AgreementProposalVersion and related local records | L2 agreement / SC-13D sources | `planning/domain/aggregates/agreement-proposal-exchange.md` | pilot candidate |

## 6. Value Object Candidate Register

| Value object candidate | Source VI items | Used by aggregates | Draft file | Status |
|---|---|---|---|---|
| AgreementDocumentRef | TBD | AgreementProposalExchange | `planning/domain/value-objects/agreement-document-ref.md` | candidate |
| ProposalComment | TBD | AgreementProposalExchange | `planning/domain/value-objects/proposal-comment.md` | candidate |
| FinalRefusalReason | TBD | AgreementProposalExchange | `planning/domain/value-objects/final-refusal-reason.md` | candidate |

## 7. Cross-Aggregate Relations

| From aggregate | To aggregate | Relation | Owner / coordination boundary | Notes |
|---|---|---|---|---|
| AgreementProposalExchange | Request / ConnectionRequest | Exchange exists for approved request | Application coordination | Exchange should not directly mutate Request unless an accepted decision says otherwise. |

## 8. Application Coordination Notes

Rules that should not be owned by a single aggregate:

```text
TBD during discovery.
```

Rules that are likely aggregate-owned:

```text
TBD during aggregate drafting.
```

Rules intentionally outside domain:

```text
TBD during discovery.
```

## 9. Discovery Questions

Open:

```text
- Which aggregate should be extracted first after scaffold? Current preferred pilot: AgreementProposalExchange.
- Which value object candidates are reusable/non-trivial enough for separate files?
- Which old domain draft decisions should move into planning/domain/decisions/ first?
```

Accepted:

```text
- New target model is aggregate-based.
- Old monolithic domain drafts are historical discovery snapshots until extracted.
```

## 10. Next Aggregate Drafts To Create

Priority:

```text
1. AgreementProposalExchange
2. Request / ConnectionRequest
3. ApplicantParty
4. Account / ClientAccount / Employee
```

Blocked:

```text
Full source/version/cascade alignment is deferred.
```

## 11. Source Delta / Change Log

```text
- Created as scaffold before full scenario-by-scenario discovery pass.
```
