using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Queries;

public sealed class ListMyRequestsHandler
    : IRequestHandler<ListMyRequestsQuery, Result<IReadOnlyList<MyRequestSummaryResponse>, IReadOnlyList<Error>>>
{
    private readonly IAccountRepository _accounts;
    private readonly IClientRequestRepository _clientRequests;

    public ListMyRequestsHandler(
        IAccountRepository accounts,
        IClientRequestRepository clientRequests)
    {
        _accounts = accounts;
        _clientRequests = clientRequests;
    }

    public async Task<Result<IReadOnlyList<MyRequestSummaryResponse>, IReadOnlyList<Error>>> Handle(
        ListMyRequestsQuery query,
        CancellationToken cancellationToken)
    {
        var account = await _accounts.GetByIdAsync(query.ClientAccountId, cancellationToken);
        if (account is not ClientAccount)
        {
            return Result.Failure<IReadOnlyList<MyRequestSummaryResponse>, IReadOnlyList<Error>>(
                [Errors.General.NotFound]);
        }

        var status = ParseStatus(query.Status);

        var requests = await _clientRequests.ListByClientAccountIdAsync(
            query.ClientAccountId,
            status,
            cancellationToken);

        return Result.Success<IReadOnlyList<MyRequestSummaryResponse>, IReadOnlyList<Error>>(
            requests
                .Select(request => new MyRequestSummaryResponse(
                    request.RequestId,
                    request.RequestType,
                    request.Status,
                    request.CreatedAt,
                    request.Details,
                    new MyRequestAddressResponse(
                        request.PostalCode,
                        request.Region,
                        request.City,
                        request.Street,
                        request.House,
                        request.Building,
                        request.Apartment)))
                .ToList());
    }

    private static RequestStatus? ParseStatus(string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return null;
        }

        return Enum.Parse<RequestStatus>(status, ignoreCase: false);
    }
}
