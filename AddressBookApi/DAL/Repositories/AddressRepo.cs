using AddressBookApi.DAL.Models;
using AddressBookApi.DAL.Repositories.Base;
using AddressBookApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.DAL.Repositories
{
  

    public class AddressRepo : BaseRepo<Address>, IAddressRepo
    {
        public AddressRepo(IMongoClient mongoClient, IMongoDbSettings mongoDbSettings) : base(mongoClient, mongoDbSettings)
        {

        }


    }

}
