# Action Log

This file records completed actions. Keep entries short and factual.

## 2026-05-08 — Pre-L1 cleanup

### Done

- Created initial planning documents.
- Updated `.gitignore`.
- Removed generated artifacts:
  - `_site/`;
  - `api/`;
  - `playwright-report/`;
  - `test-results/`;
  - client Playwright reports/test-results.
- Removed template/donor files:
  - ASP.NET WeatherForecast files;
  - Vite/React template assets;
  - `examples/`.
- Kept root Playwright E2E tests.
- Kept .NET tests in `Tests.EnergyManagement`.
- Started separating old shared constants into more focused route/contract classes.

### Notes

- Cleanup is a pre-L1 step.
- Existing domain model still uses old names and has not yet been refactored to target L1.
- Integration tests may require SQL Server.

## 2026-05-08 — Planning variant 2 archive prepared

### Done

- Created structured planning folder variant:
  - `README.md`;
  - `current-state.md`;
  - `general-project-info.md`;
  - `layer-plan.md`;
  - `domain-model.md`;
  - `use-cases.md`;
  - `api-plan.md`;
  - `testing-strategy.md`;
  - `decisions.md`;
  - `action-log.md`;
  - `risk-log.md`;
  - `agent-rules.md`;
  - `solution-map-and-cleanup-plan.md`;
  - `archive/old-project-planning.md`.

### Purpose

Make planning usable as living handoff documentation for future AI agents.
