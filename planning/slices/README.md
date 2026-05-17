# Slice Planning Index

Status: current slice-planning navigation index / L2 Employee read, details and start-review sidecars synchronized

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

Client drafters must use the canonical short-draft shape:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
```

Server/backend state-changing slice drafts must separate API boundary tests, DB state transition tests, no-mutation tests, regression guards and what-not-to-test. Use:

```text
planning/testing/server-slice-test-plan-rules.md
```

All slice drafts must include cross-cutting concerns / considerations:

```text
planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md
```

## 2. Current / Active L1 Backend Slice Files

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

## 3. Current / Active L1 Client Sidecars

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

## 4. Current L2 Drafted Backend Slices

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md
```

## 5. Current L2 Drafted Client Sidecars

```text
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
```

## 6. L2 Navigation

```text
planning/slices/l2/README.md
```

## 7. Current L1 Remaining Gaps

```text
- SL-APPL-003.client runtime implementation if not already implemented.
- Final Applicant Parties page/section replacement of old current-individual AccountPage model if not already completed.
- Tests for make-current/default backend/client behavior should be verified.
- Delete/archive/edit lifecycle remains future.
- LegalEntity / IndividualEntrepreneur ApplicantParty creation remains future unless implementation proves otherwise.
```

## 8. L2 Recommended Next Drafts

```text
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review
L2-REVIEW-APPROVE-001.client — Approve Request Review
L2-REVIEW-REJECT-001.client — Reject Request Review
```

## 9. Drafting Rules

```text
- Draft by examples, not by improvisation.
- Scenario Flow is user/system behavior from scenario sources.
- Implementation Flow is code/layer responsibility.
- Behavior items are not implementation details.
- Read-only UI belongs in entities.
- Command/user-action UI belongs in features.
- One draft covers one slice; extension slices are named but not implemented.
- Cross-cutting concerns are considered in their own section; do not pollute Scenario Flow with implementation concerns.
```

## 10. API / Generated Contract Rules

For API-changing slices, read:

```text
planning/api/openapi-contract-generation.md
planning/api/generated-artifact-check-workflow.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

Generated artifacts must come from repo commands, not manual edits.

## 11. Registers

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```
