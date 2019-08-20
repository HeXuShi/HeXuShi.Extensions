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
        public HostString changeDomainSuffix(string domain, int index, string newSuffix)
        {
            domain = domain.Remove(index, domain.Length - index);
            return new HostString(domain + newSuffix);
        }
        public Task Invoke(HttpContext context)
        {
            var index = context.Request.Host.Value.LastIndexOf(".");
            if (index == -1 || context.Connection.RemoteIpAddress.ToString().Length < 6)
                return _next(context);

            string cnSuffix = ".cn";
            var suffix = context.Request.Host.Value.Substring(index);
            if (_noCNSuffix == string.Empty && suffix == cnSuffix)
                return _next(context);

            HostString newHost;
            try
            {
                IsChinaIp.Setup();
                bool isChinaIp = false;
                switch (context.Connection.RemoteIpAddress.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        isChinaIp = IsChinaIp.VerifyIPv4(context.Connection.RemoteIpAddress.ToString());
                        break;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        isChinaIp = IsChinaIp.VerifyIPv6(context.Connection.RemoteIpAddress.ToString());
                        break;
                    default:
                        return _next(context);
                }
                if (suffix != cnSuffix && isChinaIp)
                {
                    newHost = changeDomainSuffix(context.Request.Host.Value, index, cnSuffix);
                }
                else if (suffix != _noCNSuffix && !isChinaIp && _noCNSuffix != string.Empty)
                {
                    newHost = changeDomainSuffix(context.Request.Host.Value, index, _noCNSuffix);
                }
                else
                    return _next(context);
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
