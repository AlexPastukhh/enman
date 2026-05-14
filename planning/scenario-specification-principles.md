# Scenario Specification Principles

Status: current source of truth for scenario specification principles  
Scope: textual scenario specs, scenario diagrams, scenario DATA blocks, validation addendum, UI-planning feedback rules, domain-draft coverage baseline feedback

## 1. Read Order

Read:

```text
planning/README.md
planning/planning-workflow-current.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
planning/domain-draft-generation-guide.md
planning/ui/README.md
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
- observable UI requirements;
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
- final aggregate implementation;
- final visual design.
```

## 3. Observable UI Requirements In Scenario Specs

Scenario specs may include mandatory business-visible UI behavior.

This means:

```text
what user must see;
what user must understand;
what action must be available;
what status/result/feedback must be visible;
what forbidden access/action must be prevented or rejected.
```

Examples:

```text
Client sees own requests list.
Each request shows current status.
Rejected request details show rejection feedback.
Employee can review only InReview requests.
Client can accept only employee-sent agreement proposal awaiting confirmation.
Non-active account cannot access protected functionality.
```

Do not put layout or component choices into scenario specs.

## 4. Scenario Behavior Coverage Baseline Feedback

The scenario behavior coverage baseline is:

```text
planning/tables/pre-domain-variants-input.md
```

Focused baseline addenda may extend it:

```text
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

It can be started while scenario specs and DATA files are being written.

It is derived from scenario specs and DATA files.

It may reveal that a scenario is underspecified.

If baseline creation reveals a missing required behavior, update the scenario spec.

If baseline creation reveals a missing visible/input/selectable/filter/attachment data item, update the DATA file.

If baseline creation reveals several possible UX/business interpretations, add a scenario question to the baseline and, if important, to the related scenario spec.

Do not let the baseline silently invent business behavior.

## 5. Scenario Questions

Use scenario questions when:

```text
- scenarios do not define enough behavior to choose one implementation;
- several valid UX/business interpretations exist;
- a future flow may change the core behavior;
- a rule is accepted generally but exact user-facing outcome is not fixed.
```

Scenario questions can be recorded in:

```text
- related scenario spec Open Questions;
- focused baseline addendum as SQ items;
- future ADR if the answer affects architecture or security.
```

Example:

```text
If user created account but did not activate it and then tries to log in, should login fail, create limited session, or allow session while blocking protected actions with AccountActivated policy?
```

## 6. DATA

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
- security policy;
- layout choices.
```

Validation belongs in scenario text specs and validation addendum.

The pre-domain coverage baseline uses DATA but does not replace DATA files:

```text
planning/tables/pre-domain-variants-input.md
```

UI planning uses DATA to define visible/input/selectable page content:

```text
planning/ui/test-site-ui-plan.md
```

## 7. Validation

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
authorization / account policy guards
```

The companion validation file is:

```text
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
```

The validation principles file is:

```text
planning/scenario-domain-validation-principles.md
```

In the scenario behavior coverage baseline, avoid using broad `validation` wording for all rule types.

Use:

```text
Value Integrity / Anti-Primitive-Obsession Items
```

for data/value shape questions such as email, phone, object address, passport data, SNILS/INN/OGRN and agreement document references.

State transition validation belongs to scenario state/condition matrices and command behavior items.

Account activation belongs to account/security policy behavior and protected-use-case preconditions.

## 8. Current Scenario Set

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

## 9. Current Project Decisions

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

Account activation rules:

```text
- Account has activation state/marker.
- Current core registration creates an activated account.
- Protected client/employee functionality requires activated account.
- Non-active account cannot execute protected business commands or access protected pages/data.
- Current implementation direction: application service guard + Account.EnsureActivated.
- Future implementation direction: AccountActivated authorization policy, possibly backed by account_activated claim to avoid database lookup on every protected request.
```

## 10. Source Of Truth Rule

Use corrected text specs, DATA specs, validation addendum, consistency report and scenario behavior coverage baseline.

Do not use stale generated diagram package summaries or stale `.drawio` pages as semantic source of truth until regenerated.

## 11. Current Domain-Planning Step

The current pre-domain coverage baseline is:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

The current domain draft guide is:

```text
planning/domain-draft-generation-guide.md
```

The current next step after the baseline is:

```text
Create / refine domain draft 1.
```

Do not use the old path as current workflow:

```text
scenario-domain-design-input-core.md
-> domain-discovery-core.md
```

Do not use competing-domain-variant comparison as the current workflow.

## 12. Current UI-Planning Step

The current UI-planning branch starts at:

```text
planning/ui/README.md
planning/ui/ui-planning-workflow.md
```

Current UI outputs:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```

The UI plan is textual page planning, not final visual design.
