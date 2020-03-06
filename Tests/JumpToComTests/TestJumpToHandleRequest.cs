using HeXuShi.Extensions.Middleware;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Net;

namespace JumpToComTests
{
    [TestFixture]
    public class TestJumpToHandleRequest
    {
        private HttpContext GetHttpContext(string host)
        {
            var context = new DefaultHttpContext();
             context.Connection.RemoteIpAddress = IPAddress.Parse("183.192.62.65");
            context.Request.Host = new HostString(host);
            return context;
        }
        [Test]
        public void JumpToComTest()
        {
            HandleRequest request = new HandleRequest(".com");
            var resultFirst = request.Handle(GetHttpContext("baidu.cn"));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "baidu.com");

            var resultSecond = request.Handle(GetHttpContext("baidu.com"));
            Assert.IsTrue(!resultSecond.Item1);

            request = new HandleRequest(".ai");
            var resultFourth = request.Handle(GetHttpContext("baidu.cn"));
            Assert.IsTrue(resultFourth.Item1);
            Assert.AreEqual(resultFourth.Item2, "baidu.ai");
        }
    }
}
