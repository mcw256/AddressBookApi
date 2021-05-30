using AddressBookApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Queries
{
    public class GetAddressesPageQuery : IRequest<PageOfAddressesResponse>
    {
        public int PageNo { get; }
        public string City { get; }
        public string Street { get; }

        public GetAddressesPageQuery(int page, string city, string street)
        {
            PageNo = page;
            City = city;
            Street = street;
        }
    }
}
