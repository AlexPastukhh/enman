# Apply SL-APPL-003.client make current/default archive

From repo root, apply the archive directly into the repository:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-003-client-make-current-default.zip" -DestinationPath "." -Force
```

## Verify

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run lint
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
npm run test:e2e -- tests/e2e/applicant-parties/applicant-parties-make-current-default.spec.ts
npm run test:e2e
```
