using EnergyManagement.Server.Api.Contracts.Common;

namespace EnergyManagement.Server.L1.Api.Validation;

public static class L1FieldNames
{
    public static class Auth
    {
        public static string Email => JsonField.Of<L1RegisterClientAccountDto>(x => x.Email);
        public static string Password => JsonField.Of<L1RegisterClientAccountDto>(x => x.Password);
    }

    public static class ApplicantParty
    {
        public static string FullName => JsonField.Of<L1CreateIndividualApplicantPartyDto>(x => x.FullName);
        public static string Email => JsonField.Of<L1CreateIndividualApplicantPartyDto>(x => x.Email);
        public static string PhoneNumber => JsonField.Of<L1CreateIndividualApplicantPartyDto>(x => x.PhoneNumber);
    }

    public static class FullName
    {
        public static string FirstName => JsonField.Of<L1FullNameDto>(x => x.FirstName);
        public static string MiddleName => JsonField.Of<L1FullNameDto>(x => x.MiddleName);
        public static string LastName => JsonField.Of<L1FullNameDto>(x => x.LastName);
    }

    public static class CreateConnectionRequest
    {
        public static string ApplicantContextType => JsonField.Of<L1CreateConnectionRequestDto>(x => x.ApplicantContextType);
        public static string ExistingApplicantPartyId => JsonField.Of<L1CreateConnectionRequestDto>(x => x.ExistingApplicantPartyId);
        public static string NewApplicantParty => JsonField.Of<L1CreateConnectionRequestDto>(x => x.NewApplicantParty);
        public static string Details => JsonField.Of<L1CreateConnectionRequestDto>(x => x.Details);
        public static string Address => JsonField.Of<L1CreateConnectionRequestDto>(x => x.Address);
    }

    public static class Address
    {
        public static string PostalCode => JsonField.Of<L1AddressDto>(x => x.PostalCode);
        public static string Region => JsonField.Of<L1AddressDto>(x => x.Region);
        public static string City => JsonField.Of<L1AddressDto>(x => x.City);
        public static string Street => JsonField.Of<L1AddressDto>(x => x.Street);
        public static string House => JsonField.Of<L1AddressDto>(x => x.House);
        public static string Building => JsonField.Of<L1AddressDto>(x => x.Building);
        public static string Apartment => JsonField.Of<L1AddressDto>(x => x.Apartment);
    }

    public static class ListMyRequests
    {
        public const string Status = "status";
    }


    public static class EmployeeRejectRequestReview
    {
        public static string Feedback => JsonField.Of<EmployeeRejectRequestReviewDto>(x => x.Feedback);
    }

    public static class EmployeeRequestList
    {
        public const string Status = "status";
        public const string ReviewState = "reviewState";
    }
}
