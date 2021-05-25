using AddressBookApi.DAL.Repositories;
using AddressBookApi.Queries;
using AddressBookApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBookApi.Handlers
{
    public class GetAddressByIdHandler : IRequestHandler<GetAddressByIdQuery, AddressResponse>
    {
        private readonly IAddressRepo _addressRepo;

        public GetAddressByIdHandler(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public async Task<AddressResponse> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var addressDto = await _addressRepo.GetAddressById(request.Id);

            var addressResponse = new AddressResponse() { Id = addressDto.Id, Name = addressDto.Name, City = addressDto.City, Street = addressDto.Street };

            return addressResponse;
        }
    }
}
