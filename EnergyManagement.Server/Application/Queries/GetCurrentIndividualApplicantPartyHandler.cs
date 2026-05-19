using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Queries;

public sealed class GetCurrentIndividualApplicantPartyHandler
    : IRequestHandler<GetCurrentIndividualApplicantPartyQuery, Result<GetCurrentIndividualApplicantPartyResponse, Error>>
{
    private readonly IAccountRepository _accounts;
    private readonly IApplicantPartyRepository _applicantParties;

    public GetCurrentIndividualApplicantPartyHandler(
        IAccountRepository accounts,
        IApplicantPartyRepository applicantParties)
    {
        _accounts = accounts;
        _applicantParties = applicantParties;
    }

    public async Task<Result<GetCurrentIndividualApplicantPartyResponse, Error>> Handle(
        GetCurrentIndividualApplicantPartyQuery query,
        CancellationToken cancellationToken)
    {
        var account = await _accounts.GetByIdAsync(query.ClientAccountId, cancellationToken);
        if (account is not ClientAccount)
        {
            return Result.Failure<GetCurrentIndividualApplicantPartyResponse, Error>(
                Errors.General.NotFound);
        }

        var applicantParty = await _applicantParties
            .GetCurrentActiveIndividualByClientAccountIdAsync(
                query.ClientAccountId,
                cancellationToken);

        if (applicantParty is null)
        {
            return Result.Success<GetCurrentIndividualApplicantPartyResponse, Error>(
                new GetCurrentIndividualApplicantPartyResponse(false, null));
        }

        return Result.Success<GetCurrentIndividualApplicantPartyResponse, Error>(
            new GetCurrentIndividualApplicantPartyResponse(
                true,
                new IndividualApplicantPartyResponse(
                    applicantParty.FullName.FirstName,
                    applicantParty.FullName.MiddleName,
                    applicantParty.FullName.LastName,
                    applicantParty.Email.Value,
                    applicantParty.PhoneNumber.Value,
                    applicantParty.VerificationStatus.ToString())));
    }
}
