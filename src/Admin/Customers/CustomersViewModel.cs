using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Admin.Authorization;
using Admin.Navigation;

namespace Admin.Customers
{
    public class CustomersViewModel : BaseViewModel, IEnumerable<CustomerViewModel>
    {
        private readonly IEnumerable<CustomerViewModel> customers;

        public CustomersViewModel(IEnumerable<Customer> customers, Menu menu, User user, UrlFactory urlFactory)
            : base(menu, user, urlFactory)
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            this.customers = customers.Select(customer => new CustomerViewModel(customer, menu, user, urlFactory));
        }

        public IEnumerator<CustomerViewModel> GetEnumerator()
        {
            return this.customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.customers.GetEnumerator();
        }
    }
}
