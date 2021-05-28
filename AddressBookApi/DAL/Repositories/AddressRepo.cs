using AddressBookApi.DAL.Dtos;
using AddressBookApi.Models;
using AddressBookApi.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.DAL.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly IMongoDbClient _addressDbContext;
        private readonly IApiSpecificSettings _apiSpecificSettings;

        public AddressRepo(IMongoDbClient addressDbService, IApiSpecificSettings apiSpecificSettings)
        {
            _addressDbContext = addressDbService;
            _apiSpecificSettings = apiSpecificSettings;
        }

        public async Task<PageOfAddressesDto> GetAddresses(int page, string city, string street)
        {
            int pageSize = _apiSpecificSettings.PaginationPageSize;

            page = (page <= 0) ? 1 : page;

            var cityFilter = (city == null) ? Builders<Address>.Filter.Empty : Builders<Address>.Filter.Eq(x => x.City, city);
            var streetFilter = (street == null) ? Builders<Address>.Filter.Empty : Builders<Address>.Filter.Eq(x => x.Street, street);
            var combinedFilters = Builders<Address>.Filter.And(cityFilter, streetFilter);


            // Paging could be optimized using Internal Mongo Aggregation
            var addressesList = await _addressDbContext.AddressCollection.Find(combinedFilters)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            // this maping of nested list looks bad
            var addressesDtoList = new List<AddressDto>();
            foreach (var item in addressesList)
                addressesDtoList.Add(new AddressDto() { Id = item.Id, Name = item.Name, City = item.City, Street = item.Street });

            var addressesAmount = await _addressDbContext.AddressCollection.CountDocumentsAsync(combinedFilters);
            var noOfPages = (int)Math.Ceiling((double)addressesAmount / pageSize);

            return new PageOfAddressesDto() { Items = addressesDtoList, CurrentPage = page, NoOfPages = noOfPages };
        }

        public async Task<AddressDto> GetAddressById(string id)
        {
            var address = (await _addressDbContext.AddressCollection.Find(x => x.Id == id).ToListAsync()).FirstOrDefault();

            var addressDto = address == null ? null : new AddressDto() { Id = address.Id, Name = address.Name, City = address.City, Street = address.Street };

            return addressDto;
        }

        public async Task<AddressDto> AddNewAddress(AddressDto addressDto)
        {
            //maping here
            var address = new Address() { Name = addressDto.Name, City = addressDto.City, Street = addressDto.Street };
            
            await _addressDbContext.AddressCollection.InsertOneAsync(address);

            return addressDto;
        }

        public async Task UpdateAddressById(string id, AddressDto addressDto)
        {
            //maping here
            var address = new Address() { Name = addressDto.Name, City = addressDto.City, Street = addressDto.Street };

            await _addressDbContext.AddressCollection.ReplaceOneAsync(x => x.Id == id, address);
        }

        public async Task DeleteAddressById(string id)
        {
            await _addressDbContext.AddressCollection.DeleteOneAsync(x => x.Id == id);
        }

    }
}
