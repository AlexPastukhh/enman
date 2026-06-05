using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class IndividualEntrepreneurApplicantParty : ApplicantParty
{
    public FullName FullName { get; private set; }
    public Inn Inn { get; private set; }
    public Ogrnip Ogrnip { get; private set; }

    private IndividualEntrepreneurApplicantParty(
        long clientAccountId,
        FullName fullName,
        Inn inn,
        Ogrnip ogrnip,
        Email email,
        PhoneNumber phoneNumber,
        DateTimeOffset createdAt)
        : base(
            clientAccountId,
            ApplicantPartyType.IndividualEntrepreneur,
            email,
            phoneNumber,
            createdAt)
    {
        FullName = fullName;
        Inn = inn;
        Ogrnip = ogrnip;
    }

    private IndividualEntrepreneurApplicantParty()
    {
        FullName = null!;
        Inn = null!;
        Ogrnip = null!;
    }

    public static Result<IndividualEntrepreneurApplicantParty, IReadOnlyList<Error>> Create(
        long clientAccountId,
        FullName fullName,
        Inn inn,
        Ogrnip ogrnip,
        Email contactEmail,
        PhoneNumber phoneNumber,
        DateTimeOffset createdAt)
    {
        var errors = new List<Error>();

        if (clientAccountId <= 0)
        {
            errors.Add(Errors.L1Domain.ClientAccountIsRequired);
        }

        if (fullName is null)
        {
            errors.Add(Errors.Account.FirstNameIsRequired);
        }

        if (inn is null)
        {
            errors.Add(Errors.L1Domain.InnIsRequired);
        }

        if (ogrnip is null)
        {
            errors.Add(Errors.L1Domain.OgrnipIsRequired);
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
            return Result.Failure<IndividualEntrepreneurApplicantParty, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<IndividualEntrepreneurApplicantParty, IReadOnlyList<Error>>(
            new IndividualEntrepreneurApplicantParty(
                clientAccountId,
                fullName!,
                inn!,
                ogrnip!,
                contactEmail!,
                phoneNumber!,
                createdAt));
    }

    public override string GetDisplayName()
    {
        return $"IP {FullName.LastName} {FullName.FirstName} {FullName.MiddleName}";
    }

    protected override bool HasMinimumDataForVerification()
    {
        return FullName is not null
            && Inn is not null
            && Ogrnip is not null
            && Email is not null
            && PhoneNumber is not null;
    }
}
