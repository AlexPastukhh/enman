using CSharpFunctionalExtensions;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Data;
using Xunit.Sdk;

namespace Tests.EnergyManagement.Legacy.TestHelpers;

public static class RequestAssertions
{
    public static UnitResult<string> MatchDto(IndividualRequest request, CreateIndividualRequestDto dto)
    {
        if (request is null) throw new XunitException(nameof(request) + " is null");
        if (dto is null) throw new XunitException(nameof(dto) + " is null");

        var diffs = new List<string>();

        if (!Equals(request.RequestDetails, dto.RequestDetails))
        {
            diffs.Add($"RequestDetails: expected '{dto.RequestDetails}' actual '{request.RequestDetails}'");
        }

        var requestAddress = request.Address;
        var dtoAddress = dto.Address;

        if (dtoAddress == null && requestAddress != null)
        {
            diffs.Add("Address: expected 'null' actual non-null");
        }
        else if (dtoAddress != null && requestAddress == null)
        {
            diffs.Add("Address: expected non-null actual 'null'");
        }
        else if (dtoAddress != null && requestAddress != null)
        {
            if (!Equals(requestAddress.PostalCode, dtoAddress.PostalCode))
                diffs.Add($"Address.PostalCode: expected '{dtoAddress.PostalCode}' actual '{requestAddress.PostalCode}'");

            if (!Equals(requestAddress.Region, dtoAddress.Region))
                diffs.Add($"Address.Region: expected '{dtoAddress.Region}' actual '{requestAddress.Region}'");

            if (!Equals(requestAddress.City, dtoAddress.City))
                diffs.Add($"Address.City: expected '{dtoAddress.City}' actual '{requestAddress.City}'");

            if (!Equals(requestAddress.Street, dtoAddress.Street))
                diffs.Add($"Address.Street: expected '{dtoAddress.Street}' actual '{requestAddress.Street}'");

            if (!Equals(requestAddress.House, dtoAddress.House))
                diffs.Add($"Address.House: expected '{dtoAddress.House}' actual '{requestAddress.House}'");

            if (!Equals(requestAddress.Building, dtoAddress.Building))
                diffs.Add($"Address.Building: expected '{dtoAddress.Building}' actual '{requestAddress.Building}'");

            if (!Equals(requestAddress.Apartment, dtoAddress.Apartment))
                diffs.Add($"Address.Apartment: expected '{dtoAddress.Apartment}' actual '{requestAddress.Apartment}'");
        }

        return diffs.Count == 0
            ? UnitResult.Success<string>()
            : UnitResult.Failure<string>(string.Join("; ", diffs));
    }
}

