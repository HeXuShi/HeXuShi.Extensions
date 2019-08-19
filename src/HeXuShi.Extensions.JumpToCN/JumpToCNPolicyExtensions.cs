using HeXuShi.Extensions.Middleware;
using Microsoft.AspNetCore.Builder;
using System;

namespace HeXuShi.Extensions
{
    public static class JumpToCNPolicyExtensions
    {
        public static IApplicationBuilder JumpToCN(this IApplicationBuilder app, string noCNSuffix = "")
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.UseMiddleware<JumpToCNMiddleware>(noCNSuffix);
            return app;
        }
    }
}