using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class LegalEntityApplicantParty : ApplicantParty
{
    public OrganizationName OrganizationName { get; private set; }
    public Inn Inn { get; private set; }
    public Kpp Kpp { get; private set; }
    public Ogrn Ogrn { get; private set; }

    private LegalEntityApplicantParty(
        long clientAccountId,
        OrganizationName organizationName,
        Inn inn,
        Kpp kpp,
        Ogrn ogrn,
        Email email,
        PhoneNumber phoneNumber,
        DateTimeOffset createdAt)
        : base(
            clientAccountId,
            ApplicantPartyType.LegalEntity,
            email,
            phoneNumber,
            createdAt)
    {
        OrganizationName = organizationName;
        Inn = inn;
        Kpp = kpp;
        Ogrn = ogrn;
    }

    private LegalEntityApplicantParty()
    {
        OrganizationName = null!;
        Inn = null!;
        Kpp = null!;
        Ogrn = null!;
    }

    public static Result<LegalEntityApplicantParty, IReadOnlyList<Error>> Create(
        long clientAccountId,
        OrganizationName organizationName,
        Inn inn,
        Kpp kpp,
        Ogrn ogrn,
        Email contactEmail,
        PhoneNumber phoneNumber,
        DateTimeOffset createdAt)
    {
        var errors = new List<Error>();

        if (clientAccountId <= 0)
        {
            errors.Add(Errors.L1Domain.ClientAccountIsRequired);
        }

        if (organizationName is null)
        {
            errors.Add(Errors.L1Domain.OrganizationNameIsRequired);
        }

        if (inn is null)
        {
            errors.Add(Errors.L1Domain.InnIsRequired);
        }

        if (kpp is null)
        {
            errors.Add(Errors.L1Domain.KppIsRequired);
        }

        if (ogrn is null)
        {
            errors.Add(Errors.L1Domain.OgrnIsRequired);
        }

        if (contactEmail is null)
        {
            errors.Add(Errors.Account.EmailIsRequired);
        }

        if (phoneNumber is null)
        {
            errors.Add(Errors.Account.PhoneNumberIsRequired);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<LegalEntityApplicantParty, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<LegalEntityApplicantParty, IReadOnlyList<Error>>(
            new LegalEntityApplicantParty(
                clientAccountId,
                organizationName!,
                inn!,
                kpp!,
                ogrn!,
                contactEmail!,
                phoneNumber!,
                createdAt));
    }

    public override string GetDisplayName()
    {
        return OrganizationName.Value;
    }

    protected override bool HasMinimumDataForVerification()
    {
        return OrganizationName is not null
            && Inn is not null
            && Kpp is not null
            && Ogrn is not null
            && Email is not null
            && PhoneNumber is not null;
    }
}
