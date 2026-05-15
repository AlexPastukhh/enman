# Architecture Decision Notes

Status: active accepted-decision registry  
Scope: current accepted architecture decisions that guide planning/implementation and may support diploma text

## 1. Testing / E2E Decisions

| ID | Decision | Current accepted direction | Rationale / trade-off | Affected artifacts | ADR promotion |
|---|---|---|---|---|---|
| ADN-063 | Testing responsibility split | Domain unit, server integration/API, client/component and E2E tests have separate responsibilities. | Prevents E2E from becoming slow duplicated component/server testing. | `planning/testing/testing-principles.md` | Candidate |
| ADN-064 | E2E scope | E2E verifies browser -> client -> HTTP API -> server/persistence/session -> visible outcome wiring, not exhaustive client-visible UI behavior. | Keeps E2E focused on cross-layer confidence; detailed UI remains in component tests. | `planning/testing/e2e-playwright-workflow.md` | Likely ADR |
| ADN-065 | Playwright explicit webServer startup | Playwright should explicitly start backend and frontend through webServer. | Removes manual startup ambiguity and makes E2E workflow reproducible. | Playwright config, testing workflow | Candidate |
| ADN-066 | E2E frontend-origin/proxy strategy | Browser opens frontend origin, client uses relative `/api`, Vite proxy forwards to backend. | Avoids browser CORS in dev E2E and matches current Vite setup. | Vite, Playwright, E2E docs | Candidate |
| ADN-067 | Locator strategy | Use role/label-first locators, native semantics first, explicit ARIA only when needed. | Improves accessibility, test readability and locator stability. | A11Y docs, E2E docs, client tests | Candidate |
| ADN-068 | Playwright exact matching policy | Use `{ exact: true }` or escaped anchored regex; do not build raw regex from UI text. | Playwright name matching defaults differ from Testing Library; raw regex can break literal text matching. | E2E helpers, locator docs | Candidate |
| ADN-069 | Page Object vs Component Object split | E2E Page Objects are thin/stateless by default and receive scenario data in action methods; Component Objects may expose detailed UI state helpers. | Keeps E2E readable as scenario flow and keeps UI-detail checks in component tests. | E2E tests, client/component tests | Candidate |
| ADN-070 | Current Playwright cleanup direction | Move to root Playwright config, root `tests/e2e`, webServer backend+frontend, unique data, simplified E2E assertions. | Aligns repo with E2E responsibility and existing root tests. | `planning/testing/playwright-e2e-cleanup-plan.md` | Candidate |

## 2. Cross-Cutting / Constants Decisions

| ID | Decision | Current accepted direction | Rationale / trade-off | Affected artifacts | ADR promotion |
|---|---|---|---|---|---|
| ADN-058 | Constants generation/testing is a cross-cutting slice | `CC-CONST-001` is the primary implementation-ready pseudo-slice. | It has observable behavior, implementation flow, tests and multiple consumers. | `planning/slices/cross-cutting/CC-CONST-001...` | Candidate |
| ADN-059 | Constants testing strategy | Use sync check, integration tests against generated JSON, literal tests only for critical behavioral codes, generator tests for shape/duplicates/serialization/writer/checker. | Avoids self-equality tests while limiting noise. | Tools.Tests, API integration tests | Candidate |
| ADN-060 | No constants convention/golden tests initially | Do not add regex convention tests or full golden-file tests initially. | Keeps first implementation focused and avoids noisy tests. | Tools.Tests | Candidate |
| ADN-061 | Cross-cutting/helper slices are allowed | Technical/support work with behavior, implementation flow and tests can be documented as cross-cutting/helper slice. | Avoids burying implementation-ready support work in generic workflow notes. | slice workflow docs | Candidate |
| ADN-062 | Implementation flow detail filter | Include classes/methods/code snippets only for key decisions/boundaries/non-obvious behavior; keep routine code high-level. | Keeps slice files readable while still useful for diploma/implementation. | slice docs, client docs, cross-cutting slices | Candidate |

## 3. Existing API/Contract Decisions Referenced By Testing

| ID | Decision | Current accepted direction |
|---|---|---|
| ADN-045 | API contract split | OpenAPI = structural contract; generated JSON = semantic symbolic constants. |
| ADN-046 | Native ProblemDetails remains error envelope | Use native ProblemDetails + shared errors extension. |
| ADN-048 | Human-readable stable error codes | Error codes are stable symbolic identifiers, not UI messages. |
| ADN-052 | Client constants generation command | Generate `Shared/constants.json` / `Shared/errorcodes.json` by explicit command, not hosted service. |
| ADN-055 | FluentValidation ErrorCode migration deferred | Inspect validators/helpers/tests before migration from ErrorMessage-as-code. |
