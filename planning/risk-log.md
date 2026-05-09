# Risk Log

This file stores known risks, future problems and consequences of current decisions.

## RISK-001: L2/L3 features may leak into L1

### Description

Target domain model mentions future fields and methods. An AI agent may implement L2/L3 features during L1 work.

### Impact

L1 becomes too large, hard to finish and hard to test.

### Mitigation

Maintain explicit L1 implementation cut in:

- `current-state.md`;
- `domain-model.md`;
- `layer-plan.md`.

## RISK-002: EF inheritance mapping can become complex

### Description

`Account`, `ApplicantParty`, `ClientRequest` and `NotificationMessage` may use inheritance.

### Impact

Incorrect TPH/TPT strategy can complicate migrations and queries.

### Mitigation

Use TPH for target inheritance hierarchies. Avoid inheritance for documents, templates, verification, audit and outbox.

## RISK-003: Contract workflow can become too large

### Description

Adding electronic signature, real EDO or payment would expand scope significantly.

### Impact

Diploma MVP may not be completed.

### Mitigation

Keep contract workflow limited to:

- simple `ContractDraft` in L1;
- templates/versioning/PDF in L2;
- no legal signing.

## RISK-004: Old PROJECT_PLANNING.md may confuse agents

### Status

Mitigated on 2026-05-08.

### Description

Root `PROJECT_PLANNING.md` contained old scenario planning and could conflict with new planning.

### Impact

Agent may implement outdated model: ФЛ/ИП/ЮЛ as registration types, phone confirmation in early layer, etc.

### Mitigation

Completed: the original root file was moved to `planning/archive/PROJECT_PLANNING.md`, and `planning/archive/old-project-planning.md` explains why it is deprecated.

## RISK-005: Planning files may become too large

### Description

If all decisions/actions/risks stay in one file, agents will waste tokens and may miss details.

### Impact

Context loss and inconsistent decisions.

### Mitigation

Split planning by purpose:

- current state;
- decisions;
- action log;
- risk log;
- domain model;
- API plan;
- testing strategy.

## RISK-006: Generated contracts may become another source of truth

### Description

Manual `Shared/*.json` and generated frontend constants can drift from server/domain.

### Impact

Frontend and backend validation/contracts diverge.

### Mitigation

Use OpenAPI/orval for DTO/API and separate generation for error codes/enums/metadata.

## RISK-007: Integration tests depend on external SQL Server

### Description

Existing integration tests may fail if SQL Server/test database is unavailable.

### Impact

Build/test confidence decreases.

### Mitigation

Introduce test DB strategy:
- SQL Server container;
- localdb;
- testcontainers;
- or isolated integration test profile.

## RISK-008: Playwright E2E tests are not a reliable verification layer yet

### Description

The repository has Playwright configuration and root E2E tests, but the current setup is stale:

- root Playwright dependencies are declared but root `node_modules` is not available;
- no Playwright `webServer` starts frontend/backend services;
- root and client Playwright configs point to different test directories;
- root page helpers contain likely runtime bugs such as reading `Locator.isVisible` as a property and not awaiting async fill actions.

### Impact

E2E tests may fail before running, pass/fail inconsistently, or depend on manually prepared local state.

### Mitigation

Before using E2E as diploma verification, consolidate Playwright into one config, add service startup strategy, make test data unique/isolated, and clean page object helpers.
