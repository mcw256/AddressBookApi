using AddressBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
