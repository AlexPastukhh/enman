# SL-APPL-001 — Create Individual ApplicantParty

Status: implemented slice draft  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`  
Slice type: backend / API / persistence slice with dependent UI and request-creation slices  
Current implementation status: implemented and integration-tested

## 1. Slice Overview

Observable behavior:

```text
Client saves individual applicant data.
Accepted data creates persisted IndividualApplicantParty linked to client account.
```

Why this is a real slice:

```text
- saves reusable applicant data independently from request creation;
- request creation depends on ApplicantParty;
- clear success/failure behavior;
- independently testable through domain unit tests and API/persistence integration tests;
- applicant replacement, verification provider and future applicant types can be separate slices.
```

Related extension / dependent slices:

```text
[EXTENSION][L1/L2] SL-APPL-002 — Replace current active ApplicantParty version
[EXTENSION][L2] SL-APPL-TYPE-ENT-001 — Entrepreneur applicant type
[EXTENSION][L2] SL-APPL-TYPE-LEGAL-001 — Legal entity applicant type
[PLUGIN][DEPENDENT] SL-VER-001 — Applicant verification through external provider
[DEPENDENT] SL-REQ-001 — Create request from ApplicantParty
[UI][DEPENDENT] SL-APPL-UI-001 — Applicant data form UI
```

## 2. Questions Overview

| Question | Why it matters | Current answer / candidate | Blocks implementation? |
|---|---|---|---|
| One current active ApplicantParty per account or per applicant type? | Replacement slice | Unresolved; separate SL-APPL-002 | No |
| Should missing account return validation, unauthorized, forbidden, or not found? | API/security semantics | Current integration expects validation problem | No |
| Should applicant contact email duplicate account email? | Data semantics | No; applicant email may differ from account email | No |
| Should active-account guard be inside ApplicantParty factory? | Boundary | No; application/auth concern | No |

## 3. Flow Coverage Overview

| Flow part | Current coverage | Missing / separate slice |
|---|---|---|
| Individual applicant data accepted | Implemented | UI separate |
| ApplicantParty linked to account | Implemented/integration-tested | ownership/auth semantics review later |
| Starts Unverified/current active | Domain implemented | integration may not assert all state |
| Applicant replacement | Not in this slice | SL-APPL-002 |
| External verification | Not in this slice | SL-VER-001 |
| Future applicant types | Not in this slice | L2 extension slices |

## 4. Scenario Slice Flow

### F01 — Client starts applicant data save

DATA: current client account, authenticated client context.  
Rule: protected client action; active account guard is application/auth concern inside this slice.

### F02 — Client provides individual applicant data

DATA: full name, applicant contact email, phone number.  
Items: `APPL-CMD-SAVE-001`, `APPL-VI-001`.  
Clarification: applicant contact email may differ from account email.

### F03 — System validates applicant data shape

Rule: IndividualApplicantParty requires individual-specific data shape.  
No-write: invalid applicant data is not saved.

### F04 — System creates ApplicantParty

Expected result: IndividualApplicantParty is created, Unverified, current active, linked to ClientAccountId.

### F05 — System persists ApplicantParty

Expected result: ApplicantParty row exists and can be used by request creation.

### F06 — API returns ApplicantParty identity

DATA: ApplicantPartyId, ClientAccountId.

## 5. Implementation Flow

### I01 — UI blueprint

Current implementation: UI is not part of this backend/API/persistence slice.

### I02 — API

`POST /api/l1/applicant-parties/individual` receives full name, contact email and phone.

### I03 — Application service

Resolve current account context, ensure account exists/protected action is allowed, call `IndividualApplicantParty.Create(clientAccountId, ...)`, persist applicant party, return identity.

### I04 — Domain

`ApplicantParty`, `IndividualApplicantParty`, `ApplicantPartyType`, `ApplicantPartyVerificationStatus`, current-active marker.

### I05 — Persistence

ApplicantParty row is stored with ClientAccountId and individual data.

### I06 — Response mapping

Response returns ApplicantPartyId and ClientAccountId.

## 6. UI Blueprint

Dependent UI blueprint:

```text
- client opens applicant data form;
- enters full name, applicant contact email and phone;
- submits;
- sees validation errors or saved applicant data result;
- later can choose saved ApplicantParty during request creation.
```

## 7. Test Plan / Test Coverage

Current integration coverage:

```text
CreateIndividualApplicantParty_StoresGeneratedAccountReference
CreateIndividualApplicantParty_ForMissingAccount_ReturnsValidationProblem
```

Expected/current domain unit coverage:

```text
Create_creates_unverified_current_active_applicant
Create_fails_for_invalid_applicant_data
MarkVerified_succeeds_when_minimum_data_present
MarkInactiveVersion_marks_current_flag_false
```

## 8. Detailed Implementation Notes

This section is intentionally implementation-focused and may include pseudocode or code snippets in later drafts.

## 9. Decisions

```text
Decision:
ApplicantParty creation uses clientAccountId, not ClientAccount domain object.

Reason:
Active account guard belongs to application/auth boundary.

Consequence:
Application service coordinates account lookup/guard before creating applicant party.
```

## 10. ADR Links / Candidates

ADR candidates should be promoted to `planning/adr/adr-candidates.md` when the decision affects multiple slices or diploma-level architecture explanation.

## 11. Implementation Checklist

```text
[x] Create individual ApplicantParty
[x] Link ApplicantParty to client account
[x] Persist individual applicant data
[x] Return ApplicantParty identity
[x] Missing account validation problem
[ ] Plan replacement/current-active slice
[ ] Plan applicant UI slice
[ ] Plan future applicant types
```
