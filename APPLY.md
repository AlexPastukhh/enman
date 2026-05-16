# Apply SL-APPL-002.client archive

Run from repository root.

## Apply

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-002-client-applicant-parties-read.zip" -DestinationPath "." -Force
```

This archive has repo-relative paths at zip root. It does not contain a wrapper folder.

## Verification commands

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run lint
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
npm run test:e2e
```

Notes:

```text
- check:api requires dotnet and git in the target repo.
- test:e2e requires the repo's local test DB environment.
- If lint still fails on react-refresh/only-export-components in unchanged files, handle that as a separate cleanup.
```
