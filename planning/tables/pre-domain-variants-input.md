# Pre-Domain Variants Input

Status: first draft  
Target path: `planning/tables/pre-domain-variants-input.md`

## 1. Purpose

This file is the single bridge after scenario text specs, DATA files and validation-related files are ready.

It is created before generating domain model variants.

It collects:

```text
- invariants;
- persisted/write state;
- state-changing actions;
- state-dependent allowed/forbidden actions;
- no-write / failure behavior;
- notes that help discover future domain methods.
```

This file helps generate domain variants one by one.

It does not choose the final domain model.

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
```

## 3. What This File Is Not

This file is not:

```text
- value object catalog;
- aggregate design;
- final domain model;
- database schema;
- endpoint/controller map;
- UI page map;
- implementation slice map.
```

Value object candidates should be discovered directly when generating each domain variant from:

```text
scenario specs
DATA files
validation-related files
this pre-domain input
```

## 4. How To Use This File

For each domain variant, use the pattern:

```text
scenario evidence
-> data involved
-> persisted/write state
-> invariant before write
-> state change
-> invariant after write
-> failure/no-write behavior
-> possible method pressure
```

The goal is to find domain model pieces such as:

```text
- objects that own persisted state;
- methods that must guard state transitions;
- statuses that are written and later checked;
- rules that must remain true even if UI validation is bypassed.
```

## 5. Core Persisted / Write State Summary

| State / data written | Source scenarios | Meaning |
|---|---|---|
| Account / registered identity | SC-01 | Account exists after accepted registration. |
| Recovery context / reset state | SC-03A / SC-03B | Recovery link/context can allow password reset. |
| Saved ApplicantData | SC-10 | Reusable applicant data owned by client. |
| Request-local applicant data | SC-04 | Applicant data captured for one request, copied or entered inline. |
| Request.status | SC-04 / SC-07B | Core request lifecycle state: InReview / Approved / Rejected. |
| Review decision / feedback | SC-07B / SC-05 | Employee review result visible to client. |
| Request document attachment | SC-11 | Accepted document becomes attached to request. |
| Agreement proposal exchange/proposals | SC-13A..SC-13D | Agreement document/version exchange after approved request. |
| AgreementProposal.status | SC-13A..SC-13D | Core proposal state: AwaitingClientConfirmation / SentByClient / Accepted / Rejected. |
| Verification result | SC-14 | Future request-context verification result. |
| Anonymous request/contact request | SC-17 | Future anonymous submission with follow-up contact. |

## 6. Account / Registration Writes

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-01 | Register guest | Account / registered identity | Registration input accepted server-side/domain-side. | Account exists. | Invalid input does not create account. | `Register(...)` / account creation factory. |
| SC-01 | Registration with invalid data | No account write | Input invalid. | No state change. | Validation errors returned. | Account creation returns result/errors. |

Notes:

```text
- Registration is account/auth boundary, not ApplicantData creation in core.
- Future registration may collect applicant-related data, but that is not core.
```

## 7. Password Recovery / Credential Writes

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-03A | Request password recovery for registered email | Recovery context / reset capability | Email is accepted and belongs to an account. | Recovery email/context is created/sent. | If email is not registered, visible response stays neutral. | `RequestRecovery(...)`. |
| SC-03A | Request password recovery for unregistered email | No recovery context write | Email is accepted but not registered. | No recovery context created. | Account existence is not revealed. | Recovery policy result mapping. |
| SC-03B | Set new password | Credentials/password state | Recovery context is valid and new password accepted. | Password updated. | Invalid/expired recovery context does not update password. | `ResetPassword(...)`. |
| SC-03B | Login with old/current password | No password write | User chooses login branch. | Credentials unchanged. | N/A | Off-page auth flow, not domain write. |

Notes:

```text
- Mostly auth/framework boundary.
- Still important because recovery context state controls whether password update is allowed.
```

## 8. ApplicantData Writes

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-10 | Save physical person applicant data | Saved ApplicantData | Applicant type and physical-person fields accepted server-side/domain-side. | Saved reusable applicant data exists. | Invalid applicant data is not saved. | `CreatePhysical(...)` / `SaveApplicantData(...)`. |
| SC-10 | Save entrepreneur applicant data | Saved ApplicantData | Applicant type and entrepreneur fields accepted. | Saved reusable applicant data exists. | Invalid applicant data is not saved. | `CreateEntrepreneur(...)`. |
| SC-10 | Save legal entity applicant data | Saved ApplicantData | Applicant type and legal-entity fields accepted. | Saved reusable applicant data exists. | Invalid applicant data is not saved. | `CreateLegalEntity(...)`. |
| SC-10 | Edit standalone applicant data | Saved ApplicantData | Edited applicant data accepted. | Saved applicant data updated. | Invalid edit is not saved. | future `Update...(...)`. |
| SC-10 / SC-14 | Save ApplicantData | No verification state write | ApplicantData is saved outside request context. | ApplicantData saved only. | Verification is not started. | no standalone verification method. |

Notes:

```text
- Saved ApplicantData is reusable.
- Standalone ApplicantData save/edit must not trigger client data verification.
- ApplicantData ownership belongs to client context.
```

## 9. Request Creation Writes

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-04 | Create request with valid data | Request + Request.status + request-local applicant data | Request data, object address and request-local applicant data accepted server-side/domain-side. | Request created with status InReview. | Invalid request does not create Request. | `CreateRequest(...)`. |
| SC-04 | Use saved ApplicantData as source | Request-local applicant data | Saved ApplicantData type matches request need and copied data is accepted. | Request gets request-local applicant data. | If copied/edited data invalid, request not created. | `RequestApplicantData.FromSaved(...)`. |
| SC-04 | Enter applicant data inline | Request-local applicant data | Inline applicant fields accepted. | Request gets request-local applicant data. | Invalid applicant data prevents request creation. | `RequestApplicantData.CreateInline(...)`. |
| SC-04 | Edit prefilled applicant fields | Request-local applicant data | Edited fields accepted. | Request uses edited request-local applicant data. | Invalid edited fields prevent request creation. | request-local applicant data factory/update. |
| SC-04 | Edit/clear prefilled request fields | Saved ApplicantData unchanged | Editing is request-local. | Saved ApplicantData is not changed/deleted. | N/A | boundary rule: request-local copy/snapshot. |

Core invariants:

```text
- Accepted request creation writes Request.status = InReview.
- Request-local applicant data belongs to the Request write, not to saved ApplicantData.
- Request creation must not mutate saved ApplicantData unless there is a separate explicit save action.
```

## 10. Request Review Writes

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-07B | Start/perform review | No final state write by itself unless decision submitted | Request.status is InReview. | Review action allowed. | Non-InReview request cannot enter review. | `CanStartReview()` / `StartReview()`. |
| SC-07B | Approve request | Request.status + ReviewDecision | Request.status is InReview; employee allowed; approval input accepted. | Request.status becomes Approved; review decision recorded. | Approved/Rejected request cannot be approved. | `Approve(...)`. |
| SC-07B | Reject request | Request.status + ReviewDecision + RejectionFeedback | Request.status is InReview; employee allowed; rejection feedback accepted if required. | Request.status becomes Rejected; rejection feedback recorded. | Approved/Rejected request cannot be rejected. | `Reject(...)`. |

Core invariants:

```text
- Only InReview request can be approved.
- Only InReview request can be rejected.
- Approved/Rejected requests cannot be reviewed again in core.
- Rejected request should carry client-visible feedback if the domain keeps this requirement.
- Approval does not automatically create agreement proposal.
- Approved request only enables employee-started agreement proposal exchange.
```

State transition summary:

```text
InReview -> Approved
InReview -> Rejected
```

Forbidden in core:

```text
Approved -> Approved
Approved -> Rejected
Rejected -> Approved
Rejected -> Rejected
Approved/Rejected -> Review again
```

## 11. Request Document Writes

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-11 | Attach accepted request document | Request document attachment metadata/reference | Request context exists and document accepted server-side/domain-side. | Document is attached to request. | Invalid/rejected document is not attached. | `AttachDocument(...)`. |
| SC-11 | Reject invalid document | No attachment write | Document input not accepted. | No request document state change. | Upload/attach error returned. | attachment factory/result. |

Notes:

```text
- Domain should store accepted document metadata/reference, not physical file/blob content.
- Physical file storage is infrastructure.
```

## 12. Agreement Proposal Exchange Writes

### 12.1 Employee starts exchange

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-13D | Employee sends first proposal | AgreementProposalExchange / AgreementProposal.status | Related request is Approved; employee allowed; document/comment accepted. | Employee proposal created with status AwaitingClientConfirmation. | Proposal is not created if request is not Approved or input invalid. | `StartByEmployee(...)`. |

Invariants:

```text
- Agreement exchange starts only by employee.
- Agreement exchange starts only from Approved request.
- Approval does not automatically create proposal.
- Employee proposal requires attached document/file and text details/comment.
```

### 12.2 Client accepts employee proposal

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-13B | Client accepts proposal | AgreementProposal.status | Proposal is employee-sent, belongs to client context, status is AwaitingClientConfirmation. | Proposal status becomes Accepted. | Non-awaiting or not-owned proposal cannot be accepted. | `ClientAccept(...)`. |

Invariants:

```text
- Client can accept only own proposal.
- Client can accept only employee-sent proposal awaiting confirmation.
```

### 12.3 Client sends own version

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-13B | Client sends own proposal version | New AgreementProposal.status | Proposal is employee-sent and AwaitingClientConfirmation; client has not already sent own version in core; document/comment accepted. | Client proposal created with status SentByClient. | Response rejected if duplicate, wrong status, wrong ownership or invalid input. | `ClientSendOwnVersion(...)`. |

Invariants:

```text
- Client cannot start exchange.
- Client can respond only to employee-sent awaiting proposal.
- Client can send only one own version in core.
- Client-sent proposal requires attached document/file and text details/comment.
```

### 12.4 Employee sends new version after client proposal

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-13D | Employee sends new version | Previous proposal status + new proposal status | Previous proposal is client-sent with status SentByClient; employee allowed; new document/comment accepted. | Previous client proposal becomes Rejected; new employee proposal becomes AwaitingClientConfirmation. | No write if previous proposal is not client-sent/SentByClient or input invalid. | `EmployeeSendNewVersion(...)`. |

Invariants:

```text
- Employee response to client-sent proposal creates new employee proposal.
- New employee version rejects/replaces previous client-sent proposal.
- For core, previous client-sent proposal status becomes Rejected.
```

Agreement proposal state summary:

```text
Employee sends proposal
-> AwaitingClientConfirmation

Client accepts
-> Accepted

Client sends own version
-> SentByClient

Employee sends new version after client version
-> previous client version Rejected
-> new employee version AwaitingClientConfirmation
```

## 13. Client-Owned Read Scope Invariants

These are mostly read/query constraints, but they affect domain ownership.

| Scenario | Invariant | State involved | Domain-model pressure |
|---|---|---|---|
| SC-05 | Client can view only own requests. | Request owner / ClientAccountId | Request needs owner/client context. |
| SC-13A / SC-13B | Client can view only own agreement proposals. | Proposal exchange client context | Agreement proposal exchange needs client ownership/reference. |
| SC-15 | Employee-only actions require employee permission. | EmployeeRef / permission context | Write methods need actor/permission context at application/domain boundary. |

Notes:

```text
- These are not UI rules.
- They should be enforced server-side even if hidden UI actions are bypassed.
```

## 14. Verification Writes

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-14 | Employee starts client data verification | Verification result / request-related verification state | Request context exists; employee allowed. | Verification result may be recorded as Passed / Failed / Unavailable. | Verification cannot start without request. | `StartVerificationForRequest(...)` / `RecordVerificationResult(...)`. |
| SC-10 / SC-14 | Save standalone ApplicantData | No verification write | ApplicantData save outside request context. | ApplicantData saved only. | Verification not started. | no standalone verification action. |

Invariants:

```text
- Verification is available only in request context.
- Employee cannot start verification without request.
- Standalone ApplicantData editing does not trigger verification.
```

Status:

```text
future/supporting; do not design as standalone aggregate yet.
```

## 15. Anonymous Request Writes

| Scenario | Action | State written | Must be true before write | Write effect | No-write / failure behavior | Method pressure |
|---|---|---|---|---|---|---|
| SC-17 | Submit anonymous request/contact | AnonymousRequest / ContactRequest / DraftRequest | Required request/contact data accepted; follow-up contact reachable. | Anonymous submission recorded. | Invalid contact/request data is not recorded. | future `CreateAnonymousSubmission(...)`. |

Status:

```text
deferred / unresolved.
```

Open decision:

```text
Is this a full Request, ContactRequest, or DraftRequest?
```

## 16. Cross-Cutting No-Write Rules

| Rule | Where it matters | Meaning |
|---|---|---|
| Invalid data does not create object | registration, request, applicant data, proposal, anonymous request | domain/value construction or domain method returns errors, no state write. |
| Invalid status transition does not write | request review, proposal exchange | state remains unchanged. |
| UI validation is not trusted | all write scenarios | server/domain validation must still guard write. |
| File/blob content is not domain state | request docs, agreement docs | domain stores accepted reference/metadata only. |
| Auth/session mechanics are not business aggregate state | login/recovery | business domain uses identity refs. |

## 17. How This Helps Domain Variants

When generating a domain variant, use this file to ask:

```text
- Which object owns this persisted state?
- Which method should perform this write?
- Which invariants must that method enforce?
- Which state changes must be atomic together?
- Which state only needs to be referenced, not owned?
- Which writes should not happen in this scenario?
```

This is the input for creating domain variants, not the final model.

## 18. Next Step

Next step:

```text
Generate domain model variant 1.
```

Use these inputs:

```text
1. scenario text specs
2. DATA files
3. validation-related file
4. pre-domain-variants-input.md
```
