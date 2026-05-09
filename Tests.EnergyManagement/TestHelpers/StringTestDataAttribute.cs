using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Tests.EnergyManagement.TestHelpers
{
    public class StringTestDataAttribute : DataAttribute
    {
        private readonly int[] _lengths;

    
        public StringTestDataAttribute(params int[] lengths)
        {
            _lengths = lengths;
        }
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return _lengths.Select(length=>new object[]{new string('A',length)});
        }
    }
}
