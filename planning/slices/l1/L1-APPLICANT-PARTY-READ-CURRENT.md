# L1-APPLICANT-PARTY-READ-CURRENT — Read Current Individual Applicant Party

Status: implemented backend/API read slice  
Package: L1 backend/API slice + generated contract  
Source scenario: Account page current applicant party state  
Slice type: backend read/API/persistence slice  
Current implementation status: implemented in backend; OpenAPI/generated types updated; client integration is documented in the `.client` sidecar

## 1. Slice Overview

Observable behavior:

```text
Authenticated client opens Account page.
System reads current active individual applicant party for the authenticated account.
If it exists, Account page receives applicant party data.
If it is missing, Account page receives missing-applicant state.
```

Why this is a real slice:

```text
- provides Account page initial state after refresh/navigation;
- separates read state from create applicant party command;
- lets client show read-only applicant data when it already exists;
- avoids requiring client to pass accountId or applicantPartyId;
- independently testable through protected API integration tests;
- needed before Account page can be stable beyond local post-submit state.
```

In scope:

```text
- protected GET endpoint for current individual applicant party;
- server-derived account context from L1 auth claims;
- current-active individual applicant party lookup;
- response shape for existing and missing applicant party;
- verificationStatus in read model;
- OpenAPI / generated TypeScript contract updates;
- server integration tests for protected read behavior.
```

Out of scope:

```text
- creating applicant party;
- editing applicant party;
- verifying applicant party;
- deactivating previous applicant party versions;
- enforcing uniqueness of current active applicant party;
- create connection request UI;
- My Requests page;
- client implementation, except contract/consumer notes.
```

## 2. Sources / Source Behavior Items

Sources to attach during final consolidation:

```text
- Account page scenario / UI spec once authoritative IDs are attached.
- Applicant party create client sidecar.
- L1 backend current implementation:
  - EnergyManagement.Server/L1/Controllers/L1Controller.cs
  - EnergyManagement.Server/L1/Application/Abstractions/IApplicantPartyRepository.cs
  - EnergyManagement.Server/L1/Persistence/Repositories/ApplicantPartyRepository.cs
  - Domain.EnergyManagement/L1/Applicants/ApplicantParty.cs
  - Domain.EnergyManagement/L1/Applicants/IndividualApplicantParty.cs
- OpenAPI generated contract workflow.
```

Behavior items used in this draft are readable working behavior items.

Authoritative scenario/UI behavior IDs are not attached yet and should be linked during final scenario consolidation.

Scenario facts used:

```text
- Authenticated client can open Account page.
- Account page needs to know whether current individual applicant party exists.
- System derives current client account from L1 auth session.
- Client does not provide account id for account-scoped reads.
- Existing applicant party data is displayed as read-only Account page state.
- Missing applicant party is normal Account page state and leads to create form.
- Applicant party has verification status that future UI can display.
```

## 3. Visual Scenario Flow

```text
┌──────────────────────────────┐
│ Authenticated Client          │
└──────────────┬───────────────┘
               │ opens Account page
               ▼
┌──────────────────────────────┐
│ System                        │
│ Identify current L1 account   │
│ from auth session             │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────────────────┐
│ System                                   │
│ Search current active individual         │
│ applicant party for current account      │
└──────────────┬───────────────────────────┘
               │
        ┌──────┴───────┐
        │              │
       found          missing
        │              │
        ▼              ▼
┌──────────────────────┐   ┌────────────────────────────┐
│ Return applicant data │   │ Return missing state        │
│ exists = true         │   │ exists = false              │
└──────────┬───────────┘   └────────────┬───────────────┘
           │                            │
           ▼                            ▼
┌──────────────────────┐   ┌────────────────────────────┐
│ Account page shows    │   │ Account page shows          │
│ read-only data        │   │ create applicant form       │
└──────────────────────┘   └────────────────────────────┘
```

Scenario notes:

```text
- client does not send accountId;
- client does not need applicantPartyId for this page state;
- missing applicant party is not a failure;
- this slice does not create or update applicant party records.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Scope status |
|---|---|---|---|
| F01 | Authenticated client | Opens Account page. | source behavior |
| F02 | Account page / client | Needs current applicant party state. | dependent client |
| F03 | System | Identifies current client account from L1 auth session. | backend slice |
| F04 | System | Searches current active individual applicant party for current account. | backend slice |
| F05 | System | If applicant exists, returns applicant data. | backend slice |
| F06 | System | If applicant is missing, returns `exists=false` state. | backend slice |
| F07 | Account page / client | Shows read-only applicant data when it exists. | dependent client |
| F08 | Account page / client | Shows create applicant form when applicant is missing. | dependent client |
| F09 | Future extension | Displays verification status labels and workflow-specific states. | follow-up |
| F10 | Future extension | Handles edit/update applicant party behavior. | follow-up |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ GET /api/l1/applicant-parties/current-individual
│ [Authorize]                                  │
└──────────────┬───────────────────────────────┘
               │ TryGetCurrentL1AccountId()
               ▼
┌──────────────────────────────────────────────┐
│ Current L1 auth context valid?               │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Application Query     │   │ API response                 │
│ Get current individual│   │ 401 Unauthorized             │
│ applicant party       │   └──────────────────────────────┘
└──────────┬───────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Persistence                                  │
│ Current active individual applicant lookup:  │
│ - ClientAccountId == current account id      │
│ - IsCurrentActiveVersion == true             │
│ - ApplicantPartyType == Individual           │
└──────────┬───────────────────────────────────┘
           │
    ┌──────┴───────┐
    │              │
   found          missing
    │              │
    ▼              ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Map applicant data    │   │ Map missing applicant state   │
│ exists = true         │   │ exists = false                │
└──────────┬───────────┘   └────────────┬─────────────────┘
           │                            │
           ▼                            ▼
┌─────────────────────────────────────────────────────────┐
│ API Contract                                            │
│ 200 OK                                                  │
│ L1CurrentIndividualApplicantPartyResponse               │
└─────────────────────────────────────────────────────────┘
```

No-write boundary:

```text
- no command;
- no entity creation;
- no entity update;
- no deactivation;
- no verification state change;
- no SaveChangesAsync.
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Notes |
|---|---|---|---|
| I01 | API | Add protected GET endpoint. | `GET /api/l1/applicant-parties/current-individual`. |
| I02 | API | Derive current L1 account id from auth claims. | Use same L1 auth boundary as other protected L1 endpoints. |
| I03 | API | Return 401 when L1 auth context is missing/invalid. | Observable protected endpoint behavior. |
| I04 | Application Query | Add query object for current individual applicant party. | Query carries `ClientAccountId`. |
| I05 | Application Query Handler | Load current active individual applicant party. | Use existing repository lookup for first implementation. |
| I06 | Persistence | Filter by current account, active version and individual type. | Existing repository method already follows this direction. |
| I07 | Application Query Handler | Map found applicant party into read response. | Include full name, email, phone number, verification status. |
| I08 | Application Query Handler | Map missing applicant party into `exists=false`. | Missing applicant is normal Account page state. |
| I09 | API | Return 200 OK with response DTO. | For both found and missing cases. |
| I10 | Contract tooling | Regenerate OpenAPI and TypeScript types. | Client-facing endpoint. |

Implementation detail:

```text
Response should expose `verificationStatus` as string, for example `Unverified`,
rather than relying on numeric enum serialization.
```

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO / Body | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/applicant-parties/current-individual` | GET | none | `L1CurrentIndividualApplicantPartyResponse` | 200, 401, 500 | target L1 | yes |

### Existing applicant response

```json
{
  "exists": true,
  "applicantParty": {
    "fullName": {
      "firstName": "Иван",
      "middleName": "Иванович",
      "lastName": "Иванов"
    },
    "email": "client@example.com",
    "phoneNumber": "+79990000000",
    "verificationStatus": "Unverified"
  }
}
```

### Missing applicant response

```json
{
  "exists": false,
  "applicantParty": null
}
```

### Proposed DTOs

```text
L1CurrentIndividualApplicantPartyResponse
  exists: bool
  applicantParty: L1IndividualApplicantPartyDto?

L1IndividualApplicantPartyDto
  fullName: L1FullNameDto
  email: string
  phoneNumber: string
  verificationStatus: string
```

Response does not include:

```text
- applicantPartyId
- clientAccountId
```

This is contract direction, not a separate integration-test target.

ProblemDetails statuses:

```text
401 Unauthorized
500 Internal Server Error
```

Validation `422` is not expected for the read operation because there is no client-supplied input body. Add it only if the implementation introduces validation that can fail due to request data.

OpenAPI / generated client expectations:

```text
- Shared/openapi.json includes the new endpoint.
- openapi-types.ts includes the response shape.
- Client integration uses generated response type from `components["schemas"]`.
```

## 8. Questions / Decisions

Open questions first, then accepted decisions and future-review items.

### Q-APPL-READ-001 — What response should missing applicant party use?

Question status: resolved

Question:  
Should missing current applicant party return `404 Not Found`, or a successful empty Account page state?

Decision:

```text
200 OK
exists=false
applicantParty=null
```

Impact:  
Account page can treat missing applicant party as normal page state and show create form without exception-like control flow.

Shared register:  
`planning/slices/slice-questions-register.md` — mirror because this can affect future current/read endpoints.

---

### Q-APPL-READ-002 — Should response include applicantPartyId?

Question status: resolved

Question:  
Should the read response expose `applicantPartyId`?

Decision:  
No.

Impact:  
Current Account page does not need applicant party id. Create request flow must not depend on client-supplied applicant party id. If future edit/details flow needs identity, add it deliberately through that slice.

Shared register:  
Local for now; mirror when edit/details slice starts.

---

### Q-APPL-READ-003 — Should response include clientAccountId?

Question status: resolved

Question:  
Should the response expose `clientAccountId`?

Decision:  
No.

Impact:  
The server derives account from auth context. Account page needs applicant data state, not account identity plumbing.

Shared register:  
Local only.

---

### Q-APPL-READ-004 — Should response include verificationStatus?

Question status: accepted direction

Question:  
Should Account page receive applicant party verification state?

Assumption / current direction:  
Yes. Return `verificationStatus`.

Impact:  
Account page can later display verification-related states.

```text
- Not verified
- Under review / pending verification
- Verified
- Rejected / requires update
```

Exact labels and verification workflow remain future UI/domain work.

Shared register:  
`planning/slices/slice-extension-points-register.md` — mirror as verification-status UI extension point.

---

### Q-APPL-READ-005 — Should this use existing repository lookup?

Question status: accepted direction

Question:  
Should the query reuse the existing current-active individual applicant lookup?

Assumption / current direction:  
Yes.

Use current active individual applicant party lookup by:

```text
ClientAccountId == current account id
IsCurrentActiveVersion == true
ApplicantPartyType == Individual
```

Impact:  
If read performance/tracking matters later, add a read-specific `AsNoTracking` query. Behavior should stay the same.

Shared register:  
Local only, or implementation notes register if optimization note is desired.

---

### Q-APPL-READ-006 — How is exactly one current active applicant party enforced?

Question status: future review

Question:  
What prevents multiple current active individual applicant parties for one account?

Assumption / current direction:  
This read slice uses existing current-active lookup behavior. Enforcement of uniqueness/current-active invariant belongs to a separate invariant/data-policy slice.

Impact:  
Can affect create applicant party, edit applicant party, repository behavior, DB constraints and tests.

Shared register:  
`planning/slices/slice-questions-register.md` — mirror because this is broader than this read slice.

---

### Q-APPL-READ-007 — Should this slice update OpenAPI and generated TS types?

Question status: accepted direction

Question:  
Should this endpoint be exposed in generated contract artifacts?

Assumption / current direction:  
Yes.

This is a client-facing Account page endpoint, so it must be included in `Shared/openapi.json` and generated `openapi-types.ts`.

Impact:  
Client integration should use generated response types, not handwritten API DTOs.

Shared register:  
Local only; covered by OpenAPI consumer rule.

## 9. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How slice covers it | Draft/file location | Status |
|---|---|---|---|
| Account page needs current applicant state | Adds a read endpoint for current applicant party state. | Visual Scenario Flow / API Contract | covered |
| System identifies current account from auth session | Controller derives account from L1 auth context; client sends no account id. | Visual Implementation Flow / Implementation Flow | covered |
| System searches for current active individual applicant party | Query uses current-active individual applicant lookup. | Visual Implementation Flow / Q-APPL-READ-005 | covered |
| Existing applicant data is returned | Response contains applicant data for Account page display. | API Contract | covered |
| Missing applicant party is returned as page state | Response uses `200 OK`, `exists=false`, `applicantParty=null`. | Q-APPL-READ-001 / API Contract | covered |
| Applicant data can include verification state | Response includes `verificationStatus`. | Q-APPL-READ-004 / API Contract | covered |
| Read operation has no side effects | Slice explicitly excludes command/write/save behavior. | Visual Implementation Flow / Implementation Flow | covered |
| Client does not receive account identity | Response omits `clientAccountId`. | API Contract / Q-APPL-READ-003 | covered |

## 10. Test / Verification Plan

### Server integration/API tests

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Authenticated client with current applicant gets `200 OK`, `exists=true`, and applicant data | Happy path read returns current applicant state. | Integration/API | planned |
| Existing applicant response contains full name and contact data | Returned data is usable by Account page read-only view. | Integration/API | planned |
| Existing applicant response contains verification status | Account page can display applicant status. | Integration/API | planned |
| Authenticated client without applicant gets `200 OK`, `exists=false`, `applicantParty=null` | Missing applicant is normal Account page state. | Integration/API | planned |
| Unauthenticated request gets `401 Unauthorized` | Endpoint is protected. | Integration/API | planned |
| Authenticated client does not receive another account’s applicant party | Server derives account from auth context and scopes read to current account. | Integration/API | planned |
| OpenAPI check includes endpoint and response DTO | Contract artifact updated. | Tooling | planned |
| Generated TS types include read response shape | Client can use generated contract. | Tooling | planned |

Suggested test names:

```text
GetCurrentIndividualApplicantParty_WithCurrentApplicant_ReturnsApplicantData
GetCurrentIndividualApplicantParty_WithoutCurrentApplicant_ReturnsExistsFalse
GetCurrentIndividualApplicantParty_Unauthenticated_ReturnsUnauthorized
GetCurrentIndividualApplicantParty_DoesNotReturnAnotherAccountApplicantParty
```

Do not add separate tests for:

```text
- response does not contain applicantPartyId;
- response does not contain clientAccountId;
- read endpoint does not update existing applicant party.
```

Those are contract/implementation constraints, not separate behavior tests for this slice.

No-write remains an implementation rule. Test no-write separately only if this endpoint later gains read-side effects such as projection refresh, last-seen markers, or lazy creation.

### Contract/tooling checks

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `generate-openapi --check` passes | OpenAPI artifact matches backend source. | Tooling/API | planned |
| `generate:api-types` or `check:api` passes | TypeScript generated contract matches OpenAPI. | Tooling/client contract | planned |
| Build passes | New query/controller/DTO compile. | Build | planned |

### E2E

No separate E2E is required for backend-only read slice.

E2E becomes relevant in client integration:

```text
browser opens Account page
        ↓
client calls current applicant party endpoint
        ↓
UI shows read-only applicant data or create form
```

## 11. Dependent / Follow-up Slices

```text
[CLIENT][DEPENDENT] L1-APPLICANT-PARTY-READ-CURRENT.client
Account page fetches current applicant party on open and after create success.
```

```text
[CLIENT][RELATED] L1-APPLICANT-PARTY-CREATE-INDIVIDUAL.client
After successful create, invalidates/refetches this read state.
```

```text
[BACKEND][FOLLOW-UP] L1-APPLICANT-PARTY-CURRENT-ACTIVE-INVARIANT
Defines/enforces exactly one current active individual applicant party per account/type.
```

```text
[BACKEND/CLIENT][FOLLOW-UP] L1-APPLICANT-PARTY-EDIT
Allows editing/replacing applicant data and decides whether applicantPartyId is needed.
```

```text
[BACKEND/CLIENT][FOLLOW-UP] L1-APPLICANT-PARTY-VERIFICATION
Defines verification workflow, status transitions and UI labels.
```

```text
[CLIENT][FOLLOW-UP] L1-CONNECTION-REQUEST-CREATE.client
Uses current active applicant party server-side; create request entry location remains future decision.
```

## 12. Implementation Checklist

Backend/API:

```text
[x] Add `L1CurrentIndividualApplicantPartyResponse` DTO.
[x] Add `L1IndividualApplicantPartyDto` DTO.
[x] Decide DTO property names and JSON names.
[x] Add `L1GetCurrentIndividualApplicantPartyQuery`.
[x] Add `L1GetCurrentIndividualApplicantPartyHandler`.
[x] Use existing current-active individual applicant repository lookup.
[x] Map found applicant party to read DTO.
[x] Map missing applicant party to `exists=false`.
[x] Add protected GET endpoint to `L1Controller`.
[x] Return 401 when L1 auth context is invalid/missing.
```

Tests:

```text
[x] Add authenticated happy path integration test.
[x] Add missing applicant integration test.
[x] Add unauthenticated integration test.
[x] Add other-account scoping integration test.
[x] Assert verificationStatus is returned.
```

Generated contracts:

```text
[x] Regenerate `Shared/openapi.json`.
[x] Regenerate `energymanagement.client/src/shared/api/generated/openapi-types.ts`.
[x] Run OpenAPI/check API commands.
```

Shared register sync:

```text
[ ] Mirror missing applicant response shape question.
[ ] Mirror current-active uniqueness invariant question.
[ ] Mirror verificationStatus UI/read-model extension point.
[ ] Optionally add read-specific `AsNoTracking` query note to implementation notes register.
```

Commands:

```powershell
dotnet build EnergyManagement.Server/EnergyManagement.Server.csproj
dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd --prefix energymanagement.client run generate:api-types
npm.cmd run check:api
```
