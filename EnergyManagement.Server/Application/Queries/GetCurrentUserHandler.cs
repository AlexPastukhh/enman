using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Queries;

public sealed class GetCurrentUserHandler
    : IRequestHandler<GetCurrentUserQuery, Result<GetCurrentUserResponse, Error>>
{
    private readonly IAccountRepository _accounts;

    public GetCurrentUserHandler(IAccountRepository accounts)
    {
        _accounts = accounts;
    }

    public async Task<Result<GetCurrentUserResponse, Error>> Handle(
        GetCurrentUserQuery query,
        CancellationToken cancellationToken)
    {
        var account = await _accounts.GetByIdAsync(query.AccountId, cancellationToken);
        if (account is null)
        {
            return Result.Failure<GetCurrentUserResponse, Error>(Errors.General.NotFound);
        }

        return Result.Success<GetCurrentUserResponse, Error>(
            new GetCurrentUserResponse(
                account.Id,
                account.Email.Value,
                account.Role.ToString(),
                account.IsActive));
    }
}
