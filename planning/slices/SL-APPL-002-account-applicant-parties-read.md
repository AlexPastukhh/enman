# SL-APPL-002 — Account Applicant Parties Read / Templates

Status: implementation-ready draft / one Applicant Parties page read model  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Parties Page`, `SC-10B Applicant Parties Future Management`  
Slice type: backend/API/read-model slice

## 1. Slice Overview

Target behavior:

```text
Signed-in client opens Applicant Parties page / section.

System returns account Applicant Parties page state:
- current/default template per applicant type;
- other saved ApplicantParties that are not selected as current/default;
- verification status;
- enough inline display data to render cards/list.

Missing applicants is normal: return 200 with empty arrays.
```

This slice is backend/API read support for one Applicant Parties page / section.

It does not create a separate "My Applicant Parties" page.

## 2. Visual Scenario Flow

```text
Signed-in client opens Applicant Parties page / section
        ↓
System reads saved ApplicantParties for current account
        ↓
System identifies current/default template per applicant type
        ↓
System separates:
  current/default templates
  other saved ApplicantParties
        ↓
Applicant Parties page shows default/current templates in top highlighted area
        ↓
Applicant Parties page shows other saved ApplicantParties below
        ↓
If none exist, empty state + add action can be shown by client
```

Existing request boundary:

```text
Read model only reports current ApplicantParty page state.
It does not relink or update existing requests.
```

## 3. Draft API Direction

```text
GET /api/l1/applicant-parties
```

Response direction:

```ts
type L1AccountApplicantPartiesResponse = {
  currentDefaults: L1ApplicantPartySummaryDto[];
  otherApplicantParties: L1ApplicantPartySummaryDto[];
};

type L1ApplicantPartySummaryDto = {
  applicantPartyId: number;
  applicantType: "Individual" | "IndividualEntrepreneur" | "LegalEntity";
  displayName: string;
  email?: string | null;
  phoneNumber?: string | null;
  verificationStatus: "NotVerified" | "Verified" | "Rejected";
  isCurrentDefault: boolean;
};
```

Contract rules:

```text
- no accountId query/body input;
- response is scoped to authenticated account;
- default/current templates are separated from other saved parties;
- no duplicate rendering requirement for the same ApplicantParty in both arrays;
- include verification/display data needed for cards;
- 200 empty arrays when no ApplicantParties exist;
- paging not needed for first cut.
```

## 4. Questions / Decisions

| ID | Status | Question | Assumption / current direction | Impact | Shared register / local-only reason |
|---|---|---|---|---|---|
| `SL-APPL-002-Q-001` | accepted direction | Is this read support for a separate My Applicant Parties page? | No. It supports the one Applicant Parties page / section. | Scenario/page model. | Mirrored by `SL-APPL-Q-006` in `planning/slices/slice-questions-register.md`. |
| `SL-APPL-002-Q-002` | accepted direction | Should response duplicate default/current parties inside all saved list? | Prefer separated arrays: `currentDefaults` and `otherApplicantParties`; no duplicate required. | Client rendering and DTO shape. | Local to this read slice until API implementation starts. |
| `SL-APPL-002-Q-003` | future review | Does first cut need paging? | No. Add later only if volume/UX requires it. | API shape. | Local to this read slice; not cross-slice until pagination becomes shared. |
| `SL-APPL-002-Q-004` | accepted direction | Should read endpoint change existing requests? | No. Read endpoint does not mutate anything. | Safety / no-write boundary. | Mirrored by `SC-10-BI-009` and ApplicantParty direction docs. |

## 5. Behavior Coverage

| Source behavior item | How draft covers it | Status |
|---|---|---|
| `SC-10-BI-004` Account may store multiple ApplicantParties | Returns multiple saved ApplicantParties for current account. | covered as draft |
| `SC-10-BI-005` One current/default per applicant type | Response separates current/default templates by type. | covered as draft |
| `SC-10-BI-006` First of type may initialize default | Read model can expose initialized default/current when present. | covered as draft |
| `SC-10-BI-007` Additional same-type does not change default implicitly | Read model can show existing default plus other saved same-type parties. | covered as draft |
| `SC-10-BI-009` Existing requests unchanged | Endpoint is read-only and does not mutate requests. | covered as draft |
| `SC-10-UI-002` Default/current templates top area | Response provides `currentDefaults`. | covered as draft |
| `SC-10-UI-004` Other saved parties below | Response provides `otherApplicantParties`. | covered as draft |

## 6. Test / Verification Plan

```text
- requires auth;
- empty account returns 200 with empty arrays;
- returns multiple saved ApplicantParties;
- separates current/default templates from other saved ApplicantParties;
- returns default/current per type;
- does not return another account’s parties;
- includes verification status and display data;
- read endpoint does not mutate existing requests;
- OpenAPI/types generated when implemented.
```

## 7. Out Of Scope

```text
- add ApplicantParty command;
- explicit make default/current command;
- delete/archive lifecycle;
- edit/version behavior;
- request creation;
- changing existing requests.
```
