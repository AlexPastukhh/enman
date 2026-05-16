# CC-VALIDATION-001 — Server Request Validation And FluentValidation

Status: implementation-ready cross-cutting principles / L1 adoption planned  
Slice type: cross-cutting concern slice  
Layers: API DTO boundary, FluentValidation, ProblemDetails mapping, application/domain validation, tests  
Depends on:

```text
planning/api/api-error-contract.md
planning/api/fluentvalidation-error-code-policy-note.md
planning/api/client-server-contract-principles.md
planning/slices/l1-slice-drafting-guide.md
```

Used by:

```text
L1 command/read endpoints
business slice drafts with server API input
client sidecars that map ProblemDetails to form/UI errors
OpenAPI/generated contract work
API error-code/constants work
```

## 1. Purpose

This file defines how server-side request validation should be planned for future L1 slices.

The project already has FluentValidation packages and legacy controller usage, but current L1 endpoints mostly validate through handlers and domain/value-object factories. Future L1 slice drafts must explicitly plan a request-level FluentValidation step when server API input has required fields, branch/discriminator rules, mutually exclusive fields, query validation or DTO shape rules.

## 2. Current Repo Evidence

Current implementation facts:

```text
EnergyManagement.Server/EnergyManagement.Server.csproj
- references FluentValidation and FluentValidation.AspNetCore.

EnergyManagement.Server/Program.cs
- registers legacy validators manually:
  IValidator<RegisterClientDto>
  IValidator<LoginDto>
  IValidator<ProvideIndividualClientsDataDto>
  IValidator<CreateIndividualRequestDto>

EnergyManagement.Server/Controllers/AuthController.cs
- injects legacy validators;
- calls ValidateAsync manually;
- maps FluentValidation failures through ProblemDetailsFromValidation(validationResult.Errors).

EnergyManagement.Server/L1/Controllers/L1Controller.cs
- does not inject IValidator<L1...Dto>;
- dispatches L1 DTO values directly to commands/queries.

EnergyManagement.Server/L1/Api/L1Dtos.cs
- L1 DTOs are records with JSON names;
- no DTO validators are attached there.

L1 application handlers
- currently validate via domain/value object factories and application rules.
```

Conclusion:

```text
FluentValidation exists and legacy manual usage exists.
L1 request DTO FluentValidation is not implemented as a consistent layer yet.
Future L1 slices should plan it explicitly instead of relying only on handler/domain validation.
```

## 3. Why This Is Cross-Cutting

Server request validation affects multiple slices:

```text
- register/login command DTOs;
- applicant-party create/read/default selection DTOs;
- request creation with Existing/New applicant context;
- My Requests query filters;
- future edit/delete/archive commands;
- client ProblemDetails field mapping;
- generated error constants and API error contract.
```

It has an independent concern flow and test responsibility, but concrete validators are implemented per endpoint/slice.

## 4. Concern-Derived Behavior Items

| ID | Concern behavior item | Source / reason | Status |
|---|---|---|---|
| `VAL-FV-001` | Server validates API request DTO shape before application/domain behavior depends on it. | API boundary concern | planned for L1 adoption |
| `VAL-FV-002` | Branch/discriminator rules are validated at request boundary. | Existing/New applicant context needs mutually exclusive fields | planned |
| `VAL-FV-003` | Query parameter values are validated and mapped to documented ProblemDetails status. | My Requests status filter pattern | partially implemented through handler; FV adoption planned |
| `VAL-FV-004` | Domain/application validation remains separate from DTO/request-shape validation. | Avoid duplicating ownership/domain invariants in DTO validators | accepted direction |
| `VAL-FV-005` | Validation errors use API DTO field names, not React form names. | API error contract / client form mapping | accepted direction |
| `VAL-FV-006` | Stable client-facing error codes should use the project error-code policy once migration is confirmed. | FluentValidation error-code policy note | future hardening |
| `VAL-FV-007` | Tests should distinguish request validation, domain/application validation and no-write/atomicity where relevant. | Slice verification clarity | accepted direction |

## 5. Coverage Overview

| Area | Current state | Target direction | Status |
|---|---|---|---|
| Legacy controllers | Manual FluentValidation validators are used. | Keep as evidence/pattern, do not blindly copy old error-code handling. | implemented legacy |
| L1 request DTOs | No consistent L1 FluentValidation layer found. | Add per-endpoint validators or a consistent validation pipeline when implementing new/changed L1 API contracts. | planned |
| L1 handlers/domain | Domain/value-object/application validation exists. | Keep for business invariants and persistence/ownership/state rules. | implemented baseline |
| ProblemDetails | Validation ProblemDetails use `errors` extension and status `422`. | Keep status/envelope; harden FieldName/ErrorCode semantics. | partially implemented |
| Error codes | Policy note says migration needs inspection/tests. | Use stable `ErrorCode` with `WithErrorCode(...)` only after mapper/tests are updated. | future hardening |

## 6. Concern Slice Flow

```text
[API request arrives]
        ↓
[ASP.NET model binding creates DTO]
        ↓
[Request-level FluentValidation]
Checks:
  required DTO fields
  basic DTO shape
  branch/discriminator validity
  mutually exclusive fields
  query parameter allowed values
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ request DTO valid            │ request DTO invalid          │
 ▼                              ▼
[Application command/query]     [422 ProblemDetails]
uses validated shape             errors use API DTO field names
        ↓
[Application/domain validation]
Checks:
  account exists
  ownership
  selected ApplicantParty belongs to account
  domain value object invariants
  state transitions
  no-write / atomicity
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ application/domain valid     │ application/domain invalid   │
 ▼                              ▼
[Persistence / response]        [422 ProblemDetails or other documented status]
```

## 7. Implementation Flow For Future L1 Slices

When a slice introduces or changes a server API input contract, its Visual Implementation Flow should include a validation boundary:

```text
[API Controller]
receives DTO / query
        ↓
[FluentValidation]
validates request-shape and branch rules
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ valid                        │ invalid                      │
 ▼                              ▼
[Application Handler]           [ProblemDetails 422]
        ↓
[Domain / Application Rules]
        ↓
[Persistence]
```

For request creation with explicit applicant context, FluentValidation should cover:

```text
applicantContextType is Existing or New

Existing:
- existingApplicantPartyId is required;
- newApplicantParty is absent/null.

New:
- newApplicantParty is required;
- existingApplicantPartyId is absent/null.

Always:
- details is required/not blank;
- address is present;
- required address fields are present;
- nested applicant data fields are present when New branch is used.
```

Application/domain validation should cover:

```text
- current authenticated account exists and is allowed;
- selected existing ApplicantParty exists;
- selected existing ApplicantParty belongs to current account;
- new ApplicantParty can be created;
- request can be created;
- new ApplicantParty + request are committed atomically;
- no partial write on failure;
- domain value object invariants.
```

## 8. Target Types / Components

Concrete implementation may use either explicit controller injection or a common pipeline, but the slice draft must identify the chosen path.

Acceptable implementation directions:

```text
Option A — manual per-controller/per-endpoint validation
- inject IValidator<TDto> into controller or endpoint service;
- call ValidateAsync before command dispatch;
- map failures to ProblemDetails.

Option B — common validation behavior/filter/pipeline
- register validators consistently;
- run validation before application handler;
- keep a documented bypass rule for endpoints without body/query validation.
```

Do not silently mix both patterns without documenting why.

## 9. Test / Check Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Validator unit tests for complex DTOs | Required/mutually exclusive/discriminator rules are stable. | validator/unit | planned per slice |
| API integration invalid DTO test | Invalid request-shape returns documented `422 ProblemDetails`. | API integration | planned per slice |
| API integration valid DTO reaches handler behavior | Valid request-shape does not block legitimate command/query. | API integration | planned per slice |
| Domain/application invalid test | Ownership/state/domain errors remain handled outside DTO validator. | API/application/domain | planned per slice |
| No-write/atomicity test | Failed validation/application path does not persist partial state. | API + persistence | planned per command slice |
| Error field/code contract test | ProblemDetails errors expose expected field names and stable codes when client depends on them. | contract/API | future hardening |

E2E should not assert FluentValidation mechanics. E2E asserts user-visible validation feedback/outcome. Component/model tests may assert client mapping from ProblemDetails to form fields.

## 10. Consumer Rule For Business Slices

Any server/API slice draft must state whether request-level FluentValidation is:

```text
implemented
planned
not needed because endpoint has no body/query validation responsibility
legacy/manual only
future hardening
```

Business slice API Contract sections should include:

```text
| Validation boundary | Rules | Error status | ProblemDetails fields/codes | Status |
```

Business slice Visual Implementation Flow should include `[FluentValidation]` before `[Application Handler]` when DTO/request rules exist.

## 11. Local Questions

| ID | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `CC-VALIDATION-Q-001` | open | Should L1 use manual per-controller validators or a shared validation pipeline/filter? | Start explicit per-slice planning; choose implementation style when first L1 validator is implemented. | L1 consistency, test setup, controller shape |
| `CC-VALIDATION-Q-002` | future review | Should old FluentValidation `ErrorMessage` usage migrate to `ErrorCode` with `WithErrorCode(...)`? | Do not migrate blindly; follow `fluentvalidation-error-code-policy-note.md`. | Stable client error-code contract |
| `CC-VALIDATION-Q-003` | assumption | Should handler/domain validation remain even if FluentValidation checks DTO shape? | Yes. FluentValidation handles request-shape; handlers/domain still own business invariants. | Prevents anemic/duplicated validation |

Shared register:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-implementation-notes-register.md
```

## 12. ADR Impact

No full ADR is required yet.

Potential future ADR candidate:

```text
Adopt a standard L1 request validation mechanism: controller-level validators vs pipeline/filter.
```

Create a full ADR only if the implementation choice becomes architecture-wide and contentious.
