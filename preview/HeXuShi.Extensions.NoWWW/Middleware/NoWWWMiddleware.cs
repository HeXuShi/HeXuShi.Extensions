using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeXuShi.Extensions.Middleware
{
    public class NoWWWMiddleware
    {
        private readonly RequestDelegate _next;
        public NoWWWMiddleware(RequestDelegate next)

        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Host.Value.StartsWith("www."))
            {
                return _next(context);
            }

            var newHost = new HostString(context.Request.Host.Value.Substring(4));
            var request = context.Request;

            var redirectUrl = UriHelper.BuildAbsolute(
                context.Request.Scheme,
                newHost,
                request.PathBase,
                request.Path,
                request.QueryString);

            context.Response.StatusCode = StatusCodes.Status307TemporaryRedirect;
            context.Response.Headers[HeaderNames.Location] = redirectUrl;

            return Task.CompletedTask;
        }
    }
}
