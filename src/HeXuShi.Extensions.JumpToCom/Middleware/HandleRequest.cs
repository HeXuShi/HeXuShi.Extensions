using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeXuShi.Extensions.Middleware
{
    public class HandleRequest
    {
        private string _newSuffix;
        public HandleRequest(string domain)
        {
            _newSuffix = domain;
        }
        public Tuple<bool, string> Handle(HttpContext context)
        {
            var result = new Tuple<bool, string>(false, string.Empty);
            var index = context.Request.Host.Value.LastIndexOf(".");
            if (index == -1 || context.Connection.RemoteIpAddress.ToString().Length < 6)
                return result;

            var currentSuffix = context.Request.Host.Value.Substring(index);
            if (currentSuffix == _newSuffix)
                return result;

            try
            {
                    var domain = context.Request.Host.Value;
                    domain = domain.Remove(index, domain.Length - index);
                    domain += _newSuffix;
                    return new Tuple<bool, string>(true, domain);
            }
            catch
            {
                return result;
            }
        }
    }
}
