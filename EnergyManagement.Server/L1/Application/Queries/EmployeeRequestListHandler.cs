using CSharpFunctionalExtensions;
using Dapper;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.Configuration;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed class EmployeeRequestListHandler
    : IRequestHandler<EmployeeRequestListQuery, Result<EmployeeRequestListResponse, IReadOnlyList<Error>>>
{
    private readonly IConfiguration _configuration;

    public EmployeeRequestListHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Result<EmployeeRequestListResponse, IReadOnlyList<Error>>> Handle(
        EmployeeRequestListQuery query,
        CancellationToken cancellationToken)
    {
        var status = string.IsNullOrWhiteSpace(query.Status)
            ? null
            : query.Status;

        var reviewState = string.IsNullOrWhiteSpace(query.ReviewState)
            ? null
            : query.ReviewState;

        await using var connection = new SqlConnection(
            _configuration.GetConnectionString(ConnectionStringNames.ManagementDb));

        const string sql = """
            SELECT
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
                applicant.FullName_FirstName AS ApplicantFirstName,
                applicant.FullName_MiddleName AS ApplicantMiddleName,
                applicant.FullName_LastName AS ApplicantLastName,
                applicant.VerificationStatus AS ApplicantVerificationStatus,
                review.Status AS ReviewStatus,
                review.StartedByEmployeeId
            FROM dbo.L1ClientRequests AS request
            INNER JOIN dbo.L1ApplicantParties AS applicant
                ON applicant.Id = request.ApplicantPartyId
            LEFT JOIN dbo.L1RequestReviews AS review
                ON review.RequestId = request.Id
            WHERE (@Status IS NULL OR request.Status = @Status)
            ORDER BY request.CreatedAt DESC, request.Id DESC;
            """;

        var rows = await connection.QueryAsync<EmployeeRequestListRow>(
            new CommandDefinition(
                sql,
                new { Status = status },
                cancellationToken: cancellationToken));

        var requests = rows
            .Select(row => ToResponse(row, query.EmployeeId))
            .Where(row => reviewState is null || row.ReviewState == reviewState)
            .ToList();

        return Result.Success<EmployeeRequestListResponse, IReadOnlyList<Error>>(
            new EmployeeRequestListResponse(requests));
    }

    private static EmployeeRequestListItemResponse ToResponse(
        EmployeeRequestListRow row,
        long currentEmployeeId)
    {
        return new EmployeeRequestListItemResponse(
            row.RequestId,
            row.RequestType,
            row.Status,
            FormatApplicantDisplayName(row),
            FormatAddress(row),
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
        EmployeeRequestListRow row,
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

    private static string FormatApplicantDisplayName(EmployeeRequestListRow row)
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

    private static string FormatAddress(EmployeeRequestListRow row)
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

    private sealed class EmployeeRequestListRow
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
        public string ApplicantFirstName { get; init; } = string.Empty;
        public string ApplicantMiddleName { get; init; } = string.Empty;
        public string ApplicantLastName { get; init; } = string.Empty;
        public string ApplicantVerificationStatus { get; init; } = string.Empty;
        public string? ReviewStatus { get; init; }
        public long? StartedByEmployeeId { get; init; }
    }
}
