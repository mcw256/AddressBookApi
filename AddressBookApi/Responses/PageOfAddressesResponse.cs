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
        public long NoOfPages { get; set; }
        public long CurrentPage { get; set; }
    }
}
