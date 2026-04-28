using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server.Persistance;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Hospital.proj.Server.Application.Queryes
{
    public class LoginHandler : IRequestHandler<LoginQuery, UnitResult<Error>>
    {
        private readonly IUserRepository _users;
        private readonly IHttpContextAccessor _httpAccessor;
        public LoginHandler(IUserRepository users, IHttpContextAccessor httpAccessor)
        {
            _users = users;
            _httpAccessor = httpAccessor;
        }
        public async Task<UnitResult<Error>> Handle(LoginQuery request,
            CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email).Value;

            var getUser = await _users.GetByEmailAsync(email);
            if (getUser.IsFailure)
            {
                return getUser.Error;
            }

            var user = getUser.Value;

            var verifyPwd = Password.VerifyPassword(user.Password.Hash, 
                request.Password);
            if (verifyPwd.IsFailure)
            {
                return Errors.Account.PasswordsDontMatch;
            }

            await SignInAsync(user);

            return UnitResult.Success<Error>();
        }

        private async Task SignInAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName)
            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await _httpAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties()
                {
                    IsPersistent =true
                });
        }
    }
}
