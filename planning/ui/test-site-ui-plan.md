# Test Site UI Plan

Status: template / to be filled by UI planning chat  
Scope: textual page plan for use-case coverage

## 1. Purpose

This file is a textual plan of a low-fidelity test site.

It answers:

```text
How should the site be organized so that current use cases can be walked through?
```

It is not final visual design and not an HTML/React prototype.

## 2. Source Set

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/ui/ui-planning-workflow.md
```

## 3. Page Inventory

Client pages:

```text
- Registration
- Login
- Password Recovery Request
- Account Owner Verified / Password Reset Choice
- Request Creation
- My Requests
- Own Request Details
- Applicant Data
- Request Documents section/page if needed
- My Agreements
- Agreement Proposal Details / Response
- Anonymous Request
```

Employee pages:

```text
- Employee Request Dashboard
- Employee Request Details
- Employee Request Review
- Employee Agreements
- Employee Agreement Proposal Create / Send Version
- Client Data Verification action/state if shown in request context
```

## 4. Scenario-To-Page Map

| Scenario | Page(s) | Notes |
|---|---|---|
| SC-01 Guest Registration | Registration | To be filled. |
| SC-02 Login | Login | To be filled. |
| SC-03A Password Recovery Request | Password Recovery Request | To be filled. |
| SC-03B Account Owner Verified / Password Reset Choice | Account Owner Verified / Password Reset Choice | To be filled. |
| SC-04 Client Request Creation | Request Creation | To be filled. |
| SC-05 My Requests / Own Request Details | My Requests, Own Request Details | To be filled. |
| SC-06 Employee Request Dashboard | Employee Request Dashboard | To be filled. |
| SC-07A Employee Request Details | Employee Request Details | To be filled. |
| SC-07B Employee Request Review | Employee Request Review | To be filled. |
| SC-10 Applicant Data | Applicant Data | To be filled. |
| SC-11 Request Documents | Request Documents section/page | To be filled. |
| SC-13A My Agreements | My Agreements | To be filled. |
| SC-13B Agreement Proposal Details / Response | Agreement Proposal Details / Response | To be filled. |
| SC-13C Employee Agreements | Employee Agreements | To be filled. |
| SC-13D Employee Agreement Proposal Create / Send Version | Employee Agreement Proposal Create / Send Version | To be filled. |
| SC-14 Client Data Verification | Employee Request Details / Review context | To be filled. |
| SC-15 Security Text Specification | Cross-page access behavior | To be filled. |
| SC-17 Anonymous Request | Anonymous Request | To be filled. |

## 5. Page Plan Template

Use this structure for every page:

```text
## Page: <page name>

Supported scenarios:
- SC-...

Actor:
- Client / Employee / Guest / Anonymous user

Purpose:
- ...

Entry points / UX paths:
- ...

Visible DATA:
- ...

Inputs:
- ...

Actions:
- ...

Status-dependent UI:
- ...

Validation / error states:
- ...

Empty states:
- ...

Navigation:
- ...

Open UI questions:
- [Q:UI-...]
```

## 6. Page Plans

### Page: My Requests

Supported scenarios:

```text
SC-05 — My Requests / Own Request Details
```

Actor:

```text
Client
```

Purpose:

```text
Client views own requests and current statuses.
Client can open selected request details.
```

Entry points / UX paths:

```text
- Client opens My Requests from client navigation.
- Client arrives after request creation confirmation.
- Client arrives after review feedback context if applicable.
```

Visible DATA:

```text
- request summary;
- object address;
- request status: InReview / Approved / Rejected;
- created date if needed;
- available action: open details.
```

Actions:

```text
- filter/search requests;
- open request details.
```

Status-dependent UI:

```text
InReview:
- show that request is under review.

Approved:
- show approved status;
- show agreement-related entry if proposal exists or if My Agreements should be opened.

Rejected:
- show rejected status;
- provide entry to rejection feedback in request details.
```

Validation / error states:

```text
- client cannot open another client's request details;
- unauthorized access should not show another client's data.
```

Empty state:

```text
- no requests yet;
- show action to create request.
```

Open UI questions:

```text
[Q:UI-001] Where should approved request point if agreement proposal exists?
```

### Page: Agreement Proposal Details / Response

Supported scenarios:

```text
SC-13B — Agreement Proposal Details / Response
```

Actor:

```text
Client
```

Purpose:

```text
Client views a selected agreement proposal and responds if action is available.
```

Visible DATA:

```text
- related Approved request;
- proposal sender: employee or client;
- proposal status: AwaitingClientConfirmation / SentByClient / Accepted / Rejected;
- attached agreement document/file;
- text details/comment;
- proposal history summary if needed.
```

Actions:

```text
- accept employee-sent proposal;
- attach own agreement version;
- send own version with text details/comment;
- return to My Agreements.
```

Status-dependent UI:

```text
AwaitingClientConfirmation + sender Employee:
- client can accept;
- client can send own version.

SentByClient:
- client sees that own version was sent;
- no second own version action in core.

Accepted:
- proposal is accepted;
- response actions are not available.

Rejected:
- proposal is no longer active;
- response actions are not available.
```

Validation / error states:

```text
- missing document when sending own version;
- missing/invalid text details if required;
- server rejects response if proposal is not awaiting client confirmation;
- server rejects response if proposal does not belong to client.
```

Open UI questions:

```text
[Q:UI-002] Should accepted proposal be shown as final agreement or still as proposal in core?
```

## 7. Cross-Page Navigation

To be filled.

Examples to consider:

```text
Login -> Password Recovery
Request Creation -> My Requests / Own Request Details
My Requests -> Own Request Details
Own Request Details -> Request Creation after rejected feedback
Own Request Details -> My Agreements if approved/agreement-related flow exists
My Agreements -> Agreement Proposal Details / Response
Employee Request Dashboard -> Employee Request Details
Employee Request Details -> Employee Request Review
Employee Request Details -> Employee Agreement Proposal Create / Send Version if request is Approved
Employee Agreements -> Employee Agreement Proposal Create / Send Version
```

## 8. Status-Dependent UI States

To be filled.

At minimum cover:

```text
Request.status:
- InReview
- Approved
- Rejected

AgreementProposal.status:
- AwaitingClientConfirmation
- SentByClient
- Accepted
- Rejected
```

## 9. Validation / Error / Empty States

To be filled.

At minimum cover:

```text
- form validation errors;
- server/domain validation responses;
- forbidden access to another client's resources;
- forbidden review action for non-InReview request;
- missing agreement document when sending proposal;
- duplicate client proposal response in core;
- empty My Requests;
- empty My Agreements;
- empty Employee Dashboard.
```

## 10. Coverage Gaps

To be filled.

Use this section when a scenario cannot be covered by the current page plan.

## 11. UI Questions Summary

To be filled from:

```text
planning/ui/ui-questions-register.md
```
