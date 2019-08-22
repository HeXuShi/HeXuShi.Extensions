using NUnit.Framework;
using System;
using HeXuShi.Extensions.Middleware;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace JumpToCNTests
{
    [TestFixture]
    public class TestJumpToHandleRequest
    {
        private HttpContext GetHttpContext(string host, bool isChinaIp = true, bool isIpv6 = false)
        {
            var context = new DefaultHttpContext();
            if (isChinaIp)
            {
                if (!isIpv6)
                    context.Connection.RemoteIpAddress = IPAddress.Parse("183.192.62.65");
                else
                    context.Connection.RemoteIpAddress = IPAddress.Parse("2400:da00::6666");
            }
            if (!isChinaIp)
            {
                if (!isIpv6)
                    context.Connection.RemoteIpAddress = IPAddress.Parse("8.8.8.8");
                else
                    context.Connection.RemoteIpAddress = IPAddress.Parse("2001:4860:4860::8888");
            }
            context.Request.Host = new HostString(host);
            return context;
        }

        [Test]
        public void OnlyTo_SpecSuffix_ipv4()
        {
            HandleRequest request = new HandleRequest(".cn", string.Empty, HeXuShi.Extensions.JumpOption.OnlyTo_SpecSuffix);
            var resultFirst = request.Handle(GetHttpContext("baidu.com"));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "baidu.cn");

            var resultSecond = request.Handle(GetHttpContext("baidu.cn"));
            Assert.IsTrue(!resultSecond.Item1);

            var resultThird = request.Handle(GetHttpContext("baidu.com", false));
            Assert.IsTrue(!resultThird.Item1);

            var resultFourth = request.Handle(GetHttpContext("baidu.cn", false));
            Assert.IsTrue(!resultFourth.Item1);
        }
        [Test]
        public void OnlyTo_SpecSuffix_ipv6()
        {
            HandleRequest request = new HandleRequest(".cn", string.Empty, HeXuShi.Extensions.JumpOption.OnlyTo_SpecSuffix);
            var resultFirst = request.Handle(GetHttpContext("baidu.com", true, true));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "baidu.cn");

            var resultSecond = request.Handle(GetHttpContext("baidu.cn", true, true));
            Assert.IsTrue(!resultSecond.Item1);

            var resultThird = request.Handle(GetHttpContext("baidu.com", false, true));
            Assert.IsTrue(!resultThird.Item1);

            var resultFourth = request.Handle(GetHttpContext("baidu.cn", false, true));
            Assert.IsTrue(!resultFourth.Item1);
        }
        [Test]
        public void OnlyTo_SpecDomain_ipv4()
        {
            HandleRequest request = new HandleRequest("asp.net", string.Empty, HeXuShi.Extensions.JumpOption.OnlyTo_SpecDomain);
            var resultFirst = request.Handle(GetHttpContext("github.com"));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "asp.net");

            var resultSecond = request.Handle(GetHttpContext("asp.net"));
            Assert.IsTrue(!resultSecond.Item1);

            var resultThird = request.Handle(GetHttpContext("github.com", false));
            Assert.IsTrue(!resultThird.Item1);

            var resultFourth = request.Handle(GetHttpContext("asp.net", false));
            Assert.IsTrue(!resultFourth.Item1);
        }
        [Test]
        public void OnlyTo_SpecDomain_ipv6()
        {
            HandleRequest request = new HandleRequest("asp.net", string.Empty, HeXuShi.Extensions.JumpOption.OnlyTo_SpecDomain);
            var resultFirst = request.Handle(GetHttpContext("github.com", true, true));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "asp.net");

            var resultSecond = request.Handle(GetHttpContext("asp.net", true, true));
            Assert.IsTrue(!resultSecond.Item1);

            var resultThird = request.Handle(GetHttpContext("github.com", false, true));
            Assert.IsTrue(!resultThird.Item1);

            var resultFourth = request.Handle(GetHttpContext("asp.net", false, true));
            Assert.IsTrue(!resultFourth.Item1);
        }
        [Test]
        public void SuffixTo_SpecSuffix_ipv4()
        {
            HandleRequest request = new HandleRequest(".net", ".io", HeXuShi.Extensions.JumpOption.SuffixTo_SpecSuffix);
            var resultFirst = request.Handle(GetHttpContext("github.com"));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "github.net");

            var resultSecond = request.Handle(GetHttpContext("asp.net"));
            Assert.IsTrue(!resultSecond.Item1);

            var resultThird = request.Handle(GetHttpContext("github.io", false));
            Assert.IsTrue(!resultThird.Item1);

            var resultFourth = request.Handle(GetHttpContext("asp.net", false));
            Assert.IsTrue(resultFourth.Item1);
            Assert.AreEqual(resultFourth.Item2, "asp.io");
        }
        [Test]
        public void SuffixTo_SpecSuffix_ipv6()
        {
            HandleRequest request = new HandleRequest(".net", ".io", HeXuShi.Extensions.JumpOption.SuffixTo_SpecSuffix);
            var resultFirst = request.Handle(GetHttpContext("github.com", true, true));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "github.net");

            var resultSecond = request.Handle(GetHttpContext("asp.net", true, true));
            Assert.IsTrue(!resultSecond.Item1);

            var resultThird = request.Handle(GetHttpContext("github.io", false, true));
            Assert.IsTrue(!resultThird.Item1);

            var resultFourth = request.Handle(GetHttpContext("asp.net", false, true));
            Assert.IsTrue(resultFourth.Item1);
            Assert.AreEqual(resultFourth.Item2, "asp.io");
        }
        [Test]
        public void DomainTo_SpecDomain_ipv4()
        {
            HandleRequest request = new HandleRequest("asp.net", "github.com", HeXuShi.Extensions.JumpOption.DomainTo_SpecDomain);
            var resultFirst = request.Handle(GetHttpContext("github.com"));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "asp.net");

            var resultSecond = request.Handle(GetHttpContext("asp.net"));
            Assert.IsTrue(!resultSecond.Item1);

            var resultThird = request.Handle(GetHttpContext("github.com", false));
            Assert.IsTrue(!resultThird.Item1);

            var resultFourth = request.Handle(GetHttpContext("asp.net", false));
            Assert.IsTrue(resultFourth.Item1);
            Assert.AreEqual(resultFourth.Item2, "github.com");
        }
        [Test]
        public void DomainTo_SpecDomain_ipv6()
        {
            HandleRequest request = new HandleRequest("asp.net", "github.com", HeXuShi.Extensions.JumpOption.DomainTo_SpecDomain);
            var resultFirst = request.Handle(GetHttpContext("github.com", true, true));
            Assert.IsTrue(resultFirst.Item1);
            Assert.AreEqual(resultFirst.Item2, "asp.net");

            var resultSecond = request.Handle(GetHttpContext("asp.net", true, true));
            Assert.IsTrue(!resultSecond.Item1);

            var resultThird = request.Handle(GetHttpContext("github.com", false, true));
            Assert.IsTrue(!resultThird.Item1);

            var resultFourth = request.Handle(GetHttpContext("asp.net", false, true));
            Assert.IsTrue(resultFourth.Item1);
            Assert.AreEqual(resultFourth.Item2, "github.com");
        }
    }
}
