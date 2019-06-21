using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Admin.LinkedData
{
    public class Expectation
    {
        public Expectation(HttpMethod method)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
            Required = new List<string>();
        }
        
        public HttpMethod Method { get; }
        public IList<string> Required { get; }
    }
}
