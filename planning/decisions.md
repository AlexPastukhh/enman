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

Store scalar `long` IDs for inter-aggregate references in the L1 migration model.

Examples:

- `IndividualApplicantParty.ClientAccountId: long`;
- `ConnectionRequest.ApplicantPartyId: long`.

Do not introduce typed ID value objects yet. Do not model aggregate relationships as primitive ID collections such as `List<long> ApplicantPartyIds`.

This decision controls what is stored in the domain entity and persisted as an inter-aggregate reference. It does not require every aggregate factory to accept raw `long` parameters.

For type safety during creation, a factory may accept an already existing aggregate object and extract its `Id`, while storing only the scalar `long` FK/reference internally.

Examples:

- `IndividualApplicantParty.Create(ClientAccount clientAccount, ...)` stores `ClientAccountId`;
- `ConnectionRequest.Create(IndividualApplicantParty applicantParty, ...)` stores `ApplicantPartyId`.

The referenced aggregate must already be persisted and have valid `Id > 0`. Typed ID value objects are still intentionally not introduced in the current step.

### Rationale

- simpler migration path from the existing old model;
- avoids large object graphs across aggregate boundaries;
- keeps aggregates independent;
- avoids EF primitive collection tracking/value comparer complexity.

### Consequences

- application services must load and check related aggregates before calling the target aggregate method;
- queries/read models handle display joins and view-specific data shape;
- EF can still define FK relationships without principal collection navigation, for example `HasOne<ClientAccount>().WithMany().HasForeignKey(...)`.
- unit tests should not try to simulate EF-generated IDs unless a safe test helper exists;
- EF-generated ID propagation and FK persistence are integration-test concerns;
- domain unit tests cover local invariants and invalid non-persisted references where practical.

## DEC-010: Request number is not part of current implemented L1 subset

### Status

Accepted

### Context

The current implemented L1 request subset covers submitted request creation only. A request number is a public/business identifier and has different lifecycle and generation rules than the technical primary key.

### Decision

Keep request number out of the current implemented L1 subset.

- `Id` is technical identity and FK target.
- Request number is a business/public identifier.
- Do not generate request numbers inside the entity with timestamps.
- Add request number later through an application service, generator, or database sequence if it becomes necessary.

### Consequences

- current L1 request creation remains focused on persisted aggregate identity and submitted request data;
- numbering policy can be designed separately from aggregate construction;
- timestamp-based collisions and hidden formatting policy inside the entity are avoided.

## DEC-011: Use PasswordHash directly in L1 Account

### Status

Accepted

### Context

The L1 account aggregate owns account/auth lifecycle state, but raw password handling and hashing are application/auth service responsibilities.

### Decision

L1 `Account` stores `PasswordHash` directly.

- Remove/avoid the old `Password` wrapper if it is only a thin wrapper around `PasswordHash`.
- Raw password stays outside the domain entity.
- Hashing and password verification belong to the auth/password service.

### Consequences

- account state remains explicit and persistence-friendly;
- domain model does not imply that raw passwords are stored;
- password hashing policy can evolve in application/infrastructure services without changing aggregate ownership.

## DEC-012: Domain error and optional value policy

### Status

Accepted

### Context

The L1 domain model uses `Result` for validation behavior, but aggregate factories also need clear rules for programmer errors, required dependencies and optional data.

### Decision

Use `Result` for expected business/user validation failures.

Use exceptions/guards for null required domain objects/value objects, method contract violations, impossible states, and transient aggregates passed where persisted aggregate references are required.

Raw user input may be null/empty and should be treated as validation failure when it reaches a domain factory as raw primitive data. Required domain objects and value objects should not be null.

A referenced aggregate with `Id <= 0` is a method contract/application flow violation, not a normal business validation error.

Use nullable `T?` for simple persisted optional state, DTO/API fields, EF nullable columns, private backing fields and UI/display-only optional values.

Use `Maybe<T>` for operation results where absence is expected, especially repository lookups and find/get optional operations. Resolve `Maybe<T>` in the application service before calling aggregate methods. Do not pass `Maybe<T>` into aggregate factories for required dependencies, and do not map `Maybe<T>` directly with EF by default.

Prefer domain behavior methods and predicates over exposing optional state when external code only needs to decide whether an action is allowed.

### Consequences

- aggregate factories receive actual required domain objects/value objects;
- application services translate missing lookup results into explicit `Result<T>` failures when a reason is needed;
- EF mapping stays simple by using nullable backing fields/columns instead of mapped `Maybe<T>`;
- invalid transient referenced aggregates can be guarded with exceptions instead of being modeled as user validation errors.
