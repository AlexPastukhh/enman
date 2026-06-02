# Domain Value Object Draft — ProposalComment

Status: draft / extracted with AgreementProposalExchange pilot  
Doc version: v0.1.0  
Scope: optional proposal text/comment attached to a proposal version


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

`ProposalComment` protects non-empty bounded text when a proposal comment is provided. Missing comment is represented by `null` on the proposal.

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - none
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Scenario / behavior sources:
- `AGR-VI-002`
- `L2-AGR-COMMENT-001`
- `SC-13D-DATA-01` input DATA

Existing domain sources checked in archive:
- `planning/tables/domain-drafts/domain-draft-02.md`
- `Domain.EnergyManagement/AgreementProposals/ProposalComment.cs`

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- `planning/domain/aggregates/agreement-proposal-exchange.md`
- `AgreementProposal.Comment?`

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
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
- request details;
- document metadata;
- sender identity.

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invariant | Source | Failure/error |
|---|---|---|
| Comment is non-empty when present | `L2-AGR-COMMENT-001` / implementation | `ProposalCommentIsRequired` |
| Comment max length is 2000 | implementation / domain draft | `ProposalCommentIsTooLong` |

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Invariants
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Creation:
- create only when caller has non-blank text.

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
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
- proposal id;
- sender id.

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Belongs in value object:
- non-empty when present;
- max length;
- trimming.

Belongs in DTO/input validation:
- raw form field presence/shape.

Belongs in aggregate/application:
- whether a missing comment is acceptable for a given command/scenario.

## 9. Persistence / Serialization Notes

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- Persist as optional owned/string value on proposal version/child.
- `null` means no comment; empty string should be normalized at application boundary and not stored as a value object.

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
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
| blank value passed to `ProposalComment.Create` | value object represents present comment; present comment must be non-empty | implementation/domain draft |
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
- `Q-SC-13D-002` still asks whether proposal text details/comment are required or optional.

Accepted:
- current domain direction treats comment as optional;
- if present, it must be non-empty and max-length constrained.

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
    - Domain.EnergyManagement/AgreementProposals/ProposalComment.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- Extracted from `planning/tables/domain-drafts/domain-draft-02.md` and the current AgreementProposal implementation sources in the archive.
- Added local section-level `Sources:` blocks in DOM-VO-SRC-ALL-1 without changing domain semantics.
