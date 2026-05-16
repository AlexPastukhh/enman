# Slice Planning Index

Status: current slice-planning navigation index / applicant-template-per-type synchronized / server validation principles added

## 1. Core Rule

Scenario Flow and Behavior Items come from:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Do not use questions/extension/implementation registers as behavior source.

## 2. Current / Active Slice Files

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REQ-002-my-requests-list.md
planning/slices/SL-REQ-003-own-request-details.md
```

## 3. Cross-Cutting / Helper Slices

```text
planning/slices/cross-cutting/README.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 4. ApplicantParty Target Direction

```text
- many saved ApplicantParties;
- one current/default template per applicant type;
- current/default = prefill/default selection;
- first of type may initialize default;
- additional same-type create does not switch default;
- explicit default switch is separate slice;
- ApplicantParty creation is additive;
- request creation target uses Existing/New applicant context.
```

## 5. Server Validation Direction For Slice Drafts

When a backend/API slice has request body or query validation responsibility, read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

Slice drafts should include request-level FluentValidation in implementation flow for:

```text
- required DTO/query fields;
- discriminator/branch rules;
- mutually exclusive fields;
- basic DTO/query shape;
- allowed query values.
```

Do not confuse request validation with application/domain validation:

```text
- ownership;
- selected entity belongs to account;
- account/entity existence;
- domain invariants;
- no-write/atomicity.
```

## 6. Registers

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```
