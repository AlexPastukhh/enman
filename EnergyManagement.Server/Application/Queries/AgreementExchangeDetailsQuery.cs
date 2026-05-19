using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;

namespace EnergyManagement.Server.Application.Queries;

public sealed record AgreementExchangeDetailsQuery(
    long CurrentAccountId,
    string CurrentRole,
    long ExchangeId)
    : IRequest<Result<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>>;

public sealed record AgreementExchangeDetailsQueryResult(
    AgreementExchangeDetailsResponse? Details);
