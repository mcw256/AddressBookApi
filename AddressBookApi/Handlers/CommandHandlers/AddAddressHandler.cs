using MediatR;
using AddressBookApi.Commands;
using AddressBookApi.Responses;
using AddressBookApi.DAL.Repositories;
using System.Threading.Tasks;
using System.Threading;
using AddressBookApi.DAL.Dtos;

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
            var addressDto = new AddressDto() { Name = request.Name, City = request.City, Street = request.Street };

            var responseAddressDto = await _addressRepo.AddNewAddress(addressDto);

            // maping here
            var addressResponse = new AddressResponse() { Id = responseAddressDto.Id, Name = responseAddressDto.Name, City = responseAddressDto.City, Street = responseAddressDto.Street };
            return addressResponse;
        }
    }
}
