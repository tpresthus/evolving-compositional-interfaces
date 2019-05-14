using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Admin.Authorization
{
    public class AuthorizationService
    {
        private readonly HttpClient httpClient;

        public AuthorizationService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<User> GetAuthorizedUser()
        {
            using (var response = await this.httpClient.GetAsync("http://authorization"))
            {
                if (response == null)
                {
                    throw new InvalidOperationException("The authorization response was null");
                }

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var jobject = JObject.Parse(responseBody);
                return new User(jobject["name"].ToString());
            }
        }
    }
}