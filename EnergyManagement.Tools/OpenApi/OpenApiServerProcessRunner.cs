using System.Diagnostics;

namespace EnergyManagement.Tools.OpenApi;

public sealed class OpenApiServerProcessRunner
{
    public OpenApiServerProcess Start(
        string projectPath,
        string serverUrl,
        string connectionString)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        startInfo.ArgumentList.Add("run");
        startInfo.ArgumentList.Add("--project");
        startInfo.ArgumentList.Add(projectPath);
        startInfo.ArgumentList.Add("--no-launch-profile");
        startInfo.ArgumentList.Add("--");
        startInfo.ArgumentList.Add("--urls");
        startInfo.ArgumentList.Add(serverUrl);

        startInfo.Environment["ASPNETCORE_ENVIRONMENT"] = "Development";
        startInfo.Environment["ConnectionStrings__ManagementDb"] = connectionString;

        var process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Could not start the server process.");

        process.OutputDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                Console.Error.WriteLine($"[openapi-server] {args.Data}");
            }
        };
        process.ErrorDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                Console.Error.WriteLine($"[openapi-server] {args.Data}");
            }
        };
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        return new OpenApiServerProcess(process);
    }
}

public sealed class OpenApiServerProcess : IAsyncDisposable
{
    private readonly Process _process;

    public OpenApiServerProcess(Process process)
    {
        _process = process;
    }

    public async ValueTask DisposeAsync()
    {
        if (_process.HasExited)
        {
            _process.Dispose();
            return;
        }

        try
        {
            _process.Kill(entireProcessTree: true);
            await _process.WaitForExitAsync();
        }
        finally
        {
            _process.Dispose();
        }
    }
}
