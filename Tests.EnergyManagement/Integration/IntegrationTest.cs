using EnergyManagement.Server;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration
{
    public abstract class IntegrationTest
    {
        protected readonly WebAppFactory _factory;
        protected ITestOutputHelper _output;


        public IntegrationTest(WebAppFactory factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;

        }
    }
    
}
