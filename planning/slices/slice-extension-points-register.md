# Slice Extension Points Register

Status: active / validation, My Requests filters and ApplicantParty one-page direction synchronized

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-APPL-MULTI-001` | ApplicantParty storage | many saved ApplicantParties over time. | accepted |
| `CP-APPL-PAGE-001` | ApplicantParty page model | one Applicant Parties page / section contains default/current templates, other saved ApplicantParties and add action. | accepted |
| `CP-APPL-DEFAULT-001` | default template | one current/default per applicant type; top page area highlights current/default templates. | accepted |
| `CP-APPL-DEFAULT-002` | create behavior | first-of-type/default initialization may happen; additional same-type create must not switch default/current silently. | accepted |
| `CP-APPL-DEFAULT-003` | explicit default action | make default/current is future explicit same-page action, owned by `SL-APPL-003`. | future review |
| `CP-APPL-SVC-001` | application service | shared service exists, no SaveChanges; outer handlers commit. | implemented for standalone create |
| `CP-APPL-LIFECYCLE-001` | delete/archive lifecycle | future lifecycle slice only; if ApplicantParty has contracts, InReview requests, Approved pre-contract requests or active contract-version exchange processes, deletion/archive may be blocked or require strong warning/cancel/stop semantics; verified parties may need stricter rules than unverified. | future review |
| `CP-REQ-APPL-CONTEXT-001` | request API | explicit Existing/New applicant context. | implemented/planned depending on current code branch; slice owns reconciliation |
| `CP-REQ-APPL-PICKER-001` | request UI | future picker can choose any owned saved ApplicantParty; default/current is initial prefill only. | future review |
| `CP-REQ-APPL-ATOMIC-001` | transaction | New applicant + request committed together. | target direction |
| `CP-REQ-APPL-SNAPSHOT-001` | history/read model | snapshot/reference unresolved; existing requests must not be silently changed by ApplicantParty create/default actions. | future review |
| `CP-MYREQ-FILTER-001` | My Requests filters | status is first filter in extensible filter model. | accepted |
| `CP-MYREQ-FILTER-002` | My Requests URL state | page owns URL query params; filter UI does not read location directly. | accepted |
| `CP-MYREQ-FILTER-003` | My Requests query/cache | query key includes filter object, so no manual refetch is needed for normal filter changes. | accepted |
| `CP-MYREQ-FILTER-004` | future filters | requestType/date/search remain future until backend/source support exists. | future review |
| `CP-MYREQ-DETAIL-001` | request details | create-new-from-feedback is future details/request creation behavior. | future review |
| `CP-VALIDATION-001` | L1 validation mechanism | first pass uses manual controller-level validators matching legacy pattern. | accepted |
| `CP-VALIDATION-002` | future validation mechanism | shared validation pipeline/filter may replace manual pattern after first pass stabilizes. | future review / possible ADR |
| `CP-VALIDATION-003` | ProblemDetails field/code semantics | field names should be API JSON names; stable codes remain future hardening per policy note. | accepted/future hardening |
| `CP-VALIDATION-004` | handler cleanup | remove handler DTO-shape duplication after FluentValidation covers it. | target direction |
