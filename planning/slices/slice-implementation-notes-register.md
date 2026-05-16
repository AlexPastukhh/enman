# Slice Implementation Notes Register

Status: active / synchronized with current implemented backend and first-stage L1 client sidecars  
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
3. Search `slice-extension-points-register.md` for related extension/change pressure.
4. Decide where each relevant note goes:
   - parent slice file;
   - .client.md sidecar;
   - shared support doc;
   - scenario questions register;
   - behavior items;
   - slice question register;
   - extension points register;
   - ADR candidate;
   - irrelevant/superseded.
5. Promote or resolve the note.
6. Update note status.
7. If the note reveals scenario/DATA/validation ambiguity, stop and use the scenario question loop.
```

## 3. Current Backend / Client Implementation Boundary

Current repo evidence says:

```text
- L1 backend/API/session/persistence flows are implemented for register/login/current-user/logout/applicant/request.
- Generated OpenAPI TypeScript types include L1 paths/types.
- Shared L1 API wrappers exist for register/login/current-user/logout/applicant create.
- Shared fetch/ProblemDetails/form error mapping exists.
- First-stage client flows exist for registration, login, current-user session bootstrap and Account page applicant create.
- Client/component/E2E tests for those first-stage flows were not confirmed during this documentation pass.
```

Current remaining client/read work order:

```text
Logout UI/cache/navigation, if needed
-> Current applicant read after refresh
-> Request Creation UI
-> My Requests read/list/detail
-> Browser E2E happy paths
```

## 4. Notes Register

| ID | Related slice / future slice | Scenario | Layer | Tags | Note | Why it matters | Promote to | Status |
|---|---|---|---|---|---|---|---|---|
| NOTE-APPL-READ-001 | `L1-APPLICANT-PARTY-READ-CURRENT` | SC-10 | Server/API/Client | read-current, account-page, normal-empty-state | Current applicant read should return normal Account page state when missing: `200 exists=false`, not exception-like `404`, unless user later changes target behavior. | Keeps Account page able to show create form as normal empty state. | future slice + slice questions register | open |
| NOTE-APPL-READ-002 | `L1-APPLICANT-PARTY-READ-CURRENT` | SC-10 | Persistence | read-current, performance, tracking | The first implementation can reuse existing current-active individual applicant lookup. If read performance/tracking matters later, add a read-specific `AsNoTracking` query without changing behavior. | Prevents performance optimization from becoming a behavior decision. | future slice / optional implementation detail | open |
| NOTE-APPL-READ-003 | `L1-APPLICANT-PARTY-READ-CURRENT` | SC-10 | Server/API | verification-status, read-model | Read response can include `verificationStatus` to support Account page status display, but verification workflow/labels remain future UI/domain work. | Makes verification read model visible without coupling to provider workflow. | slice-extension-points-register.md | open |
| NOTE-REG-CLIENT-001 | `SL-ACC-001.client` | registration | Client/UI | implemented, registration | First-stage registration UI is implemented: `/register`, `RegisterForm`, zod validation, password confirmation, ProblemDetails mapping, API DTO mapping and success navigation to `/login`. | Prevents future docs from treating registration UI as missing. | `SL-ACC-001-register-client-account.client.md` | promoted-to-client-sidecar |
| NOTE-REG-CLIENT-002 | `SL-ACC-001.client` / future auth UX | registration | Client/UI | auto-login, navigation | Registration currently routes to `/login`; auto-login/success page remains an open UX decision. | Prevents accidental assumption that backend registration creates a session. | slice questions register | open |
| NOTE-LOGIN-CLIENT-001 | `SL-AUTH-001.client` | auth/session | Client/UI | implemented, login | First-stage login UI is implemented: `/login`, `LoginForm`, validation, ProblemDetails mapping, API mapping, session query invalidation and navigation home. | Prevents future docs from treating login UI as missing. | `SL-AUTH-001-login-client-account.client.md` | promoted-to-client-sidecar |
| NOTE-SESSION-CLIENT-001 | `SL-AUTH-002.client` | auth/session | Client/entity | implemented, current-user | First-stage session bootstrap is implemented through `SessionProvider`, `useSessionQuery`, `getCurrentSession`, `mapCurrentUserToSession`, and `useSession`. 401 maps to null session. | Establishes current auth/session client baseline for protected client flows. | `SL-AUTH-002-current-user.client.md` | promoted-to-client-sidecar |
| NOTE-SESSION-CLIENT-002 | `SL-AUTH-002.client` / future route guard | auth/session | Client/UI | route-guard, errors | Protected route policy and non-401 bootstrap error UX remain open. Current implementation exposes session context and lets consumers branch locally. | Affects Account page, request creation and My Requests route behavior. | slice questions register + future route guard/client sidecar | open |
| NOTE-LOGOUT-CLIENT-001 | `SL-AUTH-003` / future logout `.client.md` | auth/session | Client/UI | logout, cache, navigation | Shared `logoutClientAccount()` API wrapper exists, but concrete logout button/UI, cache invalidation and post-logout navigation were not confirmed. | Avoids overclaiming logout UI as implemented. | future logout `.client.md` | open |
| NOTE-APPL-CLIENT-001 | `SL-APPL-001.client` | SC-10 | Client/UI | implemented, account-page, applicant-create | First-stage Account page applicant create flow is implemented: authenticated AccountPage renders form, submits L1 DTO, maps errors, switches to local read-only state, shows Edit and self-dismissing success notification. | Prevents future docs from treating Applicant Data UI as only planned. | `SL-APPL-001-create-individual-applicant-party.client.md` | promoted-to-client-sidecar |
| NOTE-APPL-CLIENT-002 | `SL-APPL-001.client` / future read-current | SC-10 | Client/UI | read-current, refresh | Current applicant UI uses local post-submit state only. Stable Account page after refresh needs current applicant read endpoint/client query/read model. | Required before claiming full persisted Account page applicant state. | future `L1-APPLICANT-PARTY-READ-CURRENT` | open |
| NOTE-APPL-CLIENT-003 | `SL-APPL-001.client` / future replacement | SC-10 | Client/UI | edit, replacement | Current Edit action returns to local editable form only. Persisted edit/replacement remains future slice work. | Prevents treating local edit affordance as persisted replacement. | future applicant edit/replacement slice | open |
| NOTE-APPL-CLIENT-004 | `SL-APPL-001.client` / request creation client | SC-10/SC-04 | Client/UI | request-entry, scope-boundary | Applicant create sidecar should not introduce create request entry. Entry location belongs to future request creation client sidecar. Do not test absence as a behavior item. | Keeps Applicant Data UI and request creation UI responsibilities separate. | slice questions register / request `.client.md` later | open |
| NOTE-REQ-UI-001 | SL-REQ-001 / request creation client | SC-04 | Client/UI | applicant-context, current-active, form | Request creation UI should show/reference the account's current active ApplicantParty summary. If applicant data is missing or wrong, the user should go through SC-10 Applicant Data / future replacement flow before submit. The request creation form should not create a separate request-local applicant identity in the current core direction. | Keeps UI, DTO mapping and scenario wording aligned with the one-current-active-ApplicantParty-per-account decision. Prevents accidental request-local applicant override behavior. | `.client.md` + behavior items when request creation client work starts | open |
| NOTE-REQ-UI-002 | SL-REQ-001 / request creation client | SC-04 | Client/UI | command-success, navigation | For command flows where the client does not need created entity data to continue, HTTP success without required body is enough. Client shows a success message and navigates to the next read-context screen. | Prevents client code from depending on command response fields that are not needed for the user flow. | `.client.md` | open |
| NOTE-CLIENT-AUTH-001 | multiple client slices | cross-scenario | Client/Server shared support | csrf, auth, cookie | Unsafe requests with ASP.NET Core cookie auth need antiforgery token fetch/store/attach/refetch on auth/session changes. | Affects all unsafe client mutations and diploma security explanation. | shared support + ADR candidate + extension register if broad security decision changes | open |
| NOTE-CLIENT-VALID-001 | multiple form slices | cross-scenario | Client/UI | deferred-validation | Deferred validation after input is current tested/intended client behavior and should be described in client sidecars. | Affects client tests and form UX. | shared support + client files | open |
| NOTE-CLIENT-ERRORS-001 | multiple client slices | cross-scenario | Client/UI | error-mapping | Server validation/problem responses are mapped to field-level and global/form-level client messages through shared helpers. | Affects user feedback and client tests. | shared support + client files | open |
| NOTE-FORM-DTO-001 | multiple client slices | cross-scenario | Client/UI | form-values, dto | FormValues and API DTO may differ; use mapping when confirmation fields, trim/null normalization, select conversions, nested objects or UI-only state exists. Current register and applicant flows already use feature API mappers. | Prevents accidental coupling between UI forms and API contracts. | shared support + client files | open |
| NOTE-REQ-UI-TEST-001 | SL-REQ-001 / request creation client | SC-04 | Testing | client-tests, e2e | Request creation Client/UI should have tests for deferred validation, server error mapping, current-active applicant context display/redirect/update path, DTO building, antiforgery helper use and success navigation; E2E comes after client+server flow is stable. | Prevents under-tested client layer and keeps tests aligned with current active applicant scenario decision. | `.client.md` | open |
| NOTE-REPO-MAYBE-001 | repository/query APIs / future read slices | cross-slice | Server/Application/Persistence | maybe, repositories, optional-result, refactor | New or refactored repository/query APIs should return `Maybe<T>` when absence of the resulting object is a normal outcome. Current L1 repositories still use nullable returns such as `Task<Account?>`; do not overclaim the refactor as implemented. | Keeps absence handling explicit and lets application handlers map `Maybe.None` to invalid credentials, not found, validation/domain error or empty UI state depending on use case. | `planning/slices/shared/maybe-for-optional-results.md` + future repository refactor slices | open |

## 5. Superseded Notes

| ID | Status | Reason |
|---|---|---|
| Previous wording of `NOTE-REQ-UI-001` | superseded | Earlier wording allowed request-local applicant prefill/editing without mutating saved ApplicantParty. Scenario direction now says request creation references the account's current active ApplicantParty; applicant changes go through SC-10 / replacement flow before submit. |
| Earlier blanket “concrete L1 client feature UI not completed” note | superseded | Current repo evidence now shows first-stage client flows for registration, login, session bootstrap and applicant create. Remaining gaps are narrower and are tracked here. |

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
