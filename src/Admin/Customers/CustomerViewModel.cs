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
    public class CustomerViewModel : BaseViewModel, IDictionary<Uri, object>
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

        public ICollection<Uri> Keys => ((IDictionary<Uri, object>)customer).Keys;

        public ICollection<object> Values => ((IDictionary<Uri, object>)customer).Values;

        public int Count => customer.Count;

        public bool IsReadOnly => ((IDictionary<Uri, object>)customer).IsReadOnly;

        public object this[Uri key] { get => customer[key]; set => customer[key] = value; }
        public object this[string key] { get => customer[key]; set => customer[key] = value; }

        private TransactionViewModel MapTransaction(Transaction transaction)
        {
            return new TransactionViewModel(transaction);
        }

        public void Add(Uri key, object value)
        {
            customer.Add(key, value);
        }

        public bool ContainsKey(Uri key)
        {
            return customer.ContainsKey(key);
        }

        public bool Remove(Uri key)
        {
            return customer.Remove(key);
        }

        public bool TryGetValue(Uri key, out object value)
        {
            return customer.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<Uri, object> item)
        {
            ((IDictionary<Uri, object>)customer).Add(item);
        }

        public void Clear()
        {
            customer.Clear();
        }

        public bool Contains(KeyValuePair<Uri, object> item)
        {
            return ((IDictionary<Uri, object>)customer).Contains(item);
        }

        public void CopyTo(KeyValuePair<Uri, object>[] array, int arrayIndex)
        {
            ((IDictionary<Uri, object>)customer).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Uri, object> item)
        {
            return ((IDictionary<Uri, object>)customer).Remove(item);
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
