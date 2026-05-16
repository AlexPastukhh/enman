# Backend Legacy / L1 Boundary Map

Status: current / Stage 2 cleanup boundary map  
Audience: planning agents, implementation agents, thesis/diploma readers  
Scope: backend runtime boundaries, shared domain primitives, legacy surface, tests classification, post-FluentValidation handler cleanup

## 1. Purpose

This document defines the backend boundary between:

```text
- current L1 runtime;
- shared domain primitives;
- legacy runtime;
- legacy domain surface;
- current and legacy tests;
- validation/application/domain responsibilities.
```

It exists to prevent accidental cleanup mistakes during the broader project cleanup:

```text
- do not delete shared value objects just because they live in older folders;
- do not copy legacy runtime patterns into L1;
- do not treat legacy tests as current L1 behavior;
- do not make handlers own DTO/input validation after FluentValidation;
- do not confuse business/application validation with request-shape validation;
- do not treat persisted-data guard exceptions as user validation errors.
```

This document is also useful for the diploma/thesis because it explains the architectural transition from the initial legacy backend to the current L1 slice-based backend.

## 2. Current L1 Runtime Surface

Current backend runtime for new implementation is the L1 stack:

```text
EnergyManagement.Server/L1/**
Domain.EnergyManagement/L1/**
EnergyManagement.Server/L1/Persistence/L1DbContext.cs
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/Validation/**
Tests.EnergyManagement/Integration/L1/**
```

Current evidence:

```markdown
[L1Controller.cs, lines 1-51](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Controllers/L1Controller.cs#L1-L51)
[Program.cs, lines 37-62](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/Program.cs#L37-L62)
[L1SliceIntegrationTests.cs, lines 13-68](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs#L13-L68)
```

Current L1 is the source of truth for new backend implementation and slice-driven work.

## 3. Shared Domain Primitives

Value objects under `Domain.EnergyManagement/DocumentManaging` are not legacy by default.

Several are already used by L1. Others are expected future primitives for applicant documents, applicant profiles and verification flows.

### 3.1 Current L1 / Shared Value Objects

| Value object | Status | Reason |
|---|---|---|
| `Email` | current / shared | Used by L1 auth/account and applicant-party validation/handlers. |
| `PasswordHash` | current L1 credential value object | L1 account credential flow uses `PasswordHash` directly. |
| `FullName` | current / shared | Used by L1 individual applicant party. |
| `PhoneNumber` | current / shared | Used by L1 individual applicant party. |
| `Address` | current / shared | Used by L1 connection request object address. |

Evidence:

```markdown
[Email.cs, lines 8-39](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/Email.cs#L8-L39)
[Password.cs, lines 127-186](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/Password.cs#L127-L186)
[FullName.cs, lines 7-50](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/FullName.cs#L7-L50)
[PhoneNumber.cs, lines 10-40](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/PhoneNumber.cs#L10-L40)
[Address.cs, lines 10-114](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/Address.cs#L10-L114)
[ClientAccount.cs, lines 7-40](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/L1/Accounts/ClientAccount.cs#L7-L40)
[L1ApplicantPartyValidation.cs, lines 8-54](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Api/Validation/L1ApplicantPartyValidation.cs#L8-L54)
```

### 3.2 Future / Shared Applicant Document Value Objects

| Value object | Status | Reason |
|---|---|---|
| `PassportData` | future / shared | Likely needed for applicant documents and verification. |
| `SNILS` | future / shared | Likely needed for applicant documents and verification. |

Evidence:

```markdown
[PassportData.cs, lines 8-77](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/PassportData.cs#L8-L77)
[SNILS.cs, lines 12-47](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/SNILS.cs#L12-L47)
```

## 4. Password Boundary

`PasswordHash` is the current L1 credential value object.

`Password` is a legacy compatibility wrapper over `PasswordHash`.

Current rule:

```text
L1 code should use PasswordHash directly.
```

`PasswordHash` owns:

```text
- plain password validation;
- hash/salt creation;
- password verification;
- persisted hash restoration.
```

`Password` remains only while legacy `IndividualClient` / legacy auth code still depends on it.

Evidence:

```markdown
[Password.cs, lines 24-77](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/Password.cs#L24-L77)
[Password.cs, lines 127-186](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/Password.cs#L127-L186)
[Password.cs, lines 235-251](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/Password.cs#L235-L251)
```

Validation vs guard rule:

```text
Plain password input validation:
  PasswordHash.ValidatePlainTextPassword(...)
  returns domain validation errors.

Malformed stored hash:
  PasswordHash.ConvertFromString(...)
  may throw.
  This is internal data corruption or programming error, not user validation.
```

## 5. Legacy Runtime Surface

Everything in server runtime outside `EnergyManagement.Server/L1/**` should be treated as legacy unless proven otherwise.

Legacy runtime includes:

```text
EnergyManagement.Server/Controllers/AuthController.cs
EnergyManagement.Server/Controllers/ClientRequestController.cs
EnergyManagement.Server/Commands/**
EnergyManagement.Server/Data/Validation.cs
EnergyManagement.Server/AppDbContext.cs
EnergyManagement.Server/Repositories/**
old route/DTO contracts outside L1
```

Evidence:

```markdown
[AuthController.cs, lines 18-62](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/Controllers/AuthController.cs#L18-L62)
[ClientRequestController.cs, lines 13-65](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/Controllers/ClientRequestController.cs#L13-L65)
[Validation.cs, lines 14-75](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/Data/Validation.cs#L14-L75)
```

Legacy runtime may remain temporarily for compatibility or historical comparison, but it must not be copied as the architecture pattern for new L1 work.

## 6. Legacy Domain Surface

Legacy domain is mostly old entities and use cases, not the shared value objects.

Treat these as legacy unless explicitly reused by L1:

```text
Domain.EnergyManagement/DocumentManaging/IndividualClient.cs
Domain.EnergyManagement/DocumentManaging/IndividualRequest.cs
old Client / Manager / old request model around IndividualClient
```

Evidence:

```markdown
[IndividualClient.cs, lines 10-82](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/DocumentManaging/IndividualClient.cs#L10-L82)
```

Current L1 uses newer domain concepts:

```text
Domain.EnergyManagement/L1/Accounts/ClientAccount.cs
Domain.EnergyManagement/L1/ApplicantParties/**
Domain.EnergyManagement/L1/Requests/**
```

Evidence:

```markdown
[ClientAccount.cs, lines 7-40](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Domain.EnergyManagement/L1/Accounts/ClientAccount.cs#L7-L40)
```

## 7. Validation Boundary

### 7.1 FluentValidation Owns API Input Validation

FluentValidation owns:

```text
- request DTO shape;
- query DTO shape;
- required fields;
- nested DTO null checks;
- branch/discriminator rules;
- mutually exclusive input fields;
- value-object-shaped client input;
- API field-name mapping for 422 ProblemDetails.
```

Current evidence:

```markdown
[L1Controller.cs, lines 23-51](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Controllers/L1Controller.cs#L23-L51)
[L1Controller.cs, lines 152-187](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Controllers/L1Controller.cs#L152-L187)
[L1Controller.cs, lines 309-360](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Controllers/L1Controller.cs#L309-L360)
[L1Controller.cs, lines 374-416](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Controllers/L1Controller.cs#L374-L416)
[L1CreateConnectionRequestDtoValidator.cs, lines 8-143](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Api/Validation/L1CreateConnectionRequestDtoValidator.cs#L8-L143)
[L1ApplicantPartyValidation.cs, lines 8-54](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Api/Validation/L1ApplicantPartyValidation.cs#L8-L54)
```

### 7.2 Handlers Own Application And Business Behavior

Handlers may still return validation/problem responses for application/business errors:

```text
- duplicate email;
- invalid credentials;
- account not found under auth context;
- selected entity not found;
- selected entity not owned by account;
- business state transition rejected;
- domain aggregate creation rejected by business invariant.
```

Handlers should not own normal DTO-shape validation after FluentValidation:

```text
- invalid email format;
- invalid password shape;
- invalid address field;
- blank details;
- missing nested DTO;
- invalid applicantContextType;
- Existing/New branch mutual exclusion.
```

If such a value-object creation fails after controller validation passed, treat it as:

```text
- validator coverage bug;
- controller mapping bug;
- changed domain invariant not reflected in validator;
- internal mismatch.
```

## 8. Handler Value-Object Creation Rule

### 8.1 Current Acceptable Transition

Handler/service may still create value objects from command primitives, but failure should be treated as final guard / internal mismatch after the request-boundary validator already accepted the DTO.

Current cleanup candidates:

```markdown
[L1CreateConnectionRequestHandler.cs, lines 36-56](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Application/Commands/L1CreateConnectionRequestHandler.cs#L36-L56)
[ApplicantPartyCreationService.cs, lines 22-55](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/EnergyManagement.Server/L1/Application/Services/ApplicantPartyCreationService.cs#L22-L55)
```

### 8.2 Cleaner Target

Cleaner long-term target:

```text
Controller validates DTO.
Controller or mapper creates value objects.
Command carries value objects or validated input.
Handler executes application behavior.
Value-object creation failure after validation is impossible except bug/internal mismatch.
```

Do not make this refactor inside unrelated slices.

Do not weaken domain invariants to make handler cleanup easier.

## 9. Test Classification

### 9.1 Current L1 Tests

Current L1 tests live under:

```text
Tests.EnergyManagement/Integration/L1/**
```

Evidence:

```markdown
[L1SliceIntegrationTests.cs, lines 13-68](https://github.com/AlexPastukhh/enman/blob/3a91f22714caaec3c80331628cb9ee60a6e912fa/Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs#L13-L68)
```

These test current L1 behavior.

### 9.2 Shared Value-Object / Domain Tests

Shared value-object tests are not legacy by default.

Current known examples:

```text
PasswordHash tests:
  current L1 credential value-object tests.

Password wrapper tests:
  legacy compatibility tests while legacy auth/IndividualClient remains.
```

### 9.3 Legacy Integration Tests

Treat legacy integration tests as legacy unless migrated to L1:

```text
Tests.EnergyManagement/Integration/AuthTests.cs
Tests.EnergyManagement/Integration/AuthTestsBase.cs
Tests.EnergyManagement/Integration/ClientRequestsTests.cs
Tests.EnergyManagement/Integration/ClientRequestsTestsBase.cs
```

### 9.4 Legacy Entity Tests

Treat legacy entity tests as legacy unless explicitly reclassified:

```text
Tests.EnergyManagement/Unit/UserTests.cs
Tests.EnergyManagement/Unit/IndividualClientTests.cs
Tests.EnergyManagement/Unit/IndividualClientMethodsTests.cs
Tests.EnergyManagement/Unit/ClientRequestTests.cs
Tests.EnergyManagement/Unit/ClientRequestUnitBase.cs
```

### 9.5 Shared Test Helpers

Do not delete automatically:

```text
Tests.EnergyManagement/TestHelpers/**
Tests.EnergyManagement/Integration/WebAppFactory.cs
Tests.EnergyManagement/TestHelpers/HttpResponseAssertions.cs
```

Classify each helper as one of:

```text
- shared;
- L1-only;
- legacy-only.
```

## 10. Cleanup Order

Recommended Stage 2 order:

```text
1. Maintain this boundary map.
2. Finish PasswordHash transition.
3. Audit legacy runtime/controllers/validators/commands/tests.
4. Classify tests and helpers.
5. Clean L1 handler/service post-FluentValidation checks.
6. Decide legacy runtime removal vs isolation.
7. Remove or quarantine legacy controllers/commands/validators.
8. Only then delete legacy domain entities/tests.
```

## 11. Do Not Do

```text
- Do not delete value objects only because they are in DocumentManaging.
- Do not copy legacy handlers/controllers into L1.
- Do not treat legacy tests as current L1 behavior.
- Do not turn internal/data-corruption guards into user validation errors.
- Do not weaken domain invariants to make handler cleanup easier.
- Do not remove Password wrapper until legacy code no longer depends on it.
- Do not mix this cleanup with client work.
- Do not change generated artifacts unless an explicit contract-generation task requires it.
```

## 12. Diploma / Thesis Note

This boundary map documents the project transition:

```text
Initial backend:
  legacy controllers, legacy commands, legacy AppDbContext,
  older IndividualClient/IndividualRequest model.

Current backend:
  L1 slice-driven API,
  L1Controller,
  L1DbContext,
  explicit FluentValidation request boundary,
  L1 domain model for accounts, applicant parties and requests.

Shared domain:
  reusable value objects that survive the transition.
```

It can be used to explain:

```text
- why FluentValidation was introduced as an API boundary;
- why value objects were kept instead of deleted with legacy entities;
- why PasswordHash became the direct L1 credential value object;
- why legacy tests must be classified before deletion;
- why handler validation and request validation are separated.
```
