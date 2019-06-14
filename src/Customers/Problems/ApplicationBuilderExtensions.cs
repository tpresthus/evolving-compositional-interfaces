using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Customers.Problems
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseProblemJsonExceptionHandler(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    var problem = new Problem(exception, env);
                    context.Response.StatusCode = (int)problem.Status;
                    context.Response.ContentType = Problem.ContentType;
                    await context.Response.WriteAsync(problem.ToString());
                });
            });
        }
    }
}
