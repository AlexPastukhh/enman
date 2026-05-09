using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public enum AccountRole
{
    Client,
    Employee
}

public abstract class Account : Entity
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public AccountRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    protected Account(Email email, Password password, AccountRole role)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(password);

        Email = email;
        Password = password;
        Role = role;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected Account()
    {
        Email = null!;
        Password = null!;
    }
}

public sealed class ClientAccount : Account
{
    private readonly List<ApplicantParty> _applicantParties = [];
    public IReadOnlyList<ApplicantParty> ApplicantParties => _applicantParties.AsReadOnly();

    private ClientAccount(Email email, Password password)
        : base(email, password, AccountRole.Client)
    {
    }

    private ClientAccount()
    {
    }

    public static Result<ClientAccount, IReadOnlyList<Error>> Create(
        Email email,
        Password password)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(password);

        return Result.Success<ClientAccount, IReadOnlyList<Error>>(
            new ClientAccount(email, password));
    }

    public void AddApplicantPartyOrThrow(ApplicantParty applicantParty)
    {
        Guard.IsNotNull(applicantParty);

        if (applicantParty.ClientAccount != this)
        {
            throw new ArgumentException("Applicant party account does not match.");
        }

        _applicantParties.Add(applicantParty);
    }
}

public sealed class EmployeeAccount : Account
{
    private readonly List<RequestReview> _requestReviews = [];
    public IReadOnlyList<RequestReview> RequestReviews => _requestReviews.AsReadOnly();

    private EmployeeAccount(Email email, Password password)
        : base(email, password, AccountRole.Employee)
    {
    }

    private EmployeeAccount()
    {
    }

    public static Result<EmployeeAccount, IReadOnlyList<Error>> Create(
        Email email,
        Password password)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(password);

        return Result.Success<EmployeeAccount, IReadOnlyList<Error>>(
            new EmployeeAccount(email, password));
    }

    public void AddReviewOrThrow(RequestReview review)
    {
        Guard.IsNotNull(review);

        if (review.Employee != this)
        {
            throw new ArgumentException("Review employee does not match.");
        }

        _requestReviews.Add(review);
    }
}

public enum ApplicantPartyType
{
    Individual
}

public abstract class ApplicantParty : Entity
{
    public ClientAccount ClientAccount { get; private set; }
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
        Guard.IsNotNull(clientAccount);
        Guard.IsNotNull(email);
        Guard.IsNotNull(phoneNumber);

        ClientAccount = clientAccount;
        ApplicantPartyType = applicantPartyType;
        Email = email;
        PhoneNumber = phoneNumber;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected ApplicantParty()
    {
        ClientAccount = null!;
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
    Connection,
    MeteringDevice
}

public enum RequestStatus
{
    Submitted,
    InReview,
    Approved,
    Rejected,
    ContractDraftSent
}

public abstract class ClientRequest : Entity
{
    private readonly List<RequestReview> _reviews = [];

    public string Number { get; private set; }
    public ApplicantParty ApplicantParty { get; private set; }
    public ClientRequestType RequestType { get; private set; }
    public RequestStatus Status { get; private set; }
    public string Details { get; private set; }
    public Address ObjectAddress { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public EmployeeAccount? AssignedEmployee { get; private set; }
    public ContractDraft? ContractDraft { get; private set; }
    public IReadOnlyList<RequestReview> Reviews => _reviews.AsReadOnly();

    protected ClientRequest(
        ApplicantParty applicantParty,
        ClientRequestType requestType,
        string details,
        Address objectAddress)
    {
        Guard.IsNotNull(applicantParty);
        Guard.IsNotNull(objectAddress);

        ApplicantParty = applicantParty;
        RequestType = requestType;
        Details = details;
        ObjectAddress = objectAddress;
        Status = RequestStatus.Submitted;
        CreatedAt = DateTimeOffset.UtcNow;
        Number = $"REQ-{CreatedAt:yyyyMMddHHmmssfff}";
    }

    protected ClientRequest()
    {
        Number = null!;
        ApplicantParty = null!;
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

    public UnitResult<IReadOnlyList<Error>> TakeForReview(EmployeeAccount employee)
    {
        Guard.IsNotNull(employee);

        if (Status != RequestStatus.Submitted)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.General.ValueIsInvalid]);
        }

        AssignedEmployee = employee;
        Status = RequestStatus.InReview;
        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public Result<RequestReview, IReadOnlyList<Error>> Approve(
        EmployeeAccount employee,
        string comment)
    {
        Guard.IsNotNull(employee);

        if (Status != RequestStatus.InReview)
        {
            return Result.Failure<RequestReview, IReadOnlyList<Error>>(
                [Errors.General.ValueIsInvalid]);
        }

        var review = RequestReview.Create(this, employee, ReviewDecision.Approved, comment);
        _reviews.Add(review);
        employee.AddReviewOrThrow(review);
        Status = RequestStatus.Approved;

        return Result.Success<RequestReview, IReadOnlyList<Error>>(review);
    }

    public Result<RequestReview, IReadOnlyList<Error>> Reject(
        EmployeeAccount employee,
        string reason)
    {
        Guard.IsNotNull(employee);

        if (Status != RequestStatus.InReview)
        {
            return Result.Failure<RequestReview, IReadOnlyList<Error>>(
                [Errors.General.ValueIsInvalid]);
        }

        var review = RequestReview.Create(this, employee, ReviewDecision.Rejected, reason);
        _reviews.Add(review);
        employee.AddReviewOrThrow(review);
        Status = RequestStatus.Rejected;

        return Result.Success<RequestReview, IReadOnlyList<Error>>(review);
    }

    public void AttachContractDraftOrThrow(ContractDraft contractDraft)
    {
        Guard.IsNotNull(contractDraft);

        if (contractDraft.Request != this)
        {
            throw new ArgumentException("Contract draft request does not match.");
        }

        ContractDraft = contractDraft;
    }

    public void MarkContractDraftSent()
    {
        Status = RequestStatus.ContractDraftSent;
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

public sealed class MeteringDeviceRequest : ClientRequest
{
    private MeteringDeviceRequest(
        ApplicantParty applicantParty,
        string details,
        Address objectAddress)
        : base(applicantParty, ClientRequestType.MeteringDevice, details, objectAddress)
    {
    }

    private MeteringDeviceRequest()
    {
    }

    public static Result<MeteringDeviceRequest, IReadOnlyList<Error>> Create(
        ApplicantParty applicantParty,
        string details,
        Address objectAddress)
    {
        Guard.IsNotNull(applicantParty);
        Guard.IsNotNull(objectAddress);

        var errors = ValidateCreateInput(details);
        if (errors.Any())
        {
            return Result.Failure<MeteringDeviceRequest, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<MeteringDeviceRequest, IReadOnlyList<Error>>(
            new MeteringDeviceRequest(applicantParty, details, objectAddress));
    }
}

public enum ReviewDecision
{
    Approved,
    Rejected
}

public sealed class RequestReview : Entity
{
    public ClientRequest Request { get; private set; }
    public EmployeeAccount Employee { get; private set; }
    public ReviewDecision Decision { get; private set; }
    public string Comment { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private RequestReview(
        ClientRequest request,
        EmployeeAccount employee,
        ReviewDecision decision,
        string comment)
    {
        Guard.IsNotNull(request);
        Guard.IsNotNull(employee);

        Request = request;
        Employee = employee;
        Decision = decision;
        Comment = comment;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private RequestReview()
    {
        Request = null!;
        Employee = null!;
        Comment = null!;
    }

    internal static RequestReview Create(
        ClientRequest request,
        EmployeeAccount employee,
        ReviewDecision decision,
        string comment)
    {
        return new RequestReview(request, employee, decision, comment);
    }
}

public enum ContractDraftStatus
{
    Created,
    Sent
}

public sealed class ContractDraft : Entity
{
    public ClientRequest Request { get; private set; }
    public string ContractNumber { get; private set; }
    public ClientRequestType RequestType { get; private set; }
    public ContractDraftStatus Status { get; private set; }
    public string Text { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public EmployeeAccount CreatedByEmployee { get; private set; }

    private ContractDraft(
        ClientRequest request,
        string contractNumber,
        string text,
        EmployeeAccount createdByEmployee)
    {
        Guard.IsNotNull(request);
        Guard.IsNotNull(createdByEmployee);
        Guard.IsNotNullOrWhiteSpace(contractNumber);
        Guard.IsNotNullOrWhiteSpace(text);

        Request = request;
        ContractNumber = contractNumber;
        RequestType = request.RequestType;
        Text = text;
        CreatedByEmployee = createdByEmployee;
        Status = ContractDraftStatus.Created;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private ContractDraft()
    {
        Request = null!;
        ContractNumber = null!;
        Text = null!;
        CreatedByEmployee = null!;
    }

    public static Result<ContractDraft, IReadOnlyList<Error>> Create(
        ClientRequest request,
        string contractNumber,
        string text,
        EmployeeAccount createdByEmployee)
    {
        Guard.IsNotNull(request);
        Guard.IsNotNull(createdByEmployee);
        Guard.IsNotNullOrWhiteSpace(contractNumber);
        Guard.IsNotNullOrWhiteSpace(text);

        if (request.Status != RequestStatus.Approved)
        {
            return Result.Failure<ContractDraft, IReadOnlyList<Error>>(
                [Errors.General.ValueIsInvalid]);
        }

        var contractDraft = new ContractDraft(request, contractNumber, text, createdByEmployee);
        request.AttachContractDraftOrThrow(contractDraft);

        return Result.Success<ContractDraft, IReadOnlyList<Error>>(contractDraft);
    }

    public void MarkSent()
    {
        Status = ContractDraftStatus.Sent;
        Request.MarkContractDraftSent();
    }
}

public sealed class EmailNotification : Entity
{
    public ClientRequest Request { get; private set; }
    public Email RecipientEmail { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? SentAt { get; private set; }

    private EmailNotification(
        ClientRequest request,
        Email recipientEmail,
        string subject,
        string body)
    {
        Guard.IsNotNull(request);
        Guard.IsNotNull(recipientEmail);
        Guard.IsNotNullOrWhiteSpace(subject);
        Guard.IsNotNullOrWhiteSpace(body);

        Request = request;
        RecipientEmail = recipientEmail;
        Subject = subject;
        Body = body;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private EmailNotification()
    {
        Request = null!;
        RecipientEmail = null!;
        Subject = null!;
        Body = null!;
    }

    public static Result<EmailNotification, IReadOnlyList<Error>> Create(
        ClientRequest request,
        Email recipientEmail,
        string subject,
        string body)
    {
        Guard.IsNotNull(request);
        Guard.IsNotNull(recipientEmail);
        Guard.IsNotNullOrWhiteSpace(subject);
        Guard.IsNotNullOrWhiteSpace(body);

        return Result.Success<EmailNotification, IReadOnlyList<Error>>(
            new EmailNotification(request, recipientEmail, subject, body));
    }

    public void MarkSent()
    {
        SentAt = DateTimeOffset.UtcNow;
    }
}
