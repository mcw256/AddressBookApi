using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AddressBookApi.Models
{
    public class Address
    {
        
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

    }
}
