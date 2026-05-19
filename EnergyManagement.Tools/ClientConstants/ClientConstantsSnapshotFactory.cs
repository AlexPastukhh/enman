using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Api.Validation;

namespace EnergyManagement.Tools.ClientConstants;

public sealed class ClientConstantsSnapshotFactory
{
    public ClientConstantsSnapshot Create()
        => new(CreateConstantsSnapshot(), CreateErrorCodesSnapshot());

    public ConstantsSnapshot CreateConstantsSnapshot()
        => new(
            AuthConstants: new AuthConstantsSnapshot(
                RegisterClientAccount: new RegisterClientAccountConstantsSnapshot(
                    Email: CreateField(FieldNames.Auth.Email, nameof(RegisterClientAccountDto.Email)),
                    Password: CreateField(FieldNames.Auth.Password, nameof(RegisterClientAccountDto.Password))),
                Login: new LoginConstantsSnapshot(
                    Email: CreateField(FieldNames.Auth.Email, nameof(LoginRequestDto.Email)),
                    Password: CreateField(FieldNames.Auth.Password, nameof(LoginRequestDto.Password)))),
            ApplicantPartyConstants: new ApplicantPartyConstantsSnapshot(
                CreateIndividualApplicantParty: new CreateIndividualApplicantPartyConstantsSnapshot(
                    PhoneNumber: CreateField(
                        FieldNames.ApplicantParty.PhoneNumber,
                        nameof(CreateIndividualApplicantPartyDto.PhoneNumber)),
                    Email: CreateField(
                        FieldNames.ApplicantParty.Email,
                        nameof(CreateIndividualApplicantPartyDto.Email)),
                    FirstName: CreateField(
                        FieldNames.FullName.FirstName,
                        nameof(FullNameDto.FirstName)),
                    MiddleName: CreateField(
                        FieldNames.FullName.MiddleName,
                        nameof(FullNameDto.MiddleName)),
                    LastName: CreateField(
                        FieldNames.FullName.LastName,
                        nameof(FullNameDto.LastName)))),
            ProblemDetails: new ProblemDetailsConstantsSnapshot(
                ValidationErrorStatusCode: ProblemDetailsContract.ValidationStatusCode,
                ErrorsCollectionName: ProblemDetailsContract.ErrorsExtension,
                ExceptionExtensionName: ProblemDetailsContract.ExceptionExtension));

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
    ApplicantPartyConstantsSnapshot ApplicantPartyConstants,
    ProblemDetailsConstantsSnapshot ProblemDetails);

public sealed record AuthConstantsSnapshot(
    RegisterClientAccountConstantsSnapshot RegisterClientAccount,
    LoginConstantsSnapshot Login);

public sealed record RegisterClientAccountConstantsSnapshot(
    FormFieldWithDtoSnapshot Email,
    FormFieldWithDtoSnapshot Password);

public sealed record LoginConstantsSnapshot(
    FormFieldWithDtoSnapshot Email,
    FormFieldWithDtoSnapshot Password);

public sealed record ApplicantPartyConstantsSnapshot(
    CreateIndividualApplicantPartyConstantsSnapshot CreateIndividualApplicantParty);

public sealed record CreateIndividualApplicantPartyConstantsSnapshot(
    FormFieldWithDtoSnapshot PhoneNumber,
    FormFieldWithDtoSnapshot Email,
    FormFieldWithDtoSnapshot FirstName,
    FormFieldWithDtoSnapshot MiddleName,
    FormFieldWithDtoSnapshot LastName);

public sealed record FormFieldWithDtoSnapshot(
    string FieldName,
    string DtoFieldName);

public sealed record ProblemDetailsConstantsSnapshot(
    int ValidationErrorStatusCode,
    string ErrorsCollectionName,
    string ExceptionExtensionName);
