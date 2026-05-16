# Slice Implementation Notes Register

Status: active / ApplicantParty template-per-type model synchronized  
Scope: concrete implementation notes for future slices/client sidecars/shared support

## 1. Purpose

This file stores concrete implementation thoughts that are not yet assigned to an active slice file or client sidecar, or are promoted but still useful for future work.

It must be checked before starting any new slice implementation or client sidecar.

It does not replace:

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
```

## 2. Current ApplicantParty Direction

Target scenario direction:

```text
many saved ApplicantParties
+ one current/default template per applicant type
+ first item of type initializes default
+ additional item of same type does not silently switch default
+ request creation with new applicant data is one atomic server use case
```

Current implementation may still contain older current-active concepts. Implementation tasks must separate current implementation evidence from target scenario direction.

## 3. Notes Register

| ID | Related slice / future slice | Scenario | Layer | Tags | Note | Why it matters | Promote to | Status |
|---|---|---|---|---|---|---|---|---|
| NOTE-APPL-CREATE-001 | `SL-APPL-001` | SC-10 | Server/API | applicant-create, id | Keep standalone create ApplicantParty returning ApplicantPartyId. | Stable identity supports cache/refetch, list updates, future selection/edit/archive/default actions. | `SL-APPL-001` | promoted-to-slice |
| NOTE-APPL-CREATE-002 | `SL-APPL-001` | SC-10 | Server/domain | no-replacement | Creating ApplicantParty must not delete/overwrite/deactivate existing ApplicantParties. | Aligns with multi-profile target model and historical request safety. | `SL-APPL-001` | promoted-to-slice |
| NOTE-APPL-DEFAULT-001 | `SL-APPL-001` / `SL-APPL-003` | SC-10 | Server/domain | default-template | If no current/default exists for applicant type, first created ApplicantParty may initialize default. | Natural prefill behavior. | `SL-APPL-001`, `SL-APPL-003` | promoted-to-slice |
| NOTE-APPL-DEFAULT-002 | `SL-APPL-001` / `SL-APPL-003` | SC-10 | Server/domain | default-template | If default already exists for same type, creating another ApplicantParty leaves default unchanged. | Prevents hidden future prefill changes. | `SL-APPL-003` | promoted-to-slice |
| NOTE-APPL-READ-001 | `SL-APPL-002` | SC-10 | Server/API + Client | account-page-read | Account page read model should return saved ApplicantParties plus defaults per type, not only current individual applicant. | Required for correct Account page state after create/refresh. | `SL-APPL-002` | promoted-to-slice |
| NOTE-APPL-CLIENT-REFETCH-001 | `SL-APPL-001.client` / future Account page | SC-10 | Client/UI | react-query, refetch | After create, client may optimistically add the item, but must refetch ApplicantParties for server-truth list/default/verification state. | Prevents stale/incorrect default display. | future client sidecar | open |
| NOTE-APPL-SERVICE-001 | `SL-APPL-004` | SC-10 / SC-04 | Server/Application | shared-service | Extract ApplicantParty creation logic into application service with no SaveChanges. | Reuse creation logic without nested command handler/transaction confusion. | `SL-APPL-004` | promoted-to-slice |
| NOTE-REQ-APPLCTX-001 | `SL-REQ-004` | SC-04 | Server/API | applicant-context | Request creation target should accept Existing ApplicantParty or New ApplicantParty data. | Supports many applicant profiles and new applicant creation in request flow. | `SL-REQ-004` | promoted-to-slice |
| NOTE-REQ-APPLCTX-002 | `SL-REQ-004` | SC-04 | Server/API | atomicity | Request creation with new applicant data must create ApplicantParty + Request atomically in one server call. | Avoids orphan ApplicantParty after request failure. | `SL-REQ-004` | promoted-to-slice |
| NOTE-REQ-UI-001 | future `SL-REQ-001.client` | SC-04 | Client/UI | prefill, clear | Request creation UI pre-fills from current/default; missing default and cleared prefill both enter new applicant data path. | Keeps scenario flow clear. | future request client sidecar | open |
| NOTE-REQ-UI-002 | future `SL-REQ-001.client` | SC-04 | Client/UI | dropdown | Future UI may allow dropdown/list selection from all saved ApplicantParties; current/default remains initial selection. | Future UX extension. | future request client sidecar | future review |
| NOTE-LOGOUT-CLIENT-001 | `SL-AUTH-003.client` | auth/session | Client/UI | logout, cache, navigation | Header logout action clears known user-scoped state and navigates Home on success/stale-session 401. | Records current logout UI/cache behavior. | logout sidecar | resolved |
| NOTE-REPO-MAYBE-001 | repository/query APIs | cross-slice | Server/Application/Persistence | maybe | New/refactored repository/query APIs should return `Maybe<T>` when absence is normal. Current L1 repositories may still use nullable returns. | Keeps absence handling explicit. | shared maybe convention | open |

## 4. Superseded Notes

| Previous note | Status | Reason |
|---|---|---|
| Current applicant read only / current active individual as Account page state | superseded for target scenario | Account page target needs saved ApplicantParties plus defaults per applicant type. |
| Applicant replacement/versioning on create | superseded | Creating ApplicantParty is addition; explicit default selection is separate. |
| Request creation uses server-selected single current active applicant | superseded for target scenario | Target request creation uses explicit applicantContext Existing/New. |
| Two client calls for new applicant then request | rejected | Breaks atomicity and can leave orphan ApplicantParty. |
