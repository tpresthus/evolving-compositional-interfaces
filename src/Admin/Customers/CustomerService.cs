using System.Runtime.Serialization;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Admin.Customers
{
    public class CustomerService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUrl;

        public CustomerService(HttpClient httpClient, IOptions<CustomerOptions> authorizationOptions)
        {
            if (authorizationOptions == null)
            {
                throw new ArgumentNullException(nameof(authorizationOptions));
            }

            this.baseUrl = authorizationOptions.Value.BaseUrl;
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

        private static Customer MapCustomer(JToken jtoken)
        {
            var name = jtoken["name"]?.ToString();
            var email = jtoken["email"]?.ToString();
            var birthDateString = jtoken["birthDate"]?.ToString();
            if (DateTime.TryParseExact(birthDateString, "yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out var birthDate))
            {
                return new Customer(name, email, birthDate);
            }

            throw new FormatException("Invalid birth date format");
        }

        private class CustomerException : ApplicationException
        {
            public CustomerException(Uri requestUrl, Exception innerException)
                : base($"The customer retrieval request to <{requestUrl}> failed.", innerException)
            {
            }
        }
    }
}
