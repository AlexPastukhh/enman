using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server;
using EnergyManagement.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tests.EnergyManagement.Integration;
using Tests.EnergyManagement.TestHelpers;

namespace Tests.EnergyManagement.TestHelpers
{
    public static class DatabaseHelpers
    {
        public const string ConnectionString =
            "Data Source=DESKTOP-V6S02NC;Initial Catalog=EnergyManagementTest;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static async Task AddValidIndividual(WebAppFactory factory)
        {
            using var scope = factory.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var individual = ValidTestData.GetIndividualWithoutFullData();
                context.Attach(individual);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task DeleteIndividual(string email, WebAppFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var individual = await context.Set<IndividualClient>().FirstOrDefaultAsync(x => x.Email.Value == email);
                context.Remove(individual!);
                await context.SaveChangesAsync();
            }
            catch(Exception)
            {
                
                throw;
            }
            
        }

        public static async Task AddIndividual(WebAppFactory factory, string validEmail, string validPassword)
        {
            var createIndividual = IndividualClient.Create(
                Email.Create(validEmail).Value,
                Password.Create(validPassword).Value);
                
            var individual = createIndividual.Value;

            using var scope = factory.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Attach(individual);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Result<IndividualClient>> GetIndividualByEmailAsync(WebAppFactory factory, string email)
        {
            using var scope = factory.Services.CreateScope();

            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }

        public static int GetCountOfIndividuals(WebAppFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                return context.Set<IndividualClient>().Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> IsIndividualExists(WebAppFactory factory, string email)
        {
            using var scope = factory.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var individual = await context.Set<IndividualClient>().FirstOrDefaultAsync(x => x.Email.Value == email);
                if (individual == null)
                {
                    return false;
                }
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        
    }
}