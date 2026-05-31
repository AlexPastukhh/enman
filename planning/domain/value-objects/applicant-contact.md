# Domain Value Object Draft — ApplicantContact

Status: draft / first-pass extraction  
Doc version: v0.1.0  
Scope: applicant contact data used by ApplicantParty

## 1. Purpose

`ApplicantContact` represents applicant contact information used by ApplicantParty and request-related flows.

It is intentionally separate from account authentication email.

## 2. Source Inputs

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

Aggregates:

```text
ApplicantParty / IndividualApplicantParty
```

Application/API references:

```text
Applicant party create/edit DTOs and request creation prefill.
```

## 4. Shape / Fields

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

| Invariant | Source | Failure/error |
|---|---|---|
| Applicant contact email is required for IndividualApplicantParty. | implementation/domain draft | email required |
| Applicant phone is required for IndividualApplicantParty. | implementation/domain draft | phone required |
| Applicant contact email is not account auth email. | domain draft | boundary error if mixed |

## 6. Creation / Normalization Rules

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

Equality is based on underlying contact value semantics.

Identity is not:
- account id;
- applicant party id;
- request id.

## 8. Validation Boundary

Belongs in value object/subtype create method:
- email/phone presence and value shape.

Belongs in aggregate/application:
- whether verified applicant contact can be edited.

Belongs in DTO/input validation:
- UI field requiredness and display formatting.

## 9. Persistence / Serialization Notes

Current contact fields are stored on `ApplicantParty`.

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| Applicant party without contact email | cannot satisfy minimum data | implementation/domain draft |
| Applicant party without phone | cannot satisfy minimum data | implementation/domain draft |
| Treating applicant email as account login email | mixes applicant data and auth identity | domain draft |

## 11. Questions / Decisions

Open:
- Should verified applicant contact changes create a new applicant party version later?

Accepted:
- Applicant contact is separate from Account email/auth identity.

## 12. Source Delta / Change Log

```text
- Extracted as first-pass value object draft from ApplicantParty domain draft and current ApplicantParty implementation.
```
