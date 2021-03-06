
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Customers
{
    public class CustomerRepository
    {
        private readonly ConcurrentDictionary<string, Customer> customers;

        public CustomerRepository()
        {
            this.customers = new ConcurrentDictionary<string, Customer>(new[]
            {
                new Customer
                {
                    Id = "C1231006815",
                    Name = "Michael J. Utter",
                    Address = new Address
                    {
                        Street = "3355 North Street",
                        City = "Charlottesville",
                        ZipCode = "22903",
                        State = "VA"
                    },
                    Ssn = "082-30-XXXX",
                    Phone = "315-642-3734",
                    BirthDate = "1991-04-29",
                    Email = "MichaelJUtter@jourrapide.com",
                    UserName = "Samly1991",
                    Website = "thestorelive.com"
                },
                new Customer
                {
                    Id = "C1666544295",
                    Name = "Suzan J. Steinke",
                    Address = new Address
                    {
                        Street = "2317 Shadowmar Drive",
                        City = "Kenner",
                        ZipCode = "70062",
                        State = "LA"
                    },
                    Ssn = "662-10-XXXX",
                    Phone = "504-782-8884",
                    BirthDate = "1962-07-18",
                    Email = "SuzanJSteinke@dayrep.com",
                    UserName = "Veackell",
                    Website = "santiagocusco.com"
                },
                new Customer
                {
                    Id = "C1305486145",
                    Name = "Brent W. Ashley",
                    Address = new Address
                    {
                        Street = "1578 Heavner Avenue",
                        City = "Duluth",
                        ZipCode = "30136",
                        State = "GA"
                    },
                    Ssn = "255-54-XXXX",
                    Phone = "770-813-5071",
                    BirthDate = "1984-11-12",
                    Email = "BrentWAshley@rhyta.com",
                    UserName = "Thadjaink",
                    Website = "msattitude.com"
                },
                new Customer
                {
                    Id = "C840083671",
                    Name = "Sarah J. Westerman",
                    Address = new Address
                    {
                        Street = "2261 Everette Alley",
                        City = "Fort Lauderdale",
                        ZipCode = "33301",
                        State = "FL"
                    },
                    Ssn = "589-60-XXXX",
                    Phone = "954-847-1890",
                    BirthDate = "1975-01-18",
                    Email = "SarahJWesterman@jourrapide.com",
                    UserName = "Shmeack",
                    Website = "peabodysapples.com"
                },
                new Customer
                {
                    Id = "C2048537720",
                    Name = "Dennis S. Snyder",
                    Address = new Address
                    {
                        Street = "1900 Hill Haven Drive",
                        City = "Temple",
                        ZipCode = "76501",
                        State = "TX"
                    },
                    Ssn = "463-31-XXXX",
                    Phone = "254-295-6523",
                    BirthDate = "1989-03-14",
                    Email = "DennisSSnyder@rhyta.com",
                    UserName = "Thimpare",
                    Website = "CrossingCard.com"
                }
            }.Select(customer => new KeyValuePair<string, Customer>(customer.Id, customer)));
       }

       public IEnumerable<Customer> GetCustomers()
       {
           return this.customers.Select(kvp => kvp.Value);
       }

       public Customer GetCustomer(string id)
       {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (this.customers.TryGetValue(id, out var customer))
            {
                return customer;
            }

            throw new CustomerNotFoundException(id);
       }

       public Customer Update(Customer customer)
       {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var existingCustomer = GetCustomer(customer.Id);
           this.customers.TryUpdate(customer.Id, customer, existingCustomer);
           return customer;
       }
    }
}
