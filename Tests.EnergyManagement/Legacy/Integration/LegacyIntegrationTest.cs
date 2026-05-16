using EnergyManagement.Server;
using Tests.EnergyManagement.Integration;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Legacy.Integration
{
    public abstract class LegacyIntegrationTest
    {
        protected readonly WebAppFactory _factory;
        protected ITestOutputHelper _output;


        public LegacyIntegrationTest(WebAppFactory factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;

        }
    }
    
}

