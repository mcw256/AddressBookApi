using AddressBookApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApi.Tests
{
    public abstract class ControllerTestsBase
    {
        protected IHost _host;
        
        protected HttpClient _httpClient;

        protected IMemoryCacheService _memoryCacheService;

        protected async Task SetupAndRunHost()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<AddressBookApi.Startup>();
                });

            // Create and start up the host
            _host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            _httpClient = _host.GetTestClient();

            // Create service handle
            _memoryCacheService = ((IMemoryCacheService)_host.Services.GetService(typeof(IMemoryCacheService)));
        }
    }
}
