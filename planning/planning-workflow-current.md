# Current Planning Workflow

Status: current / L1 implementation status and remaining L1 gaps synchronized

Always separate current implementation from target scenario direction.

## 1. Current Implementation Baseline

Known current repo evidence includes:

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

Server validation:
  FluentValidation owns L1 API request/query shape validation;
  application/domain validation still owns ownership, transactions and invariants.
```

## 3. Remaining L1 Finish Items

Before moving fully to a new domain draft, finish or explicitly defer:

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

## 4. Slice Drafting Rules

```text
Behavior Coverage is not Test Coverage.
Scenario Flow and Behavior Items come from source files via slice-scenario-flow-behavior-register.md.
Implementation details are not behavior items.
E2E asserts visible state/outcome, not refetch mechanics or backend internals.
Server API slices must consider request-level FluentValidation separately from application/domain validation.
Every non-trivial slice draft must include Scope, Out of scope, Related slices / owners and Future extension points.
```

## 5. Agent Scope Rules

Implementation prompts must include explicit scope boundaries:

```text
- can change code in listed implementation area;
- cannot change Domain.EnergyManagement unless explicitly in scope;
- cannot change planning docs unless explicitly in scope;
- cannot update generated artifacts unless explicitly asked or generation is part of the implementation task;
- may read docs freely for context;
- must preserve Scope / Out of scope / Related slices / Future extension points from the slice draft.
```
