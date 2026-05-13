# Scenario Diagram Consistency Report

Status: current consistency gate before domain variant generation  
Branch: `my-changes`  
Scope: scenario text specs, scenario DATA specs, validation addendum, generated package summaries, and domain-variant readiness

## 1. Purpose

This report records the current scenario source-of-truth state after semantic correction.

It prevents planning agents from using stale diagram package summaries or stale `.drawio` pages as source of truth.

## 2. Current Source Of Truth

Use these as semantic source of truth:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagram-scenario-spec.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/tables/pre-domain-variants-input.md
```

Do not use old generated package summaries as semantic source of truth.

## 3. Current Scenario Text Specs

Active:

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

DATA files are current when they include:

```text
SC-04 request creation applicant prefill/inline behavior
SC-10 applicant DATA
SC-13A client agreement proposal list DATA
SC-13B client agreement proposal details/response DATA
SC-13C employee agreement proposal list DATA
SC-13D employee agreement proposal create/send version DATA
```

## 5. Validation Status

Server/domain validation is tracked in:

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
```

Validation source-of-truth direction:

```text
value object construction / domain method
-> validation result
-> server validation response
```

Client-side validation is UX feedback only.

## 6. Pre-Domain Variant Readiness

The active post-scenario bridge file is:

```text
planning/tables/pre-domain-variants-input.md
```

It collects:

```text
- invariants;
- persisted/write state;
- state/status values;
- state-changing actions;
- state-dependent allowed/forbidden actions;
- no-write behavior;
- method pressure.
```

Next artifact:

```text
domain model variant 1
```

Use these inputs for domain variants:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
```

## 7. Superseded Domain-Planning Path

Do not use this as the current workflow:

```text
scenario-domain-design-input-core.md
-> domain-discovery-core.md
-> aggregate-boundary-candidates-core.md
-> domain-model-options-core.md
```

Those names are too fragmented for the current workflow and are superseded by:

```text
pre-domain-variants-input.md
-> domain model variants
```

## 8. Stale Package Summary Status

The following generated package summaries were based on older scenario semantics and must not be used as source of truth for domain planning:

```text
planning/diagrams/scenario-core-package.md
planning/diagrams/scenario-extension-package.md
planning/diagrams/scenario-advanced-package.md
```

Existing `.drawio`, `.png`, `.svg` files may still be visually useful, but if they represent old SC-08/SC-09/SC-12/SC-13/SC-16 semantics, they are stale for domain-variant generation.

## 9. Visual Diagram Note

Visual diagram packages still need regeneration if they are expected to match corrected text specs.

Until regenerated, diagrams are secondary visual references only.
