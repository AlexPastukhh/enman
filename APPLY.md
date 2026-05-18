# Apply

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\home-flow-ui-fix.zip" -DestinationPath "." -Force
```

Run client dev server:

```powershell
npm --prefix .\energymanagement.client run dev
```

Open:

```text
https://localhost:5173/
```

Useful routes:

```text
Client:
  https://localhost:5173/requests/create
  https://localhost:5173/requests
  https://localhost:5173/agreements

Employee:
  https://localhost:5173/employee/requests
  https://localhost:5173/employee/agreements
```
