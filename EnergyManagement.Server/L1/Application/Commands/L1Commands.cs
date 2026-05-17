using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed record L1RegisterClientAccountCommand(
    string Email,
    string Password)
    : IRequest<Result<L1RegisterClientAccountResponse, IReadOnlyList<Error>>>;

public sealed record L1RegisterClientAccountResponse(
    long AccountId,
    string Email);

public sealed record L1LoginClientAccountCommand(
    string Email,
    string Password)
    : IRequest<Result<L1LoginClientAccountResponse, IReadOnlyList<Error>>>;

public sealed record L1LoginClientAccountResponse(
    long AccountId,
    string Email,
    string Role,
    bool IsActive,
    Account Account);

public sealed record L1CreateIndividualApplicantPartyCommand(
    long ClientAccountId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber)
    : IRequest<Result<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>>;

public sealed record L1CreateIndividualApplicantPartyResponse(
    long ApplicantPartyId,
    long ClientAccountId);

public sealed record L1CreateConnectionRequestCommand(
    long ClientAccountId,
    string? ApplicantContextType,
    long? ExistingApplicantPartyId,
    L1CreateConnectionRequestNewApplicant? NewApplicantParty,
    string Details,
    string PostalCode,
    string Region,
    string City,
    string Street,
    string House,
    string? Building,
    string? Apartment)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;

public sealed record L1CreateConnectionRequestNewApplicant(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber);

public sealed record L1MakeApplicantPartyCurrentDefaultCommand(
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
