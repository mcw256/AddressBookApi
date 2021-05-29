using AddressBookApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Models
{
    public class PageOfAddressesResponse
    {
        public List<AddressResponse> Items { get; set; }
        public long TotalPages { get; set; }
        public long CurrentPage { get; set; }

    }
}
