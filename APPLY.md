# APPLY — SL-EMP-REQ-001 Employee Request List Read

Archive: `sl-emp-req-001-employee-request-list-read-v12.zip`

## PowerShell / VS Code Terminal

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-emp-req-001-employee-request-list-read-v12.zip" -DestinationPath . -Force
git status
git diff
```

## Git Bash / WSL

```bash
cd /c/enman/enman
unzip -o "/c/Users/alexa/Downloads/sl-emp-req-001-employee-request-list-read-v12.zip" -d .
git status
git diff
```

## Required checks after applying

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

Because this archive changes API contract, run generated artifact workflow locally:

```powershell
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd run check:api
```

If `check:api` reports expected API diffs for the new endpoint/DTOs, run the repo generation commands and include generated artifacts in a follow-up/staging step according to the project workflow.

## Deletions

None.

## Commit suggestion

```powershell
git add EnergyManagement.Server EnergyManagement.Testing Tests.EnergyManagement
git commit -m "Implement employee request list read endpoint"
```
