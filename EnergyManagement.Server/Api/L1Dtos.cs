using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace EnergyManagement.Server.Api;

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

public sealed record EmployeeRequestListResponseDto(
    [property: JsonPropertyName("requests")] IReadOnlyList<EmployeeRequestListItemDto> Requests);

public sealed record EmployeeRequestListItemDto(
    [property: JsonPropertyName("requestId")] long RequestId,
    [property: JsonPropertyName("requestType")] string RequestType,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("applicantDisplayName")] string ApplicantDisplayName,
    [property: JsonPropertyName("objectAddress")] string ObjectAddress,
    [property: JsonPropertyName("createdAt")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("reviewState")] string ReviewState,
    [property: JsonPropertyName("applicantVerification")] EmployeeRequestApplicantVerificationDto? ApplicantVerification);


public sealed record EmployeeRequestDetailsDto(
    [property: JsonPropertyName("requestId")] long RequestId,
    [property: JsonPropertyName("requestType")] string RequestType,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("applicant")] EmployeeRequestApplicantSummaryDto Applicant,
    [property: JsonPropertyName("objectAddress")] string ObjectAddress,
    [property: JsonPropertyName("details")] string Details,
    [property: JsonPropertyName("createdAt")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("reviewState")] string ReviewState,
    [property: JsonPropertyName("applicantVerification")] EmployeeRequestApplicantVerificationDto? ApplicantVerification);

public sealed record EmployeeRequestApplicantVerificationDto(
    [property: JsonPropertyName("required")] bool Required,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("canRun")] bool CanRun,
    [property: JsonPropertyName("message")] string? Message);

public sealed record EmployeeRequestApplicantSummaryDto(
    [property: JsonPropertyName("applicantPartyId")] long ApplicantPartyId,
    [property: JsonPropertyName("applicantPartyType")] string ApplicantPartyType,
    [property: JsonPropertyName("displayName")] string DisplayName,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("phoneNumber")] string? PhoneNumber);



public sealed record AgreementExchangeListResponseDto(
    [property: JsonPropertyName("exchanges")] IReadOnlyList<AgreementExchangeListItemDto> Exchanges);

public sealed record AgreementExchangeListItemDto(
    [property: JsonPropertyName("exchangeId")] long ExchangeId,
    [property: JsonPropertyName("requestId")] long RequestId,
    [property: JsonPropertyName("exchangeStatus")] string ExchangeStatus,
    [property: JsonPropertyName("activeProposalVersion")] int ActiveProposalVersion,
    [property: JsonPropertyName("activeProposalSender")] string ActiveProposalSender,
    [property: JsonPropertyName("activeProposalSenderId")] long ActiveProposalSenderId,
    [property: JsonPropertyName("requestDisplayName")] string RequestDisplayName,
    [property: JsonPropertyName("objectAddress")] string ObjectAddress,
    [property: JsonPropertyName("createdAt")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("lastActivityAt")] DateTimeOffset? LastActivityAt);

public sealed record AgreementExchangeListQueryDto(
    [property: JsonPropertyName("status")] string? Status);

public sealed record AgreementExchangeDetailsResponseDto(
    [property: JsonPropertyName("exchangeId")] long ExchangeId,
    [property: JsonPropertyName("requestId")] long RequestId,
    [property: JsonPropertyName("exchangeStatus")] string ExchangeStatus,
    [property: JsonPropertyName("activeProposalVersion")] int ActiveProposalVersion,
    [property: JsonPropertyName("request")] AgreementExchangeRequestSummaryDto Request,
    [property: JsonPropertyName("activeProposal")] AgreementProposalDetailsDto ActiveProposal,
    [property: JsonPropertyName("proposals")] IReadOnlyList<AgreementProposalDetailsDto> Proposals,
    [property: JsonPropertyName("currentActorSide")] string CurrentActorSide,
    [property: JsonPropertyName("createdAt")] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("lastActivityAt")] DateTimeOffset? LastActivityAt);

public sealed record AgreementExchangeRequestSummaryDto(
    [property: JsonPropertyName("requestId")] long RequestId,
    [property: JsonPropertyName("requestStatus")] string RequestStatus,
    [property: JsonPropertyName("requestDisplayName")] string RequestDisplayName,
    [property: JsonPropertyName("objectAddress")] string ObjectAddress);

public sealed record AgreementProposalDetailsDto(
    [property: JsonPropertyName("proposalId")] long ProposalId,
    [property: JsonPropertyName("version")] int Version,
    [property: JsonPropertyName("sender")] string Sender,
    [property: JsonPropertyName("senderId")] long SenderId,
    [property: JsonPropertyName("state")] string State,
    [property: JsonPropertyName("document")] AgreementDocumentRefDto Document,
    [property: JsonPropertyName("comment")] string? Comment,
    [property: JsonPropertyName("createdAt")] DateTimeOffset CreatedAt);


public sealed class UploadAgreementProposalDocumentForm
{
    public IFormFile? Document { get; init; }
}

public sealed record AgreementDocumentRefDto(
    [property: JsonPropertyName("storageKey")] string? StorageKey,
    [property: JsonPropertyName("originalFileName")] string? OriginalFileName,
    [property: JsonPropertyName("contentType")] string? ContentType,
    [property: JsonPropertyName("sizeBytes")] long SizeBytes);

public sealed record StartAgreementExchangeDto(
    [property: JsonPropertyName("document")] AgreementDocumentRefDto? Document,
    [property: JsonPropertyName("comment")] string? Comment);

public sealed record SendAgreementProposalVersionDto(
    [property: JsonPropertyName("document")] AgreementDocumentRefDto? Document,
    [property: JsonPropertyName("comment")] string? Comment);

public sealed record FinalRefuseAgreementExchangeDto(
    [property: JsonPropertyName("reason")] string? Reason);

public sealed record RunApplicantPartyVerificationResponseDto(
    [property: JsonPropertyName("requestId")] long RequestId,
    [property: JsonPropertyName("applicantPartyId")] long ApplicantPartyId,
    [property: JsonPropertyName("verificationStatus")] string VerificationStatus,
    [property: JsonPropertyName("mockResult")] string MockResult,
    [property: JsonPropertyName("message")] string? Message);

public sealed record EmployeeRejectRequestReviewDto(
    [property: JsonPropertyName("feedback")] string? Feedback);

public sealed record L1AddressDto(
    [property: JsonPropertyName("postalCode")] string? PostalCode,
    [property: JsonPropertyName("region")] string? Region,
    [property: JsonPropertyName("city")] string? City,
    [property: JsonPropertyName("street")] string? Street,
    [property: JsonPropertyName("house")] string? House,
    [property: JsonPropertyName("building")] string? Building,
    [property: JsonPropertyName("apartment")] string? Apartment);
