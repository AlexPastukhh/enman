# MANIFEST — Client Draft + OpenAPI Workflow Rules Docs Sync v2

Archive: `client-draft-openapi-rules-docs-sync-v2.zip`  
Scope: documentation-only planning update for OpenAPI generated-artifact check workflow, client short-draft rules, client layering, and SL-APPL-002 client read sidecar example.

## Add

```text
planning/api/generated-artifact-check-workflow.md
planning/client/client-layering-for-read-and-command-slices.md
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
```

## Replace

```text
planning/api/README.md
planning/api/openapi-contract-generation.md
planning/client/README.md
planning/slices/README.md
planning/slices/l1/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/l1/L1-MY-REQUESTS-READ-LIST.client.md
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

## Delete

```text
none
```

## What v2 fixes relative to v1

```text
- Adds planning/api/README.md so the new generated-artifact workflow is discoverable from API navigation.
- Adds planning/slices/l1/README.md so L1 client sidecar navigation reflects normalized filters/details/read-list status.
- Adds/normalizes L1-MY-REQUESTS-LIST-FILTERS.client.md using the same Scenario Flow vs Implementation Flow discipline.
- Adjusts SL-APPL-002.client status: target backend contract accepted, but blocked until backend endpoint and generated types exist.
- Removes invented/overconfident generated operation-id assumptions from SL-APPL-002.client.
- Adds a copyable canonical short-draft example directly into client-slice-short-draft-rules-and-example.md.
```

## Main rules captured

```text
OpenAPI / generated artifacts:
- do not manually edit Shared/openapi.json or generated openapi-types.ts;
- after backend API contract changes, run generate:openapi and generate:api-types;
- stage generated artifacts before check:api in manual/archive workflow;
- check:api then verifies rerunning generation causes no additional working-tree diff.

Client drafting:
- drafters must copy existing/canonical examples, without inventing a new shape;
- Scenario Flow comes from slice-scenario-flow-behavior-register.md and the source scenario/UI/behavior files it points to;
- Scenario Flow is only the part of the scenario that belongs to this slice;
- Implementation Flow is page/entity/feature/shared/generated layering with files/functions/classes/types;
- implementation details are not behavior items;
- extension slices are named as related/out-of-scope/future owners, not implemented inside the current draft.

Client architecture:
- read/display UI belongs in entities/<entity>/ui;
- command/user-action UI belongs in features;
- shared/api is the low-level client/server boundary even when it contains wrappers for different entities;
- entity API/model layers expose domain-facing read operation names, query keys and query hooks.
```

## Not included

```text
- no runtime code;
- no tests;
- no generated artifacts;
- no GitHub write/branch/commit/PR;
- no backend implementation;
- no manual generated OpenAPI edits.
```
