using System.Text.Json.Serialization;

namespace EnergyManagement.Server.L1.Api;

public sealed record L1RegisterClientAccountDto(
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("password")] string? Password);

public sealed record L1LoginRequest(
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("password")] string? Password);

public sealed record L1CurrentUserResponse(
    [property: JsonPropertyName("accountId")] long AccountId,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("isActive")] bool IsActive,
    [property: JsonPropertyName("isAuthenticated")] bool IsAuthenticated);

public sealed record L1CreateIndividualApplicantPartyDto(
    [property: JsonPropertyName("fullName")] L1FullNameDto? FullName,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("phoneNumber")] string? PhoneNumber);

public sealed record L1CurrentIndividualApplicantPartyResponse(
    [property: JsonPropertyName("exists")] bool Exists,
    [property: JsonPropertyName("applicantParty")] L1IndividualApplicantPartyDto? ApplicantParty);

public sealed record L1IndividualApplicantPartyDto(
    [property: JsonPropertyName("fullName")] L1FullNameDto FullName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("phoneNumber")] string PhoneNumber,
    [property: JsonPropertyName("verificationStatus")] string VerificationStatus);

public sealed record L1AccountApplicantPartiesResponse(
    [property: JsonPropertyName("applicantParties")] IReadOnlyList<L1ApplicantPartySummaryDto> ApplicantParties);

public sealed record L1ApplicantPartySummaryDto(
    [property: JsonPropertyName("applicantPartyId")] long ApplicantPartyId,
    [property: JsonPropertyName("applicantPartyType")] string ApplicantPartyType,
    [property: JsonPropertyName("displayName")] string DisplayName,
    [property: JsonPropertyName("fullName")] L1FullNameDto? FullName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("phoneNumber")] string PhoneNumber,
    [property: JsonPropertyName("verificationStatus")] string VerificationStatus,
    [property: JsonPropertyName("isCurrentDefault")] bool IsCurrentDefault,
    [property: JsonPropertyName("createdAt")] DateTimeOffset? CreatedAt);

public sealed record L1FullNameDto(
    [property: JsonPropertyName("firstName")] string? FirstName,
    [property: JsonPropertyName("middleName")] string? MiddleName,
    [property: JsonPropertyName("lastName")] string? LastName);

public sealed record L1CreateConnectionRequestDto(
    [property: JsonPropertyName("applicantContextType")] string? ApplicantContextType,
    [property: JsonPropertyName("existingApplicantPartyId")] long? ExistingApplicantPartyId,
    [property: JsonPropertyName("newApplicantParty")] L1CreateIndividualApplicantPartyDto? NewApplicantParty,
    [property: JsonPropertyName("details")] string? Details,
    [property: JsonPropertyName("address")] L1AddressDto? Address);

public sealed record L1MyRequestSummaryDto(
    [property: JsonPropertyName("requestId")] long RequestId,
    [property: JsonPropertyName("requestType")] string RequestType,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("createdAt")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("summary")] string Summary,
    [property: JsonPropertyName("objectAddress")] L1AddressDto ObjectAddress);

public sealed record L1MyRequestDetailsDto(
    [property: JsonPropertyName("requestId")] long RequestId,
    [property: JsonPropertyName("requestType")] string RequestType,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("createdAt")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("submittedRequest")] L1SubmittedRequestDto SubmittedRequest,
    [property: JsonPropertyName("reviewResult")] L1MyRequestReviewResultDto? ReviewResult);

public sealed record L1SubmittedRequestDto(
    [property: JsonPropertyName("details")] string Details,
    [property: JsonPropertyName("objectAddress")] L1AddressDto ObjectAddress);

public sealed record L1MyRequestReviewResultDto(
    [property: JsonPropertyName("decision")] string Decision,
    [property: JsonPropertyName("decidedAt")] DateTimeOffset DecidedAt,
    [property: JsonPropertyName("rejection")] L1MyRequestRejectionDto? Rejection);

public sealed record L1MyRequestRejectionDto(
    [property: JsonPropertyName("reason")] string Reason);

public sealed record L1AddressDto(
    [property: JsonPropertyName("postalCode")] string? PostalCode,
    [property: JsonPropertyName("region")] string? Region,
    [property: JsonPropertyName("city")] string? City,
    [property: JsonPropertyName("street")] string? Street,
    [property: JsonPropertyName("house")] string? House,
    [property: JsonPropertyName("building")] string? Building,
    [property: JsonPropertyName("apartment")] string? Apartment);
