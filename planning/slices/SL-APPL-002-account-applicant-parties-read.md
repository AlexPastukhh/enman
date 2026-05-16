# SL-APPL-002 — Account Applicant Parties Read / Templates

Status: implementation-ready draft / replaces old single-current read direction  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`, `SC-10B My Applicant Parties`  
Slice type: backend/API/read-model slice

## 1. Slice Overview

Target behavior:

```text
Signed-in client opens Account page.

System returns account Applicant Parties state:
- current/default template per applicant type;
- all saved ApplicantParties;
- verification status;
- enough inline data to render cards/list.

Missing applicants is normal: return 200 with empty arrays.
```

## 2. Visual Scenario Flow

```text
Signed-in client opens Account page
        ↓
System reads saved ApplicantParties for current account
        ↓
System identifies current/default template per applicant type
        ↓
Account page shows default templates separately
        ↓
Account page shows all saved ApplicantParties
        ↓
If none exist, empty state + add action
```

## 3. Draft API Direction

```text
GET /api/l1/applicant-parties
```

Response direction:

```ts
type L1AccountApplicantPartiesResponse = {
  currentDefaults: L1ApplicantPartySummaryDto[];
  applicantParties: L1ApplicantPartySummaryDto[];
};
```

## 4. Questions / Decisions

```text
- Single current read is superseded by account ApplicantParties state.
- 200 empty arrays for none.
- Include verificationStatus.
- Paging not needed for first cut.
```

## 5. Test / Verification Plan

```text
- requires auth;
- empty account returns empty arrays;
- returns multiple saved ApplicantParties;
- returns default per type;
- does not return another account’s parties;
- includes verification status;
- OpenAPI/types generated.
```
