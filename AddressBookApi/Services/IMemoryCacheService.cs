using AddressBookApi.Models;
using System.Collections.Generic;

namespace AddressBookApi.Services
{
    public interface IMemoryCacheService
    {
        public List<Address> Addresses { get; set; }
    }
}
