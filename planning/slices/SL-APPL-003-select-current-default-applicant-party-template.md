# SL-APPL-003 — Select Current/Default ApplicantParty Template

Status: full server/backend slice draft / planned command endpoint / implementation-ready after review  
Package: `[L1]`  
Slice type: backend/API command slice  
Source scenario: `SC-10 Applicant Parties Page`, `SC-10B Applicant Parties Future Management`  
Current implementation status: planned / not implemented as command endpoint

## 1. Slice Overview

This slice owns explicit current/default switching for saved ApplicantParties.

Target user/system behavior:

```text
Client explicitly selects one saved ApplicantParty as current/default template for its ApplicantPartyType.

This action lives on the same Applicant Parties page / section.

Previous current/default ApplicantParty for that type is unset.

Future request creation may use the selected ApplicantParty as initial default/prefill.

Existing requests remain unchanged.
```

This slice is explicit user switching after saved ApplicantParties already exist.

It is not first-of-type initialization during create.

First-of-type initialization is already handled by:

```text
SL-APPL-001 — Create Individual ApplicantParty
ApplicantPartyCreationService
```

## 2. Scope

```text
- add backend/API command endpoint for explicit make current/default;
- authenticate current L1 client account;
- derive current account id from L1 auth/session claims;
- verify selected ApplicantParty belongs to current account;
- determine selected ApplicantPartyType from selected ApplicantParty;
- affect only ApplicantParties of selected ApplicantPartyType;
- unset previous same-type current/default ApplicantParty if different;
- mark selected ApplicantParty as current/default;
- keep other ApplicantPartyType defaults unchanged;
- keep existing requests unchanged;
- SaveChanges once;
- return command success.
```

Target endpoint direction:

```text
POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
```

## 3. Out of Scope

| Out-of-scope item | Owner / destination |
|---|---|
| First-of-type default/current initialization | Already `SL-APPL-001` / `ApplicantPartyCreationService` |
| Account-level ApplicantParty read model | `SL-APPL-002` |
| Client button/UI/action | Future `SL-APPL-003.client` |
| Request creation applicant picker | Future `SL-REQ-001.client` |
| Delete/archive lifecycle | Future ApplicantParty lifecycle slice |
| Edit/version lifecycle | Future ApplicantParty edit/version slice |
| Renaming `IsCurrentActiveVersion` | Cleanup/default-template naming task |
| Generated OpenAPI/client artifacts | Implementation/generation workflow, not this draft |
| Runtime code changes | Future implementation prompt |
| Planning docs mutation | Out of implementation task unless explicitly requested |

## 4. Related Slices / Owners

```text
SL-APPL-001 — Create Individual ApplicantParty
Owns additive create and first-of-type default/current initialization.

SL-APPL-002 — Account Applicant Parties Read / Templates
Owns GET /api/l1/applicant-parties and exposes isCurrentDefault.

SL-APPL-003 — Select Current/Default ApplicantParty Template
Owns explicit make current/default command.

SL-REQ-001 — Create Connection Request With Applicant Context
Request creation may use default/current as initial prefill/default, but existing requests are not rewritten.

Future SL-APPL-003.client
Owns same-page client button/action and refresh/visible state behavior.

Future ApplicantParty lifecycle slices
Own delete/archive/edit/version semantics.
```

## 5. Sources / Source Behavior Items

Read source mapping through:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Primary sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/domain/applicantparty-domain-model.md
```

Relevant behavior/UI items:

```text
SC-10-BI-005 — Account may have one current/default ApplicantParty template per applicant type.
SC-10-BI-007 — Additional same-type create does not change current/default implicitly.
SC-10-BI-008 — Explicit current/default selection is separate future behavior on same Applicant Parties page.
SC-10-BI-009 — Existing requests are not changed by ApplicantParty creation or default/current changes.
SC-10-UI-010 — Explicit make default/current action is future same-page behavior.
```

## 6. Current Repo Facts Used By This Draft

Current domain direction:

```text
ApplicantParty starts non-current/default.
ApplicantParty exposes MarkInactiveVersion().
ApplicantParty exposes MarkAsCurrentDefaultTemplate().
ApplicantPartyCreationService handles first-of-type initialization.
Repository has owned lookup and account-owned list methods that implementation can reuse or extend.
```

Current persisted marker:

```text
IsCurrentActiveVersion
```

Target API/docs wording:

```text
isCurrentDefault
current/default template
```

## 7. Visual Scenario Flow

```text
Client opens Applicant Parties page / section
        ↓
Client sees current/default templates highlighted
        ↓
Client sees other saved ApplicantParties below
        ↓
Client explicitly chooses “make default/current” on one saved ApplicantParty
        ↓
System verifies the selected ApplicantParty belongs to the current account
        ↓
System marks the selected ApplicantParty as current/default for its ApplicantPartyType
        ↓
Previous same-type current/default ApplicantParty is unset
        ↓
Applicant Parties page can show the selected ApplicantParty as highlighted
        ↓
Existing requests remain unchanged
```

Invalid ownership / missing selection flow:

```text
Client chooses “make default/current” for a missing or not-owned ApplicantParty
        ↓
System rejects the command
        ↓
System does not change ApplicantParty default/current state
        ↓
Existing requests remain unchanged
```

## 8. Scenario Slice Flow

| Step | Actor/system | Behavior | Source/item | Scope status |
|---|---|---|---|---|
| S01 | Client | Opens Applicant Parties page / section. | SC-10/SC-10B | client page context |
| S02 | Client | Sees current/default highlighted and other saved ApplicantParties. | SC-10-UI | client page context |
| S03 | Client | Explicitly chooses make default/current on a saved ApplicantParty. | SC-10-BI-008 / SC-10-UI-010 | command trigger |
| S04 | System | Verifies selected ApplicantParty belongs to current account. | ownership/security boundary | in scope |
| S05 | System | Changes current/default only for selected ApplicantPartyType. | SC-10-BI-005 | in scope |
| S06 | System | Unsets previous same-type current/default if different. | SC-10-BI-005 | in scope |
| S07 | System | Does not treat create as implicit switch. | SC-10-BI-007 | boundary/reference |
| S08 | System | Leaves existing requests unchanged. | SC-10-BI-009 | in scope |

## 9. Visual Backend Implementation Flow

```text
[HTTP]
POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
        ↓
[Authorize]
authenticated L1 client account required
        ↓
[Controller]
parse route applicantPartyId
derive current account id from L1 auth claims/session
        ↓
[Command]
L1MakeApplicantPartyCurrentDefaultCommand(accountId, applicantPartyId)
        ↓
[Handler]
load selected ApplicantParty owned by current account
        ↓
[Application / Domain]
determine selected ApplicantPartyType
load same-account ApplicantParties for selected type
        ↓
Branch:
  selected already current/default
    -> idempotent success / no extra state change

  selected not current/default
    -> mark previous same-type current/default inactive
    -> mark selected ApplicantParty current/default
        ↓
[Persistence]
SaveChanges once
        ↓
[Response]
command success
```

Repository shape is implementation choice, not a draft requirement:

```text
Option A:
Use ListOwnedByAccountIdAsync and filter same type in handler.

Option B:
Add ListOwnedByAccountIdAndTypeAsync(accountId, applicantPartyType).

Option C:
Use GetOwnedByIdAsync for selected entity plus GetCurrentDefaultByAccountIdAndTypeAsync for previous default.
```

Implementation should choose the smallest clear repository shape that keeps ownership checks and same-type scope explicit.

## 10. API Contract Direction

Endpoint direction:

```text
POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
```

Route input:

```text
applicantPartyId: number / long
```

Request body:

```text
none
```

Success response direction:

```text
200 OK with no required response body
or
204 No Content
```

Prefer no required response body unless client implementation needs updated summary.

Expected statuses:

```text
200/204 success
401 unauthenticated
404/403/422 for missing/not-owned selected ApplicantParty, following current L1 convention
500 unexpected error
```

Generated artifacts are not edited manually. Implementation that changes API contract must use the OpenAPI generation workflow.

## 11. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `SL-APPL-003-Q-001` | accepted direction | Is make default/current implicit on create? | No. This slice is explicit user action after first-of-type initialization. | Keeps create additive and avoids silent switching. |
| `SL-APPL-003-Q-002` | accepted direction | Is this action on a separate page? | No. Same Applicant Parties page / section. | Client sidecar placement. |
| `SL-APPL-003-Q-003` | accepted direction | Do existing requests change? | No. Existing requests remain unchanged. | Data safety. |
| `SL-APPL-003-Q-004` | proposed direction | Exact endpoint path/name? | `POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default`. | API contract and generated artifacts during implementation. |
| `SL-APPL-003-Q-005` | implementation decision | Success response body? | Prefer no required body unless client implementation needs updated summary. | Client cache/update strategy. |
| `SL-APPL-003-Q-006` | implementation decision | Missing/not-owned selected ApplicantParty response? | Follow current L1 convention; choose consistently during implementation. | Security semantics and tests. |
| `SL-APPL-003-Q-007` | accepted direction | Idempotency when selected is already current/default? | Idempotent success is acceptable. | Handles repeated clicks/retries safely. |
| `SL-APPL-003-Q-008` | implementation decision | Repository method shape? | Keep as implementation choice; avoid over-prescribing in draft. | Minimal code changes. |
| `SL-APPL-003-Q-009` | accepted direction | Should command be limited to one current/default per ApplicantPartyType? | Yes. Only selected type changes; other types unchanged. | Preserves per-type default model. |

## 12. Extension / Change Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-APPL-DEFAULT-003` | explicit default action | Same-page explicit make current/default action belongs to this slice. | implementation-ready after draft |
| `CP-APPL-TYPES-001` | ApplicantParty types | Command should be type-aware; current implementation may only have Individual. | future extension |
| `CP-APPL-NAMING-001` | default marker naming | Continue using `IsCurrentActiveVersion` as persisted marker; API/docs use current/default wording. | future cleanup |
| `CP-REQ-APPL-PICKER-001` | request creation picker | Request UI may use selected current/default as initial prefill/default only. | future client work |
| `CP-APPL-LIFECYCLE-001` | delete/archive | Delete/archive remains future lifecycle work and must not be introduced here. | future review |

## 13. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Source behavior item | Expected behavior | Covered by this slice |
|---|---|---|
| `SC-10-BI-005` | Account may have one current/default ApplicantParty template per applicant type. | yes — selected type ends with one current/default. |
| `SC-10-BI-007` | Additional same-type create does not change current/default implicitly. | yes — this slice is explicit user action, not create side effect. |
| `SC-10-BI-008` | Explicit current/default selection is separate future behavior on same Applicant Parties page. | yes — this slice owns that explicit server command. |
| `SC-10-BI-009` | Existing requests are not changed by ApplicantParty creation or default/current changes. | yes — command updates ApplicantParty marker only. |
| `SC-10-UI-010` | Explicit make default/current action belongs to future same-page behavior. | server supports future client action; UI remains out of scope. |

Behavior coverage is proven mainly by API integration tests with DB state before/after command.

The endpoint response proves command boundary.
The DB assertions prove behavior:

```text
- selected current/default changed;
- previous same-type current/default unset;
- existing requests unchanged;
- unrelated ApplicantParty data unchanged.
```

Not behavior items:

```text
- repository method exists;
- handler calls SaveChanges;
- route constant generated;
- OpenAPI type generated;
- React Query invalidated;
- mocks were called.
```

## 14. Test / Verification Plan

Primary verification: API integration tests with direct DB state assertions.

Do not use mocks as primary proof.
Do not assert repository/handler mock call order.
The important behavior is persisted state transition.

### 14.1 API boundary / access tests

These tests verify endpoint access and rejection behavior.

#### 14.1.1 Unauthenticated request returns 401

Test shape:

```text
- call POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default without auth;
- assert 401;
- no DB state assertions required unless setup already created data cheaply.
```

#### 14.1.2 Missing ApplicantParty returns documented rejection response

Test shape:

```text
- create authenticated client account;
- optionally create one current/default ApplicantParty for the account;
- capture ApplicantParty default/current state before command;
- call endpoint with non-existing applicantPartyId;
- assert chosen convention: 404 or validation/problem response;
- reload ApplicantParty rows;
- assert default/current flags unchanged.
```

#### 14.1.3 Not-owned ApplicantParty is rejected

Test shape:

```text
- account A owns ApplicantParty A1;
- account B is authenticated;
- optionally account B owns ApplicantParty B1;
- capture A1 and B1 current/default flags before command;
- account B calls make-current-default for A1;
- assert chosen convention: 404 / 403 / 422 according to current L1 convention;
- reload A1 and B1;
- assert A1 flags unchanged;
- assert B1 flags unchanged.
```

This should be a separate test because it protects ownership/security behavior.

### 14.2 Main DB state transition test

This is the core serious test for the slice.

#### 14.2.1 Owned non-default ApplicantParty becomes current/default and previous same-type current/default is unset

Test shape:

```text
- create account;
- create first Individual ApplicantParty;
- assert first IsCurrentActiveVersion == true;
- create second Individual ApplicantParty;
- assert second IsCurrentActiveVersion == false;
- call POST /api/l1/applicant-parties/{secondId}/make-current-default;
- assert success;
- reload both ApplicantParty rows from DB;
- assert first IsCurrentActiveVersion == false;
- assert second IsCurrentActiveVersion == true;
- assert both ApplicantParties still exist;
- assert both ClientAccountId values did not change;
- assert both VerificationStatus values did not change;
- assert stable identity/contact/fullName fields did not change.
```

This one test can cover:

```text
- owned selected ApplicantParty can become current/default;
- previous same-type current/default is unset;
- command updates persisted DB state;
- command does not delete/hide/replace ApplicantParties;
- unrelated ApplicantParty fields are not rewritten.
```

### 14.3 Idempotency / no-op DB state test

#### 14.3.1 Selecting already current/default is safe and idempotent

Test shape:

```text
- create account;
- create first Individual ApplicantParty;
- assert first IsCurrentActiveVersion == true;
- capture DB row before command;
- call make-current-default for first ApplicantParty;
- assert success;
- reload row;
- assert IsCurrentActiveVersion remains true;
- assert no second ApplicantParty was created;
- assert stable fields did not change:
  - ClientAccountId
  - Email
  - PhoneNumber
  - VerificationStatus
  - FullName fields
```

This should not assert mocks or handler internals.
The proof is DB state unchanged except allowed no-op behavior.

### 14.4 Existing requests no-mutation test

This protects an important data-safety rule and should be separate from the main transition test unless setup cost becomes too high.

#### 14.4.1 Existing requests remain unchanged

Test shape:

```text
- create account;
- create first ApplicantParty;
- create request using first ApplicantParty;
- create second same-type ApplicantParty;
- capture request row before command:
  - Id
  - ApplicantPartyId
  - Status
  - Details
  - CreatedAt if available
- call make-current-default for second ApplicantParty;
- reload request row;
- assert request row still exists;
- assert request ApplicantPartyId is still first ApplicantPartyId;
- assert Status unchanged;
- assert Details unchanged;
- assert CreatedAt unchanged if available in helper.
```

Do not test request creation behavior here.
Only prove existing request rows are not relinked or rewritten by default switching.

### 14.5 Same-type scope test

#### 14.5.1 Current implementation coverage

Current repo may only support creating `IndividualApplicantParty`. If only Individual exists, do not force fake future types into this slice.

Current pass:

```text
- verify same-type behavior with two Individual ApplicantParties;
- selected Individual becomes current/default;
- previous Individual current/default is unset.
```

#### 14.5.2 Future multi-type coverage

Future pass when `IndividualEntrepreneur` / `LegalEntity` or another supported type exists:

```text
- create current/default Individual;
- create current/default LegalEntity or another supported type;
- switch Individual default;
- assert other type default remains unchanged.
```

Mark multi-type verification as future coverage if the domain/API does not yet support creating other ApplicantParty types.

Required design rule remains:

```text
Other ApplicantPartyType preservation is required by design,
but concrete integration coverage waits until another ApplicantPartyType is implemented.
Current implementation should still keep command logic type-aware where practical.
```

### 14.6 Create behavior regression guard

This may already be covered by `SL-APPL-001` tests. For `SL-APPL-003`, keep or reference the existing regression if present.

#### 14.6.1 Second same-type create still does not switch default implicitly

Test shape:

```text
- create account;
- create first Individual ApplicantParty;
- create second Individual ApplicantParty;
- assert first IsCurrentActiveVersion == true;
- assert second IsCurrentActiveVersion == false;
- do not call make-current-default in this test.
```

Purpose:

```text
- prove SL-APPL-003 remains explicit action only;
- create flow does not silently switch default/current.
```

This test can stay in create slice coverage if it already exists and is stable. The implementation prompt should avoid duplicating it unless there is no existing guard.

### 14.7 What not to test

Do not add tests for:

```text
- repository mocks;
- handler call order;
- mock SaveChanges call count;
- React Query invalidation;
- client button rendering;
- OpenAPI generation as behavior proof;
- generated TypeScript types as behavior proof;
- delete/archive/edit lifecycle;
- request creation applicant picker.
```

## 15. Implementation Checklist

```text
[ ] Add command endpoint route.
[ ] Add command DTO/record if needed.
[ ] Add handler.
[ ] Load selected owned ApplicantParty.
[ ] Determine selected ApplicantPartyType.
[ ] Load same-account/same-type ApplicantParties or current default.
[ ] If selected already current/default, return idempotent success.
[ ] If previous same-type current/default exists and differs, mark it inactive.
[ ] Mark selected current/default.
[ ] SaveChanges once.
[ ] Add API boundary/access tests.
[ ] Add DB transition test.
[ ] Add idempotency/no-op test.
[ ] Add existing requests no-mutation test.
[ ] Add or reference second-create regression guard.
[ ] Run OpenAPI generation/check workflow if API contract changes.
```

## 16. Next Step

Prepare implementation prompt for `SL-APPL-003` that preserves this boundary:

```text
Implement only the backend/API command for explicit make current/default.

Do not modify planning docs.
Do not implement client UI.
Do not implement delete/archive/edit.
Do not change request creation.
Do not rename IsCurrentActiveVersion.
Do not manually edit generated artifacts.
Use repo generation commands if API contract changes.
Use API integration tests with DB state assertions as primary proof.
Do not use mocks as primary behavior proof.
```

Implementation should first verify the current code state again:

```text
- ApplicantParty still starts non-current by default;
- MarkInactiveVersion / MarkAsCurrentDefaultTemplate still exist;
- ApplicantPartyCreationService still handles first-of-type initialization;
- SL-APPL-002 read endpoint still exposes isCurrentDefault for client refresh/grouping;
- existing integration test helpers can read ApplicantParty and request DB rows.
```
