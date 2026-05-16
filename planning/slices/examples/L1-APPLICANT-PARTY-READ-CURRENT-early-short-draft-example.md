# L1-APPLICANT-PARTY-READ-CURRENT — Early Short Draft Example

Status: example / valid shortened slice draft  
Example type: shortened backend read/API slice draft with extension/change point handling  
Source: adapted from working draft format provided for slice drafting workflow  
Not authoritative implementation evidence: check current repo and active slice files before use

## Header Example

**Status:** early backend draft  
**Slice type:** backend read/API slice  
**Scope:** authenticated client reads current active individual applicant party for Account page  
**Contract direction:** target L1 read endpoint  
**Response direction:** account-page state response, no client-provided account id  
**Related client integration:** Account page fetches this state on open and after applicant party create success.

Current backend has create applicant party and create request endpoints, but not current applicant party read endpoint. The repository already has a current active individual applicant lookup, so this slice mostly adds query/API/read contract around existing persistence capability.

This is an example only. It demonstrates shortened draft format, assumptions, questions, extension/change point notes, behavior coverage and test planning. It is not a current implementation claim.

## 1. Visual Scenario Flow

```text
[Authenticated Client]
Opens Account page
        ↓
[System]
Identifies current client account from L1 auth session
        ↓
[System]
Looks for current active individual applicant party
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ current applicant exists     │ current applicant missing     │
 ▼                              ▼
[System]                       [System]
Returns applicant data          Returns missing-applicant state
for Account page                for Account page
        ↓                              ↓
[Client UI]                    [Client UI]
Shows applicant data            Shows create applicant form
as read-only fields
```

Scenario meaning:

```text
- The client does not submit accountId.
- Missing applicant party is a normal Account page state, not a failure.
- This slice is read-only.
```

## 2. Visual Implementation Flow

```text
[API Controller: L1Controller]
GET /api/l1/applicant-parties/current-individual
[Authorize]
        ↓
[API Controller]
Derives current L1 account id from auth claims
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ L1 auth context is valid     │ auth context missing/invalid │
 ▼                              ▼
[Application Query]            [API]
Get current individual          401 Unauthorized
applicant party
        ↓
[Persistence]
Find current active individual applicant party
by current account id
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ found                        │ not found                    │
 ▼                              ▼
[Application Query]            [Application Query]
Maps applicant data             Returns exists=false state
        ↓                              ↓
[API Contract]                 [API Contract]
200 OK + exists=true            200 OK + exists=false
```

No write path:

```text
no command
no entity creation
no entity update
no SaveChangesAsync
```

## 3. Questions / Decisions

Open questions, assumptions and future-review risks go before accepted directions.

### Q-APPL-READ-001 — What response should missing applicant party use?

**Question status:** assumption.

**Question:**
Should missing current applicant party return `404 Not Found`, or a successful empty Account page state?

**Assumption / current direction:**
Return:

```text
200 OK
exists=false
applicantParty=null
```

**Impact:**
Account page can treat missing applicant party as normal page state and show create form without exception-like control flow.

**Shared register:**
Mirror to `planning/slices/slice-questions-register.md`, because this can affect future current/read endpoints.

### Q-APPL-READ-002 — Should response include applicantPartyId?

**Question status:** assumption.

**Question:**
Should the read response expose `applicantPartyId`?

**Assumption / current direction:**
Do not expose `applicantPartyId` initially.

**Impact:**
Current Account page does not need id. Create request flow must not depend on client-supplied applicant party id. If future edit/details flow needs identity, add it deliberately through that slice.

**Shared register:**
Local for now; mirror when edit/details slice starts.

### Q-APPL-READ-003 — Should response include clientAccountId?

**Question status:** accepted direction.

**Question:**
Should the response expose `clientAccountId`?

**Assumption / current direction:**
No.

The server already derives account from auth context. Account id is not needed by Account page applicant data view.

**Impact:**
Keeps read contract focused on applicant data, not account identity plumbing.

**Shared register:**
Local only.

### Q-APPL-READ-004 — Should response include verificationStatus?

**Question status:** accepted direction.

**Question:**
Should Account page receive applicant party verification state?

**Assumption / current direction:**
Yes. Return `verificationStatus`.

**Impact:**
Account page can later display:

```text
Not verified
Under review / pending verification
Verified
Rejected / requires update
```

Exact labels and verification workflow remain future UI/domain work.

**Shared register:**
Mirror to `planning/slices/slice-extension-points-register.md` as verification-status UI extension point.

### Q-APPL-READ-005 — Should this use existing repository lookup?

**Question status:** accepted direction.

**Question:**
Should the query reuse the existing current-active individual applicant lookup?

**Assumption / current direction:**
Yes.

Use current active individual applicant party lookup by:

```text
ClientAccountId == current account id
IsCurrentActiveVersion == true
ApplicantPartyType == Individual
```

**Impact:**
If read performance/tracking matters later, add a read-specific `AsNoTracking` query. Behavior should stay the same.

**Shared register:**
Local only, or implementation notes register if an optimization note is desired.

### Q-APPL-READ-006 — How is exactly one current active applicant party enforced?

**Question status:** future review.

**Question:**
What prevents multiple current active individual applicant parties for one account?

**Assumption / current direction:**
This read slice uses existing current-active lookup behavior. Enforcement of uniqueness/current-active invariant belongs to a separate invariant/data-policy slice.

**Impact:**
Can affect create applicant party, edit applicant party, repository behavior, DB constraints and tests.

**Shared register:**
Mirror to `planning/slices/slice-questions-register.md`.

### Q-APPL-READ-007 — Should this slice update OpenAPI and generated TS types?

**Question status:** accepted direction.

**Question:**
Should this endpoint be exposed in generated contract artifacts?

**Assumption / current direction:**
Yes.

This is a client-facing Account page endpoint, so it must be included in `Shared/openapi.json` and generated `openapi-types.ts`.

**Impact:**
Client integration should use generated response/request types, not handwritten API DTOs.

**Shared register:**
Local only; covered by OpenAPI consumer rule.

## 4. Extension / Change Points

Short drafts should not hide known extension pressure. They do not need full design, but they should record the current decision and target register.

| ID | Type | Area | Current direction | Register sync | Status |
|---|---|---|---|---|---|
| CP-APPL-READ-MISSING-001 | change point | Missing applicant response shape | `200 exists=false` instead of `404` for normal Account page state | `slice-questions-register.md` | assumption |
| EP-APPL-VERIFY-READ-001 | extension point | Account page verification status display | Include `verificationStatus` now; labels/workflow later | `slice-extension-points-register.md` | accepted direction |
| EP-APPL-EDIT-001 | extension point | Applicant edit/details identity | Omit `applicantPartyId` until edit/details slice needs it | future edit/replacement slice | future review |
| CP-APPL-CURRENT-ACTIVE-001 | change point / invariant pressure | Exactly one current active individual applicant | Use existing lookup now; enforce invariant in separate data-policy slice later | `slice-questions-register.md` | future review |
| NOTE-APPL-READ-PERF-001 | implementation note | Read query tracking/performance | Current behavior can reuse existing lookup; `AsNoTracking` optimization can be added later | `slice-implementation-notes-register.md` | future review |

## 5. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| Account page needs current applicant state | Adds a read endpoint for current applicant party state. | Visual Scenario Flow / API contract direction | covered |
| System identifies current account from auth session | Controller derives account from L1 auth context; client sends no account id. | Visual Implementation Flow | covered |
| System searches for current active individual applicant party | Query uses current-active individual applicant lookup. | Visual Implementation Flow / Q-APPL-READ-005 | covered |
| Existing applicant data is returned | Response includes applicant data for Account page display. | Questions / Next Step | covered |
| Missing applicant party is normal page state | Response uses `exists=false` assumption. | Q-APPL-READ-001 / CP-APPL-READ-MISSING-001 | covered |
| Applicant data can include verification state | Response includes `verificationStatus`. | Q-APPL-READ-004 / EP-APPL-VERIFY-READ-001 | covered |
| Read operation has no side effects | Draft explicitly excludes command/write/save behavior. | Visual Implementation Flow | covered |
| Client does not receive account identity | Response omits `clientAccountId`. | Q-APPL-READ-003 | covered |

## 6. Test / Verification Plan

### Server integration/API tests

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Authenticated client with current applicant gets 200 and applicant data | Happy path read | Integration/API | planned |
| Authenticated client without applicant gets 200 with `exists=false` | Missing applicant is normal Account page state | Integration/API | planned |
| Unauthenticated request gets 401 | Auth boundary | Integration/API | planned |
| Client cannot read another account's applicant party | Server derives account from auth context | Integration/API | planned |
| Response includes verification status | Account page can display verification state | Integration/API | planned |
| Read endpoint does not create/update applicant party | No-write guarantee | Integration/API | planned |
| OpenAPI check includes endpoint and response DTO | Contract artifact updated | Tooling | planned |
| Generated TS types include read response | Client can use generated contract | Tooling | planned |

### E2E

No separate E2E is needed for backend-only read slice.

E2E belongs to Account page client integration:

```text
browser opens Account page
        ↓
client calls current applicant party endpoint
        ↓
UI shows read-only applicant data or create form
```

## 7. Next Step

Implementation direction:

```text
Add response DTOs to L1Dtos.cs
        ↓
Add L1GetCurrentIndividualApplicantPartyQuery
        ↓
Add L1GetCurrentIndividualApplicantPartyHandler
        ↓
Use existing current-active individual applicant repository lookup
        ↓
Add GET /api/l1/applicant-parties/current-individual to L1Controller
        ↓
Return:
  200 exists=true + applicant data
  200 exists=false + null applicantParty
  401 if not authenticated
        ↓
Add integration tests
        ↓
Regenerate/check OpenAPI
        ↓
Regenerate/check client API types
```

Likely changed files:

```text
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Queries/L1GetCurrentIndividualApplicantPartyQuery.cs
EnergyManagement.Server/L1/Application/Queries/L1GetCurrentIndividualApplicantPartyHandler.cs
EnergyManagement.Server/L1/Controllers/L1Controller.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Shared register sync:

```text
slice-questions-register.md
  - missing applicant response shape
  - current active applicant uniqueness invariant

slice-extension-points-register.md
  - verificationStatus in Account page applicant read model

slice-implementation-notes-register.md
  - possible future AsNoTracking read-specific query
```

Checks:

```powershell
dotnet build EnergyManagement.Server/EnergyManagement.Server.csproj
dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd --prefix energymanagement.client run generate:api-types
npm.cmd run check:api
```

## 8. Scenario Flow

```text
Authenticated client opens Account page
        ↓
System identifies current client account from L1 auth session
        ↓
System searches for current active individual applicant party
        ↓
If current applicant party exists, system returns applicant data
        ↓
Account page shows applicant data as read-only fields
```

```text
Authenticated client opens Account page
        ↓
System identifies current client account from L1 auth session
        ↓
System searches for current active individual applicant party
        ↓
If current applicant party is missing, system returns missing applicant state
        ↓
Account page shows create applicant party form
```

## 9. Behavior Items

### Account page needs current applicant state

The authenticated client opens Account page.

The page needs to know whether current individual applicant party data already exists.

### System identifies current account from auth session

The system identifies the current client account from the authenticated L1 session.

The client does not submit account identity.

### System searches for current active individual applicant party

The system searches for the current active individual applicant party of the authenticated account.

### Existing applicant data is returned

If current applicant party exists, the system returns applicant data for Account page display.

### Missing applicant party is returned as page state

If current applicant party is missing, the system returns a missing-applicant state.

This is not a command failure.

### Applicant data can include verification state

Returned applicant data includes verification state so the Account page can show current applicant party status.

### Read operation has no side effects

Reading current applicant party does not create, update, deactivate or verify applicant party data.
