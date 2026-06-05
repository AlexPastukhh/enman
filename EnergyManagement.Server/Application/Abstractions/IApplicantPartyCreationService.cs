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

    Task<Result<IndividualEntrepreneurApplicantParty, IReadOnlyList<Error>>> CreateIndividualEntrepreneurAsync(
        long clientAccountId,
        string firstName,
        string middleName,
        string lastName,
        string inn,
        string ogrnip,
        string email,
        string phoneNumber,
        CancellationToken cancellationToken);

    Task<Result<LegalEntityApplicantParty, IReadOnlyList<Error>>> CreateLegalEntityAsync(
        long clientAccountId,
        string organizationName,
        string inn,
        string kpp,
        string ogrn,
        string email,
        string phoneNumber,
        CancellationToken cancellationToken);
}
