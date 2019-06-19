using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Admin.LinkedData
{
    public class LinkedDataContext : Dictionary<string, Uri>
    {
        public LinkedDataContext(Uri defaultVocabulary)
        {
            this.DefaultVocabulary = defaultVocabulary;
            Vocabularies = new Dictionary<string, Uri>();
        }

        public Uri DefaultVocabulary { get; }
        public Dictionary<string, Uri> Vocabularies { get; }


       public static LinkedDataContext Parse(JToken contextToken)
        {
            if (contextToken is null)
            {
                throw new ArgumentNullException(nameof(contextToken));
            }

            var vocabProperty = contextToken["@vocab"];
            var defaultVocabulary = new Uri(vocabProperty.ToString());
            var context = new LinkedDataContext(defaultVocabulary);

            foreach (JProperty property in contextToken.Children())
            {
                if (property.Name == "@vocab")
                {
                    continue;
                }

                var value = property.Value.ToString();
                if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
                {
                    context.Vocabularies.Add(property.Name, uri);
                    continue;
                }

                Uri valueUri = Expand(value, context);
                context.Add(property.Name, valueUri);
            }

            return context;
        }

        private static Uri Expand(string value, LinkedDataContext context)
        {
            if (value.Contains(':'))
            {
                var keyPrefix = value.Split(':');
                var prefix = keyPrefix[0];
                var key = keyPrefix[1];
                var baseUri = context.Vocabularies[prefix];
                return new Uri(baseUri, key);
            }

            return new Uri(context.DefaultVocabulary, value);
        }
    }
}
