using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Admin.LinkedData
{
    public class LinkedDataObject : Dictionary<Uri, object>
    {
        private LinkedDataObject(LinkedDataContext context, IEnumerable<Operation> operations, IDictionary<Uri, object> properties)
            : base(properties)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            Operations = operations ?? throw new ArgumentNullException(nameof(operations));
        }

        public LinkedDataContext Context { get; }
        public IEnumerable<Operation> Operations { get; }

        public object this[string key]
        {
            get
            {
                var uri = new Uri(key);
                return this[uri];
            }
            set
            {
                var uri = new Uri(key);
                this[uri] = value;
            }
        }

        public static string MapKey(Uri uri)
        {
            var uriString = uri.ToString();

            switch (uriString)
            {
                case "https://schema.org/name":
                    return "Name";

                case "https://schema.org/PostalAddress":
                    return "Address";
                
                case "https://schema.org/streetAddress":
                    return "Street";
                
                case "https://schema.org/addressLocality":
                    return "City";
                
                case "https://schema.org/postalCode":
                    return "Zip code";
                
                case "https://schema.org/addressRegion":
                    return "State";
                
                case "https://schema.org/telephone":
                    return "Phone";
                
                case "https://schema.org/globalLocationNumber":
                    return "Ssn";
                
                case "https://schema.org/birthDate":
                    return "Born";
                
                case "https://schema.org/email":
                    return "Email";
                
                case "https://schema.org/alternateName":
                    return "UserName";
                
                case "https://schema.org/WebSite":
                    return "Website";
            }

            return null;
        }
        
        public static LinkedDataObject Parse(JToken jtoken)
        {
            var contextProperty = jtoken["@context"];
            var context = LinkedDataContext.Parse(contextProperty);
            var properties = new Dictionary<Uri, object>();
            var operations = new List<Operation>();
            ParseData(jtoken, properties, operations, context);
            return new LinkedDataObject(context, operations, properties);
        }

        private static void ParseData(JToken jtoken, Dictionary<Uri, object> properties, IList<Operation> operations, LinkedDataContext context)
        {
            foreach (var child in jtoken.Children())
            {
                if (child is JProperty property)
                {
                    object value;

                    switch (property.Name)
                    {
                        case "@id":
                            var idValueUri = new Uri(property.Value.ToString());
                            // This URI is complete bogus, but there's not enough time to do this in a sane way.
                            var idKeyUri = new Uri("http://www.w3.org/ns/json-ld#id");
                            properties.Add(idKeyUri, idValueUri);
                            continue;

                        case "@type":
                            // This URI is complete bogus, but there's not enough time to do this in a sane way.
                            var typeKeyUri = new Uri("http://www.w3.org/ns/json-ld#type");
                            if (!properties.ContainsKey(typeKeyUri))
                            {
                                var typeValueUri = new Uri(context.DefaultVocabulary, property.Value.ToString());
                                // Major hack because this parser is utter shite.
                                properties.Add(typeKeyUri, typeValueUri);
                            }
                            continue;

                        case "@context":
                            continue;

                        case "operations":
                            ParseOperations(property, context, operations);
                            continue;
                    }

                    switch (property.Value.Type)
                    {
                        case JTokenType.Object:
                            ParseData(property.Value, properties, operations, context);
                            break;

                        default:
                            var uri = context[property.Name];
                            value = property.Value.ToObject<object>();
                            properties.Add(uri, value);
                            break;
                    }
                }
            }
        }

        private static void ParseOperations(JProperty property, LinkedDataContext context, IList<Operation> operations)
        {
            foreach (var operationProperty in property.Value)
            {
                var target = operationProperty["target"].ToString();
                var targetUri = new Uri(target);
                var operationType = operationProperty["@type"].ToString();
                var operationTypeUri = new Uri(context.DefaultVocabulary, operationType);
                var operation = new Operation(targetUri, operationTypeUri);

                var expects = operationProperty["expects"];
                if (expects != null)
                {
                    var method = expects["method"];
                    var methodValue = method.ToString();
                    var httpMethod = new HttpMethod(methodValue);
                    var expectsType = expects["@type"];
                    var expectsTypeValue = expectsType.ToString();
                    var expectsTypeUri = new Uri(context.DefaultVocabulary, expectsTypeValue);
                    operation.Expects = new Expectation(httpMethod, expectsTypeUri);
                    var required = expects["required"];
                    if (required != null)
                    {
                        foreach (var requirement in required)
                        {
                            var requirementString = requirement?.ToString();
                            operation.Expects.Required.Add(requirementString);
                        }
                    }
                }

                operations.Add(operation);
            }
        }
    }
}
