using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace EnergyManagement.Server.Api.Contracts.Common;

public static class JsonField
{
    public static string Of<T>(Expression<Func<T, object?>> expression)
    {
        var member = expression.Body switch
        {
            MemberExpression m => m,
            UnaryExpression u when u.Operand is MemberExpression m => m,
            _ => throw new ArgumentException("Invalid expression")
        };

        var attr = member.Member.GetCustomAttribute<JsonPropertyNameAttribute>();
        return attr?.Name ?? member.Member.Name;
    }
}
