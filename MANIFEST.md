# MANIFEST — Full Slices + Server Test Draft Rules Sync

Archive: `full-slices-server-test-rules-sync.zip`  
Scope: documentation-only planning update

## Add

```text
planning/slices/SL-REQ-001-create-connection-request.client.md
planning/testing/server-slice-test-plan-rules.md
```

## Replace

```text
planning/client/README.md
planning/slices/README.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/testing/README.md
```

## Carried forward from previous client/OpenAPI docs sync

These files remain in the archive because the package is based on the previous corrected docs-sync baseline and keeps those replacements available together with the new full slices:

```text
planning/api/README.md
planning/api/generated-artifact-check-workflow.md
planning/api/openapi-contract-generation.md
planning/client/client-layering-for-read-and-command-slices.md
planning/slices/SL-APPL-002-account-applicant-parties-read.client.md
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/l1/README.md
planning/slices/l1/L1-MY-REQUESTS-READ-LIST.client.md
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

## Delete

```text
none
```

## Non-goals

```text
- no backend/client/runtime code changes;
- no test code changes;
- no generated artifacts;
- no manual OpenAPI/type edits;
- no GitHub branch/commit/PR;
- no ApplicantParty delete/archive/edit lifecycle behavior.
```

## Source inputs

```text
- updated short SL-REQ-001.client draft with saved ApplicantParty selector in scope;
- updated short SL-APPL-003 server draft with separated API/DB/no-mutation/regression test plan;
- test-plan discussion emphasizing DB state assertions and no mocks as primary proof.
```
