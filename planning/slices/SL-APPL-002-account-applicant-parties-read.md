# SL-APPL-002 — Account Applicant Parties Read / Templates

Status: implementation-ready draft / not implemented unless repo evidence later proves otherwise  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`, `SC-10B My Applicant Parties`  
Slice type: backend/API read model slice with dependent Account page client sidecar

## 1. Slice Overview

Purpose:

```text
Provide Account page with saved ApplicantParties and current/default templates per applicant type.
```

Target response should let the client render:

```text
- current/default physical person template, if any;
- current/default individual entrepreneur template, if any;
- current/default legal entity template, if any;
- all saved ApplicantParties, including non-default profiles;
- verification state when available.
```

This replaces the older narrow planning idea of “read current individual applicant only”.

## 2. Visual Scenario Flow

```text
[Signed-in Client]
Opens Account page
        ↓
[System]
Loads saved ApplicantParties for the account
        ↓
[System]
Identifies current/default template per applicant type
        ↓
[Client UI]
Shows templates grouped by type at top
        ↓
[Client UI]
Shows all saved ApplicantParties below
```

## 3. Visual Implementation Flow

```text
[API Controller]
GET /api/l1/applicant-parties
[Authorize]
        ↓
[Application Query]
Derive account id from auth context
        ↓
[Read Repository]
Load saved ApplicantParties for account
        ↓
[Read Projection]
Group current/default by applicant type
Include all saved items
        ↓
[API Contract]
200 OK with ApplicantParties account state DTO
```

## 4. Draft API Direction

Candidate endpoint:

```text
GET /api/l1/applicant-parties
```

Candidate response shape:

```ts
type L1ApplicantPartiesAccountStateDto = {
  defaults: {
    physicalPerson: L1ApplicantPartySummaryDto | null;
    individualEntrepreneur: L1ApplicantPartySummaryDto | null;
    legalEntity: L1ApplicantPartySummaryDto | null;
  };
  items: L1ApplicantPartySummaryDto[];
};

type L1ApplicantPartySummaryDto = {
  applicantPartyId: number;
  applicantType: "PhysicalPerson" | "IndividualEntrepreneur" | "LegalEntity";
  displayName: string;
  contactEmail: string;
  contactPhone: string;
  verificationStatus: "NotVerified" | "UnderReview" | "Verified" | "RejectedRequiresUpdate";
  isCurrentDefaultForType: boolean;
};
```

## 5. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| SL-APPL-READ-Q-001 | accepted direction | Is read model one current applicant or many saved ApplicantParties? | Many saved ApplicantParties plus defaults per type. | supersedes read-current-only direction |
| SL-APPL-READ-Q-002 | assumption | Endpoint path? | `GET /api/l1/applicant-parties` for account-level state. | API contract |
| SL-APPL-READ-Q-003 | accepted direction | Include verificationStatus? | Yes when available. | Account page status display |
| SL-APPL-READ-Q-004 | future review | Include full type-specific fields or summaries? | Summary first; full inline details only if Account page needs them. | DTO size/UI |

## 6. Behavior Coverage

| Behavior item | How draft covers it | Status |
|---|---|---|
| SC-10-BI-002 — many ApplicantParties | Read model returns `items`. | covered as draft |
| SC-10-BI-005 — default per type | Response has `defaults` by type. | covered as draft |
| SC-10-UI-002 — show templates by type | Client can render from `defaults`. | covered as draft |
| SC-10-UI-004 — show all saved ApplicantParties | Client can render `items`. | covered as draft |
| SC-10-UI-009 — refetch after create | This endpoint becomes refetch target. | covered as draft |

## 7. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Requires authentication | Guest cannot read ApplicantParties. | API integration | planned |
| Empty account returns empty defaults/items | Account page can show no-data state. | API integration | planned |
| Account with saved ApplicantParties returns items | Saved list is available. | API integration | planned |
| Default is returned per type | One default per type can be rendered. | API integration | planned |
| Non-default items remain visible | Items list includes saved non-default ApplicantParties. | API integration | planned |
| Cross-account items excluded | Read model is account-scoped. | API integration/security | planned |
| Generated OpenAPI/types updated | Client can use generated contract. | tooling | planned |

## 8. Follow-up

This slice feeds:

```text
SL-APPL-001 create refetch/update behavior
SL-APPL-003 select current/default template
future request creation applicant dropdown/list
```
