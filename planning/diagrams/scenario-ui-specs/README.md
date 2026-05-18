# Scenario UI Specs

Status: canonical scenario-level UI requirements source / UI template and readiness synchronized

This folder owns scenario-level UI requirements.

UI scenario specs describe visible user outcomes and accepted UI behavior. They do not define React component placement, query keys, CSS file ownership or HTTP wrapper implementation.

## Current rule

```text
[UI-SCENARIO] = scenario-level UI source:
  user goal
  screen entry points
  screen composition
  visible data
  actions
  visible states
  actor-specific differences
  validation/feedback requirements
  accessibility notes

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
- UI specs define visible outcomes, not React implementation details.
- E2E asserts visible user outcome, not cache/refetch mechanics.
- Client-side route/query ownership belongs to client sidecars unless needed to describe visible URL behavior.
- Before updating a client slice draft, check whether its UI scenario source exists and is complete enough.
- If UI scenario source is missing or partial, update/add the UI scenario first.
```
