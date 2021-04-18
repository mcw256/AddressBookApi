using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Models
{
    public class ErrorResponse
    {
        public string ShortInfo { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
