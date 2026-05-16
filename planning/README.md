# Planning Index

Status: current planning navigation index / ApplicantParty target-model sync note  
Scope: repository planning artifacts, read order, documentation governance and slice/client/diagram planning entry points

## 1. Current Active Planning Focus

L1 backend/API/client docs are actively reconciling current implementation with the target ApplicantParty scenario model.

Target ApplicantParty model:

```text
- many saved ApplicantParties over time;
- one current/default ApplicantParty template per applicant type;
- first ApplicantParty of a type may initialize default;
- adding another same-type ApplicantParty does not silently switch default;
- request creation with new applicant data creates ApplicantParty and request atomically in one server call.
```

Do not overclaim this as fully implemented until repo evidence confirms it.

Use current slice/scenario docs for exact status:

```text
planning/slices/README.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
planning/slices/SL-REQ-004-create-request-with-applicant-context.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
```

## 2. Current Read Order

Core docs:

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/replacement-file-generation-guide.md
```

Scenario docs:

```text
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-ui-specs/README.md
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/diagrams/scenario-questions-register.md
```

Slice docs:

```text
planning/slices/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
planning/slices/SL-REQ-004-create-request-with-applicant-context.md
```

API/client/testing docs:

```text
planning/api/README.md
planning/api/client-server-contract-principles.md
planning/client/README.md
planning/client/cross-cutting/README.md
planning/testing/README.md
```

## 3. Documentation Update Direction

Documentation-only updates use:

```text
planning/documentation/
planning/replacement-file-generation-guide.md
```

Rules:

```text
- check current repo state;
- update navigation/responsibility maps;
- synchronize local file changes with shared registers when needed;
- create archive packages for manual application;
- do not write to GitHub directly unless explicitly requested.
```

## 4. Draft-Driven Discovery Direction

All slice families use draft-driven discovery:

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
```

Before writing Scenario Flow / Behavior Coverage, use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

## 5. Local Questions / Shared Registers Direction

Local `Questions / Decisions` sections are required, but they are not enough.

Use:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Important open or future questions from implemented slices must remain visible in shared registers.

## 6. Diagram Generation Direction

Diagram-related planning uses:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

Target diagram artifact direction:

```text
- draw.io XML;
- preferably one multi-page `.drawio` diagram book;
- preflight before drawing;
- no implementation overclaim.
```
