# Domain Value Object Draft — FinalRefusalReason

Status: draft / extracted with AgreementProposalExchange pilot  
Doc version: v0.1.0  
Scope: optional explanation for Employee final refusal of agreement exchange


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

`FinalRefusalReason` protects bounded non-empty text when a final refusal reason is provided. The final refusal itself is exchange state, not a separate entity.

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - none
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Scenario / behavior sources:
- `L2-AGR-FINAL-001`
- `L2-AGR-FINAL-002`
- `L2-AGR-FINAL-003`
- L2 domain direction clarification

Existing domain sources checked in archive:
- `planning/tables/domain-drafts/domain-draft-02.md`
- `Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs`

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- `planning/domain/aggregates/agreement-proposal-exchange.md`
- `AgreementProposalExchange.FinalRefusalReason?`

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Fields:
- `Value`

Optional fields:
- none inside the value object.

Forbidden fields:
- refusing employee id;
- refused timestamp;
- proposal version id.

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invariant | Source | Failure/error |
|---|---|---|
| Reason is non-empty when present | implementation / domain draft | `FinalRefusalReasonIsRequired` |
| Reason max length is 2000 | implementation / domain draft | `FinalRefusalReasonIsTooLong` |

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Invariants
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Creation:
- create only when caller has non-blank final refusal reason.

Normalization:
- trim text.

Rejected values:
- blank value;
- value longer than max length.

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Equality is based on:
- normalized `Value`

Identity is not:
- final refusal event id;
- employee id.

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Belongs in value object:
- reason text integrity.

Belongs in aggregate:
- whether final refusal is allowed;
- who refused;
- when refusal happened;
- status transition to `FinallyRefused`.

Belongs in application:
- orchestrating Request `AgreementExchangeFailed` after exchange final refusal.

## 9. Persistence / Serialization Notes

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- Persist as optional value object on `AgreementProposalExchange` final-refusal state.
- Do not create a separate `AgreementFinalRefusal` entity/class for current target direction.

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
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
| blank reason passed to factory | present reason must be non-empty | implementation/domain draft |
| length > 2000 | exceeds domain max length | implementation/domain draft |

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
- whether UI/API requires reason is not owned by this value object.

Accepted:
- reason is optional on aggregate state;
- final refusal does not create proposal version;
- no separate final refusal entity/class.

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
    - Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- Extracted from `planning/tables/domain-drafts/domain-draft-02.md` and the current AgreementProposal implementation sources in the archive.
- Added local section-level `Sources:` blocks in DOM-VO-SRC-ALL-1 without changing domain semantics.
