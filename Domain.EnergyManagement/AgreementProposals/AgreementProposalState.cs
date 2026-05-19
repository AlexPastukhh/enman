namespace Domain.EnergyManagement;

public enum AgreementProposalState
{
    AwaitingClientConfirmation = 1,
    SentByClient = 2,
    Accepted = 3,
    SupersededByCounterProposal = 4
}
