using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnergyManagement.Server.Api.Security;

public sealed class AntiforgeryProblemDetailsResultFilter : IAlwaysRunResultFilter, IOrderedFilter
{
    public int Order => -2100;

    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is not IAntiforgeryValidationFailedResult
            && context.Result is not AntiforgeryValidationFailedResult)
        {
            return;
        }

        var problemDetails = new ProblemDetails
        {
            Type = AntiforgeryConstants.FailureType,
            Title = AntiforgeryConstants.FailureTitle,
            Status = StatusCodes.Status400BadRequest,
            Detail = AntiforgeryConstants.FailureDetail
        };
        problemDetails.Extensions["code"] = AntiforgeryConstants.FailureCode;

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
