using AddressBookApi.Commands;
using AddressBookApi.DAL.Dtos;
using AddressBookApi.DAL.Models;
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
            //request to dto - maping
            var addressDto = new AddressDto() { Name = request.Name, City = request.City, Street = request.Street };

            //dto to model - maping
            var addressModel = new Address() { Name = addressDto.Name, City = addressDto.City, Street = addressDto.Street };

            await _addressRepo.UpdateOne(x => x.Id == request.Id, addressModel);
            
            return Unit.Value;
        }
    }
}
