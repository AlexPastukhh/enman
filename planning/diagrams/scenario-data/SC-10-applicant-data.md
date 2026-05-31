# SC-10 — Applicant Data DATA

Status: current target DATA spec / applicant-template-per-type model  
Doc version: v0.1.0  
Scope: data entered, seen, selected or referenced by Account page Applicant Parties section

## 1. DATA Blocks

### Applicant Parties section

Visible DATA:

```text
- Applicant Parties section;
- current/default templates grouped by applicant type;
- saved ApplicantParties list/cards;
- selected/default visual marker;
- verification status;
- add ApplicantParty action/form.
```

### Individual ApplicantParty input

Input DATA:

```text
- full name;
- applicant contact email;
- applicant contact phone.
```

### Stable identity

Reference/API DATA:

```text
- ApplicantPartyId;
- applicant type;
- isCurrentDefault;
- verificationStatus.
```

`ApplicantPartyId` is API/implementation support, not a scenario behavior item.

## 2. Accepted Direction

```text
Creating ApplicantParty adds a saved ApplicantParty.
Creating ApplicantParty does not replace existing ApplicantParties.
If no default exists for the type, created party may initialize default.
If default exists for the type, creating another party does not change default implicitly.
```
