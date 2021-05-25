using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Models
{
    public class PageOfAddresses
    {
        public List<Address> Items { get; set; }
        public long NoOfPages { get; set; }
        public long CurrentPage { get; set; }
    }
}
