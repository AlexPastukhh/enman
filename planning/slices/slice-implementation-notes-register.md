# Slice Implementation Notes Register

Status: active / synchronized with applicant current-active scenario decision  
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

## 3. Notes Register

| ID | Related slice / future slice | Scenario | Layer | Tags | Note | Why it matters | Promote to | Status |
|---|---|---|---|---|---|---|---|---|
| NOTE-APPL-CLIENT-001 | future `SL-APPL-001...client.md` | SC-10 | Client/UI | account-page, applicant-form | Applicant party create form can live on Account page for the first client implementation. The feature should remain reusable if the route changes later. | Gives the current client draft a concrete working assumption without forcing a permanent route decision. | `.client.md` when applicant client work starts | open |
| NOTE-APPL-CLIENT-002 | future `SL-APPL-001...client.md` | SC-10 | Client/UI | command-success, read-only-state | After successful applicant party creation, switch submitted applicant data to read-only local state, show Edit action and show a self-dismissing success notification. | Defines the immediate UI outcome without requiring a read endpoint first. | `.client.md` + client/component tests | open |
| NOTE-APPL-CLIENT-003 | future `SL-APPL-001...client.md` / request creation client | SC-10 / SC-04 | Client/UI | applicantPartyId, request-context | Do not store/use returned applicantPartyId for request creation. Request creation should use server-selected current active ApplicantParty. | Prevents client-side spoofing/coupling and keeps request creation aligned with current-active policy. | `.client.md` + request creation client sidecar | open |
| NOTE-APPL-CLIENT-004 | future `SL-APPL-001...client.md` / auth client baseline | SC-10 | Client/UI | session, current-user | Do not refetch current-user/session after applicant party creation unless current-user contract later includes applicant state. Applicant state belongs to current-applicant read model. | Keeps auth/session state separate from applicant profile state. | auth/client docs if current-user expands | open |
| NOTE-APPL-CLIENT-005 | future `SL-APPL-001...client.md` / request creation client | SC-10 / SC-04 | Client/UI | navigation, request-entry | Applicant party create UI must not introduce create-request entry. The exact entry point belongs to future request creation client planning. | Prevents current applicant slice from owning unrelated navigation decisions. | request creation client sidecar | open |
| NOTE-APPL-READ-001 | future `L1-APPLICANT-PARTY-READ-CURRENT` | SC-10 | Client/API | current-applicant-read, refresh | Account page needs a current-applicant read model for stable refresh behavior, preferably auth-derived `GET /api/l1/applicant-parties/current-individual` or equivalent. | Without this, Account page can only show local post-submit state after create. | future read slice + API/client contract planning | open |
| NOTE-APPL-READ-002 | future current-applicant read / verification | SC-10 | Client/UI | verification-status | Future Account page may show applicant verification state: Not verified, Under review / pending verification, Verified, Rejected / requires update. | Keeps verification UI pressure visible without adding it to the first create command. | future read/verification slice | open |
| NOTE-REQ-UI-001 | SL-REQ-001 / request creation client | SC-04 | Client/UI | applicant-context, current-active, form | Request creation UI should show/reference the account's current active ApplicantParty summary. If applicant data is missing or wrong, the user should go through SC-10 Applicant Data / future replacement flow before submit. The request creation form should not create a separate request-local applicant identity in the current core direction. | Keeps UI, DTO mapping and scenario wording aligned with the one-current-active-ApplicantParty-per-account decision. Prevents accidental request-local applicant override behavior. | `.client.md` + behavior items when request creation client work starts | open |
| NOTE-REQ-UI-002 | SL-REQ-001 / request creation client | SC-04 | Client/UI | command-success, navigation | For command flows where the client does not need created entity data to continue, HTTP success without required body is enough. Client shows a success message and navigates to the next read-context screen. | Prevents client code from depending on command response fields that are not needed for the user flow. | `.client.md` | open |
| NOTE-CLIENT-AUTH-001 | multiple client slices | cross-scenario | Client/Server shared support | csrf, auth, cookie | Unsafe requests with ASP.NET Core cookie auth need antiforgery token fetch/store/attach/refetch on auth/session changes. | Affects all unsafe client mutations and diploma security explanation. | shared support + ADR candidate + extension register if broad security decision changes | open |
| NOTE-CLIENT-VALID-001 | multiple form slices | cross-scenario | Client/UI | deferred-validation | Deferred validation after input is current tested client behavior and should be described in client sidecars. | Affects client tests and form UX. | shared support + client files | open |
| NOTE-CLIENT-ERRORS-001 | multiple client slices | cross-scenario | Client/UI | error-mapping | Server validation/problem responses need mapping to field-level and global/form-level client messages. | Affects user feedback and client tests. | shared support + client files | open |
| NOTE-FORM-DTO-001 | multiple client slices | cross-scenario | Client/UI | form-values, dto | FormValues and API DTO may differ; use mapping when trim/null normalization, confirmation, checkbox, select string->number, nested object, File/FormData or UI-only state exists. | Prevents accidental coupling between UI forms and API contracts. | shared support + client files | open |
| NOTE-REVIEW-REJECT-001 | SL-REVIEW-002 | SC-07B | Client/UI | rejection-feedback | Empty rejection feedback should be UI warning/confirmation, not hard domain invariant. Empty string should likely trim to null before DTO. | Affects reject form behavior and DTO mapping. | `.client.md` + maybe scenario question + slice question register | open |
| NOTE-REVIEW-STALE-001 | SL-REVIEW-001 / SL-REVIEW-002 | SC-07B | Client/UI | stale-state, refetch | Review page should refetch details after domain/server rejection because request status may be stale. | Affects error handling and cache invalidation. | `.client.md` | open |
| NOTE-REQ-UI-TEST-001 | SL-REQ-001 / request creation client | SC-04 | Testing | client-tests, e2e | Request creation Client/UI should have tests for deferred validation, server error mapping, current-active applicant context display/redirect/update path, DTO building, antiforgery helper use and success navigation; E2E comes after client+server flow is stable. | Prevents under-tested client layer and keeps tests aligned with current active applicant scenario decision. | `.client.md` | open |

## 4. Superseded Notes

| ID | Status | Reason |
|---|---|---|
| Previous wording of `NOTE-REQ-UI-001` | superseded | Earlier wording allowed request-local applicant prefill/editing without mutating saved ApplicantParty. Scenario direction now says request creation references the account's current active ApplicantParty; applicant changes go through SC-10 / replacement flow before submit. |

## 5. Status Values

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
