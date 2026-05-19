using CSharpFunctionalExtensions;
using Dapper;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Configuration;
using EnergyManagement.Server.Application.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Services;

public sealed class AgreementExchangeReadService : IAgreementExchangeReadService
{
    private readonly IConfiguration _configuration;
    private readonly IEmployeeRepository _employees;

    public AgreementExchangeReadService(
        IConfiguration configuration,
        IEmployeeRepository employees)
    {
        _configuration = configuration;
        _employees = employees;
    }

    public async Task<Result<AgreementExchangeListResponse, IReadOnlyList<Error>>> ListForClientAsync(
        long clientAccountId,
        AgreementExchangeStatus? status,
        CancellationToken cancellationToken)
    {
        if (clientAccountId <= 0)
        {
            return Result.Failure<AgreementExchangeListResponse, IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientAccountIsRequired]);
        }

        var exchanges = await ListAsync(
            clientAccountId,
            employeeView: false,
            status,
            cancellationToken);

        return Result.Success<AgreementExchangeListResponse, IReadOnlyList<Error>>(
            new AgreementExchangeListResponse(exchanges));
    }

    public async Task<Result<AgreementExchangeListResponse, IReadOnlyList<Error>>> ListForEmployeeAsync(
        long employeeId,
        AgreementExchangeStatus? status,
        CancellationToken cancellationToken)
    {
        var employee = await _employees.GetByIdAsync(employeeId, cancellationToken);
        if (employee is null)
        {
            return Result.Failure<AgreementExchangeListResponse, IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        var canSend = employee.EnsureCanSendAgreementProposal();
        if (canSend.IsFailure)
        {
            return Result.Failure<AgreementExchangeListResponse, IReadOnlyList<Error>>(canSend.Error);
        }

        var exchanges = await ListAsync(
            clientAccountId: null,
            employeeView: true,
            status,
            cancellationToken);

        return Result.Success<AgreementExchangeListResponse, IReadOnlyList<Error>>(
            new AgreementExchangeListResponse(exchanges));
    }

    private async Task<IReadOnlyList<AgreementExchangeListItemResponse>> ListAsync(
        long? clientAccountId,
        bool employeeView,
        AgreementExchangeStatus? status,
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
                activeProposal.Sender AS ActiveProposalSender,
                activeProposal.SenderId AS ActiveProposalSenderId,
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
                exchange.CreatedAt,
                MAX(proposal.CreatedAt) AS LastActivityAt
            FROM dbo.L1AgreementProposalExchanges AS exchange
            INNER JOIN dbo.L1ClientRequests AS request
                ON request.Id = exchange.RequestId
            INNER JOIN dbo.L1ApplicantParties AS applicant
                ON applicant.Id = request.ApplicantPartyId
            INNER JOIN dbo.L1AgreementProposals AS activeProposal
                ON activeProposal.AgreementProposalExchangeId = exchange.Id
                AND activeProposal.Version = exchange.ActiveProposalVersion
            LEFT JOIN dbo.L1AgreementProposals AS proposal
                ON proposal.AgreementProposalExchangeId = exchange.Id
            WHERE (@EmployeeView = 1 OR exchange.ClientAccountId = @ClientAccountId)
                AND (@Status IS NULL OR exchange.Status = @Status)
            GROUP BY
                exchange.Id,
                exchange.RequestId,
                exchange.Status,
                exchange.ActiveProposalVersion,
                activeProposal.Sender,
                activeProposal.SenderId,
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
                exchange.CreatedAt
            ORDER BY COALESCE(MAX(proposal.CreatedAt), exchange.CreatedAt) DESC, exchange.Id DESC;
            """;

        var rows = await connection.QueryAsync<AgreementExchangeListRow>(
            new CommandDefinition(
                sql,
                new
                {
                    ClientAccountId = clientAccountId,
                    EmployeeView = employeeView,
                    Status = status?.ToString()
                },
                cancellationToken: cancellationToken));

        return rows.Select(ToResponse).ToList();
    }

    private static AgreementExchangeListItemResponse ToResponse(AgreementExchangeListRow row)
    {
        return new AgreementExchangeListItemResponse(
            row.ExchangeId,
            row.RequestId,
            row.ExchangeStatus,
            row.ActiveProposalVersion,
            row.ActiveProposalSender,
            row.ActiveProposalSenderId,
            FormatApplicantDisplayName(row),
            FormatAddress(row),
            row.CreatedAt,
            row.LastActivityAt);
    }

    private static string FormatApplicantDisplayName(AgreementExchangeListRow row)
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

    private static string FormatAddress(AgreementExchangeListRow row)
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

    private sealed class AgreementExchangeListRow
    {
        public long ExchangeId { get; init; }
        public long RequestId { get; init; }
        public string ExchangeStatus { get; init; } = string.Empty;
        public int ActiveProposalVersion { get; init; }
        public string ActiveProposalSender { get; init; } = string.Empty;
        public long ActiveProposalSenderId { get; init; }
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
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? LastActivityAt { get; init; }
    }
}
