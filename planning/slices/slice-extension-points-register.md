# Slice Extension Points Register

Status: active / L1 current implementation status and future extension points synchronized

## 1. ApplicantParty Extensions

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-APPL-PAGE-001` | Applicant Parties page model | One Applicant Parties page/section contains current/default templates, other saved ApplicantParties, add action and future/default action. | accepted |
| `CP-APPL-READ-001` | account read | `GET /api/l1/applicant-parties` returns flat `applicantParties[]`; client groups by `isCurrentDefault`. | implemented backend |
| `CP-APPL-DEFAULT-001` | create default initialization | First account+ApplicantPartyType create initializes current/default; additional same-type create remains non-default. | implemented backend |
| `CP-APPL-DEFAULT-003` | explicit make default/current | Backend command implemented; client action remains next L1 gap. | backend implemented / client pending |
| `CP-APPL-COMPAT-001` | current-individual endpoint | Old narrow current-individual endpoint remains compatibility support until target page replacement is stable. | future cleanup decision |
| `CP-APPL-LIFECYCLE-001` | delete/archive/edit lifecycle | Keep out of current L1 finish; future lifecycle slices. | future review |
| `CP-APPL-TYPES-001` | extra ApplicantParty types | LegalEntity / IndividualEntrepreneur create/read/UI support remains future. | future review |

## 2. Request Creation Extensions

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-REQ-CLIENT-001` | saved ApplicantParty selector | Initial request creation UI implemented with simple saved ApplicantParty selector; richer search/filter/sort remains future refinement. | implemented baseline / future refinement |
| `CP-REQ-CLIENT-002` | post-create navigation | My Requests handoff is current because command success has no required requestId body; direct details navigation can be revisited if response changes. | accepted / future review |
| `CP-REQ-SNAPSHOT-001` | request/applicant history | Existing requests are not rewritten by default/current changes; deeper snapshot/reference history remains future read/history decision. | future review |

## 3. Server Testing Extension Notes

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-SERVER-TEST-001` | backend command tests | Prefer API/integration + DB state assertions over mocks as primary behavior proof for state-changing L1 server slices. | accepted |
| `CP-SERVER-TEST-002` | make-current-default | Verify ownership rejection, same-type switching, idempotency and no request mutation. | needs verification |

## 4. New Domain Transition

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-NEW-DOMAIN-001` | next domain draft | After remaining L1 gaps are closed or explicitly deferred, move to new domain scenario/domain draft work. | next phase |
