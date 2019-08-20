using System;
using System.Threading.Tasks;
using HeXuShi.Extensions;
namespace TestChinaIp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Test();
        }
        static void Test()
        {
            IsChinaIp.Setup();//Setup can be called multiple times, but only on the first initialization
            Console.WriteLine("ipv4 test:" + IsChinaIp.VerifyIPv4("183.192.62.65"));
            Console.WriteLine("ipv6 test:" + IsChinaIp.VerifyIPv6("2400:da00::6666"));
        }
    }
}
