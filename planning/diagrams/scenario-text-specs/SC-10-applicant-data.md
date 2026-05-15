# SC-10 — Applicant Data

## Status

Corrected scenario specification draft / current-active ApplicantParty policy synchronized.

## Purpose

Client provides or updates applicant data used for request creation and later agreement processing.

The applicant data represents the account-level applicant profile currently used by request creation.

## Actor / Screen

Actor: Client  
Screen: Applicant Data page / applicant section before request creation  
Goal: Provide or update applicant data

## Entry Points

Entry A: Client opens applicant data page directly.

Entry B: Client reaches applicant data while preparing to create a request.

Entry C [future]: Registration may collect part of applicant data.

Entry D [future]: Client updates/replaces current applicant data before submitting a new request.

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
- applicant identifiers relevant for selected type;
- current active applicant summary for the account.
```

## Main Flow

1. Client opens applicant data page or applicant section.
2. Client selects applicant type.
3. Client enters applicant data for selected type.
4. Client-side validation runs automatically.
5. Client corrects applicant data if validation fails.
6. Client saves applicant data.
7. Saved applicant data becomes the account's current active ApplicantParty.
8. Saved applicant data is visible.
9. Current active applicant data can be reused during request creation.

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
-> saved applicant data becomes current active for the account  
-> saved applicant data is visible/reusable

### Applicant data replaced [future]

-> client updates or replaces applicant data  
-> accepted replacement becomes current active for the account  
-> previously current ApplicantParty is no longer current  
-> future/historical request behavior may use a stable applicant reference or snapshot policy

## Invariants

Invalid applicant data is not saved.

Attach to:

- save applicant data transition;
- applicant data accepted? branch.

One L1 account has one current active ApplicantParty at a time.

Attach to:

- applicant data saved result;
- applicant replacement/versioning policy;
- request creation applicant context.

Applicant types are alternative forms of the account-level applicant profile, not simultaneously active applicant contexts per type.

Attach to:

- applicant type selection;
- applicant replacement/versioning policy.

Standalone applicant data editing does not trigger client data verification.

Attach to:

- saved applicant data result;
- verification policy side note.

Verification is available only in request/review context.

Attach to:

- verification policy side note.

## Step Postconditions

- Applicant data is saved after accepted applicant data.
- Accepted applicant data becomes current active for the account.
- Saved current active applicant data becomes available for request creation.

## Outcomes

- Client can provide applicant data for physical person, individual entrepreneur or legal entity.
- Client sees saved applicant summary.
- Client has one current active applicant profile for the account.
- Client can reuse the current active applicant data in request creation.
- Applicant data editing does not start verification.

## ADR / Policy Candidates

ADR?: Request should store a stable applicant reference or future applicant snapshot so historical requests are not silently changed by later applicant replacement.

ADR?: Physical person passport data and address are richer than current implemented L1. Current code may keep a narrower first implementation while specs describe target scenario DATA.

ADR?: Verification/check is available only in request/review context, not from standalone applicant editing.

## Questions / Decisions

Accepted direction:

```text
Decision:
Current active ApplicantParty is unique per account, not per applicant type.

Reason:
The account has one current applicant context used by request creation.
Applicant types are alternative shapes of that context.

Consequence:
Future applicant replacement/edit work should make the newly accepted ApplicantParty current active and make the old current ApplicantParty inactive/non-current.
```

```text
Decision:
Applicant contact email can differ from account email.

Reason:
Applicant contact details describe the applicant profile, while account email describes authentication/account identity.
```

Open questions:

```text
Q: Should physical person actual/residential address be target current scenario DATA now, or future only?
Q: Is phone required for all applicant types in current implementation?
Q: When applicant data changes after request creation, should historical requests use applicant snapshot?
```

## Diagram Notes

- Do not label this as Create ApplicantParty.
- Use user-facing wording: Provide applicant data.
- Show applicant type as a selection/branch, not as three unrelated scenarios.
- Show current active ApplicantParty as one account-level applicant context.
- Do not draw simultaneously active physical-person, entrepreneur and legal-entity applicant profiles for the same account.
- Use DATA side blocks for type-specific fields.
