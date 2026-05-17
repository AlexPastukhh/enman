# SL-APPL-003.client make current/default archive

Archive type: client-only runtime-code archive for `SL-APPL-003.client`.

## Added files

- `energymanagement.client/src/features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.ts`
- `energymanagement.client/src/features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.test.ts`
- `energymanagement.client/src/features/applicant-party/make-current-default/model/useMakeApplicantPartyCurrentDefaultMutation.ts`
- `energymanagement.client/src/features/applicant-party/make-current-default/ui/MakeCurrentDefaultButton.tsx`
- `energymanagement.client/src/features/applicant-party/make-current-default/ui/MakeCurrentDefaultButton.test.tsx`
- `energymanagement.client/src/features/applicant-party/make-current-default/ui/makeCurrentDefaultButton.css`
- `energymanagement.client/src/features/applicant-party/make-current-default/ui/makeCurrentDefaultButtonConst.ts`
- `tests/e2e/applicant-parties/applicant-parties-make-current-default.spec.ts`

## Replaced files

- `energymanagement.client/src/shared/api/l1ApiPaths.ts`
- `energymanagement.client/src/shared/api/l1ApplicantPartyApi.ts`
- `energymanagement.client/src/shared/api/l1ApplicantPartyApi.test.ts`
- `energymanagement.client/src/entities/applicant-party/ui/ApplicantPartiesList.tsx`
- `energymanagement.client/src/entities/applicant-party/ui/ApplicantPartiesList.test.tsx`
- `energymanagement.client/src/entities/applicant-party/ui/ApplicantPartySummaryCard.tsx`
- `energymanagement.client/src/entities/applicant-party/ui/applicantPartiesList.css`
- `energymanagement.client/src/pages/account/AccountPage.tsx`
- `energymanagement.client/src/pages/account/AccountPage.test.tsx`

## Deleted files

None.

## Generated artifacts

Unchanged. This archive does not include generated artifacts.

## Tests changed or added

- `energymanagement.client/src/shared/api/l1ApplicantPartyApi.test.ts`
- `energymanagement.client/src/entities/applicant-party/ui/ApplicantPartiesList.test.tsx`
- `energymanagement.client/src/features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.test.ts`
- `energymanagement.client/src/features/applicant-party/make-current-default/ui/MakeCurrentDefaultButton.test.tsx`
- `energymanagement.client/src/pages/account/AccountPage.test.tsx`
- `tests/e2e/applicant-parties/applicant-parties-make-current-default.spec.ts`

## Commands run and results

- `npm install` — success.
- `npm --prefix ./energymanagement.client install` — success; npm reported existing audit vulnerabilities.
- `npm --prefix ./energymanagement.client run build` — success.
- `npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/shared/api/l1ApplicantPartyApi.test.ts src/entities/applicant-party/ui/ApplicantPartiesList.test.tsx src/features/applicant-party/make-current-default/ui/MakeCurrentDefaultButton.test.tsx src/features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.test.ts src/pages/account/AccountPage.test.tsx` — success: 5 files passed, 11 tests passed.
- `npm --prefix ./energymanagement.client run test -- --run --reporter=dot` — timed out in the sandbox after printing passing dots; targeted changed tests passed separately.
- `npm --prefix ./energymanagement.client run lint` — failed on pre-existing `react-refresh/only-export-components` errors outside this slice (`TestSetup.tsx`, `router.tsx`, `SessionProvider.tsx`, `pageErrorContext.tsx`).
- `npm run check:api` — failed because sandbox has no `dotnet` executable (`sh: 1: dotnet: not found`).

## Non-goals respected

- No server/backend changes.
- No `Domain.EnergyManagement` changes.
- No planning docs changes.
- No database/migration changes.
- No generated artifact changes.
- No manual generated artifact edits.
- No unrelated cleanup.
- No GitHub write, branch, commit, or PR.

## Risks / handoff notes

- Full E2E was not run in the sandbox because it requires dotnet/localdb test environment.
- `applicant-parties-make-current-default.spec.ts` should be run locally with `npm run test:e2e -- tests/e2e/applicant-parties/applicant-parties-make-current-default.spec.ts` and then full `npm run test:e2e`.
- The full Vitest command timed out in this sandbox; targeted changed tests passed.
- Lint failure is from pre-existing files outside this archive.
