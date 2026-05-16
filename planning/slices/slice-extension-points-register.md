# Slice Extension Points Register

Status: active / validation and My Requests filters synchronized

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-APPL-MULTI-001` | ApplicantParty storage | many saved ApplicantParties over time. | accepted |
| `CP-APPL-DEFAULT-001` | default template | one current/default per applicant type. | accepted |
| `CP-APPL-DEFAULT-002` | create behavior | first-of-type/default switching needs explicit default-template model; not implemented by standalone create. | future review |
| `CP-APPL-SVC-001` | application service | shared service exists, no SaveChanges; outer handlers commit. | implemented for standalone create |
| `CP-REQ-APPL-CONTEXT-001` | request API | explicit Existing/New applicant context. | implemented/planned depending on current code branch; slice owns reconciliation |
| `CP-REQ-APPL-PICKER-001` | request UI | future picker can choose any owned saved ApplicantParty. | future review |
| `CP-REQ-APPL-ATOMIC-001` | transaction | New applicant + request committed together. | target direction |
| `CP-REQ-APPL-SNAPSHOT-001` | history/read model | snapshot/reference unresolved. | future review |
| `CP-APPL-DELETE-001` | management | delete/archive/hide semantics unresolved. | future review |
| `CP-MYREQ-FILTER-001` | My Requests filters | status is first filter in extensible filter model. | accepted |
| `CP-MYREQ-FILTER-002` | My Requests URL state | page owns URL query params; filter UI does not read location directly. | accepted |
| `CP-MYREQ-FILTER-003` | My Requests query/cache | query key includes filter object, so no manual refetch is needed for normal filter changes. | accepted |
| `CP-MYREQ-FILTER-004` | future filters | requestType/date/search remain future until backend/source support exists. | future review |
| `CP-MYREQ-DETAIL-001` | request details | create-new-from-feedback is future details/request creation behavior. | future review |
| `CP-VALIDATION-001` | L1 validation mechanism | first pass uses manual controller-level validators matching legacy pattern. | accepted |
| `CP-VALIDATION-002` | future validation mechanism | shared validation pipeline/filter may replace manual pattern after first pass stabilizes. | future review / possible ADR |
| `CP-VALIDATION-003` | ProblemDetails field/code semantics | field names should be API JSON names; stable codes remain future hardening per policy note. | accepted/future hardening |
| `CP-VALIDATION-004` | handler cleanup | remove handler DTO-shape duplication after FluentValidation covers it. | target direction |
