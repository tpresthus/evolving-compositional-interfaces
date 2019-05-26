using System;

namespace Admin.Customers
{
    public class CustomerViewModel
    {
        public CustomerViewModel(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            Name = customer.Name;
            BirthDate = customer.BirthDate.ToString();
            Email = customer.Email.ToString();
        }

        public string Name { get; }

        public string BirthDate { get; }

        public string Email { get; }
    }
}
