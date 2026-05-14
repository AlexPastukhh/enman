# SL-REQ-001 — Create ConnectionRequest From ApplicantParty

Status: implemented slice draft with known conflict  
Package: `[L1]`  
Source scenario: `SC-04 Client Request Creation`  
Slice type: backend / API / persistence slice with dependent UI/read/extension slices  
Current implementation status: implemented, but full integration suite has `Submitted` vs `InReview` conflict

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
- does not require UI, documents, notifications, agreements, or My Requests read view.
```

Related extension / dependent slices:

```text
[UI][DEPENDENT] SL-REQ-UI-001 — Request creation form UI
[READ][DEPENDENT] SL-REQ-READ-001 — My Requests visibility
[EXTENSION] SL-DOC-001 — Attach documents to request
[EXTENSION] SL-ANON-001 — Anonymous request creation
[DEPENDENT] SL-REVIEW-001 — Approve request and verify applicant
[DEPENDENT] SL-REVIEW-002 — Reject request
```

## 2. Questions Overview

| Question | Why it matters | Current answer / candidate | Blocks implementation? |
|---|---|---|---|
| Should integration expectation be updated from Submitted to InReview? | Full test conflict | Yes, align with target InReview | Yes |
| Should API expose InReview directly or map display text? | Contract/UI | Prefer domain status unless display mapping exists | Yes |
| Is ApplicantParty ownership mismatch checked? | Security/access | Needs confirmation/add test | Before production |
| Should request creation require current active ApplicantParty? | Version semantics | Clarify in this slice file before hardening | Medium |
| Should validation tests assert no persisted row? | No-write guarantee | Add if feasible | No |

## 3. Flow Coverage Overview

| Flow part | Current coverage | Missing / separate slice |
|---|---|---|
| Create request from ApplicantParty | Implemented | ownership mismatch unclear |
| Request starts InReview | Domain implemented | integration expectation still Submitted |
| Missing ApplicantParty validation | Integration-tested | wrong-owner case missing |
| Empty details validation | Integration-tested | no-row assertion missing |
| Object address persistence | Successful path partially covered | missing/invalid address test |
| Request creation UI | Not in this slice | SL-REQ-UI-001 |
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

### I01 — UI blueprint

Current implementation: UI is not part of this backend/API/persistence slice.

### I02 — API

`POST /api/l1/requests` receives ApplicantPartyId, details and address DTO.

Known conflict: API/integration expectation still uses Submitted in one test; target domain status is InReview.

### I03 — Application service

Resolve current account, apply active guard, load ApplicantParty, check ownership, call `ConnectionRequest.Create(...)`, persist request, return identity/status.

### I04 — Domain

`ConnectionRequest.Create(...)`, `ClientRequest`, `RequestStatus`, `Address`.

### I05 — Persistence

Request row is stored with ApplicantPartyId, Status, Details and ObjectAddress fields.

### I06 — Response mapping

Return created request id, applicant party id and status.

## 6. UI Blueprint

Dependent UI blueprint:

```text
- client opens request creation page;
- selects existing ApplicantParty or is guided to create one;
- enters request details;
- enters object address;
- submits;
- sees backend validation errors;
- on success sees created InReview request or navigates to request details/My Requests.
```

UI candidate items:

```text
UI-CAND-REQ-001: request creation form shows validation feedback.
UI-CAND-REQ-002: successful request creation shows InReview result or navigation.
```

## 7. Test Plan / Test Coverage

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

Needed: update Submitted expectation to InReview, wrong-owner test, object address failure test, no-write assertions if feasible.

## 8. Detailed Implementation Notes

This section is intentionally implementation-focused and may include pseudocode or code snippets in later drafts.

## 9. Decisions

```text
Decision:
Request creation initial status is InReview.

Reason:
A created request immediately enters employee review queue.

Consequence:
Integration/API expectations must align with InReview.
```

```text
Decision:
My Requests visibility is a separate read/query slice.
```

## 10. ADR Links / Candidates

ADR candidates should be promoted to `planning/adr/adr-candidates.md` when the decision affects multiple slices or diploma-level architecture explanation.

## 11. Implementation Checklist

```text
[x] Create request from ApplicantParty
[x] Persist ApplicantPartyId
[x] Persist details/address
[x] Missing ApplicantParty validation
[x] Empty details validation
[x] Domain initial status InReview
[ ] Fix integration/API expectation from Submitted to InReview
[ ] Confirm/add ApplicantParty ownership mismatch check
[ ] Add missing object address integration test
[ ] Add no-write DB assertions for failure paths if feasible
[ ] Plan dependent request creation UI slice
[ ] Plan dependent My Requests read slice
```
