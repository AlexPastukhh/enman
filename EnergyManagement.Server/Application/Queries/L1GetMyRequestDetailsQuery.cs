using CSharpFunctionalExtensions;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;

namespace EnergyManagement.Server.Application.Queries;

public sealed record L1GetMyRequestDetailsQuery(
    long ClientAccountId,
    long RequestId)
    : IRequest<Maybe<L1MyRequestDetailsResponse>>;

public sealed record L1MyRequestDetailsResponse(
    long RequestId,
    ClientRequestType RequestType,
    RequestStatus Status,
    DateTimeOffset CreatedAt,
    L1SubmittedRequestResponse SubmittedRequest,
    L1MyRequestReviewResultResponse? ReviewResult);

public sealed record L1SubmittedRequestResponse(
    string Details,
    L1MyRequestAddressResponse ObjectAddress);

public sealed record L1MyRequestReviewResultResponse(
    L1RequestReviewDecision Decision,
    DateTimeOffset DecidedAt,
    L1MyRequestRejectionResponse? Rejection);

public enum L1RequestReviewDecision
{
    Approved = 1,
    Rejected = 2
}

public sealed record L1MyRequestRejectionResponse(string Reason);
