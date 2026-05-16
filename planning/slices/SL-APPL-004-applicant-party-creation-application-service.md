# SL-APPL-004 — ApplicantParty Creation Application Service

Status: implementation-ready helper slice / planned refactor  
Package: `[L1]`  
Source scenarios: `SC-10 Applicant Data`, `SC-04 Client Request Creation`  
Slice type: application/helper slice

## 1. Decision

Extract ApplicantParty creation logic into a reusable application service.

Use it from:

```text
- standalone create ApplicantParty handler;
- create request handler when applicantContextType = New.
```

## 2. Reason

```text
Standalone applicant creation and request creation with new applicant data share validation/domain creation rules but need different transaction boundaries.

Standalone create:
  create applicant -> SaveChanges -> return ApplicantPartyId.

Request New branch:
  create applicant -> create request -> SaveChanges once.
```

## 3. Service Responsibility

Does:

```text
- validate applicant data;
- load/check ClientAccount;
- create IndividualApplicantParty;
- add ApplicantParty to repository/context;
- initialize default only if none exists for type;
- return created entity/result.
```

Does not:

```text
- call SaveChanges;
- create request;
- change default when one already exists;
- replace/deactivate existing ApplicantParties;
- send nested commands.
```

## 4. Test / Verification Plan

```text
- standalone create still commits applicant;
- request New branch commits applicant + request together;
- request failure leaves no orphan applicant;
- invalid applicant returns validation errors;
- service SaveChanges absence reviewed/covered where useful.
```
