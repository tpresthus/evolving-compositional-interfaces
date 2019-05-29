using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Navigation
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          app.Run(async (context) =>
            {
                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                var settings = new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                };

                var menu = new
                {
                    Items = new[]
                    {
                        new
                        {
                            Title = "Customers",
                            Id = "customers"
                        },
                        new
                        {
                            Title = "Transactions",
                            Id = "transactions"
                        }
                    }
                };

                var json = JsonConvert.SerializeObject(menu, settings);

                var headers = context.Response.GetTypedHeaders();

                headers.ContentType = new MediaTypeHeaderValue("application/json");

                await context.Response.WriteAsync(json);
            });
        }
    }
}
