# Apply L2-EMP-DASH-001.client archive

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-emp-dash-001-client-employee-request-dashboard.zip" -DestinationPath "." -Force
```

Then verify:

```powershell
npm install
npm --prefix .\energymanagement.client install
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
npm --prefix .\energymanagement.client run lint
npm run check:api
```

E2E should wait until the Employee request list backend endpoint and Employee session test setup are available.
