using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class AgreementProposal
{
    public long Id { get; private set; }

    public long AgreementProposalExchangeId { get; private set; }

    public AgreementProposalVersion Version { get; private set; }

    public AgreementProposalAuthor Author { get; private set; }

    public AgreementProposalState State { get; private set; }

    public AgreementDocumentRef Document { get; private set; }

    public ProposalComment? Comment { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    private AgreementProposal()
    {
        Author = null!;
        Document = null!;
    }

    internal static AgreementProposal EmployeeProposal(
        AgreementProposalVersion version,
        AgreementDocumentRef document,
        ProposalComment? comment,
        Employee employee,
        DateTimeOffset createdAt)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        return new AgreementProposal
        {
            Version = version,
            Author = AgreementProposalAuthor.Employee(employee),
            State = AgreementProposalState.AwaitingClientConfirmation,
            Document = document,
            Comment = comment,
            CreatedAt = createdAt
        };
    }

    internal static AgreementProposal ClientProposal(
        AgreementProposalVersion version,
        AgreementDocumentRef document,
        ProposalComment? comment,
        ClientAccount client,
        DateTimeOffset createdAt)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        return new AgreementProposal
        {
            Version = version,
            Author = AgreementProposalAuthor.Client(client),
            State = AgreementProposalState.SentByClient,
            Document = document,
            Comment = comment,
            CreatedAt = createdAt
        };
    }

    internal UnitResult<IReadOnlyList<Error>> MarkAccepted()
    {
        if (Author.Sender != AgreementProposalSender.Employee)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyEmployeeProposalCanBeAccepted]);
        }

        if (State != AgreementProposalState.AwaitingClientConfirmation)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyAwaitingClientConfirmationProposalCanBeAccepted]);
        }

        State = AgreementProposalState.Accepted;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    internal UnitResult<IReadOnlyList<Error>> MarkSupersededByCounterProposal()
    {
        if (State == AgreementProposalState.Accepted)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AcceptedProposalCannotBeSuperseded]);
        }

        if (State == AgreementProposalState.SupersededByCounterProposal)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ProposalAlreadySuperseded]);
        }

        State = AgreementProposalState.SupersededByCounterProposal;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
