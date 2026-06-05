using System.Text.Json.Serialization;
using EnergyManagement.Server.Api;

namespace EnergyManagement.Server.Api.Contracts.ApplicantParties;

public sealed record CreateIndividualApplicantPartyDto(
    [property: JsonPropertyName("fullName")] FullNameDto? FullName,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("phoneNumber")] string? PhoneNumber);

public sealed record CreateIndividualEntrepreneurApplicantPartyDto(
    [property: JsonPropertyName("fullName")] FullNameDto? FullName,
    [property: JsonPropertyName("inn")] string? Inn,
    [property: JsonPropertyName("ogrnip")] string? Ogrnip,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("phoneNumber")] string? PhoneNumber);

public sealed record CreateLegalEntityApplicantPartyDto(
    [property: JsonPropertyName("organizationName")] string? OrganizationName,
    [property: JsonPropertyName("inn")] string? Inn,
    [property: JsonPropertyName("kpp")] string? Kpp,
    [property: JsonPropertyName("ogrn")] string? Ogrn,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("phoneNumber")] string? PhoneNumber);

public sealed record CreateApplicantPartyResponseDto(
    [property: JsonPropertyName("applicantPartyId")] long ApplicantPartyId,
    [property: JsonPropertyName("clientAccountId")] long ClientAccountId,
    [property: JsonPropertyName("applicantPartyType")] string ApplicantPartyType);

public sealed record CurrentIndividualApplicantPartyResponseDto(
    [property: JsonPropertyName("exists")] bool Exists,
    [property: JsonPropertyName("applicantParty")] IndividualApplicantPartyDto? ApplicantParty);

public sealed record IndividualApplicantPartyDto(
    [property: JsonPropertyName("fullName")] FullNameDto FullName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("phoneNumber")] string PhoneNumber,
    [property: JsonPropertyName("verificationStatus")] string VerificationStatus);

public sealed record AccountApplicantPartiesResponseDto(
    [property: JsonPropertyName("applicantParties")] IReadOnlyList<ApplicantPartySummaryDto> ApplicantParties);

public sealed record ApplicantPartySummaryDto(
    [property: JsonPropertyName("applicantPartyId")] long ApplicantPartyId,
    [property: JsonPropertyName("applicantPartyType")] string ApplicantPartyType,
    [property: JsonPropertyName("displayName")] string DisplayName,
    [property: JsonPropertyName("fullName")] FullNameDto? FullName,
    [property: JsonPropertyName("organizationName")] string? OrganizationName,
    [property: JsonPropertyName("inn")] string? Inn,
    [property: JsonPropertyName("kpp")] string? Kpp,
    [property: JsonPropertyName("ogrn")] string? Ogrn,
    [property: JsonPropertyName("ogrnip")] string? Ogrnip,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("phoneNumber")] string PhoneNumber,
    [property: JsonPropertyName("verificationStatus")] string VerificationStatus,
    [property: JsonPropertyName("isCurrentDefault")] bool IsCurrentDefault,
    [property: JsonPropertyName("createdAt")] DateTimeOffset? CreatedAt);
