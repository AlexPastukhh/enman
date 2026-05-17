# Apply L2 Employee request page placement sync

From the repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-employee-request-page-placement-sync.zip" -DestinationPath . -Force

.\APPLY-l2-employee-request-page-placement.ps1

git status
git diff -- energymanagement.client planning
```

Then run at least:

```powershell
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

This archive moves the Employee dashboard page files from:

```text
energymanagement.client/src/pages/employee/dashboard
```

to:

```text
energymanagement.client/src/pages/employee/requests/dashboard
```

Details already lives under:

```text
energymanagement.client/src/pages/employee/requests/details
```

No OpenAPI/generated artifacts are included because API shape does not change.
