using System;
using Admin.Authorization;
using Admin.Navigation;

namespace Admin.Customers
{
    public class CustomerViewModel : BaseViewModel
    {
        public CustomerViewModel(Customer customer, Menu menu, User user, UrlFactory urlFactory)
            : base(menu, user, urlFactory)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            Id = customer.Id;
            Name = customer.Name;
            BirthDate = customer.BirthDate.ToString();
            Email = customer.Email.ToString();
        }

        public string Id { get; }

        public string Name { get; }

        public string BirthDate { get; }

        public string Email { get; }
    }
}
