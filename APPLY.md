# APPLY — L1 Backend Slice Documentation Status Reconciliation

Apply from repository root on branch `my-changes`.

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-l1-backend-slice-docs-status-reconciliation.zip" -DestinationPath . -Force
git status
```

After applying, review the three replaced planning files:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
```

Optional verification commands for the existing implementation/artifact baseline:

```powershell
dotnet test
npm run check:api
```

This archive is documentation-only. It does not contain backend code, generated artifacts, client sidecars, CSRF implementation, UI pages or database constraint changes.
