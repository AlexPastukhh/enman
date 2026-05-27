# Domain Value Object Draft — ApplicantIdentity

Status: draft / first-pass extraction  
Scope: applicant identity data used by ApplicantParty

## 1. Purpose

`ApplicantIdentity` represents applicant identity data needed to identify an applicant party in domain flows.

Current concrete coverage is individual applicant identity.

## 2. Source Inputs

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

Aggregates:

```text
ApplicantParty / IndividualApplicantParty
```

Application/API references:

```text
Applicant party create/edit DTOs and request creation prefill.
```

## 4. Shape / Fields

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

| Invariant | Source | Failure/error |
|---|---|---|
| Individual applicant identity requires full name. | implementation/domain draft | first name/full name required error |
| Applicant identity is separate from account identity. | domain draft | avoid mixing Account.Email with ApplicantParty contact/identity data |

## 6. Creation / Normalization Rules

Creation:
- full name must be present for IndividualApplicantParty.

Normalization:
- follows existing `FullName` implementation.

Rejected values:
- missing full name.

## 7. Equality Rule

Current equality rule follows underlying implementation value object semantics.

Identity is not:
- account id;
- request id;
- applicant party database id.

## 8. Validation Boundary

Belongs in value object / subtype create method:
- full name presence and shape.

Belongs in aggregate/application:
- whether the applicant party can be edited or verified.

Belongs in DTO/input validation:
- input formatting and UI-required fields before domain construction.

## 9. Persistence / Serialization Notes

Current identity is embedded in `IndividualApplicantParty` through `FullName`.

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| Individual applicant without full name | minimum identity data missing | implementation/domain draft |

## 11. Questions / Decisions

Open:
- When do entrepreneur/legal-entity identity value objects become current?
- Should applicant identity be versioned when verified applicant data changes later?

Accepted:
- Current first-pass identity is individual full name.

## 12. Source Delta / Change Log

```text
- Extracted as first-pass value object draft from ApplicantParty domain draft and current IndividualApplicantParty implementation.
```
