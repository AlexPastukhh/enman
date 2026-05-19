using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class IndividualApplicantParty : ApplicantParty
{
    public FullName FullName { get; private set; }

    private IndividualApplicantParty(
        long clientAccountId,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        DateTimeOffset createdAt)
        : base(
            clientAccountId,
            ApplicantPartyType.Individual,
            email,
            phoneNumber,
            createdAt)
    {
        FullName = fullName;
    }

    private IndividualApplicantParty()
    {
        FullName = null!;
    }

    public static Result<IndividualApplicantParty, IReadOnlyList<Error>> Create(
        long clientAccountId,
        FullName fullName,
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
            return Result.Failure<IndividualApplicantParty, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<IndividualApplicantParty, IReadOnlyList<Error>>(
            new IndividualApplicantParty(
                clientAccountId,
                fullName!,
                contactEmail!,
                phoneNumber!,
                createdAt));
    }

    public static Result<IndividualApplicantParty, IReadOnlyList<Error>> Create(
        ClientAccount clientAccount,
        FullName fullName,
        Email contactEmail,
        PhoneNumber phoneNumber)
    {
        var clientAccountId = GuardPersistedReferenceId(
            clientAccount,
            nameof(clientAccount));

        return Create(
            clientAccountId,
            fullName,
            contactEmail,
            phoneNumber,
            DateTimeOffset.UtcNow);
    }

    public override string GetDisplayName()
    {
        return $"{FullName.LastName} {FullName.FirstName} {FullName.MiddleName}";
    }

    protected override bool HasMinimumDataForVerification()
    {
        return FullName is not null
            && Email is not null
            && PhoneNumber is not null;
    }
}
