using AddressBookApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Queries
{
    public class GetAddressByIdQuery : IRequest<AddressResponse>
    {
        public string Id { get; }

        public GetAddressByIdQuery(string id)
        {
            Id = id;
        }

    }
}
