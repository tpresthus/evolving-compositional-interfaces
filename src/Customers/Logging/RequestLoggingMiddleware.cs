using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Customers.Logging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private Func<string, Exception, string> defaultFormatter = (state, exception) => state;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = UriHelper.GetDisplayUrl(context.Request);
            var uri = new Uri(url);
            var requestBody = new StreamReader(requestBodyStream).ReadToEnd();
            var formattedRequestBody = Prettify(requestBody);
            
            this.logger.Log(LogLevel.Information,
                            1,
                            $"{context.Request.Method} {uri.PathAndQuery} {context.Request.Protocol}\n" +
                            ToString(context.Request.Headers) + "\n" +
                            formattedRequestBody,
                            null,
                            this.defaultFormatter);
           
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await next(context);
            context.Request.Body = originalRequestBody;
        }

        private string ToString(IHeaderDictionary headers)
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

        private static string Prettify(string json)
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
    }
}