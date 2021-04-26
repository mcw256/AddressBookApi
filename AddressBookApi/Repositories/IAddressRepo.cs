using AddressBookApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBookApi.Repositories
{
    public interface IAddressRepo
    {
        public Task<Address> GetLastAddress();

        public Task<IEnumerable<Address>> GetAllAddresses();

        public Task<Address> GetAddressById(Guid id);

        public Task<IEnumerable<Address>> GetAddressesByCity(string street);

        public Task<Address> AddNewAddress(Address address);

        public Task<Address> UpdateAddressById(Guid id, Address address);

        public Task DeleteAddressById(Guid id);

    }
}
