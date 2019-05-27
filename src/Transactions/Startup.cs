using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Transactions
{
    public class Startup
    {
        private readonly string contentRoot;

        public Startup(IConfiguration configuration)
        {
            this.contentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
        }

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

                var list = new { Transactions = ReadTransactionsFromFile() };

                var json = JsonConvert.SerializeObject(list, settings);

                var headers = context.Response.GetTypedHeaders();

                headers.ContentType = new MediaTypeHeaderValue("application/json");

                await context.Response.WriteAsync(json);
            });
        }

        private IEnumerable<Transaction> ReadTransactionsFromFile()
        {
            // TODO: Make this IAsyncEnumerable.
            var transactionsFilePath = Path.Combine(this.contentRoot, "transactions.csv");

            using (var fileStream = File.OpenRead(transactionsFilePath))
            using (var streamReader = new StreamReader(fileStream))
            {
                string line;
                var i = 0;

                // TODO: Use ReadLineAsync() with IAsyncEnumerable.
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (i++ == 0)
                    {
                        // Skip the header line
                        continue;
                    }

                    Transaction transaction;

                    try
                    {
                        transaction = ParseTransaction(line);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Error parsing the following line:");
                        Console.WriteLine(line);
                        Console.WriteLine();
                        Console.WriteLine(exception);
                        yield break;
                    }

                    yield return transaction;
                }
            }
        }

        private static Transaction ParseTransaction(string line)
        {
            var columns = line.Split(',');
            var step = Int32.Parse(columns[0]);
            var type = columns[1];
            var amount = Decimal.Parse(columns[2], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var nameOrig = columns[3];
            var oldbalanceOrg = Decimal.Parse(columns[4], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var newbalanceOrig = Decimal.Parse(columns[5], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var nameDest = columns[6];
            var oldbalanceDest = Decimal.Parse(columns[7], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var newbalanceDest = Decimal.Parse(columns[8], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var isFraud = columns[9] == "1";
            var isFlaggedFraud = columns[10] == "1";

            return new Transaction
            {
                Step = step,
                Type = type,
                Amount = amount,
                Payer = new Party
                {
                    Name = nameOrig,
                    OldBalance = oldbalanceOrg,
                    NewBalance = newbalanceOrig
                },
                Payee = new Party
                {
                    Name = nameDest,
                    OldBalance = oldbalanceDest,
                    NewBalance = newbalanceDest
                },
                IsFraud = isFraud,
                IsFlaggedFraud = isFlaggedFraud
            };
        }

        private class Transaction
        {
            public int Step;
            public string Type;
            public decimal Amount;
            public Party Payer;
            public Party Payee;
            public bool IsFraud;
            public bool IsFlaggedFraud;
        }

        private class Party
        {
            public string Name;
            public decimal OldBalance;
            public decimal NewBalance;
        }
    }
}
