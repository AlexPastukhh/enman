using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.L1.Application.Abstractions;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed record AgreementExchangeDetailsQuery(
    long CurrentAccountId,
    string CurrentRole,
    long ExchangeId)
    : IRequest<Result<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>>;

public sealed record AgreementExchangeDetailsQueryResult(
    AgreementExchangeDetailsResponse? Details);
