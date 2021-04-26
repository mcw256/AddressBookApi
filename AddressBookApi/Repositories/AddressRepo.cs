using AddressBookApi.Models;
using AddressBookApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly IMemoryCacheService _memoryCache;

        public AddressRepo(IMemoryCacheService memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<Address> GetLastAddress()
        {
            if (_memoryCache.Addresses.Count == 0)
                return new Address(); //return empty address

            return await Task.Run(() => _memoryCache.Addresses.Last()); // I'm aware this Task here is unneccessary. Did it just to force 'await' and to force whole API to be async
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            return await Task.Run(() => _memoryCache.Addresses);
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            if (!_memoryCache.Addresses.Exists(a => a.Id == id))
                return new Address();

            return await Task.Run(() => _memoryCache.Addresses.Where(a => a.Id == id).First()); // I'm aware this Task here is unneccessary. Did it just to force 'await' and to force whole API to be async
        }

        public async Task<List<Address>> GetAddressesByCity(string city)
        {
            if (!_memoryCache.Addresses.Exists(a => a.City == city))
                return new List<Address>();

            return await Task.Run(() => _memoryCache.Addresses.Where(a => a.City == city).ToList()); // I'm aware this Task here is unneccessary. Did it just to force 'await' and to force whole API to be async
        }

        public async Task<Address> AddNewAddress(Address address)
        {
            address.Id = new Guid();

            await Task.Run(() => _memoryCache.Addresses.Add(address)); // I'm aware this Task here is unneccessary. Did it just to force 'await' and to force whole API to be async
            return address;
        }

        public async Task<Address> UpdateAddressById(Guid id, Address address)
        {
            var addressFound = await Task.Run(() => _memoryCache.Addresses.FirstOrDefault(a => a.Id == id));
            if (addressFound == null)
                throw new Exception("Address with given Id doesn't exist!");

            addressFound.Name = address.Name;
            addressFound.City = address.City;
            addressFound.Street = address.Street;

            return addressFound;
        }

        public async Task DeleteAddressById(Guid id)
        {
            var itemToRemove = await Task.Run(() => _memoryCache.Addresses.SingleOrDefault(a => a.Id == id)); // I'm aware this Task here is unneccessary. Did it just to force 'await' and to force whole API to be async
            if (itemToRemove == null)
                return;

            await Task.Run(() => _memoryCache.Addresses.Remove(itemToRemove));
            return;
        }

    }
}
