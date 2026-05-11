using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Persistence;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed class L1CreateConnectionRequestHandler
    : IRequestHandler<L1CreateConnectionRequestCommand, Result<L1CreateConnectionRequestResponse, IReadOnlyList<Error>>>
{
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly IClientRequestRepository _clientRequests;
    private readonly L1DbContext _context;

    public L1CreateConnectionRequestHandler(
        IApplicantPartyRepository applicantParties,
        IClientRequestRepository clientRequests,
        L1DbContext context)
    {
        _applicantParties = applicantParties;
        _clientRequests = clientRequests;
        _context = context;
    }

    public async Task<Result<L1CreateConnectionRequestResponse, IReadOnlyList<Error>>> Handle(
        L1CreateConnectionRequestCommand command,
        CancellationToken cancellationToken)
    {
        var applicantParty = await _applicantParties.GetByIdAsync(
            command.ApplicantPartyId,
            cancellationToken);

        if (applicantParty is null)
        {
            return Result.Failure<L1CreateConnectionRequestResponse, IReadOnlyList<Error>>(
                [Errors.General.NotFound]);
        }

        if (applicantParty.ClientAccountId != command.ClientAccountId)
        {
            return Result.Failure<L1CreateConnectionRequestResponse, IReadOnlyList<Error>>(
                [Errors.General.ValueIsInvalid]);
        }

        var addressResult = Address.Create(
            command.PostalCode,
            command.Region,
            command.City,
            command.Street,
            command.House,
            command.Building,
            command.Apartment);

        if (addressResult.IsFailure)
        {
            return Result.Failure<L1CreateConnectionRequestResponse, IReadOnlyList<Error>>(
                addressResult.Error);
        }

        var requestResult = ConnectionRequest.Create(
            applicantParty,
            command.Details,
            addressResult.Value);

        if (requestResult.IsFailure)
        {
            return Result.Failure<L1CreateConnectionRequestResponse, IReadOnlyList<Error>>(
                requestResult.Error);
        }

        _clientRequests.Add(requestResult.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<L1CreateConnectionRequestResponse, IReadOnlyList<Error>>(
            new L1CreateConnectionRequestResponse(
                requestResult.Value.Id,
                requestResult.Value.ApplicantPartyId,
                requestResult.Value.Status.ToString()));
    }
}
