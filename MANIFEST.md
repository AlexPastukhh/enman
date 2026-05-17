# SL-REQ-001.client E2E locator replacement archive

Archive type: client-only test replacement archive.

## Replaced files

- `tests/e2e/requests/create-connection-request.spec.ts`

## Added files

- `MANIFEST.md`
- `APPLY.md`

## Deleted files

None.

## Generated artifacts

None. No generated artifacts were changed.

## Tests changed

- `tests/e2e/requests/create-connection-request.spec.ts`
  - narrowed the saved ApplicantParty selector locator to `getByLabel("Saved Applicant Party", { exact: true })`.
  - fixes Playwright strict mode collision with the `Use saved Applicant Party` radio label.

## Commands run and results

Static verification only in archive workspace:

```text
unzip/list archive source file -> success
updated the two ambiguous locators -> success
verified replacement file contains exact label locators -> success
```

Runtime tests were not run in this sandbox for this replacement archive.

Recommended local verification after applying:

```powershell
npm run test:e2e -- tests/e2e/requests/create-connection-request.spec.ts
npm run test:e2e
```

## Non-goals respected

- no server/backend changes;
- no Domain.EnergyManagement changes;
- no planning docs changes;
- no generated artifact changes;
- no unrelated cleanup;
- no GitHub write;
- no manual generated artifact edits.

## Risks / handoff notes

This archive only replaces the E2E test file. It assumes the previous `SL-REQ-001.client` runtime archive has already been applied.
