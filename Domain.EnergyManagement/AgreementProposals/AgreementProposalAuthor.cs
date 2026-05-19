using CSharpFunctionalExtensions;

namespace Domain.EnergyManagement;

public sealed class AgreementProposalAuthor : ValueObject
{
    public AgreementProposalSender Sender { get; private set; }

    public long SenderId { get; private set; }

    private AgreementProposalAuthor(
        AgreementProposalSender sender,
        long senderId)
    {
        if (senderId <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(senderId),
                "Proposal sender id must be positive.");
        }

        Sender = sender;
        SenderId = senderId;
    }

    private AgreementProposalAuthor()
    {
    }

    public static AgreementProposalAuthor Employee(Employee employee)
    {
        if (employee is null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        return new AgreementProposalAuthor(
            AgreementProposalSender.Employee,
            employee.Id);
    }

    public static AgreementProposalAuthor Client(ClientAccount client)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        return new AgreementProposalAuthor(
            AgreementProposalSender.Client,
            client.Id);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Sender;
        yield return SenderId;
    }
}
