using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Requests
{
    public class GetAddressesPageRequest
    {
        public int PageNo { get; set; } = 0;
        public string City { get; set; }
        public string Street { get; set; }

    }
}
