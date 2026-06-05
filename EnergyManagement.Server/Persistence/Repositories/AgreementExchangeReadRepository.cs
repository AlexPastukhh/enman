using Dapper;
using EnergyManagement.Server.Configuration;
using EnergyManagement.Server.Application.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EnergyManagement.Server.Persistence.Repositories;

public sealed class AgreementExchangeReadRepository : IAgreementExchangeReadRepository
{
    private readonly IConfiguration _configuration;

    public AgreementExchangeReadRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<AgreementExchangeDetailsResponse?> GetDetailsForClientAsync(
        long clientAccountId,
        long exchangeId,
        CancellationToken cancellationToken)
    {
        return GetDetailsAsync(
            exchangeId,
            clientAccountId,
            employeeView: false,
            currentActorSide: "Client",
            cancellationToken);
    }

    public Task<AgreementExchangeDetailsResponse?> GetDetailsForEmployeeAsync(
        long employeeId,
        long exchangeId,
        CancellationToken cancellationToken)
    {
        return GetDetailsAsync(
            exchangeId,
            clientAccountId: null,
            employeeView: true,
            currentActorSide: "Employee",
            cancellationToken);
    }

    private async Task<AgreementExchangeDetailsResponse?> GetDetailsAsync(
        long exchangeId,
        long? clientAccountId,
        bool employeeView,
        string currentActorSide,
        CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(
            _configuration.GetConnectionString(ConnectionStringNames.ManagementDb));

        const string sql = """
            SELECT
                exchange.Id AS ExchangeId,
                exchange.RequestId,
                exchange.Status AS ExchangeStatus,
                exchange.ActiveProposalVersion,
                exchange.CreatedAt,
                MAX(proposal.CreatedAt) AS LastActivityAt,
                request.Status AS RequestStatus,
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
                applicant.ApplicantPartyType,
                applicant.IndividualEntrepreneurFullName_FirstName AS IndividualEntrepreneurFirstName,
                applicant.IndividualEntrepreneurFullName_MiddleName AS IndividualEntrepreneurMiddleName,
                applicant.IndividualEntrepreneurFullName_LastName AS IndividualEntrepreneurLastName,
                applicant.LegalEntity_OrganizationName AS LegalEntityOrganizationName
            FROM dbo.L1AgreementProposalExchanges AS exchange
            INNER JOIN dbo.L1ClientRequests AS request
                ON request.Id = exchange.RequestId
            INNER JOIN dbo.L1ApplicantParties AS applicant
                ON applicant.Id = request.ApplicantPartyId
            LEFT JOIN dbo.L1AgreementProposals AS proposal
                ON proposal.AgreementProposalExchangeId = exchange.Id
            WHERE exchange.Id = @ExchangeId
                AND (@EmployeeView = 1 OR exchange.ClientAccountId = @ClientAccountId)
            GROUP BY
                exchange.Id,
                exchange.RequestId,
                exchange.Status,
                exchange.ActiveProposalVersion,
                exchange.CreatedAt,
                request.Status,
                request.ObjectAddress_PostalCode,
                request.ObjectAddress_Region,
                request.ObjectAddress_City,
                request.ObjectAddress_Street,
                request.ObjectAddress_House,
                request.ObjectAddress_Building,
                request.ObjectAddress_Apartment,
                applicant.FullName_FirstName,
                applicant.FullName_MiddleName,
                applicant.FullName_LastName,
                applicant.ApplicantPartyType,
                applicant.IndividualEntrepreneurFullName_FirstName,
                applicant.IndividualEntrepreneurFullName_MiddleName,
                applicant.IndividualEntrepreneurFullName_LastName,
                applicant.LegalEntity_OrganizationName;

            SELECT
                proposal.Id AS ProposalId,
                proposal.Version,
                proposal.Sender,
                proposal.SenderId,
                proposal.State,
                proposal.DocumentStorageKey AS StorageKey,
                proposal.DocumentOriginalFileName AS OriginalFileName,
                proposal.DocumentContentType AS ContentType,
                proposal.DocumentSizeBytes AS SizeBytes,
                proposal.Comment,
                proposal.CreatedAt
            FROM dbo.L1AgreementProposals AS proposal
            WHERE proposal.AgreementProposalExchangeId = @ExchangeId
            ORDER BY proposal.Version ASC;
            """;

        using var reader = await connection.QueryMultipleAsync(
            new CommandDefinition(
                sql,
                new
                {
                    ExchangeId = exchangeId,
                    ClientAccountId = clientAccountId,
                    EmployeeView = employeeView
                },
                cancellationToken: cancellationToken));

        var exchange = await reader.ReadSingleOrDefaultAsync<AgreementExchangeDetailsRow>();
        if (exchange is null)
        {
            return null;
        }

        var proposals = (await reader.ReadAsync<AgreementProposalDetailsRow>()).ToList();
        var activeProposal = proposals.SingleOrDefault(x => x.Version == exchange.ActiveProposalVersion)
            ?? throw new InvalidOperationException(
                $"Agreement exchange {exchange.ExchangeId} active proposal version {exchange.ActiveProposalVersion} was not found.");

        return new AgreementExchangeDetailsResponse(
            exchange.ExchangeId,
            exchange.RequestId,
            exchange.ExchangeStatus,
            exchange.ActiveProposalVersion,
            new AgreementExchangeRequestSummaryResponse(
                exchange.RequestId,
                exchange.RequestStatus,
                FormatApplicantDisplayName(exchange),
                FormatAddress(exchange)),
            ToResponse(activeProposal),
            proposals.Select(ToResponse).ToList(),
            currentActorSide,
            exchange.CreatedAt,
            exchange.LastActivityAt);
    }

    private static AgreementProposalDetailsResponse ToResponse(AgreementProposalDetailsRow row)
    {
        return new AgreementProposalDetailsResponse(
            row.ProposalId,
            row.Version,
            row.Sender,
            row.SenderId,
            row.State,
            new AgreementDocumentRefResponse(
                row.StorageKey,
                row.OriginalFileName,
                row.ContentType,
                row.SizeBytes),
            row.Comment,
            row.CreatedAt);
    }

    private static string FormatApplicantDisplayName(AgreementExchangeDetailsRow row)
    {
        if (row.ApplicantPartyType == "IndividualEntrepreneur")
        {
            var fullName = FormatFullName(
                row.IndividualEntrepreneurLastName,
                row.IndividualEntrepreneurFirstName,
                row.IndividualEntrepreneurMiddleName);

            return string.IsNullOrWhiteSpace(fullName)
                ? string.Empty
                : $"IP {fullName}";
        }

        if (row.ApplicantPartyType == "LegalEntity")
        {
            return row.LegalEntityOrganizationName ?? string.Empty;
        }

        return FormatFullName(
            row.ApplicantLastName,
            row.ApplicantFirstName,
            row.ApplicantMiddleName);
    }

    private static string FormatFullName(
        string? lastName,
        string? firstName,
        string? middleName)
    {
        return string.Join(
                " ",
                new[]
                {
                    lastName,
                    firstName,
                    middleName
                }.Where(value => !string.IsNullOrWhiteSpace(value)))
            .Trim();
    }

    private static string FormatAddress(AgreementExchangeDetailsRow row)
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

    private sealed class AgreementExchangeDetailsRow
    {
        public long ExchangeId { get; init; }
        public long RequestId { get; init; }
        public string ExchangeStatus { get; init; } = string.Empty;
        public int ActiveProposalVersion { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? LastActivityAt { get; init; }
        public string RequestStatus { get; init; } = string.Empty;
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
        public string ApplicantPartyType { get; init; } = string.Empty;
        public string? IndividualEntrepreneurFirstName { get; init; }
        public string? IndividualEntrepreneurMiddleName { get; init; }
        public string? IndividualEntrepreneurLastName { get; init; }
        public string? LegalEntityOrganizationName { get; init; }
    }

    private sealed class AgreementProposalDetailsRow
    {
        public long ProposalId { get; init; }
        public int Version { get; init; }
        public string Sender { get; init; } = string.Empty;
        public long SenderId { get; init; }
        public string State { get; init; } = string.Empty;
        public string StorageKey { get; init; } = string.Empty;
        public string OriginalFileName { get; init; } = string.Empty;
        public string ContentType { get; init; } = string.Empty;
        public long SizeBytes { get; init; }
        public string? Comment { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
