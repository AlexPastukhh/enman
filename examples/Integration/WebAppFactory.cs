using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server;
using Hospital.proj.Server.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace Hospital.proj.Tests.Integration
{
    public class WebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public string ActivationCode = default!;
        public string PasswordChangeSecret = default!;
        public List<Claim> AuthClaims = default!;
        public Mock<IEmailService> EmailMock = default!;

        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server")
            .WithPassword("SuperPassword123")
            .WithName("database.server")
            .Build();

        public WebAppFactory()
        {
            EmailMock = new Mock<IEmailService>();

            EmailMock.Setup(em => em.SendEmailAsync(
                    It.IsAny<Email>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(UnitResult.Success<Error>()));
        }

        public async Task InitializeAsync()
        {
            await _msSqlContainer.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _msSqlContainer.StopAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                var servicesToDelete = new[]
               {
                    typeof(DbContextOptions<HospitalDbContext>)
                };
                foreach (var serviceType in servicesToDelete)
                {
                    var service = services.SingleOrDefault(
                        s => s.ServiceType == typeof(HospitalDbContext));
                    services.Remove(service!);
                }

                var connectionString = "Data Source=DESKTOP-V6S02NC;Initial Catalog=Hospital.proj.Test;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
                services.AddScoped(_ => new HospitalDbContext(_msSqlContainer.GetConnectionString()));
                services.AddTransient(_ => EmailMock.Object);


            });

            base.ConfigureWebHost(builder);
        }




        public WebApplicationFactory<Program> AuthenticatedInstance(
            params Claim[] claimsSeed)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton
                            <IAuthenticationSchemeProvider,
                            TestAuthenticationSchemeProvider>();
                    services.AddSingleton
                        <MockClaimSeed>(_ => new(claimsSeed));
                });
            });
        }

        public WebApplicationFactory<Program> AuthenticatingInstance(
            out Mock<IAuthenticationService> authServiceMock,
            Mock<IEmailService>? emailMock = default
        )
        {
            if (emailMock is null)
            {
                emailMock = new Mock<IEmailService>();
            }

            authServiceMock = new Mock<IAuthenticationService>();


            var authMockObj = authServiceMock.Object;

            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(_ => authMockObj);
                    services.AddTransient(_ => emailMock.Object);
                });
            });
        }

        public WebApplicationFactory<Program> WithEmailMock(
            Mock<IEmailService>? emailMock = default
            )
        {
            if (emailMock is null)
            {
                emailMock = new Mock<IEmailService>();
            }

            return WithWebHostBuilder(builder =>
           {
               builder.ConfigureTestServices(services =>
               {
                   services.AddTransient(_ => emailMock.Object);
               });
           });



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
                IDictionary<string, AuthenticationScheme> schemes
                )
                : base(options, schemes)
            {

            }

            public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
            {
                AuthenticationScheme mockScheme = new(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    typeof(MockAuthenticationHandler));

                return Task.FromResult(mockScheme)!;

            }


        }

        public class MockAuthenticationHandler 
            : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            private MockClaimSeed Seed;
            public MockAuthenticationHandler(
                IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger,
                UrlEncoder encoder,
                MockClaimSeed seed
                )
                : base(options, logger, encoder)
            {
                Seed = seed;
            }

            protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                var claims = Seed.getSeeds();
                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal,
                    CookieAuthenticationDefaults.AuthenticationScheme);

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

            public IEnumerable<Claim> getSeeds() => _seed;
        }



    }
}
