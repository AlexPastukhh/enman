using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed record L1GetCurrentUserQuery(long AccountId)
    : IRequest<Result<L1GetCurrentUserResponse, Error>>;

public sealed record L1GetCurrentUserResponse(
    long AccountId,
    string Email,
    string Role,
    bool IsActive);
