# Slice Extension Points Register

Status: active register / synchronized with applicant template per type target scenario  
Scope: cross-slice extension points, change points, extension pressure, anti-coupling decisions and related extension questions

## 1. Purpose

This register makes planned extension points and extension pressure visible across slices.

It must be checked before starting a parent slice or `.client.md` sidecar.

It does not replace local slice sections.

It also does not replace:

```text
planning/slices/slice-questions-register.md
```

Use the slice questions register as the shared overview of currently relevant local slice questions.

Use this extension register when a question is tied to extension/change pressure, anti-coupling or future slices.

## 2. Intake Rule

Before starting a slice/client sidecar:

```text
1. Search this register by parent slice, future slice, scenario and layer.
2. Search `slice-questions-register.md` for related local questions.
3. Check whether relevant extension pressure affects current implementation.
4. Decide whether current work needs explicit seam, anti-coupling only, convention-first, ignore, or revisit.
5. Record the decision locally in the slice/client file.
6. Update this register when the decision can affect future slices.
7. Update `slice-questions-register.md` when a local/cross-slice question status changes.
```

## 3. Extension Points Coverage

| ID | Parent slice | Future extension slice | Layer | Current seam | Certainty | Time horizon | Covered locally? | Status |
|---|---|---|---|---|---|---|---|---|
| EP-APPL-TEMPLATE-001 | SC-10 / applicant slices | Per-type current/default ApplicantParty template | Server + Client | current/default template marker scoped by applicant type | high | next applicant design pass | scenario sources | accepted direction |
| EP-APPL-MY-LIST-001 | SC-10B | My Applicant Parties management | Server + Client | saved ApplicantParty list/details/add/edit/delete/archive/set-default | medium/high | future | scenario sources | planned |
| EP-APPL-SELECT-001 | SC-04 / request client | Select from all saved ApplicantParties during request creation | Server + Client | current/default template remains initial prefill/default selection | medium | future request UI | scenario sources | future review |
| EP-APPL-VERIFY-READ-001 | SC-10 / request review | Applicant verification UI / verification workflow | Server + Client | new ApplicantParty starts NotVerified; verification in request/review context | medium/high | near future UI/read/review | scenario sources | accepted direction |
| EP-APPL-DELETE-001 | SC-10B | ApplicantParty delete/archive/hide | Server + Client | warning-driven destructive action; hard delete only if safe | medium | later | scenario sources | future review |
| EP-APPL-HISTORY-001 | SC-04 / SC-10 / details | Applicant snapshot/version/reference policy | Server + Client | request must preserve submitted applicant context | high | before edit/details hardening | scenario sources | future review |
| EP-REQ-APPROVED-001 | SL-REVIEW-001 | Agreement proposal creation | Server + Client | Approved request status / approved request details | high | next layer / near future | to confirm in slice | planned |
| EP-REQ-DOCS-001 | SL-REQ-001 | Request documents | Server + Client | RequestId / request context | medium | later | to confirm in slice | planned |
| EP-NOTIFY-001 | SL-REVIEW-001 / SL-REVIEW-002 | Notifications after approve/reject | Server + Client | review decision result | medium | later | to confirm in slice | planned |

## 4. Extension Pressure / Anti-Coupling Decisions

| ID | Related extension point | Affected current slice | Layer | Pressure importance | Probability | Time horizon | Discussed? | Decision | Anti-coupling constraint | Trade-off | Revisit when |
|---|---|---|---|---|---|---|---|---|---|---|---|
| EPRESS-APPL-TEMPLATE-001 | EP-APPL-TEMPLATE-001 | applicant read/create/request slices | Server + Client | high | high | next applicant/request work | yes | Target scenario uses current/default per applicant type | Do not keep writing future scenarios as one global current active ApplicantParty | Current implementation may remain narrow until refactor | before changing applicant APIs |
| EPRESS-APPL-NEW-REQ-001 | EP-APPL-SELECT-001 | request creation client/API | Server + Client | high | medium/high | request client work | yes | New applicant data in request creates new ApplicantParty and uses it for request | Do not model this as hidden replacement or in-place mutation of existing ApplicantParty | API contract may need significant redesign | before request client/API implementation |
| EPRESS-APPL-SET-DEFAULT-001 | EP-APPL-TEMPLATE-001 | request creation client/account page | Client + Server | medium | high | request UI/account UI | yes | Offer to make newly created ApplicantParty current/default for its type | Do not silently replace user’s template without visible UI decision | More UI complexity | before request UI implementation |
| EPRESS-APPL-HISTORY-001 | EP-APPL-HISTORY-001 | request details / applicant edit | Server + Client | high | medium | before edit/details hardening | yes | Preserve submitted applicant context | Do not allow edit/delete behavior to rewrite historical request meaning | May require snapshot/version/reference policy | before applicant edit/delete and request details enrichment |
| EPRESS-APPL-DELETE-001 | EP-APPL-DELETE-001 | My Applicant Parties | Server + Client | medium/high | medium | later | yes | Prefer warning-driven archive/hide unless hard delete is safe | Do not hard-delete ApplicantParty used by important request history without explicit policy | Future management is more complex | before delete/archive slice |
| EPRESS-APPL-VERIFY-001 | EP-APPL-VERIFY-READ-001 | applicant/request review slices | Server + Client | medium | medium/high | near future | yes | New profiles start NotVerified; verification in review context | Do not verify in standalone applicant data save | Requires review UI/status clarity | before verification work |
| EPRESS-APPROVAL-001 | EP-REQ-APPROVED-001 | SL-REVIEW-001 | Server + Client | high | high | next layer / near future | yes | Anti-coupling only now | Approval must not auto-create agreement proposal; approve client must not import/couple to agreement proposal feature | User will need separate future action; current flow stays simpler and clearer | before agreement proposal slice |
| EPRESS-REQ-DOCS-001 | EP-REQ-DOCS-001 | SL-REQ-001 | Server + Client | medium | medium | later | yes | Anti-coupling only now | Request creation must not require documents in L1 and should leave request context usable for later documents slice | Later document workflow may need extra UI/API | before documents slice |

## 5. Change Points Coverage

| ID | Affected slice | Layer | Behavior aspect | Change point owner | Current decision | Configurable now? | Tests affected | Status |
|---|---|---|---|---|---|---|---|---|
| CP-APPL-PER-TYPE-CURRENT-001 | SC-10 / applicant slices | Server + Client | current/default applicant template | applicant template policy | at most one current/default ApplicantParty per applicant type | no | future applicant read/template tests | accepted direction |
| CP-APPL-REQUEST-NEW-001 | SC-04 / request creation | Server + Client | new applicant data during request | request/applicant API policy | accepted new applicant data creates new ApplicantParty and uses it for request | no | future request/applicant integration tests | accepted direction |
| CP-APPL-SET-DEFAULT-001 | SC-04 / SC-10 | Client + Server | set current/default after new profile | UI/API policy | offer to make new ApplicantParty current/default for its type | maybe | future client tests | assumption |
| CP-APPL-SELECT-ALL-001 | SC-04 | Client + Server | select from all saved ApplicantParties | future request UI | current/default is initial prefill; dropdown/list is future | no | future client/API tests | future review |
| CP-APPL-HISTORY-001 | request details / applicant edit | Server | request applicant context preservation | domain/read policy | request must preserve submitted applicant context | no | future details/edit tests | future review |
| CP-APPL-DELETE-001 | SC-10B | Server + Client | delete/archive/hide ApplicantParty | safety/history policy | warning-driven; hard delete only if safe | no | future management tests | future review |
| CP-REQ-LIST-PAGING-001 | SL-REQ-002 | Server + Client | My Requests paging | list/read contract | no paging in first server read slice; add only when UX/data volume requires it | no | API/client list tests later | future review |
| CP-REQ-LIST-FILTER-001 | SL-REQ-002 | Server + Client | My Requests filters | list/read contract | status filter only; request type/text/date filters are future additions | no | API/client list tests later | future review |
| CP-REQ-DETAILS-001 | SL-REQ-002 / SL-REQ-003 | Server + Client | List summary vs selected request details | read contract split | list endpoint returns summary only; details belong to `SL-REQ-003` | no | details API/client tests later | active |
| CP-REQ-DETAILS-APPLICANT-001 | SL-REQ-003 | Server + Client | Applicant snapshot in request details | details/read contract | no applicant snapshot in first cut; revisit after applicant history policy | no | details API/client tests later | future review |
| CP-ERROR-MAPPING-001 | multiple slices | Server + Client | Domain/application error to HTTP/client display | response mapper + client error mapper | stable problem/error mapping | later | integration/client error tests | active |
| CP-FORM-VALIDATION-001 | form sidecars | Client | Deferred validation behavior | CL-FORM-VALIDATION-001 / form hooks | deferred after input, immediate on submit | maybe | client form tests | active |

## 6. Questions Across Slices

This section contains extension/change-related questions.

For the broader shared overview of local slice questions, use:

```text
planning/slices/slice-questions-register.md
```

| ID | Related slice(s) | Related EP/CP | Question | Assumption | Why it matters | Blocks current work? | Status |
|---|---|---|---|---|---|---|---|
| Q-EP-APPL-001 | SC-10 / applicant slices | CP-APPL-PER-TYPE-CURRENT-001 | Is current/default per account or per type? | Per applicant type. | Determines prefill and applicant read model. | yes for future applicant redesign | accepted direction |
| Q-EP-APPL-002 | SC-04 / request client | CP-APPL-REQUEST-NEW-001 | What happens when new applicant data is entered during request creation? | Create new ApplicantParty and use it for request. | Determines API and request/applicant transaction. | yes before target request implementation | accepted direction |
| Q-EP-APPL-003 | SC-04 / Account page | CP-APPL-SET-DEFAULT-001 | Should newly created ApplicantParty become current/default? | Offer to make it current/default for its type. | Affects user control and future prefill. | yes before client implementation | assumption |
| Q-EP-APPL-004 | SC-10B | CP-APPL-DELETE-001 | Can ApplicantParty be deleted? | Future warning-driven archive/hide unless safe. | Affects history and request integrity. | no now | future review |
| Q-EP-APPL-005 | Request details / applicant edit | CP-APPL-HISTORY-001 | Reference, version or snapshot for request applicant context? | Preserve submitted context; exact mechanism future. | Affects edit/delete and request details. | no now; yes before edit/details enrichment | future review |
| Q-EP-001 | SL-REVIEW-001, agreement proposal slice | EP-REQ-APPROVED-001 / EPRESS-APPROVAL-001 | Should approval create agreement proposal automatically? | No. Agreement proposal starts by separate employee action on Approved request. | Prevents coupling approval to agreement proposal. | no for current docs; yes if agreement behavior changes | accepted direction |

## 7. Status Values

```text
planned
candidate
active
accepted direction
assumption
future review
open for future
resolved
superseded
ignored
```
