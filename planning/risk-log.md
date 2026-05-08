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

### Description

Root `PROJECT_PLANNING.md` contains old scenario planning and may conflict with new planning.

### Impact

Agent may implement outdated model: ФЛ/ИП/ЮЛ as registration types, phone confirmation in early layer, etc.

### Mitigation

Move it to `planning/archive/old-project-planning.md` and mark as deprecated.

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
