using System.Collections.Generic;
using Newtonsoft.Json;

namespace Customers
{
    public class CustomerContext
    {
        public string Schema => "https://schema.org/";
        public string Name => "schema:name";
        public string Address => "schema:PostalAddress";
        public string Street => "schema:streetAddress";
        public string City => "schema:addressLocality";
        public string ZipCode => "schema:postalCode";
        public string State => "schema:addressRegion";
        public string Phone => "schema:telephone";
        public string BirthDate => "schema:birthDate";
        public string Email => "schema:email";
        public string UserName => "schema:alternateName";
        public string Website => "schema:WebSite";
        public string Operations => "schema:Action";
        public string Target => "schema:target";
        public string Method => "schema:httpMethod";
    }
}
