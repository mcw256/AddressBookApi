﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Requests
{
    public class UpdateAddressRequest
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

    }
}
