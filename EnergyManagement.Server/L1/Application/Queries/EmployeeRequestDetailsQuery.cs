using CSharpFunctionalExtensions;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed record EmployeeRequestDetailsQuery(
    long EmployeeId,
    long RequestId)
    : IRequest<Maybe<EmployeeRequestDetailsResponse>>;

public sealed record EmployeeRequestDetailsResponse(
    long RequestId,
    string RequestType,
    string Status,
    EmployeeRequestApplicantSummaryResponse Applicant,
    string ObjectAddress,
    string Details,
    DateTimeOffset CreatedAt,
    string ReviewState);

public sealed record EmployeeRequestApplicantSummaryResponse(
    long ApplicantPartyId,
    string ApplicantPartyType,
    string DisplayName,
    string? Email,
    string? PhoneNumber);
