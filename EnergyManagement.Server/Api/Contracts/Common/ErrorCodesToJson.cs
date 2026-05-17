using static Domain.EnergyManagement.Common.Error.Errors;

namespace EnergyManagement.Server.Api.Contracts.Common;

public class ErrorObject
{
    public EmailErrorCodes Email { get; set; }
    public PasswordErrorCodes Password { get; set; }
    public PasswordConfirmationErrorCodes PasswordConfirmation { get; set; }
    public PhoneErrorCodes Phone { get; set; }
    public ServerValidationErrorContract ServerValidationError { get; set; }

    public ErrorObject(
        EmailErrorCodes email,
        PasswordErrorCodes password,
        PasswordConfirmationErrorCodes passwordConfirmation,
        PhoneErrorCodes phone,
        ServerValidationErrorContract serverValidationError)
    {
        Email = email;
        Password = password;
        PasswordConfirmation = passwordConfirmation;
        Phone = phone;
        ServerValidationError = serverValidationError;
    }

    public static ErrorObject Create()
        => new(
            EmailErrorCodes.Create(),
            PasswordErrorCodes.Create(),
            PasswordConfirmationErrorCodes.Create(),
            PhoneErrorCodes.Create(),
            ServerValidationErrorContract.Create());
}

public class ServerValidationErrorContract
{
    public string FieldNameField { get; set; }
    public string ErrorCodeField { get; set; }

    public ServerValidationErrorContract(
        string fieldNameField,
        string errorCodeField)
    {
        FieldNameField = fieldNameField;
        ErrorCodeField = errorCodeField;
    }

    public static ServerValidationErrorContract Create()
    {
        var instance = ServerValidationError.Create("", "");

        return new ServerValidationErrorContract(
            nameof(instance.FieldName),
            nameof(instance.ErrorCode));
    }
}

public class PhoneErrorCodes
{
    public string IsRequired { get; set; }
    public string IsInvalid { get; set; }

    public PhoneErrorCodes(string isRequired, string isInvalid)
    {
        IsRequired = isRequired;
        IsInvalid = isInvalid;
    }

    public static PhoneErrorCodes Create()
        => new(
            Account.PhoneNumberIsRequired.Code,
            Account.PhoneNumberIsInvalid.Code);
}

public class PasswordConfirmationErrorCodes
{
    public string IsRequired { get; set; }
    public string DoesNotMatch { get; set; }

    public PasswordConfirmationErrorCodes(string isRequired, string doesNotMatch)
    {
        IsRequired = isRequired;
        DoesNotMatch = doesNotMatch;
    }

    public static PasswordConfirmationErrorCodes Create()
        => new(
            Account.PasswordConfirmationIsRequired.Code,
            Account.PasswordConfirmationDoesntMatch.Code);
}

public class PasswordErrorCodes
{
    public string IsRequired { get; set; }
    public string IsTooShort { get; set; }
    public string IsTooLong { get; set; }
    public string LacksSpecialChars { get; set; }

    public PasswordErrorCodes(string isRequired, string isTooShort, string isTooLong, string lacksSpecialChars)
    {
        IsRequired = isRequired;
        IsTooShort = isTooShort;
        IsTooLong = isTooLong;
        LacksSpecialChars = lacksSpecialChars;
    }

    public static PasswordErrorCodes Create()
        => new(
            Account.PasswordIsRequired.Code,
            Account.PasswordIsTooShort.Code,
            Account.PasswordIsTooLong.Code,
            Account.PasswordLacksSpecialCharacters.Code);
}

public class EmailErrorCodes
{
    public string IsRegisteredAlready { get; set; }
    public string IsRequired { get; set; }
    public string IsInvalid { get; set; }

    public EmailErrorCodes(
        string isRegisteredAlready,
        string isInvalid,
        string isRequired)
    {
        IsRegisteredAlready = isRegisteredAlready;
        IsInvalid = isInvalid;
        IsRequired = isRequired;
    }

    public static EmailErrorCodes Create()
        => new(
            Account.EmailIsRegisteredAlready.Code,
            Account.EmailIsInvalid.Code,
            Account.EmailIsRequired.Code);
}
