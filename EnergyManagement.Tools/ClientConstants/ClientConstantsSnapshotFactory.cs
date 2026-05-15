using EnergyManagement.Server.Api.Contracts.Auth;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api.Routes;
using EnergyManagement.Server.Data;

namespace EnergyManagement.Tools.ClientConstants;

public sealed class ClientConstantsSnapshotFactory
{
    public ClientConstantsSnapshot Create()
        => new(CreateConstantsSnapshot(), CreateErrorCodesSnapshot());

    public ConstantsSnapshot CreateConstantsSnapshot()
        => new(
            AuthConstants: new AuthConstantsSnapshot(
                RegisterIndividualClient: new RegisterIndividualClientConstantsSnapshot(
                    Email: CreateField(AuthFieldNames.Register.Email, nameof(RegisterClientDto.Email)),
                    Password: CreateField(AuthFieldNames.Register.Password, nameof(RegisterClientDto.Password)),
                    PasswordConfirmation: CreateField(
                        AuthFieldNames.Register.PasswordConfirmation,
                        nameof(RegisterClientDto.PasswordConfirmation))),
                Login: new LoginConstantsSnapshot(
                    Email: CreateField(AuthFieldNames.Login.Email, nameof(LoginDto.Email)),
                    Password: CreateField(AuthFieldNames.Login.Password, nameof(LoginDto.Password))),
                ProvideIndividualClientsData: new ProvideIndividualClientsDataConstantsSnapshot(
                    PhoneNumber: CreateField(
                        AuthFieldNames.ProvideIndividualClientData.Phone,
                        nameof(ProvideIndividualClientsDataDto.PhoneNumber)),
                    FirstName: CreateField(
                        JsonField.Of<FullNameDto>(x => x.FirstName),
                        nameof(FullNameDto.FirstName)),
                    MiddleName: CreateField(
                        JsonField.Of<FullNameDto>(x => x.MiddleName),
                        nameof(FullNameDto.MiddleName)),
                    LastName: CreateField(
                        JsonField.Of<FullNameDto>(x => x.LastName),
                        nameof(FullNameDto.LastName)))),
            GeneralConstants: new GeneralConstantsSnapshot(
                ValidationErrorStatusCode: ProblemDetailsContract.ValidationStatusCode,
                ErrorsCollectionName: ProblemDetailsContract.ErrorsExtension,
                ExceptionExtensionName: ProblemDetailsContract.ExceptionExtension),
            Routes: new RoutesSnapshot(
                RegisterIndividualPath: AuthRoutes.RegisterIndividualPath,
                ProvideIndividualClientsDataPath: AuthRoutes.ProvideIndividualClientsDataPath,
                LoginPath: AuthRoutes.LoginPath,
                GetUserPath: AuthRoutes.GetUserPath));

    public ErrorObject CreateErrorCodesSnapshot()
        => ErrorObject.Create();

    private static FormFieldWithDtoSnapshot CreateField(string fieldName, string dtoFieldName)
        => new(fieldName, dtoFieldName);
}

public sealed record ClientConstantsSnapshot(
    ConstantsSnapshot Constants,
    ErrorObject ErrorCodes);

public sealed record ConstantsSnapshot(
    AuthConstantsSnapshot AuthConstants,
    GeneralConstantsSnapshot GeneralConstants,
    RoutesSnapshot Routes);

public sealed record AuthConstantsSnapshot(
    RegisterIndividualClientConstantsSnapshot RegisterIndividualClient,
    LoginConstantsSnapshot Login,
    ProvideIndividualClientsDataConstantsSnapshot ProvideIndividualClientsData);

public sealed record RegisterIndividualClientConstantsSnapshot(
    FormFieldWithDtoSnapshot Email,
    FormFieldWithDtoSnapshot Password,
    FormFieldWithDtoSnapshot PasswordConfirmation);

public sealed record LoginConstantsSnapshot(
    FormFieldWithDtoSnapshot Email,
    FormFieldWithDtoSnapshot Password);

public sealed record ProvideIndividualClientsDataConstantsSnapshot(
    FormFieldWithDtoSnapshot PhoneNumber,
    FormFieldWithDtoSnapshot FirstName,
    FormFieldWithDtoSnapshot MiddleName,
    FormFieldWithDtoSnapshot LastName);

public sealed record FormFieldWithDtoSnapshot(
    string FieldName,
    string DtoFieldName);

public sealed record GeneralConstantsSnapshot(
    int ValidationErrorStatusCode,
    string ErrorsCollectionName,
    string ExceptionExtensionName);

public sealed record RoutesSnapshot(
    string RegisterIndividualPath,
    string ProvideIndividualClientsDataPath,
    string LoginPath,
    string GetUserPath);
