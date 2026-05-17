using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public abstract class ClientRequest : L1Entity
{
    public long ApplicantPartyId { get; private set; }
    public long ClientAccountId { get; private set; }
    public ClientRequestType RequestType { get; private set; }
    public RequestStatus Status { get; protected set; }
    public string Details { get; private set; }
    public Address ObjectAddress { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    protected ClientRequest(
        ApplicantParty applicantParty,
        ClientRequestType requestType,
        string details,
        Address objectAddress,
        DateTimeOffset createdAt)
    {
        ApplicantPartyId = applicantParty.Id;
        ClientAccountId = applicantParty.ClientAccountId;
        RequestType = requestType;
        Status = RequestStatus.InReview;
        Details = details;
        ObjectAddress = objectAddress;
        CreatedAt = createdAt;
    }

    protected ClientRequest()
    {
        Details = null!;
        ObjectAddress = null!;
    }

    protected static IReadOnlyList<Error> ValidateCreateInput(
        ApplicantParty applicantParty,
        string details,
        Address objectAddress)
    {
        var errors = new List<Error>();

        if (applicantParty is null)
        {
            errors.Add(Errors.L1Domain.ApplicantPartyIsRequired);
        }
        else if (applicantParty.Id <= 0)
        {
            errors.Add(Errors.L1Domain.ApplicantPartyMustBePersisted);
        }

        if (string.IsNullOrWhiteSpace(details))
        {
            errors.Add(Errors.ClientRequestErrors.ClientRequestTextIsRequired);
        }

        if (details is not null && details.Length > 3000)
        {
            errors.Add(Errors.ClientRequestErrors.ClientRequestTextIsTooLong);
        }

        if (objectAddress is null)
        {
            errors.Add(Errors.L1Domain.RequestObjectAddressIsRequired);
        }

        return errors;
    }
}
