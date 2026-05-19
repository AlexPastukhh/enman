namespace EnergyManagement.Server.Api.Validation;

public sealed record ListMyRequestsQueryDto(string? Status);

public sealed record EmployeeRequestListQueryDto(string? Status, string? ReviewState);
