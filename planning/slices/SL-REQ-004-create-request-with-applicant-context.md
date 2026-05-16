# SL-REQ-004 — Create Request With Explicit Applicant Context

Status: planned redesign slice / target scenario alignment  
Package: `[L1]`  
Source scenario: `SC-04 Client Request Creation`  
Slice type: backend/API command slice; future client sidecar required

## 1. Slice Overview

Current implemented request creation is narrower: server selects current active individual ApplicantParty.

Target scenario direction requires explicit applicant context:

```text
Existing saved ApplicantParty
or
New ApplicantParty data entered during request creation
```

When applicant context is `New`, request creation should create ApplicantParty and ConnectionRequest atomically in one server call.

## 2. Visual Scenario Flow

```text
[Client]
Creates request
        ↓
[Client]
Uses existing applicant data OR enters new applicant data
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ Existing ApplicantParty      │ New applicant data           │
 ▼                              ▼
[System]                       [System]
Uses selected saved             Creates new ApplicantParty
ApplicantParty                  and uses it for this request
        ↓                              ↓
[System]
Creates ConnectionRequest in InReview
```

## 3. Visual Implementation Flow

```text
[API Controller]
POST /api/l1/requests
requestData + applicantContext
        ↓
[Application Handler]
Derive account id from auth context
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ applicantContext = Existing  │ applicantContext = New       │
 ▼                              ▼
Load ApplicantParty by id       ApplicantPartyCreationService
and verify ownership            creates ApplicantParty
        ↓                              ↓
[Domain]
Create ConnectionRequest
        ↓
[Persistence]
Save ApplicantParty + Request once
```

## 4. Draft Contract Direction

Candidate shape:

```ts
type L1CreateConnectionRequestDto = {
  applicantContext: ExistingApplicantContext | NewApplicantContext;
  details: string;
  address: L1AddressDto;
};

type ExistingApplicantContext = {
  type: "Existing";
  applicantPartyId: number;
};

type NewApplicantContext = {
  type: "New";
  applicantType: "PhysicalPerson" | "IndividualEntrepreneur" | "LegalEntity";
  applicantData: L1ApplicantDataDto;
  makeCurrentDefault?: boolean;
};
```

Notes:

```text
The exact DTO names/shape are draft only.
Ownership must be verified server-side.
Do not let the client spoof account ownership.
```

## 5. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| SL-REQ-CTX-Q-001 | accepted direction | Should request creation with new applicant use two client calls? | No. One server call. | atomicity |
| SL-REQ-CTX-Q-002 | accepted direction | Who owns transaction? | Outer request handler saves ApplicantParty + Request once. | no orphan applicant |
| SL-REQ-CTX-Q-003 | accepted direction | Can existing ApplicantParty be selected? | Yes, target supports Existing context with ownership check. | API contract |
| SL-REQ-CTX-Q-004 | future review | Should new ApplicantParty be offered as default? | UI offers when default already exists; if none exists it may initialize default. | client/default behavior |
| SL-REQ-CTX-Q-005 | future review | Reference or snapshot? | Preserve submitted applicant context; exact model future. | request details/history |

## 6. Behavior Coverage

| Behavior item | How draft covers it | Status |
|---|---|---|
| SC-04-BI-003 — one accepted applicant context | DTO has applicantContext. | covered as draft |
| SC-04-BI-008 — new data creates ApplicantParty and request uses it | New branch creates ApplicantParty before request. | covered as draft |
| SC-04-BI-010 — no ownership spoofing | Existing branch verifies ownership server-side. | covered as draft |
| Atomicity | Save once in outer handler. | covered as draft |

## 7. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Existing ApplicantParty owned by account creates request | Existing context happy path. | API integration | planned |
| Existing ApplicantParty from another account rejected | Ownership check. | API integration/security | planned |
| New applicant + request created atomically | Single user intent succeeds. | API integration | planned |
| Request failure does not leave orphan ApplicantParty | No partial state. | API integration | planned |
| New applicant starts NotVerified | Applicant default state. | API/domain integration | planned |
| No default exists -> new applicant initializes default | Default policy. | API/domain integration | planned if policy implemented |
| Default exists -> new applicant does not silently switch | No hidden default mutation. | API/domain integration | planned if policy implemented |
| OpenAPI/generated types updated | Client can use contract. | tooling | planned |

## 8. Follow-up

Depends on:

```text
SL-APPL-004 — ApplicantParty Creation Application Service
SL-APPL-002 — Account Applicant Parties Read / Templates
future request creation client sidecar
```
