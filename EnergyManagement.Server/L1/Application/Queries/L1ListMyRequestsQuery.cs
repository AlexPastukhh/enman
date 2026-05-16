using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed record L1ListMyRequestsQuery(
    long ClientAccountId,
    string? Status)
    : IRequest<Result<IReadOnlyList<L1MyRequestSummaryResponse>, IReadOnlyList<Error>>>;

public sealed record L1MyRequestSummaryResponse(
    long RequestId,
    ClientRequestType RequestType,
    RequestStatus Status,
    DateTimeOffset CreatedAt,
    string Summary,
    L1MyRequestAddressResponse ObjectAddress);

public sealed record L1MyRequestAddressResponse(
    string PostalCode,
    string Region,
    string City,
    string Street,
    string House,
    string? Building,
    string? Apartment);
