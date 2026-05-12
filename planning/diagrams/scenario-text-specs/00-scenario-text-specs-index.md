# Scenario Text Specifications Index

Status: draft textual scenario specification package for scenario semantic correction pass.

Purpose: provide one Markdown companion specification per corrected scenario / scenario status item before regenerating scenario diagrams.

These files are not responsibility tables and do not define architecture implementation. They describe user-facing behavior, entry points, preconditions, DATA refs, main flow, branches, invariants, outcomes, open questions and ADR candidates.

## Source Of Truth

Read first:

```text
planning/scenario-specification-principles.md
planning/diagram-scenario-spec.md
planning/diagrams/scenario-data/00-scenario-data-index.md
```

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

Merged / removed from old core package:

```text
SC-08  Approved Result — merged into SC-07B + SC-05 + SC-13A/SC-13B
SC-09  Rejected Result — merged into SC-07B + SC-05
```

Extension / corrected:

```text
SC-10  Applicant Data
SC-11  Request Documents
SC-12  Review Feedback / Correction Navigation — merged into SC-05 + SC-04
SC-13A My Agreements
SC-13B Agreement Proposal Details / Response
SC-14  Client Data Verification — future employee-started action, not triggered-by
```

Advanced / policy / deferred:

```text
SC-15  Security Text Specification
SC-16  Removed: Notification Navigation is part of SC-05 / SC-13 / review-result flows
SC-17  Anonymous Request
SC-18  Archive / Audit — deferred / low priority
```

## Global Decisions Reflected Here

Request statuses:

```text
InReview
Approved
Rejected
```

Removed / no longer used:

```text
Submitted status in scenario specs
standalone Approved Result scenario
standalone Rejected Result scenario
standalone Reliable Notification Delivery scenario
Contract Acknowledgement wording
Clarification as L1 live update submitted request
generic triggered-by for normal UX paths
email link as default entry-point wording, except explicit password recovery email
mandatory dark theme
DETAIL terminology
```

## Applicant Decisions Reflected Here

Applicant types:

```text
physical person
individual entrepreneur
legal entity
```

Physical person applicant target DATA:

```text
- ФИО;
- СНИЛС;
- паспортные данные;
- phone;
- email;
- actual/residential address as target/future expansion relative to current implemented L1.
```

Individual entrepreneur applicant DATA:

```text
- ФИО ИП;
- ИНН;
- ОГРНИП;
- phone;
- email;
- registration address as future/extension.
```

Legal entity applicant DATA:

```text
- organization name;
- ИНН;
- ОГРН;
- phone;
- email;
- legal address / КПП / representative as future/extension.
```

## Agreement Proposal Decisions Reflected Here

Use:

```text
Agreement Proposal
```

Meaning:

```text
Agreement Proposal = конкретный вариант договора / договорный документ,
отправленный одной стороной другой стороне в рамках approved request.
```

Core statuses:

```text
AwaitingClientConfirmation
SentByClient
Accepted
```

Future statuses:

```text
Signed
Rejected
Expired
Superseded
Cancelled
```

Key flow DATA:

```text
- related Approved request;
- sender: employee or client;
- status;
- attached agreement document/file;
- visible summary/name.
```

## DATA Rules Reflected Here

Use:

```text
DATA blocks for actor-entered, actor-visible, selected, filtered or attached data.
```

Do not use:

```text
DETAIL
SC-XX-DETAIL-YY
```

Do not put into DATA files:

```text
validation/rules sections
testable behavior sections
branches
invariants
access rules
security policy
```
