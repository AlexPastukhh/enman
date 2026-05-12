# Scenario Text Specifications Index

Status: draft textual scenario specification package for scenario semantic correction pass.

Purpose: provide one Markdown companion specification per corrected scenario / scenario status item before regenerating scenario diagrams.

Canonical target folder in repository:

```text
planning/diagrams/
```

These files are not responsibility tables and do not define architecture implementation. They describe user-facing behavior, entry points, preconditions, main flow, branches, DETAIL nodes, invariants, outcomes, open questions and ADR candidates.

## Corrected Scenario Set

Core package:

```text
SC-01  Guest Registration
SC-02  Login
SC-03A Password Recovery Request
SC-03B Set New Password
SC-04  Client Request Creation
SC-05  My Requests / Own Request Details
SC-06  Employee Request Dashboard
SC-07A Employee Request Details
SC-07B Employee Request Review
```

Merged / removed from old core package:

```text
SC-08  Approved Result — merged into SC-07B + SC-05 + SC-13
SC-09  Rejected Result — merged into SC-07B + SC-05 + SC-12
```

Extension package:

```text
SC-10  Applicant Data
SC-11  Request Documents
SC-12  Review Feedback / Correction Navigation
SC-13  My Agreements / Agreement Details / Agreement Response
SC-14  Client Data Verification
```

Advanced package:

```text
SC-15  Security Text Specification
SC-16  Removed: Notification Navigation is part of SC-05 / SC-12 / SC-13
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
Submitted status
standalone Approved Result scenario
standalone Rejected Result scenario
standalone Reliable Notification Delivery scenario
Contract Acknowledgement wording
Clarification as L1 live update submitted request
generic triggered-by for normal UX paths
email link as default entry-point wording
mandatory dark theme
```

## Diagram Rules Reflected Here

Use:

```text
DETAIL nodes for user-visible input / filter / criteria details.
Client-side validation when observable to the user.
Correction loops for correctable validation errors.
Entry points for actor-started UX/business contexts.
Off-page links when current scenario sends actor to another scenario/page.
Triggered by only for rare non-actor/system/background/domain activation.
Side-rail Q / ADR? / RISK blocks on diagrams when questions appear in text.
```

Do not use:

```text
implementation navigation as default flow wording:
- URL
- route
- controller
- email provider
- notification service

Use UX/business wording instead:
- after review feedback
- after agreement option became available
- from My Requests list
- while creating a request
```
