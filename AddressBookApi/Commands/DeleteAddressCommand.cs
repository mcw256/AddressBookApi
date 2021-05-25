using AddressBookApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Commands
{
    public class DeleteAddressCommand : IRequest
    {
        public string Id { get; }

        public DeleteAddressCommand(string id)
        {
            Id = Id;
        }
    }
}
