using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.Application.ApplicantParties.Commands;

public sealed record CreateIndividualApplicantPartyCommand(
    long ClientAccountId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber)
    : IRequest<Result<CreateApplicantPartyResponse, IReadOnlyList<Error>>>;

public sealed record CreateIndividualEntrepreneurApplicantPartyCommand(
    long ClientAccountId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Inn,
    string Ogrnip,
    string Email,
    string PhoneNumber)
    : IRequest<Result<CreateApplicantPartyResponse, IReadOnlyList<Error>>>;

public sealed record CreateLegalEntityApplicantPartyCommand(
    long ClientAccountId,
    string OrganizationName,
    string Inn,
    string Kpp,
    string Ogrn,
    string Email,
    string PhoneNumber)
    : IRequest<Result<CreateApplicantPartyResponse, IReadOnlyList<Error>>>;

public sealed record CreateApplicantPartyResponse(
    long ApplicantPartyId,
    long ClientAccountId,
    string ApplicantPartyType);

public sealed record MakeApplicantPartyCurrentDefaultCommand(
    long ClientAccountId,
    long ApplicantPartyId)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;
