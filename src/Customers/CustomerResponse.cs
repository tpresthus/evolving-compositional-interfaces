using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Customers
{
    public class CustomerResponse
    {
        public CustomerResponse(Customer customer, OperationFactory operationFactory)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (operationFactory == null)
            {
                throw new ArgumentNullException(nameof(operationFactory));
            }

            Operations = new List<Operation>
            {
                { operationFactory.CreateOperation(customer, "schema:UpdateAction") }
            };

            var idOperation = operationFactory.CreateOperation(customer, "id");
            Id = idOperation.Target;
            Name = customer.Name;
            Ssn = customer.Ssn;
            Phone = customer.Phone;
            BirthDate = customer.BirthDate;
            Email = customer.Email;
            UserName = customer.UserName;
            Website = customer.Website;

            if (customer.Address != null)
            {
                Address = new AddressResponse(customer.Address);
            }
        }

        [JsonProperty("@context", Order = 1)]
        public CustomerContext Context => new CustomerContext();

        [JsonProperty("@id", Order = 2)]
        public Uri Id { get; }

        [JsonProperty("@type", Order = 3)]
        public string Type => "schema:customer";

        [JsonProperty(Order = 4)]
        public string Name { get; }

        [JsonProperty(Order = 5)]
        public AddressResponse Address { get;}

        [JsonProperty(Order = 6)]
        public string Ssn { get; }

        [JsonProperty(Order = 7)]
        public string Phone { get; }

        [JsonProperty(Order = 8)]
        public string BirthDate { get; }

        [JsonProperty(Order = 9)]
        public string Email { get; }

        [JsonProperty(Order = 10)]
        public string UserName { get; }

        [JsonProperty(Order = 11)]
        public string Website { get; }

        [JsonProperty(Order = 12)]
        public IEnumerable<Operation> Operations { get; }
    }
}
