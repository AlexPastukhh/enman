# Archive Plan: playwright-locator-cleanup-v1.4

Review folder: `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/`

## Purpose

Safely apply Playwright locator cleanup and E2E seed cleanup while preserving original versions of every replaced file.

## Scope

Replacement files:

| Target file | Change | Risk | Original snapshot |
|---|---|---|---|
| `EnergyManagement.Testing/TestDatabase/TestDatabaseManager.cs` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/EnergyManagement.Testing/TestDatabase/TestDatabaseManager.cs` |
| `EnergyManagement.Tools/TestDatabase/SeedE2eDemoDataCommand.cs` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/EnergyManagement.Tools/TestDatabase/SeedE2eDemoDataCommand.cs` |
| `tests/e2e/agreement-exchange/agreement-exchange-flow.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/agreement-exchange/agreement-exchange-flow.spec.ts` |
| `tests/e2e/applicant-parties/applicant-parties-make-current-default.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/applicant-parties/applicant-parties-make-current-default.spec.ts` |
| `tests/e2e/applicant-parties/applicant-parties-read.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/applicant-parties/applicant-parties-read.spec.ts` |
| `tests/e2e/applicant-party/create-individual.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/applicant-party/create-individual.spec.ts` |
| `tests/e2e/auth/login.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/auth/login.spec.ts` |
| `tests/e2e/auth/register.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/auth/register.spec.ts` |
| `tests/e2e/employee/employee-request-review.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/employee/employee-request-review.spec.ts` |
| `tests/e2e/pages/LoginPage.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/pages/LoginPage.ts` |
| `tests/e2e/pages/RegisterPage.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/pages/RegisterPage.ts` |
| `tests/e2e/requests/create-connection-request.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/requests/create-connection-request.spec.ts` |
| `tests/e2e/requests/my-request-details.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/requests/my-request-details.spec.ts` |
| `tests/e2e/requests/my-requests-filters.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/requests/my-requests-filters.spec.ts` |
| `tests/e2e/requests/my-requests.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/requests/my-requests.spec.ts` |
| `tests/e2e/support/locators.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/support/locators.ts` |
| `tests/e2e/vkr-screenshots/vkr-client-screenshots.spec.ts` | Replace with locator/test-tooling cleanup version | medium | `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/tests/e2e/vkr-screenshots/vkr-client-screenshots.spec.ts` |

## Explicit non-scope

- No production UI changes.
- No backend business logic changes.
- No SMTP/email runtime changes.
- No EF mapping or migration changes.
- No generated OpenAPI/types changes.

## Validation

Run the commands listed in `README.playwright-locator-cleanup-v1.4.md` after applying.
