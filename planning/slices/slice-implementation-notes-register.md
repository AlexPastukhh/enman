# Slice Implementation Notes Register

Status: active / L1 current implementation status and remaining gaps synchronized

## 1. Current Implemented Notes

| ID | Related slice | Layer | Note | Status |
|---|---|---|---|---|
| `NOTE-APPL-002-BE-001` | `SL-APPL-002` | backend/API | `GET /api/l1/applicant-parties` is implemented and returns flat `applicantParties[]` with `isCurrentDefault`. | implemented |
| `NOTE-APPL-003-BE-001` | `SL-APPL-003` | backend/API | `POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default` is implemented. | implemented |
| `NOTE-APPL-003-BE-002` | `SL-APPL-003` | backend behavior | Handler loads selected owned ApplicantParty, unsets other same-type current/default parties, marks selected current/default and saves. | implemented |
| `NOTE-REQ-CLIENT-001` | `SL-REQ-001.client` | client architecture | Request creation UI is implemented as command sidecar: route/page + feature form/model + entities + shared/api. | implemented |
| `NOTE-REQ-CLIENT-002` | `SL-REQ-001.client` | applicant selector | Existing branch uses saved ApplicantParty selector; current/default is initial selection only. | implemented |
| `NOTE-REQ-CLIENT-003` | `SL-REQ-001.client` | success UX | Success invalidates My Requests and navigates to My Requests. | implemented |
| `NOTE-MYREQ-CLIENT-001` | My Requests sidecars | client | My Requests list, filters and details are implemented first-stage client flows. | implemented |
| `NOTE-API-GEN-001` | `CC-API-001` | generated artifacts | After API source changes, run `generate:openapi`, `generate:api-types`, stage generated artifacts, then run `check:api`. | accepted |

## 2. Remaining L1 Implementation Notes

| ID | Related slice | Layer | Note | Status |
|---|---|---|---|---|
| `NOTE-APPL-003-CLIENT-001` | future `SL-APPL-003.client` | client | Add make default/current client button/action using backend endpoint. | remaining L1 gap |
| `NOTE-APPL-003-CLIENT-002` | future `SL-APPL-003.client` | shared API | Add client shared API wrapper/path for make-current-default if still missing at implementation time. | remaining L1 gap |
| `NOTE-APPL-002-CLIENT-002` | `SL-APPL-002.client` / AccountPage | client | Replace old current-individual AccountPage UI with target flat Applicant Parties page/section if not already done. | remaining L1 gap |
| `NOTE-APPL-003-TEST-001` | `SL-APPL-003` | tests | Confirm/add API integration tests for make-current-default: ownership, same-type switching, idempotency, no request mutation. | needs verification |
| `NOTE-APPL-COMPAT-001` | current-individual endpoint | cleanup | Keep old current-individual endpoint until target page replacement is stable, then decide cleanup. | future review |

## 3. Future / New Domain Notes

| ID | Area | Note | Status |
|---|---|---|---|
| `NOTE-APPL-LIFECYCLE-001` | ApplicantParty lifecycle | Delete/archive/edit lifecycle remains future, not current L1 finish. | future review |
| `NOTE-APPL-TYPES-001` | ApplicantParty types | LegalEntity / IndividualEntrepreneur creation remains future type-specific work. | future review |
| `NOTE-NEW-DOMAIN-001` | new domain draft | Once remaining L1 gaps are closed or deferred, move to new domain scenario/domain draft. | next phase |
