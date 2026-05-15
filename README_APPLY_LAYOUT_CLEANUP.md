# enman client layout cleanup replacement archive

Extract this archive at the repository root.

It contains:
- new `energymanagement.client/src/shared/ui/layout/*` files;
- updated Home/Register/Login/Account page imports;
- updated `planning/slices/l1/L1-CLIENT-AUTH-SESSION-MIGRATION.client.md`;
- cleanup scripts to remove old `energymanagement.client/src/Components/Layout`.

## Apply

PowerShell from repository root:

```powershell
Expand-Archive .\enman-client-layout-cleanup-replacement.zip -DestinationPath . -Force
.\apply-client-layout-cleanup.ps1
```

Manual:
1. Extract this archive at repo root with overwrite enabled.
2. Delete `energymanagement.client/src/Components/Layout`.
3. Run checks.

## Checks

```powershell
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```

If E2E hits a transient dotnet DLL lock, close running dotnet/node processes and rerun.
