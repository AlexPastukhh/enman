# SC-10 — Applicant DATA

## Purpose

Define applicant DATA for applicant profiles used by request creation and later agreement processing.

Target scenario direction:

```text
A client account can store multiple ApplicantParty profiles over time.
For convenience, the account may have one current/default ApplicantParty template per applicant type.
Adding new applicant data creates a new ApplicantParty profile and does not overwrite existing profiles.
```

Current implementation note:

```text
Current L1 implementation is narrower and supports individual applicant create/read behavior.
The multi-type, per-type current/default template model is target scenario planning, not fully implemented code.
```

## DATA Blocks

### SC-10-DATA-01 — Applicant type selection DATA

Type: Selection DATA  
Actor: Client  
Used by: Applicant Data page / applicant section / future My Applicant Parties

Selection DATA:

```text
- physical person;
- individual entrepreneur;
- legal entity.
```

Notes:

```text
Applicant types are alternative data shapes.
Each applicant type may have one current/default template for future prefill.
The account may still store many ApplicantParty profiles over time.
```

### SC-10-DATA-02 — Physical person applicant DATA

Type: Input DATA / Visible DATA  
Actor: Client  
Used by: Physical person applicant form and saved applicant summary

Input DATA:

```text
- ФИО;
- СНИЛС;
- паспортные данные;
- phone;
- email.
```

Visible DATA:

```text
- applicant type = physical person;
- applicant display name = ФИО;
- СНИЛС;
- phone/email contact summary;
- current/default physical person template marker, when set;
- verification status, when available.
```

Current narrow L1 implemented DATA:

```text
- full name;
- email;
- phone number.
```

Target / Future DATA:

```text
[VAR:EXPAND]
- actual/residential address.
```

Notes:

```text
Applicant contact email can differ from account email.
New saved ApplicantParty starts as NotVerified.
```

### SC-10-DATA-03 — Individual entrepreneur applicant DATA

Type: Input DATA / Visible DATA  
Actor: Client  
Used by: Individual entrepreneur applicant form and saved applicant summary

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
- phone/email contact summary;
- current/default individual entrepreneur template marker, when set;
- verification status, when available.
```

Target / Future DATA:

```text
[VAR:EXPAND]
- registration address;
- additional ЕГРИП record details if later needed.
```

Notes:

```text
Use ОГРНИП in DATA.
UI may show “ОГРН/ОГРНИП” if we want wording close to real-world forms.
Applicant contact email can differ from account email.
```

### SC-10-DATA-04 — Legal entity applicant DATA

Type: Input DATA / Visible DATA  
Actor: Client  
Used by: Legal entity applicant form and saved applicant summary

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
- phone/email contact summary;
- current/default legal entity template marker, when set;
- verification status, when available.
```

Target / Future DATA:

```text
[VAR:EXPAND]
- КПП;
- legal address;
- representative / signer;
- representative authority basis.
```

Notes:

```text
Applicant contact email can differ from account email.
```

### SC-10-DATA-05 — Saved applicant profile visible DATA

Type: Visible DATA / Reference DATA  
Actor: Client  
Used by: Account page applicant section, Request Creation applicant context/reuse, future My Applicant Parties

Visible DATA:

```text
- applicant type;
- applicant display name;
- applicant contact summary;
- applicant identifiers relevant for selected type;
- verification state, for example NotVerified / UnderReview / Verified / Rejected or RequiresUpdate;
- current/default marker for the applicant type, when set.
```

Current narrow L1 visible DATA after successful individual applicant creation:

```text
- full name;
- email;
- phone number;
- verificationStatus, when read model exposes it.
```

Reference DATA for request creation:

```text
- selected/suggested ApplicantParty used by the request;
- current/default ApplicantParty template for the selected applicant type;
- future selectable list of all saved ApplicantParty profiles.
```

### SC-10-DATA-06 — Current/default template DATA

Type: Visible DATA / Reference DATA  
Actor: Client  
Used by: Account page and request creation prefill

Visible DATA:

```text
- applicant type;
- currently selected/default ApplicantParty for that type;
- action or marker to make another ApplicantParty current/default for that type.
```

Meaning:

```text
Current/default is a prefill/template marker for future request creation.
It does not rewrite previous requests.
It does not delete older ApplicantParties.
```

## Notes

```text
Do not treat applicant DATA changes as hidden request-local mutation.
New applicant data creates a saved ApplicantParty when accepted.
Future request creation may allow selecting from all saved ApplicantParties using a dropdown, while current/default template remains the initial prefill.
```

Open questions:

```text
Q: Should new ApplicantParty become current/default automatically when there is no current/default profile for that type?
Q: Should edit mutate an unused ApplicantParty in place, or always create a new version?
Q: What deletion/archive rules apply when ApplicantParty is used by requests or approved requests?
Q: Should historical requests show applicant snapshot, stored ApplicantParty version, or both?
Q: When should applicant verification state be shown on Account page and request creation page?
```

Accepted direction:

```text
Account may have many ApplicantParty profiles.
At most one ApplicantParty per type is current/default for future prefill.
New ApplicantParty starts as NotVerified.
Request review verifies applicant data.
Existing ApplicantParties remain stored when a new one is created.
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
```
