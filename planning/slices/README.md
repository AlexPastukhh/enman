# Slice Planning Index

Status: current slice-planning navigation index / validation, My Requests filters and ApplicantParty one-page direction synchronized

## 1. Core Rule

Scenario Flow and Behavior Items come from:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Do not use questions/extension/implementation registers as behavior source.

Implementation prompts must respect:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

## 2. Current / Active Backend Slice Files

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REQ-002-my-requests-list.md
planning/slices/SL-REQ-003-own-request-details.md
```

## 3. Current / Active Client Sidecars

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/l1/L1-MY-REQUESTS-READ-LIST.client.md
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

L1 sidecar navigation:

```text
planning/slices/l1/README.md
```

## 4. ApplicantParty Target Direction

```text
- one Applicant Parties page / section is the planning model;
- do not split current docs into separate "Account page applicant section" and "My Applicant Parties page" user scenarios;
- top page area shows current/default ApplicantParty templates by applicant type;
- current/default templates are visually outlined/highlighted;
- below, the page shows other saved ApplicantParties that are not selected as current/default;
- the same page owns add ApplicantParty action/form;
- the same page will later own explicit make default/current action;
- many saved ApplicantParties can exist over time;
- one current/default template may exist per applicant type;
- current/default = prefill/default selection, not replacement;
- first of type may initialize default/current;
- additional same-type create does not switch default/current silently;
- explicit default switch is separate future slice;
- ApplicantParty creation is additive;
- existing requests are not changed by ApplicantParty create or default/current changes;
- request creation target uses Existing/New applicant context.
```

## 5. My Requests Target Direction

```text
- backend list and details read endpoints are implemented;
- client list UI is first-stage implemented;
- filters are a separate client sidecar with status as first filter;
- details page is a separate client sidecar;
- list/details/filter responsibilities should not be collapsed into one client file.
```

## 6. Server Validation Direction

Server/API slices with request body or query input must distinguish:

```text
FluentValidation request-boundary validation:
- DTO/query shape;
- required/nested fields;
- branch/discriminator rules;
- mutually exclusive fields;
- allowed query values.

Application/domain validation:
- account existence;
- ownership;
- transactions/no-write;
- domain invariants and state transitions.
```

Use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 7. Registers

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

## 8. Cross-Cutting / Helper Slices

```text
planning/slices/cross-cutting/README.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```
