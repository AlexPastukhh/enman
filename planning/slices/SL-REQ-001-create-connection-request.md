# SL-REQ-001 — Create Connection Request With Applicant Context

Status: implemented backend/API/persistence with explicit applicant context  
Package: `[L1]`  
Source scenario: `SC-04 Client Request Creation`  
Slice type: backend / API / persistence slice

## 1. Slice Overview

Target behavior:

```text
Signed-in client creates a connection request.

The request uses one applicant context:
- Existing: selected owned saved ApplicantParty;
- New: applicant data entered during request journey.

New branch:
- creates new ApplicantParty;
- starts it NotVerified / Unverified;
- uses it for this request;
- commits ApplicantParty + ConnectionRequest atomically.

Created request enters InReview.
```

Current implementation note:

```text
Backend no longer selects one current active individual ApplicantParty implicitly for request creation.
POST /api/l1/requests uses explicit applicant context: Existing selected ApplicantParty or New applicant data.
```

## 2. Sources

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-data/SC-04-request-creation-data.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
```

## 3. Visual Scenario Flow

```text
Signed-in client opens request creation journey
        ↓
Request creation page shows request fields and applicant section
        ↓
System uses current/default ApplicantParty as initial prefill/default when available
        ↓
Client chooses applicant path:
  keep/use existing applicant,
  choose another saved applicant,
  or clear/enter new applicant data
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ selected saved ApplicantParty│ new applicant data           │
 ▼                              ▼
Request uses selected            New ApplicantParty is created
ApplicantParty                   and used for this request
        ↓                              ↓
Client provides details + object address
        ↓
Client submits request
        ↓
accepted -> ConnectionRequest created, InReview
rejected -> feedback, no partial write
```

## 4. Visual Implementation Flow

```text
[API Controller]
POST /api/l1/requests
        ↓
[Request DTO]
details + address + applicantContextType
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ Existing                     │ New                          │
 ▼                              ▼
load selected ApplicantParty     ApplicantPartyCreationService
and verify ownership             creates ApplicantParty, no commit
        ↓                              ↓
        └────────── resolved ApplicantParty ──────────┘
                         ↓
[Domain]
validate address/details, create ConnectionRequest
        ↓
[Commit]
SaveChanges once
Existing: request only
New: ApplicantParty + request
```

## 5. Draft API Contract

```ts
type L1CreateConnectionRequestDto = {
  applicantContextType: "Existing" | "New";
  existingApplicantPartyId?: number | null;
  newApplicantParty?: L1CreateIndividualApplicantPartyDto | null;
  details: string;
  address: L1AddressDto;
};
```

Existing branch:

```text
- existingApplicantPartyId required;
- newApplicantParty null;
- selected ApplicantParty can be current/default or any owned saved ApplicantParty;
- server verifies ownership.
```

New branch:

```text
- newApplicantParty required;
- existingApplicantPartyId null;
- create ApplicantParty;
- create request;
- commit atomically.
```

Response:

```text
200 OK / command success.
No response body required for initial flow.
```

## 6. Applicant Creation Reuse Decision

```text
Extract ApplicantParty creation logic into a reusable application service.

The service validates/creates/adds ApplicantParty but does not SaveChanges.

Standalone create ApplicantParty handler commits applicant.

Create request New branch commits ApplicantParty + ConnectionRequest together.
```

## 7. Questions / Decisions

| ID | Status | Question | Current direction |
|---|---|---|---|
| `SL-REQ-Q-APPL-001` | accepted | Does request creation rely on one current active ApplicantParty? | No. Explicit applicant context. |
| `SL-REQ-Q-APPL-002` | accepted | How choose branch? | `applicantContextType = Existing/New`. |
| `SL-REQ-Q-APPL-003` | accepted | Existing branch contract? | selected ApplicantPartyId + ownership check. |
| `SL-REQ-Q-APPL-004` | accepted | Can selected applicant be non-default? | Yes. Any owned saved ApplicantParty. |
| `SL-REQ-Q-APPL-005` | accepted | New branch contract? | New applicant payload; create applicant + request atomically. |
| `SL-REQ-Q-APPL-006` | accepted | Two client calls? | No. One server operation. |
| `SL-REQ-Q-APPL-007` | open | Return request id? | Current direction: no required body; revisit if direct details navigation is needed. |
| `SL-REQ-Q-APPL-008` | future | Snapshot or reference? | Future read/history decision. |

## 8. Behavior Coverage

| Behavior item | How draft covers it | Status |
|---|---|---|
| `SC-04-BI-001` | protected request creation. | covered/current |
| `SC-04-BI-003` | explicit applicant context in request DTO. | implemented |
| `SC-04-BI-004` | Existing branch loads selected owned ApplicantParty. | implemented |
| `SC-04-BI-005` | non-default saved applicant allowed by owned-id lookup. | implemented backend / future UI |
| `SC-04-BI-006` | New branch accepts applicant payload. | implemented |
| `SC-04-BI-007` | New branch creates ApplicantParty for request. | implemented |
| `SC-04-BI-008` | New branch commits applicant + request through one handler SaveChanges. | implemented |
| `SC-04-BI-009` | New branch uses additive creation service and does not replace existing ApplicantParties. | implemented |
| `SC-04-BI-010` | request InReview. | implemented |

## 9. Test / Verification Plan

```text
- create request with current/default ApplicantParty succeeds;
- create request with non-default saved ApplicantParty succeeds;
- selected ApplicantParty must belong to current account;
- Existing branch rejects missing id;
- Existing branch rejects extra newApplicantParty;
- New branch rejects missing applicant data;
- New branch rejects extra existing id;
- New branch creates ApplicantParty + request;
- New ApplicantParty starts Unverified;
- New ApplicantParty does not replace existing ApplicantParties;
- New applicant + request are atomic;
- invalid request data in New branch leaves no orphan applicant;
- request starts InReview;
- OpenAPI includes applicant context DTO.
```

Do not test React Query invalidation, UI dropdown behavior, service method call internals or My Requests rendering here.

## 10. Dependent / Follow-up Slices

```text
SL-APPL-002 — Account Applicant Parties Read / Templates
SL-APPL-003 — Select Current/Default ApplicantParty Template
SL-APPL-004 — ApplicantParty Creation Application Service
future SL-REQ-001.client — request creation UI with Existing/New paths
```

## 11. Implementation Checklist

```text
[x] add explicit applicant context marker
[x] add Existing branch validation
[x] add New branch validation
[x] verify selected ApplicantParty ownership
[x] extract ApplicantParty creation service
[x] reuse service in New branch
[x] SaveChanges once
[x] rollback/no orphan applicant on failure
[x] keep command success no required body
[x] update OpenAPI/types
```
