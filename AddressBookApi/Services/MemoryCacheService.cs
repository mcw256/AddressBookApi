using AddressBookApi.Models;
using System.Collections.Generic;

namespace AddressBookApi.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
