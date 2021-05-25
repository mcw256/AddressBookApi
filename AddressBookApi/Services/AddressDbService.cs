using AddressBookApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Services
{
    public class AddressDbService : IAddressDbService
    {
        private IMongoCollection<Address> _addresses;

        public IMongoCollection<Address> AddressCollection { get => _addresses; }

        public AddressDbService(IAddressDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _addresses = database.GetCollection<Address>(settings.AddressCollectionName);
        }
    }
}
