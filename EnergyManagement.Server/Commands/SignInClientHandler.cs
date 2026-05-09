using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Dapper;
using static Domain.EnergyManagement.Common.Error.Errors;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Domain.EnergyManagement.DocumentManaging;

namespace EnergyManagement.Server.Commands
{
    public class SignInClientHandler : IRequestHandler<SignInClient, UnitResult<Error>>
    {
        private readonly IClientRepository _client;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public SignInClientHandler(IClientRepository clientRepository, IHttpContextAccessor httpContextAccessor)
        {
            _client = clientRepository;

            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UnitResult<Error>> Handle(
            SignInClient request,
            CancellationToken cancellationToken)
        {
            

            var getClient = await _client.GetClientByEmail(request.email);
            if (getClient.IsFailure)
            {
                return UnitResult.Failure<Error>(getClient.Error);
            }
            var client = getClient.Value;

            var verifyPassword = client.Password.VerifyPassword(request.password);
            if (verifyPassword.IsFailure)
            {
                return UnitResult.Failure<Error>(Account.PasswordIsWrong);
            }

            await SignInAsync(client);
            return UnitResult.Success<Error>();
        }
        private async Task SignInAsync(IndividualClient client)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,client.Id.ToString()),
                new Claim(ClaimTypes.Email,client.Email.Value)
            };
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties()
                {
                    IsPersistent =false
                });
        }
    }
}
