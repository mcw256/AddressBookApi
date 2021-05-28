using AddressBookApi.Commands;
using AddressBookApi.DAL.Models;
using AddressBookApi.DAL.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBookApi.Handlers.CommandHandlers
{
    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand>
    {
        private readonly IAddressRepo _addressRepo;

        public DeleteAddressHandler(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            await _addressRepo.DeleteOne(x => x.Id == request.Id);

            return Unit.Value;
        }
    }
}
