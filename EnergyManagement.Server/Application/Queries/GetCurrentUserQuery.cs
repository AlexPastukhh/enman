using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.Application.Queries;

public sealed record GetCurrentUserQuery(long AccountId)
    : IRequest<Result<GetCurrentUserResponse, Error>>;

public sealed record GetCurrentUserResponse(
    long AccountId,
    string Email,
    string Role,
    bool IsActive);
