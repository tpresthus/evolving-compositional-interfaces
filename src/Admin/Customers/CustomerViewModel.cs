using System;
using System.Collections.Generic;
using System.Linq;
using Admin.Authorization;
using Admin.Navigation;
using Admin.Transactions;

namespace Admin.Customers
{
    public class CustomerViewModel : BaseViewModel
    {
        public CustomerViewModel(CustomerResponse customerResponse, Menu menu, User user, UrlFactory urlFactory, IEnumerable<Transaction> transactions)
            : this(customerResponse?.Customer, menu, user, urlFactory)
        {
            if (transactions == null)
            {
                throw new ArgumentNullException(nameof(transactions));
            }

            Json = customerResponse.Json;
            Transactions = transactions.Select(MapTransaction);
        }

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
            Ssn = customer.Ssn;
            Phone = customer.Phone;
            Email = customer.Email.ToString();
            UserName = customer.UserName;
            Website = customer.Website;
            Transactions = Enumerable.Empty<TransactionViewModel>();

            if (customer.Address != null)
            {
                Address = new AddressViewModel(customer.Address);
            }
        }

        public string Id { get; }
        public string Name { get; }
        public string BirthDate { get; }
        public string Email { get; }
        public string Ssn { get; }
        public string Phone { get; }
        public AddressViewModel Address { get; set; }
        public string UserName { get; set; }
        public string Website { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; }
        public string Json { get; }

        private TransactionViewModel MapTransaction(Transaction transaction)
        {
            return new TransactionViewModel(transaction);
        }
    }
}
