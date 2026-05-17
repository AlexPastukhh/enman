using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace EnergyManagement.Server.Api.Contracts.Common;

public sealed class ServerValidationError : ValueObject
{
    public string FieldName { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;

    [JsonConstructor]
    private ServerValidationError(string fieldName, string errorCode)
    {
        FieldName = fieldName;
        ErrorCode = errorCode;
    }

    private ServerValidationError()
    {
    }

    public static ServerValidationError Create(string fieldName, string errorCode)
        => new(fieldName, errorCode);

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return FieldName;
        yield return ErrorCode;
    }
}
