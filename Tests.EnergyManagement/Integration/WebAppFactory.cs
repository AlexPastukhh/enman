using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EnergyManagement.Server.Api.Auth;
using EnergyManagement.Server;
using EnergyManagement.Server.Configuration;
using EnergyManagement.Server.L1.Persistence;
using EnergyManagement.Server.L1.Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.EnergyManagement.Integration
{
    public class WebAppFactory : WebApplicationFactory<Program>
    {
        private string ConnectionString { get; } 
        public WebAppFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    [ConnectionStringNames.ManagementDbConfigurationKey] =
                        ConnectionString
                });
            });

            builder.ConfigureTestServices(services =>
            {
                var l1DbContextDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(L1DbContext));

                if (l1DbContextDescriptor != null)
                {
                    services.Remove(l1DbContextDescriptor);
                }

                services.AddScoped(_ => new L1DbContext(ConnectionString));

                var authSchemeProvider = services
                    .FirstOrDefault(d => d.ServiceType == typeof(IAuthenticationSchemeProvider));

                if (authSchemeProvider != null)
                {
                    services.Remove(authSchemeProvider);
                }

                services.AddSingleton<IAuthenticationSchemeProvider,
                    TestEmployeeWindowsAuthenticationSchemeProvider>();
                services.AddSingleton<MockClaimSeed>(_ => new MockClaimSeed([]));
            });

            base.ConfigureWebHost(builder);
        }


        public WebApplicationFactory<Program> WithEmailSender(IEmailSender emailSender)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptors = services
                        .Where(d => d.ServiceType == typeof(IEmailSender))
                        .ToList();

                    foreach (var descriptor in descriptors)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddSingleton(emailSender);
                });
            });
        }

        public WebApplicationFactory<Program> AuthenticatedInstanceWithClaims(
            params Claim[] claimsSeed)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Remove existing authentication scheme provider
                    var authSchemeProvider = services
                        .FirstOrDefault(d => d.ServiceType == typeof(IAuthenticationSchemeProvider));
                    
                    if (authSchemeProvider != null)
                    {
                        services.Remove(authSchemeProvider);
                    }

                    services.AddSingleton<IAuthenticationSchemeProvider,
                        TestAuthenticationSchemeProvider>();
                    services.AddSingleton<MockClaimSeed>(_ => new(claimsSeed));
                });
            });
        }


        public WebApplicationFactory<Program> EmployeeWindowsIdentity(
            params Claim[] claimsSeed)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var authSchemeProvider = services
                        .FirstOrDefault(d => d.ServiceType == typeof(IAuthenticationSchemeProvider));

                    if (authSchemeProvider != null)
                    {
                        services.Remove(authSchemeProvider);
                    }

                    services.AddSingleton<IAuthenticationSchemeProvider,
                        TestEmployeeWindowsAuthenticationSchemeProvider>();
                    services.AddSingleton<MockClaimSeed>(_ => new(claimsSeed));
                });
            });
        }

        public WebApplicationFactory<Program> CheckingAuthentication(
           out Mock<IAuthenticationService> authServiceMock)
        {
            authServiceMock = new Mock<IAuthenticationService>();
            var authMockObj = authServiceMock.Object;
            
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Remove existing authentication service
                    var authService = services
                        .FirstOrDefault(d => d.ServiceType == typeof(IAuthenticationService));
                    
                    if (authService != null)
                    {
                        services.Remove(authService);
                    }

                    services.AddSingleton(_ => authMockObj);
                });
            });
        }
    }

    public class MockAuthenticationHandler
        : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly MockClaimSeed _seed;
        
        public MockAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            MockClaimSeed seed)
            : base(options, logger, encoder)
        {
            _seed = seed;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = _seed.GetSeeds();
            var identity = new ClaimsIdentity(
                claims,
                Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal,
                Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public class MockClaimSeed
    {
        private readonly IEnumerable<Claim> _seed;

        public MockClaimSeed(IEnumerable<Claim> seed)
        {
            _seed = seed;
        }

        public IEnumerable<Claim> GetSeeds() => _seed;
    }

    public class TestAuthenticationSchemeProvider
        : AuthenticationSchemeProvider
    {
        public TestAuthenticationSchemeProvider(
            IOptions<AuthenticationOptions> options)
            : base(options)
        {
        }

        protected TestAuthenticationSchemeProvider(
            IOptions<AuthenticationOptions> options,
            IDictionary<string, AuthenticationScheme> schemes)
            : base(options, schemes)
        {
        }

        public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
        {
            var schemeName = string.IsNullOrWhiteSpace(name)
                ? CookieAuthenticationDefaults.AuthenticationScheme
                : name;

            AuthenticationScheme mockScheme = new(
                schemeName,
                schemeName,
                typeof(MockAuthenticationHandler));

            return Task.FromResult<AuthenticationScheme?>(mockScheme);
        }
    }

    public class TestEmployeeWindowsAuthenticationSchemeProvider
        : AuthenticationSchemeProvider
    {
        public TestEmployeeWindowsAuthenticationSchemeProvider(
            IOptions<AuthenticationOptions> options)
            : base(options)
        {
        }

        public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
        {
            if (name == EmployeeAuthSchemes.EmployeeWindows)
            {
                AuthenticationScheme mockScheme = new(
                    EmployeeAuthSchemes.EmployeeWindows,
                    EmployeeAuthSchemes.EmployeeWindows,
                    typeof(MockAuthenticationHandler));

                return Task.FromResult<AuthenticationScheme?>(mockScheme);
            }

            return base.GetSchemeAsync(name);
        }
    }

}
