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
    public class BlockChinaMiddleware
    {
        private readonly RequestDelegate _next;
        private HandleRequest _handleRequest;
        public BlockChinaMiddleware(
            RequestDelegate next
            )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
       
        public Task Invoke(HttpContext context)
        {
            var result = _handleRequest.Handle(context);
            if (!result)
                return _next(context);

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return Task.CompletedTask;
        }
    }
}
