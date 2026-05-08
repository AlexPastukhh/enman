using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Data;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace EnergyManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        protected ActionResult ProblemDetailsFromValidation(IEnumerable<ValidationFailure>failures)
        {
            var endpoint = HttpContext.GetEndpoint();
            var routePath = (endpoint as RouteEndpoint)?.RoutePattern?.RawText
                            ?? (ControllerContext.ActionDescriptor as ControllerActionDescriptor)?.AttributeRouteInfo?.Template
                            ?? HttpContext.Request.Path.Value?.TrimStart('/')
                            ?? string.Empty;
            
            var errors =failures.Select(f=>ServerValidationError.Create(f.PropertyName,f.ErrorMessage, routePath));
            var problemDetails = new ProblemDetails()
            {
                Type="https://problems-registry.smartbear.com/validation-error",
                Title="Validation Error",
                Status=ProblemDetailsContract.ValidationStatusCode,
                Detail="Validation Error",
                Extensions=new Dictionary<string, object>()
                {
                    {ProblemDetailsContract.ErrorsExtension,errors}
                }!
            };
            return new ObjectResult(problemDetails){StatusCode=problemDetails.Status};
        }
        
        protected ActionResult ProblemDetailsFromValidation(IEnumerable<Error> errors)
        {

            var problemDetails = new ProblemDetails()
            {
                Type="https://problems-registry.smartbear.com/validation-error",
                Title="Validation Error",
                Status=ProblemDetailsContract.ValidationStatusCode,
                Detail="Validation Error",
                Extensions=new Dictionary<string, object>()
                {
                    {ProblemDetailsContract.ErrorsExtension,errors}
                }!
            };
            return new ObjectResult(problemDetails){StatusCode=problemDetails.Status};
        }

        protected ActionResult ProblemDetailsFromValidation(Error error)
        {
            
            var problemDetails = new ProblemDetails()
            {
                Type="https://problems-registry.smartbear.com/validation-error",
                Title="Validation Error",
                Status=ProblemDetailsContract.ValidationStatusCode,
                Detail="Validation Error",
                Extensions=new Dictionary<string, object>()
                {
                    {ProblemDetailsContract.ErrorsExtension,new List<Error>{error}}
                }!
            };
            return new ObjectResult(problemDetails){StatusCode=problemDetails.Status};
        }
        
        protected ActionResult ProblemDetailsFromInternalServerError(IEnumerable<Error> errors)
        {

            var problemDetails = new ProblemDetails()
            {
                Type = "https://problems-registry.smartbear.com/server-error",
                Title = "Server error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Server error",
                Extensions = new Dictionary<string, object>()
                {
                    {ProblemDetailsContract.ErrorsExtension,errors}
                }!
            };
            return new ObjectResult(problemDetails){StatusCode=problemDetails.Status};
        }
        protected ActionResult ProblemDetailsFromInternalServerError(Error error)
        {
            
            var problemDetails = new ProblemDetails()
            {
                Type="https://problems-registry.smartbear.com/server-error",
                Title="Server error",
                Status=StatusCodes.Status500InternalServerError,
                Detail="Server error",
                Extensions=new Dictionary<string, object>()
                {
                    {ProblemDetailsContract.ErrorsExtension,new List<Error>{error}}
                }!
            };
            return new ObjectResult(problemDetails){StatusCode=problemDetails.Status};
        }
        protected ActionResult ProblemDetailsWithExceptionDev(Exception ex)
        {
            
            var problemDetails = new ProblemDetails()
            {
                Type="https://problems-registry.smartbear.com/server-error",
                Title="Server error",
                Status=StatusCodes.Status500InternalServerError,
                Detail="Server error",
                Extensions=new Dictionary<string, object>()
                {
                    {ProblemDetailsContract.ExceptionExtension,ServerExceptionDto.FromException(ex)}
                }!
            };
            return new ObjectResult(problemDetails){StatusCode=problemDetails.Status};
        }
    }
}
