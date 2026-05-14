# Scenario Behavior Coverage Baseline

Status: first coverage-baseline draft  
Historical filename: `planning/tables/pre-domain-variants-input.md`

## 1. Purpose

This file is the scenario behavior coverage baseline and pre-domain discovery control artifact.

It is created from scenario text specs, scenario DATA files and validation-related files.

It exists before domain drafts.

It does not define domain classes, aggregates or final method names.

It collects scenario-derived behavior items with stable IDs so each domain draft can explain how those items are covered.

A coverage item is not “something the domain class must implement.”

A coverage item is:

```text
something the system must explain/cover.
```

A domain draft may cover a baseline item with:

```text
- domain class / aggregate;
- value object;
- domain service;
- application/use-case orchestration;
- read/query/access placement;
- DB constraint;
- infrastructure/integration;
- future/deferred decision.
```

## 2. Source Set

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/scenario-domain-validation-principles.md
```

Optional context:

```text
planning/diagrams/scenario-diagram-consistency-report.md
planning/scenario-specification-principles.md
```

## 3. Item Categories And Statuses

### 3.1 Item categories

These categories describe scenario behavior item types, not layers.

```text
CMD  — command behavior
LC   — lifecycle / state / condition behavior
IBS  — impossible business state candidate
VI   — value integrity / anti-primitive-obsession item
UCQ  — use-case coordination item
READ — read/access/listing behavior
INT  — integration/side-effect expectation
FUT  — future/deferred behavior
NW   — no-write / failure preservation behavior, if it deserves separate tracking
```

### 3.2 Coverage statuses for domain drafts

```text
Missing
Partial
Covered
Resolved outside current domain model
Deferred
Question
```

Meaning:

```text
Missing
  Current draft does not answer this item.

Partial
  Current draft has a possible answer but leaves important behavior, failure guarantee, state, data or placement unclear.

Covered
  Current draft gives a clear answer and names the model element/method/value/object placement.

Resolved outside current domain model
  Current draft explains that the item is not owned by current domain classes and records a likely later layer/placement.

Deferred
  Item belongs to future implementation or intentionally postponed scenario.

Question
  Item remains unclear and needs scenario/domain clarification.
```

## 4. Coverage Items Registry

This registry is only a short index.

Detailed item descriptions live in later sections.

| ID | Category | Short name | Source | Detail section | Initial status |
|---|---|---|---|---|---|
| ACC-CMD-REGISTER-001 | CMD | Register account | SC-01 | 5.1 | Needs draft answer |
| ACC-CMD-RECOVERY-001 | CMD | Request password recovery | SC-03A | 5.2 | Needs draft answer |
| ACC-CMD-RESET-001 | CMD | Set new password | SC-03B | 5.3 | Needs draft answer |
| APPL-CMD-SAVE-001 | CMD | Save applicant data | SC-10 | 5.4 | Needs draft answer |
| REQ-CMD-CREATE-001 | CMD | Create request | SC-04 | 5.5 | Needs draft answer |
| REQ-CMD-APPROVE-001 | CMD | Approve request | SC-07B | 5.6 | Needs draft answer |
| REQ-CMD-REJECT-001 | CMD | Reject request | SC-07B | 5.7 | Needs draft answer |
| DOC-CMD-ATTACH-001 | CMD | Attach request document | SC-11 | 5.8 | Needs draft answer |
| AGR-CMD-EMP-SEND-001 | CMD | Employee sends first agreement proposal | SC-13D | 5.9 | Needs draft answer |
| AGR-CMD-CLIENT-ACCEPT-001 | CMD | Client accepts proposal | SC-13B | 5.10 | Needs draft answer |
| AGR-CMD-CLIENT-SEND-001 | CMD | Client sends own proposal version | SC-13B | 5.11 | Needs draft answer |
| AGR-CMD-EMP-NEW-001 | CMD | Employee sends new version after client proposal | SC-13D | 5.12 | Needs draft answer |
| VER-CMD-START-001 | CMD | Start request-context verification | SC-14 | 5.13 | Deferred / future |
| ANON-CMD-SUBMIT-001 | CMD | Submit anonymous request/contact | SC-17 | 5.14 | Deferred / question |
| REQ-LC-001 | LC | Request creation creates InReview | SC-04 | 6.1 | Needs draft answer |
| REQ-LC-002 | LC | InReview request can be approved | SC-07B | 6.1 | Needs draft answer |
| REQ-LC-003 | LC | InReview request can be rejected | SC-07B | 6.1 | Needs draft answer |
| REQ-LC-004 | LC | Approved request cannot be reviewed again | SC-07A / SC-07B | 6.1 | Needs draft answer |
| REQ-LC-005 | LC | Rejected request cannot be approved in core | SC-07B | 6.1 | Needs draft answer |
| REQ-LC-006 | LC | Rejected request cannot be rejected again in core | SC-07B | 6.1 | Needs draft answer |
| AGR-LC-001 | LC | Employee first proposal creates awaiting state | SC-13D | 6.2 | Needs draft answer |
| AGR-LC-002 | LC | Awaiting proposal can be accepted | SC-13B | 6.2 | Needs draft answer |
| AGR-LC-003 | LC | Awaiting proposal can receive client version | SC-13B | 6.2 | Needs draft answer |
| AGR-LC-004 | LC | Accepted proposal has no response actions in core | SC-13B | 6.2 | Needs draft answer |
| AGR-LC-005 | LC | Rejected proposal has no response actions in core | SC-13B | 6.2 | Needs draft answer |
| AGR-LC-006 | LC | Client own version can be sent only once in core | SC-13B | 6.3 | Needs draft answer |
| AGR-LC-007 | LC | Employee new version rejects previous client version | SC-13D | 6.2 | Needs draft answer |
| REQ-IBS-001 | IBS | Approved request has review decision | SC-07B | 7.1 | Needs draft answer |
| REQ-IBS-002 | IBS | Rejected request has feedback if required | SC-07B / SC-05 | 7.1 | Question |
| REQ-IBS-003 | IBS | Request has object address | SC-04 | 7.1 | Needs draft answer |
| AGR-IBS-001 | IBS | Proposal has sender | SC-13A..SC-13D | 7.2 | Needs draft answer |
| AGR-IBS-002 | IBS | Proposal has document/file | SC-13B / SC-13D | 7.2 | Needs draft answer |
| AGR-IBS-003 | IBS | Client does not start exchange | SC-13B / SC-13D | 7.2 | Needs draft answer |
| REQ-VI-001 | VI | Object address value integrity | SC-04-DATA | 8.1 | Needs draft answer |
| APPL-VI-001 | VI | Applicant data by applicant type | SC-10-DATA | 8.2 | Needs draft answer |
| AGR-VI-001 | VI | Agreement document reference | SC-13B / SC-13D-DATA | 8.3 | Needs draft answer |
| AGR-VI-002 | VI | Proposal text details/comment | SC-13B / SC-13D-DATA | 8.3 | Needs draft answer |
| REQ-UCQ-001 | UCQ | Request uses saved ApplicantData without mutating it | SC-04 / SC-10 | 9.1 | Needs draft answer |
| AGR-UCQ-001 | UCQ | Proposal creation requires Approved request | SC-13D / SC-07B | 9.2 | Needs draft answer |
| AGR-UCQ-002 | UCQ | Approval enables but does not create proposal | SC-07B / SC-13D | 9.2 | Needs draft answer |
| VER-UCQ-001 | UCQ | Verification is request-context-only | SC-10 / SC-14 | 9.3 | Deferred / future |
| REQ-READ-001 | READ | Client sees only own requests | SC-05 / SC-15 | 10.1 | Needs draft answer |
| AGR-READ-001 | READ | Client sees only own agreements/proposals | SC-13A / SC-13B / SC-15 | 10.1 | Needs draft answer |
| EMP-READ-001 | READ | Employee request dashboard visibility | SC-06 / SC-07A | 10.1 | Needs draft answer |
| AUTH-INT-001 | INT | Password recovery email side effect | SC-03A | 10.2 | Needs draft answer |
| FILE-INT-001 | INT | File/blob storage is infrastructure | SC-11 / SC-13B / SC-13D | 10.2 | Needs draft answer |

## 5. Command Behavior Cards

Command card describes the general command behavior.

State/condition-specific applicability belongs to section 6.

### 5.1 ACC-CMD-REGISTER-001 — Register account

Source:

```text
SC-01 Guest Registration
```

Required behavior / guarantee:

```text
Accepted registration creates an account / registered identity from email, password and password confirmation.
```

Failure / no-write guarantee:

```text
Invalid registration input does not create account.
```

Draft must explain:

```text
- whether this remains outside main business domain as auth/account boundary;
- how account identity becomes available to request/applicant/agreement behavior;
- how email/password value integrity is represented or intentionally delegated.
```

### 5.2 ACC-CMD-RECOVERY-001 — Request password recovery

Source:

```text
SC-03A Password Recovery Request
```

Required behavior / guarantee:

```text
Password recovery request accepts an email-like value and behaves according to account recovery rules without revealing account existence.
```

Failure / no-write guarantee:

```text
If email is not registered, no recovery context is created and account existence is not revealed.
```

Draft must explain:

```text
- whether recovery context is part of auth boundary, business domain, or excluded from current domain draft;
- what state/condition controls ability to set a new password.
```

### 5.3 ACC-CMD-RESET-001 — Set new password

Source:

```text
SC-03B Account Owner Verified / Password Reset Choice
```

Required behavior / guarantee:

```text
Verified account owner can set a new password when recovery context is valid, unless user chooses to log in with existing password instead.
```

Failure / no-write guarantee:

```text
Invalid, expired or used recovery context does not update password.
```

Draft must explain:

```text
- whether recovery token/context lifecycle is modeled in current domain draft;
- whether password/account mechanics are treated as auth/framework boundary.
```

### 5.4 APPL-CMD-SAVE-001 — Save applicant data

Source:

```text
SC-10 Applicant Data
```

Required behavior / guarantee:

```text
Accepted applicant data is saved as reusable applicant data owned by client context.
```

Failure / no-write guarantee:

```text
Invalid applicant data is not saved.
Standalone applicant data save/edit must not start verification.
```

Draft must explain:

```text
- how saved ApplicantData-like concept is represented;
- how applicant type affects required data shape;
- how standalone saved applicant data is separated from request-local applicant data.
```

### 5.5 REQ-CMD-CREATE-001 — Create request

Source:

```text
SC-04 Client Request Creation
```

Required behavior / guarantee:

```text
A valid request creation creates a persisted request with status InReview, object address, request details and request-local applicant data.
```

Failure / no-write guarantee:

```text
If request data is invalid, no request is created.
If saved ApplicantData was used as source, it is not mutated by failed or edited request-local data.
```

Draft must explain:

```text
- how request creation is represented;
- how initial InReview status is assigned;
- how request-local applicant data is represented;
- how copied applicant data is protected from implicit mutation.
```

### 5.6 REQ-CMD-APPROVE-001 — Approve request

Source:

```text
SC-07B Employee Request Review
```

Required behavior / guarantee:

```text
Successful employee approval records a positive review result and makes the request Approved.
```

Failure / no-write guarantee:

```text
Failed approval does not change request status and does not record a new review decision.
```

Related items:

```text
REQ-LC-002
REQ-LC-004
REQ-LC-005
REQ-IBS-001
AGR-UCQ-002
```

Draft must explain:

```text
- how approval command relates to request lifecycle;
- how review decision and status change stay consistent;
- why approval does not automatically create agreement proposal.
```

### 5.7 REQ-CMD-REJECT-001 — Reject request

Source:

```text
SC-07B Employee Request Review
```

Required behavior / guarantee:

```text
Successful employee rejection records a negative review result and makes the request Rejected.
```

Failure / no-write guarantee:

```text
Failed rejection does not change request status and does not record a new review decision or accepted rejection feedback.
```

Related items:

```text
REQ-LC-003
REQ-LC-004
REQ-LC-006
REQ-IBS-002
```

Draft must explain:

```text
- how rejection command relates to request lifecycle;
- whether rejection feedback is mandatory;
- how rejection feedback is kept consistent with Rejected status.
```

### 5.8 DOC-CMD-ATTACH-001 — Attach request document

Source:

```text
SC-11 Request Documents
```

Required behavior / guarantee:

```text
Accepted document/file reference becomes attached to request context.
```

Failure / no-write guarantee:

```text
Invalid/rejected document is not attached.
Physical file/blob storage failure does not create accepted domain attachment state.
```

Draft must explain:

```text
- whether request document attachment is part of current request model;
- what domain state is a document reference/metadata versus infrastructure file/blob content.
```

### 5.9 AGR-CMD-EMP-SEND-001 — Employee sends first agreement proposal

Source:

```text
SC-13D Employee Agreement Proposal Create / Send Version
```

Required behavior / guarantee:

```text
Employee can start agreement proposal exchange only for an Approved request by sending an agreement document/file with text details/comment.
```

Failure / no-write guarantee:

```text
If request is not Approved or proposal input is invalid, no agreement proposal is created and request status remains unchanged.
```

Related items:

```text
AGR-LC-001
AGR-UCQ-001
AGR-IBS-001
AGR-IBS-002
AGR-VI-001
AGR-VI-002
```

Draft must explain:

```text
- how first employee proposal is represented;
- how proposal is related to Approved request;
- how proposal status AwaitingClientConfirmation is established.
```

### 5.10 AGR-CMD-CLIENT-ACCEPT-001 — Client accepts proposal

Source:

```text
SC-13B Agreement Proposal Details / Response
```

Required behavior / guarantee:

```text
Client can accept an employee-sent proposal that is awaiting client confirmation and belongs to the client context.
```

Failure / no-write guarantee:

```text
Wrong status, wrong ownership/context or inactive proposal does not become Accepted.
```

Related items:

```text
AGR-LC-002
AGR-LC-004
AGR-LC-005
```

Draft must explain:

```text
- how proposal acceptability is represented;
- how accepted state affects future actions in core.
```

### 5.11 AGR-CMD-CLIENT-SEND-001 — Client sends own proposal version

Source:

```text
SC-13B Agreement Proposal Details / Response
```

Required behavior / guarantee:

```text
Client can send one own agreement version in response to an employee-sent proposal awaiting confirmation.
```

Failure / no-write guarantee:

```text
No client proposal is created if proposal is not awaiting client confirmation, not employee-sent, not in client context, input is invalid, or client already sent own version in core.
```

Related items:

```text
AGR-LC-003
AGR-LC-006
AGR-IBS-003
AGR-VI-001
AGR-VI-002
```

Draft must explain:

```text
- how client response is represented;
- how one-own-version core limit is enforced;
- how client response remains tied to employee proposal/exchange.
```

### 5.12 AGR-CMD-EMP-NEW-001 — Employee sends new version after client proposal

Source:

```text
SC-13D Employee Agreement Proposal Create / Send Version
```

Required behavior / guarantee:

```text
Employee can respond to a client-sent proposal version by sending a new employee proposal version; the previous client-sent proposal becomes Rejected in core.
```

Failure / no-write guarantee:

```text
If previous proposal is not client-sent/SentByClient or new input is invalid, no new proposal is created and previous proposal status remains unchanged.
```

Related items:

```text
AGR-LC-007
AGR-IBS-001
AGR-IBS-002
AGR-VI-001
AGR-VI-002
```

Draft must explain:

```text
- how proposal version sequence/replacement is represented;
- how previous client proposal becomes Rejected when employee sends a new version.
```

### 5.13 VER-CMD-START-001 — Start request-context verification

Source:

```text
SC-14 Client Data Verification
```

Required behavior / guarantee:

```text
Verification can be started only in request context and only by employee/future review action.
```

Failure / no-write guarantee:

```text
Verification cannot start without request context.
Standalone ApplicantData edit/save does not create verification state.
```

Draft must explain:

```text
- whether verification is deferred;
- if modeled, where request-context verification result belongs.
```

### 5.14 ANON-CMD-SUBMIT-001 — Submit anonymous request/contact

Source:

```text
SC-17 Anonymous Request
```

Required behavior / guarantee:

```text
Anonymous submission is recorded only if required request/contact data and follow-up contact data are accepted.
```

Failure / no-write guarantee:

```text
Invalid contact/request data is not recorded.
```

Draft must explain:

```text
- whether this becomes AnonymousRequest, ContactRequest or DraftRequest;
- whether it is deferred from current core.
```

## 6. Scenario State / Condition Matrices

This section contains scenario-level state/condition matrices.

It does not define class state machines.

Domain drafts must answer these items with concrete model state/method decisions.

Use this section when command availability depends on:

```text
- status/state/stage/phase;
- exists / does not exist;
- null / not null;
- used / unused;
- valid / expired;
- count = 0 / count >= 1 / limit reached;
- already done / not done;
- final / not final.
```

### 6.1 Request status lifecycle

Lifecycle concept:

```text
Request status
```

Known scenario states:

```text
InReview
Approved
Rejected
```

| ID | Current condition | Scenario action | Result condition | Allowed? | Required behavior / guarantee | Failure / no-write guarantee | Draft must explain |
|---|---|---|---|---|---|---|
| REQ-LC-001 | no request exists | Create request | request exists, status InReview | Yes | Successful request creation creates request with status InReview. | Invalid request creates no request. | What model creates request and initial status. |
| REQ-LC-002 | request status InReview | Approve request | status Approved | Yes | Approval changes status to Approved and records review decision. | If approval fails, status and decision remain unchanged. | How approval transition and review decision are kept consistent. |
| REQ-LC-003 | request status InReview | Reject request | status Rejected | Yes | Rejection changes status to Rejected and records rejection decision/feedback if required. | If rejection fails, status and decision/feedback remain unchanged. | How rejection transition and feedback are kept consistent. |
| REQ-LC-004 | request status Approved | start/repeat review or approve/reject again | unchanged | No | Approved request is final for review in core. | Status unchanged; no new decision. | How processed request review is prevented. |
| REQ-LC-005 | request status Rejected | Approve request | unchanged | No | Rejected request cannot be approved in core. | Status unchanged; no new decision. | How invalid approval is rejected. |
| REQ-LC-006 | request status Rejected | Reject request again | unchanged | No | Rejected request cannot be rejected again in core. | Status unchanged; no new decision. | How repeated rejection is rejected. |

### 6.2 Agreement proposal status lifecycle

Lifecycle concept:

```text
Agreement proposal status
```

Known scenario states:

```text
AwaitingClientConfirmation
SentByClient
Accepted
Rejected
```

| ID | Current condition | Scenario action | Result condition | Allowed? | Required behavior / guarantee | Failure / no-write guarantee | Draft must explain |
|---|---|---|---|---|---|---|
| AGR-LC-001 | no exchange/proposal for approved request | employee sends first proposal | employee proposal AwaitingClientConfirmation | Yes | First employee proposal starts exchange and awaits client confirmation. | No proposal created if request is not Approved or input invalid. | How proposal exchange starts and how request condition is checked. |
| AGR-LC-002 | employee proposal AwaitingClientConfirmation | client accepts | proposal Accepted | Yes | Client can accept active employee proposal. | Wrong status/context remains unchanged. | How acceptability is checked. |
| AGR-LC-003 | employee proposal AwaitingClientConfirmation | client sends own version | client proposal SentByClient | Yes | Client can send own version in response. | Wrong status/context/input creates no client version. | How client response is represented. |
| AGR-LC-004 | proposal Accepted | client accepts/sends own version | unchanged | No | Accepted proposal has no response actions in core. | Status unchanged; no new proposal. | How final/inactive proposal actions are prevented. |
| AGR-LC-005 | proposal Rejected | client accepts/sends own version | unchanged | No | Rejected proposal has no response actions in core. | Status unchanged; no new proposal. | How inactive proposal actions are prevented. |
| AGR-LC-007 | client proposal SentByClient | employee sends new version | previous client proposal Rejected + new employee proposal AwaitingClientConfirmation | Yes | Employee new version rejects/replaces previous client proposal. | Invalid previous state/input leaves previous proposal unchanged and no new proposal. | How version replacement/status update is represented. |

### 6.3 Client own proposal count/limit condition

Condition concept:

```text
Client own proposal response count in core
```

| ID | Current condition | Scenario action | Result condition | Allowed? | Required behavior / guarantee | Failure / no-write guarantee | Draft must explain |
|---|---|---|---|---|---|---|
| AGR-LC-006 | client own version count = 0 for active employee proposal | client sends own version | client own version count = 1; proposal SentByClient | Yes | Client can send one own version in core. | Invalid input creates no own version. | How one-response limit is checked. |
| AGR-LC-006B | client own version count >= 1 | client sends own version again | unchanged | No | Client cannot send second own version in core. | No new proposal created; existing proposals unchanged. | How duplicate client response is rejected. |

## 7. Impossible Business State Candidates

Impossible business states are not the same as state transitions.

State/condition matrix asks:

```text
Can command X happen under condition/state Y?
```

Impossible state asks:

```text
What business-invalid combination of data/state must never exist?
```

### 7.1 Request impossible states

| ID | Impossible business state | Source | Why business-invalid | Failure/no-write relation | Draft must explain |
|---|---|---|---|---|---|
| REQ-IBS-001 | Approved request without recorded review decision. | SC-07B | Approval is employee review result and must be traceable/visible as decision. | Failed approval must not write status or decision. | Where review decision belongs and how it is consistent with Approved status. |
| REQ-IBS-002 | Rejected request without rejection feedback if feedback is required. | SC-07B / SC-05 | Client needs rejection explanation if rejection feedback is accepted as core requirement. | Failed rejection must not write status/feedback. | Whether feedback is mandatory and where it belongs. |
| REQ-IBS-003 | Request without object address. | SC-04 | Request must target an object address. | Invalid address prevents request creation. | How object address is represented and required. |

### 7.2 Agreement proposal impossible states

| ID | Impossible business state | Source | Why business-invalid | Failure/no-write relation | Draft must explain |
|---|---|---|---|---|---|
| AGR-IBS-001 | Agreement proposal without sender. | SC-13A..SC-13D | Proposal flow depends on who sent version: employee or client. | Invalid proposal input creates no proposal. | How sender/source is represented. |
| AGR-IBS-002 | Agreement proposal without attached document/file. | SC-13B / SC-13D | Core proposal is a concrete agreement document/version. | Missing document creates no proposal. | How proposal document/reference is represented. |
| AGR-IBS-003 | Client-started agreement exchange without employee proposal. | SC-13B / SC-13D | Core exchange starts only by employee. | Client cannot create first proposal. | How exchange start is constrained. |

## 8. Value Integrity / Anti-Primitive-Obsession Items

Do not use this section for state transition validation.

This section is about data/value shape and domain value pressure.

### 8.1 Request value integrity

| ID | Value/data | Required behavior / guarantee | Source | Draft must explain |
|---|---|---|---|---|
| REQ-VI-001 | Object address | Request must not be accepted with missing/structurally invalid object address. | SC-04-DATA | Whether an ObjectAddress-like value exists and what rules it owns. |

### 8.2 Applicant value integrity

| ID | Value/data | Required behavior / guarantee | Source | Draft must explain |
|---|---|---|---|---|
| APPL-VI-001 | Applicant data by applicant type | Applicant data must match required data shape for selected applicant type. | SC-10-DATA / SC-04-DATA | How applicant type affects required data, and which value concepts prevent primitive obsession. |

Candidate value pressure from DATA:

```text
EmailAddress
PhoneNumber
FullName
PassportData
SNILS
INN
OGRN
OGRNIP
ObjectAddress
ApplicantType-specific data shape
```

### 8.3 Agreement proposal value integrity

| ID | Value/data | Required behavior / guarantee | Source | Draft must explain |
|---|---|---|---|---|
| AGR-VI-001 | Agreement document/file reference | Proposal submission requires accepted agreement document/file reference. | SC-13B / SC-13D-DATA | How document/reference is represented and how physical storage is separated. |
| AGR-VI-002 | Proposal text details/comment | Proposal submission includes text details/comment if required by core scenario. | SC-13B / SC-13D-DATA | Whether comment/details are required value, optional value or future extension. |

## 9. Use-Case Coordination Items

Use-case coordination item means:

```text
scenario-derived behavior where one user action depends on consistency between several scenario concepts, lifecycles or stored data areas, and a future draft must explain how the system coordinates them.
```

It does not mean cross-aggregate yet.

Aggregates do not exist before domain drafts.

### 9.1 Request / ApplicantData coordination

| ID | Required behavior / guarantee | Concepts involved | Failure/no-write guarantee | Draft must explain |
|---|---|---|---|---|
| REQ-UCQ-001 | Request creation may use saved ApplicantData as source, but request-local edits must not implicitly mutate saved ApplicantData. | saved ApplicantData; request-local applicant data; request creation | Failed/edited request-local data leaves saved ApplicantData unchanged. | Copy/snapshot/reference decision and where mutation boundary is enforced. |

### 9.2 Request / AgreementProposal coordination

| ID | Required behavior / guarantee | Concepts involved | Failure/no-write guarantee | Draft must explain |
|---|---|---|---|---|
| AGR-UCQ-001 | Employee can start agreement proposal exchange only for Approved request. | request lifecycle; agreement proposal lifecycle | Proposal is not created for non-Approved request; request status unchanged. | How Approved request condition is checked and how proposal relates to request. |
| AGR-UCQ-002 | Request approval enables agreement proposal creation but must not automatically create proposal. | request review lifecycle; agreement proposal lifecycle | Approval failure writes neither Approved status nor proposal. | How proposal creation remains a separate employee action after approval. |

### 9.3 ApplicantData / Verification coordination

| ID | Required behavior / guarantee | Concepts involved | Failure/no-write guarantee | Draft must explain |
|---|---|---|---|---|
| VER-UCQ-001 | Verification is request-context-only and is not triggered by standalone ApplicantData save/edit. | saved ApplicantData; request context; verification | Standalone applicant data save/edit creates no verification state. | Whether verification is deferred, external, or request-processing-only. |

## 10. Read / Access / Integration / Future Items

These are still scenario behavior items.

Do not mark them as outside-domain before a draft answers placement.

### 10.1 Read / access items

| ID | Required behavior / guarantee | Source | Draft must explain |
|---|---|---|---|
| REQ-READ-001 | Client can see only own requests and own request details. | SC-05 / SC-15 | Whether current draft stores enough ownership/reference data and where enforcement should be placed later. |
| AGR-READ-001 | Client can see only own agreement proposals/details. | SC-13A / SC-13B / SC-15 | Whether current draft stores enough ownership/reference data and where enforcement should be placed later. |
| EMP-READ-001 | Employee dashboard/details show employee-accessible request data and review actions only where allowed. | SC-06 / SC-07A / SC-07B | Whether current draft stores enough state/reference data and where employee access/action availability belongs. |

### 10.2 Integration / infrastructure items

| ID | Required behavior / guarantee | Source | Draft must explain |
|---|---|---|---|
| AUTH-INT-001 | Password recovery email is sent only as side effect of acceptable recovery flow; account existence is not revealed. | SC-03A | Whether recovery/email delivery is outside current business domain and what state/reference is needed. |
| FILE-INT-001 | Physical file/blob content is not the same as accepted domain document/reference state. | SC-11 / SC-13B / SC-13D | How current draft separates domain document/reference metadata from infrastructure storage. |

### 10.3 Future / deferred items

| ID | Required behavior / guarantee | Source | Draft must explain |
|---|---|---|---|
| VER-CMD-START-001 | Request-context verification may exist in future implementation. | SC-14 | Whether deferred from current draft. |
| ANON-CMD-SUBMIT-001 | Anonymous request/contact submission may exist in future implementation. | SC-17 | Whether deferred from current draft. |

## 11. Draft Start Check

Can domain draft 1 start?

```text
Yes.
```

Blocking questions before draft 1:

```text
None.
```

Questions/items draft 1 should answer at least partially:

```text
REQ-CMD-CREATE-001
REQ-CMD-APPROVE-001
REQ-CMD-REJECT-001
REQ-LC-001
REQ-LC-002
REQ-LC-003
REQ-LC-004
REQ-IBS-001
REQ-IBS-003
REQ-VI-001
APPL-CMD-SAVE-001
APPL-VI-001
REQ-UCQ-001
AGR-CMD-EMP-SEND-001
AGR-CMD-CLIENT-ACCEPT-001
AGR-CMD-CLIENT-SEND-001
AGR-CMD-EMP-NEW-001
AGR-LC-001
AGR-LC-002
AGR-LC-003
AGR-LC-006
AGR-LC-007
AGR-UCQ-001
AGR-UCQ-002
```

Items that may remain deferred/question in draft 1:

```text
VER-CMD-START-001
VER-UCQ-001
ANON-CMD-SUBMIT-001
REQ-IBS-002 if rejection feedback mandatory status is still questioned
```

## 12. How Domain Drafts Use This File

Each domain draft should include:

```text
Coverage Against Scenario Behavior Baseline
```

Coverage format:

```text
Item ID | Status | Draft answer / placement | Covered by | Gap / next action
```

Example:

```text
REQ-LC-002 | Covered | Request candidate owns approval transition. | Request.Approve(...) | -
REQ-READ-001 | Resolved outside current domain model | Ownership reference exists; enforcement belongs to query/application access. | Cross-layer placement note | Verify during slice planning.
REQ-VI-001 | Partial | ObjectAddress candidate exists. | ObjectAddress.Create(...) | Exact fields still need final check.
```

Drafts must not copy the full details of this baseline.

They should reference item IDs and explain current coverage.
