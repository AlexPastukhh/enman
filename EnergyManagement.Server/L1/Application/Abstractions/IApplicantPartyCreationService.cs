using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IApplicantPartyCreationService
{
    Task<Result<IndividualApplicantParty, IReadOnlyList<Error>>> CreateIndividualAsync(
        long clientAccountId,
        string firstName,
        string middleName,
        string lastName,
        string email,
        string phoneNumber,
        CancellationToken cancellationToken);
}
