using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Customers
{
    public class Expectation
    {
        public Expectation(HttpMethod method, string type)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            Method = method.Method;
            Type = type;
            Required = new List<string>();
        }

        public string Method { get; }

        [JsonProperty("@type")]
        public string Type { get; }

        public IList<string> Required { get; }
    }
}
