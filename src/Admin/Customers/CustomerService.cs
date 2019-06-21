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
using Admin.LinkedData;

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

        public async Task<IEnumerable<LinkedDataObject>> GetCustomers()
        {
            try
            {
                using (var response = await this.httpClient.GetAsync(this.baseUrl))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    var jtoken = jobject["customers"];
                    return jtoken.Select(LinkedDataObject.Parse);
                }
            }
            catch (Exception exception)
            {
                throw new CustomerException(this.baseUrl, exception);
            }
        }

        public async Task<LinkedDataObject> GetCustomer(string id)
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
                    var jtoken = JObject.Parse(responseBody);
                    var linkedDataObject = LinkedDataObject.Parse(jtoken);
                    Console.WriteLine(JsonConvert.SerializeObject(linkedDataObject));
                    return linkedDataObject;
                }
            }
            catch (Exception exception)
            {
                throw new CustomerException(url, exception);
            }
        }
        
        private class CustomerException : ApplicationException
        {
            public CustomerException(Uri requestUrl, Exception innerException)
                : base($"The customer operation request to <{requestUrl}> failed.", innerException)
            {
            }
        }
    }
}
