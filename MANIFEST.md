# MANIFEST — SL-APPL-002 Account Applicant Parties Read / Templates v3

## Package

Replacement archive for manual application to repository root.

Repository: `AlexPastukhh/enman`  
Branch target: `my-changes`  
Slice: `SL-APPL-002 — Account Applicant Parties Read / Templates`

This v3 package supersedes v2 by including generated artifacts produced by the repository workflow after the new endpoint was added.

## Added files

- `EnergyManagement.Server/L1/Application/Queries/L1GetAccountApplicantPartiesQuery.cs`
- `EnergyManagement.Server/L1/Application/Queries/L1GetAccountApplicantPartiesHandler.cs`

## Replaced files

- `EnergyManagement.Server/L1/Api/L1Dtos.cs`
- `EnergyManagement.Server/L1/Controllers/L1Controller.cs`
- `EnergyManagement.Server/L1/Application/Abstractions/IApplicantPartyRepository.cs`
- `EnergyManagement.Server/L1/Persistence/Repositories/ApplicantPartyRepository.cs`
- `Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs`

## Deleted files

- None.

## Generated artifacts

Included:

- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`

Reason:

- `GET /api/l1/applicant-parties` adds a new OpenAPI path.
- New schemas are generated for `L1AccountApplicantPartiesResponse` and `L1ApplicantPartySummaryDto`.
- `openapi-typescript` generates the matching TypeScript path, schemas and operation.

Generated files were not manually authored from scratch; they reflect the generated diff reported by the local repo workflow after applying the backend changes.

## Tests changed

- `Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs`

Added integration coverage for:

- unauthenticated `GET /api/l1/applicant-parties` returns `401`;
- authenticated account with no ApplicantParties returns `200` and empty `applicantParties`;
- first Individual ApplicantParty returns summary/card fields and `isCurrentDefault=true`;
- response JSON does not expose `clientAccountId`;
- multiple same-type ApplicantParties are all returned with only the first current/default;
- another account's ApplicantParties are not returned.

## API shape implemented

```http
GET /api/l1/applicant-parties
```

Response:

```ts
type L1AccountApplicantPartiesResponse = {
  applicantParties: L1ApplicantPartySummaryDto[];
};

type L1ApplicantPartySummaryDto = {
  applicantPartyId: number;
  applicantPartyType: string;
  displayName: string;
  fullName?: L1FullNameDto | null;
  email: string;
  phoneNumber: string;
  verificationStatus: string;
  isCurrentDefault: boolean;
  createdAt?: string | null;
};
```

Notes:

- API returns one flat `applicantParties` list.
- API does not return grouped `currentDefaults` / `otherApplicantParties` arrays.
- Client groups by `isCurrentDefault`.
- `isCurrentDefault` maps from persisted `ApplicantParty.IsCurrentActiveVersion`.
- Endpoint has no body/query input; no `422` request-shape validation is expected.

## Commands run and results

In this sandbox, these commands could not be executed because `dotnet`/`npm.cmd` are unavailable:

```text
dotnet build EnergyManagement.Server/EnergyManagement.Server.csproj -> dotnet: command not found
dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj -> dotnet: command not found
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json -> dotnet: command not found
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check -> dotnet: command not found
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check -> dotnet: command not found
npm.cmd run check:api -> npm.cmd: command not found
```

Local user workflow evidence after applying v2:

```text
npm run check:api
  check:openapi -> OpenAPI artifact is up to date
  generate:api-types -> generated TypeScript types
  git diff showed expected changes in Shared/openapi.json and generated openapi-types.ts
```

Those generated changes are now included in this v3 archive.

Recommended local verification after applying v3:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
npm run check:api
```

Expected: after v3 is applied, `npm run check:api` should no longer show the generated OpenAPI/type diffs that v2 missed. If it still changes files, keep the new generated diff and report it.

## Non-goals respected

- No GitHub push.
- No branch created.
- No commit created.
- No PR created.
- No planning docs changed.
- No domain files changed.
- No client UI changed.
- No make-default/current command implemented.
- No delete/archive behavior implemented.
- No edit ApplicantParty behavior implemented.
- No request creation changes made.
- No legacy cleanup performed.
- No FluentValidation cleanup performed.
- No handler/value-object cleanup performed.
- No `IsCurrentActiveVersion` rename performed.
- No separate My Applicant Parties page work performed.

## Handoff notes / risks

- The uploaded repository archive already contained the prerequisite default/current behavior: new ApplicantParty starts non-current by default, and `ApplicantPartyCreationService` marks first-of-type as current/default. This package only reads the persisted marker.
- Generated constants were not observed changing from the user-provided `check:api` output. If `generate-client-constants --check` changes additional files locally, include those generated files as generated artifacts.
- This package is merge-ready: paths are repo-relative directly, with no wrapper folder.
