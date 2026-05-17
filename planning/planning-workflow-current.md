# Current Planning Workflow

Status: current / L1 baseline and near-final L2 Employee Review + Agreement Exchange planning synchronized

Always separate current implementation from target scenario direction.

## 1. Current Implementation Baseline

Known L1 current repo evidence includes:

```text
- L1 backend register/login/current-user/logout;
- create individual ApplicantParty;
- account ApplicantParties flat-list read endpoint;
- explicit make ApplicantParty current/default backend command;
- old current-individual ApplicantParty read compatibility endpoint;
- create connection request using Existing/New applicant context;
- My Requests list endpoint with status filter;
- Own Request Details endpoint;
- /requests/create client route and form;
- My Requests list/filter/details client UI;
- OpenAPI artifact and generated TypeScript types.
```

Use:

```text
planning/l1-current-implementation-status.md
```

For L2 implementation status, inspect GitHub/current branch directly. Do not infer implementation status from slice drafts or archives.

## 2. Current Target Direction

```text
ApplicantParty:
  one Applicant Parties page / section is the planning model;
  flat account read model is implemented on backend;
  explicit make default/current backend command is implemented;
  client make default/current action remains to implement;
  old AccountPage single-current UI still needs replacement by target page/section if not already done;
  current/default is prefill/default selection only;
  existing requests are not changed by ApplicantParty creation or default/current changes.

Request creation:
  explicit Existing/New applicant context is implemented on backend;
  /requests/create client route/form is implemented;
  success hands off to My Requests because command success has no required requestId body.

My Requests:
  list, filters and details are implemented first-stage client/server flows.

L2 Employee/Review/Agreement:
  planning is nearly complete for the current L2 cut;
  current source navigation is in planning/l2-current-planning-status.md;
  implementation status must be checked from GitHub/current branch.
```

## 3. Remaining L1 Finish Items

Before moving fully to the next domain cut, finish or explicitly defer:

```text
1. SL-APPL-003.client make default/current button/action.
2. Applicant Parties page/section replacement for old current-individual AccountPage UI.
3. make-current-default API integration tests / coverage confirmation.
4. Compatibility decision for current-individual endpoint after target page replacement.
```

Future but not current L1 finish:

```text
- ApplicantParty delete/archive/edit lifecycle;
- multi-type ApplicantParty creation;
- larger new domain scenario/draft.
```

## 4. L2 Planning Status

The current L2 planning cut is nearly complete for:

```text
- Employee request dashboard/details reads;
- StartReview / ApproveReview / RejectReview;
- AgreementProposalExchange start/counter-proposal/list/details/accept/final-refuse;
- AgreementDocumentRef metadata references;
- client sidecars for Employee Review and Agreement Exchange;
- diagram prompt workflow and three-part diagram generation plan.
```

Use the canonical L2 files:

```text
planning/l2-current-planning-status.md
planning/slices/l2/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-clarifications/README.md
```

## 5. Slice Drafting Rules

```text
Behavior Coverage is not Test Coverage.
Scenario Flow and Behavior Items come from source files via slice-scenario-flow-behavior-register.md.
Implementation details are not behavior items.
Scenario Flow is not Implementation Flow.
E2E asserts visible state/outcome, not refetch mechanics or backend internals.
Server API slices must consider request-level FluentValidation separately from application/domain validation.
Every non-trivial slice draft must include Scope, Out of scope, Related slices / owners and Future extension points.
Every full server/client draft must include Implementation Checklist near the end.
```

## 6. Agent Scope Rules

Implementation prompts must include explicit scope boundaries:

```text
- can change code in listed implementation area;
- cannot change Domain.EnergyManagement unless explicitly in scope;
- cannot change planning docs unless explicitly in scope;
- cannot update generated artifacts unless explicitly asked or generation is part of the implementation task;
- may read docs freely for context;
- must preserve Scope / Out of scope / Related slices / Future extension points from the slice draft.
```

Current-state prompts must instruct agents to inspect GitHub/current branch, not archives.
