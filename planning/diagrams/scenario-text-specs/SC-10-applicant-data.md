# SC-10 — Applicant Data

## Status

Corrected scenario specification draft.

## Purpose

Client provides or updates applicant data used for request creation and later agreement processing.

## Actor / Screen

Actor: Client  
Screen: Applicant Data page / applicant section in request creation  
Goal: Provide applicant data

## Entry Points

Entry A: Client opens applicant data page directly.

Entry B: Client reaches applicant data while creating a request.

Entry C [future]: Registration may collect part of applicant data.

## Preconditions

- Client is signed in.
- Applicant data screen or applicant section is reachable.

## DATA

Applicant type selection DATA  
`SC-10-DATA-01`

Selection DATA:

```text
- physical person;
- individual entrepreneur;
- legal entity.
```

Physical person applicant DATA  
`SC-10-DATA-02`

Input DATA:

```text
- ФИО;
- СНИЛС;
- паспортные данные;
- phone;
- email.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- actual/residential address.
```

Individual entrepreneur applicant DATA  
`SC-10-DATA-03`

Input DATA:

```text
- ФИО ИП;
- ИНН;
- ОГРНИП;
- phone;
- email.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- registration address;
- additional ЕГРИП record details if later needed.
```

Legal entity applicant DATA  
`SC-10-DATA-04`

Input DATA:

```text
- organization name;
- ИНН;
- ОГРН;
- phone;
- email.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- КПП;
- legal address;
- representative / signer;
- representative authority basis.
```

Saved applicant visible DATA  
`SC-10-DATA-05`

Visible DATA:

```text
- applicant type;
- applicant display name;
- applicant contact summary;
- applicant identifiers relevant for selected type.
```

## Main Flow

1. Client opens applicant data page or applicant section.
2. Client selects applicant type.
3. Client enters applicant data for selected type.
4. Client-side validation runs automatically.
5. Client corrects applicant data if validation fails.
6. Client saves applicant data.
7. Saved applicant data is visible.
8. Saved applicant data can be reused during request creation.

## Branches

### Physical person applicant

-> applicant type = physical person  
-> client enters physical person applicant data  
-> saved applicant display uses ФИО

### Individual entrepreneur applicant

-> applicant type = individual entrepreneur  
-> client enters ИП applicant data  
-> saved applicant display uses ФИО ИП

### Legal entity applicant

-> applicant type = legal entity  
-> client enters legal entity applicant data  
-> saved applicant display uses organization name

### Applicant data invalid

-> validation errors are visible  
-> client corrects applicant data  
-> back to selected applicant type form

### Applicant data saved

-> applicant data accepted  
-> applicant data is saved  
-> saved applicant data is visible/reusable

## Invariants

Invalid applicant data is not saved.

Attach to:

- save applicant data transition;
- applicant data accepted? branch.

Standalone applicant data editing does not trigger client data verification.

Attach to:

- saved applicant data result;
- verification policy side note.

Verification is available only in request context.

Attach to:

- verification policy side note.

## Step Postconditions

- Applicant data is saved after accepted applicant data.
- Saved applicant data becomes available for request creation.

## Outcomes

- Client can provide applicant data for physical person, individual entrepreneur or legal entity.
- Client sees saved applicant summary.
- Client can reuse saved applicant data in request creation.
- Applicant data editing does not start verification.

## ADR / Policy Candidates

ADR?: Applicant data can be reused by request creation, but request should store a stable applicant reference or future applicant snapshot.

ADR?: Physical person passport data and address are richer than current implemented L1. Current code may keep a narrower first implementation while specs describe target scenario DATA.

ADR?: Verification/check is available only in request context, not from standalone applicant editing.

## Open Questions

Q: Should physical person actual/residential address be target current scenario DATA now, or future only?

Q: Is applicant contact email always the account email, or can it differ?

Q: Is phone required for all applicant types in current implementation?

Q: When applicant data changes after request creation, should historical requests use applicant snapshot?

## Diagram Notes

- Do not label this as Create ApplicantParty.
- Use user-facing wording: Provide applicant data.
- Show applicant type as a selection/branch, not as three unrelated scenarios.
- Use DATA side blocks for type-specific fields.
