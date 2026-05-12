# Scenario Text Specifications Index

Status: draft textual scenario specification package for scenario semantic correction pass.

## Corrected Scenario Set

Core package:

```text
SC-01  Guest Registration
SC-02  Login
SC-03A Password Recovery Request
SC-03B Account Owner Verified / Password Reset Choice
SC-04  Client Request Creation
SC-05  My Requests / Own Request Details
SC-06  Employee Request Dashboard
SC-07A Employee Request Details
SC-07B Employee Request Review
```

Merged / removed:

```text
SC-08  Approved Result — merged into SC-07B + SC-05 + SC-13A/SC-13B/SC-13C/SC-13D
SC-09  Rejected Result — merged into SC-07B + SC-05
SC-12  Review Feedback / Correction Navigation — merged into SC-05 + SC-04
SC-16  Removed: Notification Navigation is part of SC-05 / SC-13 / review-result flows
SC-18  Archive / Audit — deferred / low priority
```

Extension / corrected:

```text
SC-10  Applicant Data
SC-11  Request Documents
SC-13A My Agreements
SC-13B Agreement Proposal Details / Response
SC-13C Employee Agreements
SC-13D Employee Agreement Proposal Create / Send Version
SC-14  Client Data Verification — future employee-started action, not triggered-by
SC-15  Security Text Specification
SC-17  Anonymous Request
```

## Request Creation Decisions Reflected Here

Request object location means object address.

Applicant DATA behavior in request creation:

```text
- matching previously provided applicant DATA may be copied/prefilled into the request form;
- if no matching applicant DATA exists, client enters applicant DATA inline;
- if client wants different applicant DATA for this request, client edits the request form fields;
- clear/restore prefill controls are future UX, not current core.
```

## Agreement Proposal Decisions Reflected Here

Agreement Proposal = concrete agreement document/version sent by one side to another side in the context of an Approved request.

Core statuses:

```text
AwaitingClientConfirmation
SentByClient
Accepted
Rejected
```

Core agreement proposal rules:

```text
- agreement proposal exchange starts only by employee action on an Approved request;
- employee can start agreement proposal creation from Approved request details or Approved request list row/action;
- approval does not automatically create agreement proposal;
- employee and client proposal submissions include attached agreement document/file and text details/comment;
- client can send only one own proposal version in response to an employee-sent proposal in core;
- client cannot start agreement proposal exchange without an existing employee-sent proposal;
- employee responds to client-sent proposal by sending a new employee version;
- when employee sends a new version in response to a client-sent proposal, the previous client-sent proposal becomes Rejected.
```
