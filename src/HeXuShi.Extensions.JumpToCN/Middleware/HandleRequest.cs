using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeXuShi.Extensions.Middleware
{
    internal class HandleRequest
    {
        private JumpOption _option;
        private string _first;
        private string _second;
        public HandleRequest(
            string first,
            string second,
            JumpOption option)
        {
            _option = option;
            _first = first;
            _second = second;
        }
        public Tuple<bool, string> Handle(HttpContext context)
        {
            switch(_option)
            {
                case JumpOption.OnlyTo_SpecSuffix:
                    return OnlyTo_SpecSuffix(context);
                case JumpOption.OnlyTo_SpecDomain:
                    return OnlyTo_SpecDomain(context);
                case JumpOption.SuffixTo_SpecSuffix:
                    return SuffixTo_SpecSuffix(context);
                case JumpOption.DomainTo_SpecDomain:
                    return DomainTo_SpecDomain(context);
                default:
                    break;
            }
            throw new InvalidOperationException("jump to cn error handle request range");
        }
        private Tuple<bool, string> OnlyTo_SpecSuffix(HttpContext context)
        {
            var result = new Tuple<bool, string>(false, string.Empty);
            var index = context.Request.Host.Value.LastIndexOf(".");
            if (index == -1 || context.Connection.RemoteIpAddress.ToString().Length < 6)
                return result;

            var suffix = context.Request.Host.Value.Substring(index);
            if (suffix == _first)
                return result;

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
                        return result;
                }
                if (isChinaIp)
                {
                    var domain = context.Request.Host.Value;
                    domain = domain.Remove(index, domain.Length - index);
                    domain += _first;
                    return new Tuple<bool, string>(true, domain);
                }
                else
                    return result;
            }
            catch
            {
                return result;
            }
        }
        public Tuple<bool, string> OnlyTo_SpecDomain(HttpContext context)
        {
            var result = new Tuple<bool, string>(false, string.Empty);
            if (context.Request.Host.Value  == _first)
                return result;

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
                        return result;
                }
                if (isChinaIp)
                {
                    return new Tuple<bool, string>(true, _first);
                }
                else
                    return result;
            }
            catch
            {
                return result;
            }
        }
        public Tuple<bool, string> SuffixTo_SpecSuffix(HttpContext context)
        {
            var result = new Tuple<bool, string>(false, string.Empty);
            var index = context.Request.Host.Value.LastIndexOf(".");
            if (index == -1 || context.Connection.RemoteIpAddress.ToString().Length < 6)
                return result;

            var suffix = context.Request.Host.Value.Substring(index);

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
                        return result;
                }
                if (suffix != _first && isChinaIp)
                {
                    var domain = context.Request.Host.Value;
                    domain = domain.Remove(index, domain.Length - index);
                    domain += _first;
                    return new Tuple<bool, string>(true, domain);
                }
                else if (suffix != _second && !isChinaIp)
                {
                    var domain = context.Request.Host.Value;
                    domain = domain.Remove(index, domain.Length - index);
                    domain += _second;
                    return new Tuple<bool, string>(true, domain);
                }
                else
                    return result;
            }
            catch
            {
                return result;
            }
        }
        public Tuple<bool, string> DomainTo_SpecDomain(HttpContext context)
        {
            var result = new Tuple<bool, string>(false, string.Empty);

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
                        return result;
                }
                if (context.Request.Host.Value != _first && isChinaIp)
                {
                    return new Tuple<bool, string>(true, _first);
                }
                else if (context.Request.Host.Value != _second && !isChinaIp)
                {
                    return new Tuple<bool, string>(true, _second);
                }
                else
                    return result;
            }
            catch
            {
                return result;
            }
        }
    }
}
