using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed class L1GetAccountApplicantPartiesHandler
    : IRequestHandler<L1GetAccountApplicantPartiesQuery, Result<L1GetAccountApplicantPartiesResponse, Error>>
{
    private readonly IAccountRepository _accounts;
    private readonly IApplicantPartyRepository _applicantParties;

    public L1GetAccountApplicantPartiesHandler(
        IAccountRepository accounts,
        IApplicantPartyRepository applicantParties)
    {
        _accounts = accounts;
        _applicantParties = applicantParties;
    }

    public async Task<Result<L1GetAccountApplicantPartiesResponse, Error>> Handle(
        L1GetAccountApplicantPartiesQuery query,
        CancellationToken cancellationToken)
    {
        var account = await _accounts.GetByIdAsync(query.ClientAccountId, cancellationToken);
        if (account is not ClientAccount)
        {
            return Result.Failure<L1GetAccountApplicantPartiesResponse, Error>(
                Errors.General.NotFound);
        }

        var applicantParties = await _applicantParties.ListOwnedByAccountIdAsync(
            query.ClientAccountId,
            cancellationToken);

        return Result.Success<L1GetAccountApplicantPartiesResponse, Error>(
            new L1GetAccountApplicantPartiesResponse(
                applicantParties.Select(ToSummary).ToList()));
    }

    private static L1ApplicantPartySummaryResponse ToSummary(ApplicantParty applicantParty)
    {
        return new L1ApplicantPartySummaryResponse(
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

    private static L1ApplicantPartyFullNameResponse? ToFullName(ApplicantParty applicantParty)
    {
        if (applicantParty is not IndividualApplicantParty individualApplicantParty)
        {
            return null;
        }

        return new L1ApplicantPartyFullNameResponse(
            individualApplicantParty.FullName.FirstName,
            individualApplicantParty.FullName.MiddleName,
            individualApplicantParty.FullName.LastName);
    }
}
