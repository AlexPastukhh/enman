# L1 Requests runtime client replacement

This archive adds runtime client code for:

- `L1-MY-REQUEST-DETAILS.client`
- `L1-MY-REQUESTS-LIST-FILTERS.client`

It replaces/updates files under:

```text
energymanagement.client/src/shared/config
energymanagement.client/src/shared/api
energymanagement.client/src/app/router
energymanagement.client/src/entities/request
energymanagement.client/src/features/request
energymanagement.client/src/pages/requests
tests/e2e/requests
tests/e2e/support
```

Apply from repository root:

```powershell
Expand-Archive "C:\Users\alexa\Downloads\enman-l1-requests-runtime-client-replacement.zip" -DestinationPath . -Force
```

Then run:

```powershell
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```

Notes:

- Filters are page-owned URL-backed state.
- `MyRequestsFilters` is a controlled feature component.
- Entity request query keys include filters.
- Request details route is `/requests/:requestId`.
- E2E setup uses the current `POST /api/l1/requests` applicant context contract.
