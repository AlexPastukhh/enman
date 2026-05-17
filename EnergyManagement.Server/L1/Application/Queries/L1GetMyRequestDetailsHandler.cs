using CSharpFunctionalExtensions;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Queries;

public sealed class L1GetMyRequestDetailsHandler
    : IRequestHandler<L1GetMyRequestDetailsQuery, Maybe<L1MyRequestDetailsResponse>>
{
    private readonly IClientRequestRepository _clientRequests;

    public L1GetMyRequestDetailsHandler(IClientRequestRepository clientRequests)
    {
        _clientRequests = clientRequests;
    }

    public async Task<Maybe<L1MyRequestDetailsResponse>> Handle(
        L1GetMyRequestDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var request = await _clientRequests.GetDetailsByIdAndClientAccountIdAsync(
            query.RequestId,
            query.ClientAccountId,
            cancellationToken);

        if (request is null)
        {
            return Maybe<L1MyRequestDetailsResponse>.None;
        }

        return new L1MyRequestDetailsResponse(
            request.RequestId,
            request.RequestType,
            request.Status,
            request.CreatedAt,
            new L1SubmittedRequestResponse(
                request.Details,
                new L1MyRequestAddressResponse(
                    request.PostalCode,
                    request.Region,
                    request.City,
                    request.Street,
                    request.House,
                    request.Building,
                    request.Apartment)),
            ToReviewResult(request));
    }

    private static L1MyRequestReviewResultResponse? ToReviewResult(
        ClientRequestDetailsReadModel request)
    {
        if (request.ReviewStatus is null || request.ReviewCompletedAt is null)
        {
            return null;
        }

        var decision = request.ReviewStatus.Value switch
        {
            RequestReviewStatus.Approved => L1RequestReviewDecision.Approved,
            RequestReviewStatus.Rejected => L1RequestReviewDecision.Rejected,
            _ => (L1RequestReviewDecision?)null
        };

        if (decision is null)
        {
            return null;
        }

        return new L1MyRequestReviewResultResponse(
            decision.Value,
            request.ReviewCompletedAt.Value,
            decision == L1RequestReviewDecision.Rejected
                && !string.IsNullOrWhiteSpace(request.RejectionReason)
                    ? new L1MyRequestRejectionResponse(request.RejectionReason)
                    : null);
    }
}
