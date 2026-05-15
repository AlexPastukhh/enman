# Slice Implementation Notes Register

Status: active / synchronized with current implemented backend L1 slice docs, client-missing state and Applicant Data UI sidecar draft  
Scope: concrete implementation notes for future slices/client sidecars/shared support

## 1. Purpose

This file stores concrete implementation thoughts that are not yet assigned to an active slice file or client sidecar.

It must be checked before starting any new slice implementation or client sidecar.

It does not replace:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
```

If a note contains a question that remains relevant for future work, mirror the question to `slice-questions-register.md`.

If a note describes extension/change pressure, mirror or link it to `slice-extension-points-register.md`.

## 2. Intake Rule

Before starting work on a slice/client sidecar:

```text
1. Search this register by slice id, scenario id, layer and tags.
2. Search `slice-questions-register.md` for related questions.
3. Decide where each relevant note goes:
   - parent slice file;
   - .client.md sidecar;
   - shared support doc;
   - scenario questions register;
   - behavior items;
   - slice question register;
   - extension points register;
   - ADR candidate;
   - irrelevant/superseded.
4. Promote or resolve the note.
5. Update note status.
6. If the note reveals scenario/DATA/validation ambiguity, stop and use the scenario question loop.
```

## 3. Current Backend / Client Implementation Boundary

Current repo evidence says:

```text
- L1 backend/API/session/persistence flows are implemented for register/login/current-user/logout/applicant/request.
- Generated OpenAPI TypeScript types include L1 paths/types.
- Concrete L1 client feature UI is not completed.
- Applicant Data UI now has an implementation-ready sidecar draft:
  planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
```

Recommended client work order:

```text
L1 auth/session client baseline
-> applicant data UI
-> current applicant read model
-> request creation UI
-> My Requests read/list/detail
-> browser E2E happy paths
```

## 4. Notes Register

| ID | Related slice / future slice | Scenario | Layer | Tags | Note | Why it matters | Promote to | Status |
|---|---|---|---|---|---|---|---|---|
| NOTE-AUTH-CLIENT-001 | `SL-AUTH-001` / `SL-AUTH-002` / `SL-AUTH-003` / future auth `.client.md` | auth/session | Client/UI | auth-session, current-user, route-guard | Create the L1 auth/session client baseline before or alongside protected applicant/request UI. It should cover login, current-user bootstrap, logout, session state, route guard direction and generated type usage. | Applicant/request UI endpoints are protected; doing them before auth baseline creates duplicated temporary auth handling. | `.client.md` auth/session sidecar | open |
| NOTE-AUTH-CLIENT-002 | `SL-AUTH-001` / registration client | SC-01/auth | Client/UI | registration, auto-login | Registration backend returns `AccountId` + `Email` and does not issue a session. Client registration flow must decide whether to route to login, automatically login after successful registration, or show a confirmation/success state. | Prevents assuming auto-login from backend registration response. | auth/register `.client.md` + slice questions register | open |
| NOTE-AUTH-CLIENT-003 | `SL-AUTH-002` | auth/session | Client/UI | current-user, bootstrap, cache | Current-user client handling should distinguish 401 unauthenticated from server/global failures and should define app bootstrap/protected route behavior. | Affects route guard UX, loading states and query cache strategy. | auth/session `.client.md` | open |
| NOTE-AUTH-CLIENT-004 | `SL-AUTH-003` | auth/session | Client/UI | logout, cache, navigation | Logout client handling should clear auth state and decide whether to invalidate all user-scoped queries and where to navigate. | Prevents stale user data after logout. | auth/session `.client.md` | open |
| NOTE-AUTH-E2E-001 | auth/session client | auth/session | Testing | e2e, auth | L1 login/current-user/logout browser E2E should be added only after concrete auth UI/session client work exists. | Avoids migrating/proliferating E2E before client baseline is real. | testing plan / auth `.client.md` | open |
| NOTE-APPL-CLIENT-001 | `SL-APPL-001-create-individual-applicant-party.client.md` | SC-10 | Client/UI | account-page, create-form | Account page hosts the first individual applicant party create form for now. The create feature should remain reusable under feature/entity layers if route composition changes later. | Keeps the first UI implementation concrete without coupling feature code to one route forever. | active `.client.md` | promoted-to-client-sidecar |
| NOTE-APPL-CLIENT-002 | `SL-APPL-001-create-individual-applicant-party.client.md` | SC-10 | Client/UI | success-state, read-only | After create success, the UI may switch to read-only local applicant state using submitted values and show a self-dismissing success notification. | Enables first command-sidecar implementation before the stable read-current endpoint exists. | active `.client.md` | promoted-to-client-sidecar |
| NOTE-APPL-CLIENT-003 | `SL-APPL-001-create-individual-applicant-party.client.md` | SC-10 / SC-04 | Client/UI | applicant-id, request-context | Do not store `applicantPartyId` for request creation. The request creation backend uses server-selected current active ApplicantParty. | Prevents accidental client spoofing/coupling to command response identity. | active `.client.md` + slice questions register | promoted-to-client-sidecar |
| NOTE-APPL-CLIENT-004 | `SL-APPL-001-create-individual-applicant-party.client.md` | auth/session | Client/UI | session, current-user | Applicant party creation should not refetch current-user/session by default. Applicant party state belongs to applicant read model, not auth session. | Prevents mixing applicant data state into authentication state unless current-user contract explicitly changes. | active `.client.md` + slice questions register | promoted-to-client-sidecar |
| NOTE-APPL-CLIENT-005 | `SL-APPL-001-create-individual-applicant-party.client.md` | SC-10 / SC-04 | Client/UI | request-entry, scope-boundary | Applicant create sidecar does not introduce create request entry. Future request creation sidecar decides entry location. Do not create a standalone test only for absence of that entry. | Keeps applicant create UI focused and avoids testing unrelated absence/scope boundaries. | active `.client.md` + slice questions register | promoted-to-client-sidecar |
| NOTE-APPL-READ-001 | `L1-APPLICANT-PARTY-READ-CURRENT` | SC-10 | Client/API | current-applicant, read-model, refresh | Stable Account page state after refresh needs a current applicant read slice. Target direction: `GET /api/l1/applicant-parties/current-individual`, with server-derived current account context. | Without a read model, the create sidecar can only use local post-submit state. | future read slice + slice questions register | open |
| NOTE-REQ-UI-001 | SL-REQ-001 / request creation client | SC-04 | Client/UI | applicant-context, current-active, form | Request creation UI should show/reference the account's current active ApplicantParty summary. If applicant data is missing or wrong, the user should go through SC-10 Applicant Data / future replacement flow before submit. The request creation form should not create a separate request-local applicant identity in the current core direction. | Keeps UI, DTO mapping and scenario wording aligned with the one-current-active-ApplicantParty-per-account decision. Prevents accidental request-local applicant override behavior. | `.client.md` + behavior items when request creation client work starts | open |
| NOTE-REQ-UI-002 | SL-REQ-001 / request creation client | SC-04 | Client/UI | command-success, navigation | For command flows where the client does not need created entity data to continue, HTTP success without required body is enough. Client shows a success message and navigates to the next read-context screen. | Prevents client code from depending on command response fields that are not needed for the user flow. | `.client.md` | open |
| NOTE-CLIENT-AUTH-001 | multiple client slices | cross-scenario | Client/Server shared support | csrf, auth, cookie | Unsafe requests with ASP.NET Core cookie auth need antiforgery token fetch/store/attach/refetch on auth/session changes. | Affects all unsafe client mutations and diploma security explanation. | shared support + ADR candidate + extension register if broad security decision changes | open |
| NOTE-CLIENT-VALID-001 | multiple form slices | cross-scenario | Client/UI | deferred-validation | Deferred validation after input is current tested client behavior and should be described in client sidecars. | Affects client tests and form UX. | shared support + client files | open |
| NOTE-CLIENT-ERRORS-001 | multiple client slices | cross-scenario | Client/UI | error-mapping | Server validation/problem responses need mapping to field-level and global/form-level client messages. | Affects user feedback and client tests. | shared support + client files | open |
| NOTE-FORM-DTO-001 | multiple client slices | cross-scenario | Client/UI | form-values, dto | FormValues and API DTO may differ; use mapping when trim/null normalization, confirmation, checkbox, select string->number, nested object, File/FormData or UI-only state exists. | Prevents accidental coupling between UI forms and API contracts. | shared support + client files | open |
| NOTE-REVIEW-REJECT-001 | SL-REVIEW-002 | SC-07B | Client/UI | rejection-feedback | Empty rejection feedback should be UI warning/confirmation, not hard domain invariant. Empty string should likely trim to null before DTO. | Affects reject form behavior and DTO mapping. | `.client.md` + maybe scenario question + slice question register | open |
| NOTE-REVIEW-STALE-001 | SL-REVIEW-001 / SL-REVIEW-002 | SC-07B | Client/UI | stale-state, refetch | Review page should refetch details after domain/server rejection because request status may be stale. | Affects error handling and cache invalidation. | `.client.md` | open |
| NOTE-REQ-UI-TEST-001 | SL-REQ-001 / request creation client | SC-04 | Testing | client-tests, e2e | Request creation Client/UI should have tests for deferred validation, server error mapping, current-active applicant context display/redirect/update path, DTO building, antiforgery helper use and success navigation; E2E comes after client+server flow is stable. | Prevents under-tested client layer and keeps tests aligned with current active applicant scenario decision. | `.client.md` | open |

## 5. Superseded Notes

| ID | Status | Reason |
|---|---|---|
| Previous wording of `NOTE-REQ-UI-001` | superseded | Earlier wording allowed request-local applicant prefill/editing without mutating saved ApplicantParty. Scenario direction now says request creation references the account's current active ApplicantParty; applicant changes go through SC-10 / replacement flow before submit. |

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
