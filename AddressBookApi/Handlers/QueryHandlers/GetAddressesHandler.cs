using AddressBookApi.DAL.Models;
using AddressBookApi.DAL.Repositories;
using AddressBookApi.Models;
using AddressBookApi.Queries;
using AddressBookApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBookApi.Handlers
{
    public class GetAddressesHandler : IRequestHandler<GetAddressesQuery, PageOfAddressesResponse>
    {
        private readonly IAddressRepo _addressRepo;
        private readonly IApiSpecificSettings _apiSpecificSettings;

        public GetAddressesHandler( IAddressRepo  addressRepo, IApiSpecificSettings apiSpecificSettings)
        {
            _addressRepo = addressRepo;
            _apiSpecificSettings = apiSpecificSettings;
        }

        public async Task<PageOfAddressesResponse> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Address> addressesList;
            if (request.City != null && request.Street != null)
                addressesList = await _addressRepo.FindWithPaging(x => x.City == request.City && x.Street == request.Street, request.Page, _apiSpecificSettings.PaginationPageSize);

            else if (request.City != null)
                addressesList = await _addressRepo.FindWithPaging(x => x.City == request.City, request.Page, _apiSpecificSettings.PaginationPageSize);

            else if (request.Street != null)
                addressesList = await _addressRepo.FindWithPaging(x => x.Street == request.Street, request.Page, _apiSpecificSettings.PaginationPageSize);

            else(request.Street != null)
                addressesList = await _addressRepo.FindWithPaging(x => true, request.Page, _apiSpecificSettings.PaginationPageSize);


            var addressesList = await _addressRepo.FindWithPaging()
            
            var pageOfAddressesDto = await _addressRepo.GetAddresses(request.Page, request.City, request.Street);

            //maping
            var addressesDto = pageOfAddressesDto.Items;
            var addressesResponse = new List<AddressResponse>();
            foreach (var item in addressesDto)
                addressesResponse.Add(new AddressResponse() { Id = item.Id, Name = item.Name, City = item.City, Street = item.Street });
            var pageOfAddressResponse = new PageOfAddressesResponse() { Items = addressesResponse, CurrentPage = pageOfAddressesDto.CurrentPage, NoOfPages = pageOfAddressesDto.NoOfPages };

            return pageOfAddressResponse;
        }
    }
}
