using AddressBookApi.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AddressBookApi.Tests
{
    public class AddressControllerTests : ControllerTestsBase
    {
        private readonly string _baseEndpoint = "https://localhost/api/Address";

        [Fact]
        public async Task GetLast_ShouldWork()
        {
            //Arrange
            await SetupAndRunHost();
            var firstAddress = new Address()
            {
                Id = 1,
                Name = "a",
                City = "sdfsdf",
                Street = "c"
            };
            var secondAddress = new Address()
            {
                Id = 2,
                Name = "q",
                City = "gdfg",
                Street = "w"
            };
            var thirdAddress = new Address()
            {
                Id = 3,
                Name = "y",
                City = "DDD",
                Street = "i"
            };

            _memoryCacheService.Addresses.Add(firstAddress);
            _memoryCacheService.Addresses.Add(secondAddress);
            _memoryCacheService.Addresses.Add(thirdAddress);

            var expectedStr = JsonSerializer.Serialize(thirdAddress);

            // Act
            var response = await _httpClient.GetAsync(_baseEndpoint);
            var actualStr = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedStr, actualStr, true);
        }

        [Fact]
        public async Task GetByCity_ShouldWork()
        {
            //Arrange
            await SetupAndRunHost();
            var firstAddress = new Address()
            {
                Id = 1,
                Name = "a",
                City = "LONDON",
                Street = "c"
            };
            var secondAddress = new Address()
            {
                Id = 2,
                Name = "q",
                City = "LONDON",
                Street = "w"
            };
            var thirdAddress = new Address()
            {
                Id = 3,
                Name = "y",
                City = "DDD",
                Street = "i"
            };

            _memoryCacheService.Addresses.Add(firstAddress);
            _memoryCacheService.Addresses.Add(secondAddress);
            _memoryCacheService.Addresses.Add(thirdAddress);

            var expectedStr = JsonSerializer.Serialize(new List<Address> { firstAddress, secondAddress });

            // Act
            var response = await _httpClient.GetAsync(_baseEndpoint + "/LONDON");
            var actualStr = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedStr, actualStr, true);

        }

        [Fact]
        public async Task GetById_ShouldWork()
        {
            //Arrange
            await SetupAndRunHost();
            var firstAddress = new Address()
            {
                Id = 1,
                Name = "a",
                City = "b",
                Street = "c"
            };

            _memoryCacheService.Addresses.Add(firstAddress);
            _memoryCacheService.Addresses.Add(new Address()
            {
                Id = 2,
                Name = "q",
                City = "w",
                Street = "e"
            });

            var expectedStr = JsonSerializer.Serialize(firstAddress);

            // Act
            var response = await _httpClient.GetAsync(_baseEndpoint + "/id/" + "1");
            var actualStr = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedStr, actualStr, true);
        }

        [Fact]
        public async Task Add_ShouldWork()
        {
            // Arrange
            await SetupAndRunHost();
            var content = new StringContent(JsonSerializer.Serialize(new Address
            {
                Id = 1,
                Name = "Aa",
                City = "qq",
                Street = "ww"
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_baseEndpoint, content);
            dynamic obj = JObject.Parse(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Single(_memoryCacheService.Addresses);
        }

        [Fact]
        public async Task Update_ShouldWork()
        {
            // Arrange
            await SetupAndRunHost();
            _memoryCacheService.Addresses.Add(new Address()
            {
                Id = 1,
                Name = "Aa",
                City = "qq",
                Street = "ww"
            });
            var content = new StringContent(JsonSerializer.Serialize(new Address
            {
                Id = 1,
                Name = "UPDATED",
                City = "qq",
                Street = "ww"
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync(_baseEndpoint + "/1", content);
            dynamic obj = JObject.Parse(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("UPDATED", (string)obj.name);
            Assert.Equal("UPDATED", _memoryCacheService.Addresses.First(a => a.Id == 1).Name);
        }

        [Fact]
        public async Task Delete_ShouldWork()
        {
            // Arrange 
            await SetupAndRunHost();
            _memoryCacheService.Addresses.Add(new Address()
            {
                Id = 1,
                Name = "Aa",
                City = "qq",
                Street = "ww"
            });

            // Act
            var response = await _httpClient.DeleteAsync(_baseEndpoint + "/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(_memoryCacheService.Addresses);
        }

    }
}
