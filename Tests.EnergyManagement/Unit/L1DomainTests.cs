using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using static Domain.EnergyManagement.Common.Error.Errors.ClientRequestErrors;

namespace Tests.EnergyManagement.Unit;

public class L1DomainTests : ClientRequestUnitBase
{
    private static ClientAccount CreateClientAccount()
    {
        var email = Email.Create(ValidTestData.ValidEmail).Value;
        var password = Password.Create(ValidTestData.ValidPassword).Value;
        return ClientAccount.Create(email, password).Value;
    }

    private static EmployeeAccount CreateEmployeeAccount()
    {
        var email = Email.Create("employee@example.com").Value;
        var password = Password.Create(ValidTestData.ValidPassword).Value;
        return EmployeeAccount.Create(email, password).Value;
    }

    private static IndividualApplicantParty CreateIndividualApplicantParty()
    {
        var account = CreateClientAccount();
        var (fullName, email, phone, _) = ValidTestData.GetAllIndividualsValues();
        var applicantParty = IndividualApplicantParty.Create(
            account,
            fullName,
            email,
            phone).Value;

        account.AddApplicantPartyOrThrow(applicantParty);
        return applicantParty;
    }

    private static ConnectionRequest CreateSubmittedConnectionRequest()
    {
        var applicantParty = CreateIndividualApplicantParty();
        return ConnectionRequest.Create(
            applicantParty,
            ValidTestData.RequestDetails,
            ValidTestData.GetAddressWithApartment()).Value;
    }

    private static ConnectionRequest CreateInReviewConnectionRequest(
        EmployeeAccount employee)
    {
        var request = CreateSubmittedConnectionRequest();
        request.TakeForReview(employee).IsSuccess.Should().BeTrue();
        return request;
    }

    [Fact]
    public void CreatesClientAccountSuccessfully()
    {
        var (_, email, _, password) = ValidTestData.GetAllIndividualsValues();

        var createAccount = ClientAccount.Create(email, password);

        createAccount.IsSuccess.Should().BeTrue();
        var account = createAccount.Value;
        account.Email.Should().Be(email);
        account.Password.Should().Be(password);
        account.Role.Should().Be(AccountRole.Client);
        account.IsActive.Should().BeTrue();
        account.ApplicantParties.Should().BeEmpty();
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void CantCreateClientAccountWithNullAuthData(
        bool isEmailNull,
        bool isPasswordNull)
    {
        var (_, email, _, password) = ValidTestData.GetAllIndividualsValues();
        if (isEmailNull)
        {
            email = null!;
        }

        if (isPasswordNull)
        {
            password = null!;
        }

        Func<Result<ClientAccount, IReadOnlyList<Error>>> createAccount =
            () => ClientAccount.Create(email, password);

        createAccount.Should().Throw<Exception>();
    }

    [Fact]
    public void CreatesEmployeeAccountSuccessfully()
    {
        var email = Email.Create("employee@example.com").Value;
        var password = Password.Create(ValidTestData.ValidPassword).Value;

        var createAccount = EmployeeAccount.Create(email, password);

        createAccount.IsSuccess.Should().BeTrue();
        var account = createAccount.Value;
        account.Email.Should().Be(email);
        account.Password.Should().Be(password);
        account.Role.Should().Be(AccountRole.Employee);
        account.IsActive.Should().BeTrue();
        account.RequestReviews.Should().BeEmpty();
    }

    [Fact]
    public void CreatesIndividualApplicantPartyAndAddsItToClientAccount()
    {
        var account = CreateClientAccount();
        var (fullName, email, phone, _) = ValidTestData.GetAllIndividualsValues();

        var createApplicantParty = IndividualApplicantParty.Create(
            account,
            fullName,
            email,
            phone);
        var applicantParty = createApplicantParty.Value;
        account.AddApplicantPartyOrThrow(applicantParty);

        createApplicantParty.IsSuccess.Should().BeTrue();
        applicantParty.ClientAccount.Should().Be(account);
        applicantParty.ApplicantPartyType.Should().Be(ApplicantPartyType.Individual);
        applicantParty.FullName.Should().Be(fullName);
        applicantParty.Email.Should().Be(email);
        applicantParty.PhoneNumber.Should().Be(phone);
        applicantParty.GetDisplayName().Should().Be(
            $"{fullName.LastName} {fullName.FirstName} {fullName.MiddleName}");
        account.ApplicantParties.Should().ContainSingle()
            .Which.Should().BeSameAs(applicantParty);
    }

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public void CreatesConnectionRequestSuccessfully(
        string requestDetails,
        Address address)
    {
        var applicantParty = CreateIndividualApplicantParty();

        var createRequest = ConnectionRequest.Create(
            applicantParty,
            requestDetails,
            address);

        createRequest.IsSuccess.Should().BeTrue();
        var request = createRequest.Value;
        request.Should().BeOfType<ConnectionRequest>();
        request.ApplicantParty.Should().Be(applicantParty);
        request.RequestType.Should().Be(ClientRequestType.Connection);
        request.Status.Should().Be(RequestStatus.Submitted);
        request.Details.Should().Be(requestDetails);
        request.ObjectAddress.Should().Be(address);
        request.Reviews.Should().BeEmpty();
    }

    [Fact]
    public void CreatesMeteringDeviceRequestSuccessfully()
    {
        var applicantParty = CreateIndividualApplicantParty();
        var address = ValidTestData.GetAddressWithApartment();

        var createRequest = MeteringDeviceRequest.Create(
            applicantParty,
            ValidTestData.RequestDetails,
            address);

        createRequest.IsSuccess.Should().BeTrue();
        var request = createRequest.Value;
        request.Should().BeOfType<MeteringDeviceRequest>();
        request.ApplicantParty.Should().Be(applicantParty);
        request.RequestType.Should().Be(ClientRequestType.MeteringDevice);
        request.Status.Should().Be(RequestStatus.Submitted);
    }

    [Theory]
    [StringTestData(3001, 3500)]
    public void CantCreateConnectionRequestWithTooLongRequestDetails(
        string requestDetails)
    {
        var applicantParty = CreateIndividualApplicantParty();

        var createRequest = ConnectionRequest.Create(
            applicantParty,
            requestDetails,
            ValidTestData.GetAddressWithApartment());

        createRequest.IsFailure.Should().BeTrue();
        createRequest.Error.Should().Contain(ClientRequestTextIsTooLong);
    }

    [Theory]
    [StringTestData(0)]
    public void CantCreateConnectionRequestWithoutRequestDetails(
        string requestDetails)
    {
        var applicantParty = CreateIndividualApplicantParty();

        var createRequest = ConnectionRequest.Create(
            applicantParty,
            requestDetails,
            ValidTestData.GetAddressWithApartment());

        createRequest.IsFailure.Should().BeTrue();
        createRequest.Error.Should().Contain(ClientRequestTextIsRequired);
    }

    [Fact]
    public void EmployeeTakesRequestForReview()
    {
        var employee = CreateEmployeeAccount();
        var request = CreateSubmittedConnectionRequest();

        var takeForReview = request.TakeForReview(employee);

        takeForReview.IsSuccess.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.InReview);
        request.AssignedEmployee.Should().Be(employee);
    }

    [Fact]
    public void EmployeeApprovesRequestInReview()
    {
        var employee = CreateEmployeeAccount();
        var request = CreateInReviewConnectionRequest(employee);

        var approve = request.Approve(employee, "Approved");

        approve.IsSuccess.Should().BeTrue();
        var review = approve.Value;
        review.Request.Should().Be(request);
        review.Employee.Should().Be(employee);
        review.Decision.Should().Be(ReviewDecision.Approved);
        review.Comment.Should().Be("Approved");
        request.Status.Should().Be(RequestStatus.Approved);
        request.Reviews.Should().ContainSingle().Which.Should().BeSameAs(review);
        employee.RequestReviews.Should().ContainSingle().Which.Should().BeSameAs(review);
    }

    [Fact]
    public void EmployeeRejectsRequestInReview()
    {
        var employee = CreateEmployeeAccount();
        var request = CreateInReviewConnectionRequest(employee);

        var reject = request.Reject(employee, "Rejected");

        reject.IsSuccess.Should().BeTrue();
        var review = reject.Value;
        review.Decision.Should().Be(ReviewDecision.Rejected);
        review.Comment.Should().Be("Rejected");
        request.Status.Should().Be(RequestStatus.Rejected);
        request.Reviews.Should().ContainSingle().Which.Should().BeSameAs(review);
    }

    [Fact]
    public void CantApproveRequestBeforeItIsTakenForReview()
    {
        var employee = CreateEmployeeAccount();
        var request = CreateSubmittedConnectionRequest();

        var approve = request.Approve(employee, "Approved");

        approve.IsFailure.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.Submitted);
        request.Reviews.Should().BeEmpty();
    }

    [Fact]
    public void CreatesContractDraftForApprovedRequest()
    {
        var employee = CreateEmployeeAccount();
        var request = CreateInReviewConnectionRequest(employee);
        request.Approve(employee, "Approved").IsSuccess.Should().BeTrue();

        var createContractDraft = ContractDraft.Create(
            request,
            "CON-001",
            "Contract text",
            employee);

        createContractDraft.IsSuccess.Should().BeTrue();
        var contractDraft = createContractDraft.Value;
        contractDraft.Request.Should().Be(request);
        contractDraft.RequestType.Should().Be(ClientRequestType.Connection);
        contractDraft.Status.Should().Be(ContractDraftStatus.Created);
        contractDraft.CreatedByEmployee.Should().Be(employee);
        request.ContractDraft.Should().Be(contractDraft);
    }

    [Fact]
    public void CantCreateContractDraftBeforeRequestApproval()
    {
        var employee = CreateEmployeeAccount();
        var request = CreateInReviewConnectionRequest(employee);

        var createContractDraft = ContractDraft.Create(
            request,
            "CON-001",
            "Contract text",
            employee);

        createContractDraft.IsFailure.Should().BeTrue();
        request.ContractDraft.Should().BeNull();
    }

    [Fact]
    public void MarksContractDraftSentAndUpdatesRequestStatus()
    {
        var employee = CreateEmployeeAccount();
        var request = CreateInReviewConnectionRequest(employee);
        request.Approve(employee, "Approved").IsSuccess.Should().BeTrue();
        var contractDraft = ContractDraft.Create(
            request,
            "CON-001",
            "Contract text",
            employee).Value;

        contractDraft.MarkSent();

        contractDraft.Status.Should().Be(ContractDraftStatus.Sent);
        request.Status.Should().Be(RequestStatus.ContractDraftSent);
    }

    [Fact]
    public void CreatesEmailNotificationAndMarksItSent()
    {
        var request = CreateSubmittedConnectionRequest();
        var recipient = request.ApplicantParty.Email;

        var createNotification = EmailNotification.Create(
            request,
            recipient,
            "Request status changed",
            "Your request status changed.");
        var notification = createNotification.Value;

        createNotification.IsSuccess.Should().BeTrue();
        notification.Request.Should().Be(request);
        notification.RecipientEmail.Should().Be(recipient);
        notification.SentAt.Should().BeNull();

        notification.MarkSent();
        notification.SentAt.Should().NotBeNull();
    }
}
