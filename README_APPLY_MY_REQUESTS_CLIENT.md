# Apply My Requests Client Slice

From repository root:

```powershell
Expand-Archive "C:\Users\alexa\Downloads\enman-my-requests-client-replacement.zip" -DestinationPath . -Force
```

The archive contains full replacement/addition files for the client My Requests list slice.

Run checks:

```powershell
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```

Scope included:

```text
- /requests route
- shared L1 request API wrapper
- entities/request query layer
- My Requests page
- list/empty/error/loading UI
- authenticated header link
- component test for list rendering
- E2E happy path for created request in My Requests
```

Out of scope:

```text
- status filtering UI
- request details page
- request creation UI
```
