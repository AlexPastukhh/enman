# MANIFEST — SL-EMP-REQ-001 Employee Request List Read

Archive: `sl-emp-req-001-employee-request-list-read-v12.zip`

Scope: backend/API read slice implementation for `SL-EMP-REQ-001 — Employee Request List Read`.

## Added files

- `EnergyManagement.Server/L1/Api/Validation/EmployeeRequestListQueryDtoValidator.cs`
- `EnergyManagement.Server/L1/Application/Queries/EmployeeRequestListHandler.cs`
- `EnergyManagement.Server/L1/Application/Queries/EmployeeRequestListQuery.cs`
- `EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs`
- `Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRequestListIntegrationTests.cs`

## Replaced files

- `EnergyManagement.Server/L1/Api/L1Dtos.cs`
- `EnergyManagement.Server/L1/Api/Validation/L1FieldNames.cs`
- `EnergyManagement.Server/L1/Api/Validation/L1RequestValidationDto.cs`
- `EnergyManagement.Server/Program.cs`
- `EnergyManagement.Testing/TestDatabase/TestDatabaseManager.cs`
- `Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs`

## Deleted files

None.

## API shape

Adds:

```http
GET /api/employee/requests?status=...&reviewState=...
```

Response:

```ts
type EmployeeRequestListResponseDto = {
  requests: EmployeeRequestListItemDto[];
};

type EmployeeRequestListItemDto = {
  requestId: number;
  requestType: string;
  status: string;
  applicantDisplayName: string;
  objectAddress: string;
  createdAt: string;
  reviewState: "NotStarted" | "StartedByCurrentEmployee" | "StartedByAnotherEmployee" | "Approved" | "Rejected";
};
```

## Implementation notes

- Uses a dedicated employee read controller at `/api/employee/requests`.
- Requires authenticated L1 identity with `Employee` role.
- Uses FluentValidation only for query shape / allowed `status` and `reviewState` values.
- Uses Dapper read projection for request rows and review state.
- Does not load or mutate request aggregates.
- Does not implement details endpoint.
- Does not implement StartReview / ApproveReview / RejectReview commands.
- Adds test database support for `dbo.L1RequestReviews` if missing, so integration tests can seed review state.

## Tests added/updated

- Adds focused API integration tests for auth/access, empty list, compact rows, review-state projection, status filter, reviewState filter, and invalid query values.
- Updates `L1IntegrationTestBase` with employee request list and request review seed helpers.

## Generated artifacts

Not included.

This archive changes the server API contract, so after applying it run the repository OpenAPI/type generation workflow locally.

## Commands run in this environment

Not run: `.NET SDK` is not available in this sandbox.

## Non-goals respected

- No planning/docs changes.
- No client UI changes.
- No domain behavior changes.
- No review commands.
- No details endpoint.
- No AgreementProposalExchange behavior.
- No generated artifact manual edits.
