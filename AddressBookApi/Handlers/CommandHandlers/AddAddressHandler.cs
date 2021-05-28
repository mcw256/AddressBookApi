using MediatR;
using AddressBookApi.Commands;
using AddressBookApi.Responses;
using AddressBookApi.DAL.Repositories;
using System.Threading.Tasks;
using System.Threading;
using AddressBookApi.DAL.Dtos;
using AddressBookApi.DAL.Models;

namespace AddressBookApi.Handlers.CommandHandlers
{
    public class AddAddressHandler : IRequestHandler<AddAddressCommand, AddressResponse>
    {
        private readonly IAddressRepo _addressRepo;

        public AddAddressHandler(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public async Task<AddressResponse> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            // request to dto - maping
            var addressDto = new AddressDto() { Name = request.Name, City = request.City, Street = request.Street };
            // dto to model - maping
            var addressModel = new Address() { Name = addressDto.Name, City = addressDto.City, Street = addressDto.Street };
            // ACT
            var resultAddressModel = await _addressRepo.AddOne(addressModel);
            // model to response - maping... should've firstly mapped to dto but..
            var resultAdddresResponse = new AddressResponse() { Id = resultAddressModel.Id, Name = resultAddressModel.Name, City = resultAddressModel.City, Street = responseAddressDto.Street };
            
            return resultAdddresResponse;
        }
    }
}
