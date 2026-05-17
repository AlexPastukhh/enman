using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.Api.Security;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequireAntiforgeryTokenAttribute : TypeFilterAttribute
{
    public RequireAntiforgeryTokenAttribute()
        : base(typeof(RequireAntiforgeryTokenFilter))
    {
    }
}
