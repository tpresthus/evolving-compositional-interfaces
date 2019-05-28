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
        private readonly Uri baseUrl;

        public NavigationService(HttpClient httpClient, IOptionsSnapshot<HttpServiceOptions> httpServiceOptions)
        {
            if (httpServiceOptions == null)
            {
                throw new ArgumentNullException(nameof(httpServiceOptions));
            }

            this.baseUrl = httpServiceOptions.Get("NavigationOptions").BaseUrl;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Menu> GetMenuAsync()
        {
            try
            {
                using (var response = await this.httpClient.GetAsync(this.baseUrl))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    var jtoken = jobject["items"];
                    var menuItems = jtoken.Select(MapMenuItem);
                    return new Menu(menuItems);
                }
            }
            catch (Exception exception)
            {
                throw new NavigationException(this.baseUrl, exception);
            }
        }

        private MenuItem MapMenuItem(JToken jToken)
        {
            var title = jToken["title"]?.ToString();
            var id = jToken["id"]?.ToString();
            return new MenuItem(title, id);
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
