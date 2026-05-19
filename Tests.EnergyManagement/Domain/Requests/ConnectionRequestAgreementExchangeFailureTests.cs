using Domain.EnergyManagement;
using FluentAssertions;
using Tests.EnergyManagement.Domain;
using Tests.EnergyManagement.TestHelpers.App;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Requests;

public class ConnectionRequestAgreementExchangeFailureTests
{
    [Fact]
    public void MarkAgreementExchangeFailed_succeeds_for_approved_request()
    {
        var request = CreateApprovedRequest();

        var result = request.MarkAgreementExchangeFailed(
            agreementProposalExchangeId: 15,
            DateTimeOffset.UtcNow);

        result.IsSuccess.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.AgreementExchangeFailed);
    }

    [Fact]
    public void MarkAgreementExchangeFailed_fails_when_exchange_id_is_missing()
    {
        var request = CreateApprovedRequest();

        var result = request.MarkAgreementExchangeFailed(
            agreementProposalExchangeId: 0,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AgreementProposalExchangeIsRequired);
        request.Status.Should().Be(RequestStatus.Approved);
    }

    [Fact]
    public void MarkAgreementExchangeFailed_fails_when_request_is_not_approved()
    {
        var request = CreatePersistedInReviewRequest();

        var result = request.MarkAgreementExchangeFailed(
            agreementProposalExchangeId: 15,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OnlyApprovedRequestCanBeMarkedAgreementExchangeFailed);
        request.Status.Should().Be(RequestStatus.InReview);
    }

    private static ConnectionRequest CreateApprovedRequest()
    {
        var request = CreatePersistedInReviewRequest();
        var employee = CreatePersistedEmployee(id: 7);

        request.StartReview(employee, DateTimeOffset.UtcNow);
        request.ApproveReview(employee, DateTimeOffset.UtcNow.AddMinutes(1));

        return request;
    }

    private static ConnectionRequest CreatePersistedInReviewRequest()
    {
        return ConnectionRequest.Create(
            CreatePersistedApplicant(),
            ValidDomainTestData.RequestDetails,
            ValidDomainTestData.Address).Value.WithId(100);
    }

    private static IndividualApplicantParty CreatePersistedApplicant()
    {
        return IndividualApplicantParty.Create(
            clientAccountId: 10,
            ValidDomainTestData.FullName,
            ValidDomainTestData.Email,
            ValidDomainTestData.PhoneNumber,
            DateTimeOffset.UtcNow).Value.WithId(42);
    }

    private static Employee CreatePersistedEmployee(long id)
    {
        return Employee.Create(
            ValidDomainTestData.Email,
            ValidDomainTestData.PasswordHash,
            ValidDomainTestData.FullName,
            DateTimeOffset.UtcNow).Value.WithId(id);
    }
}
