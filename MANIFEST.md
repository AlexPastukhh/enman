# MANIFEST — SL-APPL-002 Account Applicant Parties Read / Templates

Archive: `sl-appl-002-account-applicant-parties-read-v4.zip`

## Added files

- `EnergyManagement.Server/L1/Application/Queries/L1GetAccountApplicantPartiesQuery.cs`
- `EnergyManagement.Server/L1/Application/Queries/L1GetAccountApplicantPartiesHandler.cs`

## Replaced files

- `EnergyManagement.Server/L1/Api/L1Dtos.cs`
- `EnergyManagement.Server/L1/Controllers/L1Controller.cs`
- `EnergyManagement.Server/L1/Application/Abstractions/IApplicantPartyRepository.cs`
- `EnergyManagement.Server/L1/Persistence/Repositories/ApplicantPartyRepository.cs`
- `Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs`

## Generated artifacts included

- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`

These generated artifacts include:

- `GET /api/l1/applicant-parties`
- `L1AccountApplicantPartiesResponse`
- `L1ApplicantPartySummaryDto`
- `L1ListAccountApplicantParties`

`v4` also fixes the generated-file drift observed locally:
- OpenAPI media type keys use the local generator output form `application/*\u002Bjson`.
- TypeScript generated types include the `L1ApplicantPartySummaryDto` schema block.

## Tests changed

- `Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs`

Added integration coverage for:

- unauthenticated `GET /api/l1/applicant-parties` returns `401`;
- authenticated account with no ApplicantParties returns `200` and `applicantParties: []`;
- authenticated account with one ApplicantParty returns a flat summary list;
- response does not expose `clientAccountId`;
- multiple same-type ApplicantParties are all returned with first default `true` and second default `false`;
- another account's ApplicantParties are excluded.

## Commands run here

The local sandbox used to create this archive still has no .NET SDK, so I could not execute project build/test/generation locally.

## User-provided local verification evidence

The user's local run showed:

- `dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check` reports OpenAPI up to date after local generation.
- `openapi-typescript` runs successfully.
- Remaining `git diff` was generated-artifact drift only.

## Commands to run after applying

From repo root:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out .\Shared\openapi.json --check
npm --prefix .\energymanagement.client run generate:api-types
```

Because `npm run check:api` ends with `git diff --exit-code Shared/openapi.json energymanagement.client/src/shared/api/generated/openapi-types.ts`, it checks for **unstaged generated-file drift**. If the generated artifacts are intentionally changed by this slice, stage them before running the script:

```powershell
git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts
npm run check:api
```

If `npm run check:api` still prints a diff after staging generated artifacts, the generated files are still stale and should be regenerated again.

## Non-goals respected

- No domain changes.
- No planning docs changes.
- No client UI implementation.
- No make-default/current command.
- No delete/archive lifecycle.
- No request creation changes.
- No legacy cleanup.
- No unrelated cleanup.
- No GitHub write.
