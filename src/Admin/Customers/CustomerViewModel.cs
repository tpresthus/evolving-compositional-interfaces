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
    public class CustomerViewModel : BaseViewModel, IEnumerable<KeyValuePair<string, object>>
    {
        private readonly IDictionary<string, object> data;
        
        public CustomerViewModel(LinkedDataObject customer, Menu menu, User user, UrlFactory urlFactory, IEnumerable<Transaction> transactions)
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
            Name = (string)customer["https://schema.org/name"];
            this.data = new Dictionary<string, object>(Map(customer));
        }

        public string Id { get; }
        public string Name { get; }
        public IEnumerable<TransactionViewModel> Transactions { get; }
        public object this[string key] { get => data[key]; set => data[key] = value; }

        private static IEnumerable<KeyValuePair<string, object>> Map(LinkedDataObject data)
        {
            foreach (var kvp in data)
            {
                var key = Map(kvp.Key);
                if (key == null)
                {
                    continue;
                }
                
                yield return new KeyValuePair<string, object>(key, kvp.Value);
            }
        }

        private static string Map(Uri uri)
        {
            var uriString = uri.ToString();

            switch (uriString)
            {
                case "https://schema.org/name":
                    return "Name";

                case "https://schema.org/PostalAddress":
                    return "Address";
                
                case "https://schema.org/streetAddress":
                    return "Street";
                
                case "https://schema.org/addressLocality":
                    return "City";
                
                case "https://schema.org/postalCode":
                    return "Zip code";
                
                case "https://schema.org/addressRegion":
                    return "State";
                
                case "https://schema.org/telephone":
                    return "Phone";
                
                case "https://schema.org/globalLocationNumber":
                    return "Ssn";
                
                case "https://schema.org/birthDate":
                    return "Born";
                
                case "https://schema.org/email":
                    return "Email";
                
                case "https://schema.org/alternateName":
                    return "UserName";
                
                case "https://schema.org/WebSite":
                    return "Website";
            }

            return null;
        }

        private TransactionViewModel MapTransaction(Transaction transaction)
        {
            return new TransactionViewModel(transaction);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IDictionary<string, object>)data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, object>)data).GetEnumerator();
        }
    }
}
