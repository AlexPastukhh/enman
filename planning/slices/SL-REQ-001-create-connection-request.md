# SL-REQ-001 — Create ConnectionRequest From ApplicantParty

Status: implemented slice draft  
Package: `[L1]`  
Source scenario: `SC-04 Client Request Creation`  
Slice type: backend / API / persistence slice with dependent Client/UI/read/extension slices  
Current implementation status: implemented; active API/persistence/integration expectation uses `InReview`

## 1. Slice Overview

Observable behavior:

```text
Client creates a connection request from an existing ApplicantParty.
Accepted request is persisted and starts with target status InReview.
```

Why this is a real slice:

```text
- separate command behavior;
- clear success result: request is created;
- clear failure/no-write behavior: invalid request does not create request;
- independently testable through domain unit tests and API/persistence integration tests;
- does not require Client/UI, documents, notifications, agreements, or My Requests read view.
```

Related extension / dependent slices:

```text
[CLIENT/UI][DEPENDENT] SL-REQ-UI-001 — Request creation Client/UI
[READ][DEPENDENT] SL-REQ-READ-001 — My Requests visibility
[EXTENSION] SL-DOC-001 — Attach documents to request
[EXTENSION] SL-ANON-001 — Anonymous request creation
[DEPENDENT] SL-REVIEW-001 — Approve request and verify applicant
[DEPENDENT] SL-REVIEW-002 — Reject request
```

Shared support likely used by dependent Client/UI slice:

```text
[SHARED SUPPORT] CLIENT-VALIDATION-SUPPORT-001 — deferred client validation
[SHARED SUPPORT] ERROR-MAPPING-SUPPORT-001 — server validation errors to field/global client errors
[SHARED SUPPORT] CSRF-SUPPORT-001 — antiforgery token/session context for unsafe requests
[SHARED SUPPORT] PREFILL-SUPPORT-001 — applicant data prefill notes
```

## 2. Questions Overview

| Question | Why it matters | Current answer / candidate | Blocks implementation? |
|---|---|---|---|
| Should integration expectation be updated from Submitted to InReview? | Full test conflict | Resolved: aligned with target InReview | No |
| Should API expose InReview directly or map display text? | Contract/Client UI | API exposes domain status InReview directly | No |
| Is ApplicantParty ownership mismatch checked? | Security/access | Needs confirmation/add test | Before production |
| Should request creation require current active ApplicantParty? | Version semantics | Clarify before hardening | Medium |
| Should validation tests assert no persisted row? | No-write guarantee | Add if feasible | No |
| What concrete Client/UI page/form implements request creation? | Needed for dependent Client/UI implementation | Get UI plan from UI planning chat | Yes for SL-REQ-UI-001 |
| How should applicant data be prefilled into request form? | Prevents accidental mutation of saved ApplicantParty | Prefill only; no saved ApplicantParty mutation | Yes for SL-REQ-UI-001 |
| How are unsafe requests protected from CSRF? | Cookie auth requires antiforgery handling | Use shared antiforgery token/session support | Yes before broad Client/UI unsafe requests |

## 3. Flow Coverage Overview

| Flow part | Current coverage | Missing / separate slice |
|---|---|---|
| Create request from ApplicantParty | Implemented | ownership mismatch unclear |
| Request starts InReview | Domain/API/persistence/integration implemented | - |
| Missing ApplicantParty validation | Integration-tested | wrong-owner case missing |
| Empty details validation | Integration-tested | no-row assertion missing |
| Object address persistence | Successful path partially covered | missing/invalid address test |
| Request creation Client/UI | Not in this slice | SL-REQ-UI-001 |
| Deferred client validation | Shared support/current client pattern | document/use in SL-REQ-UI-001 |
| Server error mapping to client messages | Shared support/future implementation | ERROR-MAPPING-SUPPORT-001 |
| CSRF token support for unsafe request | Shared support/future implementation | CSRF-SUPPORT-001 |
| My Requests visibility | Not in this slice | SL-REQ-READ-001 |
| Documents | Not in this slice | SL-DOC-001 |

## 4. Scenario Slice Flow

### F01 — Client starts request creation

DATA: current client context.  
Rule: protected client action; active-account guard is application/auth concern.

### F02 — Client selects ApplicantParty

DATA: ApplicantPartyId, applicant source/contact data.  
Items: `REQ-UCQ-001`.  
Rule: request uses ApplicantParty without mutating saved applicant data.

### F03 — System checks ApplicantParty belongs to current client

DATA: ApplicantParty.ClientAccountId, current ClientAccountId.  
Rule: client must not create request using another client’s ApplicantParty.

### F04 — Client provides request details

DATA: request details text.  
Items: `REQ-CMD-CREATE-001`.  
No-write: invalid or empty details do not create request.

### F05 — Client provides object address

DATA: object address.  
Items/invariants: `REQ-IBS-003`, `REQ-VI-001`.  
Invariant: request cannot exist without object address.

### F06 — System creates ConnectionRequest

DATA: ApplicantPartyId, details, object address.  
Items: `REQ-LC-001`.  
Expected result: request is created with status InReview.

### F07 — System persists request and returns result

DATA: RequestId, ApplicantPartyId, Status.

## 5. Implementation Flow

### I01 — Client/UI Flow

Current implementation:

```text
Client/UI is not part of this implemented backend/API/persistence slice.
It is planned as dependent slice SL-REQ-UI-001.
```

Concrete Client/UI details must come from the UI planning chat before implementation.

Expected Client/UI flow for dependent slice:

```text
Page:
Request creation page.

Form/component:
Connection request form.

Visible data:
- selected/current ApplicantParty summary;
- request details;
- object address fields;
- current validation state.

Fields:
- ApplicantParty selector or summary;
- request details textarea;
- object address fields.

Primary action:
Create request button.

Client-side validation:
- deferred validation after input changes;
- submit triggers immediate validation if needed;
- field errors are shown near affected fields;
- global/form errors are shown in a form-level area.

Applicant data prefill:
- if request form needs applicant fields, prefill from selected ApplicantParty;
- editing request-local fields must not mutate saved ApplicantParty.

Unsafe request support:
- submit uses current CSRF/antiforgery request token;
- token support comes from shared CSRF support.

Success:
- response status is InReview;
- UI shows success or navigates to request details / My Requests depending on UI plan.
```

### I02 — API endpoint / contract

`POST /api/l1/requests` receives ApplicantPartyId, details and address DTO.

API returns the domain status directly. Created requests return InReview.

### I03 — Server endpoint / handler

Server endpoint accepts the request DTO and delegates to the application flow.

### I04 — Application service / orchestration

Resolve current account, apply active guard, load ApplicantParty, check ownership, call `ConnectionRequest.Create(...)`, persist request, return identity/status.

### I05 — Domain

`ConnectionRequest.Create(...)`, `ClientRequest`, `RequestStatus`, `Address`.

### I06 — Persistence / transaction

Request row is stored with ApplicantPartyId, Status, Details and ObjectAddress fields.

### I07 — Response mapping

Return created request id, applicant party id and status.

## 6. Test Plan / Test Coverage

### Client tests

Planned for dependent `SL-REQ-UI-001`:

```text
- request creation page renders expected form;
- deferred validation runs after input delay;
- invalid details/address show field-level errors;
- server validation/problem response maps to field/global errors;
- create button builds correct API request DTO;
- unsafe request uses antiforgery token helper;
- successful InReview response shows success/navigates according to UI plan;
- applicant data prefill works without mutating saved ApplicantParty.
```

### Server tests

Current integration coverage:

```text
CreateConnectionRequest_StoresGeneratedApplicantPartyReference
CreateConnectionRequest_ForMissingApplicantParty_ReturnsValidationProblem
CreateConnectionRequest_WithEmptyDetails_ReturnsValidationProblem
L1Flow_PropagatesEfGeneratedIdsAcrossAggregates
```

Expected/current domain unit coverage:

```text
Create_creates_in_review_request
Create_fails_without_details
Create_fails_without_object_address
Create_guards_transient_applicant_party
```

Needed server tests:

```text
- wrong-owner ApplicantParty test;
- object address failure test;
- no-write assertions if feasible.
```

### End-to-end tests

Planned after dependent Client/UI slice is implemented:

```text
- user creates request through browser/client UI;
- backend persists request;
- created request status is InReview;
- validation errors are visible when input is invalid.
```

## 7. Shared Support Used By This Slice

```text
planning/slices/shared/client-deferred-validation.md
planning/slices/shared/client-server-validation-error-mapping.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/shared/client-applicant-data-prefill-notes.md
```

## 8. Detailed Implementation Notes

This section is intentionally implementation-focused and may include pseudocode or code snippets in later drafts.

The next implementation document should likely be `SL-REQ-UI-001-request-creation-client-ui.md`, after getting a concrete UI plan.

## 9. Decisions

```text
Decision:
Request creation initial status is InReview.

Reason:
A created request immediately enters employee review queue.

Consequence:
Integration/API expectations align with InReview.
```

```text
Decision:
My Requests visibility is a separate read/query slice.
```

```text
Decision:
Request creation Client/UI is a dependent slice, not part of SL-REQ-001 backend implementation scope.

Reason:
Server/API/persistence behavior is already testable and implemented; client can be completed separately with its own tests.
```

## 10. ADR Links / Candidates

Potential candidates:

```text
- ASP.NET Core cookie auth + antiforgery token support.
- Client/server validation error mapping.
- Request creation Client/UI as dependent slice.
```

## 11. Implementation Checklist

```text
[x] Create request from ApplicantParty
[x] Persist ApplicantPartyId
[x] Persist details/address
[x] Missing ApplicantParty validation
[x] Empty details validation
[x] Domain initial status InReview
[x] Fix integration/API expectation from Submitted to InReview
[ ] Confirm/add ApplicantParty ownership mismatch check
[ ] Add missing object address integration test
[ ] Add no-write DB assertions for failure paths if feasible
[ ] Get concrete UI plan for request creation page/form
[ ] Create/refine SL-REQ-UI-001 request creation Client/UI slice file
[ ] Implement request creation Client/UI
[ ] Add client tests
[ ] Add E2E tests after Client/UI and server flow are stable
[ ] Plan dependent My Requests read slice
```
