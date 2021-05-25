using AddressBookApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Services
{
    public interface IAddressDbContext
    {
        public IMongoCollection<Address> AddressCollection { get; }
    }
}
