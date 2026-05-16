# Slice Implementation Notes Register

Status: active / synchronized with current implemented backend, first-stage L1 client sidecars and new request/logout/feedback planning  
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

If a note contains a question that remains relevant for future work, mirror the question to `slice-questions-register.md`.

If a note describes extension/change pressure, mirror or link it to `slice-extension-points-register.md`.

## 2. Current Backend / Client Implementation Boundary

Current repo evidence says:

```text
- L1 backend/API/session/persistence flows are implemented for register/login/current-user/logout/applicant/request.
- Generated OpenAPI TypeScript types include L1 paths/types.
- Shared L1 API wrappers exist for register/login/current-user/logout/applicant create.
- Shared fetch/ProblemDetails/form error mapping exists.
- First-stage client flows exist for registration, login, current-user session bootstrap and Account page applicant create.
- Logout shared API wrapper exists, but concrete logout UI/cache/navigation is not confirmed.
- Request creation UI is not implemented.
```

## 3. Current Remaining Client Work Order

```text
Logout UI/cache/navigation
-> Current applicant read after refresh
-> Request Creation UI with applicant fields/prefill/clear behavior
-> My Requests read/list/detail
-> Browser E2E happy paths
```

## 4. Notes Register

| ID | Related slice / future slice | Scenario | Layer | Tags | Note | Why it matters | Promote to | Status |
|---|---|---|---|---|---|---|---|---|
| NOTE-LOGOUT-CLIENT-001 | `SL-AUTH-003.client` | auth/session | Client/UI | logout, cache, navigation | Shared `logoutClientAccount()` API wrapper exists; concrete logout UI/cache/navigation is planned in `SL-AUTH-003-logout.client.md`. | Avoids overclaiming logout UI as implemented while preserving implementation-ready sidecar. | `SL-AUTH-003-logout.client.md` | promoted-to-client-sidecar |
| NOTE-LOGOUT-CLIENT-002 | `SL-AUTH-003.client` / `CL-FEEDBACK-001` | auth/session | Client/UI | feedback, error | Unexpected logout failure should use action/global feedback and must not show false success. Logout success message is not required. | Prevents one-off logout error UI and ties feedback to client-wide convention. | `CL-FEEDBACK-001` + logout sidecar | open |
| NOTE-FEEDBACK-001 | `CL-FEEDBACK-001` | cross-scenario | Client/UI | success, info, warning, error | Add client-wide feedback/message surface convention for success/info/warning/error outcomes. | Login/register/applicant/request/logout should share feedback vocabulary and not duplicate one-off mechanics. | `planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md` | promoted-to-shared-support |
| NOTE-REQ-UI-001 | future `SL-REQ-001.client` | SC-04 | Client/UI | applicant-data, prefill, clear | Request creation UI should contain applicant data fields/section. If current active applicant data exists, fields are prefilled; user can clear and enter new applicant data before submit. | Aligns request journey with user expectation while keeping accepted applicant changes in account-level ApplicantParty flow. | future request `.client.md` + SC-04 UI/behavior docs | open |
| NOTE-REQ-UI-002 | future `SL-REQ-001.client` | SC-04 | Client/UI | applicant-context, current-active | Request submit should not send ApplicantPartyId or create request-local applicant identity. It uses server/current active applicant after applicant data is accepted. | Prevents spoofing and keeps server-selected applicant context. | future request `.client.md` | open |
| NOTE-REQ-UI-003 | future `SL-REQ-001.client` | SC-04 | Client/UI | command-success, navigation | For command flows where returned entity data is not needed, HTTP success is enough; client shows success outcome and moves to read context. | Prevents client depending on unnecessary response body. | future request `.client.md` | open |
| NOTE-APPL-CLIENT-002 | future `SL-APPL-002` | SC-10 | Client/UI | read-current, refresh | Current applicant UI uses local post-submit state only. Stable Account page after refresh needs current applicant read endpoint/client query/read model. | Required before claiming full persisted Account page applicant state. | future `L1-APPLICANT-PARTY-READ-CURRENT` | open |
| NOTE-CLIENT-AUTH-001 | multiple client slices | cross-scenario | Client/Server shared support | csrf, auth, cookie | Unsafe requests with cookie auth need antiforgery token fetch/store/attach/refetch on auth/session changes. | Affects all unsafe client mutations. | CC-CSRF-001 / future implementation | open |
| NOTE-CLIENT-ERRORS-001 | multiple client slices | cross-scenario | Client/UI | error-mapping | Server validation/problem responses are mapped to field-level and global/form-level client messages through shared helpers. | Affects user feedback and client tests. | shared support + client files | open |
| NOTE-FORM-DTO-001 | multiple client slices | cross-scenario | Client/UI | form-values, dto | FormValues and API DTO may differ; use mapping for UI-only fields, confirmation fields, null/trim normalization and nested objects. | Prevents coupling UI forms to API contracts. | shared support + client files | open |
| NOTE-REPO-MAYBE-001 | repository/query APIs / future read slices | cross-slice | Server/Application/Persistence | maybe, repositories, optional-result | New or refactored repository/query APIs should return `Maybe<T>` when absence of the resulting object is a normal outcome. Current L1 repositories still use nullable returns. | Keeps absence handling explicit. | `planning/slices/shared/maybe-for-optional-results.md` | open |

## 5. Superseded Notes

| ID | Status | Reason |
|---|---|---|
| Earlier blanket “concrete L1 client feature UI not completed” note | superseded | Current repo evidence shows first-stage client flows for registration, login, session bootstrap and applicant create. Remaining gaps are narrower and tracked here. |
| Earlier SC-04 applicant summary-only request UI note | superseded | SC-04 now requires applicant fields/section, prefilled from current applicant data when available, with clear/re-enter behavior before submit. |

## 6. Status Values

```text
open
promoted-to-slice
promoted-to-client-sidecar
promoted-to-shared-support
promoted-to-slice-question
promoted-to-extension-register
promoted-to-scenario-question
promoted-to-ADR
resolved
superseded
not-relevant
```
