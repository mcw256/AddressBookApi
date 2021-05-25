using AddressBookApi.Models;
using AddressBookApi.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly IAddressDbService _addressDbService;
        private readonly IApiSpecificSettings _apiSpecificSettings;

        public AddressRepo(IAddressDbService addressDbService, IApiSpecificSettings apiSpecificSettings)
        {
            _addressDbService = addressDbService;
            _apiSpecificSettings = apiSpecificSettings;
        }

        public async Task<PageOfAddresses> GetAddresses(int page, string city, string street)
        {
            int pageSize = _apiSpecificSettings.PaginationPageSize;

            page = (page <= 0) ? 1 : page;

            var cityFilter = (city == null) ? Builders<Address>.Filter.Empty : Builders<Address>.Filter.Eq(x => x.City, city);
            var streetFilter = (street == null) ? Builders<Address>.Filter.Empty : Builders<Address>.Filter.Eq(x => x.Street, street);
            var combinedFilters = Builders<Address>.Filter.And(cityFilter, streetFilter);


            // Paging could be optimized using Internal Mongo Aggregation
            var data = await _addressDbService.AddressCollection.Find(combinedFilters)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var count = await _addressDbService.AddressCollection.CountDocumentsAsync(combinedFilters);

            var noOfPages = (int)Math.Ceiling((double)count/pageSize);

            return new PageOfAddresses() { Items = data, CurrentPage = page, NoOfPages = noOfPages };
        }

        public async Task<Address> GetAddressById(string id)
        {
            return (await _addressDbService.AddressCollection.Find(x => x.Id == id).ToListAsync()).FirstOrDefault();
        }


        public async Task AddNewAddress(Address address)
        {
            await _addressDbService.AddressCollection.InsertOneAsync(address);
        }

        public async Task UpdateAddressById(string id, Address address)
        {
            await _addressDbService.AddressCollection.ReplaceOneAsync(x => x.Id == id, address);
        }

        public async Task DeleteAddressById(string id)
        {
            await _addressDbService.AddressCollection.DeleteOneAsync(x => x.Id == id);
        }

    }
}
