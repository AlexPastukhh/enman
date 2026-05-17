using EnergyManagement.Server;
using Tests.EnergyManagement.Legacy.TestHelpers;
using Tests.EnergyManagement.Integration;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Legacy.Integration
{
    public abstract class LegacyIntegrationTest : IAsyncLifetime
    {
        protected readonly WebAppFactory _factory;
        protected ITestOutputHelper _output;
        protected TestIndividualActor Client { get; private set; } = null!;


        public LegacyIntegrationTest(WebAppFactory factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;

        }

        protected virtual bool CreateLegacyBaselineClient => false;

        public async Task InitializeAsync()
        {
            if (!CreateLegacyBaselineClient)
            {
                return;
            }

            Client = await DatabaseHelpers.CreateRegisteredIndividualAsync(
                _factory,
                $"fixture-{Guid.NewGuid():N}@example.com",
                ValidTestData.ValidPassword);
        }

        public async Task DisposeAsync()
        {
            if (CreateLegacyBaselineClient && Client is not null)
            {
                await DatabaseHelpers.DeleteIndividual(Client.Email, _factory);
            }
        }
    }
    
}

