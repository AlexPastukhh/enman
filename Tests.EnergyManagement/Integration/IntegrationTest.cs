using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server;
using EnergyManagement.Server.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration
{
    public abstract class IntegrationTest
    {
        protected readonly WebAppFactory _factory;
        protected ITestOutputHelper _output;
            public const string ConnectionString =
                "Data Source=DESKTOP-V6S02NC;Initial Catalog=EnergyManagementTest;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public IntegrationTest(WebAppFactory factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }

        public void LogTwoErrorCollections(IEnumerable<ServerValidationError> expected,
            IEnumerable<ServerValidationError> actual)
        {
            _output?.WriteLine("Errors from expected collection:");
            foreach (var error in expected)
            {
                _output?.WriteLine($"Code: {error.ErrorCode}, Field: {error.FieldName}");
            }
            _output?.WriteLine("Errors from actual collection:");
            foreach (var error in actual)
            {
                _output?.WriteLine($"Code: {error.ErrorCode}, Field: {error.FieldName}");
            }
        }

        public void ClearDatabase()
        {
           string query =@"
           BEGIN TRANSACTION;
           DELETE FROM dbo.IndividualClients;
           DELETE FROM dbo.Clients;
           COMMIT TRANSACTION;";
           using (var connection = new SqlConnection(ConnectionString))
           {
               var command = new SqlCommand(query, connection)
               {
                   CommandType = CommandType.Text
               };
               try
               {
                    connection.Open();
                    command.ExecuteNonQuery();
               }
               catch (System.Exception)
               {
                
                throw;
               }
               finally
               {
                  if(connection.State == ConnectionState.Open)
                  {
                      connection.Close();
                  } 
               }
               
           }
        }
    }
    
}