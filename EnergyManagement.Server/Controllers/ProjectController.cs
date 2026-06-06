using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Api.Contracts.Common;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        protected ActionResult ProblemDetailsFromValidation(IEnumerable<ValidationFailure>failures)
        {
            var errors =failures.Select(f=>ServerValidationError.Create(f.PropertyName,f.ErrorMessage));
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
            var validationErrors = errors.Select(ToServerValidationError);

            var problemDetails = new ProblemDetails()
            {
                Type="https://problems-registry.smartbear.com/validation-error",
                Title="Validation Error",
                Status=ProblemDetailsContract.ValidationStatusCode,
                Detail="Validation Error",
                Extensions=new Dictionary<string, object>()
                {
                    {ProblemDetailsContract.ErrorsExtension,validationErrors}
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
                    {ProblemDetailsContract.ErrorsExtension,new List<ServerValidationError>{ToServerValidationError(error)}}
                }!
            };
            return new ObjectResult(problemDetails){StatusCode=problemDetails.Status};
        }

        private static ServerValidationError ToServerValidationError(Error error)
        {
            return ServerValidationError.Create(
                FieldNameFromErrorCode(error.Code),
                error.Code);
        }

        private static string FieldNameFromErrorCode(string code)
        {
            return code switch
            {
                "account.email.is.registered.already" => "Email",
                "account.email.wasnt.registered" => "Email",
                "account.email.is.required" => "Email",
                "account.email.value.is.invalid" => "Email",

                "account.password.is.required" => "Password",
                "account.password.is.too.short" => "Password",
                "account.password.is.too.long" => "Password",
                "account.password.lacks.special.characters" => "Password",
                "account.password.is.wrong" => "Password",

                "account.passwordConfirmation.is.required" => "PasswordConfirmation",
                "account.passwordConfirmation.doesnt.match" => "PasswordConfirmation",

                "account.firstName.is.required" => "FirstName",
                "account.firstName.is.too.large" => "FirstName",
                "account.middleName.is.required" => "MiddleName",
                "account.middleName.is.too.large" => "MiddleName",
                "account.lastName.is.required" => "LastName",
                "account.lastName.is.too.large" => "LastName",

                "account.phoneNumber.is.required" => "PhoneNumber",
                "account.phoneNumber.is.invalid" => "PhoneNumber",

                "address.postalCode.is.required" => "Address.PostalCode",
                "address.postalCode.is.invalid" => "Address.PostalCode",
                "address.region.is.required" => "Address.Region",
                "address.region.is.too.long" => "Address.Region",
                "address.city.is.required" => "Address.City",
                "address.city.is.too.long" => "Address.City",
                "address.street.is.required" => "Address.Street",
                "address.street.is.too.long" => "Address.Street",
                "address.house.is.required" => "Address.House",
                "address.house.is.too.long" => "Address.House",
                "address.building.is.too.long" => "Address.Building",
                "address.apartment.is.too.long" => "Address.Apartment",

                "l1.applicant.inn.is.required" => "Inn",
                "l1.applicant.inn.is.invalid" => "Inn",
                "l1.applicant.ogrn.is.required" => "Ogrn",
                "l1.applicant.ogrn.is.invalid" => "Ogrn",
                "l1.applicant.ogrnip.is.required" => "Ogrnip",
                "l1.applicant.ogrnip.is.invalid" => "Ogrnip",
                "l1.applicant.kpp.is.required" => "Kpp",
                "l1.applicant.kpp.is.invalid" => "Kpp",
                "l1.applicant.organization.name.is.required" => "OrganizationName",
                "l1.applicant.organization.name.is.too.long" => "OrganizationName",

                "l1.request.object.address.is.required" => "Address",
                "client.request.text.is.required" => "Details",
                "client.request.text.is.too.long" => "Details",

                "l1.agreement.document.storage.key.is.required" => "Document",
                "l1.agreement.document.file.name.is.required" => "Document",
                "l1.agreement.document.content.type.is.required" => "Document",
                "l1.agreement.document.size.is.required" => "Document",
                "l1.agreement.proposal.comment.is.required" => "Comment",
                "l1.agreement.proposal.comment.is.too.long" => "Comment",
                "l1.agreement.final.refusal.reason.is.required" => "FinalRefusalReason",
                "l1.agreement.final.refusal.reason.is.too.long" => "FinalRefusalReason",

                _ => "root"
            };
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
