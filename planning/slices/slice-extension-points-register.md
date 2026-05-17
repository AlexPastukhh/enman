# Slice Extension Points Register

Status: active / L2 Employee Review client command extension points synchronized

## 1. L2 Employee Review Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-L2-EMP-REQ-READ-001` | Employee request reads | List/dashboard read is owned by `SL-EMP-REQ-001`; details read is owned by `SL-EMP-REQ-002`; client read UI belongs to `L2-EMP-DASH-001.client` and `L2-EMP-DETAILS-001.client`. | drafted |
| `CP-L2-EMP-VIS-001` | temporary Employee visibility | First pass: all active Employees can see all review-relevant requests. EmployeeId is used for current-vs-another review labels, not list filtering. | accepted temporary policy |
| `CP-L2-EMP-VIS-002` | future Employee visibility | Department/region/assignment/personal-queue visibility can narrow read endpoints later. | future slice |
| `CP-L2-REVIEW-START-001` | StartReview client action | `L2-REVIEW-START-001.client` owns only Start Review button/action/mutation, composed through Employee details action slot. | full sidecar draft |
| `CP-L2-REVIEW-START-002` | StartReview response | `StartReviewResponseDto` is compact command result and must not be used as Employee request details DTO. | accepted |
| `CP-L2-REVIEW-START-003` | Details refresh | StartReview success refreshes details read state and dashboard/list state if present. | accepted |
| `CP-L2-REVIEW-START-004` | Stale state | If another Employee starts review between read and click, server rejects and client shows conflict/lifecycle feedback then refreshes details. | accepted direction |
| `CP-L2-REVIEW-APPROVE-001` | Approve Review | Approve is separate backend/client command slice; not part of StartReview or details read sidecar. | future draft |
| `CP-L2-REVIEW-REJECT-001` | Reject Review | Reject + rejection feedback is separate backend/client command slice; not part of StartReview or details read sidecar. | future draft |
| `CP-L2-REVIEW-CSRF-001` | Unsafe review commands | Start/Approve/Reject are unsafe browser commands and consume shared `CC-CSRF-001`; no local CSRF mechanics in feature sidecars. | accepted |
| `CP-L2-REVIEW-A11Y-001` | Command UX/accessibility | Pending state, disabled reason and error feedback must be visible/accessible in command feature UI. | implementation consideration |
| `CP-L2-REVIEW-QUEUE-001` | assignment/queue | Employee assignment/queue/lock semantics are future and not owned by StartReview first pass beyond review started state. | future slice |
| `CP-L2-REVIEW-HISTORY-001` | review history/audit | Detailed review history/audit display is future details refinement. | future slice |

## 2. ApplicantParty / L1 Extension Points Still Relevant

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-APPL-MULTI-001` | ApplicantParty storage | many saved ApplicantParties over time. | accepted |
| `CP-APPL-PAGE-001` | ApplicantParty page model | one Applicant Parties page / section contains default/current templates, other saved ApplicantParties and add/action areas. | accepted |
| `CP-APPL-READ-001` | ApplicantParty read API | flat `applicantParties[]` list; client groups by `isCurrentDefault`. | implemented backend / client-consumable |
| `CP-APPL-DEFAULT-001` | default template | one current/default per applicant type; top page area highlights current/default templates. | accepted |
| `CP-APPL-DEFAULT-002` | create behavior | first account+type can initialize current/default; additional same-type create remains non-default. | implemented |
| `CP-APPL-DEFAULT-003` | explicit default action backend | make current/default backend command is implemented. | implemented backend |
| `CP-APPL-DEFAULT-004` | explicit default action client | client same-page action belongs to `SL-APPL-003.client`; use feature button/action + entity action slot. | full sidecar draft |
| `CP-APPL-ACTION-SLOT-001` | entity card/list extensibility | entity display UI may expose optional action slot/render prop without owning command logic. | accepted |
| `CP-APPL-NAMING-001` | persisted marker naming | current code uses `IsCurrentActiveVersion`; API/docs use `isCurrentDefault` / current-default template. Rename is later cleanup. | cleanup later |
| `CP-APPL-LIFECYCLE-001` | delete/archive lifecycle | delete/archive/edit/version remains future lifecycle work. | future review |
| `CP-REQ-CLIENT-001` | saved ApplicantParty selector | request creation Existing branch uses saved ApplicantParty selector; current/default is initial selection only. | implemented/drafted |
| `CP-REQ-CLIENT-002` | post-create navigation | My Requests handoff preferred until command returns requestId. | accepted |
| `CP-SERVER-TEST-001` | backend command tests | prefer API/integration + DB state assertions over mocks as primary behavior proof for state-changing L1/L2 server slices. | accepted |
