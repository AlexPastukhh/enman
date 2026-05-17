namespace Domain.EnergyManagement.L1;

public enum AgreementProposalState
{
    AwaitingClientConfirmation = 1,
    SentByClient = 2,
    Accepted = 3,
    SupersededByCounterProposal = 4
}
