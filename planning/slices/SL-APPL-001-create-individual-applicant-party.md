# SL-APPL-001 — Create Individual ApplicantParty

Status: implemented narrow endpoint / target behavior reconciliation draft  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`  
Slice type: backend/API + scenario/UI alignment slice  
Current implementation status: create endpoint exists, returns ApplicantPartyId, uses shared creation service without SaveChanges, and preserves existing ApplicantParties; default-template selection remains future work

## 1. Slice Overview

Target behavior:

```text
Signed-in client adds an individual ApplicantParty.

Accepted data creates a new saved ApplicantParty linked to the account.

New ApplicantParty starts Unverified / NotVerified.

Existing ApplicantParties remain visible and unchanged.

If no current/default ApplicantParty exists for this applicant type,
the created ApplicantParty may initialize default/current for that type.

If current/default already exists for the type,
creating another ApplicantParty does not switch default implicitly.
```

Out of scope:

```text
- explicit default switch;
- read all ApplicantParties;
- request creation with New applicant;
- edit/delete/archive;
- entrepreneur/legal entity creation.
```

## 2. Sources / Source Behavior Items

```text
SC-10-BI-001
SC-10-BI-002
SC-10-BI-003
SC-10-BI-006
SC-10-BI-007
SC-10-UI-001..010
```

## 3. Visual Scenario Flow

```text
Signed-in Client opens Account page
        ↓
Account page shows Applicant Parties section
        ↓
Top area shows current/default templates by applicant type when available
        ↓
Saved list area shows all saved ApplicantParties
        ↓
User adds new individual ApplicantParty
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ accepted                     │ not accepted                 │
 ▼                              ▼
New ApplicantParty appears       User sees validation/error
in saved list                    feedback and can correct data
        ↓
Existing ApplicantParties remain visible and unchanged
        ↓
Default rule:
  no default for type -> new party may initialize default;
  default exists -> existing default remains selected
```

Scenario flow intentionally does not mention ApplicantPartyId, SaveChanges, service or repository.

## 4. Scenario Slice Flow

| Step | Actor/system | Behavior | Source/item | Status |
|---|---|---|---|---|
| F01 | Client | Opens Account page Applicant Parties section. | SC-10-UI-001 | target |
| F02 | UI/System | Shows templates and saved list. | SC-10-UI-002/004 | target |
| F03 | Client | Enters individual applicant data. | SC-10-BI-001 | covered |
| F04 | System | Creates saved ApplicantParty when accepted. | SC-10-BI-001 | covered/current narrow |
| F05 | System | Starts ApplicantParty as Unverified. | SC-10-BI-002 | covered |
| F06 | System | Leaves existing parties unchanged. | SC-10-BI-003 | covered/current standalone create |
| F07 | System | Applies default initialization only when none exists for type. | SC-10-BI-006/007 | future default-template slice |

## 5. Visual Implementation Flow

```text
[API Controller]
POST /api/l1/applicant-parties/individual
        ↓
[Application Handler]
standalone create ApplicantParty use case
        ↓
[ApplicantPartyCreationService]
validate/create/add entity; no SaveChanges
        ↓
[Default rule future]
explicit per-type default-template persistence remains separate
        ↓
[Handler]
SaveChanges for standalone create
        ↓
[API Response]
ApplicantPartyId
```

## 6. API Contract

| Endpoint | Method | Request DTO | Response DTO / Body | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/applicant-parties/individual` | POST | fullName/email/phone | ApplicantPartyId response kept | 200, 401, 403, 422, 500 | current endpoint / target behavior adjusted | yes |

Contract note:

```text
ApplicantPartyId is useful for stable list identity, cache reconciliation,
future selection, edit/archive/delete/default-template actions and future request applicant selection.

It is not a user-visible scenario behavior item.
```

## 7. Questions / Decisions

| ID | Status | Question | Current direction |
|---|---|---|---|
| `SL-APPL-Q-001` | accepted | Does create replace existing ApplicantParties? | No. Create is additive. |
| `SL-APPL-Q-002` | future default-template slice | What happens to default/current on create? | Current code keeps legacy current-active field true on new records; explicit per-type default-template persistence remains future. |
| `SL-APPL-Q-003` | accepted | Is a details page required? | No. Details can be inline. |
| `SL-APPL-Q-004` | accepted | Should create return ApplicantPartyId? | Yes, as API/implementation support. |
| `SL-APPL-Q-005` | implemented | Should shared creation logic be extracted? | Yes. `ApplicantPartyCreationService` validates/creates/adds; it does not call SaveChanges; outer handler commits. |

## 8. Behavior Coverage

| Behavior item | How slice covers it | Status |
|---|---|---|
| `SC-10-BI-001` | create endpoint creates saved ApplicantParty. | covered/current narrow |
| `SC-10-BI-002` | new party starts Unverified. | covered/current |
| `SC-10-BI-003` | integration test verifies second create keeps first stored/current flag unchanged. | covered/current standalone create |
| `SC-10-BI-006` | default initialization rule. | future default-template slice |
| `SC-10-BI-007` | no implicit default switch. | future default-template slice |
| ApplicantPartyId response | API contract note only. | not behavior |

## 9. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| authenticated create succeeds | valid signed-in create. | API integration | keep/current |
| unauthenticated create fails | auth boundary. | API integration | keep/current |
| invalid data rejects and writes nothing | validation/no-write. | API/domain | implemented/current |
| response includes ApplicantPartyId | stable identity support. | API integration | keep/current |
| created party belongs to authenticated account | ownership from auth. | API integration | keep/current |
| created party starts Unverified | verification default. | domain/API | keep/current |
| creating another party does not replace first | additive model. | API/domain | implemented/current |
| first of type initializes default | default rule. | API/domain | future default-template slice |
| second same-type does not switch default | no implicit replacement. | API/domain | future default-template slice |
| standalone create commits only applicant | transaction boundary. | API integration | implemented via handler-owned SaveChanges |
| request-create with New applicant is atomic | applicant + request together. | API integration | future SL-REQ-001 |

Do not test exact service method/repository call/SaveChanges location here.

## 10. Dependent / Follow-up Slices

```text
SL-APPL-002 — Account Applicant Parties Read / Templates
SL-APPL-003 — Select Current/Default ApplicantParty Template
SL-APPL-004 — ApplicantParty Creation Application Service
SL-REQ-001 — Create Connection Request With Applicant Context
```

## 11. Implementation Checklist

```text
[x] endpoint exists
[x] response includes ApplicantPartyId
[x] server derives account from auth context
[x] applicant starts Unverified
[x] remove current-active/replacement wording from implementation docs for standalone create
[x] extract reusable creation service
[x] create is additive
[ ] first-of-type initializes default
[ ] second same-type does not switch default through explicit default-template model
```
