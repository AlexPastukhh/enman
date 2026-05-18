# Pre-Release Archive Check

Archive: `sl-appl-ver-001-server-implementation-v1.zip`

## File scope checks

- [x] Server/API DTO updated: `EnergyManagement.Server/L1/Api/L1Dtos.cs`
- [x] Application command/result/status updated: `EnergyManagement.Server/L1/Application/Commands/L1Commands.cs`
- [x] New handler added: `EnergyManagement.Server/L1/Application/Commands/RunApplicantPartyVerificationFromRequestHandler.cs`
- [x] New mock service abstraction added: `EnergyManagement.Server/L1/Application/Abstractions/IApplicantPartyMockVerificationService.cs`
- [x] New deterministic mock service added: `EnergyManagement.Server/L1/Application/Services/MockApplicantPartyVerificationService.cs`
- [x] Controller endpoint added: `EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs`
- [x] DI registration added: `EnergyManagement.Server/Program.cs`
- [x] Integration tests added: `Tests.EnergyManagement/Integration/L1/EmployeeRequests/RunApplicantPartyVerificationIntegrationTests.cs`

## Guardrail checks

- [x] No files under `Domain.EnergyManagement/` are included.
- [x] No files under `energymanagement.client/` are included.
- [x] No generated artifacts are included.
- [x] No planning docs are included.
- [x] No migrations are included.
- [x] Endpoint accepts `requestId` route only; no `applicantPartyId` request input.
- [x] Command returns deterministic mock `Passed` result.
- [x] Handler mutates ApplicantParty only through `ApplicantParty.MarkVerified()`.
- [x] No Failed / Unavailable persisted states are added.
- [x] ApproveReview / RejectReview behavior is not changed.

## Original backup checks

- [x] Original snapshot saved for `EnergyManagement.Server/L1/Api/L1Dtos.cs`.
- [x] Original snapshot saved for `EnergyManagement.Server/L1/Application/Commands/L1Commands.cs`.
- [x] Original snapshot saved for `EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs`.
- [x] Original snapshot saved for `EnergyManagement.Server/Program.cs`.
- [x] New files are listed in `NEW-FILES.md`.

## Local command checks

- [!] `dotnet build EnergyManagement.sln --no-restore` was attempted but could not run in this environment: `dotnet: command not found`.
- [!] Integration tests were added but not executed here because the .NET SDK is unavailable in this environment.

## Manual syntax/scope checks

- [x] Brace/parenthesis balance checked for edited/new C# files.
- [x] Diff reviewed against the uploaded source zip.
- [x] Archive contents checked after zip creation.
