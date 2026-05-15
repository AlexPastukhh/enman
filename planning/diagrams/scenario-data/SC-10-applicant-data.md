# SC-10 — Applicant DATA

## Purpose

Define applicant DATA for the applicant types used by request creation and later agreement processing.

Applicant DATA is account-level. One L1 account has one current active ApplicantParty at a time.

## DATA Blocks

### SC-10-DATA-01 — Applicant type selection DATA

Type: Selection DATA  
Actor: Client  
Used by: Applicant Data page / applicant section

Selection DATA:

```text
- physical person;
- individual entrepreneur;
- legal entity.
```

Notes:

```text
Applicant types are alternative data shapes for the account-level applicant profile.
They are not separate simultaneously-active applicant contexts for the same account.
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
- phone/email contact summary.
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
Current implemented L1 already includes FullName, Email and PhoneNumber.
PassportData and ActualAddress are richer target/future applicant DATA relative to current narrow implementation.
Applicant contact email can differ from account email.
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
- phone/email contact summary.
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
- phone/email contact summary.
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

### SC-10-DATA-05 — Saved / current applicant visible DATA

Type: Visible DATA / Reference DATA  
Actor: Client  
Used by: Account page applicant section, Applicant Data page, Request Creation applicant context/reuse

Visible DATA:

```text
- current active applicant marker/summary;
- applicant type;
- applicant display name;
- applicant contact summary;
- applicant identifiers relevant for selected type;
- saved applicant data shown as read-only when current applicant data exists.
```

Current narrow L1 visible DATA after successful individual applicant creation:

```text
- full name;
- email;
- phone number.
```

Future visible DATA when read/verification model exists:

```text
[VAR:EXPAND]
- applicant verification state, for example:
  - Not verified;
  - Under review / pending verification;
  - Verified;
  - Rejected / requires update.
```

Reference DATA for request creation:

```text
- current active ApplicantParty for the account.
```

Notes:

```text
The applicant create UI does not introduce the create-request entry point.
The exact request-creation entry location is a future client-slice decision.
```

Open questions:

```text
Q: Should physical person actual/residential address become current target scenario DATA or stay future?
Q: Is phone required for all applicant types in current implementation?
Q: Should historical request creation store applicant snapshot later?
Q: What exact current-applicant read model should the Account page use after refresh?
Q: When should applicant verification state be shown on the Account page?
```

Accepted direction:

```text
Current active ApplicantParty is unique per account.
Applicant contact email can differ from account email.
Applicant creation success can make submitted applicant data visible as read-only local UI state until a current-applicant read model exists.
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
```
