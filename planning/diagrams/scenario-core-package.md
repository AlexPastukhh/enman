# Scenario Core Package — Superseded Summary

Status: superseded / compatibility note  
Doc version: v0.1.0  
Scope: previous generated core diagram package summary

## Important

This Markdown summary is no longer the semantic source of truth for scenario planning.

Use the corrected textual scenario specifications instead:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/scenario-specification-principles.md
planning/diagrams/scenario-diagram-consistency-report.md
```

## Why superseded

The previous core package summary described older scenario pages:

```text
SC-03 Password Recovery as one scenario
SC-05 Client Request Status / Result
SC-07 Employee Request Review as one scenario
SC-08 Approval Result
SC-09 Rejection Result
```

The corrected scenario model now uses:

```text
SC-03A Password Recovery Request
SC-03B Account Owner Verified / Password Reset Choice
SC-05 My Requests / Own Request Details
SC-07A Employee Request Details
SC-07B Employee Request Review
SC-08 merged into SC-07B + SC-05 + SC-13A/SC-13B/SC-13C/SC-13D
SC-09 merged into SC-07B + SC-05
```

## Current core scenario set

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

## Planning rule

Do not build responsibility tables from the old core package summary.

If visual `.drawio` pages still exist for the old package, treat them as stale visual references only until regenerated from corrected text specs.
