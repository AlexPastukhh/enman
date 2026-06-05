using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api.Contracts.ApplicantParties;

namespace EnergyManagement.Server.Api.Validation;

public static class FieldNames
{
    public static class Auth
    {
        public static string Email => JsonField.Of<RegisterClientAccountDto>(x => x.Email);
        public static string Password => JsonField.Of<RegisterClientAccountDto>(x => x.Password);
    }

    public static class ApplicantParty
    {
        public static string FullName => JsonField.Of<CreateIndividualApplicantPartyDto>(x => x.FullName);
        public static string Email => JsonField.Of<CreateIndividualApplicantPartyDto>(x => x.Email);
        public static string PhoneNumber => JsonField.Of<CreateIndividualApplicantPartyDto>(x => x.PhoneNumber);
        public static string ApplicantPartyType => JsonField.Of<InlineApplicantPartyForRequestDto>(x => x.ApplicantPartyType);
        public static string OrganizationName => JsonField.Of<InlineApplicantPartyForRequestDto>(x => x.OrganizationName);
        public static string Inn => JsonField.Of<InlineApplicantPartyForRequestDto>(x => x.Inn);
        public static string Kpp => JsonField.Of<InlineApplicantPartyForRequestDto>(x => x.Kpp);
        public static string Ogrn => JsonField.Of<InlineApplicantPartyForRequestDto>(x => x.Ogrn);
        public static string Ogrnip => JsonField.Of<InlineApplicantPartyForRequestDto>(x => x.Ogrnip);
    }

    public static class FullName
    {
        public static string FirstName => JsonField.Of<FullNameDto>(x => x.FirstName);
        public static string MiddleName => JsonField.Of<FullNameDto>(x => x.MiddleName);
        public static string LastName => JsonField.Of<FullNameDto>(x => x.LastName);
    }

    public static class CreateConnectionRequest
    {
        public static string ApplicantContextType => JsonField.Of<CreateConnectionRequestDto>(x => x.ApplicantContextType);
        public static string ExistingApplicantPartyId => JsonField.Of<CreateConnectionRequestDto>(x => x.ExistingApplicantPartyId);
        public static string NewApplicantParty => JsonField.Of<CreateConnectionRequestDto>(x => x.NewApplicantParty);
        public static string Details => JsonField.Of<CreateConnectionRequestDto>(x => x.Details);
        public static string Address => JsonField.Of<CreateConnectionRequestDto>(x => x.Address);
    }

    public static class Address
    {
        public static string PostalCode => JsonField.Of<AddressDto>(x => x.PostalCode);
        public static string Region => JsonField.Of<AddressDto>(x => x.Region);
        public static string City => JsonField.Of<AddressDto>(x => x.City);
        public static string Street => JsonField.Of<AddressDto>(x => x.Street);
        public static string House => JsonField.Of<AddressDto>(x => x.House);
        public static string Building => JsonField.Of<AddressDto>(x => x.Building);
        public static string Apartment => JsonField.Of<AddressDto>(x => x.Apartment);
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
    public static class AgreementExchangeList
    {
        public const string Status = "status";
    }

    public static class AgreementProposalVersion
    {
        public const string Document = "document";
        public const string DocumentStorageKey = "document.storageKey";
        public const string DocumentOriginalFileName = "document.originalFileName";
        public const string DocumentContentType = "document.contentType";
        public const string DocumentSizeBytes = "document.sizeBytes";
        public static string Comment => JsonField.Of<SendAgreementProposalVersionDto>(x => x.Comment);
    }

    public static class AgreementProposalDocumentUpload
    {
        public const string Document = "document";
        public const string DocumentContentType = "document.contentType";
        public const string DocumentSizeBytes = "document.sizeBytes";
    }

    public static class FinalRefuseAgreementExchange
    {
        public static string Reason => JsonField.Of<FinalRefuseAgreementExchangeDto>(x => x.Reason);
    }

}
