# Scenario Server / Domain Validation Addendum

Status: draft companion to scenario text specifications  
Scope: server-side validation, domain validation, value-object and state-transition validation hints

## 1. Purpose

This file adds validation semantics to the scenario text specifications without moving validation into DATA files.

DATA files stay narrow:

```text
what actor enters / sees / selects / filters by / attaches
```

This addendum records:

```text
- where client-side validation is expected;
- where server-side validation is required;
- which rules should come from value objects / domain model methods;
- which invalid operations must be rejected by domain/application use cases.
```

## 2. General Validation Model

Client-side validation:

```text
visible UX feedback and correction loops
```

Server-side/domain validation:

```text
authoritative validation through value objects, domain factories and domain state-transition methods
```

Server response errors should be derived from domain/value-object validation results where possible.

## 3. Scenario Validation Catalog

### SC-01 — Guest Registration

Client-side validation:

```text
- email is present and email-like;
- password is present;
- password confirmation is present;
- password and confirmation match.
```

Server-side / domain validation:

```text
- account cannot be created if EmailAddress cannot be created;
- account cannot be created if password policy fails;
- account cannot be created if password confirmation does not match;
- invalid registration input returns validation errors and does not create account.
```

Domain / value-object candidates:

```text
EmailAddress
Password / PasswordPolicy
Credentials
ClientAccount / Account
```

### SC-02 — Login

Client-side validation:

```text
- email is present and email-like;
- password is present.
```

Server-side / domain validation:

```text
- credentials must be validated server-side;
- session is issued only for valid credentials;
- invalid credentials do not create session;
- security policies such as throttling/account lock are server-side/cross-cutting.
```

Domain / value-object candidates:

```text
EmailAddress
Credentials
Session / AuthSession
AccountSecurityPolicy
```

### SC-03A — Password Recovery Request

Client-side validation:

```text
- recovery email is present and email-like.
```

Server-side / domain validation:

```text
- recovery request input must create/validate EmailAddress;
- if email belongs to account, RecoveryContext may be created and email sent;
- if email does not belong to account, visible response remains neutral;
- account existence must not be revealed.
```

Domain / value-object candidates:

```text
EmailAddress
RecoveryContext
RecoveryToken
AccountRecoveryPolicy
```

### SC-03B — Account Owner Verified / Password Reset Choice

Client-side validation:

```text
- new password is present when password reset branch is used;
- password confirmation is present;
- new password and confirmation match.
```

Server-side / domain validation:

```text
- recovery context/token must be valid before password update;
- expired/invalid recovery context cannot update password;
- new password must satisfy password policy;
- invalid reset input returns validation errors and does not update password;
- login-with-old/current-password branch does not change credentials.
```

Domain / value-object candidates:

```text
RecoveryContext
RecoveryToken
Password / PasswordPolicy
Credentials
Account
```

### SC-04 — Client Request Creation

Client-side validation:

```text
- required request fields are present;
- object address is present;
- applicant fields required for selected applicant type are present;
- visible form errors allow correction before submit.
```

Server-side / domain validation:

```text
- Request cannot be created if ObjectAddress cannot be created;
- Request cannot be created if required request data is missing/invalid;
- applicant data used for request must pass domain/value-object validation;
- saved applicant DATA copied into request form must still be accepted as request-local applicant data;
- edited request-local applicant data must be validated independently;
- request-local applicant data changes must not mutate saved ApplicantData;
- accepted request starts with status InReview;
- invalid request input returns validation errors and does not create Request.
```

Domain / value-object candidates:

```text
Request / ConnectionRequest
RequestStatus
ObjectAddress
RequestSubject / RequestDetails
ApplicantData
RequestApplicantData
RequestedPowerKw [future]
```

### SC-05 — My Requests / Own Request Details

Client-side validation:

```text
- filter controls may validate only supported status/filter values.
```

Server-side / domain validation:

```text
- request list/details must be scoped to current client ownership;
- client cannot open another client's request;
- request status must be one of supported persisted statuses;
- rejected request feedback must belong to the selected own request.
```

Domain / value-object candidates:

```text
Request
RequestStatus
RequestOwner / ClientAccountId
ReviewDecision
RejectionFeedback
```

### SC-06 — Employee Request Dashboard

Client-side validation:

```text
- filter controls may validate supported status/filter values.
```

Server-side / domain validation:

```text
- employee dashboard requires employee authorization;
- review queue must include only review-eligible/currently visible request states;
- start-review action must be rejected for non-InReview requests.
```

Domain / value-object candidates:

```text
Request
RequestStatus
ReviewEligibility
EmployeePermission
```

### SC-07A — Employee Request Details

Client-side validation:

```text
- no major input validation; action availability is visible by status.
```

Server-side / domain validation:

```text
- employee access to request details must be authorized;
- start-review action must be rejected when request is Approved or Rejected;
- processed requests are read-only for review actions in core.
```

Domain / value-object candidates:

```text
Request
RequestStatus
ReviewEligibility
EmployeePermission
```

### SC-07B — Employee Request Review

Client-side validation:

```text
- decision must be selected;
- rejection explanation should be present when decision is Rejected, if required by domain decision;
- visible errors allow correction before submit.
```

Server-side / domain validation:

```text
- review can start only for InReview request;
- approve transition is valid only from InReview;
- reject transition is valid only from InReview;
- Approved/Rejected requests cannot be reviewed again in core;
- rejection explanation must be accepted if required by domain rule;
- review decision must be recorded atomically with status transition.
```

Domain / value-object candidates:

```text
Request
RequestStatus
ReviewDecision
RejectionFeedback
ApprovalMessage [optional/future]
EmployeeId / ReviewerRef
```

### SC-10 — Applicant Data

Client-side validation:

```text
- applicant type must be selected;
- required fields for selected applicant type are visible and checked before submit;
- format-like validation may be shown for email, phone, SNILS, INN, OGRN/OGRNIP.
```

Server-side / domain validation:

```text
- ApplicantData cannot be saved if applicant type is missing/unsupported;
- physical person data must create valid FullName, Snils, PassportData, PhoneNumber, EmailAddress;
- individual entrepreneur data must create valid EntrepreneurName/FullName, Inn, Ogrnip, PhoneNumber, EmailAddress;
- legal entity data must create valid OrganizationName, Inn, Ogrn, PhoneNumber, EmailAddress;
- invalid applicant data returns validation errors and is not saved;
- saving standalone ApplicantData must not start verification.
```

Domain / value-object candidates:

```text
ApplicantData
ApplicantType
FullName
Snils
PassportData
PhoneNumber
EmailAddress
Inn
Ogrn
Ogrnip
OrganizationName
ActualAddress [future]
LegalAddress [future]
RegistrationAddress [future]
```

### SC-11 — Request Documents

Client-side validation:

```text
- document/file is selected before attach;
- visible errors for missing/rejected document;
- future: format/size/type feedback if required by UX.
```

Server-side / domain validation:

```text
- document cannot be attached without request context;
- invalid/rejected document is not attached;
- accepted document becomes request attachment;
- document metadata/file reference must be accepted before recording attachment;
- attachment must be scoped to request/client ownership.
```

Domain / value-object candidates:

```text
RequestDocument
DocumentAttachment
DocumentFileRef
DocumentType [future]
Request
```

### SC-13A — My Agreements

Client-side validation:

```text
- filter controls may validate supported proposal status/sender values.
```

Server-side / domain validation:

```text
- My Agreements list must be scoped to current client ownership;
- client can view only own agreement proposals;
- agreement proposal status must be supported domain status;
- proposal must be related to Approved request accessible to client.
```

Domain / value-object candidates:

```text
AgreementProposal
AgreementProposalStatus
AgreementProposalSender
AgreementProposalOwner
RequestRef
```

### SC-13B — Agreement Proposal Details / Response

Client-side validation:

```text
- accept action requires selected/visible proposal;
- send-own-version action requires attached document/file;
- text details/comment input should be present if required by domain rule.
```

Server-side / domain validation:

```text
- client can open only own proposal details;
- client can respond only to employee-sent proposal with status AwaitingClientConfirmation;
- client cannot start agreement proposal exchange without employee-sent proposal;
- client can send only one own proposal version in response in core;
- client-sent proposal must include attached document/file;
- text details/comment must be accepted if required by domain rule;
- accept transition changes proposal to Accepted;
- send-own-version transition creates/records client-sent proposal with status SentByClient.
```

Domain / value-object candidates:

```text
AgreementProposalExchange
AgreementProposal
AgreementProposalStatus
AgreementProposalSender
AgreementDocument / ProposalAttachment
ProposalComment / TextDetails
RequestRef
```

### SC-13C — Employee Agreements

Client-side validation:

```text
- filter controls may validate supported proposal status/sender values.
```

Server-side / domain validation:

```text
- employee access to agreement proposals must be authorized;
- employee can view proposal exchange only in authorized context;
- client-sent proposals are valid follow-up candidates for employee new-version action;
- proposal must relate to Approved request.
```

Domain / value-object candidates:

```text
AgreementProposalExchange
AgreementProposal
AgreementProposalStatus
AgreementProposalSender
EmployeePermission
RequestRef
```

### SC-13D — Employee Agreement Proposal Create / Send Version

Client-side validation:

```text
- agreement document/file must be selected before submit;
- text details/comment should be present if required by domain rule;
- visible errors allow correction before submit.
```

Server-side / domain validation:

```text
- employee must be authorized to manage agreement proposals;
- initial agreement proposal can be created only from Approved request;
- approval does not automatically create agreement proposal;
- employee-sent proposal must include attached document/file;
- employee-sent proposal must include text details/comment if required by domain rule;
- initial employee-sent proposal receives status AwaitingClientConfirmation;
- employee can send new version in response to client-sent proposal;
- when employee sends new version, previous client-sent proposal becomes Rejected;
- client cannot start agreement proposal exchange.
```

Domain / value-object candidates:

```text
AgreementProposalExchange
AgreementProposal
AgreementProposalStatus
AgreementProposalSender
AgreementDocument / ProposalAttachment
ProposalComment / TextDetails
RequestRef
EmployeePermission
```

### SC-14 — Client Data Verification

Client-side validation:

```text
- no major field validation in current model; action availability is request-context dependent.
```

Server-side / domain validation:

```text
- verification can be started only from request context;
- employee cannot start verification without request;
- standalone ApplicantData editing does not trigger verification;
- verification result must be normalized to supported result state;
- review may use verification result only when related to the request.
```

Domain / value-object candidates:

```text
VerificationResult
VerificationStatus
VerificationIssue
RequestRef
VerificationPolicy
```

### SC-15 — Security Text Specification

Client-side validation:

```text
- not primary; mostly policy and server/framework enforcement.
```

Server-side / domain validation:

```text
- client resource access must be ownership-scoped;
- employee actions require employee permission;
- recovery must not reveal account existence;
- rate limiting/account lock/password recovery abuse protection are server-side security policies.
```

Domain / value-object candidates:

```text
ClientAccountId
EmployeePermission
AccountSecurityPolicy
RecoveryPolicy
```

### SC-17 — Anonymous Request

Client-side validation:

```text
- request/contact fields required by anonymous form are visible and checked before submit;
- follow-up contact must be present;
- visible errors allow correction.
```

Server-side / domain validation:

```text
- anonymous request/contact request cannot be recorded without valid follow-up contact;
- invalid contact data is rejected;
- accepted anonymous submission creates the chosen domain object: AnonymousRequest, ContactRequest or DraftRequest;
- exact object type remains a domain decision.
```

Domain / value-object candidates:

```text
AnonymousRequest / ContactRequest / DraftRequest
FollowUpContact
EmailAddress
PhoneNumber
RequestSubject / RequestDetails
```

## 4. Deferred / Merged Scenarios

```text
SC-08 Approved Result — merged into SC-07B + SC-05 + SC-13A/B/C/D.
SC-09 Rejected Result — merged into SC-07B + SC-05 + SC-04.
SC-12 Review Feedback / Correction Navigation — merged into SC-05 + SC-04.
SC-16 Notification Navigation — removed as standalone scenario.
SC-18 Archive / Audit — deferred.
```

## 5. How To Use This File

Use this file with:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/tables/scenario-domain-design-input-core.md
```

Do not copy validation rules into DATA files.

When domain models are designed, prefer:

```text
value object creation / domain method result
-> validation result
-> application response / UI error
```

over duplicated manual validation scattered across controllers or UI.
