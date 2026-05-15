using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.L1.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed class L1GetCurrentUserHandler
    : IRequestHandler<L1GetCurrentUserQuery, Result<L1GetCurrentUserResponse, Error>>
{
    private readonly IAccountRepository _accounts;

    public L1GetCurrentUserHandler(IAccountRepository accounts)
    {
        _accounts = accounts;
    }

    public async Task<Result<L1GetCurrentUserResponse, Error>> Handle(
        L1GetCurrentUserQuery query,
        CancellationToken cancellationToken)
    {
        var account = await _accounts.GetByIdAsync(query.AccountId, cancellationToken);
        if (account is null)
        {
            return Result.Failure<L1GetCurrentUserResponse, Error>(Errors.General.NotFound);
        }

        return Result.Success<L1GetCurrentUserResponse, Error>(
            new L1GetCurrentUserResponse(
                account.Id,
                account.Email.Value,
                account.Role.ToString(),
                account.IsActive));
    }
}
