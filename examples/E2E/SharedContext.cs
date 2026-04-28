using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Playwright;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Tests.E2E
{
    public class SharedContext:IAsyncLifetime
    {
        private IPlaywright _playwright;
        public IBrowser Browser { get; private set; }
        public const string AppUrl = Routes.BaseSpaUrl;
        private const string ConnectionString = "Data Source=DESKTOP-V6S02NC;Initial Catalog=FinalHospitalServerDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public const string FirstPassword = "eword24sdfdf";
        public const string SecondPassword = "Anothereword24sdfdf";


        public const string TestClientEmail =
            "alisa.lebsack37@ethereal.email";
        public const string TestPassword =
            "aS2hvZXP8Tak8URxZK";
        public SharedContext()
        {
            
        }
        public Task DisposeAsync()
        {
            _playwright.Dispose();
            Browser.DisposeAsync();
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Headless = false,
                SlowMo = 100
            });


            string query ="DELETE FROM dbo.Users WHERE Email=@Email;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text
                };

                command.Parameters.AddWithValue(
                    "@Email",
                    TestClientEmail);

                connection.Open();
                command.ExecuteNonQuery();
            }

        }

        public void SetLogsForDebug(IPage page, bool logRedirects = false)
        {

            page.Request += (_, request) =>
            {
                LogRequest(request, logRedirects);
            };


            page.Response +=  (_, response) =>
            {
                LogResponse(response);
            };

        }

        public void LogRequest(IRequest request, bool logRedirects)
        {

            Log.Debug("\nRequest method: {method}", request.Method);

            Log.Debug("\nRequest url: {url}", request.Url);

            Log.Debug("Request headers:");
            foreach (var header in request.Headers)
            {
                Log.Debug("Header:{header}", header.ToString());
            }

            Log.Debug("\nRequest body: {body}", request.PostDataJSON());

            Log.Debug("\nRequest body binary: {bodyBinary}", request.PostDataBuffer);



            if (request.RedirectedFrom is not null)
            {
                Log.Debug("\nRedirected from: {url}", request.RedirectedFrom.Url);

                if (!logRedirects)
                {
                    return;
                }

                LogRequest(request.RedirectedFrom, logRedirects);
            }
        }

        public void  LogResponse(IResponse response)
        {
            //Log.Debug("\nResponse body: {body}", await response.BodyAsync());

            Log.Debug("Response headers:");
            foreach (var header in response.Headers)
            {
                Log.Debug("Header:{header}", header.ToString());
            }

        }

        public void MonitorPageErrors(
            IPage page,
            IEnumerable<string> intoCollection)
        {
            page.Console += (_, msg) =>
            {
                if (msg.Type == "error")
                {
                    intoCollection.Append(msg.Text);
                }
            };

        }

        public void MonitorServerResponse(
            IPage page,
            IResponse response)
        {
            page.Response += (_, resp) =>
            {
                response = resp;
            };

        }
    }
}
