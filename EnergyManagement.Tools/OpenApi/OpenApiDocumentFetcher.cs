namespace EnergyManagement.Tools.OpenApi;

public sealed class OpenApiDocumentFetcher
{
    public async Task<string> FetchAsync(
        string serverUrl,
        string swaggerPath,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        using var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        using var httpClient = new HttpClient(handler);
        var swaggerUrl = new Uri(new Uri(serverUrl.TrimEnd('/') + "/"), swaggerPath.TrimStart('/'));
        var deadline = DateTimeOffset.UtcNow + timeout;
        Exception? lastException = null;

        while (DateTimeOffset.UtcNow < deadline)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                using var response = await httpClient.GetAsync(swaggerUrl, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync(cancellationToken);
                }

                lastException = new InvalidOperationException(
                    $"Swagger endpoint returned {(int)response.StatusCode} {response.ReasonPhrase}.");
            }
            catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
            {
                lastException = exception;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
        }

        throw new TimeoutException(
            $"Timed out waiting for OpenAPI document at {swaggerUrl}.",
            lastException);
    }
}
