# Apply L1 applicant party client replacement

This archive adds the client-side create individual applicant party slice.

## Apply

From repository root:

```powershell
Expand-Archive "C:\Users\alexa\Downloads\enman-applicant-party-client-replacement.zip" -DestinationPath . -Force
powershell -ExecutionPolicy Bypass -File .\apply-applicant-party-client.ps1
```

Or on bash:

```bash
unzip -o ~/Downloads/enman-applicant-party-client-replacement.zip -d .
bash ./apply-applicant-party-client.sh
```

## Expected changes

- Adds `shared/api/l1ApplicantPartyApi.ts`.
- Adds applicant party create feature under `features/applicant-party/create-individual`.
- Updates Account page to render the feature for authenticated users.
- Adds `/api/l1/applicant-parties/individual` to typed L1 API paths.
- Adds an E2E happy-path test for applicant party creation.
- Adds short client sidecar draft.

## Verify

```powershell
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```

This replacement does not implement current applicant party read state after refresh. That remains the future `L1-APPLICANT-PARTY-READ-CURRENT` slice.
