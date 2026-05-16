# Slice Questions Register

Status: active register / synchronized with applicant per-type template scenario direction  
Scope: shared overview of currently relevant local slice, client sidecar and cross-cutting/helper questions, future-review items and important accepted directions

## 1. Purpose

This register makes important local slice questions visible from one place.

Local `Questions / Decisions` sections keep detailed context.

Implemented slices can still have open questions.

Implementation status does not close future UX/security/cache/API/domain questions.

If a question remains relevant after local implementation, mirror it here.

## 2. What This Register Is Not

This register is not the source for Scenario Flow or Behavior Items.

Use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

for scenario text/DATA/UI/behavior item sources.

## 3. Required Fields

Every row must make the question state explicit:

```text
ID
Local file(s)
Area
Question status
Question / decision question
Assumption / current direction
Impact / shared target
Blocks current work?
```

Status values:

```text
open
blocked
assumption
accepted direction
future review
deferred
resolved
superseded
local only
```

Do not leave an open question without an assumption/current direction.

Do not mark an assumption as resolved.

## 4. Questions Register

| ID | Local file(s) | Area | Question status | Question / decision question | Assumption / current direction | Impact / shared target | Blocks current work? |
|---|---|---|---|---|---|---|---|
| SL-ACC-Q-003 | `SL-ACC-001-register-client-account.md` / `.client.md` | Auth UX | open | Should registration automatically sign the user in? | Current client routes to `/login`; explicit login remains current direction. | registration/client auth flow | yes before changing UX |
| SL-AUTH-Q-005 | `SL-AUTH-002-current-user.client.md` | Error UX | open | How should client distinguish unauthenticated from server failure during current-user bootstrap? | Current implementation maps 401 to null session and rethrows non-401 failures. | global error / route UX | yes before final protected route UX |
| SL-AUTH-CURRENT-CLIENT-Q-001 | `SL-AUTH-002-current-user.client.md` | Route guard | open | What is the protected route policy? | Current implementation exposes session context; consumers branch locally. Shared redirect/guard policy is future work. | account/request/My Requests routing | yes before route-guard design |
| SL-AUTH-Q-007 | `SL-AUTH-003-logout.md` / `.client.md` | CSRF/security | open / future security | When should logout require antiforgery token handling? | Defer to CC-CSRF-001; do not implement custom CSRF inside logout sidecar. | unsafe command security | yes before claiming CSRF |
| SL-APPL-Q-001 | `SC-10` / applicant slices | Applicant model | accepted direction / target scenario | Is current/default ApplicantParty unique per account or per applicant type? | Account may store many ApplicantParties; at most one per applicant type can be current/default template. Current/default affects future prefill only. | future applicant read/template/request contracts | yes before redesigning applicant APIs |
| SL-APPL-Q-002 | `SC-10` / `SC-10B` | Applicant history | accepted direction | Does creating new ApplicantParty replace/delete existing ApplicantParties? | No. Adding new ApplicantParty does not overwrite, delete or deactivate existing ApplicantParties. | request history and My Applicant Parties | no |
| SL-APPL-Q-003 | `SC-10` / `SC-04` | Verification | accepted direction | What status does new ApplicantParty start with? | `NotVerified`; verification happens in request/review context. | review flow and UI status | no |
| SL-APPL-Q-004 | `SC-10` / `SC-04` / future request client | Current/default update | assumption | Should newly created ApplicantParty become current/default template? | UI should offer to make it current/default for its applicant type. Exact automatic/default-on behavior remains future client/API decision. | request creation and Account page prefill | yes before implementing set-default UX |
| SL-APPL-Q-005 | `SC-10B` / future applicant management | Delete/archive | future review | How should ApplicantParty deletion work? | Prefer warning-driven archive/hide unless hard delete is proven safe. Profiles used by requests/approved requests need stronger warnings or block. | future My Applicant Parties | no now |
| SL-APPL-Q-006 | `SC-10B` / request details | Edit/versioning | future review | Can ApplicantParty be edited in place after being used by requests? | Avoid breaking historical request context; decide version/snapshot/edit policy before edit slice. | request detail accuracy and audit | no now |
| SL-APPL-CLIENT-Q-001 | `SL-APPL-001-create-individual-applicant-party.client.md` | Current applicant read | open / needs reconciliation | How does Account page load applicant templates after refresh? | Current implementation/narrow docs may use current individual read; target scenario needs per-type template/read model later. | Account page stability / read model | yes before claiming full target scenario support |
| SL-APPL-CLIENT-D-002 | `SL-APPL-001-create-individual-applicant-party.client.md` | Create request entry | accepted direction | Should applicant create sidecar introduce a create request entry? | No. Create request entry/location belongs to request creation client sidecar. | request creation planning | no |
| SL-REQ-Q-003 | `SL-REQ-001-create-connection-request.md` / future `.client.md` | Client UI | open | What concrete page/form implements request creation? | Future request `.client.md` should use SC-04 UI/behavior sources. | request creation client sidecar | yes before request UI implementation |
| SL-REQ-Q-006 | `SC-04` / future `SL-REQ-001.client` | Applicant data in request journey | accepted direction | Does request creation UI include applicant fields and allow clearing prefilled data? | Yes. Prefilled template can be kept or cleared. Missing template starts empty. New data creates new ApplicantParty and uses it for request. | SC-04 UI/source behavior and request client sidecar | yes before request UI implementation |
| SL-REQ-Q-007 | `SC-04` / `SC-10` | Applicant creation boundary | open | How does request creation create new ApplicantParty and link it to request? | Target scenario says accepted new data creates a new ApplicantParty and uses it for the request; exact API contract is future. | backend/client API design | yes before implementing new target request flow |
| SL-REQ-Q-018 | `SC-04` / future request client | Applicant selection | future review | Should future request creation show dropdown/list of all saved ApplicantParties? | Yes as future direction; current/default template remains initial prefill/default selection. | request client UX/API | no now |
| SL-REQ-Q-019 | `SC-04` / request details | Applicant history | future review | Should request store ApplicantParty reference, immutable version or snapshot? | Must preserve submitted applicant context. Exact policy is future domain/read decision. | request details and edit safety | no now |
| SL-REQ-Q-009 | `SL-REQ-002-my-requests-list.md` | Paging | future review | When should My Requests list add paging? | Not needed for first server read slice; add when UX/data volume requires it. | list API/client contract | no |
| SL-REQ-Q-013 | `SL-REQ-003-own-request-details.md` | Applicant snapshot | future review | Should own request details include applicant snapshot? | No in first cut; future applicant snapshot/version policy may change this. | details API/client contract | no |
| CL-FEEDBACK-Q-001 | `CL-FEEDBACK-001-client-feedback-messages.md` | Feedback infrastructure | assumption | Should feedback start local or global? | Use local action/form/page alerts first; add global host when multiple features need shell-level messages. | client cross-cutting implementation | no until shared host work starts |

## 5. Superseded / Removed During Syncs

| Previous register item | Status | Reason |
|---|---|---|
| Earlier blanket “L1 client missing” status | superseded | Repo evidence shows first-stage registration, login, session bootstrap and applicant create client flows. Remaining gaps are tracked per sidecar. |
| Earlier request creation statement “applicant data only referenced as summary” | superseded | SC-04 says request creation journey includes applicant fields. |
| Earlier one-global-current ApplicantParty policy | superseded | Target scenario now uses multiple saved ApplicantParties and current/default template per applicant type. |
| Earlier applicant replacement means previous global current becomes non-current | superseded | New ApplicantParty creation does not overwrite/delete existing profiles. Current/default is per-type prefill template. |

## 6. Closed / Resolved Question Policy

Resolved questions may be removed from the active table when they are no longer useful for future work.

If keeping a resolved row, set `Question status = resolved` and make the answer explicit.

Do not leave a resolved question with status `open`.
