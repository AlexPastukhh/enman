# Current Planning Workflow

Status: current / ApplicantParty one-page direction, validation and My Requests filters synchronized

Always separate current implementation from target scenario direction.

## 1. Current Target Direction

```text
ApplicantParty:
  one Applicant Parties page / section is the planning model;
  do not split current docs into separate "Account page applicant section"
  and "My Applicant Parties page" user scenarios;
  top page area shows current/default ApplicantParty templates by applicant type;
  current/default templates are visually outlined/highlighted;
  below, the page shows other saved ApplicantParties that are not selected as current/default;
  the same page owns add ApplicantParty action/form;
  the same page will later own explicit make default/current action;
  many saved ApplicantParties can exist over time;
  one current/default template may exist per applicant type;
  current/default is prefill/default selection only;
  first of type may initialize default/current;
  additional same-type create does not switch default/current silently;
  create is additive, not replacement;
  existing requests are not changed by ApplicantParty creation or default/current changes.

Request creation:
  explicit Existing/New applicant context;
  Existing can use any owned saved ApplicantParty;
  New applicant + request is one atomic server operation.

My Requests:
  list, filters and details are separate slices/sidecars;
  status is the first filter entry in an extensible filter model.

Server validation:
  FluentValidation should become the request-boundary layer for L1 DTO/query shape;
  application/domain validation still owns ownership, transactions and invariants.

Backend cleanup:
  legacy runtime must not be copied as the architecture reference for new L1 work;
  classify legacy/current/shared surfaces before removal or handler cleanup;
  use planning/architecture/backend-legacy-and-l1-boundaries.md.
```

## 2. Current Implementation Baseline

Known current repo evidence includes:

```text
- L1 backend register/login/current-user/logout;
- create individual ApplicantParty;
- current individual ApplicantParty read;
- create connection request using current implemented server model;
- My Requests list endpoint;
- Own Request Details endpoint;
- OpenAPI artifact and generated TypeScript types;
- first-stage client My Requests list UI.
```

Current implementation may still contain narrow/current-active ApplicantParty names or older Account-page wording. When docs describe target planning, use the one Applicant Parties page / section direction above. When docs describe existing code, label it as current implementation evidence.

Client details and filters are still planned/implementation-ready sidecars unless current repo code proves otherwise.

L1 FluentValidation adoption is a planned/implementation-ready cross-cutting transition. Legacy controllers use manual FluentValidation, but L1 request DTO validation must be introduced deliberately.

## 3. Slice Drafting Rules

```text
Behavior Coverage is not Test Coverage.
Scenario Flow and Behavior Items come from source files via slice-scenario-flow-behavior-register.md.
ApplicantPartyId is API support, not behavior item.
E2E asserts visible state/outcome, not refetch mechanics or backend internals.
Server API slices must consider request-level FluentValidation separately from application/domain validation.
```

## 4. ApplicantParty Scenario Sync Rule

When updating ApplicantParty docs, keep these files synchronized:

```text
planning/README.md
planning/planning-workflow-current.md
planning/slices/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/client/README.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
```

Do not record the one-page-vs-two-page documentation correction as a user-visible behavior item. Put it in scenario rules, questions/registers and navigation/current-direction docs.

## 5. Agent Scope Rules

Implementation prompts must include explicit scope boundaries:

```text
- can change code in listed implementation area;
- cannot change Domain.EnergyManagement unless explicitly in scope;
- cannot change planning docs unless explicitly in scope;
- cannot update generated artifacts unless explicitly asked or generation is part of the implementation task;
- may read docs freely for context.
```

Use:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```
