using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace IpSearchDemo
{
    public class IPAdressRange
    {
        public string Min { get; set; }
        public string Max { get; set; }
    }
    public class IpAddressByte
    {
        public ulong Min { get; set; }
        public ulong Max { get; set; }
    }
    class Program
    {
        static List<IpAddressByte> ipv4Ranges = new List<IpAddressByte>();
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
            Console.WriteLine("is china ip ?" + TestIpv4());
        }
        static bool TestIpv4()
        {
            string testIpv4Address = "183.192.62.65";
            var addressByte = Ipv4ToByte(testIpv4Address);
            foreach(var item in ipv4Ranges)
            {
                if (addressByte >= item.Min && addressByte <= item.Max)
                    return true;
            }
            return false;
        }
        static ulong Ipv4ToByte(string ip)
        {
            return BitConverter.ToUInt32(IPAddress.Parse(ip).GetAddressBytes().Reverse().ToArray(), 0);
        }
        static void readIpv4Range(string text)
        {
            List<IPAdressRange> ipRanges = new List<IPAdressRange>();
            var table = text.Split('\n');
            foreach (var item in table)
            {
                if (item.Length < 5)
                    continue;
                var keyVal = item.Split('/');
                if (keyVal.Length != 2)
                    throw new InvalidOperationException("ipv4 error ip range");
                var ipRange = new IPAdressRange
                {
                    Min = keyVal[0]
                };
                var maxIp = keyVal[0];
                var index = maxIp.LastIndexOf(".");
                maxIp = maxIp.Remove(index, maxIp.Length - index);
                ipRange.Max = maxIp + "." + keyVal[1];
                ipRanges.Add(ipRange);
            }
            foreach(var item in ipRanges)
            {
                var rangeBytes = new IpAddressByte();
                rangeBytes.Min = Ipv4ToByte(item.Min);
                rangeBytes.Max = Ipv4ToByte(item.Max);
                ipv4Ranges.Add(rangeBytes);
            }
        }
        static void readIpv6Range(string text)
        {
            List<IPAdressRange> ipRanges = new List<IPAdressRange>();
        }
    }
}
