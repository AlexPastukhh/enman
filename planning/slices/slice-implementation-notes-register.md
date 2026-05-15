# Slice Implementation Notes Register

Status: active  
Scope: concrete implementation notes for future slices/client sidecars/shared support

## 1. Purpose

This file stores concrete implementation thoughts that are not yet assigned to an active slice file or client sidecar.

It must be checked before starting any new slice implementation or client sidecar.

## 2. Intake Rule

Before starting work on a slice/client sidecar:

```text
1. Search this register by slice id, scenario id, layer and tags.
2. Decide where each relevant note goes:
   - parent slice file;
   - .client.md sidecar;
   - shared support doc;
   - scenario questions register;
   - behavior items;
   - ADR candidate;
   - irrelevant/superseded.
3. Promote or resolve the note.
4. Update note status.
5. If the note reveals scenario/DATA/validation ambiguity, stop and use the scenario question loop.
```

## 3. Notes Register

| ID | Related slice / future slice | Scenario | Layer | Tags | Note | Why it matters | Promote to | Status |
|---|---|---|---|---|---|---|---|---|
| NOTE-REQ-UI-001 | SL-REQ-001 / request creation client | SC-04 | Client/UI | applicant-prefill, form | Request creation form may prefill applicant data from selected ApplicantParty. Editing request-local fields must not mutate saved ApplicantParty. | Affects request form behavior, DTO mapping and user understanding. | `.client.md` + maybe behavior items | open |
| NOTE-REQ-UI-002 | SL-REQ-001 / request creation client | SC-04 | Client/UI | command-success, navigation | For command flows where the client does not need created entity data to continue, HTTP success without required body is enough. Client shows a success message and navigates to the next read-context screen. | Prevents client code from depending on command response fields that are not needed for the user flow. | `.client.md` | open |
| NOTE-CLIENT-AUTH-001 | multiple client slices | cross-scenario | Client/Server shared support | csrf, auth, cookie | Unsafe requests with ASP.NET Core cookie auth need antiforgery token fetch/store/attach/refetch on auth/session changes. | Affects all unsafe client mutations and diploma security explanation. | shared support + ADR candidate | open |
| NOTE-CLIENT-VALID-001 | multiple form slices | cross-scenario | Client/UI | deferred-validation | Deferred validation after input is current tested client behavior and should be described in client sidecars. | Affects client tests and form UX. | shared support + client files | open |
| NOTE-CLIENT-ERRORS-001 | multiple client slices | cross-scenario | Client/UI | error-mapping | Server validation/problem responses need mapping to field-level and global/form-level client messages. | Affects user feedback and client tests. | shared support + client files | open |
| NOTE-FORM-DTO-001 | multiple client slices | cross-scenario | Client/UI | form-values, dto | FormValues and API DTO may differ; use mapping when trim/null normalization, confirmation, checkbox, select string->number, nested object, File/FormData or UI-only state exists. | Prevents accidental coupling between UI forms and API contracts. | shared support + client files | open |
| NOTE-REVIEW-REJECT-001 | SL-REVIEW-002 | SC-07B | Client/UI | rejection-feedback | Empty rejection feedback should be UI warning/confirmation, not hard domain invariant. Empty string should likely trim to null before DTO. | Affects reject form behavior and DTO mapping. | `.client.md` + maybe scenario question | open |
| NOTE-REVIEW-STALE-001 | SL-REVIEW-001 / SL-REVIEW-002 | SC-07B | Client/UI | stale-state, refetch | Review page should refetch details after domain/server rejection because request status may be stale. | Affects error handling and cache invalidation. | `.client.md` | open |
| NOTE-REQ-UI-TEST-001 | SL-REQ-001 / request creation client | SC-04 | Testing | client-tests, e2e | Request creation Client/UI should have tests for deferred validation, server error mapping, DTO building, antiforgery helper use and success navigation; E2E comes after client+server flow is stable. | Prevents under-tested client layer. | `.client.md` | open |

## 4. Status Values

```text
open
promoted-to-slice
promoted-to-client-sidecar
promoted-to-shared-support
promoted-to-scenario-question
promoted-to-ADR
resolved
superseded
not-relevant
```
