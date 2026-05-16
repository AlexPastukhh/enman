# SL-APPL-003 — Select Current/Default ApplicantParty Template

Status: planned / future explicit action  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`, `SC-10B My Applicant Parties`  
Slice type: backend/API + client future command slice

## 1. Slice Overview

Target behavior:

```text
Client explicitly selects one saved ApplicantParty as current/default template for its applicant type.

Previous current/default for that type is unset.

Future request creation uses the selected ApplicantParty as initial prefill/default.

Existing requests remain unchanged.
```

## 2. Visual Scenario Flow

```text
Client sees saved ApplicantParties and default markers
        ↓
Client chooses "make default/current"
        ↓
System verifies ownership
        ↓
System sets selected party default for its type
        ↓
Previous same-type default is unset
        ↓
Account page highlights new default
```

## 3. Test / Verification Plan

```text
- owned ApplicantParty can become default;
- previous same-type default is unset;
- other type default unchanged;
- another account’s ApplicantParty rejected;
- existing requests unchanged.
```
