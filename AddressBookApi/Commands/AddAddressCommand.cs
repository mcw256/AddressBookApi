using AddressBookApi.Responses;
using MediatR;

namespace AddressBookApi.Commands
{
    public class AddAddressCommand : IRequest<AddressResponse>
    {
        public string Name { get; }
        public string City { get;}
        public string Street { get; }

        public AddAddressCommand(string name, string city, string street)
        {
            Name = name;
            City = city;
            Street = street;
        }
    }
}