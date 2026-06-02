# Domain Value Object Draft — AgreementDocumentRef

Status: draft / extracted with AgreementProposalExchange pilot  
Doc version: v0.1.0  
Scope: metadata reference to an accepted/stored agreement proposal document


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

`AgreementDocumentRef` protects the value integrity of a proposal document reference. It is not the document bytes, not a file upload service and not a storage adapter.

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - none
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Scenario / behavior sources:
- `AGR-VI-001`
- `AGR-IBS-002`
- `L2-AGR-DOC-001`
- `L2-AGR-DOC-002`
- `SC-13D-DATA-01` attachment DATA

Existing domain sources checked in archive:
- `planning/tables/domain-drafts/domain-draft-02.md`
- `Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs`

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- `planning/domain/aggregates/agreement-proposal-exchange.md`
- `AgreementProposal.Document` child state

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Fields:
- `StorageKey`
- `OriginalFileName`
- `ContentType`
- `SizeBytes`

Optional fields:
- none in current direction.

Forbidden fields:
- file bytes;
- storage adapter/service object;
- upload stream.

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invariant | Source | Failure/error |
|---|---|---|
| Storage key is required | Domain Draft 02 / implementation | `AgreementDocumentStorageKeyIsRequired` |
| Original file name is required | Domain Draft 02 / implementation | `AgreementDocumentFileNameIsRequired` |
| Content type is required | Domain Draft 02 / implementation | `AgreementDocumentContentTypeIsRequired` |
| Size must be positive | Domain Draft 02 / implementation | `AgreementDocumentSizeIsRequired` |
| Proposal cannot be created without document ref | `AGR-IBS-002`, `L2-AGR-DOC-001` | proposal creation fails/no-write |

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Invariants
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Creation:
- application/file layer receives uploaded or previously accepted document metadata;
- domain creates `AgreementDocumentRef` from metadata only.

Normalization:
- trim string fields.

Rejected values:
- blank storage key;
- blank original file name;
- blank content type;
- non-positive size.

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Equality is based on:
- `StorageKey`
- `OriginalFileName`
- `ContentType`
- `SizeBytes`

Identity is not:
- DB id;
- file stream identity.

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Belongs in value object:
- required metadata fields;
- positive size;
- metadata trimming.

Belongs in DTO/input validation:
- multipart/form shape;
- allowed upload transport constraints.

Belongs in application/infrastructure:
- storing bytes;
- generating storage key;
- deleting/downloading file.

## 9. Persistence / Serialization Notes

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- Can be persisted as owned/value object data inside proposal child entity.
- Should serialize as metadata reference, not file contents.

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
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
| blank `StorageKey` | no persisted document reference | implementation/domain draft |
| blank `OriginalFileName` | missing user-visible/reference metadata | implementation/domain draft |
| blank `ContentType` | missing document metadata | implementation/domain draft |
| `SizeBytes <= 0` | invalid document metadata | implementation/domain draft |

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
- exact allowed content types/sizes are likely API/application concerns, not this value object.

Accepted:
- this value object is metadata-only;
- it is not bytes/storage adapter.

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
    - Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

- Extracted from `planning/tables/domain-drafts/domain-draft-02.md` and the current AgreementProposal implementation sources in the archive.
- Added local section-level `Sources:` blocks in DOM-VO-SRC-ALL-1 without changing domain semantics.
