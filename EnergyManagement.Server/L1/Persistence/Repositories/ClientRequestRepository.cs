using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server.L1.Persistence.Repositories;

public sealed class ClientRequestRepository : IClientRequestRepository
{
    private readonly L1DbContext _context;

    public ClientRequestRepository(L1DbContext context)
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
        var query =
            from request in _context.ClientRequests.AsNoTracking()
            join applicantParty in _context.ApplicantParties.AsNoTracking()
                on request.ApplicantPartyId equals applicantParty.Id
            where applicantParty.ClientAccountId == clientAccountId
            select request;

        if (status is not null)
        {
            query = query.Where(request => request.Status == status);
        }

        var requests = await query
            .OrderByDescending(request => request.CreatedAt)
            .ThenByDescending(request => request.Id)
            .ToListAsync(cancellationToken);

        return requests
            .Select(request => new ClientRequestSummaryReadModel(
                request.Id,
                request.RequestType,
                request.Status,
                request.CreatedAt,
                request.Details,
                request.ObjectAddress.PostalCode,
                request.ObjectAddress.Region,
                request.ObjectAddress.City,
                request.ObjectAddress.Street,
                request.ObjectAddress.House,
                request.ObjectAddress.Building.HasValue
                    ? request.ObjectAddress.Building.Value
                    : null,
                request.ObjectAddress.Apartment.HasValue
                    ? request.ObjectAddress.Apartment.Value
                    : null))
            .ToList();
    }

    public async Task<ClientRequestDetailsReadModel?> GetDetailsByIdAndClientAccountIdAsync(
        long requestId,
        long clientAccountId,
        CancellationToken cancellationToken)
    {
        var row = await (
                from clientRequest in _context.ClientRequests.AsNoTracking()
                join applicantParty in _context.ApplicantParties.AsNoTracking()
                    on clientRequest.ApplicantPartyId equals applicantParty.Id
                where clientRequest.Id == requestId
                    && applicantParty.ClientAccountId == clientAccountId
                select new
                {
                    Request = clientRequest,
                    ReviewDecision = EF.Property<string?>(clientRequest, "ReviewDecision"),
                    ReviewDecidedAt = EF.Property<DateTimeOffset?>(clientRequest, "ReviewDecidedAt"),
                    RejectionReason = EF.Property<string?>(clientRequest, "ReviewRejectionReason")
                })
            .FirstOrDefaultAsync(cancellationToken);

        if (row is null)
        {
            return null;
        }

        var request = row.Request;
        var reviewDecision = Enum.TryParse<ReviewDecision>(
            row.ReviewDecision,
            ignoreCase: false,
            out var parsedDecision)
                ? parsedDecision
                : (ReviewDecision?)null;

        return new ClientRequestDetailsReadModel(
            request.Id,
            request.RequestType,
            request.Status,
            request.CreatedAt,
            request.Details,
            request.ObjectAddress.PostalCode,
            request.ObjectAddress.Region,
            request.ObjectAddress.City,
            request.ObjectAddress.Street,
            request.ObjectAddress.House,
            request.ObjectAddress.Building.HasValue
                ? request.ObjectAddress.Building.Value
                : null,
            request.ObjectAddress.Apartment.HasValue
                ? request.ObjectAddress.Apartment.Value
                : null,
            reviewDecision,
            row.ReviewDecidedAt,
            row.RejectionReason);
    }
}
