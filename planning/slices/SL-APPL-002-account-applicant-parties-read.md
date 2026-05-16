# SL-APPL-002 — Account Applicant Parties Read / Templates

Status: implementation-ready backend/API read-model full draft  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Parties Page`, `SC-10B Applicant Parties Future Management`  
Slice type: backend/server read slice  
Current implementation status: target endpoint is not confirmed implemented; current repo has old narrow `GET /api/l1/applicant-parties/current-individual` behavior.

## 1. Slice Overview

SL-APPL-002 adds a protected backend endpoint that returns all `ApplicantParty` records owned by the current authenticated account.

Purpose:

```text
Unblock the single Applicant Parties page / section on the client.
```

Target endpoint:

```text
GET /api/l1/applicant-parties
```

The backend returns one flat account-owned list.

The client decides how to visually group that list on the page:

```ts
const currentDefaults = applicantParties.filter(x => x.isCurrentDefault);
const otherSaved = applicantParties.filter(x => !x.isCurrentDefault);
```

Backend returns facts, not page layout sections:

```text
- all owned ApplicantParties;
- applicantPartyType;
- card display data;
- verificationStatus;
- isCurrentDefault marker.
```

`isCurrentDefault` is mapped from current persisted marker `IsCurrentActiveVersion` for now.

The domain field name is legacy/current-implementation wording. In this scenario it means:

```text
current/default template
```

Rename of the persisted/domain field is not part of this slice.

## 2. Sources / Source Behavior Items

Scenario / UI / behavior sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/api/client-server-contract-principles.md
```

Relevant source behavior/UI items:

```text
SC-10-BI-004 — Account may store multiple ApplicantParties over time.
SC-10-BI-005 — Account may have one current/default ApplicantParty template per applicant type.
SC-10-BI-006 — First ApplicantParty of a type may initialize current/default template for that type.
SC-10-BI-007 — Additional ApplicantParty of the same type does not change current/default implicitly.
SC-10-BI-009 — Existing requests are not changed by ApplicantParty creation or default/current changes.
SC-10-UI-002 — Applicant Parties page shows default/current templates separately from other saved ApplicantParties.
SC-10-UI-003 — Current/default templates are visually highlighted.
SC-10-UI-004 — Applicant Parties page shows other saved ApplicantParties below the default/current area.
```

## 3. Scope / Out of Scope / Related Slices

### Scope

```text
- protected account-level ApplicantParty read endpoint;
- return all ApplicantParties owned by the authenticated account;
- include current/default marker per item;
- include enough data for client cards;
- return 200 with empty list when none exist;
- do not expose clientAccountId;
- do not mutate data;
- support Individual ApplicantParty first while keeping DTO type-extensible.
```

### Out of scope

| Out-of-scope item | Owner / destination |
|---|---|
| Create ApplicantParty | `SL-APPL-001 — Create Individual ApplicantParty` |
| Explicit make default/current action | `SL-APPL-003 — Select Current/Default ApplicantParty Template` |
| Request creation applicant context | `SL-REQ-001 — Create Connection Request` |
| Client UI implementation | `SL-APPL-001.client` for current add flow; future dedicated client sidecar when concrete read-page implementation starts |
| Separate “My Applicant Parties” page | Not planned as a separate current scenario |
| Delete/archive lifecycle | Future ApplicantParty lifecycle slice(s) |
| Edit ApplicantParty | Future edit/version lifecycle slice |
| Legal entity / entrepreneur creation | Future type-specific create slices unless already supported by current implementation |
| Replacing old current-individual endpoint | Cleanup / compatibility decision after new read endpoint is implemented |
| Domain field rename `IsCurrentActiveVersion` | Cleanup/default-template naming task |
| OpenAPI/client generated artifact updates | Implementation-time generation/check step, not documentation-only work |

### Related slices

```text
SL-APPL-001 — Create Individual ApplicantParty
Creates ApplicantParty. First-of-type may initialize current/default; additional same-type creates do not silently switch current/default.

SL-APPL-002 — Account Applicant Parties Read / Templates
Returns all ApplicantParties for current account with isCurrentDefault marker.

SL-APPL-003 — Select Current/Default ApplicantParty Template
Future explicit action for changing default/current.

SL-REQ-001 — Create Connection Request
Uses Existing/New ApplicantParty context during request creation.
```

## 4. Corrected Scenario Direction

There is one Applicant Parties page / section, not two separate current user scenarios.

Not this:

```text
Account page applicant section
My Applicant Parties page
```

Correct model:

```text
Applicant Parties page / section
        ↓
Top visual area:
  current/default ApplicantParty templates
  highlighted with border/visual marker
        ↓
Other saved ApplicantParties:
  all saved parties not selected as current/default
        ↓
Same page:
  add ApplicantParty action/form
        ↓
Future:
  explicit make default/current action
        ↓
Far future:
  delete/archive lifecycle
```

Backend does not return layout sections.

Backend returns facts:

```text
- all owned ApplicantParties;
- applicantPartyType;
- card display data;
- verificationStatus;
- isCurrentDefault marker.
```

Client groups the flat list by `isCurrentDefault`.

## 5. Business Rules

```text
- ApplicantParty creation is additive.
- Creating a second ApplicantParty of the same applicant type does not silently change default/current.
- If no ApplicantParty of that type exists, the first one may become current/default.
- If same-type ApplicantParty already exists, the new one is not current/default.
- Changing default/current is an explicit future action.
- Existing requests are not changed by ApplicantParty creation.
- Existing requests are not changed by future default/current changes.
- Default/current means template/prefill/default selection for future flows.
```

This read slice reports persisted state only.

It does not create, repair or infer missing defaults.

## 6. Visual Scenario Flow

```text
Signed-in client opens Applicant Parties page / section
        ↓
System returns all ApplicantParties for current account
        ↓
Each item includes isCurrentDefault marker
        ↓
Client renders:
  top highlighted area = isCurrentDefault true
        ↓
Client renders:
  saved list area = isCurrentDefault false
        ↓
Client also shows add ApplicantParty action/form
```

Empty state:

```text
Signed-in client opens Applicant Parties page / section
        ↓
No ApplicantParties exist
        ↓
Backend returns:
  200 OK
  { applicantParties: [] }
        ↓
Client shows empty state + add action
```

## 7. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Opens Applicant Parties page / section. | SC-10 / UI source | page behavior supported |
| F02 | API/Auth boundary | Derives current L1 account from auth/session. | L1 auth/session boundary | backend/API slice |
| F03 | System | Reads all ApplicantParties owned by current account. | SC-10-BI-004 | in scope |
| F04 | System | Includes persisted current/default marker per item. | SC-10-BI-005 / SC-10-BI-007 | in scope |
| F05 | API | Returns one flat `applicantParties[]` response. | API contract direction | in scope |
| F06 | Client UI | Groups current/default vs other saved cards. | SC-10-UI-002..004 | consumer behavior |
| F07 | Client UI | Shows add action/form on same page. | SC-10 UI direction | related to `SL-APPL-001.client` |
| F08 | Client UI/API | Explicit make default/current action. | SC-10B future management | future `SL-APPL-003` |
| F09 | Lifecycle | Delete/archive rules. | SC-10B future management | future lifecycle slice |
| F10 | System | Existing requests remain unchanged. | SC-10-BI-009 | read-only / no mutation |

## 8. Visual Backend Implementation Flow

```text
[HTTP]
GET /api/l1/applicant-parties
        ↓
[Authorize]
requires authenticated L1 account
        ↓
[Controller]
derive accountId from auth claims/session
        ↓
[Query]
L1GetAccountApplicantPartiesQuery(accountId)
        ↓
[Repository / Read model]
load all ApplicantParties owned by account
        ↓
[Mapping]
for each ApplicantParty:
  applicantPartyId
  applicantPartyType
  displayName
  fullName when Individual
  email
  phoneNumber
  verificationStatus
  isCurrentDefault
  createdAt
        ↓
[Response]
200 OK
{
  applicantParties: [...]
}
```

Important:

```text
Read endpoint does not create defaults.
Read endpoint does not repair defaults.
Read endpoint does not mutate ApplicantParties.
Read endpoint does not mutate requests.
Read endpoint only exposes persisted state.
```

## 9. Implementation Flow

| Step | Layer | Responsibility | Notes |
|---|---|---|---|
| I01 | API Controller | Expose protected `GET /api/l1/applicant-parties`. | No request body. |
| I02 | Auth boundary | Resolve current L1 account id from cookie/session claims. | No `accountId` query/body input. |
| I03 | Query | Send `L1GetAccountApplicantPartiesQuery(accountId)`. | Read-only query. |
| I04 | Repository/read model | Load all ApplicantParties owned by account. | Exclude unowned parties. |
| I05 | Mapping | Map applicant type, display fields, verification status, default marker. | Individual first; DTO remains type-extensible. |
| I06 | API response | Return `L1AccountApplicantPartiesResponse`. | Flat list. |
| I07 | No mutation | Do not write ApplicantParty/request/default data. | Read endpoint is no-write. |

## 10. API Contract

Endpoint:

```text
GET /api/l1/applicant-parties
```

Response:

```ts
type L1AccountApplicantPartiesResponse = {
  applicantParties: L1ApplicantPartySummaryDto[];
};
```

Summary DTO:

```ts
type L1ApplicantPartySummaryDto = {
  applicantPartyId: number;
  applicantPartyType: "Individual" | "IndividualEntrepreneur" | "LegalEntity";
  displayName: string;
  fullName?: L1FullNameDto | null;
  email?: string | null;
  phoneNumber?: string | null;
  verificationStatus: string;
  isCurrentDefault: boolean;
  createdAt?: string | null;
};
```

For first implementation, only `Individual` may be returned if only `IndividualApplicantParty` exists today.

Statuses:

```text
200 OK
401 Unauthorized
500 Internal Server Error
```

No `422` is expected because this read endpoint has no request body/query input.

Contract notes:

```text
- Do not expose clientAccountId.
- Return all owned ApplicantParties in one flat list.
- Do not split currentDefaults and other saved parties in API response.
- Client owns page grouping/layout.
- Missing ApplicantParties is normal.
- Empty account returns 200 with applicantParties = [].
- `IsCurrentActiveVersion` is current implementation wording only; API exposes `isCurrentDefault`.
```

## 11. Current Implementation Notes

Current implementation has old narrow read endpoint:

```text
GET /api/l1/applicant-parties/current-individual
```

That endpoint supports old client behavior:

```text
exists=true  -> show one read-only ApplicantParty
exists=false -> show create form
```

SL-APPL-002 supersedes that as the target read model for the new Applicant Parties page / section.

The old endpoint can remain temporarily for compatibility, but new client work should use:

```text
GET /api/l1/applicant-parties
```

`IsCurrentActiveVersion` is current implementation wording.

In this slice it is treated as the persisted marker for:

```text
isCurrentDefault / default template / current template
```

Rename can be a later cleanup/default-template slice.

The API should expose target wording:

```text
isCurrentDefault
```

## 12. Questions / Decisions

| ID | Status | Question | Assumption / current direction | Impact | Shared register / local-only reason |
|---|---|---|---|---|---|
| `SL-APPL-002-Q-001` | accepted direction | One page or separate Account/My Applicant Parties pages? | One Applicant Parties page / section. | Scenario wording, UI sidecar placement, backend read slice scope. | Mirrored as `SL-APPL-Q-006` in `slice-questions-register.md`. |
| `SL-APPL-002-Q-002` | accepted direction | Should backend return grouped arrays? | No. Return one flat `applicantParties[]` list. | API DTO shape and client grouping responsibility. | Mirrored as `SL-APPL-Q-008` in `slice-questions-register.md`. |
| `SL-APPL-002-Q-003` | accepted direction | Who groups defaults vs others? | Client groups by `isCurrentDefault`. | Client page/component logic and API contract. | Mirrored as `SL-APPL-Q-009` in `slice-questions-register.md`. |
| `SL-APPL-002-Q-004` | accepted direction | What is current/default source? | Persisted `IsCurrentActiveVersion`, exposed as `isCurrentDefault`. | API naming and future cleanup naming task. | Mirrored by implementation note `NOTE-APPL-PAGE-002`. |
| `SL-APPL-002-Q-005` | accepted direction | Should read endpoint mutate/fix defaults? | No. Read only. | No-write/read model boundary. | Local and reflected in behavior coverage. |
| `SL-APPL-002-Q-006` | accepted direction | Empty account response? | `200 OK` with `applicantParties: []`. | API contract and client empty state. | Local to this read slice. |
| `SL-APPL-002-Q-007` | accepted direction | Expose `clientAccountId`? | No. Account comes from auth/session. | Security/API minimization. | Local to this read slice. |
| `SL-APPL-002-Q-008` | accepted direction | Include `createdAt`? | Include if available; useful for stable card ordering. | DTO shape and ordering. | Local to this read slice until implementation confirms storage. |
| `SL-APPL-002-Q-009` | accepted direction | Only Individual first? | OK for first implementation; DTO remains type-extensible. | Future applicant types. | Mirrored by extension point `CP-APPL-TYPES-001`. |
| `SL-APPL-002-Q-010` | future review | Make default/current action? | Future `SL-APPL-003`. | Same-page command behavior. | Mirrored by `CP-APPL-DEFAULT-003`. |
| `SL-APPL-002-Q-011` | future review | Delete/archive lifecycle? | Future lifecycle slices only. | Lifecycle/safety rules. | Mirrored by `CP-APPL-LIFECYCLE-001`. |
| `SL-APPL-002-Q-012` | future review | Rename `IsCurrentActiveVersion`? | Later rename to `IsCurrentDefault` / `IsDefaultTemplate`. | Cleanup/default-template naming. | Mirrored by `NOTE-APPL-PAGE-002`. |

## 13. Extension / Change Points

| ID | Type | Area | Current direction | Status |
|---|---|---|---|---|
| `CP-APPL-DEFAULT-003` | future command | make current/default | Explicit make current/default action belongs to `SL-APPL-003`. | future review |
| `CP-APPL-TYPES-001` | extension | applicant types | Individual first; DTO remains extensible for IndividualEntrepreneur and LegalEntity. | future review |
| `CP-REQ-APPL-PICKER-001` | future request creation | applicant picker | User may choose from all saved ApplicantParties; current/default is initial selection only. | future review |
| `CP-APPL-LIFECYCLE-001` | future lifecycle | delete/archive | Deletion may be blocked or warned when ApplicantParty has contracts, InReview requests, Approved pre-contract requests, or active contract-version exchange processes. | future review |
| `CP-APPL-VERIFY-001` | future lifecycle | verification | Verified ApplicantParty may require stricter warning/rules than Unverified. | future review |
| `CP-APPL-NAMING-001` | cleanup | default marker naming | Rename `IsCurrentActiveVersion` later if/when cleanup/default-template naming task is accepted. | future cleanup |

Do not implement delete/archive behavior or tests in this slice.

## 14. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior / source item | Expected behavior | Covered by this slice |
|---|---|---|
| Protected endpoint | Unauthenticated request returns 401. | yes |
| Account ownership | Only current account’s ApplicantParties returned. | yes |
| Empty state | Authenticated account with none gets empty list. | yes |
| `SC-10-BI-004` multiple saved parties | All owned saved parties returned. | yes |
| `SC-10-BI-005` default/current per type | Each item includes `isCurrentDefault`. | yes |
| `SC-10-BI-007` no implicit switch | Existing default and non-default same-type parties can be represented together. | yes |
| Client grouping support | Client can group by `isCurrentDefault`. | yes |
| No duplicate grouping in API | Backend returns one flat list. | yes |
| Summary card data | DTO includes display/card fields. | yes |
| Verification status | Included per item. | yes |
| No `clientAccountId` leak | Response excludes account id. | yes |
| No mutation | Read endpoint does not change data. | yes |
| `SC-10-BI-009` existing requests unchanged | Read endpoint does not touch requests. | yes |
| Explicit make default | Not included. | future |
| Delete/archive | Not included. | future |

## 15. Test / Verification Plan

### API Integration Tests

| Test | Expected |
|---|---|
| unauthenticated request | `401 Unauthorized` |
| authenticated account with no ApplicantParties | `200 OK`, `applicantParties=[]` |
| authenticated account with one first-of-type ApplicantParty | one item with `isCurrentDefault=true` |
| authenticated account with multiple same-type ApplicantParties | all returned; default item has `isCurrentDefault=true`, others false |
| authenticated account with multiple types later | all returned with correct `applicantPartyType` |
| another account’s ApplicantParties exist | not returned |
| verificationStatus included | every summary has status |
| clientAccountId not exposed | response JSON does not include `clientAccountId` |
| createdAt included if contract chooses it | valid value present |
| read endpoint no mutation | optional low-cost assertion only; do not over-test generic no-op |

### Query / Repository Tests

```text
- repository can list all owned ApplicantParties;
- query maps IndividualApplicantParty to summary;
- query maps IsCurrentActiveVersion to isCurrentDefault;
- query does not include unowned parties.
```

### OpenAPI / Generated Checks

```text
- OpenAPI includes GET /api/l1/applicant-parties;
- OpenAPI includes L1AccountApplicantPartiesResponse;
- generated TypeScript includes new response/schema;
- route constants/client API checks updated if repo workflow requires.
```

Generated artifacts are not part of documentation-only work, but implementation of this slice should run the repo’s contract generation/check workflow when API code is changed.

## 16. Client / Consumer Notes

This slice unblocks the new client Applicant Parties page because the client can fetch one account-level list.

Client behavior after this backend slice:

```text
GET /api/l1/applicant-parties
        ↓
applicantParties
        ↓
current/default top area:
  filter isCurrentDefault == true
        ↓
other saved list:
  filter isCurrentDefault == false
        ↓
same page:
  add ApplicantParty form/action
```

Old client currently depends on narrow current-individual behavior.

New client sidecar should replace that with account-level list query.

## 17. Dependent / Follow-up Slices

```text
SL-APPL-001 — Create Individual ApplicantParty
SL-APPL-001.client — Add ApplicantParty on the same Applicant Parties page / section
SL-APPL-003 — Select Current/Default ApplicantParty Template
SL-REQ-001 — Create Connection Request With Applicant Context
future SL-APPL-002.client or Applicant Parties page read sidecar, when concrete client read work starts
future ApplicantParty delete/archive lifecycle slices
future default-marker naming cleanup task
```

## 18. Docs / Register Sync

When this draft changes, keep these in sync:

```text
planning/README.md
planning/planning-workflow-current.md
planning/api/client-server-contract-principles.md
planning/slices/README.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Specific sync rules:

```text
- use one Applicant Parties page / section;
- use target wording default/current template;
- document flat list API response;
- document client grouping responsibility;
- document IsCurrentActiveVersion as current implementation marker only;
- keep delete/archive as future extension;
- do not reintroduce `currentDefaults` / `otherApplicantParties` as API response arrays unless the API decision changes.
```

## 19. Implementation Checklist

```text
[ ] Add L1AccountApplicantPartiesResponse DTO.
[ ] Add L1ApplicantPartySummaryDto DTO.
[ ] Add GET /api/l1/applicant-parties endpoint.
[ ] Add L1GetAccountApplicantPartiesQuery.
[ ] Add handler for account ApplicantParties read.
[ ] Add repository method to list all owned ApplicantParties.
[ ] Map IndividualApplicantParty summary.
[ ] Map IsCurrentActiveVersion -> isCurrentDefault.
[ ] Include verificationStatus.
[ ] Include createdAt if chosen.
[ ] Exclude clientAccountId.
[ ] Return 200 with empty list for none.
[ ] Add integration tests.
[ ] Run OpenAPI/client generated checks.
[ ] Do not implement make default/current.
[ ] Do not implement delete/archive.
[ ] Do not implement client UI.
[ ] Do not rename domain/persistence default marker in this slice.
```

## 20. Draft Decision

Use a flat account-owned ApplicantParty list response.

Backend returns persisted facts:

```text
applicantParties[]
each item has isCurrentDefault
```

Client owns visual grouping:

```text
top highlighted area = isCurrentDefault true
other saved list = isCurrentDefault false
```

This keeps API as read-model data, not page layout structure, and still supports the corrected single Applicant Parties page / section scenario.
