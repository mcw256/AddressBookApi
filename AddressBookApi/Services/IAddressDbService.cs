using AddressBookApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Services
{
    public interface IAddressDbService
    {
        public IMongoCollection<Address> AddressCollection { get; }
    }
}
