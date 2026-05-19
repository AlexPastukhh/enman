using CSharpFunctionalExtensions;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;

namespace EnergyManagement.Server.Application.Queries;

public sealed record GetMyRequestDetailsQuery(
    long ClientAccountId,
    long RequestId)
    : IRequest<Maybe<MyRequestDetailsResponse>>;

public sealed record MyRequestDetailsResponse(
    long RequestId,
    ClientRequestType RequestType,
    RequestStatus Status,
    DateTimeOffset CreatedAt,
    SubmittedRequestResponse SubmittedRequest,
    MyRequestReviewResultResponse? ReviewResult);

public sealed record SubmittedRequestResponse(
    string Details,
    MyRequestAddressResponse ObjectAddress);

public sealed record MyRequestReviewResultResponse(
    RequestReviewDecision Decision,
    DateTimeOffset DecidedAt,
    MyRequestRejectionResponse? Rejection);

public enum RequestReviewDecision
{
    Approved = 1,
    Rejected = 2
}

public sealed record MyRequestRejectionResponse(string Reason);
