using System.Linq;
using Customers.Logging;
using Customers.Problems;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Customers
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CustomerRepository>();
            services.AddMvc(options =>
            {
                var jsonOutputFormatter = (JsonOutputFormatter)options.OutputFormatters.First(formatter => formatter is JsonOutputFormatter);
                options.OutputFormatters.Add(new JsonLdOutputFormatter(jsonOutputFormatter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseProblemJsonExceptionHandler(env);
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ResponseLoggingMiddleware>();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=CustomerApi}/{action=Index}/{id?}");
            });
        }
    }
}
