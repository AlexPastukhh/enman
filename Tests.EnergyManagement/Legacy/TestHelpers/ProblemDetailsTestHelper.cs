using System.Net.Http.Json;
using System.Text.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Tests.EnergyManagement.Legacy.TestHelpers;

public static class ProblemDetailsTestHelper
{
    public static async Task<ProblemDetails> GetProblemDetailsAsync(HttpResponseMessage response)
    {
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return problemDetails
            ?? throw new InvalidOperationException("Response body does not contain ProblemDetails.");
    }

    public static IReadOnlyList<ServerValidationError> GetValidationErrors(ProblemDetails? problemDetails)
    {
        return GetCollectionFromProblemDetailsExtension<ServerValidationError>(
            problemDetails!,
            ProblemDetailsContract.ErrorsExtension);
    }

    public static void ShouldHaveValidationErrorsEquivalentTo(
        IEnumerable<ServerValidationError> actualErrors,
        IEnumerable<ServerValidationError> expectedErrors)
    {
        actualErrors.Should().BeEquivalentTo(expectedErrors);
    }

    private static IReadOnlyList<TExtensionType> GetCollectionFromProblemDetailsExtension<TExtensionType>(
        ProblemDetails problemDetails,
        string extensionName)
    {
        if (problemDetails.Extensions.TryGetValue(extensionName, out var extensionObject)
            && extensionObject is JsonElement jsonElement)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                PropertyNameCaseInsensitive = true
            };

            var values = jsonElement.Deserialize<IEnumerable<TExtensionType>>(options);
            return values?.ToList()
                ?? throw new InvalidOperationException($"Extension '{extensionName}' is empty.");
        }

        throw new InvalidOperationException($"Extension '{extensionName}' not found in ProblemDetails.");
    }
}
