using IP2Region;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        private string _webRootPath;
        private string _noCNSuffix;
        public JumpToCNMiddleware(
            RequestDelegate next,
            string noCNSuffix,
            IHostingEnvironment hostingEnvironment
            )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _hostingEnvironment = hostingEnvironment;
            _webRootPath = _hostingEnvironment.WebRootPath;
            _noCNSuffix = noCNSuffix;
        }
        public Task Invoke(HttpContext context)
        {
            var index = context.Request.Host.Value.LastIndexOf(".");
            if (index == -1 || context.Connection.RemoteIpAddress.ToString().Length < 6)
                return _next(context);

            string cnSuffix = ".cn";
            var suffix = context.Request.Host.Value.Substring(index);
            if(_noCNSuffix == string.Empty && suffix == cnSuffix)
                return _next(context);

            HostString newHost;
            try
            {
                using (var _search = new DbSearcher(_webRootPath + @"\db\ip2region.db"))
                {
                    var region = _search.MemorySearch(context.Connection.RemoteIpAddress.ToString()).Region;
                    var isChinaIp = region.StartsWith("中国|");
                    if (suffix != cnSuffix && isChinaIp)
                    {
                        newHost = new HostString(context.Request.Host.Value.Replace(suffix, cnSuffix));
                    }
                    else if(suffix != _noCNSuffix && !isChinaIp  && _noCNSuffix != string.Empty)
                    {
                        newHost = new HostString(context.Request.Host.Value.Replace(suffix, _noCNSuffix));
                    }
                    else
                       return _next(context);
                }
            }
            catch
            {
                return _next(context);
            }
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
