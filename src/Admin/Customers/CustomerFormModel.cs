using System.ComponentModel.DataAnnotations;

namespace Admin.Customers
{
    public class CustomerFormModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can be maximum 100 characters of length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "BirthDate is required")]
        [StringLength(100, ErrorMessage = "BirthDate can be maximum 100 characters of length")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Birth date must be in the yyyy-dd-mm format")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email can be maximum 100 characters of length")]
        [RegularExpression(@"^\w+@\w+\.\w+$", ErrorMessage = "Email must be in the format user@domain.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(100, ErrorMessage = "Phone can be maximum 100 characters of length")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in the format 123-456-7890")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [StringLength(100, ErrorMessage = "UserName can be maximum 100 characters of length")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Website is required")]
        [StringLength(100, ErrorMessage = "Website can be maximum 100 characters of length")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public CustomerAddressFormModel Address { get; set; }

        public Customer Map(string id)
        {
            return new Customer(id, Name, BirthDate)
            {
                Email = Email,
                Phone = Phone,
                UserName = UserName,
                Website = Website,
                Address = Address.Map()
            };
        }
    }
}
