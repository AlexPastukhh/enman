# Scenario Diagram Consistency Report

Status: current consistency gate before responsibility tables  
Branch: `my-changes`  
Scope: scenario text specs, scenario DATA specs, generated package summaries, and responsibility-table readiness

## 1. Purpose

This report records the current scenario source-of-truth state after semantic correction.

It exists to prevent responsibility tables from being generated from stale diagram package summaries.

## 2. Current Source Of Truth

Use these as semantic source of truth:

```text
planning/scenario-specification-principles.md
planning/diagram-scenario-spec.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-diagram-consistency-report.md
```

Do not use old generated package summaries as semantic source of truth.

## 3. Scenario Text Specs Status

Current corrected scenario text specs define this model:

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
SC-10  Applicant Data
SC-11  Request Documents
SC-13A My Agreements
SC-13B Agreement Proposal Details / Response
SC-13C Employee Agreements
SC-13D Employee Agreement Proposal Create / Send Version
SC-14  Client Data Verification
SC-15  Security Text Specification
SC-17  Anonymous Request
```

Merged / removed / deferred:

```text
SC-08  Approved Result — merged into SC-07B + SC-05 + SC-13A/SC-13B/SC-13C/SC-13D
SC-09  Rejected Result — merged into SC-07B + SC-05
SC-12  Review Feedback / Correction Navigation — merged into SC-05 + SC-04
SC-16  Removed standalone notification scenario
SC-18  Archive / Audit — deferred / low priority
```

## 4. DATA Specs Status

DATA files are aligned to corrected text specs when they include:

```text
SC-04 request creation applicant prefill/inline behavior
SC-10 applicant DATA
SC-13A client agreement proposal list DATA
SC-13B client agreement proposal details/response DATA
SC-13C employee agreement proposal list DATA
SC-13D employee agreement proposal create/send version DATA
```

Agreement proposal core statuses are:

```text
AwaitingClientConfirmation
SentByClient
Accepted
Rejected
```

`Rejected` is core because employee-sent replacement rejects/replaces the previous client-sent proposal.

## 5. Stale Package Summary Status

The following generated package summaries were based on older scenario semantics and must not be used as source of truth for responsibility tables:

```text
planning/diagrams/scenario-core-package.md
planning/diagrams/scenario-extension-package.md
planning/diagrams/scenario-advanced-package.md
```

They should be replaced by superseded compatibility notes until visual diagrams are regenerated from corrected text specs.

Existing `.drawio`, `.png`, `.svg` files may still be visually useful, but if they represent old SC-08/SC-09/SC-12/SC-13/SC-16 semantics, they are stale for responsibility-table generation.

## 6. Key Corrections

### Request statuses

Use:

```text
InReview
Approved
Rejected
```

Do not use `Submitted` in scenario specs unless reintroduced later with precise meaning.

### Request object location

Request object location means:

```text
object address
```

### Request creation applicant DATA

When request creation needs applicant DATA:

```text
- matching saved applicant DATA may be copied/prefilled into request form;
- if no matching applicant DATA exists, client enters applicant DATA inline;
- client can edit prefilled fields for this request;
- future UX may add clear/restore prefill controls;
- clearing/editing request-local applicant fields does not delete saved applicant DATA.
```

### Agreement proposal exchange

Core rules:

```text
- exchange starts only by employee action from Approved request;
- approval does not automatically create agreement proposal;
- employee can start from Approved request details or Approved request list row/action;
- client can only send proposal in response to employee-sent proposal;
- both employee and client submissions include attached agreement document/file and text details/comment;
- client sends only one own proposal version in response in core;
- employee responds to client-sent proposal by sending a new employee version;
- previous client-sent proposal becomes Rejected when employee sends new version.
```

## 7. Responsibility Table Readiness

After this report and the DATA replacements are applied, responsibility tables may be started from corrected textual specs and DATA specs.

Recommended first table:

```text
planning/tables/scenario-responsibility-core.md
```

Recommended source set for that table:

```text
planning/scenario-specification-principles.md
planning/diagram-scenario-spec.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-data/
planning/diagrams/scenario-diagram-consistency-report.md
```

Do not use stale generated package summaries or stale `.drawio` pages as authoritative source until regenerated.

## 8. Table Generation Scope Recommendation

For the first responsibility table pass, include:

```text
SC-01
SC-02
SC-03A
SC-03B
SC-04
SC-05
SC-06
SC-07A
SC-07B
SC-10
SC-11
SC-13A
SC-13B
SC-13C
SC-13D
SC-14
SC-15
SC-17
```

Handle separately:

```text
SC-08 merged
SC-09 merged
SC-12 merged
SC-16 removed
SC-18 deferred
```

## 9. Open Consistency Notes

Visual diagram packages still need regeneration if they are expected to match corrected text specs.

Until regenerated, diagrams are secondary visual references only.

Responsibility tables should cite / reference scenario text spec item refs and DATA refs rather than old diagram item refs.
