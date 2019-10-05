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
            int count = 50;
            while (count-- != 0)
            {
                var startTime = DateTime.UtcNow;
                IsChinaIpAddress.Setup();//Setup can be called multiple times, but only on the first initialization
                Console.WriteLine(count + ") ipv4 test:" + IsChinaIpAddress.VerifyIPv4("183.192.62.65"));
                Console.WriteLine("runTime:" + (DateTime.UtcNow - startTime));
                startTime = DateTime.UtcNow;
                Console.WriteLine(count + ") ipv6 test:" + IsChinaIpAddress.VerifyIPv6("2400:da00::6666"));
                Console.WriteLine("runTime:" + (DateTime.UtcNow - startTime));
            }
        }
    }
}
