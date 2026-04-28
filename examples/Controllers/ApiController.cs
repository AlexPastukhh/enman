using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Hospital.proj.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hospital.proj.Server.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected ActionResult BodyValidationProblems(
            IEnumerable<ValidationFailure> validationFailures)
        {
            var errors = new List<object>();
            foreach (var failure in validationFailures)
            {
                errors.Add(new
                {
                    pointer = $"#/{failure.PropertyName.ToLower()}",
                    code = failure.ErrorCode
                });
            }
            var problemDetails = new ProblemDetails
            {
                Type = "https://problems-registry.smartbear.com/validation-error",
                Title = "Validation Error",
                Detail = "The request is not valid.",
                Status = 422,
                Extensions = new Dictionary<string, object>
                    {
                        {
                            "errors", errors
                        }
                    }!
            };
            return new ObjectResult(problemDetails);

        }

        protected ActionResult RouteValidationProblem(string name,Error error)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://problems-registry.smartbear.com/validation-error",
                Title = "Validation Error",
                Detail = "The request is not valid.",
                Status = 422,
                Extensions = new Dictionary<string, object>
                {
                        {
                            "parameter", name
                        },
                        {
                            "error", error.Code
                        }

                    }!
            };
            return new ObjectResult(problemDetails);
        }

        protected ActionResult MissingRouteValueProblem(string name)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://problems-registry.smartbear.com/validation-error",
                Title = "Validation Error",
                Detail = "The request is not valid.",
                Status = 422,
                Extensions = new Dictionary<string, object>
                {
                        {
                            "parameter", name
                        },
                        {
                            "error", Errors.General.RouteValueIsNull.Code
                        }

                    }!
            };
            return new ObjectResult(problemDetails);
        }

        protected ActionResult InternalProblem(Maybe<Error> error = default)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://problems-registry.smartbear.com/validation-error",
                Title = "Internal server error",
                Detail = "The server encountered an unexpected error.",
                Status = 500,
                Extensions = new Dictionary<string, object>
                    {
                        {
                            "code", error.Value.Code
                        }
                    }!
            };
            return new ObjectResult(problemDetails);
        }

        protected ActionResult FromErrorToProblem(Error error)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://problems-registry.smartbear.com/validation-error",
                Title = "Validation Error",
                Detail = "The request is not valid.",
                Status = error.StatusCode,
                Extensions = new Dictionary<string, object>
                    {
                        {
                            "Code", error.Code
                        }
                    }!
            };
            return new ObjectResult(problemDetails);
        }
    }
}
