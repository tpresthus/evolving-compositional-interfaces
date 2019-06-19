using System.Collections.Generic;
using Newtonsoft.Json;

namespace Customers
{
    public class CustomerContext
    {
        [JsonProperty("@vocab")]
        public string Vocab => "https://schema.org/";
        public string Admin => "https://admin.example.com/";
        public string Name => "name";
        public string Address => "PostalAddress";
        public string Street => "streetAddress";
        public string City => "addressLocality";
        public string ZipCode => "postalCode";
        public string State => "addressRegion";
        public string Phone => "telephone";
        public string BirthDate => "birthDate";
        public string Email => "email";
        public string UserName => "alternateName";
        public string Website => "WebSite";
        public string Operations => "Action";
        public string Target => "target";
        public string Method => "httpMethod";
        public string Expects => "admin:expectation";
    }
}
