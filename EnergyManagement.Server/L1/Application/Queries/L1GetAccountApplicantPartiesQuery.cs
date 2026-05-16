using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed record L1GetAccountApplicantPartiesQuery(long ClientAccountId)
    : IRequest<Result<L1GetAccountApplicantPartiesResponse, Error>>;

public sealed record L1GetAccountApplicantPartiesResponse(
    IReadOnlyList<L1ApplicantPartySummaryResponse> ApplicantParties);

public sealed record L1ApplicantPartySummaryResponse(
    long ApplicantPartyId,
    ApplicantPartyType ApplicantPartyType,
    string DisplayName,
    L1ApplicantPartyFullNameResponse? FullName,
    string Email,
    string PhoneNumber,
    ApplicantPartyVerificationStatus VerificationStatus,
    bool IsCurrentDefault,
    DateTimeOffset CreatedAt);

public sealed record L1ApplicantPartyFullNameResponse(
    string FirstName,
    string MiddleName,
    string LastName);
