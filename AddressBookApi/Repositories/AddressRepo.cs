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

            return await Task.Run(() => _memoryCache.Addresses.Last());
        }

        public async Task<List<Address>> GetAddressesByStreet(string street)
        {
            return await Task.Run( () => _memoryCache.Addresses.Where(a => a.Street == street).ToList() ); 
        }

        public async Task<Address> AddNewAddress(Address address)
        {
            if (address.City == null || address.Name == null || address.Street == null)
                throw new Exception("Missing data fields");

            if (address.Id == null) address.Id = _memoryCache.Addresses.Last().Id + 1;

            if (_memoryCache.Addresses.Exists(a => a.Id == address.Id))
                throw new Exception("Address with given Id already exists!");

            await Task.Run( () => _memoryCache.Addresses.Add(address) );
            return address;
        }

        public async Task<Address> UpdateAddressById(int id, Address address)
        {
            var addressFound = await Task.Run( () => _memoryCache.Addresses.FirstOrDefault(a => a.Id == id) );
            if (addressFound == null)
                throw new Exception("Address doesn't exist!");
            else
            {
                // do not update to null
                addressFound.Name = (address.Name == null) ? addressFound.Name : address.Name;
                addressFound.City = (address.City == null) ? addressFound.City : address.City;
                addressFound.Street = (address.Street == null) ? addressFound.Street : address.Street;
            }
            // id updates are possible
            if (address.Id != null)
            {
                if (await Task.Run(() => _memoryCache.Addresses.Exists(a => a.Id == address.Id)) )
                    throw new Exception("Address with given Id already exists!");
                else
                    addressFound.Id = address.Id;
            }
            return addressFound;
        }

        public async Task<Address> DeleteAddressById(int id)
        {
            var itemToRemove = await Task.Run( () => _memoryCache.Addresses.SingleOrDefault(a => a.Id == id));
            if (itemToRemove == null)
                throw new Exception("Address with given Id doesn't exist!");

            await Task.Run( () => _memoryCache.Addresses.Remove(itemToRemove));
            return itemToRemove;
        }

    }
}
