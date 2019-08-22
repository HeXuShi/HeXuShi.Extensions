using HeXuShi.Extensions.Middleware;
using Microsoft.AspNetCore.Builder;
using System;

namespace HeXuShi.Extensions
{
    public static class JumpToCNPolicyExtensions
    {
        public static IApplicationBuilder JumpToCN(this IApplicationBuilder app, string noCNSuffix = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if(noCNSuffix == null)
                app.UseMiddleware<JumpToCNMiddleware>(".cn", string.Empty, JumpOption.OnlyTo_SpecSuffix);
            else
                app.UseMiddleware<JumpToCNMiddleware>(".cn", noCNSuffix, JumpOption.SuffixTo_SpecSuffix);
            return app;
        }
        public static IApplicationBuilder ComplexJumpToCN(this IApplicationBuilder app, string first, string second, JumpOption option)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.UseMiddleware<JumpToCNMiddleware>(first, second, option);
            return app;
        }
    }
}