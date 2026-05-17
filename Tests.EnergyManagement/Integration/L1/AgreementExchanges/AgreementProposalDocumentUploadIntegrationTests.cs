using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.L1.Api;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.L1.AgreementExchanges;

[Collection("IntegrationTestCollection")]
public sealed class AgreementProposalDocumentUploadIntegrationTests : L1IntegrationTestBase
{
    public AgreementProposalDocumentUploadIntegrationTests(
        IntegrationTestFixture fixture,
        ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task Upload_without_auth_returns_unauthorized()
    {
        var response = await UploadDocumentRequestAsync(
            _factory.CreateClient(),
            includeCsrf: false);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Upload_without_csrf_returns_bad_request()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await UploadDocumentRequestAsync(
            client,
            includeCsrf: false);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Upload_with_missing_document_returns_validation_problem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await UploadDocumentRequestAsync(
            client,
            includeDocument: false);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Upload_with_unsupported_content_type_returns_validation_problem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await UploadDocumentRequestAsync(
            client,
            contentType: "text/plain",
            fileName: "agreement.txt");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Client_can_upload_valid_pdf_document()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await UploadDocumentRequestAsync(client);

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
        var document = await response.Content.ReadFromJsonAsync<AgreementDocumentRefDto>()
            ?? throw new InvalidOperationException("Upload response body was empty.");

        document.StorageKey.Should().NotBeNullOrWhiteSpace();
        document.StorageKey.Should().StartWith("agreement-proposals/");
        document.StorageKey.Should().NotContain("..");
        document.OriginalFileName.Should().Be("agreement.pdf");
        document.ContentType.Should().Be("application/pdf");
        document.SizeBytes.Should().Be(ValidPdfBytes.Length);
    }

    [Fact]
    public async Task Employee_can_upload_valid_pdf_document()
    {
        const long employeeId = 930;
        await InsertEmployeeAsync(employeeId);
        var client = AuthenticatedL1Client(employeeId, "employee-930@example.com", role: "Employee");

        var response = await UploadDocumentRequestAsync(client);

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
    }

    private async Task<HttpResponseMessage> UploadDocumentRequestAsync(
        HttpClient client,
        byte[]? content = null,
        string contentType = "application/pdf",
        string fileName = "agreement.pdf",
        bool includeCsrf = true,
        bool includeDocument = true)
    {
        using var form = new MultipartFormDataContent();

        if (includeDocument)
        {
            var fileContent = new ByteArrayContent(content ?? ValidPdfBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            form.Add(fileContent, "document", fileName);
        }

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "/api/agreement-proposal-documents")
        {
            Content = form
        };

        if (includeCsrf)
        {
            request.Headers.Add(
                AntiforgeryConstants.HeaderName,
                await GetAntiforgeryTokenAsync(client));
        }

        return await client.SendAsync(request);
    }

    private static readonly byte[] ValidPdfBytes = Encoding.UTF8.GetBytes("%PDF-1.4 test document");
}
