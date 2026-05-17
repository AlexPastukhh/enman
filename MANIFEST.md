# MANIFEST — SL-REQ-001.client Create Connection Request UI Archive

Archive type: runtime-code client/non-server implementation archive  
Slice: `SL-REQ-001.client — Create Connection Request UI with Applicant Context`

## Summary

Implements client-only request creation UI with explicit applicant context:

- `/requests/create` route and page.
- Existing/New applicant context form.
- Saved ApplicantParty selector for Existing branch.
- Current/default ApplicantParty is initially selected when available.
- Non-default saved ApplicantParty can be selected.
- New applicant data branch is submitted as part of the request journey.
- One client command posts to `POST /api/l1/requests`.
- Success hands off to My Requests.
- Read dependencies use existing ApplicantParty entity query from `SL-APPL-002.client`.

## Added files

```text
energymanagement.client/src/features/request/create-connection-request/api/createConnectionRequest.ts
energymanagement.client/src/features/request/create-connection-request/model/buildCreateConnectionRequestDto.test.ts
energymanagement.client/src/features/request/create-connection-request/model/buildCreateConnectionRequestDto.ts
energymanagement.client/src/features/request/create-connection-request/model/createConnectionRequestFieldMap.ts
energymanagement.client/src/features/request/create-connection-request/model/createConnectionRequestTypes.ts
energymanagement.client/src/features/request/create-connection-request/model/useCreateConnectionRequestForm.ts
energymanagement.client/src/features/request/create-connection-request/ui/ApplicantContextSection.tsx
energymanagement.client/src/features/request/create-connection-request/ui/CreateConnectionRequestForm.test.tsx
energymanagement.client/src/features/request/create-connection-request/ui/CreateConnectionRequestForm.tsx
energymanagement.client/src/features/request/create-connection-request/ui/ExistingApplicantSelector.tsx
energymanagement.client/src/features/request/create-connection-request/ui/FormErrorMessage.tsx
energymanagement.client/src/features/request/create-connection-request/ui/NewApplicantFields.tsx
energymanagement.client/src/features/request/create-connection-request/ui/ObjectAddressFields.tsx
energymanagement.client/src/features/request/create-connection-request/ui/RequestDetailsFields.tsx
energymanagement.client/src/features/request/create-connection-request/ui/TextAreaField.tsx
energymanagement.client/src/features/request/create-connection-request/ui/TextInputField.tsx
energymanagement.client/src/features/request/create-connection-request/ui/createConnectionRequest.css
energymanagement.client/src/features/request/create-connection-request/ui/createConnectionRequestConst.ts
energymanagement.client/src/pages/requests/create/CreateConnectionRequestPage.tsx
energymanagement.client/src/pages/requests/create/createConnectionRequestPage.css
energymanagement.client/src/shared/api/l1RequestApi.test.ts
tests/e2e/requests/create-connection-request.spec.ts
```

## Replaced files

```text
energymanagement.client/src/app/router/router.tsx
energymanagement.client/src/features/request/my-requests-list/ui/myRequestsConst.ts
energymanagement.client/src/pages/requests/my/MyRequestsPage.tsx
energymanagement.client/src/shared/api/l1RequestApi.ts
energymanagement.client/src/shared/config/clientRoutes.ts
```

## Deleted files

```text
None.
```

## Generated artifacts

```text
None changed.
```

No manual edits were made to:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
Shared/constants.json
Shared/errorcodes.json
```

## Tests changed / added

```text
energymanagement.client/src/features/request/create-connection-request/model/buildCreateConnectionRequestDto.test.ts
energymanagement.client/src/features/request/create-connection-request/ui/CreateConnectionRequestForm.test.tsx
energymanagement.client/src/shared/api/l1RequestApi.test.ts
tests/e2e/requests/create-connection-request.spec.ts
```

## Commands run and results

```text
npm install
Result: success

npm --prefix ./energymanagement.client install
Result: success

npm --prefix ./energymanagement.client run build
Result: success

npm --prefix ./energymanagement.client run test -- --run --reporter=dot
Result: success — 15 test files passed, 89 tests passed

npm --prefix ./energymanagement.client run lint
Result: failed on pre-existing react-refresh/only-export-components errors in existing files:
- energymanagement.client/src/Tests/ComponentTest/TestClasses/TestSetup.tsx
- energymanagement.client/src/app/router/router.tsx
- energymanagement.client/src/entities/session/model/SessionProvider.tsx
- energymanagement.client/src/shared/errors/pageErrorContext.tsx

npm run check:api
Result: not runnable in sandbox — dotnet executable is unavailable (sh: 1: dotnet: not found)

npm run test:e2e
Result: not run in sandbox — requires dotnet/localdb test environment
```

## Non-goals respected

```text
- no server/backend changes;
- no Domain.EnergyManagement changes;
- no planning docs changes;
- no generated artifact changes;
- no database/migration changes;
- no make-default/current behavior;
- no ApplicantParty delete/archive/edit behavior;
- no ApplicantParty management page behavior;
- no direct request-details navigation requiring requestId response;
- no GitHub branch/commit/PR/write.
```

## Risks / handoff notes

```text
- This archive assumes SL-APPL-002.client read foundation is already applied because request creation page uses useAccountApplicantPartiesQuery().
- E2E was added but not executed in the sandbox because dotnet/localdb tooling is unavailable here.
- Lint still fails on existing react-refresh export-rule issues outside this slice; this archive does not fix cross-cutting lint cleanup.
- The Existing branch sends no newApplicantParty property because generated TypeScript type does not allow null for that property. Server validation should accept omitted branch payload for Existing according to generated contract and backend behavior.
```
