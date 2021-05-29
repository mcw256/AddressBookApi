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
    public class GetAddressesPageHandler : IRequestHandler<GetAddressesQuery, PageOfAddressesResponse>
    {
        private readonly IAddressRepo _addressRepo;
        private readonly IApiSpecificSettings _apiSpecificSettings;

        public GetAddressesPageHandler(IAddressRepo addressRepo, IApiSpecificSettings apiSpecificSettings)
        {
            _addressRepo = addressRepo;
            _apiSpecificSettings = apiSpecificSettings;
        }

        public async Task<PageOfAddressesResponse> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Address> addressesList;
            if (request.City != null && request.Street != null)
                addressesList = await _addressRepo.FindWithPaging(x => x.City == request.City && x.Street == request.Street, request.PageNo, _apiSpecificSettings.PagingPageSize);

            else if (request.City != null)
                addressesList = await _addressRepo.FindWithPaging(x => x.City == request.City, request.PageNo, _apiSpecificSettings.PagingPageSize);

            else if (request.Street != null)
                addressesList = await _addressRepo.FindWithPaging(x => x.Street == request.Street, request.PageNo, _apiSpecificSettings.PagingPageSize);

            else
                addressesList = await _addressRepo.FindWithPaging(x => true, request.PageNo, _apiSpecificSettings.PagingPageSize);


            var totalPages = (int)Math.Ceiling((double)addressesList.Count() / _apiSpecificSettings.PagingPageSize);

            var pageOfAddressesResponse = new PageOfAddressesResponse()
            {
                CurrentPage = request.PageNo,
                TotalPages = totalPages,
                Items = new List<AddressResponse>()
            };

            //maping
            foreach (var item in addressesList)
                pageOfAddressesResponse.Items.Add(new AddressResponse { Id = item.Id, Name = item.Name, City = item.City, Street = item.Street });

            return pageOfAddressesResponse;
        }
    }
}
