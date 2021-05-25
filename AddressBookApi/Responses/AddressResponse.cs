using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Responses
{
    public class AddressResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }
}
