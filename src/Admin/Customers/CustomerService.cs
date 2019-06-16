using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace Admin.Customers
{
    public class CustomerService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUrl;

        public CustomerService(HttpClient httpClient, IOptionsSnapshot<HttpServiceOptions> httpServiceOptions)
        {
            if (httpServiceOptions == null)
            {
                throw new ArgumentNullException(nameof(httpServiceOptions));
            }

            this.baseUrl = httpServiceOptions.Get("CustomerOptions").BaseUrl;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                using (var response = await this.httpClient.GetAsync(this.baseUrl))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    var jtoken = jobject["customers"];
                    return jtoken.Select(MapCustomer);
                }
            }
            catch (Exception exception)
            {
                throw new CustomerException(this.baseUrl, exception);
            }
        }

        public async Task<CustomerResponse> GetCustomer(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var url = new Uri(this.baseUrl, id);

            try
            {
                using (var response = await this.httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return MapCustomerResponse(responseBody);
                }
            }
            catch (Exception exception)
            {
                throw new CustomerException(url, exception);
            }
        }

        public async Task<CustomerResponse> UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var url = new Uri(this.baseUrl, customer.Id);

            try
            {
                var customerRequestModel = new CustomerRequestModel(customer);
                var json = customerRequestModel.ToString();
                var content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var response = await this.httpClient.PutAsync(url, content))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return MapCustomerResponse(responseBody);
                }
            }
            catch (Exception exception)
            {
                throw new CustomerException(url, exception);
            }
        }

        private static Customer MapCustomer(JToken jtoken)
        {
            var id = jtoken["id"]?.ToString();
            var name = jtoken["name"]?.ToString();
            var email = jtoken["email"]?.ToString();
            var birthDate = jtoken["birthDate"]?.ToString();
            return new Customer(id, name, email, birthDate);
        }

        private static CustomerResponse MapCustomerResponse(string json)
        {
            var jobject = JObject.Parse(json);
            var customer = MapCustomer(jobject);
            return new CustomerResponse(customer, json);
        }

        private class CustomerException : ApplicationException
        {
            public CustomerException(Uri requestUrl, Exception innerException)
                : base($"The customer operation request to <{requestUrl}> failed.", innerException)
            {
            }
        }

        private class CustomerRequestModel
        {
            public CustomerRequestModel(Customer customer)
            {
                Id = customer.Id;
                Name = customer.Name;
                Email = customer.Email.ToString();
                BirthDate = customer.BirthDate.ToString();
            }

            public string Id { get; }

            public string Name { get; }

            public string Email { get; }

            public string BirthDate { get; }

            public override string ToString()
            {
                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                var settings = new JsonSerializerSettings
                {
                    ContractResolver = contractResolver
                };

                return JsonConvert.SerializeObject(this, settings);
            }
        }
    }
}
