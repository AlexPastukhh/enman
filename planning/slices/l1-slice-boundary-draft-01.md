# L1 Slice Boundary Draft 01

Status: draft  
Scope: L1 slice discovery, boundary validation, questions and decisions  
Purpose: identify real L1 slices before implementing future work slice-by-slice

## 1. Purpose

This document defines L1 slice boundaries.

It is a boundary/discovery document, not a detailed implementation plan.

It answers:

```text
What are the L1 slices?
Why is each one a real slice?
Which scenario flow does each slice cover?
Which parts are separate dependent/extension/Client-UI/read/plugin/shared-support-assisted slices?
Which boundary questions and decisions affect slice planning?
```

Detailed implementation flow belongs to per-slice files.

## 2. Slice Boundary Criteria

A candidate is a real slice when:

```text
1. It has observable behavior.
2. It is derived from scenario flow.
3. It has a clear start and end.
4. It can be tested independently.
5. It includes relevant DATA, behavior items, invariants and no-write rules.
6. It has clear scope and out-of-scope.
7. It is not just an endpoint, controller, repository, DB table, UI component, shared helper or aggregate.
8. It can be implemented independently or almost independently.
9. If it depends on or extends another slice, that relationship is explicit.
10. If one scenario contains L1 and later-package behavior, those parts can become separate slices.
```

## 3. Package / Marker Model

```text
[L1]              current L1 package slice
[L2]              later package slice
[EXTENSION]       adds behavior to an existing slice
[DEPENDENT]       depends on another slice
[CLIENT/UI]       client/UI-focused slice or client-visible behavior
[READ]            read/query slice
[AUTH/FRAMEWORK]  auth/framework/application guard concern
[PLUGIN]          external provider / replaceable integration
[CROSS-CUTTING]   concern used by multiple slices
[SHARED SUPPORT]  reusable support artifact, not a business slice by default
[IMPLEMENTED]     implemented for its declared scope
[PARTIAL]         partially implemented
[PLANNED]         boundary selected but implementation not yet done
```

Boundary decisions:

```text
Protected active account guard is not a standalone business slice.
It is [AUTH/FRAMEWORK][CROSS-CUTTING] and is applied inside protected application slices.

Antiforgery token/session-context support is not a business slice.
It is [SHARED SUPPORT][AUTH/FRAMEWORK][CROSS-SLICE].
```

## 4. Composite L1 Slice Overview

| Slice | Scenario | Type / layers | Package / marker | Boundary status | Implementation status | Main gap |
|---|---|---|---|---|---|---|
| SL-ACC-001 Register client account | SC-01 | Backend / API / persistence; future Client/UI | [L1][IMPLEMENTED] | Valid slice | Implemented | Auth Client/UI and auth extensions are separate |
| SL-APPL-001 Create individual ApplicantParty | SC-10 | Backend / API / persistence; dependent Client/UI | [L1][IMPLEMENTED] | Valid slice | Implemented | Replacement/current-active workflow is separate |
| SL-REQ-001 Create ConnectionRequest | SC-04 | Backend / API / persistence; dependent Client/UI/read | [L1][IMPLEMENTED] | Valid slice | Implemented | Client/UI slice missing |
| SL-REQ-UI-001 Request creation Client/UI | SC-04 | Client/UI + API client + shared support | [CLIENT/UI][DEPENDENT][PLANNED] | Valid dependent slice | Not implemented | Needs UI plan from separate UI planning chat |
| SL-REVIEW-001 Approve request and verify ApplicantParty | SC-07B | Backend / API / persistence; dependent Client/UI/read | [L1][PARTIAL] | Valid slice | Domain foundation only | Needs application/API/persistence slice |
| SL-REVIEW-002 Reject request | SC-07B | Backend / API / persistence; dependent Client/UI/read | [L1][PARTIAL] | Valid slice | Domain foundation only | Needs application/API/persistence slice |
| SL-REQ-READ-001 My Requests visibility | SC-05 | Read/query + API + Client/UI | [READ][DEPENDENT][PLANNED] | Valid dependent slice | Not implemented | Needs read projection/access planning |
| AUTH-GUARD-001 Active account guard | Cross-scenario | Application/auth framework concern | [AUTH/FRAMEWORK][CROSS-CUTTING] | Not business slice | Partially supported by domain foundation | Must be applied inside protected slices |
| CSRF-SUPPORT-001 Antiforgery token/session context | Cross-scenario | Client + ASP.NET Core antiforgery support | [SHARED SUPPORT][AUTH/FRAMEWORK][CROSS-SLICE] | Not business slice | Planned | Needed for unsafe cookie-auth client requests |

## 5. Boundary Questions Overview

| Question | Affects slice boundary? | Related scenario/slice | Candidate answer | Blocks next slice? | ADR? |
|---|---|---|---|---|---|
| Is active account guard a standalone slice? | Yes | Protected client actions | No. It is auth/framework/application guard inside protected slices. | No | Maybe |
| Is antiforgery token management a standalone slice? | Yes | Unsafe client requests | No. It is shared auth/framework support used by client-visible slices. | No | Maybe |
| Should request creation and My Requests visibility be one slice? | Yes | SC-04 / SC-05 | No. Command and read visibility are independently testable. | No | Yes |
| Should request creation Client/UI be part of SL-REQ-001? | Yes | SC-04 | No. It is a dependent Client/UI slice using completed backend/API behavior. | No | Maybe |
| Should documents be part of request creation? | Yes | SC-04 / SC-11 | No. Documents are an extension slice. | No | Maybe |
| Should approve and applicant verification be one slice? | Yes | SC-07B | Yes for L1 application slice: one use case coordinates both in transaction. | Yes before implementation | Yes |
| Should reject feedback warning be part of domain rejection slice? | Yes | SC-07B | No. Domain allows optional feedback; warning is Client/UI behavior. | No | Maybe |
| Should future agreement proposal be part of approval slice? | Yes | SC-07B / SC-13D | No. Approval enables proposal but does not create it. | No | Yes |

## 6. Scenario Flow Assignment Coverage Overview

| Scenario | Scenario flow part | DATA/items/rules included | Assigned slice/support | Marker | Boundary status | Notes |
|---|---|---|---|---|---|---|
| SC-01 | Register account | ACC-CMD-REGISTER-001, ACC-LC-001, email/password | SL-ACC-001 | [L1] | Assigned | Current core creates active account |
| SC-01 | Login/logout unsafe request support | auth/session state, antiforgery request token | CSRF-SUPPORT-001 | [SHARED SUPPORT] | Support artifact | Token should be refreshed after auth state changes |
| SC-10 | Save individual applicant data | APPL-CMD-SAVE-001, APPL-VI-001, full name/email/phone | SL-APPL-001 | [L1] | Assigned | Current implementation exists |
| SC-10 | Applicant form deferred validation | form fields, delayed validation | CLIENT-VALIDATION-SUPPORT-001 | [SHARED SUPPORT] | Support artifact | Used by Client/UI slices |
| SC-04 | Create request command | REQ-CMD-CREATE-001, REQ-LC-001, REQ-IBS-003, REQ-VI-001 | SL-REQ-001 | [L1] | Assigned | Implemented with InReview initial status |
| SC-04 | Request creation Client/UI | fields, form errors, submit, success, navigation | SL-REQ-UI-001 | [CLIENT/UI][DEPENDENT] | Separate slice | Next likely implementation after UI plan |
| SC-04 | Applicant data prefill in request form | selected ApplicantParty data | PREFILL-SUPPORT-001 / SL-REQ-UI-001 | [SHARED SUPPORT] / [CLIENT/UI] | Note for future slice | Must not mutate saved ApplicantParty |
| SC-04 / SC-05 | Client sees own request | REQ-READ-001 | SL-REQ-READ-001 | [READ][DEPENDENT] | Separate slice | Read/query projection |
| SC-07B | Approve request | REQ-CMD-APPROVE-001, REQ-LC-002, REQ-IBS-001 | SL-REVIEW-001 | [L1] | Assigned | Domain exists; app slice planned |
| SC-07B | Reject request | REQ-CMD-REJECT-001, REQ-LC-003, REQ-IBS-002 | SL-REVIEW-002 | [L1] | Assigned | Domain exists; app slice planned |
| SC-07B | Empty feedback warning | Client/UI warning candidate | SL-REVIEW-UI-002 | [CLIENT/UI][DEPENDENT] | Separate slice | Domain feedback remains optional |
| Cross-slice | Server validation errors to client messages | ProblemDetails/validation payload | ERROR-MAPPING-SUPPORT-001 | [SHARED SUPPORT] | Support artifact | Used by multiple Client/UI slices |

## 7. Existing L1 Foundation Context

Current L1 domain foundation supports:

```text
ClientAccount / Account activation marker
ApplicantParty / IndividualApplicantParty / verification status
ConnectionRequest / RequestStatus / ReviewDecisionRecord
Approve / Reject domain behavior
```

Current SL-REQ-001 implementation alignment:

```text
ConnectionRequest domain/API/persistence/integration expectation uses InReview.
Submitted is legacy terminology and not an active L1 request creation status.
```

## 8. Next Implementation Planning Note

The next work is expected to complete missing Client/UI for existing server-side logic.

Before implementing it, get/prepare a UI plan for the concrete page and form.

Likely target:

```text
SL-REQ-UI-001 — Request creation Client/UI
```

It depends on:

```text
SL-REQ-001 — Create ConnectionRequest from ApplicantParty
```

It will use shared support:

```text
CLIENT-VALIDATION-SUPPORT-001 — deferred client validation
ERROR-MAPPING-SUPPORT-001 — server validation errors to client errors
CSRF-SUPPORT-001 — antiforgery token/session context for unsafe requests
PREFILL-SUPPORT-001 — applicant data prefill notes
```

## 9. Decisions / Resolved Boundary Questions

| Decision | Context | Alternatives | Chosen direction | Reason | Consequence | Related slices/support |
|---|---|---|---|---|---|---|
| Request creation Client/UI is a dependent slice | SC-04 | Include UI in backend slice vs separate Client/UI slice | Separate dependent Client/UI slice | Server/API behavior is already implemented and testable; client can be added next | Client/UI gets own slice and tests | SL-REQ-001, SL-REQ-UI-001 |
| Antiforgery token management is shared support | Cookie auth unsafe requests | Business slice vs shared support | Shared auth/framework support | No independent business behavior; used by many unsafe requests | Document and test as helper/support plus slice tests | CSRF-SUPPORT-001 |
| Deferred validation is shared Client/UI support | Client forms | Per-slice duplication vs shared support | Shared support pattern with per-slice concrete usage | Many forms use same delayed validation idea | Helper/client tests plus per-slice form tests | CLIENT-VALIDATION-SUPPORT-001 |
| Server error mapping is shared Client/UI support | Validation/problem responses | Per-slice mapping vs shared mapper | Shared support with per-slice field mappings | Consistent client language and field/global errors | Helper tests plus slice client tests | ERROR-MAPPING-SUPPORT-001 |
| Applicant data prefill is a Client/UI note for request slice | Request form | Mutate saved applicant vs prefill request form | Prefill only; no mutation of saved ApplicantParty | Matches domain rule that request data changes do not update saved applicant data | Detailed planning in SL-REQ-UI-001 | SL-REQ-UI-001 |

## 10. ADR Candidates

| ADR candidate | Decision needed | Related slices/support | Urgency |
|---|---|---|---|
| ASP.NET Core cookie auth + antiforgery token support | How client fetches/stores/refetches token and server issues/validates it | CSRF-SUPPORT-001 | High before broad unsafe Client/UI work |
| Client/server validation error mapping | Shape of server errors and client field/global mapping | ERROR-MAPPING-SUPPORT-001 | Medium |
| Deferred client validation pattern | Delayed validation behavior and tests | CLIENT-VALIDATION-SUPPORT-001 | Medium |
| Request creation Client/UI as dependent slice | Client page/form/navigation boundary | SL-REQ-UI-001 | High before client implementation |
