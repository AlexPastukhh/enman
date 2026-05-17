namespace EnergyManagement.Server.L1.Api.Validation;

public sealed record L1ListMyRequestsQueryDto(string? Status);

public sealed record EmployeeRequestListQueryDto(string? Status, string? ReviewState);
