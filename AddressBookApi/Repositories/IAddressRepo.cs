using AddressBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Repositories
{
    public interface IAddressRepo
    {
        public Address GetLastAddress();

        public List<Address> GetAddressesByStreet(string street);

        public Address AddNewAddress(Address address);

        public Address UpdateAddressById(int id, Address address);

        public Address DeleteAddressById(int id);
    }
}
