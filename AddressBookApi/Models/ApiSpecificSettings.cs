using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Models
{
    public class ApiSpecificSettings : IApiSpecificSettings
    {
        public int PaginationPageSize { get; set; }

    }

    public interface IApiSpecificSettings
    {
        public int PaginationPageSize { get; set; }
    }
}
