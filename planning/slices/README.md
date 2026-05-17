# Slice Planning Index

Status: current slice-planning navigation index / L1 current implementation and remaining gaps synchronized

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

For current implementation status, read:

```text
planning/l1-current-implementation-status.md
```

## 2. Current / Active Backend Slice Files

Implemented/current backend slices:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-AUTH-001-login-client-account.md
planning/slices/SL-AUTH-002-current-user.md
planning/slices/SL-AUTH-003-logout.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REQ-002-my-requests-list.md
planning/slices/SL-REQ-003-own-request-details.md
```

Future backend slices:

```text
future ApplicantParty delete/archive/edit lifecycle slices
future LegalEntity / IndividualEntrepreneur ApplicantParty creation slices
```

## 3. Current / Active Client Sidecars

Implemented/current client sidecars:

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-AUTH-003-logout.client.md
planning/slices/SL-REQ-001-create-connection-request.client.md
planning/slices/l1/L1-MY-REQUESTS-READ-LIST.client.md
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

Implemented/partial or target client sidecars:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
```

Remaining likely client work:

```text
future SL-APPL-003.client — make ApplicantParty current/default button/action
Applicant Parties page/section replacement for old current-individual AccountPage model
```

## 4. Drafting Rules

```text
- Draft by examples, not by improvisation.
- Scenario Flow is user/system behavior from scenario sources.
- Implementation Flow is code/layer responsibility.
- Behavior items are not implementation details.
- Read-only UI belongs in entities.
- Command/user-action UI belongs in features.
- One draft covers one slice; extension slices are named but not implemented.
```

## 5. API / Generated Contract Rules

For API-changing slices, read:

```text
planning/api/openapi-contract-generation.md
planning/api/generated-artifact-check-workflow.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

Generated artifacts must come from repo commands, not manual edits.

## 6. Registers

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```
