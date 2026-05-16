# Apply request client test fixes

This archive replaces the request client tests that had strict locator issues after adding My Requests details and filters.

## Changes

- Fixes duplicate `Rejected` assertion in `MyRequestDetailsView.test.tsx`.
- Wraps `MyRequestsList.test.tsx` in `MemoryRouter` because request cards render details links.
- Uses exact object address text in E2E instead of broad `/Заринск/`, which also matches the site header.
- Scopes reset-filter button locators so Playwright does not match both the filter-panel reset and filtered-empty-state reset.
- Makes the filtered empty-state E2E actually click reset and assert return to `/requests`.

## Apply

From repo root:

```powershell
Expand-Archive "C:\Users\alexa\Downloads\enman-request-client-test-fixes.zip" -DestinationPath . -Force
```

## Verify

```powershell
npm.cmd --prefix energymanagement.client run test
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```
