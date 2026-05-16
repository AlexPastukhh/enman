# CC-VALIDATION-001 — L1 Server Request Validation With FluentValidation

Status: final transition draft / implementation-planning / composite transition example
Slice type: cross-cutting backend maintenance
Primary purpose: introduce consistent request-level FluentValidation for L1 endpoints and remove DTO-shape validation duplication from handlers/services/controllers.

Navigation: this is the authoritative cross-cutting planning file for L1 FluentValidation adoption. Business slice drafts should reference this file instead of re-explaining the validation boundary locally.

## 1. Why this slice exists

Current L1 backend already works, but request validation boundaries are blurred.

Some validation currently happens in:

```text
- controller mapping;
- application handlers;
- application services;
- domain/value-object factories;
- domain aggregate creation.
```

This caused a concrete problem during `POST /api/l1/requests`: the handler had to manually validate `details`, `applicantContextType`, mutually exclusive fields, and address shape before it could safely execute the New applicant transaction.

Example of current handler-side DTO/request-shape validation:

[L1CreateConnectionRequestHandler.cs — request input precheck before branch flow](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/L1/Application/Commands/L1CreateConnectionRequestHandler.cs#L36-L72)

[L1CreateConnectionRequestHandler.cs — `ValidateApplicantContext` / `ValidateRequestInput`](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/L1/Application/Commands/L1CreateConnectionRequestHandler.cs#L146-L228)

The same `details` invariant is still checked in domain:

[ClientRequest.cs — domain request validation](https://github.com/AlexPastukhh/enman/blob/my-changes/Domain.EnergyManagement/L1/Requests/ClientRequest.cs#L38-L66)

This is a good transition example for the diploma:

```text
The project initially relied on handler/domain validation for L1 inputs.
As API contracts became more complex, this caused duplicated checks and unsafe controller mapping.
FluentValidation is introduced as a request-boundary layer to keep controllers safe,
keep handlers focused on application behavior, and keep domain validation as final invariant guard.
```

## 2. Repo evidence

FluentValidation already exists in the project and is used by older controllers.

Validators are registered manually in `Program.cs`:

[Program.cs — legacy validator registrations](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/Program.cs#L48-L51)

Legacy controller pattern:

[AuthController.cs — manual validation before command dispatch](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/Controllers/AuthController.cs#L46-L54)

ProblemDetails mapping already exists:

[ProjectController.cs — `ProblemDetailsFromValidation`](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/Controllers/ProjectController.cs#L14-L29)

Legacy validators already reuse value-object factories:

[Validation.cs — `Email.Create`, `Password.Create`, `Address.Create` inside validators](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/Data/Validation.cs#L17-L167)

The cross-cutting planning doc already states the same direction:

[CC-VALIDATION-001 — current gap and purpose](https://github.com/AlexPastukhh/enman/blob/my-changes/planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md#L24-L68)

[CC-VALIDATION-001 — request creation validation rules](https://github.com/AlexPastukhh/enman/blob/my-changes/planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md#L160-L197)

## 3. Core decision

```text
Use FluentValidation as the L1 API request-boundary validation layer.
```

FluentValidation owns:

```text
- request DTO shape;
- query DTO shape;
- required fields;
- null nested DTOs;
- branch/discriminator rules;
- mutually exclusive fields;
- basic value-object-shaped input;
- API field-name mapping for 422 ProblemDetails.
```

Application handlers own:

```text
- authenticated account context;
- account existence;
- ownership;
- selected entity belongs to account;
- command orchestration;
- transaction boundary;
- no partial write / atomicity.
```

Domain owns:

```text
- value object invariants as final guard;
- aggregate invariants;
- state transitions;
- impossible-state protection.
```

## 4. Controller Safety Rule

For endpoints with body/query validation:

```text
1. Controller receives DTO/query.
2. Controller runs FluentValidation before reading nested DTO properties.
3. If validation fails, controller returns 422 ProblemDetails.
4. If validation succeeds, controller maps DTO to command without manual defensive null checks.
5. Handler does not repeat request-shape rules already guaranteed by FluentValidation.
6. Handler still handles application/business outcomes.
7. Domain remains final invariant guard.
```

This matters because current `L1Controller` dereferences nested DTOs directly.

Applicant creation:

[L1Controller.cs — `dto.FullName.FirstName` mapping](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/L1/Controllers/L1Controller.cs#L120-L160)

Request creation:

[L1Controller.cs — `dto.NewApplicantParty.FullName` and `dto.Address` mapping](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/L1/Controllers/L1Controller.cs#L250-L305)

After FluentValidation passes, that mapping is safe. Before FluentValidation, malformed nested payloads can turn into controller exceptions instead of clean `422 ProblemDetails`.

## 5. Visual Scenario Flow

```text
User submits API request
        ↓
Server receives DTO/query
        ↓
Server validates request input
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ input accepted               │ input rejected               │
 ▼                              ▼
Request is handled              User/client receives
by application behavior          422 validation ProblemDetails
        ↓
Application/domain checks
business rules and invariants
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ behavior accepted            │ behavior rejected            │
 ▼                              ▼
Command/query completes          User/client receives documented
                                  business/domain error response
```

Scenario meaning:

```text
Users should get predictable validation feedback for invalid input.
Invalid request shape should not leak into application handlers as normal flow.
Application/domain errors remain meaningful business outcomes, not DTO-shape cleanup.
```

## 6. Visual Implementation Flow — General

```text
[HTTP request]
        ↓
[ASP.NET model binding]
        ↓
[FluentValidation]
Validates request/query shape:
  required fields
  null nested DTOs
  branch/discriminator rules
  mutually exclusive fields
  allowed query values
  value-object-shaped input
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ validation passes            │ validation fails             │
 ▼                              ▼
[Controller maps DTO]            [422 ProblemDetails]
        ↓                         field names = API JSON fields
[Application Handler]
Checks:
  account
  ownership
  application state
  transactions
        ↓
[Domain]
Checks:
  invariants
  aggregate creation
  state transitions
        ↓
[Persistence / response]
```

## 7. Visual Implementation Flow — `POST /api/l1/requests`

```text
[POST /api/l1/requests]
        ↓
[L1CreateConnectionRequestDtoValidator]
        ↓
Validates:
  applicantContextType is Existing or New

  Existing:
    existingApplicantPartyId is required
    newApplicantParty is absent

  New:
    newApplicantParty is required
    existingApplicantPartyId is absent

  Always:
    details is required / not blank / within max length
    address is present
    address fields are valid through Address.Create
    nested applicant fields are valid when New branch is used
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ DTO valid                    │ DTO invalid                  │
 ▼                              ▼
[L1Controller maps command]      [422 ProblemDetails]
        ↓
[L1CreateConnectionRequestHandler]
        ↓
Existing:
  load selected owned ApplicantParty
  verify ownership/application behavior
  create request
  save
        ↓
New:
  create applicant through service
  transaction
  save applicant
  create request with persisted applicant
  save request
  commit
        ↓
[200 OK]
```

Cleanup target for this endpoint:

```text
Remove handler-level request-shape validation after FluentValidation covers it:
- ValidateApplicantContext
- ValidateRequestInput
- branch null/mutual-exclusion checks
- DTO-level details checks
```

Current duplicated handler logic:

[L1CreateConnectionRequestHandler.cs — `ValidateApplicantContext` / `ValidateRequestInput`](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/L1/Application/Commands/L1CreateConnectionRequestHandler.cs#L146-L228)

## 8. Visual Implementation Flow — `POST /api/l1/applicant-parties/individual`

```text
[POST /api/l1/applicant-parties/individual]
        ↓
[L1CreateIndividualApplicantPartyDtoValidator]
        ↓
Validates:
  fullName object is present
  fullName values pass FullName.Create
  email passes Email.Create
  phoneNumber passes PhoneNumber.Create
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ DTO valid                    │ DTO invalid                  │
 ▼                              ▼
[L1Controller maps command]      [422 ProblemDetails]
        ↓
[ApplicantPartyCreationService]
Checks:
  ClientAccount exists
  domain factory still succeeds as final guard
        ↓
[Handler adds applicant + SaveChanges]
        ↓
[200 OK with ApplicantPartyId]
```

Current service validates value objects:

[ApplicantPartyCreationService.cs — `FullName.Create`, `Email.Create`, `PhoneNumber.Create`](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/L1/Application/Services/ApplicantPartyCreationService.cs#L22-L55)

Target after validation cleanup:

```text
- validator owns client-facing field validation;
- service may keep value-object/domain validation as final guard;
- service failure for pure DTO-shape input after validator passed indicates a validator coverage gap or mapping bug.
```

## 9. Visual Implementation Flow — `GET /api/l1/requests?status=...`

```text
[GET /api/l1/requests?status=...]
        ↓
[L1ListMyRequestsQueryValidator / endpoint query validator]
        ↓
Validates:
  status is empty
  OR status is a known RequestStatus value
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ query valid                  │ query invalid                │
 ▼                              ▼
[L1Controller maps query]        [422 ProblemDetails]
        ↓
[L1ListMyRequestsHandler]
Checks:
  account exists
  list owned requests
        ↓
[200 OK]
```

Current query parsing/validation is in handler:

[L1ListMyRequestsHandler.cs — `ParseStatus`](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/L1/Application/Queries/L1ListMyRequestsHandler.cs#L58-L76)

Target:

```text
Move query-shape validation to FluentValidation.
Handler may parse/use already-validated value or receive a typed filter later.
```

## 10. Endpoint Coverage Map

| Endpoint                                           | Current risk / duplication                                               | FluentValidation target                                                                         | Handler/application remains responsible for                                      |
| -------------------------------------------------- | ------------------------------------------------------------------------ | ----------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------- |
| `POST /api/l1/requests`                            | Controller derefs nested DTOs; handler validates branch/details/address. | Full request DTO validation, Existing/New branch rules, details, address, nested new applicant. | Ownership, applicant exists, transaction, request creation, no orphan applicant. |
| `POST /api/l1/applicant-parties/individual`        | Controller derefs `FullName`; service validates client input.            | FullName/email/phone input and field mapping.                                                   | Account exists, domain creation final guard, persistence.                        |
| `GET /api/l1/requests?status`                      | Handler parses invalid query status.                                     | Query param allowed values.                                                                     | Account exists, read owned requests.                                             |
| `POST /api/l1/auth/register`                       | Handler validates email/password; legacy pattern exists elsewhere.       | Email/password request input.                                                                   | Duplicate email, account creation, persistence.                                  |
| `POST /api/l1/auth/login`                          | Handler parses email before credential lookup.                           | Email/password request input.                                                                   | Account lookup, activation, password verification.                               |
| `GET /api/l1/auth/current-user`                    | No body/query.                                                           | Not needed.                                                                                     | Auth claim/account lookup.                                                       |
| `POST /api/l1/auth/logout`                         | No body/query.                                                           | Not needed.                                                                                     | Session clearing.                                                                |
| `GET /api/l1/applicant-parties/current-individual` | No body/query.                                                           | Not needed.                                                                                     | Auth/account/current lookup.                                                     |
| `GET /api/l1/requests/{requestId}`                 | Route constraint already enforces long.                                  | Optional route/query validation not needed initially.                                           | Ownership/not found.                                                             |

## 11. Validation Ownership Rules

### Request DTO / FluentValidation owns

```text
- missing body/nested object;
- missing required fields;
- blank string where input requires non-blank;
- too-long string where max is request-shape contract;
- invalid enum/discriminator strings;
- mutually exclusive branch payloads;
- allowed query values;
- field-level mapping to API JSON names.
```

### Application owns

```text
- authenticated account exists;
- selected applicant exists;
- selected applicant belongs to account;
- account can perform operation;
- operation orchestration;
- transaction/no partial writes;
- race-sensitive checks.
```

### Domain owns

```text
- value-object invariants as final guard;
- aggregate construction invariants;
- state transitions;
- persisted applicant requirement;
- impossible state prevention.
```

## 12. About using value objects in validators

Use pure value-object factories inside validators where practical:

```text
Good:
- Email.Create(dto.Email)
- PhoneNumber.Create(dto.PhoneNumber)
- FullName.Create(...)
- Address.Create(...)
```

Do not put side effects in validators:

```text
Bad:
- repository writes;
- transactions;
- command dispatch;
- request creation;
- applicant creation;
- ownership checks with mutation.
```

Repository reads in validators should not be copied blindly from legacy code. The old `RegisterClientDtoValidator` checks duplicate email through repository:

[Validation.cs — duplicate email check in legacy validator](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/Data/Validation.cs#L17-L38)

For L1, duplicate email can remain application-level unless the team explicitly accepts repo-backed validators as standard.

## 13. ProblemDetails / Field Names

Validators should return existing `422 ProblemDetails` envelope through current mapping:

[ProjectController.cs — FluentValidation failure mapping](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/Controllers/ProjectController.cs#L14-L29)

Field names should be API JSON field names, not React form state names and not necessarily C# property names.

Existing helper:

[JsonField.cs — read `JsonPropertyName`](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/Api/Contracts/Common/JsonField.cs#L7-L17)

Existing pattern:

[RequestFieldNames.cs — field names from DTO JSON names](https://github.com/AlexPastukhh/enman/blob/my-changes/EnergyManagement.Server/Api/Contracts/Requests/RequestFieldNames.cs#L5-L18)

Suggested L1 equivalent:

```text
L1FieldNames.CreateConnectionRequest.ApplicantContextType
L1FieldNames.CreateConnectionRequest.ExistingApplicantPartyId
L1FieldNames.CreateConnectionRequest.NewApplicantParty
L1FieldNames.CreateConnectionRequest.Details
L1FieldNames.CreateConnectionRequest.Address
L1FieldNames.CreateConnectionRequest.Address.PostalCode
L1FieldNames.CreateConnectionRequest.NewApplicantParty.Email
...
```

## 14. Test / Verification Plan

Primary verification should be **API integration tests**, not validator unit tests by default.

Reason:

```text
The important behavior is the HTTP boundary:
invalid input -> 422 ProblemDetails
controller does not throw
handler side effects do not happen
field mapping is usable by client
```

### Integration test groups

For `POST /api/l1/requests`:

```text
- missing applicantContextType -> 422
- unknown applicantContextType -> 422
- Existing without existingApplicantPartyId -> 422
- Existing with newApplicantParty -> 422
- New without newApplicantParty -> 422
- New with existingApplicantPartyId -> 422
- missing address -> 422, not 500
- invalid address field -> 422
- blank details -> 422
- too-long details -> 422
- New with missing fullName -> 422, not 500
- New with invalid email/phone/fullName -> 422
- invalid New branch creates no applicant/request
```

For `POST /api/l1/applicant-parties/individual`:

```text
- missing fullName -> 422, not 500
- invalid fullName -> 422
- invalid email -> 422
- invalid phoneNumber -> 422
- invalid input creates no applicant
```

For `GET /api/l1/requests?status=`:

```text
- empty status accepted
- known status accepted
- unknown status -> 422
```

Regression:

```text
- existing request Existing branch still works
- request New branch still works atomically
- applicant create still works
- auth endpoints still work
- My Requests list/details still work
```

Validator unit tests:

```text
Optional only for complex reusable helper logic.
Not default for each rule.
```

## 15. Questions / Decisions

| ID             | Status                  | Question                                                           | Decision / Direction                                                             |
| -------------- | ----------------------- | ------------------------------------------------------------------ | -------------------------------------------------------------------------------- |
| `CC-VAL-Q-001` | accepted for first pass | Manual validators or shared pipeline?                              | Start with manual controller-level validation, matching existing legacy pattern. |
| `CC-VAL-Q-002` | accepted                | Do validators make controller mapping safe?                        | Yes. Validate before nested DTO dereference.                                     |
| `CC-VAL-Q-003` | accepted                | Should handlers repeat rules already checked by FluentValidation?  | No, not as normal architecture.                                                  |
| `CC-VAL-Q-004` | accepted                | Should value objects be used in validators?                        | Yes, for pure input validation.                                                  |
| `CC-VAL-Q-005` | accepted                | Should domain validation remain?                                   | Yes, as final invariant guard.                                                   |
| `CC-VAL-Q-006` | accepted                | Should validator tests be default?                                 | No. Prefer API integration invalid variants.                                     |
| `CC-VAL-Q-007` | future                  | Global exception handling for unexpected post-validation failures? | Separate cross-cutting concern.                                                  |
| `CC-VAL-Q-008` | open                    | Should L1 eventually use validation pipeline/filter?               | Future ADR after manual pattern stabilizes.                                      |

## 16. Implementation Checklist

```text
[ ] Add L1 FluentValidation validators.
[ ] Register validators in Program.cs.
[ ] Inject relevant validators into L1Controller.
[ ] Validate before nested DTO dereference.
[ ] Map FluentValidation failures through existing ProblemDetailsFromValidation.
[ ] Add L1 field-name constants using JsonPropertyName pattern.
[ ] Add L1CreateConnectionRequestDtoValidator.
[ ] Add L1CreateIndividualApplicantPartyDtoValidator.
[ ] Add L1 request status query validator or endpoint-specific query validation.
[ ] Optionally add L1 auth DTO validators.
[ ] Remove ValidateApplicantContext from L1CreateConnectionRequestHandler.
[ ] Remove ValidateRequestInput from L1CreateConnectionRequestHandler.
[ ] Move status query validation out of L1ListMyRequestsHandler or reduce handler to typed/validated parse.
[ ] Keep ownership/account/transaction/domain checks in handlers/domain.
[ ] Add API integration tests for invalid input variants.
[ ] Verify no invalid nested DTO causes 500.
[ ] Keep generated artifacts only if contract changes require regeneration.
```

## 17. Non-goals

```text
- Do not change domain model.
- Do not weaken domain invariants.
- Do not move ownership checks into validators.
- Do not open transactions in validators.
- Do not create request/applicant in validators.
- Do not change client UI.
- Do not redesign business behavior.
- Do not use validator unit tests as primary proof.
- Do not introduce global validation pipeline in first pass unless explicitly chosen.
```

## 18. Next Implementation Prompt Shape

```text
Implement CC-VALIDATION-001 first pass for L1 API validation.

Use manual controller-level FluentValidation, matching existing legacy pattern.

Start with:
1. L1CreateConnectionRequestDtoValidator;
2. L1CreateIndividualApplicantPartyDtoValidator;
3. L1ListMyRequests status query validation.

Validators must make controller mapping safe before nested DTO dereference.

Use value-object factories inside validators for pure input validation.
Do not perform writes, transactions, command dispatch or ownership checks in validators.

After validators cover request-shape rules:
- remove ValidateApplicantContext from L1CreateConnectionRequestHandler;
- remove ValidateRequestInput from L1CreateConnectionRequestHandler;
- keep application/domain checks for ownership, account existence, transaction and domain invariants.

Verify primarily with API integration tests covering many invalid input variants.
Do not add validator unit tests unless a reusable validator helper becomes complex.

Do not change domain.
Do not change client UI.
Do not change planning docs unless explicitly requested.
```

This is the transition draft I would use as the “why FluentValidation this way” example: it shows the actual duplication problem, the safety issue in controllers, and the clean boundary between API input validation, application behavior and domain invariants.
