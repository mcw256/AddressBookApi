using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.DAL.Dtos
{
    public class PageOfAddressesDto
    {
            public List<AddressDto> Items { get; set; }
            public long NoOfPages { get; set; }
            public long CurrentPage { get; set; }
    }
}
