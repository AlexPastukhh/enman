using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public abstract class ApplicantParty : L1Entity
{
    private ApplicantPartyVerificationStatus _verificationStatus;
    private bool _isCurrentActiveVersion;

    public long ClientAccountId { get; private set; }
    public ApplicantPartyType ApplicantPartyType { get; private set; }
    public ApplicantPartyType Type => ApplicantPartyType;
    public ApplicantPartyVerificationStatus VerificationStatus => _verificationStatus;
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public bool IsCurrentActiveVersion => _isCurrentActiveVersion;
    public DateTimeOffset CreatedAt { get; private set; }

    protected ApplicantParty(
        long clientAccountId,
        ApplicantPartyType applicantPartyType,
        Email email,
        PhoneNumber phoneNumber,
        DateTimeOffset createdAt)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(phoneNumber);

        ClientAccountId = clientAccountId;
        ApplicantPartyType = applicantPartyType;
        _verificationStatus = ApplicantPartyVerificationStatus.Unverified;
        Email = email;
        PhoneNumber = phoneNumber;
        _isCurrentActiveVersion = false;
        CreatedAt = createdAt;
    }

    protected ApplicantParty()
    {
        Email = null!;
        PhoneNumber = null!;
    }

    public abstract string GetDisplayName();

    public UnitResult<IReadOnlyList<Error>> CanMarkVerified()
    {
        if (VerificationStatus == ApplicantPartyVerificationStatus.Verified)
        {
            return UnitResult.Success<IReadOnlyList<Error>>();
        }

        if (!HasMinimumDataForVerification())
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ApplicantPartyIsIncomplete]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> MarkVerified()
    {
        var canMarkVerified = CanMarkVerified();
        if (canMarkVerified.IsFailure)
        {
            return canMarkVerified;
        }

        _verificationStatus = ApplicantPartyVerificationStatus.Verified;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> MarkInactiveVersion()
    {
        _isCurrentActiveVersion = false;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> MarkAsCurrentDefaultTemplate()
    {
        _isCurrentActiveVersion = true;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    protected abstract bool HasMinimumDataForVerification();
}
