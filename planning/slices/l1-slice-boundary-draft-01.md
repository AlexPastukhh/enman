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
Which parts are separate dependent/extension/UI/read/plugin slices?
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
7. It is not just an endpoint, controller, repository, DB table, UI component or aggregate.
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
[UI]              UI-focused slice
[READ]            read/query slice
[AUTH/FRAMEWORK]  auth/framework/application guard concern
[PLUGIN]          external provider / replaceable integration
[CROSS-CUTTING]   concern used by multiple slices
[IMPLEMENTED]     implemented for its declared scope
[PARTIAL]         partially implemented
[PLANNED]         boundary selected but implementation not yet done
```

Boundary decision:

```text
Protected active account guard is not a standalone business slice.
It is [AUTH/FRAMEWORK][CROSS-CUTTING] and is applied inside protected application slices.
```

## 4. Composite L1 Slice Overview

| Slice | Scenario | Type / layers | Package / marker | Boundary status | Implementation status | Main gap |
|---|---|---|---|---|---|---|
| SL-ACC-001 Register client account | SC-01 | Backend / API / persistence | [L1][IMPLEMENTED] | Valid slice | Implemented | Auth extensions are separate slices |
| SL-APPL-001 Create individual ApplicantParty | SC-10 | Backend / API / persistence, dependent UI | [L1][IMPLEMENTED] | Valid slice | Implemented | Replacement/current-active workflow is separate |
| SL-REQ-001 Create ConnectionRequest | SC-04 | Backend / API / persistence, dependent UI/read | [L1][IMPLEMENTED] | Valid slice | Implemented with status conflict | Submitted vs InReview expectation |
| SL-REVIEW-001 Approve request and verify ApplicantParty | SC-07B | Backend / API / persistence, dependent read/UI | [L1][PARTIAL] | Valid slice | Domain foundation only | Needs application/API/persistence slice |
| SL-REVIEW-002 Reject request | SC-07B | Backend / API / persistence, dependent UI/read | [L1][PARTIAL] | Valid slice | Domain foundation only | Needs application/API/persistence slice |
| SL-REQ-READ-001 My Requests visibility | SC-05 | Read/query + API + UI | [READ][DEPENDENT][PLANNED] | Valid dependent slice | Not implemented | Needs read projection/access planning |
| SL-EMP-READ-001 Employee request dashboard/details | SC-06 / SC-07A | Read/query + API + UI | [READ][DEPENDENT][PLANNED] | Valid dependent slice | Not implemented | Needs employee read model/action availability |
| SL-REQ-UI-001 Request creation form UI | SC-04 | UI + API client | [UI][DEPENDENT][PLANNED] | Valid UI slice | Not implemented | Depends on SL-REQ-001 API contract |
| SL-REVIEW-UI-001 Employee review UI | SC-07B | UI + API client | [UI][DEPENDENT][PLANNED] | Valid UI slice | Not implemented | Depends on review API slices |
| AUTH-GUARD-001 Active account guard | Cross-scenario | Application/auth framework concern | [AUTH/FRAMEWORK][CROSS-CUTTING] | Not business slice | Partially supported by domain foundation | Must be applied inside protected slices |

## 5. Boundary Questions Overview

| Question | Affects slice boundary? | Related scenario/slice | Candidate answer | Blocks next slice? | ADR? |
|---|---|---|---|---|---|
| Is active account guard a standalone slice? | Yes | Protected client actions | No. It is auth/framework/application guard inside protected slices. | No | Maybe |
| Should request creation and My Requests visibility be one slice? | Yes | SC-04 / SC-05 | No. Command and read visibility are independently testable. | No | Yes |
| Should request creation UI be part of SL-REQ-001? | Yes | SC-04 | No. UI is a dependent UI slice. | No | Maybe |
| Should documents be part of request creation? | Yes | SC-04 / SC-11 | No. Documents are an extension slice. | No | Maybe |
| Should approve and applicant verification be one slice? | Yes | SC-07B | Yes for L1 application slice: one use case coordinates both in transaction. | Yes before implementation | Yes |
| Should reject feedback warning be part of domain rejection slice? | Yes | SC-07B | No. Domain allows optional feedback; UI warning is dependent UI slice. | No | Maybe |
| Should Submitted be preserved as API value? | Yes | SC-04 / SL-REQ-001 | Current target is InReview; integration/API should align. | Yes | Yes |
| Should future agreement proposal be part of approval slice? | Yes | SC-07B / SC-13D | No. Approval enables proposal but does not create it. | No | Yes |

## 6. Scenario Flow Assignment Coverage Overview

| Scenario | Scenario flow part | DATA/items/rules included | Assigned slice | Marker | Boundary status | Notes |
|---|---|---|---|---|---|---|
| SC-01 | Register account | ACC-CMD-REGISTER-001, ACC-LC-001, email/password | SL-ACC-001 | [L1] | Assigned | Current core creates active account |
| SC-01 | Email confirmation / PendingActivation | account activation lifecycle | SL-AUTH-EMAIL-001 | [EXTENSION][L2] | Separate slice | Not part of L1 registration slice |
| SC-10 | Save individual applicant data | APPL-CMD-SAVE-001, APPL-VI-001, full name/email/phone | SL-APPL-001 | [L1] | Assigned | Current implementation exists |
| SC-10 | Replace current active applicant | current-active marker, version-like behavior | SL-APPL-002 | [EXTENSION][L1/L2] | Separate slice | Current marker exists but workflow not implemented |
| SC-04 | Create request command | REQ-CMD-CREATE-001, REQ-LC-001, REQ-IBS-003, REQ-VI-001 | SL-REQ-001 | [L1] | Assigned | Implemented with status conflict |
| SC-04 | Request creation form | input fields, validation feedback, success feedback | SL-REQ-UI-001 | [UI][DEPENDENT] | Separate slice | UI depends on backend/API contract |
| SC-04 / SC-05 | Client sees own request | REQ-READ-001 | SL-REQ-READ-001 | [READ][DEPENDENT] | Separate slice | Read/query projection |
| SC-11 | Attach documents to request | DOC-CMD-ATTACH-001, file/document refs | SL-DOC-001 | [EXTENSION] | Separate slice | Not part of core request creation |
| SC-07B | Approve request | REQ-CMD-APPROVE-001, REQ-LC-002, REQ-IBS-001 | SL-REVIEW-001 | [L1] | Assigned | Domain exists; app slice planned |
| SC-07B | Verify applicant on approval | applicant verification rule | SL-REVIEW-001 | [L1] | Assigned | Application coordinates Request + ApplicantParty |
| SC-07B | Reject request | REQ-CMD-REJECT-001, REQ-LC-003, REQ-IBS-002 | SL-REVIEW-002 | [L1] | Assigned | Domain exists; app slice planned |
| SC-07B | Empty feedback warning | UI warning candidate | SL-REVIEW-UI-002 | [UI][DEPENDENT] | Separate slice | Domain feedback remains optional |
| SC-13D | Agreement proposal after approval | AGR-UCQ-001, AGR-UCQ-002 | SL-AGR-001 | [DEPENDENT][L2] | Separate slice | Approval does not create proposal |

## 7. Existing L1 Foundation Context

Current L1 domain foundation supports:

```text
ClientAccount / Account activation marker
ApplicantParty / IndividualApplicantParty / verification status
ConnectionRequest / RequestStatus / ReviewDecisionRecord
Approve / Reject domain behavior
```

The implemented foundation is useful for slices, but the foundation itself is not a full scenario slice.

Known current implementation conflict:

```text
ConnectionRequest domain target uses InReview.
One existing integration expectation still expects Submitted.
```

## 8. Scenario Sections

### 8.1 SC-01 — Guest Registration

Derived slices:

```text
SL-ACC-001 — Register client account [L1][IMPLEMENTED]
SL-AUTH-EMAIL-001 — Email confirmation / PendingActivation [EXTENSION][L2]
SL-AUTH-RECOVERY-001 — Password recovery [EXTENSION][L2]
SL-AUTH-UI-001 — Login/register/recovery UI [UI][DEPENDENT]
AUTH-GUARD-001 — Active account guard [AUTH/FRAMEWORK][CROSS-CUTTING]
```

SL-ACC-001 is a real slice because it creates account identity independently, has clear success/failure behavior, and does not require applicant/request/review behavior.

Scenario Slice Flow:

```text
F01. Guest provides registration data.
     DATA: email, password, password confirmation.
     Items: ACC-CMD-REGISTER-001.

F02. System validates registration input.
     Rules: invalid input does not create account; duplicate email does not create second account.

F03. System creates client account.
     DATA: email, password hash, role/type = Client.
     Items: ACC-CMD-REGISTER-001, ACC-LC-001.
     Result: client account is created and active in current L1 core.

F04. System persists account.
     Result: persisted account identity exists.

F05. Registration result is available.
     DATA: AccountId, Email.
```

### 8.2 SC-10 — Applicant Data

Derived slices:

```text
SL-APPL-001 — Create individual ApplicantParty [L1][IMPLEMENTED]
SL-APPL-002 — Replace current active ApplicantParty [EXTENSION][L1/L2]
SL-APPL-TYPE-ENT-001 — Entrepreneur applicant type [EXTENSION][L2]
SL-APPL-TYPE-LEGAL-001 — Legal entity applicant type [EXTENSION][L2]
SL-APPL-UI-001 — Applicant data form UI [UI][DEPENDENT]
SL-VER-001 — Applicant verification through external provider [PLUGIN][DEPENDENT]
```

SL-APPL-001 is a real slice because it saves reusable applicant data independently from request creation and can be tested independently.

Scenario Slice Flow:

```text
F01. Client starts applicant data save.
     DATA: current client context.
     Rule: protected client action; active account guard is application/auth concern.

F02. Client provides individual applicant data.
     DATA: full name, applicant contact email, phone number.
     Items: APPL-CMD-SAVE-001, APPL-VI-001.
     Note: applicant contact email may differ from account email.

F03. System validates applicant data shape.
     Rule: IndividualApplicantParty requires individual-specific data shape.
     No-write: invalid applicant data is not saved.

F04. System creates ApplicantParty.
     Result: IndividualApplicantParty is created, Unverified, current active, linked to ClientAccountId.

F05. System persists ApplicantParty.
     Result: ApplicantParty can be used by request creation.

F06. ApplicantParty identity is available.
     DATA: ApplicantPartyId, ClientAccountId.
```

### 8.3 SC-04 — Client Request Creation

Derived slices:

```text
SL-REQ-001 — Create ConnectionRequest from ApplicantParty [L1][IMPLEMENTED]
SL-REQ-UI-001 — Request creation form UI [UI][DEPENDENT]
SL-REQ-READ-001 — My Requests visibility [READ][DEPENDENT]
SL-DOC-001 — Attach documents to request [EXTENSION]
SL-ANON-001 — Anonymous request creation [EXTENSION][L2]
```

SL-REQ-001 is a real slice because it is a separate command behavior with clear success/failure/no-write behavior and independent API/persistence tests.

Scenario Slice Flow:

```text
F01. Client starts request creation.
     DATA: current client context.
     Rule: protected client action; active-account guard is application/auth concern.

F02. Client selects ApplicantParty.
     DATA: ApplicantPartyId, applicant source/contact data.
     Items: REQ-UCQ-001.
     Rule: request uses ApplicantParty without mutating saved applicant data.

F03. System checks ApplicantParty belongs to current client.
     DATA: ApplicantParty.ClientAccountId, current ClientAccountId.
     Rule: client must not create request using another client’s ApplicantParty.

F04. Client provides request details.
     DATA: request details text.
     Items: REQ-CMD-CREATE-001.
     No-write: invalid or empty request details do not create request.

F05. Client provides object address.
     DATA: object address.
     Items: REQ-IBS-003, REQ-VI-001.
     Invariant: request cannot exist without object address.

F06. System creates request.
     DATA: ApplicantPartyId, details, object address.
     Items: REQ-LC-001.
     Result: request status becomes InReview.

F07. Created request result is available.
     DATA: RequestId, ApplicantPartyId, Status.
```

### 8.4 SC-07B — Employee Request Review

Derived slices:

```text
SL-REVIEW-001 — Approve request and verify ApplicantParty [L1][PARTIAL]
SL-REVIEW-002 — Reject request [L1][PARTIAL]
SL-REVIEW-UI-001 — Employee review screen UI [UI][DEPENDENT]
SL-REVIEW-UI-002 — Empty feedback warning [UI][DEPENDENT]
SL-REQ-READ-001 — Client sees review result [READ][DEPENDENT]
SL-NOTIF-001 — Notify client about review result [EXTENSION]
SL-AGR-001 — Start agreement proposal exchange after approved request [DEPENDENT][L2]
```

SL-REVIEW-001 is a real slice because it is a separate employee command, changes request lifecycle, coordinates Request and ApplicantParty, and needs transaction/application orchestration.

Scenario Slice Flow for SL-REVIEW-001:

```text
F01. Employee opens request review context.
     DATA: request id, request status, applicant/request details, employee identity.

F02. System confirms request is reviewable.
     Items: REQ-LC-002, REQ-LC-004, REQ-LC-005.
     Rule: only InReview request can be approved.
     No-write: failed approval does not change request status or review decision.

F03. System checks ApplicantParty can be verified.
     Rule: approval verifies ApplicantParty in current implementation policy.
     No-write: if ApplicantParty cannot be verified, approval should not partially mutate request.

F04. Employee approves request.
     Items: REQ-CMD-APPROVE-001, REQ-IBS-001.
     Result: request becomes Approved and ReviewDecisionRecord is recorded.

F05. System marks ApplicantParty verified.
     Result: ApplicantParty.VerificationStatus becomes Verified.

F06. System persists both changes consistently.

F07. Result is available.
     Note: approval enables agreement proposal creation but does not create proposal automatically.
```

SL-REVIEW-002 is a real slice because rejection is a separate employee command with its own result, feedback behavior and failure/no-write rules.

Scenario Slice Flow for SL-REVIEW-002:

```text
F01. Employee opens request review context.
     DATA: request id, request status, request details, applicant summary, employee identity.

F02. System confirms request is rejectable.
     Items: REQ-LC-003, REQ-LC-004, REQ-LC-006.
     Rule: only InReview request can be rejected.
     No-write: failed rejection does not change request status or review decision.

F03. Employee provides rejection feedback or leaves it empty.
     DATA: rejection feedback text, optional.
     Items: REQ-CMD-REJECT-001, REQ-IBS-002.
     Decision: feedback is optional in domain; empty feedback warning is UI slice.

F04. Employee rejects request.
     Result: request becomes Rejected; ReviewDecisionRecord is recorded; feedback is stored if provided.

F05. System persists rejection.

F06. Result is available.
     Note: client result visibility belongs to read/query slice.
```

## 9. Consolidated Slice Boundary Table

| Slice | Valid because | Depends on | Extends / enables | Separate from |
|---|---|---|---|---|
| SL-ACC-001 | Creates account identity independently | None | Enables protected client slices | Email confirmation, password recovery |
| SL-APPL-001 | Saves applicant data independently | SL-ACC-001 / auth context | Enables request creation | Applicant replacement, verification provider |
| SL-REQ-001 | Creates request independently | SL-APPL-001 | Enables review/read/document slices | UI, My Requests, documents |
| SL-REVIEW-001 | Approves request and verifies applicant | SL-REQ-001 | Enables agreement proposal slice | Agreement proposal creation |
| SL-REVIEW-002 | Rejects request independently | SL-REQ-001 | Enables rejected-result read/UI | Empty-feedback UI warning |
| SL-REQ-READ-001 | Shows own requests independently | SL-REQ-001 | Enables client request tracking | Request creation command |
| SL-EMP-READ-001 | Shows reviewable requests independently | SL-REQ-001 | Enables review UI | Review command |
| SL-REQ-UI-001 | Provides request creation UI independently | SL-REQ-001 API | Enables full-stack request creation experience | Backend command slice |

## 10. Decisions / Resolved Boundary Questions

| Decision | Context | Alternatives | Chosen direction | Reason | Consequence | Related slices |
|---|---|---|---|---|---|---|
| Active account guard is not standalone business slice | Protected actions | Business slice vs auth/framework concern | Auth/framework/application guard | It is applied inside protected slices | Tested through protected slice tests | AUTH-GUARD-001, SL-APPL-001, SL-REQ-001 |
| Request creation and My Requests are separate slices | SC-04 / SC-05 | One full scenario slice vs command/read split | Split | Command and read visibility are independently testable | My Requests becomes read/query slice | SL-REQ-001, SL-REQ-READ-001 |
| Request creation UI is a dependent UI slice | SC-04 | Include UI in backend slice vs separate UI slice | Separate dependent UI slice | Backend/API behavior is testable without UI | UI gets own slice and tests | SL-REQ-001, SL-REQ-UI-001 |
| Documents are extension slice | SC-04 / SC-11 | Include in request creation vs extension | Extension | Request creation is valid without documents in L1 | Document upload planned separately | SL-REQ-001, SL-DOC-001 |
| Approval does not create agreement proposal | SC-07B / SC-13D | Include agreement creation in approval vs separate slice | Separate dependent slice | Approval enables but does not create proposal | Agreement planning remains separate | SL-REVIEW-001, SL-AGR-001 |
| Rejection feedback warning is UI slice | SC-07B | Hard domain invariant vs UI warning | UI warning | Feedback optional in domain | UI must warn/confirm later | SL-REVIEW-002, SL-REVIEW-UI-002 |

## 11. Open Boundary Questions

| Question | Related slice | Category | Blocks boundary? | Candidate answer | ADR? |
|---|---|---|---|---|---|
| Should SL-REQ-001 require current active ApplicantParty? | SL-REQ-001 | Boundary / application rule | No | Keep as app/service decision in per-slice file | Maybe |
| Should applicant replacement be L1 or L2? | SL-APPL-002 | Package split | No | L1/L2 depending on UI need | Maybe |
| Should approve+verify be implemented before read slices? | SL-REVIEW-001 | Delivery order | No | likely yes, after request creation conflict fixed | Maybe |
| Should My Requests be L1 read slice before review slices? | SL-REQ-READ-001 | Delivery order | No | depends on demo needs | No |
| Should status conflict be fixed before new slices? | SL-REQ-001 | Integration/API contract | Yes | update Submitted to InReview | Yes |

## 12. ADR Candidates

| ADR candidate | Decision needed | Related slices | Urgency |
|---|---|---|---|
| Request initial status: InReview vs Submitted | Align domain/API/integration contract | SL-REQ-001 | High |
| Command/read slice split | Use read projection instead of write model duplication | SL-REQ-001, SL-REQ-READ-001 | Medium |
| Approval verifies ApplicantParty via application service | Multi-aggregate transaction boundary | SL-REVIEW-001 | High before review implementation |
| Active account guard placement | Auth/framework/application boundary | Protected slices | Medium |
| ApplicantParty version/current-active policy | Replacement slice boundary | SL-APPL-001, SL-APPL-002 | Medium |
| Agreement proposal as dependent L2 slice | Approval enables but does not create proposal | SL-REVIEW-001, SL-AGR-001 | Medium |

## 13. Next Per-Slice Files

Current per-slice files:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REVIEW-001-approve-request-and-verify-applicant.md
planning/slices/SL-REVIEW-002-reject-request.md
```

Recommended next implementation focus:

```text
1. Resolve SL-REQ-001 Submitted/InReview conflict.
2. Then choose either:
   - SL-REVIEW-001 approve + verify applicant;
   - or SL-REQ-READ-001 My Requests visibility, depending on demo needs.
```
