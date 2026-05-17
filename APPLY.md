# Apply SL-REQ-001.client E2E locator replacement archive

Run from repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-req-001-client-e2e-locator-fix.zip" -DestinationPath "." -Force
```

Recommended verification:

```powershell
npm run test:e2e -- tests/e2e/requests/create-connection-request.spec.ts
npm run test:e2e
```

Optional client checks:

```powershell
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```
