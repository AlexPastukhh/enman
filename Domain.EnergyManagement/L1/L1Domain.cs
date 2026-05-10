using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public enum AccountRole
{
    Client
}

public abstract class L1Entity : Entity
{
    protected static long GuardPersistedReferenceId(
        L1Entity referencedEntity,
        string parameterName)
    {
        Guard.IsNotNull(referencedEntity);

        if (referencedEntity.Id <= 0)
        {
            throw new ArgumentException(
                "Referenced aggregate must already be persisted and have Id > 0.",
                parameterName);
        }

        return referencedEntity.Id;
    }
}

public abstract class Account : L1Entity
{
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public AccountRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    protected Account(Email email, PasswordHash passwordHash, AccountRole role)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(passwordHash);

        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected Account()
    {
        Email = null!;
        PasswordHash = null!;
    }
}

public sealed class ClientAccount : Account
{
    private ClientAccount(Email email, PasswordHash passwordHash)
        : base(email, passwordHash, AccountRole.Client)
    {
    }

    private ClientAccount()
    {
    }

    public static Result<ClientAccount, IReadOnlyList<Error>> Create(
        Email email,
        PasswordHash passwordHash)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(passwordHash);

        return Result.Success<ClientAccount, IReadOnlyList<Error>>(
            new ClientAccount(email, passwordHash));
    }
}

public enum ApplicantPartyType
{
    Individual
}

public abstract class ApplicantParty : L1Entity
{
    public long ClientAccountId { get; private set; }
    public ApplicantPartyType ApplicantPartyType { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    protected ApplicantParty(
        ClientAccount clientAccount,
        ApplicantPartyType applicantPartyType,
        Email email,
        PhoneNumber phoneNumber)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(phoneNumber);

        ClientAccountId = GuardPersistedReferenceId(
            clientAccount,
            nameof(clientAccount));
        ApplicantPartyType = applicantPartyType;
        Email = email;
        PhoneNumber = phoneNumber;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected ApplicantParty()
    {
        Email = null!;
        PhoneNumber = null!;
    }

    public abstract string GetDisplayName();
}

public sealed class IndividualApplicantParty : ApplicantParty
{
    public FullName FullName { get; private set; }

    private IndividualApplicantParty(
        ClientAccount clientAccount,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
        : base(clientAccount, ApplicantPartyType.Individual, email, phoneNumber)
    {
        Guard.IsNotNull(fullName);
        FullName = fullName;
    }

    private IndividualApplicantParty()
    {
        FullName = null!;
    }

    public static Result<IndividualApplicantParty, IReadOnlyList<Error>> Create(
        ClientAccount clientAccount,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
    {
        Guard.IsNotNull(clientAccount);
        Guard.IsNotNull(fullName);
        Guard.IsNotNull(email);
        Guard.IsNotNull(phoneNumber);

        return Result.Success<IndividualApplicantParty, IReadOnlyList<Error>>(
            new IndividualApplicantParty(clientAccount, fullName, email, phoneNumber));
    }

    public override string GetDisplayName()
    {
        return $"{FullName.LastName} {FullName.FirstName} {FullName.MiddleName}";
    }
}

public enum ClientRequestType
{
    Connection
}

public enum RequestStatus
{
    Submitted
}

public abstract class ClientRequest : L1Entity
{
    public long ApplicantPartyId { get; private set; }
    public ClientRequestType RequestType { get; private set; }
    public RequestStatus Status { get; private set; }
    public string Details { get; private set; }
    public Address ObjectAddress { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    protected ClientRequest(
        ApplicantParty applicantParty,
        ClientRequestType requestType,
        string details,
        Address objectAddress)
    {
        Guard.IsNotNull(objectAddress);

        ApplicantPartyId = GuardPersistedReferenceId(
            applicantParty,
            nameof(applicantParty));
        RequestType = requestType;
        Details = details;
        ObjectAddress = objectAddress;
        Status = RequestStatus.Submitted;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected ClientRequest()
    {
        Details = null!;
        ObjectAddress = null!;
    }

    protected static IReadOnlyList<Error> ValidateCreateInput(string details)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(details))
        {
            errors.Add(Errors.ClientRequestErrors.ClientRequestTextIsRequired);
        }

        if (details is not null && details.Length > 3000)
        {
            errors.Add(Errors.ClientRequestErrors.ClientRequestTextIsTooLong);
        }

        return errors;
    }
}

public sealed class ConnectionRequest : ClientRequest
{
    private ConnectionRequest(
        ApplicantParty applicantParty,
        string details,
        Address objectAddress)
        : base(applicantParty, ClientRequestType.Connection, details, objectAddress)
    {
    }

    private ConnectionRequest()
    {
    }

    public static Result<ConnectionRequest, IReadOnlyList<Error>> Create(
        ApplicantParty applicantParty,
        string details,
        Address objectAddress)
    {
        Guard.IsNotNull(applicantParty);
        Guard.IsNotNull(objectAddress);

        var errors = ValidateCreateInput(details);
        if (errors.Any())
        {
            return Result.Failure<ConnectionRequest, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<ConnectionRequest, IReadOnlyList<Error>>(
            new ConnectionRequest(applicantParty, details, objectAddress));
    }
}
