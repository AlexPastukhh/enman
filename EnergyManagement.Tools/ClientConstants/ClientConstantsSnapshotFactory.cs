using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Api.Validation;

namespace EnergyManagement.Tools.ClientConstants;

public sealed class ClientConstantsSnapshotFactory
{
    public ClientConstantsSnapshot Create()
        => new(CreateConstantsSnapshot(), CreateErrorCodesSnapshot());

    public ConstantsSnapshot CreateConstantsSnapshot()
        => new(
            L1AuthConstants: new L1AuthConstantsSnapshot(
                RegisterClientAccount: new L1RegisterClientAccountConstantsSnapshot(
                    Email: CreateField(L1FieldNames.Auth.Email, nameof(L1RegisterClientAccountDto.Email)),
                    Password: CreateField(L1FieldNames.Auth.Password, nameof(L1RegisterClientAccountDto.Password))),
                Login: new L1LoginConstantsSnapshot(
                    Email: CreateField(L1FieldNames.Auth.Email, nameof(L1LoginRequest.Email)),
                    Password: CreateField(L1FieldNames.Auth.Password, nameof(L1LoginRequest.Password)))),
            L1ApplicantPartyConstants: new L1ApplicantPartyConstantsSnapshot(
                CreateIndividualApplicantParty: new L1CreateIndividualApplicantPartyConstantsSnapshot(
                    PhoneNumber: CreateField(
                        L1FieldNames.ApplicantParty.PhoneNumber,
                        nameof(L1CreateIndividualApplicantPartyDto.PhoneNumber)),
                    Email: CreateField(
                        L1FieldNames.ApplicantParty.Email,
                        nameof(L1CreateIndividualApplicantPartyDto.Email)),
                    FirstName: CreateField(
                        L1FieldNames.FullName.FirstName,
                        nameof(L1FullNameDto.FirstName)),
                    MiddleName: CreateField(
                        L1FieldNames.FullName.MiddleName,
                        nameof(L1FullNameDto.MiddleName)),
                    LastName: CreateField(
                        L1FieldNames.FullName.LastName,
                        nameof(L1FullNameDto.LastName)))),
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
    L1AuthConstantsSnapshot L1AuthConstants,
    L1ApplicantPartyConstantsSnapshot L1ApplicantPartyConstants,
    ProblemDetailsConstantsSnapshot ProblemDetails);

public sealed record L1AuthConstantsSnapshot(
    L1RegisterClientAccountConstantsSnapshot RegisterClientAccount,
    L1LoginConstantsSnapshot Login);

public sealed record L1RegisterClientAccountConstantsSnapshot(
    FormFieldWithDtoSnapshot Email,
    FormFieldWithDtoSnapshot Password);

public sealed record L1LoginConstantsSnapshot(
    FormFieldWithDtoSnapshot Email,
    FormFieldWithDtoSnapshot Password);

public sealed record L1ApplicantPartyConstantsSnapshot(
    L1CreateIndividualApplicantPartyConstantsSnapshot CreateIndividualApplicantParty);

public sealed record L1CreateIndividualApplicantPartyConstantsSnapshot(
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
