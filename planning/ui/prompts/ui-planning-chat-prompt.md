# UI Planning Chat Prompt

Use this prompt for a dedicated UI planning chat.

```text
You continue work on repo AlexPastukhh/enman, branch `my-changes`.

Your role:
UI planning chat.

Your task:
Create / update textual UI planning artifacts for the Energy Management project.

Important:
You are not creating final visual design.
You are not creating React components.
You are not creating HTML prototype yet.
You are creating a textual test-site UI plan that checks whether current use cases can be served by pages, visible DATA, actions, status states, validation/error states and navigation.

Current UI branch:
scenario text specs
-> scenario DATA files
-> validation-related file
-> ui-planning-workflow.md
-> test-site-ui-plan.md
-> ui-questions-register.md
-> optional visual / HTML / React low-fidelity mockup later

Read:
- planning/README.md
- planning/planning-workflow-current.md
- planning/ui/README.md
- planning/ui/ui-planning-workflow.md
- planning/diagrams/scenario-text-specs/
- planning/diagrams/scenario-data/
- planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
- planning/tables/pre-domain-variants-input.md
- planning/diagrams/scenario-diagram-consistency-report.md
- planning/scenario-specification-principles.md

Do not use stale package summaries as semantic source of truth.

Active scenarios:
SC-01 Guest Registration
SC-02 Login
SC-03A Password Recovery Request
SC-03B Account Owner Verified / Password Reset Choice
SC-04 Client Request Creation
SC-05 My Requests / Own Request Details
SC-06 Employee Request Dashboard
SC-07A Employee Request Details
SC-07B Employee Request Review
SC-10 Applicant Data
SC-11 Request Documents
SC-13A My Agreements
SC-13B Agreement Proposal Details / Response
SC-13C Employee Agreements
SC-13D Employee Agreement Proposal Create / Send Version
SC-14 Client Data Verification
SC-15 Security Text Specification
SC-17 Anonymous Request

Merged / removed / deferred:
SC-08 Approved Result — merged
SC-09 Rejected Result — merged
SC-12 Review Feedback / Correction Navigation — merged
SC-16 Notification Navigation — removed as standalone
SC-18 Archive / Audit — deferred

Key decisions:
- Request statuses: InReview, Approved, Rejected.
- Do not use Submitted unless reintroduced later with precise meaning.
- Request object location = object address.
- Saved ApplicantData may be copied/prefilled into Request Creation.
- Request-local applicant data may differ from saved ApplicantData.
- Editing request-local fields does not mutate saved ApplicantData.
- Standalone ApplicantData editing does not trigger verification.
- Agreement Proposal = concrete agreement document/version sent by one side to the other side in the context of an Approved request.
- Core proposal statuses: AwaitingClientConfirmation, SentByClient, Accepted, Rejected.
- Agreement proposal exchange starts only by employee action on Approved request.
- Approval does not automatically create agreement proposal.
- Client cannot start exchange without employee-sent proposal.
- Client can send only one own proposal version in response in core.
- Employee responds to client-sent proposal by sending a new employee version.
- Previous client-sent proposal becomes Rejected when employee sends a new version.

Create/update:
- planning/ui/test-site-ui-plan.md
- planning/ui/ui-questions-register.md

In test-site-ui-plan.md include:
- purpose;
- source set;
- page inventory;
- scenario-to-page map;
- page plans;
- cross-page navigation;
- status-dependent UI states;
- validation/error/empty states;
- coverage gaps;
- UI questions summary.

For each page include:
- supported scenarios;
- actor;
- purpose;
- entry points / UX paths;
- visible DATA;
- inputs;
- actions;
- status-dependent UI;
- validation / error states;
- empty states;
- navigation;
- open UI questions.

In ui-questions-register.md include unresolved UI alternatives:
- id;
- status;
- affected scenarios;
- affected pages;
- question;
- options;
- current preference;
- what blocks final decision;
- notes.

Rules:
- Scenario specs own mandatory observable behavior.
- DATA files own what actor enters/sees/selects/filters/attaches/references.
- UI plan owns pages/actions/status states/navigation.
- UI questions register owns unresolved UI alternatives.
- If UI planning reveals missing mandatory observable behavior, propose update to scenario spec.
- If UI planning reveals missing visible/input/selectable DATA, propose update to DATA file.
- If the issue is layout/component/navigation style or one of several acceptable UX options, keep it in UI plan/questions register.
- Do not silently invent business behavior.
- Do not create final visual design.
- Do not create React/HTML prototype now.

Output:
If asked for files, generate complete replacement files, not patches.
Use repository-relative paths.
```
