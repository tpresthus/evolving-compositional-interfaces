using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Customers
{
    public class Operation
    {
        public Operation(string actionType, Uri target, HttpMethod method)
        {
            if (string.IsNullOrWhiteSpace(actionType))
            {
                throw new ArgumentNullException(nameof(actionType));
            }

            Target = target ?? throw new System.ArgumentNullException(nameof(target));
            Type = actionType;
        }

        public Uri Target { get; }

        [JsonProperty("@type")]
        public string Type { get; }

        public Expectation Expects { get; set; }
    }
}
