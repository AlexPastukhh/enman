# SL-APPL-002 — Account Applicant Parties Read / Templates

Status: implemented backend/API read slice  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Parties Page`, `SC-10B Applicant Parties Future Management`  
Slice type: backend/server read slice  
Current implementation status: implemented as `GET /api/l1/applicant-parties` with flat `applicantParties[]` response.

## 1. Slice Overview

This slice owns the protected backend endpoint that returns all `ApplicantParty` records owned by the current authenticated account.

Endpoint:

```text
GET /api/l1/applicant-parties
```

Response direction/current behavior:

```text
L1AccountApplicantPartiesResponse {
  applicantParties: L1ApplicantPartySummaryDto[]
}
```

The backend returns one flat account-owned list. The client groups this list visually:

```ts
const currentDefaults = applicantParties.filter(x => x.isCurrentDefault);
const otherSaved = applicantParties.filter(x => !x.isCurrentDefault);
```

`isCurrentDefault` is mapped from the current persisted marker `IsCurrentActiveVersion`.

## 2. Scope

Implemented scope:

```text
- protected account-level ApplicantParty read endpoint;
- current L1 account derived from auth/session;
- return all owned ApplicantParties;
- return flat applicantParties[] list;
- include ApplicantParty type, display/card data, verification status and current/default marker;
- map Individual full name where applicable;
- no clientAccountId in response;
- read-only/no mutation.
```

## 3. Out of Scope

| Out-of-scope item | Owner / destination |
|---|---|
| Create ApplicantParty | `SL-APPL-001` |
| Explicit make default/current command | `SL-APPL-003` — backend implemented; client action remains future `SL-APPL-003.client` |
| Applicant Parties target page replacement | `SL-APPL-002.client` / AccountPage replacement work |
| Separate “My Applicant Parties” page | Not current scenario |
| Delete/archive/edit lifecycle | Future ApplicantParty lifecycle slices |
| LegalEntity / IndividualEntrepreneur create | Future type-specific slices |
| `IsCurrentActiveVersion` rename | Cleanup/default-template naming task |

## 4. Scenario Flow

```text
Signed-in client opens Applicant Parties page / section
        ↓
System returns all ApplicantParties for current account
        ↓
Each item includes isCurrentDefault marker
        ↓
Client renders current/default highlighted area from isCurrentDefault == true
        ↓
Client renders other saved area from isCurrentDefault == false
        ↓
Client can use the same data for request creation applicant selection
```

## 5. Backend Implementation Flow

```text
[HTTP]
GET /api/l1/applicant-parties
        ↓
[Authorize]
current L1 client required
        ↓
[Controller]
derive accountId from auth claims/session
        ↓
[Query]
L1GetAccountApplicantPartiesQuery(accountId)
        ↓
[Handler]
verify account exists
list owned ApplicantParties
map summaries
        ↓
[Response]
200 OK { applicantParties: [...] }
```

Mapping includes:

```text
applicantPartyId
applicantPartyType
displayName
fullName when Individual
email
phoneNumber
verificationStatus
isCurrentDefault = IsCurrentActiveVersion
createdAt
```

## 6. Behavior Coverage

| Behavior item | Current coverage |
|---|---|
| Account may store multiple ApplicantParties | endpoint returns all owned parties |
| One current/default template per type is visible | response exposes `isCurrentDefault` marker |
| Additional same-type create does not silently switch | read exposes persisted marker after create/default operations |
| Existing requests unchanged | read endpoint does not mutate requests |

## 7. Test / Verification Plan

Expected proof:

```text
- unauthenticated -> 401;
- account with no ApplicantParties -> 200 and empty list;
- account with one first-of-type ApplicantParty -> item has isCurrentDefault true;
- account with multiple ApplicantParties -> all owned rows returned;
- unowned ApplicantParties excluded;
- response does not include clientAccountId;
- endpoint does not mutate ApplicantParties or requests.
```

## 8. Current Remaining Work

Backend slice itself is implemented.

Remaining adjacent work:

```text
- target Applicant Parties page/section replacement on client;
- future SL-APPL-003.client make default/current action;
- later cleanup decision for old current-individual endpoint.
```
