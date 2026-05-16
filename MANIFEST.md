# MANIFEST — SL-APPL-002 Account Applicant Parties Read / Templates

## Package

Replacement archive for manual application to repository root.

Repository: `AlexPastukhh/enman`  
Branch target: `my-changes`  
Slice: `SL-APPL-002 — Account Applicant Parties Read / Templates`

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

- None included.

Reason: this sandbox has no .NET SDK (`dotnet: command not found`), so the OpenAPI generation commands could not be executed. Generated artifacts were not manually edited.

After applying this archive in a local environment with the project toolchain, run the repository generation/check commands and include generated diffs if they appear:

```bash
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
npm.cmd run check:api
```

Expected generated artifacts if the workflow updates them:

- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`
- any generated route/constants files changed by the repo workflow

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

The required commands were attempted from repository root.

```text
$ dotnet build EnergyManagement.Server/EnergyManagement.Server.csproj
bash: dotnet: command not found
exit=127

$ dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj
bash: dotnet: command not found
exit=127

$ dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
bash: dotnet: command not found
exit=127

$ dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
bash: dotnet: command not found
exit=127

$ dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
bash: dotnet: command not found
exit=127

$ npm.cmd run check:api
bash: npm.cmd: command not found
exit=127
```

Additional local checks:

- Confirmed no `planning/**` files changed.
- Confirmed no `Domain.EnergyManagement/**` files changed.
- Confirmed package contains complete replacement files, not snippets/patches.

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

- OpenAPI/client generated artifacts are expected to need regeneration after applying this archive, but were not generated here because `dotnet` is unavailable in the sandbox.
- The uploaded repository archive already contained the prerequisite default/current behavior: new ApplicantParty starts non-current by default, and `ApplicantPartyCreationService` marks first-of-type as current/default. This package only reads the persisted marker.
- If local generation changes `Shared/openapi.json` or generated TypeScript/constants, include those generated files in a follow-up commit/package generated by the repo tooling.
