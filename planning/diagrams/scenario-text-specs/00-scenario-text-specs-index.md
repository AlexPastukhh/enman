# Scenario Text Specifications Index

Status: current textual scenario specification package index

## 1. Source Of Truth

Read with:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-diagram-consistency-report.md
```

## 2. Corrected Scenario Set

Core:

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

Merged / removed / deferred:

```text
SC-08  Approved Result — merged into SC-07B + SC-05 + SC-13A/SC-13B/SC-13C/SC-13D
SC-09  Rejected Result — merged into SC-07B + SC-05
SC-12  Review Feedback / Correction Navigation — merged into SC-05 + SC-04
SC-16  Removed: Notification Navigation is part of SC-05 / SC-13 / review-result flows
SC-18  Archive / Audit — deferred / low priority
```

## 3. Validation Companion

Use:

```text
scenario-server-domain-validation-addendum.md
```

It adds for each scenario:

```text
- client-side validation;
- server-side/domain validation;
- value object / domain candidates.
```

Validation rules do not belong in DATA files.

## 4. Key Current Decisions

Request statuses:

```text
InReview
Approved
Rejected
```

Request object location means:

```text
object address
```

Agreement Proposal:

```text
concrete agreement document/version sent by one side to the other side
in the context of an Approved request.
```

Core agreement proposal statuses:

```text
AwaitingClientConfirmation
SentByClient
Accepted
Rejected
```

Core agreement proposal rules:

```text
- exchange starts only by employee action on an Approved request;
- approval does not automatically create agreement proposal;
- employee and client proposal submissions include attached agreement document/file and text details/comment;
- client can send only one own proposal version in response to an employee-sent proposal in core;
- client cannot start exchange without employee-sent proposal;
- employee responds to client-sent proposal by sending a new employee version;
- previous client-sent proposal becomes Rejected when employee sends a new version.
```

## 5. Downstream Consumers

```text
planning/tables/scenario-domain-design-input-core.md
planning/tables/domain-discovery-core.md
planning/tables/ui-page-responsibility-map-core.md
```
