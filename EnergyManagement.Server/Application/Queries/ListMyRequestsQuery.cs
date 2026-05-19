using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using MediatR;

namespace EnergyManagement.Server.Application.Queries;

public sealed record ListMyRequestsQuery(
    long ClientAccountId,
    string? Status)
    : IRequest<Result<IReadOnlyList<MyRequestSummaryResponse>, IReadOnlyList<Error>>>;

public sealed record MyRequestSummaryResponse(
    long RequestId,
    ClientRequestType RequestType,
    RequestStatus Status,
    DateTimeOffset CreatedAt,
    string Summary,
    MyRequestAddressResponse ObjectAddress);

public sealed record MyRequestAddressResponse(
    string PostalCode,
    string Region,
    string City,
    string Street,
    string House,
    string? Building,
    string? Apartment);
