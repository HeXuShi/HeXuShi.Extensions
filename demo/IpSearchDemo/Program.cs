using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace IpSearchDemo
{
    public class IpRange
    {
        public string Min { get; set; }
        public string Max { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ReadDataBase().Wait();
        }
        static async Task ReadDataBase()
        {
            const string ipv4File = "ipv4.cn.zone.txt";
            const string ipv6File = "ipv6.cn.zone.txt";
            string ipv4Text;
            string ipv6Text;
            using (var file = File.OpenText(ipv4File))
            {
                ipv4Text = await file.ReadToEndAsync();
            }
            using (var file = File.OpenText(ipv6File))
            {
                ipv6Text = await file.ReadToEndAsync();
            }
            readIpv4Range(ipv4Text);
            readIpv6Range(ipv6Text);
        }
        static void readIpv4Range(string text)
        {
            List<IpRange> ipRanges = new List<IpRange>();
            var table = text.Split('\n');
            foreach(var item in table)
            {
                if (item.Length < 5)
                    continue;
                var keyVal = item.Split('/');
                if (keyVal.Length != 2)
                    throw new InvalidOperationException("ipv4 error ip range");
                var ipRange = new IpRange
                {
                    Min = keyVal[0]
                };
                var maxIp = keyVal[0];
                var index = maxIp.LastIndexOf(".");
                maxIp = maxIp.Remove(index, maxIp.Length - index);
                ipRange.Max = maxIp + "." + keyVal[1];
            }
        }
        static void readIpv6Range(string text)
        {
            List<IpRange> ipRanges = new List<IpRange>();
        }
    }
}
