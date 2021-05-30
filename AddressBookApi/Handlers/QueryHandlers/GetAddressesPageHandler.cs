using AddressBookApi.DAL.Models;
using AddressBookApi.DAL.Repositories;
using AddressBookApi.Models;
using AddressBookApi.Queries;
using AddressBookApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBookApi.Handlers
{
    public class GetAddressesPageHandler : IRequestHandler<GetAddressesPageQuery, PageOfAddressesResponse>
    {
        private readonly IAddressRepo _addressRepo;
        private readonly IApiSpecificSettings _apiSpecificSettings;

        public GetAddressesPageHandler(IAddressRepo addressRepo, IApiSpecificSettings apiSpecificSettings)
        {
            _addressRepo = addressRepo;
            _apiSpecificSettings = apiSpecificSettings;
        }

        public async Task<PageOfAddressesResponse> Handle(GetAddressesPageQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Address> addressesList;
            long addressesAmount;

            Expression<Func<Address, bool>> filter;
            if (request.City != null && request.Street != null)
                filter = x => x.City == request.City && x.Street == request.Street;
            else if (request.City != null)
                filter = x => x.City == request.City;
            else if (request.Street != null)
                filter = x => x.Street == request.Street;
            else
                filter = x => true;

            addressesList = await _addressRepo.FindWithPaging(filter, request.PageNo <= 0 ? 1 : request.PageNo, _apiSpecificSettings.PagingPageSize);
            addressesAmount = await _addressRepo.Count(filter);

            var totalPages = (int)Math.Ceiling(addressesAmount / (double)_apiSpecificSettings.PagingPageSize);

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
