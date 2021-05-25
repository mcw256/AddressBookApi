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

        public GetAddressesHandler( IAddressRepo  addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public async Task<PageOfAddressesResponse> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
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
