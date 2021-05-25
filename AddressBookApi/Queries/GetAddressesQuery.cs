using AddressBookApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Queries
{
    public class GetAddressesQuery : IRequest<PageOfAddressesResponse>
    {
        public int Page { get; }
        public string City { get; }
        public string Street { get; }


        public GetAddressesQuery(int page, string city, string street)
        {
            Page = page;
            City = city;
            Street = street;
        }
    }
}
