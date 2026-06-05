using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using MediatR;

namespace EnergyManagement.Server.Application.Commands;

public sealed record RegisterClientAccountCommand(
    string Email,
    string Password)
    : IRequest<Result<RegisterClientAccountResponse, IReadOnlyList<Error>>>;

public sealed record RegisterClientAccountResponse(
    long AccountId,
    string Email);

public sealed record LoginClientAccountCommand(
    string Email,
    string Password)
    : IRequest<Result<LoginClientAccountResponse, IReadOnlyList<Error>>>;

public sealed record LoginClientAccountResponse(
    long AccountId,
    string Email,
    string Role,
    bool IsActive,
    Account Account);

public sealed record CreateIndividualApplicantPartyCommand(
    long ClientAccountId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber)
    : IRequest<Result<CreateApplicantPartyResponse, IReadOnlyList<Error>>>;

public sealed record CreateIndividualEntrepreneurApplicantPartyCommand(
    long ClientAccountId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Inn,
    string Ogrnip,
    string Email,
    string PhoneNumber)
    : IRequest<Result<CreateApplicantPartyResponse, IReadOnlyList<Error>>>;

public sealed record CreateLegalEntityApplicantPartyCommand(
    long ClientAccountId,
    string OrganizationName,
    string Inn,
    string Kpp,
    string Ogrn,
    string Email,
    string PhoneNumber)
    : IRequest<Result<CreateApplicantPartyResponse, IReadOnlyList<Error>>>;

public sealed record CreateApplicantPartyResponse(
    long ApplicantPartyId,
    long ClientAccountId);

public sealed record CreateConnectionRequestCommand(
    long ClientAccountId,
    string? ApplicantContextType,
    long? ExistingApplicantPartyId,
    CreateConnectionRequestNewApplicant? NewApplicantParty,
    string Details,
    string PostalCode,
    string Region,
    string City,
    string Street,
    string House,
    string? Building,
    string? Apartment)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;

public sealed record CreateConnectionRequestNewApplicant(
    string ApplicantPartyType,
    string? FirstName,
    string? MiddleName,
    string? LastName,
    string? OrganizationName,
    string? Inn,
    string? Kpp,
    string? Ogrn,
    string? Ogrnip,
    string Email,
    string PhoneNumber);

public sealed record MakeApplicantPartyCurrentDefaultCommand(
    long ClientAccountId,
    long ApplicantPartyId)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;

public sealed record EmployeeStartRequestReviewCommand(
    long EmployeeId,
    long RequestId)
    : IRequest<EmployeeStartRequestReviewCommandResult>;

public sealed record EmployeeStartRequestReviewCommandResult(
    EmployeeStartRequestReviewCommandStatus Status,
    IReadOnlyList<Error> Errors)
{
    public static EmployeeStartRequestReviewCommandResult Started()
    {
        return new EmployeeStartRequestReviewCommandResult(
            EmployeeStartRequestReviewCommandStatus.Started,
            []);
    }

    public static EmployeeStartRequestReviewCommandResult NotFound()
    {
        return new EmployeeStartRequestReviewCommandResult(
            EmployeeStartRequestReviewCommandStatus.NotFound,
            []);
    }

    public static EmployeeStartRequestReviewCommandResult Forbidden()
    {
        return new EmployeeStartRequestReviewCommandResult(
            EmployeeStartRequestReviewCommandStatus.Forbidden,
            []);
    }

    public static EmployeeStartRequestReviewCommandResult Invalid(IReadOnlyList<Error> errors)
    {
        return new EmployeeStartRequestReviewCommandResult(
            EmployeeStartRequestReviewCommandStatus.Invalid,
            errors);
    }
}

public enum EmployeeStartRequestReviewCommandStatus
{
    Started = 1,
    NotFound = 2,
    Forbidden = 3,
    Invalid = 4
}

public sealed record EmployeeApproveRequestReviewCommand(
    long EmployeeId,
    long RequestId)
    : IRequest<EmployeeApproveRequestReviewCommandResult>;

public sealed record EmployeeApproveRequestReviewCommandResult(
    EmployeeApproveRequestReviewCommandStatus Status,
    IReadOnlyList<Error> Errors)
{
    public static EmployeeApproveRequestReviewCommandResult Approved()
    {
        return new EmployeeApproveRequestReviewCommandResult(
            EmployeeApproveRequestReviewCommandStatus.Approved,
            []);
    }

    public static EmployeeApproveRequestReviewCommandResult NotFound()
    {
        return new EmployeeApproveRequestReviewCommandResult(
            EmployeeApproveRequestReviewCommandStatus.NotFound,
            []);
    }

    public static EmployeeApproveRequestReviewCommandResult Forbidden()
    {
        return new EmployeeApproveRequestReviewCommandResult(
            EmployeeApproveRequestReviewCommandStatus.Forbidden,
            []);
    }

    public static EmployeeApproveRequestReviewCommandResult Invalid(IReadOnlyList<Error> errors)
    {
        return new EmployeeApproveRequestReviewCommandResult(
            EmployeeApproveRequestReviewCommandStatus.Invalid,
            errors);
    }
}

public enum EmployeeApproveRequestReviewCommandStatus
{
    Approved = 1,
    NotFound = 2,
    Forbidden = 3,
    Invalid = 4
}

public sealed record EmployeeRejectRequestReviewCommand(
    long EmployeeId,
    long RequestId,
    string Feedback)
    : IRequest<EmployeeRejectRequestReviewCommandResult>;

public sealed record EmployeeRejectRequestReviewCommandResult(
    EmployeeRejectRequestReviewCommandStatus Status,
    IReadOnlyList<Error> Errors)
{
    public static EmployeeRejectRequestReviewCommandResult Rejected()
    {
        return new EmployeeRejectRequestReviewCommandResult(
            EmployeeRejectRequestReviewCommandStatus.Rejected,
            []);
    }

    public static EmployeeRejectRequestReviewCommandResult NotFound()
    {
        return new EmployeeRejectRequestReviewCommandResult(
            EmployeeRejectRequestReviewCommandStatus.NotFound,
            []);
    }

    public static EmployeeRejectRequestReviewCommandResult Forbidden()
    {
        return new EmployeeRejectRequestReviewCommandResult(
            EmployeeRejectRequestReviewCommandStatus.Forbidden,
            []);
    }

    public static EmployeeRejectRequestReviewCommandResult Invalid(IReadOnlyList<Error> errors)
    {
        return new EmployeeRejectRequestReviewCommandResult(
            EmployeeRejectRequestReviewCommandStatus.Invalid,
            errors);
    }
}

public enum EmployeeRejectRequestReviewCommandStatus
{
    Rejected = 1,
    NotFound = 2,
    Forbidden = 3,
    Invalid = 4
}

public sealed record RunApplicantPartyVerificationFromRequestCommand(
    long EmployeeId,
    long RequestId)
    : IRequest<RunApplicantPartyVerificationFromRequestCommandResult>;

public sealed record RunApplicantPartyVerificationResponse(
    long RequestId,
    long ApplicantPartyId,
    string VerificationStatus,
    string MockResult,
    string? Message);

public sealed record RunApplicantPartyVerificationFromRequestCommandResult(
    RunApplicantPartyVerificationFromRequestCommandStatus Status,
    RunApplicantPartyVerificationResponse? Response,
    IReadOnlyList<Error> Errors)
{
    public static RunApplicantPartyVerificationFromRequestCommandResult Verified(
        RunApplicantPartyVerificationResponse response)
    {
        return new RunApplicantPartyVerificationFromRequestCommandResult(
            RunApplicantPartyVerificationFromRequestCommandStatus.Verified,
            response,
            []);
    }

    public static RunApplicantPartyVerificationFromRequestCommandResult NotFound()
    {
        return new RunApplicantPartyVerificationFromRequestCommandResult(
            RunApplicantPartyVerificationFromRequestCommandStatus.NotFound,
            null,
            []);
    }

    public static RunApplicantPartyVerificationFromRequestCommandResult Forbidden()
    {
        return new RunApplicantPartyVerificationFromRequestCommandResult(
            RunApplicantPartyVerificationFromRequestCommandStatus.Forbidden,
            null,
            []);
    }

    public static RunApplicantPartyVerificationFromRequestCommandResult Invalid(IReadOnlyList<Error> errors)
    {
        return new RunApplicantPartyVerificationFromRequestCommandResult(
            RunApplicantPartyVerificationFromRequestCommandStatus.Invalid,
            null,
            errors);
    }
}

public enum RunApplicantPartyVerificationFromRequestCommandStatus
{
    Verified = 1,
    NotFound = 2,
    Forbidden = 3,
    Invalid = 4
}
