using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Queries;

public sealed class AgreementExchangeDetailsHandler
    : IRequestHandler<AgreementExchangeDetailsQuery, Result<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>>
{
    private readonly IAgreementExchangeReadRepository _repository;
    private readonly IEmployeeRepository _employees;

    public AgreementExchangeDetailsHandler(
        IAgreementExchangeReadRepository repository,
        IEmployeeRepository employees)
    {
        _repository = repository;
        _employees = employees;
    }

    public async Task<Result<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>> Handle(
        AgreementExchangeDetailsQuery query,
        CancellationToken cancellationToken)
    {
        if (query.CurrentRole == "Client")
        {
            if (query.CurrentAccountId <= 0)
            {
                return Result.Failure<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>(
                    [Errors.L1Domain.ClientAccountIsRequired]);
            }

            var details = await _repository.GetDetailsForClientAsync(
                query.CurrentAccountId,
                query.ExchangeId,
                cancellationToken);

            return Result.Success<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>(
                new AgreementExchangeDetailsQueryResult(details));
        }

        if (query.CurrentRole == "Employee")
        {
            var employee = await _employees.GetByIdAsync(query.CurrentAccountId, cancellationToken);
            if (employee is null)
            {
                return Result.Failure<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>(
                    [Errors.L1Domain.EmployeeIsRequired]);
            }

            var canRead = employee.EnsureCanSendAgreementProposal();
            if (canRead.IsFailure)
            {
                return Result.Failure<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>(canRead.Error);
            }

            var details = await _repository.GetDetailsForEmployeeAsync(
                query.CurrentAccountId,
                query.ExchangeId,
                cancellationToken);

            return Result.Success<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>(
                new AgreementExchangeDetailsQueryResult(details));
        }

        return Result.Failure<AgreementExchangeDetailsQueryResult, IReadOnlyList<Error>>(
            [Errors.General.InternalServerError]);
    }
}
