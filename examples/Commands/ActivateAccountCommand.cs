using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using MediatR;

namespace Hospital.proj.Server.Application.Commands
{
    public record ActivateAccountCommand(
        string code) : IRequest<UnitResult<Error>>;


}
