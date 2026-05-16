using Domain.EnergyManagement.DocumentManaging;

namespace Tests.EnergyManagement.TestHelpers.L1;

public static class L1ValidTestData
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
}
