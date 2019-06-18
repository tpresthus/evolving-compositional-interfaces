using System;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace Customers
{
    public class JsonLdOutputFormatter : TextOutputFormatter
    {
        private readonly JsonOutputFormatter jsonOutputFormatter;

        public JsonLdOutputFormatter(JsonOutputFormatter jsonOutputFormatter)
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/ld+json"));
            this.jsonOutputFormatter = jsonOutputFormatter ?? throw new ArgumentNullException(nameof(jsonOutputFormatter));
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return context.Object != null
                && context.ContentType.Value == "application/ld+json";
        }


        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            return this.jsonOutputFormatter.WriteResponseBodyAsync(context, selectedEncoding);
        }
    }
}
