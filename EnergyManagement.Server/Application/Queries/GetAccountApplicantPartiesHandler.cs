using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Queries;

public sealed class GetAccountApplicantPartiesHandler
    : IRequestHandler<GetAccountApplicantPartiesQuery, Result<GetAccountApplicantPartiesResponse, Error>>
{
    private readonly IAccountRepository _accounts;
    private readonly IApplicantPartyRepository _applicantParties;

    public GetAccountApplicantPartiesHandler(
        IAccountRepository accounts,
        IApplicantPartyRepository applicantParties)
    {
        _accounts = accounts;
        _applicantParties = applicantParties;
    }

    public async Task<Result<GetAccountApplicantPartiesResponse, Error>> Handle(
        GetAccountApplicantPartiesQuery query,
        CancellationToken cancellationToken)
    {
        var account = await _accounts.GetByIdAsync(query.ClientAccountId, cancellationToken);
        if (account is not ClientAccount)
        {
            return Result.Failure<GetAccountApplicantPartiesResponse, Error>(
                Errors.General.NotFound);
        }

        var applicantParties = await _applicantParties.ListOwnedByAccountIdAsync(
            query.ClientAccountId,
            cancellationToken);

        return Result.Success<GetAccountApplicantPartiesResponse, Error>(
            new GetAccountApplicantPartiesResponse(
                applicantParties.Select(ToSummary).ToList()));
    }

    private static ApplicantPartySummaryResponse ToSummary(ApplicantParty applicantParty)
    {
        return new ApplicantPartySummaryResponse(
            applicantParty.Id,
            applicantParty.ApplicantPartyType,
            applicantParty.GetDisplayName(),
            ToFullName(applicantParty),
            applicantParty.Email.Value,
            applicantParty.PhoneNumber.Value,
            applicantParty.VerificationStatus,
            applicantParty.IsCurrentActiveVersion,
            applicantParty.CreatedAt);
    }

    private static ApplicantPartyFullNameResponse? ToFullName(ApplicantParty applicantParty)
    {
        if (applicantParty is not IndividualApplicantParty individualApplicantParty)
        {
            return null;
        }

        return new ApplicantPartyFullNameResponse(
            individualApplicantParty.FullName.FirstName,
            individualApplicantParty.FullName.MiddleName,
            individualApplicantParty.FullName.LastName);
    }
}
