using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using MediatR;

namespace EnergyManagement.Server.Application.Queries;

public sealed record GetAccountApplicantPartiesQuery(long ClientAccountId)
    : IRequest<Result<GetAccountApplicantPartiesResponse, Error>>;

public sealed record GetAccountApplicantPartiesResponse(
    IReadOnlyList<ApplicantPartySummaryResponse> ApplicantParties);

public sealed record ApplicantPartySummaryResponse(
    long ApplicantPartyId,
    ApplicantPartyType ApplicantPartyType,
    string DisplayName,
    ApplicantPartyFullNameResponse? FullName,
    string Email,
    string PhoneNumber,
    ApplicantPartyVerificationStatus VerificationStatus,
    bool IsCurrentDefault,
    DateTimeOffset CreatedAt);

public sealed record ApplicantPartyFullNameResponse(
    string FirstName,
    string MiddleName,
    string LastName);
