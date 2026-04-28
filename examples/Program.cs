using FluentValidation;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server;
using Hospital.proj.Server.Application.Validation;
using Hospital.proj.Server.Contracts;
using Hospital.proj.Server.Infrastructure;
using Hospital.proj.Server.Persistance;
using Hospital.proj.Server.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Net;
using static Hospital.proj.Server.Application.Validation.RegisterRequestValidator;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Add services to the container.

    builder.Services.AddControllers(configure =>
    {
        configure.ReturnHttpNotAcceptable = true;

        configure.Filters.Add(
            new ProducesResponseTypeAttribute(
                StatusCodes.Status400BadRequest));

        configure.Filters.Add(
            new ProducesResponseTypeAttribute(
                StatusCodes.Status500InternalServerError));

        configure.Filters.Add(
            new ProducesResponseTypeAttribute(
                StatusCodes.Status406NotAcceptable));

    }).AddNewtonsoftJson(actionSetup =>
    {
        actionSetup.SerializerSettings.ContractResolver =
            new CamelCasePropertyNamesContractResolver();
    });


    builder.Services.Configure<MvcOptions>(configOptions =>
    {
        var outputFormatter = configOptions.OutputFormatters
            .OfType<NewtonsoftJsonOutputFormatter>().FirstOrDefault();

        if (outputFormatter!.SupportedMediaTypes.Contains("text/json"))
        {
            outputFormatter.SupportedMediaTypes.Remove("text/json");
        }
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddDataProtection();


    builder.Services.AddTransient<IValidator<RegisterDto>, RegisterRequestValidator>();
    builder.Services.AddTransient<IValidator<LoginDto>, LoginRequestValidator>();
    builder.Services.AddTransient<IValidator<ChangePasswordDto>, ChangePasswordRequestValidator>();
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


    builder.Services.AddScoped(_ => 
        new HospitalDbContext(builder.Configuration.GetConnectionString("db")!));
    builder.Services.AddScoped<IUserRepository, UserRepository>();



    builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
    builder.Services.AddTransient<IEmailService, EmailService>();
    builder.Services.AddScoped
        <IVerificationLinkFactory, 
        VerificationLinkFactory>();
    builder.Services.Configure<EmailOptions>(
        builder.Configuration.GetSection("EmailOptionsEth"));



    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie();
    builder.Services.AddAuthorization();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("react", builder =>
        {
            builder.WithOrigins("https://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
    });

    var app = builder.Build();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        using var serviceScope = app.Services.CreateScope();
        using var dbContext = serviceScope.ServiceProvider.GetService<HospitalDbContext>();
        dbContext?.Database.Migrate();
    }

    app.UseHttpsRedirection();

    app.UseCors("react");

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapFallbackToFile("/index.html");

    app.Run();

}
catch (HostAbortedException)
{
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

#pragma warning disable CS1591 // ����������� ����������� XML ��� ��������� �������� ���� ��� �����
public partial class Program { }
#pragma warning restore CS1591 // ����������� ����������� XML ��� ��������� �������� ���� ��� �����