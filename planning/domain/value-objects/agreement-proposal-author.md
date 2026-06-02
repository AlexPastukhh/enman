# Domain Value Object Draft — AgreementProposalAuthor

Status: draft / extracted with AgreementProposalExchange pilot  
Doc version: v0.1.0  
Scope: proposal author as sender type plus sender id


## 1. Purpose

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
  Internal dependencies:
    - none
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

`AgreementProposalAuthor` protects unambiguous proposal authorship without EmployeeRef, ClientRef or generic AggregateId.

## 2. Source Inputs

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - none
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Scenario / behavior sources:
- `AGR-IBS-001`
- `L2-AGR-AUTHOR-001`
- L2 naming guardrails

Existing domain sources checked in archive:
- `planning/tables/domain-drafts/domain-draft-02.md`
- `Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs`

Not checked:
- Current implementation/test files were not freshly rechecked in this pass.

## 3. Used By

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- `planning/domain/aggregates/agreement-proposal-exchange.md`
- `AgreementProposal.Author`

## 4. Shape / Fields

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Fields:
- `Sender: AgreementProposalSender`
- `SenderId: long`

Optional fields:
- none.

Forbidden fields:
- `EmployeeRef`;
- `ClientRef`;
- generic `AggregateId`.

## 5. Invariants

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invariant | Source | Failure/error |
|---|---|---|
| Sender type is explicit | `L2-AGR-AUTHOR-001` | factory requires Employee or Client path |
| Sender id is positive | implementation / domain draft | constructor rejects non-positive id |
| Proposal cannot exist without author | `AGR-IBS-001` | proposal factory requires author |

## 6. Creation / Normalization Rules

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Invariants
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Creation:
- `AgreementProposalAuthor.Employee(employee)` from Employee domain actor.
- `AgreementProposalAuthor.Client(client)` from ClientAccount.

Normalization:
- none.

Rejected values:
- missing actor;
- non-positive sender id.

## 7. Equality Rule

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Equality is based on:
- `Sender`
- `SenderId`

Identity is not:
- proposal id;
- account profile object reference.

## 8. Validation Boundary

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Belongs in value object:
- sender type + positive sender id.

Belongs in aggregate:
- ensuring Employee can only respond to Client proposal and Client can only respond to Employee proposal.

Belongs in application/auth:
- resolving current Employee/ClientAccount from authenticated account.

## 9. Persistence / Serialization Notes

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- Persist with proposal child entity.
- Use scalar sender id, not domain navigation requirement.

## 10. Invalid Examples

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
    - Validation Boundary
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invalid value/state | Why invalid | Source |
|---|---|---|
| missing Employee/Client | cannot derive sender id | implementation/domain draft |
| `SenderId <= 0` | invalid identity reference | implementation/domain draft |
| generic `AggregateId` | ambiguous author model | L2 naming guardrails |

## 11. Questions / Decisions

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
  Internal dependencies:
    - Source Inputs
    - Invariants
    - Validation Boundary
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Open:
- none for first extraction.

Accepted:
- author uses `Sender + SenderId`;
- do not use `EmployeeRef`, `ClientRef` or `AggregateId`.

## 12. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- Extracted from `planning/tables/domain-drafts/domain-draft-02.md` and the current AgreementProposal implementation sources in the archive.
- Added local section-level `Sources:` blocks in DOM-VO-SRC-ALL-1 without changing domain semantics.
