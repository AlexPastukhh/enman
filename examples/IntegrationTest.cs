using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Tests
{
    public abstract class IntegrationTest
    {
        
        protected const string ConnectionString = "Data Source=DESKTOP-V6S02NC;Initial Catalog=Hospital.proj.Test;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        protected IntegrationTest()
        {
        }



    }
}
