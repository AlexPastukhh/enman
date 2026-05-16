# Slice Planning Index

Status: current slice-planning navigation index / applicant-template-per-type synchronized

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

## 3. ApplicantParty Target Direction

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

## 4. Registers

```text
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```
