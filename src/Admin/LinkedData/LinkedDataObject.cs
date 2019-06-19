using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Admin.LinkedData
{
    public class LinkedDataObject
    {
        private LinkedDataObject(LinkedDataContext context, IEnumerable<Operation> operations, IDictionary<Uri, object> properties)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            Operations = operations ?? throw new ArgumentNullException(nameof(operations));
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public LinkedDataContext Context { get; }
        public IEnumerable<Operation> Operations { get; }
        public IDictionary<Uri, object> Properties { get; }

        public static LinkedDataObject Parse(string json)
        {
            var jobject = JObject.Parse(json);
            var contextProperty = jobject["@context"];
            var context = LinkedDataContext.Parse(contextProperty);
            var properties = new Dictionary<Uri, object>();
            var operations = new List<Operation>();
            ParseData(jobject, properties, operations, context);
            return new LinkedDataObject(context, operations, properties);
        }

        private static void ParseData(JToken jtoken, Dictionary<Uri, object> properties, IList<Operation> operations, LinkedDataContext context)
        {
            foreach (var child in jtoken.Children())
            {
                if (child is JProperty property)
                {
                    if (property.Name.StartsWith("@"))
                    {
                        continue;
                    }

                    if (property.Name == "operations")
                    {
                        ParseOperations(property, context, operations);
                        continue;
                    }

                    var uri = context[property.Name];
                    object value;

                    switch (property.Value.Type)
                    {
                        case JTokenType.Object:
                            ParseData(property.Value, properties, operations, context);
                            break;

                        default:
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
                }

                operations.Add(operation);
            }
        }
    }
}
