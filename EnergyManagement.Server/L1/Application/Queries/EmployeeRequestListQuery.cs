using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed record EmployeeRequestListQuery(
    long EmployeeId,
    string? Status,
    string? ReviewState)
    : IRequest<Result<EmployeeRequestListResponse, IReadOnlyList<Error>>>;

public sealed record EmployeeRequestListResponse(
    IReadOnlyList<EmployeeRequestListItemResponse> Requests);

public sealed record EmployeeRequestListItemResponse(
    long RequestId,
    string RequestType,
    string Status,
    string ApplicantDisplayName,
    string ObjectAddress,
    DateTimeOffset CreatedAt,
    string ReviewState,
    EmployeeRequestApplicantVerificationResponse? ApplicantVerification);

public sealed record EmployeeRequestApplicantVerificationResponse(
    bool Required,
    string Status,
    bool CanRun,
    string? Message);

public enum EmployeeRequestReviewState
{
    NotStarted = 1,
    StartedByCurrentEmployee = 2,
    StartedByAnotherEmployee = 3,
    Approved = 4,
    Rejected = 5
}
