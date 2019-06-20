using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Admin.Authorization;
using Admin.LinkedData;
using Admin.Navigation;
using Admin.Transactions;

namespace Admin.Customers
{
    public class CustomerViewModel : BaseViewModel, IEnumerable<KeyValuePair<Uri, object>>
    {
        public CustomerViewModel(Customer customer, Menu menu, User user, UrlFactory urlFactory, IEnumerable<Transaction> transactions)
            : this(customer, menu, user, urlFactory)
        {
            if (transactions == null)
            {
                throw new ArgumentNullException(nameof(transactions));
            }

            Transactions = transactions.Select(MapTransaction);
        }

        public CustomerViewModel(LinkedDataObject customer, Menu menu, User user, UrlFactory urlFactory)
            : base(menu, user, urlFactory)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            Id = ((Uri)customer["http://www.w3.org/ns/json-ld#id"]).PathAndQuery.TrimStart('/');
            this.customer = customer;
        }

        public string Id { get; }
        public IEnumerable<TransactionViewModel> Transactions { get; }
        public object this[string key] { get => customer[key]; set => customer[key] = value; }

        private TransactionViewModel MapTransaction(Transaction transaction)
        {
            return new TransactionViewModel(transaction);
        }

        public IEnumerator<KeyValuePair<Uri, object>> GetEnumerator()
        {
            return ((IDictionary<Uri, object>)customer).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<Uri, object>)customer).GetEnumerator();
        }
    }
}
