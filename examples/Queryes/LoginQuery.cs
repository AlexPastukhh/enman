using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using MediatR;

namespace Hospital.proj.Server.Application.Queryes
{
    public record LoginQuery(string Email, string Password)
        : IRequest<UnitResult<Error>>;

}
