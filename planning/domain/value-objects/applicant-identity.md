# Domain Value Object Draft — ApplicantIdentity

Status: draft / first-pass extraction  
Doc version: v0.1.0  
Scope: applicant identity data used by ApplicantParty


## 1. Purpose

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
  Internal dependencies:
    - none
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

`ApplicantIdentity` represents applicant identity data needed to identify an applicant party in domain flows.

Current concrete coverage is individual applicant identity.

## 2. Source Inputs

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - planning/diagrams/scenario-text-specs/SC-10-applicant-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-data/SC-10-applicant-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - none
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Scenario/domain sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/tables/domain-drafts/domain-draft-01.md
Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs
Domain.EnergyManagement/DocumentManaging/FullName.cs
```

Not checked:

```text
Future legal entity / entrepreneur applicant identity sources.
```

## 3. Used By

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Aggregates:

```text
ApplicantParty / IndividualApplicantParty
```

Application/API references:

```text
Applicant party create/edit DTOs and request creation prefill.
```

## 4. Shape / Fields

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
    - Used By
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Current individual applicant identity:

```text
FullName
```

Future candidate fields:

```text
entrepreneur identifiers
legal entity identifiers
```

## 5. Invariants

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invariant | Source | Failure/error |
|---|---|---|
| Individual applicant identity requires full name. | implementation/domain draft | first name/full name required error |
| Applicant identity is separate from account identity. | domain draft | avoid mixing Account.Email with ApplicantParty contact/identity data |

## 6. Creation / Normalization Rules

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Invariants
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Creation:
- full name must be present for IndividualApplicantParty.

Normalization:
- follows existing `FullName` implementation.

Rejected values:
- missing full name.

## 7. Equality Rule

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Current equality rule follows underlying implementation value object semantics.

Identity is not:
- account id;
- request id;
- applicant party database id.

## 8. Validation Boundary

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Belongs in value object / subtype create method:
- full name presence and shape.

Belongs in aggregate/application:
- whether the applicant party can be edited or verified.

Belongs in DTO/input validation:
- input formatting and UI-required fields before domain construction.

## 9. Persistence / Serialization Notes

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Used By
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Current identity is embedded in `IndividualApplicantParty` through `FullName`.

## 10. Invalid Examples

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
    - Validation Boundary
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invalid value/state | Why invalid | Source |
|---|---|---|
| Individual applicant without full name | minimum identity data missing | implementation/domain draft |

## 11. Questions / Decisions

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
  Internal dependencies:
    - Source Inputs
    - Invariants
    - Validation Boundary
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Open:
- When do entrepreneur/legal-entity identity value objects become current?
- Should applicant identity be versioned when verified applicant data changes later?

Accepted:
- Current first-pass identity is individual full name.

## 12. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/FullName.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - future legal entity / entrepreneur applicant identity sources
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

```text
- Extracted as first-pass value object draft from ApplicantParty domain draft and current IndividualApplicantParty implementation.
- Added local section-level Sources blocks in DOM-VO-SRC-ALL-1 without changing domain semantics.
```
