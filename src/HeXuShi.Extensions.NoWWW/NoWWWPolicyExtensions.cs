using HeXuShi.Extensions.Middleware;
using Microsoft.AspNetCore.Builder;
using System;

namespace HeXuShi.Extensions
{
    public static class NoWWWPolicyExtensions
    {
        public static IApplicationBuilder UseNoWWW(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.UseMiddleware<NoWWWMiddleware>();
            return app;
        }
    }
}
