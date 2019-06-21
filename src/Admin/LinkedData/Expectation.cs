using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Admin.LinkedData
{
    public class Expectation
    {
        public Expectation(HttpMethod method, Uri type)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Required = new List<string>();
        }
        
        public HttpMethod Method { get; }
        public Uri Type { get; }
        public IList<string> Required { get; }
    }
}
