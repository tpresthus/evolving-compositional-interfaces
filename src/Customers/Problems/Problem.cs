using System.Net;
using System;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Customers.Problems
{
    /// <summary>
    /// An RFC 7807 Problem Details for HTTP APIs implementation.
    /// </summary>
    public class Problem
    {
        private const string TypeBaseUrl = "https://api.example.com/problems/";

        public const string ContentType = "application/problem+json";

        public Problem(Exception exception, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Status = HttpStatusCode.InternalServerError;
            Title = exception?.Message ?? "An unexpected error occurred";
            Type = ProblemTypeOf(exception);

            if (exception is CustomerNotFoundException)
            {
                Status = HttpStatusCode.NotFound;
            }

            if (env.IsDevelopment())
            {
                Detail = exception.ToString();
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public Uri Type { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public string Title { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 3)]
        public HttpStatusCode Status { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 4)]
        public string Detail { get;}

        public override string ToString()
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

            return JsonConvert.SerializeObject(this, settings);
        }

        private Uri ProblemTypeOf(Exception exception)
        {
            string type = "general";
            if (exception is CustomerNotFoundException)
            {
                type = "notfound";
            }

            return new Uri(TypeBaseUrl + type);
        }
    }
}
