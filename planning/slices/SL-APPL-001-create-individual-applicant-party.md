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

## 2. Sources / Source Behavior Items

Sources:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
```

Behavior items:

```text
APPL-CMD-SAVE-001 — Save applicant data
APPL-VI-001 — Applicant data by applicant type
```

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Client                                       │
│ submits individual applicant data            │
│ full name + contact email + phone            │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ System                                       │
│ authenticated client account context is known│
└──────────────┬───────────────────────────────┘
               │ validate account/applicant data
               ▼
┌──────────────────────────────────────────────┐
│ Input accepted and account exists?           │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Create Individual     │   │ Validation/access error       │
│ ApplicantParty        │   │ no applicant party created    │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           ▼                              ▼
┌──────────────────────┐        ┌──────────────────────────┐
│ Link to account       │        │ Client corrects input or  │
│ current active        │        │ resolves account issue    │
│ Unverified            │        └──────────────────────────┘
└──────────┬───────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ ApplicantParty saved                         │
│ can be used by request creation slice        │
└──────────────────────────────────────────────┘

Out of this backend slice:
- applicant form UI;
- applicant replacement/versioning;
- external verification;
- entrepreneur/legal entity applicant types.
```

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

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ POST /api/l1/applicant-parties/individual    │
│ Body: full name + contact email + phone      │
└──────────────┬───────────────────────────────┘
               │ derive ClientAccountId
               │ from authenticated context
               ▼
┌──────────────────────────────────────────────┐
│ Application Handler                          │
│ create individual applicant party command    │
└──────────────┬───────────────────────────────┘
               │ ensure account exists /
               │ protected action allowed
               ▼
┌──────────────────────────────────────────────┐
│ Account exists and input accepted?           │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Domain               │   │ API error mapping             │
│ IndividualApplicant  │   │ validation ProblemDetails     │
│ Party.Create         │   │ no applicant write            │
└──────────┬───────────┘   └──────────────────────────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Persistence                                  │
│ store applicant party linked to account      │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ API response                                 │
│ ApplicantPartyId + ClientAccountId           │
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

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

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/applicant-parties/individual` | POST | individual applicant party data | ApplicantPartyId + ClientAccountId | 200, 401, 403, 422, 500 | target L1 | yes |

Contract notes:

```text
- client does not submit arbitrary account ownership;
- account context is resolved by server/auth boundary;
- applicant contact email may differ from account email;
- missing account or invalid input returns validation ProblemDetails.
```

## 8. Questions / Decisions

Open questions:

| Question | Why it matters | Current answer / candidate | Blocks implementation? |
|---|---|---|---|
| One current active ApplicantParty per account or per applicant type? | Replacement slice | Unresolved; separate SL-APPL-002 | No |
| Should missing account return validation, unauthorized, forbidden, or not found? | API/security semantics | Current integration expects validation problem | No |
| Should applicant contact email duplicate account email? | Data semantics | No; applicant email may differ from account email | No |
| Should active-account guard be inside ApplicantParty factory? | Boundary | No; application/auth concern | No |

Accepted decisions:

```text
Decision:
ApplicantParty creation uses clientAccountId, not ClientAccount domain object.

Reason:
Active account guard belongs to application/auth boundary.

Consequence:
Application service coordinates account lookup/guard before creating applicant party.
```

## 9. Behavior Coverage

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| APPL-CMD-SAVE-001 — Save applicant data | API/application/domain flow creates individual applicant party from accepted input. | Scenario Slice Flow / Implementation Flow | covered |
| APPL-VI-001 — Applicant data by applicant type | Individual data shape is validated before save. | Scenario Slice Flow / Visual Implementation Flow | covered |
| ApplicantParty linked to account | Application resolves account context and persists ClientAccountId. | Visual Implementation Flow / Implementation Flow | covered |
| ApplicantParty starts Unverified/current active | Scenario flow and domain notes record initial state. | Scenario Slice Flow / Behavior Coverage | covered |
| Applicant replacement/versioning | Explicitly separate slice. | Slice Overview / Questions | deferred |
| Applicant UI behavior | Explicitly separate dependent slice. | Visual Scenario Flow / UI Blueprint | separate UI slice |

## 10. Test / Verification Plan

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

## 11. Dependent / Follow-up Slices

```text
[EXTENSION][L1/L2] SL-APPL-002 — Replace current active ApplicantParty version
[EXTENSION][L2] SL-APPL-TYPE-ENT-001 — Entrepreneur applicant type
[EXTENSION][L2] SL-APPL-TYPE-LEGAL-001 — Legal entity applicant type
[PLUGIN][DEPENDENT] SL-VER-001 — Applicant verification through external provider
[DEPENDENT] SL-REQ-001 — Create request from ApplicantParty
[UI][DEPENDENT] SL-APPL-UI-001 — Applicant data form UI
```

## 12. Implementation Checklist

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
