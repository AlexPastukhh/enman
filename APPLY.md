# APPLY — Employee Details Client API Placement Full Sync

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\employee-details-client-api-placement-full-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

## Expected result

The Employee Request Details client sidecar is synchronized with the current API placement policy:

```text
- read endpoint wrapper lives in entities/employee-request/api;
- DTO aliases live near the entity;
- future review command wrappers live in features/employee-request/<action>/api;
- shared/api remains generic transport/generated infrastructure only;
- no shared/api/employeeRequestApi.ts is added by this read sidecar.
```

No code, tests or generated artifacts are included.
