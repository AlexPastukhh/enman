## L1 Application / API Implementation Rules

### Controllers

Controllers are thin HTTP boundaries.

Controllers are responsible for:

- accepting HTTP requests / DTOs;
- reading authenticated user context when needed;
- calling MediatR commands/queries;
- mapping application `Result`/errors to HTTP responses;
- returning proper status codes;
- returning validation/problem details.

Controllers must not contain domain logic and must not know EF mapping details, aggregate construction rules, or persistence rules.

Normal controller shape:

```csharp
[HttpPost]
public async Task<IActionResult> CreateRequest(CreateRequestDto dto)
{
    var command = new L1CreateConnectionRequestCommand(
        UserId: GetCurrentUserId(),
        Details: dto.Details,
        Address: dto.Address);

    var result = await _sender.Send(command);

    return result.ToActionResult();
}
```

### SaveChanges boundary

For normal single-command use cases, controllers should not call:

```csharp
_context.SaveChanges();
await _context.SaveChangesAsync();
```

Rule:

```text
Controller is HTTP boundary.
Command handler is use-case boundary.
Command handler owns SaveChanges for normal command use cases.
```

Wrong:

```csharp
var result = await _sender.Send(command);
await _context.SaveChangesAsync();
return Ok(result);
```

Right:

```csharp
var result = await _sender.Send(command);
return result.ToActionResult();
```

Do not keep controller-level `SaveChanges` as a safety net.

Why:

- handler looks like a use case but does not finish the use case;
- controller must remember which handlers require commit;
- it is easy to forget `SaveChanges`;
- handler is harder to test separately;
- command handler is harder to reuse outside HTTP;
- transaction boundary becomes unclear.

### Composite controller actions

Preferred option: create one application use case / command for the composite HTTP request.

Example:

```text
POST /some-complex-flow
  -> L1CompleteClientOnboardingCommand
```

The composite handler performs the whole use case and one commit.

Allowed exception: a controller may open an explicit transaction only for an intentional orchestration endpoint that combines several independent use cases.

```csharp
await using var transaction = await db.Database.BeginTransactionAsync();

var result1 = await sender.Send(command1);
var result2 = await sender.Send(command2);

await transaction.CommitAsync();
```

This is an exception, not the default. Ordinary L1 commands must still be able to complete their own use case.

### Command handlers

An L1 command handler is a complete write use-case boundary.

It is responsible for:

- validating/converting application input;
- loading required aggregates;
- calling L1 domain factories/methods;
- adding/updating entities through repositories;
- calling `SaveChangesAsync`;
- returning application `Result`.

Typical flow:

1. Receive command.
2. Convert primitive input to value objects.
3. Load required persisted aggregate(s).
4. If a required aggregate is missing, return business/application error.
5. Call domain factory/method.
6. Add/update aggregate through repository.
7. Call `SaveChangesAsync`.
8. Return `Result`.

Do not use old domain behavior for L1 write flow.

Forbidden L1 approach:

```csharp
var client = await _clientRepository.GetIndividualClient(...);

var request = client.CreateConnectionRequest(...);
client.AddRequestOrThrow(request);

await SaveChangesAsync();
```

Why this is wrong for L1:

- `Client`/`Account` must not create the `Request` aggregate;
- `Account` does not own `Request`;
- old `IndividualClient.ClientRequests` collection must not participate in L1 flow;
- aggregate boundaries are violated.

Correct L1 approach:

```text
handler loads ApplicantParty
handler calls ConnectionRequest.Create(applicantParty, ...)
handler adds request to ClientRequestRepository
handler commits
```

### Lightweight CQRS

Commands:

- use EF;
- use repositories;
- use domain aggregates;
- call `SaveChangesAsync`.

Queries:

- use Dapper / SQL projections;
- return read DTOs;
- do not hydrate aggregates;
- do not call `SaveChanges`.

Command handlers should not return complex read models when query side can provide them.

Query handlers should not load aggregate roots only to display data.

### L1 application slice

Do not patch old handlers for new L1 flow. Add a separate L1 application slice.

Example commands:

- `L1RegisterClientAccountCommand`;
- `L1SignInCommand`, if needed later;
- `L1CreateIndividualApplicantPartyCommand`;
- `L1CreateConnectionRequestCommand`.

Example queries:

- `L1GetCurrentUserQuery`;
- `L1GetMyRequestsQuery`;
- `L1GetRequestDetailsQuery`.

Do not create hybrid handlers such as:

```text
old command handler + L1 domain entity + old repository + old DbContext navigation mapping
```

### L1 DbContext

Create a separate DbContext for the L1 model, for example:

- `L1AppDbContext`;
- or `L1DbContext`.

Rules:

- old `AppDbContext` remains for old flow;
- `L1DbContext` is used by L1 repositories and L1 command handlers;
- both contexts may use the same connection string / physical database;
- model mapping must be separate;
- do not silently map old and L1 entities as one mixed EF model.

If one physical database is used, explicitly decide:

- table names for L1 tables;
- migration strategy;
- whether old and L1 tables coexist temporarily;
- how the test database is reset.

Register both contexts explicitly:

```csharp
builder.Services.AddDbContext<AppDbContext>(...);
builder.Services.AddDbContext<L1DbContext>(...);
```

L1 repositories must receive `L1DbContext`, not old `AppDbContext`.

### L1 repositories

Create repositories per L1 aggregate root:

- `IAccountRepository`;
- `IApplicantPartyRepository`;
- `IClientRequestRepository`.

Minimal methods:

```text
IAccountRepository
- Add(Account account)
- GetByIdAsync(long id)
- GetByEmailAsync(Email email)
- ExistsByEmailAsync(Email email)

IApplicantPartyRepository
- Add(ApplicantParty applicantParty)
- GetByIdAsync(long id)
- GetByAccountIdAsync(long accountId)

IClientRequestRepository
- Add(ClientRequest request)
- GetByIdAsync(long id)
```

Read lists can go through Dapper query side instead of repositories.

Repositories must not hide aggregate boundary violations by convenience-loading aggregate graphs with `Include`.

Do not do this for L1:

```csharp
_context.Accounts
    .Include(a => a.ApplicantParties)
    .ThenInclude(p => p.Requests)
```

These are separate aggregates.

### Query side / Dapper

Read side is separate.

Use Dapper / SQL projections for query handlers. Queries return DTO/read models such as:

- `CurrentUserDto`;
- `MyRequestDto`;
- `RequestDetailsDto`.

Query handler responsibilities:

- receive query;
- build SQL;
- execute via Dapper;
- map rows to DTO;
- return `Result`/DTO;
- do not call `SaveChanges`.

Dapper query side avoids complicating the domain model for display needs, especially because L1 domain intentionally avoids navigation collections between aggregates.

# API Plan

## Назначение

Файл фиксирует целевые HTTP endpoints по слоям.

## Naming rules

- API должен отражать use cases, а не старые имена классов.
- L1 endpoints должны быть минимальными и стабильными.
- L2/L3 endpoints не добавлять в L1-задачах.
- В перспективе frontend API client генерируется из OpenAPI/orval.

## L1 API

### Auth

```http
POST /api/auth/register
POST /api/auth/login
POST /api/auth/logout
GET  /api/auth/me
```

### Profile

```http
GET /api/profile
PUT /api/profile
```

### Applicant Parties

```http
POST /api/applicant-parties/individual
GET  /api/applicant-parties
```

### Client Requests

```http
POST /api/requests
GET  /api/requests/my
GET  /api/requests/{id}
```

### Employee Requests

```http
GET  /api/employee/requests
GET  /api/employee/requests?view=all
GET  /api/employee/requests?view=unprocessed
GET  /api/employee/requests?view=processed
GET  /api/employee/requests/{id}
POST /api/employee/requests/{id}/take
POST /api/employee/requests/{id}/approve
POST /api/employee/requests/{id}/reject
```

### Contracts

```http
GET /api/requests/{id}/contract-draft
```

In L1 contract draft is created automatically during approve flow.

### Notifications

No public L1 notification endpoint is required.

Email is sent internally by application service during approve/reject.

## L2 API

### Applicant Parties

```http
POST /api/applicant-parties/entrepreneur
POST /api/applicant-parties/legal-entity
PUT  /api/applicant-parties/{id}
```

### Documents

```http
POST   /api/requests/{id}/documents
GET    /api/requests/{id}/documents
GET    /api/documents/{id}/download
DELETE /api/documents/{id}
```

### Verification

```http
POST /api/employee/requests/{id}/verification/run
GET  /api/employee/requests/{id}/verification
```

### Clarification

```http
POST /api/employee/requests/{id}/clarification
POST /api/requests/{id}/clarification-response
```

### Contract drafts

```http
GET  /api/employee/contract-templates
POST /api/employee/requests/{id}/contract-drafts
POST /api/employee/contract-drafts/{id}/generate-pdf
POST /api/employee/contract-drafts/{id}/send
GET  /api/requests/{id}/contract-draft
GET  /api/contract-drafts/{id}/download
POST /api/contract-drafts/{id}/acknowledge
POST /api/contract-drafts/{id}/request-correction
```

### Templates

```http
GET    /api/employee/feedback-templates
POST   /api/employee/feedback-templates
PUT    /api/employee/feedback-templates/{id}
DELETE /api/employee/feedback-templates/{id}
```

## L3 API

### Anonymous requests

```http
POST /api/anonymous/requests
GET  /api/anonymous/requests/{trackingNumber}
```

### Windows auth

```http
GET /api/auth/windows/me
```

### Admin / audit

```http
GET /api/admin/audit
GET /api/admin/security-events
GET /api/admin/health
```

## Current code mismatch notes

Current code still contains older endpoint and DTO naming, for example:

- register individual client;
- provide individual client data;
- create individual request.

Target L1 naming should gradually move to:

- account/client registration;
- applicant party creation;
- generic request creation by request type.

Do not rename all endpoints in one large unsafe change. Prefer small migration steps with tests.
