using HeXuShi.Extensions.Middleware;
using Microsoft.AspNetCore.Builder;
using System;

namespace HeXuShi.Extensions
{
    public static class BlockChinaPolicyExtensions
    {
        public static IApplicationBuilder JumpToCom(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.UseMiddleware<BlockChinaMiddleware>();
            return app;
        }
    }
}