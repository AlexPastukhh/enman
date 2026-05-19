using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;

namespace EnergyManagement.Server.Application.Abstractions;

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
