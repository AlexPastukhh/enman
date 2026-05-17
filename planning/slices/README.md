# Slice Planning Index

Status: current slice-planning navigation index / cross-cutting concerns drafting checklist synchronized

## 1. Core Rule

Scenario Flow and Behavior Items come from:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Do not use questions/extension/implementation registers or domain drafts as direct behavior source.

Implementation prompts must respect:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

All non-trivial slice drafts must include:

```text
Cross-Cutting Concerns / Considerations
```

Use:

```text
planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md
```

Client drafters must use the canonical short-draft shape:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
```

Server/backend state-changing slice drafts must separate API boundary tests, DB state transition tests, no-mutation tests, regression guards and what-not-to-test. Use:

```text
planning/testing/server-slice-test-plan-rules.md
```

## 2. Current / Active Backend Slice Files

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

## 3. Current / Active Client Sidecars

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-AUTH-003-logout.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.client.md
planning/slices/SL-REQ-001-create-connection-request.client.md
planning/slices/l1/L1-MY-REQUESTS-READ-LIST.client.md
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

## 4. Cross-Cutting / Helper Slices

```text
planning/slices/cross-cutting/README.md
planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 5. Drafting Rules

```text
- Draft by examples, not by improvisation.
- Scenario Flow is user/system behavior from scenario sources.
- Implementation Flow is code/layer responsibility.
- Behavior items are not implementation details.
- Cross-cutting concerns belong in their own considerations section unless source behavior says otherwise.
- Read-only UI belongs in entities.
- Command/user-action UI belongs in features.
- One draft covers one slice; extension slices are named but not implemented.
```

## 6. API / Generated Contract Rules

For API-changing slices, read:

```text
planning/api/openapi-contract-generation.md
planning/api/generated-artifact-check-workflow.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

Generated artifacts must come from repo commands, not manual edits.

## 7. Registers

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```
