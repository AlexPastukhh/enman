using EnergyManagement.Server;
using EnergyManagement.Server.Api.Auth;
using EnergyManagement.Server.Configuration;
using EnergyManagement.Server.Infrastructure;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Api.Validation;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Application.Services;
using EnergyManagement.Server.L1.Application.Security;
using EnergyManagement.Server.L1.Persistence;
using EnergyManagement.Server.L1.Persistence.Repositories;
using EnergyManagement.Server.L1.Infrastructure.Documents;
using EnergyManagement.Server.Api.Security;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();
    
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AntiforgeryProblemDetailsResultFilter());
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressMapClientErrors = true;
});
builder.Services.AddProblemDetails();
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = AntiforgeryConstants.HeaderName;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(
    _=>new L1DbContext(builder.Configuration.GetConnectionString(ConnectionStringNames.ManagementDb)!));
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IApplicantPartyRepository, ApplicantPartyRepository>();
builder.Services.AddTransient<IApplicantPartyCreationService, ApplicantPartyCreationService>();
builder.Services.AddTransient<IClientRequestRepository, ClientRequestRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IAgreementProposalExchangeRepository, AgreementProposalExchangeRepository>();
builder.Services.AddTransient<IAgreementExchangeReadRepository, AgreementExchangeReadRepository>();
builder.Services.AddTransient<IAgreementExchangeReadService, AgreementExchangeReadService>();
builder.Services.AddTransient<IAgreementExchangeApplicationService, AgreementExchangeApplicationService>();
builder.Services.AddTransient<IDocumentStorage, LocalDocumentStorage>();
builder.Services.AddSingleton<L1ClaimsPrincipalFactory>();

builder.Services.AddMediatR(c=>c.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient<IValidator<L1RegisterClientAccountDto>, L1RegisterClientAccountDtoValidator>();
builder.Services.AddTransient<IValidator<L1LoginRequest>, L1LoginRequestValidator>();
builder.Services.AddTransient<IValidator<L1CreateIndividualApplicantPartyDto>, L1CreateIndividualApplicantPartyDtoValidator>();
builder.Services.AddTransient<IValidator<L1CreateConnectionRequestDto>, L1CreateConnectionRequestDtoValidator>();
builder.Services.AddTransient<IValidator<L1ListMyRequestsQueryDto>, L1ListMyRequestsQueryDtoValidator>();
builder.Services.AddTransient<IValidator<EmployeeRequestListQueryDto>, EmployeeRequestListQueryDtoValidator>();
builder.Services.AddTransient<IValidator<EmployeeRejectRequestReviewDto>, EmployeeRejectRequestReviewDtoValidator>();
builder.Services.AddTransient<IValidator<AgreementExchangeListQueryDto>, AgreementExchangeListQueryDtoValidator>();
builder.Services.AddTransient<IValidator<SendAgreementProposalVersionDto>, SendAgreementProposalVersionDtoValidator>();
builder.Services.AddTransient<IValidator<StartAgreementExchangeDto>, StartAgreementExchangeDtoValidator>();
builder.Services.AddTransient<IValidator<FinalRefuseAgreementExchangeDto>, FinalRefuseAgreementExchangeDtoValidator>();
builder.Services.AddTransient<IValidator<UploadAgreementProposalDocumentForm>, UploadAgreementProposalDocumentFormValidator>();

// 1️⃣ Register your config (it's already loaded by SharedFileService)
// builder.Services.AddSingleton<ConstantsConfig>(_ => 
//     SharedFileService.Config);  // Your existing static config

// 2️⃣ Register health check service
// builder.Services.AddHealthChecks()
//     .AddCheck<ConstantsDoctor>("constants_doctor");  // Name it whatever

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options=>
    {
        options.Events.OnRedirectToLogin = async ctx =>
        {
            if (ctx.Request.Path.StartsWithSegments("/api"))
            {
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                ctx.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Type = "https://httpstatuses.com/401",
                    Detail = "Authentication is required to access this resource."
                };

                await ctx.HttpContext
                    .RequestServices
                    .GetRequiredService<IProblemDetailsService>()
                    .WriteAsync(new ProblemDetailsContext
                    {
                        HttpContext = ctx.HttpContext,
                        ProblemDetails = problem
                    });
            }
            else
            {
                ctx.Response.Redirect(ctx.RedirectUri);
            }
        };

        options.Events.OnRedirectToAccessDenied = async ctx =>
        {
            if (ctx.Request.Path.StartsWithSegments("/api"))
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                ctx.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Type = "https://httpstatuses.com/403",
                    Detail = "You do not have permission to access this resource."
                };

                await ctx.HttpContext
                    .RequestServices
                    .GetRequiredService<IProblemDetailsService>()
                    .WriteAsync(new ProblemDetailsContext
                    {
                        HttpContext = ctx.HttpContext,
                        ProblemDetails = problem
                    });
            }
            else
            {
                ctx.Response.Redirect(ctx.RedirectUri);
            }
        };
    })
    .AddNegotiate();


var app = builder.Build();
// app.MapHealthChecks("/health");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStatusCodePages(async ctx =>
{
    var http = ctx.HttpContext;
    if (http.Response.HasStarted) return;

    var status = http.Response.StatusCode;

    // Only handle 4xx/5xx, skip if it should be returned raw
    if (status < 400 || status > 599) return;

    var pd = new ProblemDetails
    {
        Status = status,
        Title = ReasonPhrases.GetReasonPhrase(status),
        Type = $"https://httpstatuses.com/{status}",
        Instance = http.Request.Path
    };

    http.Response.ContentType = "application/problem+json";
    await http.Response.WriteAsJsonAsync(pd);
});



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

#pragma warning disable CS1591
public partial class Program
{
    
}

#pragma warning restore CS1591
