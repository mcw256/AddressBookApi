using AddressBookApi.Controllers;
using AddressBookApi.Models;
using AddressBookApi.Repositories;
using AddressBookApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AddressBookApi.Tests
{
    public class AddressControllerTests
    {
        private readonly string _baseEndpoint = "https://localhost/api/Address";

        [Fact]
        public async Task GetLast_ShouldWork()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<AddressBookApi.Startup>();
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            // Act
            var response = await client.GetAsync(_baseEndpoint);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetByCity_ShouldWork()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<AddressBookApi.Startup>();
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            // Act
            var response = await client.GetAsync(_baseEndpoint + "/" + "exampleCity");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task GetById_ShouldWork()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<AddressBookApi.Startup>();
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            // Act
            var response = await client.GetAsync(_baseEndpoint + "/id/" + "4");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Add_ShouldWork()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<AddressBookApi.Startup>();
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            var content = new StringContent(JsonConvert.SerializeObject(new Address
            {
                Id = 1,
                Name = "Aa",
                City = "qq",
                Street = "ww"
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(_baseEndpoint, content);

            dynamic obj = JObject.Parse(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotEqual(0, (int)obj.id);
        }

        [Fact]
        public async Task Update_ShouldWork()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<AddressBookApi.Startup>();
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();


            ((IMemoryCacheService)host.Services.GetService(typeof(IMemoryCacheService))).Addresses.Add(new Address()
            {
                Id = 1,
                Name = "Aa",
                City = "qq",
                Street = "ww"
            });


            var content = new StringContent(JsonConvert.SerializeObject(new Address
            {
                Id = 1,
                Name = "UPDATED",
                City = "qq",
                Street = "ww"
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(_baseEndpoint + "/1", content);

            dynamic obj = JObject.Parse(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("UPDATED", (string)obj.name);

        }

        [Fact] 
        public async Task Delete_ShouldWork()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<AddressBookApi.Startup>();
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            ((IMemoryCacheService)host.Services.GetService(typeof(IMemoryCacheService))).Addresses.Add(new Address()
            {
                Id = 1,
                Name = "Aa",
                City = "qq",
                Street = "ww"
            });

            // Act
            var response = await client.DeleteAsync(_baseEndpoint + "/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
