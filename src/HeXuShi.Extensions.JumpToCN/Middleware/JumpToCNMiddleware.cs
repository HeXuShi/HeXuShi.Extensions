using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeXuShi.Extensions.Middleware
{
    public class JumpToCNMiddleware
    {
        private readonly RequestDelegate _next;
        private HandleRequest _handleRequest;
        public JumpToCNMiddleware(
            RequestDelegate next,
            string first,
            string second,
            JumpOption option
            )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _handleRequest = new HandleRequest(first, second, option);
        }
       
        public Task Invoke(HttpContext context)
        {
            var result = _handleRequest.Handle(context);
            if (!result.Item1)
                return _next(context);

            var newHost = new HostString(result.Item2);
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
