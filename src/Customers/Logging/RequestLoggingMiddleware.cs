using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

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
            var formattedRequestBody = requestBody.Prettify();
            
            this.logger.Log(LogLevel.Information,
                            1,
                            $"{context.Request.Method} {uri.PathAndQuery} {context.Request.Protocol}\n" +
                            context.Request.Headers.ConvertToString() + "\n" +
                            formattedRequestBody,
                            null,
                            this.defaultFormatter);
           
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await next(context);
            context.Request.Body = originalRequestBody;
        }
    }
}