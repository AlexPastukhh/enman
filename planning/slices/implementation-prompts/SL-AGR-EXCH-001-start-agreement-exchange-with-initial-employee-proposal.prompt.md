# Implementation Prompt — SL-AGR-EXCH-001 Start Agreement Exchange With Initial Employee Proposal

Status: implementation prompt / non-canonical helper  
Parent slice: `planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md`  
Purpose: copyable prompt for an implementation chat. The parent slice remains the canonical planning file.

## Prompt

```text
Нужно реализовать серверный слайс:

SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal

Цель:
Employee после approve request запускает agreement exchange, отправляя первую версию договорного предложения. Это НЕ пустой start. Start сразу создаёт AgreementProposalExchange + proposal version 1 от Employee.

Endpoint:
POST /api/employee/requests/{requestId}/agreement-exchange/start

Auth:
- Employee only
- CSRF required

Success:
204 No Content

Request body:
{
  "document": {
    "storageKey": "...",
    "originalFileName": "...",
    "contentType": "application/pdf",
    "sizeBytes": 4096
  },
  "comment": "optional"
}

Scope:
1. Add/finish DTO + validator for start exchange request.
2. Add employee-only controller endpoint.
3. Add command/service method returning UnitResult<IReadOnlyList<Error>>.
4. Do NOT add per-command status enum.
5. Load current Employee from session.
6. Load request by requestId.
7. Require request to be ConnectionRequest.
8. Require request Status == Approved through domain StartByEmployee validation.
9. Check duplicate exchange by requestId before creating.
10. Build AgreementDocumentRef from DTO.
11. Build optional ProposalComment only if comment is not null/blank.
12. Call AgreementProposalExchange.StartByEmployee(...).
13. Persist AgreementProposalExchange + first AgreementProposal.
14. Return 204 on success.
15. Map failures through existing Error / ProblemDetails mapping.
16. Add integration tests.

Important domain requirement:
AgreementProposalExchange must store ClientAccountId.

StartByEmployee must set:
- RequestId = approvedRequest.Id
- ClientAccountId = approvedRequest owner client account id
- Status = AwaitingClientConfirmation
- ActiveProposalVersion = AgreementProposalVersion.First
- first proposal authored by current Employee

ClientAccountId must come from approved request owner, not from request body.

Do not add ResponsibleEmployeeId as authorization guard.
Any active Employee can service the same exchange later.
Starting Employee is recorded as first proposal author:
AgreementProposal.Author.Sender = Employee
AgreementProposal.Author.SenderId = employee.Id

Do not change ApproveReview behavior.
ApproveReview must NOT create AgreementProposalExchange.

Do not implement:
- counter-proposal send
- exchange list
- exchange details
- accept proposal
- final refusal
- binary upload
- client UI
- generated OpenAPI/types manually

DTO validation only:
- document required
- storageKey required
- originalFileName required
- contentType required
- sizeBytes > 0
- comment optional
- comment max length

Do not put lifecycle/ownership checks into FluentValidation.
Lifecycle checks belong to domain/application flow.

Repository requirements:
AgreementProposalExchangeRepository should support:
- GetByRequestIdAsync(requestId, ct)
- Add(exchange)

Prefer unique index on RequestId if already consistent with persistence approach, but do not block the slice on 409/error mapping cleanup.

Tests:
- unauthenticated -> 401
- Client role -> 403
- missing CSRF -> 400
- request not found -> mapped ProblemDetails
- request not approved -> lifecycle ProblemDetails / 422
- missing document -> 422
- invalid document fields -> 422
- too long comment -> 422
- approved request + valid document -> 204
- success creates exchange
- success stores RequestId
- success stores ClientAccountId from request owner
- success creates proposal version 1
- success proposal author is Employee + employee.Id
- success status AwaitingClientConfirmation
- duplicate start rejected
- ApproveReview alone still does not create exchange

Checks:
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

After green server tests:
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd run generate:api
npm.cmd run check:api

Report:
- files changed
- endpoint added
- domain changes
- tests added/updated
- checks result
- confirm no ResponsibleEmployeeId guard
- confirm no per-command status enum
- confirm ApproveReview still does not create exchange
```

## Contract note

This prompt follows the current parent server slice route/response direction:

```text
POST /api/employee/requests/{requestId}/agreement-exchange/start
success: 204 No Content
```

The client sidecar `L2-AGR-EXCH-START-001.client` also records a preferred future UX contract candidate:

```text
POST /api/agreement-exchanges
success: 200/201 with exchangeId
```

Do not let implementation agents mix the two silently. Generated OpenAPI must decide the client implementation contract.
