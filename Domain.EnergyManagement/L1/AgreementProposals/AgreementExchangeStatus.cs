namespace Domain.EnergyManagement.L1;

public enum AgreementExchangeStatus
{
    AwaitingClientConfirmation = 1,
    AwaitingEmployeeResponse = 2,
    Accepted = 3,
    FinallyRefused = 4
}
