using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Admin.Navigation
{
    public class NavigationService
    {
        private readonly HttpClient httpClient;
        private Uri baseUrl;

        public NavigationService(HttpClient httpClient, IOptions<NavigationOptions> navigationOptions)
        {
            if (navigationOptions == null)
            {
                throw new ArgumentNullException(nameof(navigationOptions));
            }

            this.baseUrl = navigationOptions.Value.BaseUrl;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Menu> GetMenu()
        {
            try
            {
                using (var response = await this.httpClient.GetAsync(this.baseUrl))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    var jtoken = jobject["items"];
                    var menuItems = jtoken.Select(x => new MenuItem(x["title"]?.ToString()));
                    return new Menu(menuItems);
                }
            }
            catch (Exception exception)
            {
                throw new NavigationException(this.baseUrl, exception);
            }
        }

        private class NavigationException : ApplicationException
        {
            public NavigationException(Uri requestUrl, Exception innerException)
                : base($"The navigation request to <{requestUrl}> failed.", innerException)
            {
            }
        }
    }
}