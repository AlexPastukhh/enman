using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed record L1GetCurrentIndividualApplicantPartyQuery(long ClientAccountId)
    : IRequest<Result<L1GetCurrentIndividualApplicantPartyResponse, Error>>;

public sealed record L1GetCurrentIndividualApplicantPartyResponse(
    bool Exists,
    L1IndividualApplicantPartyResponse? ApplicantParty);

public sealed record L1IndividualApplicantPartyResponse(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    string VerificationStatus);
