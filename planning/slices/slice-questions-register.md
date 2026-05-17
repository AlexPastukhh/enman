# Slice Questions Register

Status: active / L1 current implementation status and remaining gaps synchronized

## 1. Current Accepted / Implemented Decisions

| ID | Local file(s) | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|---|
| `SL-APPL-Q-006` | `SC-10` / `SC-10B` / `SL-APPL-*` | page model | accepted direction | Is ApplicantParty management split into two current pages? | No. One Applicant Parties page/section; SC-10B is same-page future management addendum. | Scenario wording and client placement. |
| `SL-APPL-002-Q-IMPL` | `SL-APPL-002` | backend read | implemented | Is account ApplicantParties flat read implemented? | Yes. `GET /api/l1/applicant-parties` returns flat `applicantParties[]`. | Backend status/current docs. |
| `SL-APPL-003-Q-IMPL` | `SL-APPL-003` | backend command | implemented | Is explicit make current/default implemented on backend? | Yes. `POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default`. | Backend status/current docs. |
| `SL-REQ-001-CLIENT-Q-IMPL` | `SL-REQ-001.client` | client command | implemented | Is request creation UI implemented? | Yes. `/requests/create` route/page/form exists with Existing/New applicant context. | Client status/current docs. |
| `SL-REQ-Q-APPL-001` | `SL-REQ-001` | request applicant context | implemented | Does request creation rely on one current active ApplicantParty? | No. Explicit Existing/New context. | Request creation API/client behavior. |
| `SL-REQ-Q-APPL-004` | `SL-REQ-001.client` | existing applicant | implemented | Can selected Existing applicant be non-default? | Yes. Any owned saved ApplicantParty. | Selector/DTO behavior. |
| `CL-LAYER-Q-001` | client sidecars | shared API | accepted | Why are multiple entity API fetch wrappers in shared/api? | `shared/api` is the low-level client/server boundary layer. | Prevents unnecessary architecture churn. |

## 2. Remaining L1 Questions / Gaps

| ID | Local file(s) | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|---|
| `SL-APPL-003-CLIENT-Q-001` | future `SL-APPL-003.client` | client action | open / next L1 gap | How should make default/current button/action be implemented on client? | Add client action on Applicant Parties page/section using backend endpoint. | Remaining L1 client work. |
| `SL-APPL-002-CLIENT-Q-STATUS` | `SL-APPL-002.client` / AccountPage | client page replacement | open / implementation gap | Has old AccountPage current-individual flow been replaced by target flat page/section? | Not confirmed. Treat as remaining gap until code proves target replacement. | L1 finish before new domain draft. |
| `SL-APPL-003-TEST-Q-001` | `SL-APPL-003` | tests | needs verification | Are make-current-default API integration tests present? | Not confirmed. Verify/add tests for ownership, same-type switching, idempotency and no request mutation. | Test confidence. |
| `SL-APPL-COMPAT-Q-001` | current-individual endpoint | compatibility | future review | What happens to old current-individual endpoint? | Keep until target page replacement is stable; cleanup decision later. | Compatibility cleanup. |

## 3. Future / Not Current L1

| ID | Area | Status | Direction |
|---|---|---|---|
| `SL-APPL-LIFECYCLE-Q-001` | delete/archive/edit | future review | Future lifecycle slices only. |
| `SL-APPL-TYPES-Q-001` | LegalEntity / IndividualEntrepreneur | future review | Future type-specific create/read/UI slices. |
| `NEW-DOMAIN-Q-001` | next domain draft | future after L1 finish | Move after remaining L1 gaps are closed or explicitly deferred. |
