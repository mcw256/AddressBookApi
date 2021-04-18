using AddressBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Repositories
{
    public interface IAddressRepo
    {
        public Task<Address> GetLastAddress();

        public Task<Address> GetAddressById(int id);

        public Task<List<Address>> GetAddressesByCity(string street);

        public Task<Address> AddNewAddress(Address address);

        public Task<Address> UpdateAddressById(int id, Address address);

        public Task DeleteAddressById(int id);

    }
}
