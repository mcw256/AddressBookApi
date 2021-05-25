using AddressBookApi.Commands;
using AddressBookApi.DAL.Dtos;
using AddressBookApi.DAL.Repositories;
using AddressBookApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBookApi.Handlers.CommandHandlers
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand>
    {
        private readonly IAddressRepo _addressRepo;

        public UpdateAddressHandler(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public async Task<Unit> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            //mapping should be done here
            var addressDto = new AddressDto() { Name = request.Name, City = request.City, Street = request.Street };

            await _addressRepo.UpdateAddressById(request.Id, addressDto);
            
            return Unit.Value;
        }
    }
}
