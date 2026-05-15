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
    long ApplicantPartyId,
    string Details,
    string PostalCode,
    string Region,
    string City,
    string Street,
    string House,
    string? Building,
    string? Apartment)
    : IRequest<Result<L1CreateConnectionRequestResponse, IReadOnlyList<Error>>>;

public sealed record L1CreateConnectionRequestResponse(
    long RequestId,
    long ApplicantPartyId,
    string Status);
