using CSharpFunctionalExtensions;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server;
using EnergyManagement.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tests.EnergyManagement.Integration;

namespace Tests.EnergyManagement.TestHelpers
{
    public static class DatabaseHelpers
    {

        public static async Task AddValidIndividual(WebAppFactory factory)
        {
            using var scope = factory.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var individual = ValidTestData.GetIndividualWithoutFullData();
            context.Attach(individual);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteIndividual(string email, WebAppFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var individual = await context.Set<IndividualClient>().FirstOrDefaultAsync(x => x.Email.Value == email);
            if (individual != null)
            {
                context.Remove(individual);
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddIndividual(WebAppFactory factory, string validEmail, string validPassword)
        {
            var createIndividual = IndividualClient.Create(
                Email.Create(validEmail).Value,
                Password.Create(validPassword).Value);
                
            var individual = createIndividual.Value;

            using var scope = factory.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Attach(individual);
            await context.SaveChangesAsync();
        }

        public static async Task<TestIndividualActor> CreateRegisteredIndividualAsync(
            WebAppFactory factory,
            string email,
            string password)
        {
            await AddIndividual(factory, email, password);
            var addedIndividual = await GetIndividualByEmailAsync(factory, email);

            return new TestIndividualActor(
                addedIndividual.Value.Id,
                addedIndividual.Value.Email.Value,
                password,
                IntegrationTestHelper.GetClaimsForIndividual(addedIndividual.Value));
        }

        public static Task<TestIndividualActor> CreateRegisteredIndividualAsync(WebAppFactory factory)
        {
            var email = $"test-{Guid.NewGuid():N}@example.com";
            return CreateRegisteredIndividualAsync(factory, email, ValidTestData.ValidPassword);
        }

        public static async Task<Result<IndividualClient>> GetIndividualByEmailAsync(WebAppFactory factory, string email)
        {
            using var scope = factory.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var individual = await context.Set<IndividualClient>()
                .FirstOrDefaultAsync(x => x.Email.Value == email);
            if (individual == null)
            {
                return Result.Failure<IndividualClient>("Client not found");
            }
            context.Entry(individual).Collection(i => i.ClientRequests).Load();
            return Result.Success(individual);
        }

        public static int GetCountOfIndividuals(WebAppFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return context.Set<IndividualClient>().Count();
        }

        public static async Task<bool> IsIndividualExists(WebAppFactory factory, string email)
        {
            using var scope = factory.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var individual = await context.Set<IndividualClient>().FirstOrDefaultAsync(x => x.Email.Value == email);
            return individual != null;
        }

        
    }
}
