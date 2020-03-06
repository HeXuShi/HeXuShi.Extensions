using HeXuShi.Extensions.Middleware;
using Microsoft.AspNetCore.Builder;
using System;

namespace HeXuShi.Extensions
{
    public static class JumpToComPolicyExtensions
    {
        public static IApplicationBuilder JumpToCN(this IApplicationBuilder app, string noCNSuffix = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if(noCNSuffix == null)
                app.UseMiddleware<JumpToComMiddleware>(".com");
            else
                app.UseMiddleware<JumpToComMiddleware>(noCNSuffix);
            return app;
        }
    }
}