using HeXuShi.Extensions.Middleware;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Net;

namespace BlockChinaTests
{
    [TestFixture]
    public class TestBlockChinaHandleRequest
    {
        private HttpContext GetHttpContext(string IpAddress)
        {
            var context = new DefaultHttpContext();
             context.Connection.RemoteIpAddress = IPAddress.Parse(IpAddress);
            return context;
        }
        [Test]
        public void JumpToComTest()
        {
            HandleRequest request = new HandleRequest();
            var resultFirst = request.Handle(GetHttpContext("183.192.62.65"));
            Assert.IsTrue(resultFirst);

            var resultSecond = request.Handle(GetHttpContext("63.211.153.96"));
            Assert.IsTrue(!resultSecond);

            request = new HandleRequest();
            var resultFourth = request.Handle(GetHttpContext("183.192.62.60"));
            Assert.IsTrue(resultFourth);
        }
    }
}
