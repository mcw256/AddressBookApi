﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Requests
{
    public class AddAddressRequest
    {
        public string Name { get; }
        public string City { get; }
        public string Street { get; }

        public AddAddressRequest(string name, string city, string street)
        {
            Name = name;
            City = city;
            Street = street;
        }
    }
}
