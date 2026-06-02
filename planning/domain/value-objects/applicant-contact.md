# Domain Value Object Draft — ApplicantContact

Status: draft / first-pass extraction  
Doc version: v0.1.0  
Scope: applicant contact data used by ApplicantParty


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
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

`ApplicantContact` represents applicant contact information used by ApplicantParty and request-related flows.

It is intentionally separate from account authentication email.

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
    - Domain.EnergyManagement/Applicants/ApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - none
  Not checked:
    - full contact editing/versioning behavior
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
Domain.EnergyManagement/Applicants/ApplicantParty.cs
Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs
Domain.EnergyManagement/DocumentManaging/Email.cs
Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs
```

Not checked:

```text
Full contact editing/versioning behavior.
```

## 3. Used By

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Applicants/ApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
  Not checked:
    - full contact editing/versioning behavior
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
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
    - Used By
  Not checked:
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Current fields:

```text
Email
PhoneNumber
```

Optional fields:

```text
none in current core
```

Forbidden fields:

```text
Account password/auth credentials
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
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invariant | Source | Failure/error |
|---|---|---|
| Applicant contact email is required for IndividualApplicantParty. | implementation/domain draft | email required |
| Applicant phone is required for IndividualApplicantParty. | implementation/domain draft | phone required |
| Applicant contact email is not account auth email. | domain draft | boundary error if mixed |

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
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Invariants
  Not checked:
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Creation:
- contact email must be present;
- phone number must be present.

Normalization:
- follows underlying `Email` and `PhoneNumber` value object implementations.

Rejected values:
- missing contact email;
- missing phone number;
- invalid email/phone shape by underlying value object rules.

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
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Equality is based on underlying contact value semantics.

Identity is not:
- account id;
- applicant party id;
- request id.

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
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
  Not checked:
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Belongs in value object/subtype create method:
- email/phone presence and value shape.

Belongs in aggregate/application:
- whether verified applicant contact can be edited.

Belongs in DTO/input validation:
- UI field requiredness and display formatting.

## 9. Persistence / Serialization Notes

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Applicants/ApplicantParty.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Used By
  Not checked:
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Current contact fields are stored on `ApplicantParty`.

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
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
    - Validation Boundary
  Not checked:
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invalid value/state | Why invalid | Source |
|---|---|---|
| Applicant party without contact email | cannot satisfy minimum data | implementation/domain draft |
| Applicant party without phone | cannot satisfy minimum data | implementation/domain draft |
| Treating applicant email as account login email | mixes applicant data and auth identity | domain draft |

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
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

Open:
- Should verified applicant contact changes create a new applicant party version later?

Accepted:
- Applicant contact is separate from Account email/auth identity.

## 12. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/domain/aggregates/applicant-party.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - Domain.EnergyManagement/DocumentManaging/Email.cs @ implementation evidence from prior extraction/source pass, version not applicable
    - Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - full contact editing/versioning behavior
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

```text
- Extracted as first-pass value object draft from ApplicantParty domain draft and current ApplicantParty implementation.
- Added local section-level Sources blocks in DOM-VO-SRC-ALL-1 without changing domain semantics.
```
