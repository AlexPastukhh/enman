# Scenario Specification Principles

Status: current source of truth for scenario specification principles  
Scope: textual scenario specs, scenario diagrams, scenario DATA blocks, validation addendum

## 1. Read Order

Read:

```text
planning/README.md
planning/planning-workflow-current.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
```

## 2. Scenario Specs

Scenario specs describe user-facing behavior and planning semantics.

They may contain:

```text
- actor / screen / goal;
- entry points;
- preconditions;
- DATA refs;
- main flow;
- branches;
- invariants;
- client-side validation;
- server-side / domain validation;
- observable outcomes;
- open questions;
- ADR candidates.
```

They must not become:

```text
- controller specs;
- endpoint maps;
- database schemas;
- ORM/EF mappings;
- React component plans;
- final aggregate implementation.
```

## 3. DATA

Use `DATA`, not `DETAIL`.

DATA means only what actor:

```text
- enters;
- sees;
- selects;
- filters/searches by;
- attaches/uploads;
- references as visible/selectable business item.
```

DATA files must not contain:

```text
- validation/rules sections;
- testable behavior sections;
- invariants;
- preconditions;
- branches;
- access rules;
- security policy.
```

Validation belongs in scenario text specs, validation addendum and domain design input.

## 4. Validation

Scenario specs should distinguish:

```text
Client-side validation
Server-side / Domain validation
```

Client-side validation is UX feedback and correction loop.

Server-side/domain validation is authoritative and should be driven by:

```text
value objects
domain model methods
domain factories
state-transition methods
```

The companion validation file is:

```text
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
```

The validation principles file is:

```text
planning/scenario-domain-validation-principles.md
```

## 5. Current Scenario Set

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
SC-08  Approved Result — merged
SC-09  Rejected Result — merged
SC-12  Review Feedback / Correction Navigation — merged
SC-16  Notification Navigation — removed as standalone
SC-18  Archive / Audit — deferred
```

## 6. Current Project Decisions

Request statuses:

```text
InReview
Approved
Rejected
```

Do not use `Submitted` unless reintroduced later with precise meaning.

Request object location means:

```text
object address
```

Applicant DATA reuse in request creation:

```text
- matching saved applicant DATA may be copied/prefilled into request form;
- if no matching applicant DATA exists, client enters applicant DATA inline;
- client may edit prefilled request-local fields;
- clearing/editing request-local applicant fields does not delete saved ApplicantData;
- clear/restore prefill controls are future UX.
```

Agreement Proposal means:

```text
concrete agreement document/version sent by one side to the other side
in the context of an Approved request.
```

Core proposal statuses:

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
- employee and client proposal submissions include attached document/file and text details/comment;
- client can send only one own proposal version in response to an employee-sent proposal in core;
- client cannot start exchange without employee-sent proposal;
- employee responds to client-sent proposal by sending a new employee version;
- previous client-sent proposal becomes Rejected when employee sends a new version.
```

## 7. Source Of Truth Rule

Use corrected text specs, DATA specs, validation addendum and consistency report.

Do not use stale generated diagram package summaries or stale `.drawio` pages as semantic source of truth until regenerated.

## 8. Next Planning Step

The next domain planning artifact after scenario/domain-design input is:

```text
planning/tables/domain-discovery-core.md
```
