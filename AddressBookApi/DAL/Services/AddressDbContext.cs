using AddressBookApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Services
{
    public class AddressDbContext : IAddressDbContext
    {
        private readonly IMongoDbSettings _mongoDbSettings; 
        private readonly IMongoCollection<Address> _addressCollection;

        public IMongoCollection<Address> AddressCollection { get => _addressCollection; }

        public AddressDbContext(IMongoDbSettings mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
            
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);
            _addressCollection = database.GetCollection<Address>(mongoDbSettings.CollectionName);
        }
    }
}
