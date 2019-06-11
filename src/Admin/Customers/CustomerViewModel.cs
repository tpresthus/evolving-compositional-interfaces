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
        public CustomerViewModel(Customer customer, Menu menu, User user, UrlFactory urlFactory)
            : this(customer, menu, user, urlFactory, Enumerable.Empty<Transaction>())
        {
        }

        public CustomerViewModel(Customer customer, Menu menu, User user, UrlFactory urlFactory, IEnumerable<Transaction> transactions)
            : base(menu, user, urlFactory)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (transactions == null)
            {
                throw new ArgumentNullException(nameof(transactions));
            }

            Id = customer.Id;
            Name = customer.Name;
            BirthDate = customer.BirthDate.ToString();
            Email = customer.Email.ToString();
            Transactions = transactions.Select(MapTransaction);
        }

        public string Id { get; }

        public string Name { get; }

        public string BirthDate { get; }

        public string Email { get; }

        public IEnumerable<TransactionViewModel> Transactions { get; }

        private TransactionViewModel MapTransaction(Transaction transaction)
        {
            return new TransactionViewModel(transaction);            
        }
    }
}
