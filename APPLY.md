# APPLY — SL-REQ-001.client Create Connection Request UI Archive

Run from repo root.

## 1. Confirm repo root

```powershell
Get-ChildItem .\EnergyManagement.sln, .\energymanagement.client\package.json, .\package.json
```

## 2. Apply archive

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-req-001-client-create-connection-request.zip" -DestinationPath "." -Force
```

This archive is merge-ready and has no wrapper folder.

## 3. Verification commands

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run lint
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
npm run test:e2e
```

Known handoff note: lint may still fail on existing `react-refresh/only-export-components` issues outside this slice until that cross-cutting lint cleanup is addressed.
