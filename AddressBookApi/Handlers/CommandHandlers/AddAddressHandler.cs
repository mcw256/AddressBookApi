using MediatR;
using AddressBookApi.Commands;
using AddressBookApi.Responses;
using AddressBookApi.DAL.Repositories;
using System.Threading.Tasks;
using System.Threading;
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
            //maping
            var addressModel = new Address() { Name = request.Name, City = request.City, Street = request.Street };

            var resultAddressModel = await _addressRepo.AddOne(addressModel);
           
            //maping
            var resultAdddresResponse = new AddressResponse() { Id = resultAddressModel.Id, Name = resultAddressModel.Name, City = resultAddressModel.City, Street = resultAddressModel.Street };
            
            return resultAdddresResponse;
        }
    }
}
