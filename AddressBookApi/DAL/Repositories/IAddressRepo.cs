using AddressBookApi.DAL.Models;
using AddressBookApi.DAL.Repositories.Base;
using AddressBookApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBookApi.DAL.Repositories
{
    public interface IAddressRepo : IBaseRepo<Address>
    {

    }
}
