using Domain.EnergyManagement;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Application.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.Controllers;

[ApiController]
[Route("api/agreement-proposal-documents")]
public sealed class AgreementProposalDocumentsController : ProjectController
{
    private readonly IDocumentStorage _documentStorage;
    private readonly IValidator<UploadAgreementProposalDocumentForm> _validator;
    private readonly ILogger<AgreementProposalDocumentsController> _logger;

    public AgreementProposalDocumentsController(
        IDocumentStorage documentStorage,
        IValidator<UploadAgreementProposalDocumentForm> validator,
        ILogger<AgreementProposalDocumentsController> logger)
    {
        _documentStorage = documentStorage;
        _validator = validator;
        _logger = logger;
    }

    [Authorize(Roles = "Client,Employee")]
    [RequireAntiforgeryToken]
    [HttpPost(Name = "UploadAgreementProposalDocument")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(AgreementDocumentRefDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Upload(
        [FromForm] UploadAgreementProposalDocumentForm form,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(form, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ProblemDetailsFromValidation(validationResult.Errors);
            }

            var file = form.Document!;
            await using var stream = file.OpenReadStream();
            var stored = await _documentStorage.SaveAsync(
                stream,
                file.FileName,
                file.ContentType,
                file.Length,
                cancellationToken);

            var documentRef = AgreementDocumentRef.Create(
                stored.StorageKey,
                stored.OriginalFileName,
                stored.ContentType,
                stored.SizeBytes);

            if (documentRef.IsFailure)
            {
                return ProblemDetailsFromValidation(documentRef.Error);
            }

            return Ok(new AgreementDocumentRefDto(
                documentRef.Value.StorageKey,
                documentRef.Value.OriginalFileName,
                documentRef.Value.ContentType,
                documentRef.Value.SizeBytes));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Agreement proposal document upload failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }
}
