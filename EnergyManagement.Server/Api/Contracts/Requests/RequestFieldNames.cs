using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Data;

namespace EnergyManagement.Server.Api.Contracts.Requests;

public static class RequestFieldNames
{
    public static class CreateIndividualRequest
    {
        public static string RequestDetails => JsonField.Of<CreateIndividualRequestDto>(x => x.RequestDetails);
        public static string Address => JsonField.Of<CreateIndividualRequestDto>(x => x.Address);
        public static string PostalCode => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.PostalCode);
        public static string Region => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.Region);
        public static string City => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.City);
        public static string Street => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.Street);
        public static string House => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.House);
        public static string Apartment => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.Apartment);
        public static string Building => JsonField.Of<CreateIndividualRequestDto>(x => x.Address.Building);
    }
}
