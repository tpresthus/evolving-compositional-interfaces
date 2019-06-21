using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Customers.Logging
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private Func<string, Exception, string> defaultFormatter = (state, exception) => state;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var bodyStream = context.Response.Body;

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();
            var formattedResponseBody = responseBody.Prettify();
            var statusCode = (HttpStatusCode)context.Response.StatusCode;
            this.logger.Log(LogLevel.Information,
                            1,
                            $"{context.Request.Protocol} {context.Response.StatusCode} {statusCode}\n" +
                            context.Response.Headers.ConvertToString() + "\n" +
                            formattedResponseBody,
                            null,
                            this.defaultFormatter);
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(bodyStream);
        }
    }
}