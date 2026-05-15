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

### SC-10-DATA-05 — Saved applicant visible DATA

Type: Visible DATA / Reference DATA  
Actor: Client  
Used by: Applicant Data page, Request Creation applicant context/reuse

Visible DATA:

```text
- current active applicant marker/summary;
- applicant type;
- applicant display name;
- applicant contact summary;
- applicant identifiers relevant for selected type.
```

Reference DATA for request creation:

```text
- current active ApplicantParty for the account.
```

Open questions:

```text
Q: Should physical person actual/residential address become current target scenario DATA or stay future?
Q: Is phone required for all applicant types in current implementation?
Q: Should historical request creation store applicant snapshot later?
```

Accepted direction:

```text
Current active ApplicantParty is unique per account.
Applicant contact email can differ from account email.
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
```
