using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeXuShi.Extensions.Middleware
{
    public class HandleRequest
    {
        public bool Handle(HttpContext context)
        {
            IsChinaIpAddress.Setup();
            bool isChinaIp = false;
            switch (context.Connection.RemoteIpAddress.AddressFamily)
            {
                case System.Net.Sockets.AddressFamily.InterNetwork:
                    isChinaIp = IsChinaIpAddress.VerifyIPv4(context.Connection.RemoteIpAddress.ToString());
                    break;
                case System.Net.Sockets.AddressFamily.InterNetworkV6:
                    isChinaIp = IsChinaIpAddress.VerifyIPv6(context.Connection.RemoteIpAddress.ToString());
                    break;
            }
            return isChinaIp;
        }
    }
}
