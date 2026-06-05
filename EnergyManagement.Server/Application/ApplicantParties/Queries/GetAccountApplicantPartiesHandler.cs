using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.ApplicantParties.Queries;

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
            ToOrganizationName(applicantParty),
            ToInn(applicantParty),
            ToKpp(applicantParty),
            ToOgrn(applicantParty),
            ToOgrnip(applicantParty),
            applicantParty.Email.Value,
            applicantParty.PhoneNumber.Value,
            applicantParty.VerificationStatus,
            applicantParty.IsCurrentActiveVersion,
            applicantParty.CreatedAt);
    }

    private static ApplicantPartyFullNameResponse? ToFullName(ApplicantParty applicantParty)
    {
        var fullName = applicantParty switch
        {
            IndividualApplicantParty individualApplicantParty => individualApplicantParty.FullName,
            IndividualEntrepreneurApplicantParty individualEntrepreneurApplicantParty => individualEntrepreneurApplicantParty.FullName,
            _ => null
        };

        return fullName is null
            ? null
            : new ApplicantPartyFullNameResponse(
                fullName.FirstName,
                fullName.MiddleName,
                fullName.LastName);
    }

    private static string? ToOrganizationName(ApplicantParty applicantParty)
    {
        return applicantParty is LegalEntityApplicantParty legalEntity
            ? legalEntity.OrganizationName.Value
            : null;
    }

    private static string? ToInn(ApplicantParty applicantParty)
    {
        return applicantParty switch
        {
            IndividualEntrepreneurApplicantParty individualEntrepreneur => individualEntrepreneur.Inn.Value,
            LegalEntityApplicantParty legalEntity => legalEntity.Inn.Value,
            _ => null
        };
    }

    private static string? ToKpp(ApplicantParty applicantParty)
    {
        return applicantParty is LegalEntityApplicantParty legalEntity
            ? legalEntity.Kpp.Value
            : null;
    }

    private static string? ToOgrn(ApplicantParty applicantParty)
    {
        return applicantParty is LegalEntityApplicantParty legalEntity
            ? legalEntity.Ogrn.Value
            : null;
    }

    private static string? ToOgrnip(ApplicantParty applicantParty)
    {
        return applicantParty is IndividualEntrepreneurApplicantParty individualEntrepreneur
            ? individualEntrepreneur.Ogrnip.Value
            : null;
    }
}
