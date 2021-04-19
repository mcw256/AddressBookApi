using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Models
{
    public class Address
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

    }
}
