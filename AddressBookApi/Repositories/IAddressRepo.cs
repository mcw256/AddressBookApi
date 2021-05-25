using AddressBookApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBookApi.Repositories
{
    public interface IAddressRepo
    {
        public Task<PageOfAddresses> GetAddresses(int page, string city, string street);

        public Task<Address> GetAddressById(string id);

        public Task AddNewAddress(Address address);

        public Task UpdateAddressById(string id, Address address);

        public Task DeleteAddressById(string id);
    }
}
