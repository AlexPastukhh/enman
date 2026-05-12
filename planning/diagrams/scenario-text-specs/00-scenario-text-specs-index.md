# Scenario Text Specifications Index

Status: draft textual scenario specification package.

These files describe user-facing behavior, entry points, preconditions, DATA refs, main flow, branches, invariants, outcomes, open questions and ADR candidates.

Read first:

```text
planning/scenario-specification-principles.md
planning/diagram-scenario-spec.md
planning/diagrams/scenario-data/00-scenario-data-index.md
```

Core:

```text
SC-01 Guest Registration
SC-02 Login
SC-03A Password Recovery Request
SC-03B Account Owner Verified / Password Reset Choice
SC-04 Client Request Creation
SC-05 My Requests / Own Request Details
SC-06 Employee Request Dashboard
SC-07A Employee Request Details
SC-07B Employee Request Review
```

Merged/removed:

```text
SC-08 Approved Result — merged into SC-07B + SC-05 + future agreement scenarios
SC-09 Rejected Result — merged into SC-07B + SC-05
SC-12 Review Feedback / Correction Navigation — merged into SC-05 + SC-04
SC-16 Reliable Notification Delivery — removed
```

Pending/deferred:

```text
SC-10 Applicant Data — pending applicant DATA discussion
SC-13 Agreement / Proposal — pending agreement/proposal discussion
SC-18 Archive / Audit — deferred
```

Global decisions: statuses are InReview / Approved / Rejected; no Submitted; DATA replaces DETAIL.
