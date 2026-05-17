using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed record L1RegisterClientAccountCommand(
    string Email,
    string Password)
    : IRequest<Result<L1RegisterClientAccountResponse, IReadOnlyList<Error>>>;

public sealed record L1RegisterClientAccountResponse(
    long AccountId,
    string Email);

public sealed record L1LoginClientAccountCommand(
    string Email,
    string Password)
    : IRequest<Result<L1LoginClientAccountResponse, IReadOnlyList<Error>>>;

public sealed record L1LoginClientAccountResponse(
    long AccountId,
    string Email,
    string Role,
    bool IsActive);

public sealed record L1CreateIndividualApplicantPartyCommand(
    long ClientAccountId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber)
    : IRequest<Result<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>>;

public sealed record L1CreateIndividualApplicantPartyResponse(
    long ApplicantPartyId,
    long ClientAccountId);

public sealed record L1CreateConnectionRequestCommand(
    long ClientAccountId,
    string? ApplicantContextType,
    long? ExistingApplicantPartyId,
    L1CreateConnectionRequestNewApplicant? NewApplicantParty,
    string Details,
    string PostalCode,
    string Region,
    string City,
    string Street,
    string House,
    string? Building,
    string? Apartment)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;

public sealed record L1CreateConnectionRequestNewApplicant(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber);

public sealed record L1MakeApplicantPartyCurrentDefaultCommand(
    long ClientAccountId,
    long ApplicantPartyId)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;
