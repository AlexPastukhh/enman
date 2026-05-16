using EnergyManagement.Server;
using EnergyManagement.Server.Configuration;
using EnergyManagement.Server.Contracts;
using EnergyManagement.Server.Data;
using EnergyManagement.Server.Infrastructure;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Application.Services;
using EnergyManagement.Server.L1.Persistence;
using EnergyManagement.Server.L1.Persistence.Repositories;
using EnergyManagement.Server.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();
    
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(
    _=>new AppDbContext(builder.Configuration.GetConnectionString(ConnectionStringNames.ManagementDb)!));
builder.Services.AddScoped(
    _=>new L1DbContext(builder.Configuration.GetConnectionString(ConnectionStringNames.ManagementDb)!));
builder.Services.AddTransient<IClientRepository,ClientRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IApplicantPartyRepository, ApplicantPartyRepository>();
builder.Services.AddTransient<IApplicantPartyCreationService, ApplicantPartyCreationService>();
builder.Services.AddTransient<IClientRequestRepository, ClientRequestRepository>();

// builder.Services.AddSingleton(_=>ConstantsToWrite.Create());
builder.Services.AddSingleton(_=>ErrorObject.Create());
// builder.Services.AddHostedService<ConstantWriterService>();


builder.Services.AddMediatR(c=>c.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient<IValidator<RegisterClientDto>,RegisterClientDtoValidator>();
builder.Services.AddTransient<IValidator<LoginDto>,LoginClientDtoValidator>();
builder.Services.AddTransient<IValidator<ProvideIndividualClientsDataDto>,ProvideIndividualClientsDataDtoValidator>();
builder.Services.AddTransient<IValidator<CreateIndividualRequestDto>,CreateIndividualRequestDtoValidator>();

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
    });


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
