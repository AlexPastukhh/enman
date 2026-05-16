# Scenario Text Specifications Index

Status: current textual scenario specification package index / ApplicantParty template-per-type model synchronized

## 1. Source Of Truth

Read with:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-questions-register.md
planning/slices/slice-scenario-flow-behavior-register.md
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
SC-10B My Applicant Parties
SC-11  Request Documents
SC-13A My Agreements
SC-13B Agreement Proposal Details / Response
SC-13C Employee Agreements
SC-13D Employee Agreement Proposal Create / Send Version
SC-14  Client Data Verification — future employee-started action
SC-15  Security Text Specification
SC-17  Anonymous Request
```

## 3. Key Current Decisions

ApplicantParty target model:

```text
A client account may store many ApplicantParty profiles over time.

For convenience, one current/default ApplicantParty template may exist per applicant type:
- physical person;
- individual entrepreneur;
- legal entity.

Current/default template controls initial prefill/default selection for future request creation.

Creating a new ApplicantParty does not delete, overwrite, deactivate or replace existing ApplicantParties.

Creating the first ApplicantParty of a type may initialize the current/default template for that type.

Creating an additional ApplicantParty of the same type does not silently change the existing current/default template.

Changing current/default when one already exists is a separate explicit behavior.
```

Request creation applicant context:

```text
Request creation uses one accepted applicant context.

If current/default ApplicantParty exists for selected type, fields may be prefilled.

If current/default is missing, fields are empty and ready for input.

If user clears prefilled fields or starts with missing default, new applicant data path is used.

Accepted new applicant data creates a new ApplicantParty and uses it for the request.

Request creation with new applicant data should be atomic in one server call, not two client calls.
```

Superseded applicant wording:

```text
- one current active ApplicantParty per account;
- replacement makes previous current inactive/non-current;
- request creation always uses server-selected single current active applicant.
```

Use the target model above for new scenario and slice planning.

## 4. Current Downstream Use

```text
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-behavior-items/
planning/slices/slice-scenario-flow-behavior-register.md
planning/tables/pre-domain-variants-input.md
domain model variants
slice/client planning
diagram-generation preflight
```

Do not let implementation planning silently decide scenario behavior.
