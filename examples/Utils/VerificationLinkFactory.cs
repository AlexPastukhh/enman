using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;

namespace Hospital.proj.Server.Utils
{
    public sealed class VerificationLinkFactory: IVerificationLinkFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _link;
        public VerificationLinkFactory(IHttpContextAccessor contextAccessor, LinkGenerator link)
        {
            _contextAccessor = contextAccessor;
            _link = link;
        }

        public string CreateLink(
            string action, 
            string controller, 
            string code, 
            string scheme = "https")
        {
            var link = _link.GetUriByAction(
                _contextAccessor.HttpContext!,
                action,
                controller,
                new { code },
                scheme);
            if (link is null)
            {
                throw new InvalidOperationException("Can't create verification link");
            }

            return link;
        }
    }
}
