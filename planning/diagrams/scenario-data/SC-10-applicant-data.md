# SC-10 — Applicant DATA

Status: target DATA spec / synchronized with ApplicantParty template-per-type model  
Scope: applicant DATA entered, seen, selected and referenced from Account page / Applicant Parties section

DATA means what the actor enters, sees, selects, filters, attaches or references.

Validation, invariants, access rules and implementation mechanics do not belong in this file.

## 1. DATA Blocks

### SC-10-DATA-01 — Applicant type selection DATA

Type: Selection DATA  
Actor: Client  
Used by: Account page Applicant Parties section, request creation applicant section, future My Applicant Parties

Selection DATA:

```text
- physical person;
- individual entrepreneur;
- legal entity.
```

Notes:

```text
Target scenario allows one current/default template per applicant type.
Applicant types are not mutually exclusive account-wide; an account may store applicant profiles of different types.
```

### SC-10-DATA-02 — Physical person applicant DATA

Type: Input DATA / Visible DATA  
Actor: Client  
Used by: physical person applicant form and saved applicant summary

Input DATA:

```text
- ФИО;
- СНИЛС;
- паспортные данные;
- phone;
- email.
```

Current narrow L1 implemented DATA:

```text
- full name;
- email;
- phone number.
```

Visible DATA:

```text
- applicant type = physical person;
- applicant display name = ФИО / full name;
- contact summary;
- current/default marker if selected as physical-person template;
- verification state when exposed.
```

Target / Future DATA:

```text
[VAR:EXPAND]
- actual/residential address.
```

### SC-10-DATA-03 — Individual entrepreneur applicant DATA

Type: Input DATA / Visible DATA  
Actor: Client  
Used by: individual entrepreneur applicant form and saved applicant summary

Input DATA:

```text
- ФИО ИП;
- ИНН;
- ОГРНИП;
- phone;
- email.
```

Visible DATA:

```text
- applicant type = individual entrepreneur;
- applicant display name = ФИО ИП;
- ИНН;
- ОГРНИП;
- contact summary;
- current/default marker if selected as entrepreneur template;
- verification state when exposed.
```

Target / Future DATA:

```text
[VAR:EXPAND]
- registration address;
- additional ЕГРИП record details if later needed.
```

### SC-10-DATA-04 — Legal entity applicant DATA

Type: Input DATA / Visible DATA  
Actor: Client  
Used by: legal entity applicant form and saved applicant summary

Input DATA:

```text
- organization name;
- ИНН;
- ОГРН;
- phone;
- email.
```

Visible DATA:

```text
- applicant type = legal entity;
- applicant display name = organization name;
- ИНН;
- ОГРН;
- contact summary;
- current/default marker if selected as legal-entity template;
- verification state when exposed.
```

Target / Future DATA:

```text
[VAR:EXPAND]
- КПП;
- legal address;
- representative / signer;
- representative authority basis.
```

### SC-10-DATA-05 — Saved ApplicantParties / current-default templates DATA

Type: Visible DATA / Reference DATA / Selection DATA  
Actor: Client  
Used by: Account page Applicant Parties section, future My Applicant Parties, request creation prefill/selection

Visible DATA:

```text
- saved ApplicantParty list/cards;
- ApplicantPartyId as stable internal/reference identity when needed by client state or future actions;
- applicant type;
- applicant display name;
- contact summary;
- identifiers relevant for selected type;
- verification state, for example NotVerified / UnderReview / Verified / RejectedRequiresUpdate;
- current/default template marker for the applicant type;
- non-default saved ApplicantParties remain visible.
```

Reference DATA for request creation:

```text
- current/default ApplicantParty for selected applicant type;
- selected existing ApplicantParty, when future dropdown/list selection is introduced;
- newly created ApplicantParty from request journey.
```

Notes:

```text
Current/default is a prefill/default-selection concept.
It is not deletion or replacement of older ApplicantParties.
A dedicated details page is not required by current scenario direction because applicant details can be displayed inline.
```

## 2. Questions

```text
Q: Should delete mean hard delete, archive, deactivate or hide?
Q: Can used ApplicantParties be edited in place, or should edit create a new version/profile?
Q: Should request details display applicant reference only, applicant snapshot, or both?
Q: What exact verification states should be visible to the client?
```

## 3. Accepted Direction

```text
A client account may store many ApplicantParties.
One current/default template may exist per applicant type.
Creating the first ApplicantParty of a type initializes the default for that type.
Creating another ApplicantParty of the same type leaves the existing default unchanged.
Changing default when one already exists is a separate explicit behavior.
New ApplicantParty starts NotVerified.
```

## 4. Scenario Spec References

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
```
