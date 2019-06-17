using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Transactions
{
    public class Startup
    {
        private readonly string contentRoot;

        public Startup(IConfiguration configuration)
        {
            contentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(json =>
                {
                    json.SerializerSettings.Formatting = Formatting.Indented;
                    json.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                });
            
            services.AddSingleton(s =>
                new TransactionRepository(() => TransactionRepository.ReadTransactionsFromFile(contentRoot)));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
