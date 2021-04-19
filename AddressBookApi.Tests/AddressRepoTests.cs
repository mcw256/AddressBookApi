using AddressBookApi.Models;
using AddressBookApi.Repositories;
using AddressBookApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AddressBookApi.Tests
{
    public class AddressRepoTests
    {
        private readonly IAddressRepo _sut;
        private readonly IMemoryCacheService _memoryCacheService;

        public AddressRepoTests()
        {
            _memoryCacheService = new MemoryCacheService();
            _sut = new AddressRepo(_memoryCacheService);
        }

        private void CleanMemoryCache()
        {
            _memoryCacheService.Addresses = new List<Address>();
        }

        [Fact]
        public async void GetLastAddress_ShouldReturnLastAddress()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new Address() { Id = 2, Name = "A2", City = "B2", Street = "C2" };
            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A1", City = "B1", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A2", City = "B2", Street = "C2" });

            // Act
            var actual = await _sut.GetLastAddress();

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void GetLastAddress_ShouldReturnEmptyAddress()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new Address(); // empty address

            // Act
            var actual = await _sut.GetLastAddress();

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void GetAddressesByCity_ShouldReturnOneAddress()
        {
            // Arrange
            CleanMemoryCache();
            var secondAddress = new Address() { Id = 2, Name = "A", City = "B2", Street = "C2" };
            var expected = new List<Address>() { secondAddress };

            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B1", Street = "C1" });
            _memoryCacheService.Addresses.Add(secondAddress);

            // Act
            var actual = await _sut.GetAddressesByCity("B2");

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void GetAddressesByCity_ShouldReturnTwoAddresses()
        {
            // Arrange
            CleanMemoryCache();
            var secondAddress = new Address() { Id = 2, Name = "A", City = "B", Street = "C2" };
            var thirdAddress = new Address() { Id = 3, Name = "A", City = "B", Street = "C2" };
            var expected = new List<Address>() { secondAddress, thirdAddress };

            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B1", Street = "C2" });
            _memoryCacheService.Addresses.Add(secondAddress);
            _memoryCacheService.Addresses.Add(thirdAddress);

            // Act
            var actual = await _sut.GetAddressesByCity("B");

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void GetAddressesByCity_ShouldReturnNoAddress()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new List<Address>();
            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            // Act
            var actual = await _sut.GetAddressesByCity("X");

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void GetAddressById_ShouldReturnOneAddress()
        {
            // Arrange
            CleanMemoryCache();
            var firstAddress = new Address() { Id = 1, Name = "A", City = "B", Street = "C" };
            var secondAddress = new Address() { Id = 2, Name = "A", City = "B", Street = "C" };
            var expected = secondAddress;

            _memoryCacheService.Addresses.Add(firstAddress);
            _memoryCacheService.Addresses.Add(secondAddress);

            // Act
            var actual = await _sut.GetAddressById(2);

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void GetAddressById_ShouldReturnNoAddress()
        {
            // Arrange
            CleanMemoryCache();
            var firstAddress = new Address() { Id = 1, Name = "A", City = "B", Street = "C" };
            var secondAddress = new Address() { Id = 2, Name = "A", City = "B", Street = "C" };
            var expected = new Address();

            _memoryCacheService.Addresses.Add(firstAddress);
            _memoryCacheService.Addresses.Add(secondAddress);

            // Act
            var actual = await _sut.GetAddressById(3);

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void AddNewAddress_ShouldAddNewAddress()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new Address() { Id = 3, Name = "A", City = "B", Street = "C3" };
            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            // Act
            await _sut.AddNewAddress(expected);
            var actual = _memoryCacheService.Addresses.Last();

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void AddNewAddress_ShouldAddNoAddress_WhenAddressWithGivenIdAlreadyExist()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new List<Address>();
            expected.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            expected.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            // Act
            Func<Task> act = async () => await _sut.AddNewAddress(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });
            var actual = _memoryCacheService.Addresses;

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("Address with given Id already exists!", exception.Message);

            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void UpdateAddressById_ShouldUpdateOneAddress()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new List<Address>();
            expected.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            expected.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "UPDATED" });

            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            // Act
            await _sut.UpdateAddressById(2, new Address() { Id = 2, Name = "A", City = "B", Street = "UPDATED" });
            var actual = _memoryCacheService.Addresses;

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void UpdateAddressById_ShouldUpdateNoAddress_WhenThereIsNoMatchingOne()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new List<Address>();
            expected.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            expected.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            // Act
            Func<Task> act = async () => await _sut.UpdateAddressById(3, new Address() { Id = 5, Name = "A", City = "UPDATED", Street = "C1" });
            var actual = _memoryCacheService.Addresses;

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("Address with given Id doesn't exist!", exception.Message);

            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void UpdateAddressById_ShouldNotUpdateId_WhenAddressWithGivenIdAlreadyExist()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new List<Address>();
            expected.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            expected.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            // Act
            Func<Task> act = async () => await _sut.UpdateAddressById(2, new Address { Id = 1 });
            var actual = _memoryCacheService.Addresses;

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("Address with given Id already exist!", exception.Message);

            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void DeleteAddressById_ShouldDeleteOneAddress()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new List<Address>();
            expected.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            // Act
            await _sut.DeleteAddressById(1);
            var actual = _memoryCacheService.Addresses;

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public async void DeleteAddressById_ShouldDeleteNoAddress_WhenIdIsNotMatching()
        {
            // Arrange
            CleanMemoryCache();
            var expected = new List<Address>();
            expected.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            expected.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            _memoryCacheService.Addresses.Add(new Address() { Id = 1, Name = "A", City = "B", Street = "C1" });
            _memoryCacheService.Addresses.Add(new Address() { Id = 2, Name = "A", City = "B", Street = "C2" });

            // Act
            await _sut.DeleteAddressById(4);
            var actual = _memoryCacheService.Addresses;

            // Assert
            var expectedStr = JsonSerializer.Serialize(expected);
            var actualStr = JsonSerializer.Serialize(actual);
            Assert.Equal(expectedStr, actualStr);
        }
    }
}
