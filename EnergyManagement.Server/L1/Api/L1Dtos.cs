using System.Text.Json.Serialization;

namespace EnergyManagement.Server.L1.Api;

public sealed record L1RegisterClientAccountDto(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password);

public sealed record L1LoginRequest(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password);

public sealed record L1CurrentUserResponse(
    [property: JsonPropertyName("accountId")] long AccountId,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("isActive")] bool IsActive,
    [property: JsonPropertyName("isAuthenticated")] bool IsAuthenticated);

public sealed record L1CreateIndividualApplicantPartyDto(
    [property: JsonPropertyName("fullName")] L1FullNameDto FullName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("phoneNumber")] string PhoneNumber);

public sealed record L1FullNameDto(
    [property: JsonPropertyName("firstName")] string FirstName,
    [property: JsonPropertyName("middleName")] string MiddleName,
    [property: JsonPropertyName("lastName")] string LastName);

public sealed record L1CreateConnectionRequestDto(
    [property: JsonPropertyName("details")] string Details,
    [property: JsonPropertyName("address")] L1AddressDto Address);

public sealed record L1AddressDto(
    [property: JsonPropertyName("postalCode")] string PostalCode,
    [property: JsonPropertyName("region")] string Region,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("street")] string Street,
    [property: JsonPropertyName("house")] string House,
    [property: JsonPropertyName("building")] string? Building,
    [property: JsonPropertyName("apartment")] string? Apartment);
