# Decisions

This file stores accepted architectural and process decisions.

## DEC-001: Separate Account and ApplicantParty

### Status

Accepted

### Context

The system must support clients, employees, and later different applicant types: ФЛ, ИП, ЮЛ.

### Decision

Use:

- `Account` for authentication/login;
- `ApplicantParty` for legal applicant data.

ФЛ/ИП/ЮЛ are `ApplicantParty` types, not `Account` subclasses.

### Consequences

Positive:

- one account can later have several applicant parties;
- easier to add ИП/ЮЛ in L2;
- easier to add anonymous requests in L3;
- account is not polluted with legal/business data.

Negative:

- model is more complex than a simple `User`.

## DEC-002: L1 supports only IndividualApplicantParty

### Status

Accepted

### Context

L1 must remain small, finishable and defendable.

### Decision

Implement only physical person applicant in L1.

ИП and ЮЛ are moved to L2.

### Consequences

- L1 is easier to complete and test;
- API is smaller;
- later extension is possible without changing the account model.

## DEC-003: No electronic signature in diploma implementation

### Status

Accepted

### Context

Electronic signature requires legal and infrastructure complexity.

### Decision

Do not implement electronic signature in L1/L2/L3.

Contract workflow is limited to:

- contract draft;
- optional PDF;
- email notification;
- acknowledgement/correction in L2.

### Consequences

- scope remains realistic;
- project avoids юридически значимый ЭДО;
- electronic signature can be mentioned as future work.

## DEC-004: Layered implementation strategy

### Status

Accepted

### Decision

Use L0/L1/L2/L3:

- L0 — cleanup and planning;
- L1 — MVP for diploma defense;
- L2 — good diploma features;
- L3 — production-like extension.

### Consequences

Agents must not implement L2/L3 features during L1 tasks.

## DEC-005: EF Core inheritance strategy

### Status

Accepted

### Decision

Use TPH for:

- `Account`;
- `ApplicantParty`;
- `ClientRequest`;
- `NotificationMessage`.

Avoid inheritance for:

- documents;
- templates;
- verification;
- audit;
- outbox.

### Consequences

- simpler migrations;
- easier lists/filters;
- fewer joins;
- nullable columns may appear in TPH tables.

## DEC-006: Playwright in TypeScript

### Status

Accepted

### Decision

Use Playwright TypeScript for E2E.

Use C# xUnit for domain/backend tests.

### Consequences

- E2E stays close to React frontend;
- backend tests stay in .NET ecosystem;
- test pyramid is easy to explain in the diploma.

## DEC-007: SharedConst should not be developed as compatibility facade

### Status

Accepted

### Context

Old shared constants mix routes, validation field names, frontend constants and server concerns.

### Decision

Split constants by responsibility:

- server routes → `Api/Routes`;
- ProblemDetails contract → `Api/Contracts/Common`;
- validation JSON field names → `Api/Contracts/*FieldNames`;
- API DTO/types for frontend → OpenAPI/orval later;
- error codes/enums/validation metadata → separate generator/tool later.

### Consequences

- less accidental coupling;
- old `SharedConst` should be removed after references are replaced;
- frontend contract generation becomes a future task.

## DEC-008: E2E tests remain in root tests folder

### Status

Accepted

### Decision

Keep Playwright E2E tests in root `tests/`.

### Consequences

- no unnecessary churn;
- component tests remain in frontend project;
- .NET tests remain in `Tests.EnergyManagement`.

## DEC-009: Use long IDs for inter-aggregate references in L1 migration model

### Status

Accepted

### Context

The parallel L1 domain model is a migration target while EF/API still use the old model. Current L1 aggregate boundaries must stay clear without forcing large public navigation graphs between account, applicant party and request aggregates.

### Decision

Use scalar `long` IDs for inter-aggregate references in the L1 migration model.

Examples:

- `IndividualApplicantParty.ClientAccountId: long`;
- `ConnectionRequest.ApplicantPartyId: long`.

Do not introduce typed ID value objects yet. Do not model aggregate relationships as primitive ID collections such as `List<long> ApplicantPartyIds`.

### Rationale

- simpler migration path from the existing old model;
- avoids large object graphs across aggregate boundaries;
- keeps aggregates independent;
- avoids EF primitive collection tracking/value comparer complexity.

### Consequences

- application services must load and check related aggregates before calling the target aggregate method;
- queries/read models handle display joins and view-specific data shape;
- EF can still define FK relationships without principal collection navigation, for example `HasOne<ClientAccount>().WithMany().HasForeignKey(...)`.
