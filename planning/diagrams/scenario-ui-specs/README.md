# Scenario UI Specs

Status: canonical scenario-level UI presentation / UX requirements source

This folder owns scenario-level UI presentation and UX requirements.

UI scenario specs describe how core scenario business behavior and DATA are presented to the user. They do not define business capability/content, React component placement, query keys, CSS file ownership or HTTP wrapper implementation.

## Current rule

```text
[CORE / BUSINESS SCENARIO] = business capability source:
  actor
  goal
  business flow
  business branches
  business outcomes

[DATA] = scenario information source:
  entered data
  visible data
  selected/referenced data
  attached/uploaded data
  result/feedback information

[UI-SCENARIO] = scenario-level presentation / UX source:
  screen entry points
  screen composition
  visual presentation of DATA
  available actions from user's point of view
  empty/loading/error/success states
  deferred validation and feedback
  accessibility notes
  mockup/layout/color/animation requirements when relevant

[CLIENT-SLICE] = client implementation translation:
  page/entity/widget/feature ownership
  API hooks/wrappers
  CSS ownership
  tests
  implementation checklist

[CLIENT-CONVENTION] = global client workflow:
  layering
  style/CSS rules
  validation workflow
  accessibility workflow
  handoff rules
```

UI scenarios must be consistent with core scenario and DATA. If a UI scenario appears to add business behavior, treat it as a consistency issue: correct it, record a question, or update the core scenario only through an accepted decision.

## Required UI scenario docs

```text
UI-SCENARIO-CONVENTIONS.md
UI-SCENARIO-TEMPLATE.md
UI-SCENARIO-READINESS.md
```

## Current UI scenario files

```text
APP-UI-001-app-shell-home-auth-flow-ui.md
SC-04-request-creation-ui.md
SC-05-my-requests-ui.md
SC-10-applicant-data-ui.md
SC-10B-my-applicant-parties-ui.md
```

## Important ApplicantParty decision

`SC-10B-my-applicant-parties-ui.md` is not the current canonical UI target as a separate page/scenario.

Current direction:

```text
Account page contains the "My Applicant Parties" section.

The Account Applicant Parties section owns:
  - showing saved ApplicantParties;
  - identifying current/default ApplicantParty per type;
  - add ApplicantParty action/form;
  - future edit/delete/archive/current actions when implemented.

Edit/delete/archive are not implemented now.
```

Use:

```text
SC-10-applicant-data-ui.md
```

as the canonical ApplicantParty UI scenario source.

## Rules

```text
- UI specs define presentation/UX outcomes, not React implementation details.
- UI specs present core scenario behavior and DATA; they do not create independent business behavior.
- E2E asserts visible user outcome, not cache/refetch mechanics.
- Client-side route/query ownership belongs to client sidecars unless needed to describe visible URL behavior.
- Before updating a client slice draft, check whether its UI scenario source exists and is complete enough.
- If UI scenario source is missing or partial, update/add the UI scenario first.
```
