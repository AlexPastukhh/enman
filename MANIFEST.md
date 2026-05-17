# SL-APPL-003 — Select Current/Default ApplicantParty Template

Archive type: complete repo-relative replacement files.

## Added files

- `EnergyManagement.Server/L1/Application/Commands/L1MakeApplicantPartyCurrentDefaultHandler.cs`

## Replaced files

- `EnergyManagement.Server/L1/Application/Commands/L1Commands.cs`
- `EnergyManagement.Server/L1/Controllers/L1Controller.cs`
- `Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs`
- `Tests.EnergyManagement/Integration/L1/ApplicantParties/L1ApplicantPartiesIntegrationTests.cs`

## Deleted files

- None.

## Generated artifacts

Not included.

Reason: this sandbox does not have the .NET SDK and cannot run `EnergyManagement.Tools`. Do not manually edit generated OpenAPI/client artifacts. After applying the archive locally, run the repo generation workflow:

```powershell
npm run generate:openapi
npm run generate:api-types
git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts
npm run check:api
```

Expected generated artifacts after local generation:

- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`

## Implementation summary

- Added `POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default`.
- Added `L1MakeApplicantPartyCurrentDefaultCommand`.
- Added handler that:
  - verifies selected ApplicantParty ownership through `GetOwnedByIdAsync`;
  - loads current account ApplicantParties;
  - unsets current/default for previous same-type ApplicantParties;
  - marks the selected ApplicantParty current/default;
  - leaves existing requests untouched;
  - commits through `L1DbContext.SaveChangesAsync`.
- Missing/not-owned selected ApplicantParty returns the existing L1 validation problem shape (`422`) using `l1.applicant.party.is.required`.
- Success returns `200 OK` with no required response body.

## Tests changed

Updated `Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs`:

- Added overload for creating ApplicantParty with custom DTO.
- Added helpers for calling make-current-default endpoint.

Updated `Tests.EnergyManagement/Integration/L1/ApplicantParties/L1ApplicantPartiesIntegrationTests.cs` with API integration + DB state assertions:

- unauthenticated request returns 401;
- missing ApplicantParty returns validation problem and does not change default state;
- not-owned ApplicantParty returns validation problem and does not change owner/other account default state;
- owned non-default ApplicantParty becomes current/default and previous same-type default is unset;
- selecting already current/default is idempotent;
- existing request row remains unchanged after default switch.

No mock assertions were added.

## Commands run in sandbox

```text
$ dotnet build EnergyManagement.Server/EnergyManagement.Server.csproj
bash: dotnet: command not found
exit=127

$ dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj
bash: dotnet: command not found
exit=127

$ npm run generate:openapi
sh: 1: dotnet: not found
exit=127

$ npm run generate:api-types
sh: 1: openapi-typescript: not found
exit=127

$ npm run check:api
sh: 1: dotnet: not found
exit=127
```

## Non-goals respected

- No planning docs changed.
- No domain files changed.
- No client UI changed.
- No delete/archive/edit lifecycle implemented.
- No request creation behavior changed.
- No `IsCurrentActiveVersion` rename.
- No generated artifacts manually edited.
- No GitHub write performed.
- No unrelated cleanup.

## Risks / handoff notes

- Build/tests/generation must be run locally because this sandbox has no .NET SDK and no installed client npm dependencies.
- The command currently uses the existing L1 validation problem convention (`422`) for missing/not-owned ApplicantParty. If the API convention changes to 404/403 for route-owned commands, update controller/handler tests together.
- Multi-type preservation is implemented type-aware in handler logic, but current integration coverage only uses `IndividualApplicantParty` because this repo currently exposes Individual creation in L1.
