using AddressBookApi.DAL.Dtos;
using AddressBookApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBookApi.DAL.Repositories
{
    public interface IAddressRepo
    {
        public Task<PageOfAddressesDto> GetAddresses(int page, string city, string street);

        public Task<AddressDto> GetAddressById(string id);

        public Task<AddressDto> AddNewAddress(AddressDto addressDto);

        public Task UpdateAddressById(string id, AddressDto addressDto);

        public Task DeleteAddressById(string id);
    }
}
