using System;
using System.Net.Http;
using System.Text.Json;
using Xunit;
using Xunit.Sdk;
using Xunit.Abstractions;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Data;
using System.Net;

namespace Tests.EnergyManagement.TestHelpers
{
    public class HttpResponseAssertions
    {
        private readonly HttpResponseMessage _response;
        private readonly ITestOutputHelper? _output;
        private string _body = string.Empty;
        

        public HttpResponseAssertions(HttpResponseMessage response, ITestOutputHelper? output = null)
        {
            _response = response ?? throw new ArgumentNullException(nameof(response));
            _output = output;
        }

        public static HttpResponseAssertions For(HttpResponseMessage response, ITestOutputHelper? output = null)
            => new HttpResponseAssertions(response, output);


        public async Task<HttpResponseAssertions> ShouldBeSuccess()
        {
            if (!_response.IsSuccessStatusCode)
            {
               await AssertStatusCode(500, (int)_response.StatusCode);
            }

            return this;
        }
        private async Task AssertStatusCode(int expectedStatus, int actual)
        {
            if (_response.StatusCode == HttpStatusCode.InternalServerError 
                    && _response.Content.Headers.ContentType?.MediaType == "application/problem+json")
                {
                    var info = await GetExceptionInfo();
                    _output?.WriteLine($"Assertion failed: expected status {expectedStatus} but was {actual}");
                    _output?.WriteLine(info ?? "No exception info available");
                    throw new XunitException($"Expected status {expectedStatus} but was {actual}. Exception: {info}");
                }
                else
                {
                    await ReadBody();
                    _output?.WriteLine($"Expected status {expectedStatus} but was {actual}");
                    _output?.WriteLine("Response body:");
                    if(_body.Length!=0)
                    {
                        _output?.WriteLine(_body);
                    }else
                    {
                        _output?.WriteLine("No response body.");
                    }
                    throw new XunitException($"Expected status {expectedStatus} but was {actual}. Response body: {_body}");
                }
        }

        public async Task<HttpResponseAssertions> ShouldBeStatusCode(int expectedStatus)
        {
            var actual = (int)_response.StatusCode;
            if (actual != expectedStatus)
            {
                await AssertStatusCode(expectedStatus, actual);
            }

            return this;
        }
        
            

        public async Task<string> GetExceptionInfo()
        {
            try
            {
                var problemDetails = await _response.Content.ReadFromJsonAsync<ProblemDetails>();
                if (problemDetails == null ||
                    !problemDetails.Extensions.TryGetValue(
                        ProblemDetailsContract.ExceptionExtension,
                        out var exceptionObj) ||
                    exceptionObj == null)
                {
                    throw new InvalidOperationException("Exception extension not found in ProblemDetails.");
                }
                
                if( exceptionObj is not JsonElement jsonElement)
                {
                    throw new InvalidOperationException("Exception extension is not a JsonElement.");
                }
                
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var exceptionDto = jsonElement.Deserialize<ServerExceptionDto>(jsonSerializerOptions);
                
                var info = $"Message: {exceptionDto?.Message}, StackTrace: {exceptionDto?.StackTrace}";
                return info;
            }
            catch (Exception ex)
            {
                _output?.WriteLine("Failed to read response body: " + ex);
               throw;
            }
        }
        
        public async Task ReadBody()
        {
            try
            {
                _body =await  _response.Content.ReadAsStringAsync();
                
            }
            catch (Exception ex)
            {
                _output?.WriteLine("Failed to read response body: " + ex);
               throw;
            }
        }
    }
}
