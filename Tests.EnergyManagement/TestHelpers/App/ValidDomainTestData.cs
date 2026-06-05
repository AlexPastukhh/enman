using Domain.EnergyManagement.DocumentManaging;

namespace Tests.EnergyManagement.TestHelpers.App;

public static class ValidDomainTestData
{
    public const string RequestDetails =
        "Request DetailsRequest DetailsRequest DetailsRequest DetailsRequest DetailsRequest DetailsRequest Details";

    public static Email Email => global::Domain.EnergyManagement.DocumentManaging.Email
        .Create("l1.client@example.com")
        .Value;

    public static PasswordHash PasswordHash => global::Domain.EnergyManagement.DocumentManaging.PasswordHash
        .ConvertFromString(
            "0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF-" +
            "0123456789ABCDEF0123456789ABCDEF");

    public static FullName FullName => global::Domain.EnergyManagement.DocumentManaging.FullName
        .Create("John", "Michael", "Doe")
        .Value;

    public static PhoneNumber PhoneNumber => global::Domain.EnergyManagement.DocumentManaging.PhoneNumber
        .Create("79237554726")
        .Value;

    public static Address Address => global::Domain.EnergyManagement.DocumentManaging.Address
        .Create(
            "123456",
            "Region",
            "City",
            "Street",
            "House",
            null,
            "Apartment")
        .Value;


    public static global::Domain.EnergyManagement.Inn IndividualEntrepreneurInn => global::Domain.EnergyManagement.Inn
        .Create("123456789012")
        .Value;

    public static global::Domain.EnergyManagement.Inn LegalEntityInn => global::Domain.EnergyManagement.Inn
        .Create("1234567890")
        .Value;

    public static global::Domain.EnergyManagement.Ogrn Ogrn => global::Domain.EnergyManagement.Ogrn
        .Create("1234567890123")
        .Value;

    public static global::Domain.EnergyManagement.Ogrnip Ogrnip => global::Domain.EnergyManagement.Ogrnip
        .Create("123456789012345")
        .Value;

    public static global::Domain.EnergyManagement.Kpp Kpp => global::Domain.EnergyManagement.Kpp
        .Create("123456789")
        .Value;

    public static global::Domain.EnergyManagement.OrganizationName OrganizationName => global::Domain.EnergyManagement.OrganizationName
        .Create("Test Organization")
        .Value;
}
