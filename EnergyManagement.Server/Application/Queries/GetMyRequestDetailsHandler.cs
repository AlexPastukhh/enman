using CSharpFunctionalExtensions;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;

namespace EnergyManagement.Server.Application.Queries;

public sealed class GetMyRequestDetailsHandler
    : IRequestHandler<GetMyRequestDetailsQuery, Maybe<MyRequestDetailsResponse>>
{
    private readonly IClientRequestRepository _clientRequests;

    public GetMyRequestDetailsHandler(IClientRequestRepository clientRequests)
    {
        _clientRequests = clientRequests;
    }

    public async Task<Maybe<MyRequestDetailsResponse>> Handle(
        GetMyRequestDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var request = await _clientRequests.GetDetailsByIdAndClientAccountIdAsync(
            query.RequestId,
            query.ClientAccountId,
            cancellationToken);

        if (request is null)
        {
            return Maybe<MyRequestDetailsResponse>.None;
        }

        return new MyRequestDetailsResponse(
            request.RequestId,
            request.RequestType,
            request.Status,
            request.CreatedAt,
            new SubmittedRequestResponse(
                request.Details,
                new MyRequestAddressResponse(
                    request.PostalCode,
                    request.Region,
                    request.City,
                    request.Street,
                    request.House,
                    request.Building,
                    request.Apartment)),
            ToReviewResult(request));
    }

    private static MyRequestReviewResultResponse? ToReviewResult(
        ClientRequestDetailsReadModel request)
    {
        if (request.ReviewStatus is null || request.ReviewCompletedAt is null)
        {
            return null;
        }

        var decision = request.ReviewStatus.Value switch
        {
            RequestReviewStatus.Approved => RequestReviewDecision.Approved,
            RequestReviewStatus.Rejected => RequestReviewDecision.Rejected,
            _ => (RequestReviewDecision?)null
        };

        if (decision is null)
        {
            return null;
        }

        return new MyRequestReviewResultResponse(
            decision.Value,
            request.ReviewCompletedAt.Value,
            decision == RequestReviewDecision.Rejected
                && !string.IsNullOrWhiteSpace(request.RejectionReason)
                    ? new MyRequestRejectionResponse(request.RejectionReason)
                    : null);
    }
}
