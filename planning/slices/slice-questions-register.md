# Slice Questions Register

Status: active register / synchronized with current implemented backend, first-stage client sidecars and planned request/applicant UI sources  
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
| SL-ACC-Q-001 | `SL-ACC-001-register-client-account.md` | DB hardening | accepted direction | Should duplicate email also be enforced by a DB unique constraint? | Yes as future persistence hardening. Current implementation uses application precheck. | DB constraint implementation | no for current docs |
| SL-ACC-Q-003 | `SL-ACC-001-register-client-account.md` / `.client.md` | Auth UX | open | Should registration automatically sign the user in? | Current client routes to `/login`; explicit login remains current direction. | registration/client auth flow | yes before changing UX |
| SL-AUTH-Q-001 | `SL-AUTH-001-login-client-account.md` / `.client.md` | Client auth baseline | accepted direction | What owns L1 current-user after login? | React Query session query + SessionProvider; login invalidates session query. | auth/session client baseline | no |
| SL-AUTH-Q-005 | `SL-AUTH-002-current-user.client.md` | Error UX | open | How should client distinguish unauthenticated from server failure during current-user bootstrap? | Current implementation maps 401 to null session and rethrows non-401 failures. | global error / route UX | yes before final protected route UX |
| SL-AUTH-CURRENT-CLIENT-Q-001 | `SL-AUTH-002-current-user.client.md` | Route guard | open | What is the protected route policy? | Current implementation exposes session context; consumers branch locally. Shared redirect/guard policy is future work. | account/request/My Requests routing | yes before route-guard design |
| SL-AUTH-Q-006 | `SL-AUTH-003-logout.md` / `SL-AUTH-003-logout.client.md` | Client logout UX | resolved | Where should client navigate after logout? | Navigate to Home / public home. | logout sidecar/router/tests | no |
| SL-AUTH-Q-007 | `SL-AUTH-003-logout.md` / `.client.md` | CSRF/security | open / future security | When should logout require antiforgery token handling? | Defer to CC-CSRF-001; do not implement custom CSRF inside logout sidecar. | unsafe command security | yes before claiming CSRF |
| SL-AUTH-Q-008 | `SL-AUTH-003-logout.md` / `.client.md` | Client cache | resolved for current known caches | Should logout clear only auth state or all user-scoped query caches? | Logout clears current-user/session query and removes current applicant-party query; broader user-scoped cache convention can evolve when more protected read queries exist. | stale user data prevention | no |
| SL-AUTH-CLIENT-Q-001 | `SL-AUTH-003-logout.client.md` | Stale session | accepted direction | How should client handle 401 from logout? | Treat as already unauthenticated/stale session; clear local state and navigate Home. | stale session recovery | no |
| SL-AUTH-CLIENT-Q-002 | `SL-AUTH-003-logout.client.md` | Feedback | accepted direction | Should logout show a success notification? | No required success message; Home + guest/public state is visible success outcome. | UI copy/tests | no |
| SL-AUTH-CLIENT-Q-003 | `SL-AUTH-003-logout.client.md` / `CL-FEEDBACK-001` | Feedback | resolved for current UI | How should unexpected logout failure be shown? | Header logout action shows local `role="alert"` feedback and does not navigate as if successful. | feedback concern / tests | no |
| SL-AUTH-CLIENT-Q-004 | `SL-AUTH-003-logout.client.md` | Pending state | resolved for current UI | Should logout show a visible pending/loading state? | Header logout button disables and shows a pending label while logout is in flight. | component behavior/tests | no |
| SL-APPL-Q-001 | `SL-APPL-001-create-individual-applicant-party.md` / `SC-10` | Replacement/versioning | accepted direction | Is current active ApplicantParty unique per account or per applicant type? | One current active ApplicantParty per account; future replacement makes new accepted data current and previous data non-current. | applicant replacement/current-active policy | yes for replacement work |
| SL-APPL-CLIENT-Q-001 | `SL-APPL-001-create-individual-applicant-party.client.md` | Current applicant read | open | How does Account page load an existing applicant after refresh? | Stable refresh requires current applicant read endpoint/client slice. | Account page stability / read model | yes before claiming persisted Account page state |
| SL-APPL-CLIENT-D-002 | `SL-APPL-001-create-individual-applicant-party.client.md` | Create request entry | accepted direction | Should applicant create sidecar introduce a create request entry? | No. Create request entry/location belongs to request creation client sidecar. | request creation planning | no |
| SL-APPL-READ-Q-001 | `SL-APPL-002-read-current-individual-applicant-party.md` | Missing applicant state | assumption | Should missing current applicant return 404 or successful empty state? | Use `200 exists=false applicantParty=null` as normal Account page state. | read endpoint / client flow | yes before implementation |
| SL-REQ-Q-001 | `SL-REQ-001-create-connection-request.md` / future `.client.md` | My Requests read model | open | What exact My Requests list/detail response does the client need after request success? | Request creation command does not need returned request data; read/list/detail slice defines My Requests shape later. | future read/client slice | yes before request UI completion |
| SL-REQ-Q-003 | `SL-REQ-001-create-connection-request.md` / future `.client.md` | Client UI | open | What concrete page/form implements request creation? | Future request `.client.md` should use SC-04 UI/behavior sources. | request creation client sidecar | yes before request UI implementation |
| SL-REQ-Q-009 | `SL-REQ-002-my-requests-list.md` | Paging | future review | When should My Requests list add paging? | Not needed for first server read slice; add when UX/data volume requires it. | list API/client contract | no |
| SL-REQ-Q-010 | `SL-REQ-002-my-requests-list.md` | Summary shape | resolved | What summary fields are enough for first My Requests list API? | `requestId`, `requestType`, `status`, `createdAt`, `summary`, `objectAddress`. | list API/OpenAPI/client wrapper | no |
| SL-REQ-Q-011 | `SL-REQ-002-my-requests-list.md` | Invalid filter | resolved | What status code should invalid status filter return? | Existing validation ProblemDetails / 422. | API error contract/tests | no |
| SL-REQ-Q-012 | `SL-REQ-002-my-requests-list.md` | Sorting | resolved | What default sort should My Requests list use? | Newest first by `CreatedAt DESC`, then `Id DESC`. | API behavior/tests | no |
| SL-REQ-Q-006 | `SC-04` / future `SL-REQ-001.client` | Applicant data in request journey | assumption | Does request creation UI include applicant fields and allow clearing prefilled data? | Yes. Applicant fields are part of request journey; prefilled values can be cleared and replaced before submit. Accepted changes update account-level ApplicantParty. | SC-04 UI/source behavior and request client sidecar | yes before request UI implementation |
| SL-REQ-Q-007 | `SC-04` / `SC-10` / future applicant replacement | Applicant replacement boundary | open | Does inline applicant replacement reuse SC-10 save behavior or need a dedicated replacement endpoint? | Draft assumes SC-10 / applicant replacement behavior owns accepted applicant changes before request submit. | backend/client API design | yes before implementing inline replacement |
| CL-FEEDBACK-Q-001 | `CL-FEEDBACK-001-client-feedback-messages.md` | Feedback infrastructure | assumption | Should feedback start local or global? | Use local action/form/page alerts first; add global host when multiple features need shell-level messages. | client cross-cutting implementation | no until shared host work starts |
| CL-FEEDBACK-Q-002 | `CL-FEEDBACK-001-client-feedback-messages.md` | Success messages | future review | Should success messages auto-dismiss by default? | Feature decides. Applicant create uses self-dismissing notification; logout success needs no message. | UI consistency | no |
| CL-FEEDBACK-Q-003 | `CL-FEEDBACK-001-client-feedback-messages.md` | Error feedback | accepted direction | Should failures ever display success outcome? | No. Unexpected failure must show failure feedback or preserve prior state. | logout/request/applicant tests | no |

## 5. Superseded / Removed During Syncs

| Previous register item | Status | Reason |
|---|---|---|
| Earlier blanket “L1 client missing” status | superseded | Repo evidence now shows first-stage registration, login, session bootstrap and applicant create client flows. Remaining gaps are tracked per sidecar. |
| Earlier request creation statement “applicant data only referenced as summary” | superseded | SC-04 now says request creation journey includes applicant fields; existing data is prefilled and can be cleared/replaced before submit. |

## 6. Closed / Resolved Question Policy

Resolved questions may be removed from the active table when they are no longer useful for future work.

If keeping a resolved row, set `Question status = resolved` and make the answer explicit.

Do not leave a resolved question with status `open`.
