# APPLY

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-start-client-sidecar-sync.zip" -DestinationPath . -Force
.\APPLY-l2-agr-exch-start-sidecar-sync.ps1
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

## Notes

This archive intentionally does not change runtime code, tests, OpenAPI or generated TypeScript types.

It adds the missing client sidecar for starting an Agreement Exchange from Employee request details and keeps the current server/client contract mismatch visible as a blocked OpenAPI question.
