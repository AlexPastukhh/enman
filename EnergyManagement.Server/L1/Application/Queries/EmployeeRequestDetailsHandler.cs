using CSharpFunctionalExtensions;
using Dapper;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.Configuration;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed class EmployeeRequestDetailsHandler
    : IRequestHandler<EmployeeRequestDetailsQuery, Maybe<EmployeeRequestDetailsResponse>>
{
    private readonly IConfiguration _configuration;

    public EmployeeRequestDetailsHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Maybe<EmployeeRequestDetailsResponse>> Handle(
        EmployeeRequestDetailsQuery query,
        CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(
            _configuration.GetConnectionString(ConnectionStringNames.ManagementDb));

        const string sql = """
            SELECT TOP (1)
                request.Id AS RequestId,
                request.RequestType,
                request.Status,
                request.CreatedAt,
                request.Details,
                request.ObjectAddress_PostalCode AS PostalCode,
                request.ObjectAddress_Region AS Region,
                request.ObjectAddress_City AS City,
                request.ObjectAddress_Street AS Street,
                request.ObjectAddress_House AS House,
                request.ObjectAddress_Building AS Building,
                request.ObjectAddress_Apartment AS Apartment,
                applicant.Id AS ApplicantPartyId,
                applicant.ApplicantPartyType,
                applicant.FullName_FirstName AS ApplicantFirstName,
                applicant.FullName_MiddleName AS ApplicantMiddleName,
                applicant.FullName_LastName AS ApplicantLastName,
                applicant.Email,
                applicant.PhoneNumber,
                applicant.VerificationStatus AS ApplicantVerificationStatus,
                review.Status AS ReviewStatus,
                review.StartedByEmployeeId
            FROM dbo.L1ClientRequests AS request
            INNER JOIN dbo.L1ApplicantParties AS applicant
                ON applicant.Id = request.ApplicantPartyId
            LEFT JOIN dbo.L1RequestReviews AS review
                ON review.RequestId = request.Id
            WHERE request.Id = @RequestId;
            """;

        var row = await connection.QuerySingleOrDefaultAsync<EmployeeRequestDetailsRow>(
            new CommandDefinition(
                sql,
                new { query.RequestId },
                cancellationToken: cancellationToken));

        if (row is null)
        {
            return Maybe<EmployeeRequestDetailsResponse>.None;
        }

        return ToResponse(row, query.EmployeeId);
    }

    private static EmployeeRequestDetailsResponse ToResponse(
        EmployeeRequestDetailsRow row,
        long currentEmployeeId)
    {
        return new EmployeeRequestDetailsResponse(
            row.RequestId,
            row.RequestType,
            row.Status,
            new EmployeeRequestApplicantSummaryResponse(
                row.ApplicantPartyId,
                row.ApplicantPartyType,
                FormatApplicantDisplayName(row),
                row.Email,
                row.PhoneNumber),
            FormatAddress(row),
            row.Details,
            row.CreatedAt,
            DeriveReviewState(row, currentEmployeeId).ToString(),
            ToApplicantVerificationResponse(row.ApplicantVerificationStatus));
    }

    private static EmployeeRequestApplicantVerificationResponse ToApplicantVerificationResponse(
        string verificationStatus)
    {
        if (verificationStatus == ApplicantPartyVerificationStatus.Verified.ToString())
        {
            return new EmployeeRequestApplicantVerificationResponse(
                Required: true,
                Status: ApplicantPartyVerificationStatus.Verified.ToString(),
                CanRun: false,
                Message: "Данные проверены");
        }

        return new EmployeeRequestApplicantVerificationResponse(
            Required: true,
            Status: ApplicantPartyVerificationStatus.Unverified.ToString(),
            CanRun: true,
            Message: "Данные не проверены");
    }

    private static EmployeeRequestReviewState DeriveReviewState(
        EmployeeRequestDetailsRow row,
        long currentEmployeeId)
    {
        if (Enum.TryParse<RequestReviewStatus>(row.ReviewStatus, ignoreCase: false, out var reviewStatus)
            && Enum.IsDefined(reviewStatus))
        {
            return reviewStatus switch
            {
                RequestReviewStatus.Started => row.StartedByEmployeeId == currentEmployeeId
                    ? EmployeeRequestReviewState.StartedByCurrentEmployee
                    : EmployeeRequestReviewState.StartedByAnotherEmployee,
                RequestReviewStatus.Approved => EmployeeRequestReviewState.Approved,
                RequestReviewStatus.Rejected => EmployeeRequestReviewState.Rejected,
                _ => EmployeeRequestReviewState.NotStarted
            };
        }

        if (Enum.TryParse<RequestStatus>(row.Status, ignoreCase: false, out var requestStatus)
            && Enum.IsDefined(requestStatus))
        {
            return requestStatus switch
            {
                RequestStatus.Approved => EmployeeRequestReviewState.Approved,
                RequestStatus.Rejected => EmployeeRequestReviewState.Rejected,
                _ => EmployeeRequestReviewState.NotStarted
            };
        }

        return EmployeeRequestReviewState.NotStarted;
    }

    private static string FormatApplicantDisplayName(EmployeeRequestDetailsRow row)
    {
        return string.Join(
                " ",
                new[]
                {
                    row.ApplicantLastName,
                    row.ApplicantFirstName,
                    row.ApplicantMiddleName
                }.Where(value => !string.IsNullOrWhiteSpace(value)))
            .Trim();
    }

    private static string FormatAddress(EmployeeRequestDetailsRow row)
    {
        return string.Join(
            ", ",
            new[]
            {
                row.PostalCode,
                row.Region,
                row.City,
                row.Street,
                row.House,
                row.Building,
                row.Apartment
            }.Where(value => !string.IsNullOrWhiteSpace(value)));
    }

    private sealed class EmployeeRequestDetailsRow
    {
        public long RequestId { get; init; }
        public string RequestType { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public DateTimeOffset CreatedAt { get; init; }
        public string Details { get; init; } = string.Empty;
        public string PostalCode { get; init; } = string.Empty;
        public string Region { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string Street { get; init; } = string.Empty;
        public string House { get; init; } = string.Empty;
        public string? Building { get; init; }
        public string? Apartment { get; init; }
        public long ApplicantPartyId { get; init; }
        public string ApplicantPartyType { get; init; } = string.Empty;
        public string ApplicantFirstName { get; init; } = string.Empty;
        public string ApplicantMiddleName { get; init; } = string.Empty;
        public string ApplicantLastName { get; init; } = string.Empty;
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string ApplicantVerificationStatus { get; init; } = string.Empty;
        public string? ReviewStatus { get; init; }
        public long? StartedByEmployeeId { get; init; }
    }
}
