using Dapper;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server.Persistence.Repositories;

public sealed class ClientRequestRepository : IClientRequestRepository
{
    private readonly EnergyManagementDbContext _context;

    public ClientRequestRepository(EnergyManagementDbContext context)
    {
        _context = context;
    }

    public void Add(ClientRequest request)
    {
        _context.ClientRequests.Add(request);
    }

    public Task<ClientRequest?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _context.ClientRequests.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ClientRequestSummaryReadModel>> ListByClientAccountIdAsync(
        long clientAccountId,
        RequestStatus? status,
        CancellationToken cancellationToken)
    {
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
                request.ObjectAddress_Apartment AS Apartment
            FROM dbo.ClientRequests AS request
            INNER JOIN dbo.ApplicantParties AS applicantParty
                ON request.ApplicantPartyId = applicantParty.Id
            WHERE applicantParty.ClientAccountId = @ClientAccountId
              AND (@Status IS NULL OR request.Status = @Status)
            ORDER BY request.CreatedAt DESC, request.Id DESC;
            """;

        var connection = _context.Database.GetDbConnection();
        var rows = await connection.QueryAsync<ClientRequestSummaryRow>(
            new CommandDefinition(
                sql,
                new
                {
                    ClientAccountId = clientAccountId,
                    Status = status?.ToString()
                },
                cancellationToken: cancellationToken));

        return rows
            .Select(row => new ClientRequestSummaryReadModel(
                row.RequestId,
                Enum.Parse<ClientRequestType>(row.RequestType, ignoreCase: false),
                Enum.Parse<RequestStatus>(row.Status, ignoreCase: false),
                row.CreatedAt,
                row.Details,
                row.PostalCode,
                row.Region,
                row.City,
                row.Street,
                row.House,
                row.Building,
                row.Apartment))
            .ToList();
    }

    public async Task<ClientRequestDetailsReadModel?> GetDetailsByIdAndClientAccountIdAsync(
        long requestId,
        long clientAccountId,
        CancellationToken cancellationToken)
    {
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
                review.Status AS ReviewStatus,
                review.CompletedAt AS ReviewCompletedAt,
                review.RejectionReason
            FROM dbo.ClientRequests AS request
            INNER JOIN dbo.ApplicantParties AS applicantParty
                ON request.ApplicantPartyId = applicantParty.Id
            LEFT JOIN dbo.RequestReviews AS review
                ON review.RequestId = request.Id
            WHERE request.Id = @RequestId
              AND applicantParty.ClientAccountId = @ClientAccountId;
            """;

        var connection = _context.Database.GetDbConnection();
        var row = await connection.QuerySingleOrDefaultAsync<ClientRequestDetailsRow>(
            new CommandDefinition(
                sql,
                new
                {
                    RequestId = requestId,
                    ClientAccountId = clientAccountId
                },
                cancellationToken: cancellationToken));

        if (row is null)
        {
            return null;
        }

        var reviewStatus = Enum.TryParse<RequestReviewStatus>(
            row.ReviewStatus,
            ignoreCase: false,
            out var parsedReviewStatus)
                ? parsedReviewStatus
                : (RequestReviewStatus?)null;

        return new ClientRequestDetailsReadModel(
            row.RequestId,
            Enum.Parse<ClientRequestType>(row.RequestType, ignoreCase: false),
            Enum.Parse<RequestStatus>(row.Status, ignoreCase: false),
            row.CreatedAt,
            row.Details,
            row.PostalCode,
            row.Region,
            row.City,
            row.Street,
            row.House,
            row.Building,
            row.Apartment,
            reviewStatus,
            row.ReviewCompletedAt,
            row.RejectionReason);
    }

    private class ClientRequestSummaryRow
    {
        public long RequestId { get; set; }
        public string RequestType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public string Details { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string House { get; set; } = null!;
        public string? Building { get; set; }
        public string? Apartment { get; set; }
    }

    private sealed class ClientRequestDetailsRow : ClientRequestSummaryRow
    {
        public string? ReviewStatus { get; set; }
        public DateTimeOffset? ReviewCompletedAt { get; set; }
        public string? RejectionReason { get; set; }
    }
}
