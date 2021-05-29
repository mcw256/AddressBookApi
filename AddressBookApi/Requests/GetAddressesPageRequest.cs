using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Requests
{
    public class GetAddressesPageRequest
    {
        public int PageNo { get; }
        public string City { get; }
        public string Street { get; }

        public GetAddressesPageRequest(int pageNo, string city, string street)
        {
            PageNo = pageNo;
            City = city;
            Street = street;
        }   
    }
}
