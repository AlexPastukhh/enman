using System.Collections.Generic;
using Tests.EnergyManagement.TestHelpers;

namespace Tests.EnergyManagement.Unit
{
    public class ClientRequestUnitBase
    {
        public static IEnumerable<object[]>GetValidRequestData()
        {
            yield return new object[]
            {
                new string('A', 1000),ValidTestData.GetAddressWithApartment()
            };
            yield return new object[]
            {
                new string('A', 100),ValidTestData.GetAddressWithApartmentAndBuilding()
            };
            yield return new object[]
            {
                new string('A', 1000),ValidTestData.GetAddressWithBuilding()
            };
            yield return new object[]
            {
                new string('A', 100),ValidTestData.GetAddressWithOutApartmentAndBuilding()
            };
        }
    }
}
