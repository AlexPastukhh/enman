# SL-APPL-002.client — Account Applicant Parties Read / Templates

Archive type: client/non-server runtime-code archive  
Repository: AlexPastukhh/enman  
Target branch: my-changes  
Scope: L1 client read sidecar for account Applicant Parties flat-list read

## Summary

Implements the client read path for `GET /api/l1/applicant-parties`:

- adds shared API path/wrapper for account Applicant Parties read;
- adds ApplicantParty entity API/query/model helpers;
- adds read-only ApplicantParty display UI under `entities/applicant-party/ui`;
- updates Account page to render account Applicant Parties from the flat list read model;
- updates/adds component, shared API/entity and E2E tests for the read path.

The archive is merge-ready and contains repo-relative files directly, without a wrapper folder.

## Added files

```text
energymanagement.client/src/entities/applicant-party/api/listAccountApplicantParties.ts
energymanagement.client/src/entities/applicant-party/api/listAccountApplicantParties.test.ts
energymanagement.client/src/entities/applicant-party/model/groupAccountApplicantParties.ts
energymanagement.client/src/entities/applicant-party/model/groupAccountApplicantParties.test.ts
energymanagement.client/src/entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
energymanagement.client/src/entities/applicant-party/ui/ApplicantPartiesEmptyState.tsx
energymanagement.client/src/entities/applicant-party/ui/ApplicantPartiesList.tsx
energymanagement.client/src/entities/applicant-party/ui/ApplicantPartiesList.test.tsx
energymanagement.client/src/entities/applicant-party/ui/ApplicantPartySummaryCard.tsx
energymanagement.client/src/entities/applicant-party/ui/applicantPartiesList.css
energymanagement.client/src/entities/applicant-party/ui/applicantPartiesListConst.ts
energymanagement.client/src/entities/applicant-party/ui/formatApplicantParty.ts
tests/e2e/applicant-parties/applicant-parties-read.spec.ts
energymanagement.client/src/shared/api/l1ApplicantPartyApi.test.ts
```

## Replaced files

```text
energymanagement.client/src/shared/api/l1ApiPaths.ts
energymanagement.client/src/shared/api/l1ApplicantPartyApi.ts
energymanagement.client/src/entities/applicant-party/model/applicantPartyQueryKeys.ts
energymanagement.client/src/entities/applicant-party/model/applicantPartyTypes.ts
energymanagement.client/src/pages/account/AccountPage.tsx
energymanagement.client/src/pages/account/AccountPage.test.tsx
tests/e2e/applicant-party/create-individual.spec.ts
```

## Deleted files

```text
None.
```

## Generated artifacts

```text
No generated artifacts changed.
Shared/openapi.json was not included.
energymanagement.client/src/shared/api/generated/openapi-types.ts was not included.
No manual generated artifact edits were made.
```

## Tests changed/added

```text
Added:
- ApplicantPartiesList component tests
- groupAccountApplicantParties model tests
- listAccountApplicantParties entity API test
- l1ApplicantPartyApi account-list shared API test
- applicant-parties read E2E spec

Updated:
- AccountPage component tests for flat account Applicant Parties read model
- create-individual E2E synchronization from current-individual read to account Applicant Parties list read
```

## Commands run and results

```text
npm install
Result: success.

npm install (inside energymanagement.client)
Result: success; npm reported 8 audit vulnerabilities in existing dependency tree.

npm --prefix ./energymanagement.client run lint
Result: failed on pre-existing react-refresh/only-export-components errors in files not changed by this archive:
- energymanagement.client/src/Tests/ComponentTest/TestClasses/TestSetup.tsx
- energymanagement.client/src/app/router/router.tsx
- energymanagement.client/src/entities/session/model/SessionProvider.tsx
- energymanagement.client/src/shared/errors/pageErrorContext.tsx

npm --prefix ./energymanagement.client run build
Result: success; Vite emitted existing large chunk warning.

npm --prefix ./energymanagement.client run test -- --run
Result: success; 12 test files passed, 81 tests passed.

npm run check:api
Result: failed before API check because sandbox has no dotnet executable: `sh: 1: dotnet: not found`.

npm run test:e2e
Result: not run in sandbox because root E2E script requires dotnet/local test DB tooling.
```

## Non-goals respected

```text
- no server/backend code changes;
- no Domain.EnergyManagement changes;
- no planning docs changes;
- no database/migration changes;
- no manual OpenAPI/generated TypeScript edits;
- no GitHub write;
- no branch/commit/PR;
- no make-default/delete/archive/request-creation behavior;
- no unrelated cleanup.
```

## Risks / handoff notes

```text
- AccountPage now reads Applicant Parties through the flat account list model and still composes the existing create form as a neighboring existing behavior.
- The create-individual E2E was updated to synchronize on GET /api/l1/applicant-parties because the page read model no longer needs GET /api/l1/applicant-parties/current-individual.
- Full E2E should be run on the target Windows/localdb environment after applying the archive.
- check:api should be re-run in the target repo where dotnet and git are available.
- Lint failure observed in sandbox is from pre-existing files outside this archive's changed files.
```
