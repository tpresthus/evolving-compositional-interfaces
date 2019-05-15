using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Admin.Authorization
{
    public class AuthorizationService
    {
        private readonly HttpClient httpClient;
        private Uri baseUrl;

        public AuthorizationService(HttpClient httpClient, IOptions<AuthorizationOptions> authorizationOptions)
        {
            if (authorizationOptions == null)
            {
                throw new ArgumentNullException(nameof(authorizationOptions));
            }

            this.baseUrl = authorizationOptions.Value.BaseUrl;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<User> GetAuthorizedUserAsync()
        {
            try
            {
                using (var response = await this.httpClient.GetAsync(this.baseUrl))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    return new User(jobject["name"]?.ToString());
                }
            }
            catch (Exception exception)
            {
                throw new AuthorizationException(this.baseUrl, exception);
            }
        }

        private class AuthorizationException : ApplicationException
        {
            public AuthorizationException(Uri requestUrl, Exception innerException)
                : base($"The authorization request to <{requestUrl}> failed.", innerException)
            {
            }
        }
    }
}