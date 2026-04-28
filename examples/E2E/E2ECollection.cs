using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Tests.E2E
{
    [CollectionDefinition("E2E")]
    public class E2ECollection:ICollectionFixture<SharedContext>
    {
    }
}
