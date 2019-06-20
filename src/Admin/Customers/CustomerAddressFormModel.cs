using System.ComponentModel.DataAnnotations;

namespace Admin.Customers
{
    public class CustomerAddressFormModel
    {
        [Required(ErrorMessage = "Street is required")]
        [StringLength(100, ErrorMessage = "Street can be maximum 100 characters of length")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City can be maximum 100 characters of length")]
        public string City { get; set; }

        [Required(ErrorMessage = "ZipCode is required")]
        [StringLength(100, ErrorMessage = "ZipCode can be maximum 100 characters of length")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(100, ErrorMessage = "State can be maximum 100 characters of length")]
        public string State { get; set; }

        public Address Map()
        {
            return new Address(Street, City, ZipCode, State);
        }
    }
}
