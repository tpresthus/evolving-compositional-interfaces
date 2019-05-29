using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Customers
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

                var list = new
                {
                    Customers = new[]
                    {
                        new
                        {
                            Id = "C1231006815",
                            Name = "Michael J. Utter",
                            Address = new {
                                Street = "3355 North Street",
                                City = "Charlottesville",
                                ZipCode = "22903",
                                State = "VA"
                            },
                            Ssn = "082-30-XXXX",
                            Phone = "315-642-3734",
                            BirthDate = "1991-04-29",
                            Email = "MichaelJUtter@jourrapide.com",
                            UserName = "Samly1991",
                            Website = "thestorelive.com"
                        },
                        new
                        {
                            Id = "C1666544295",
                            Name = "Suzan J. Steinke",
                            Address = new {
                                Street = "2317 Shadowmar Drive",
                                City = "Kenner",
                                ZipCode = "70062",
                                State = "LA"
                            },
                            Ssn = "662-10-XXXX",
                            Phone = "504-782-8884",
                            BirthDate = "1962-07-18",
                            Email = "SuzanJSteinke@dayrep.com",
                            UserName = "Veackell",
                            Website = "santiagocusco.com"
                        },
                        new
                        {
                            Id = "C1305486145",
                            Name = "Brent W. Ashley",
                            Address = new {
                                Street = "1578 Heavner Avenue",
                                City = "Duluth",
                                ZipCode = "30136",
                                State = "GA"
                            },
                            Ssn = "255-54-XXXX",
                            Phone = "770-813-5071",
                            BirthDate = "1984-11-12",
                            Email = "BrentWAshley@rhyta.com",
                            UserName = "Thadjaink",
                            Website = "msattitude.com"
                        },
                        new
                        {
                            Id = "C840083671",
                            Name = "Sarah J. Westerman",
                            Address = new {
                                Street = "2261 Everette Alley",
                                City = "Fort Lauderdale",
                                ZipCode = "33301",
                                State = "FL"
                            },
                            Ssn = "589-60-XXXX",
                            Phone = "954-847-1890",
                            BirthDate = "1975-01-18",
                            Email = "SarahJWesterman@jourrapide.com",
                            UserName = "Shmeack",
                            Website = "peabodysapples.com"
                        },
                        new
                        {
                            Id = "C2048537720",
                            Name = "Dennis S. Snyder",
                            Address = new {
                                Street = "1900 Hill Haven Drive",
                                City = "Temple",
                                ZipCode = "76501",
                                State = "TX"
                            },
                            Ssn = "463-31-XXXX",
                            Phone = "254-295-6523",
                            BirthDate = "1989-03-14",
                            Email = "DennisSSnyder@rhyta.com",
                            UserName = "Thimpare",
                            Website = "CrossingCard.com"
                        },
                    }
                };

                var json = JsonConvert.SerializeObject(list, settings);

                var headers = context.Response.GetTypedHeaders();

                headers.ContentType = new MediaTypeHeaderValue("application/json");

                await context.Response.WriteAsync(json);
            });
        }
    }
}
