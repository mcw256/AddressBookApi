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
            var addressModel = await _addressRepo.FindOne(x => x.Id == request.Id);

            // at this point I don't see sense in doing dto maping
            var addressResponse = new AddressResponse() { Id = addressModel.Id, Name = addressModel.Name, City = addressModel.City, Street = addressModel.Street };

            return addressResponse;
        }
    }
}
