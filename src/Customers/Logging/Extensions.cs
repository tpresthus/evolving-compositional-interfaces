using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Customers.Logging
{
    public static class Extensions
    {
        public static string Prettify(this string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }
 
        public static string ConvertToString(this IHeaderDictionary headers)
        {
            var sb = new StringBuilder();
            foreach (var header in headers)
            {
                sb.Append(header.Key);
                sb.Append(": ");
                sb.AppendLine(header.Value.ToString());
            }
            return sb.ToString();
        }
   }
}