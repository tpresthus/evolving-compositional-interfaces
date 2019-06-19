using System;
using System.Net.Http;

namespace Admin.LinkedData
{
    public class Expectation
    {
        public Expectation(HttpMethod method, Uri type)
        {
            this.Method = method ?? throw new ArgumentNullException(nameof(method));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));

        }
        public HttpMethod Method { get; }
        public Uri Type { get; }
    }
}
