using AddressBookApi.Models;
using AddressBookApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Commands
{
    public class UpdateAddressCommand : IRequest
    {
        public string Id { get; set; }
        public string Name { get; }
        public string City { get; }
        public string Street { get; }

        public UpdateAddressCommand(string id, string name, string city, string street)
        {
            Id = id;
            Name = name;
            City = city;
            Street = street;
        }

    }
}
