using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.Application.ApplicantParties.Queries;

public sealed record GetCurrentIndividualApplicantPartyQuery(long ClientAccountId)
    : IRequest<Result<GetCurrentIndividualApplicantPartyResponse, Error>>;

public sealed record GetCurrentIndividualApplicantPartyResponse(
    bool Exists,
    IndividualApplicantPartyResponse? ApplicantParty);

public sealed record IndividualApplicantPartyResponse(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    string VerificationStatus);
