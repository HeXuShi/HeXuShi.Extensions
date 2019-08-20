using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
/*
compare IP address range in C#?
https://stackoverflow.com/questions/10525531/how-to-compare-ip-address-range-in-c

download link:
https://github.com/HeXuShi/HeXuShi.Extensions/tree/master/database
 */
namespace IpSearchDemo
{
    public class IpRange
    {
        public string Address { get; set; }
        public int PrefixLength { get; set; }
    }
    class Program
    {
        static List<IpRange> ipv4Ranges = new List<IpRange>();
        static List<IpRange> ipv6Ranges = new List<IpRange>();
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
            await SaveIpRange(ipv4Ranges, "ipv4.cn.zone.json");
            await SaveIpRange(ipv6Ranges, "ipv6.cn.zone.json");
            ipv4Ranges = await ReadIpRange("ipv4.cn.zone.json");
            ipv6Ranges = await ReadIpRange("ipv6.cn.zone.json");
            var startTime = DateTime.UtcNow;
            Console.WriteLine("is china ipv4 ip ?" + TestIpv4(ipv4Ranges));
            Console.WriteLine("runTime:" + (DateTime.UtcNow - startTime));
            startTime = DateTime.UtcNow;
            Console.WriteLine("is china ipv6 ip ?" + TestIpv6(ipv6Ranges));
            Console.WriteLine("runTime:" + (DateTime.UtcNow - startTime));


        }
        static async Task SaveIpRange(List<IpRange> ipRanges, string path)
        {
            string text = JsonConvert.SerializeObject(ipRanges);
            using (var file = File.CreateText(path))
            {
                await file.WriteAsync(text);
            }
        }
        static async Task<List<IpRange>> ReadIpRange(string path)
        {
            string text;
            using (var file = File.OpenText(path))
            {
                text = await file.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<List<IpRange>>(text);
        }
        static bool TestIpv4(List<IpRange> ipRanges)
        {
            string address = "183.192.62.65";
            var addressByte = IPAddress.Parse(address).GetAddressBytes();
            foreach (var item in ipRanges)
            {
                var rangeByte = IPAddress.Parse(item.Address).GetAddressBytes();
                if (CompareIp(rangeByte, item.PrefixLength, addressByte))
                    return true;
            }
            return false;
        }
        static bool TestIpv6(List<IpRange> ipRanges)
        {
            string address = "2400:da00::6666";
            var addressByte = IPAddress.Parse(address).GetAddressBytes();
            foreach (var item in ipRanges)
            {
                var rangeByte = IPAddress.Parse(item.Address).GetAddressBytes();
                if (CompareIp(rangeByte, item.PrefixLength, addressByte))
                    return true;
            }
            return false;
        }
        public static bool CompareIp(byte[] addressRange, int prefixLength, byte[] address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            if (address.Length != addressRange.Length)
                return false; // IPv4/IPv6 mismatch

            int index = 0;
            int bits = prefixLength;

            for (; bits >= 8; bits -= 8)
            {
                if (address[index] != addressRange[index])
                    return false;
                ++index;
            }

            if (bits > 0)
            {
                int mask = (byte)~(255 >> bits);
                if ((address[index] & mask) != (addressRange[index] & mask))
                    return false;
            }
            return true;
        }
        static void readIpv4Range(string text)
        {
            var table = text.Split('\n');
            foreach (var item in table)
            {
                if (item.Length < 5)
                    continue;
                var keyVal = item.Split('/');
                if (keyVal.Length != 2)
                    throw new InvalidOperationException("ipv4 error ip range");
                var ipRange = new IpRange
                {
                    Address = keyVal[0],
                    PrefixLength = Convert.ToInt32(keyVal[1])
                };
                ipv4Ranges.Add(ipRange);
            }
        }
        static void readIpv6Range(string text)
        {
            var table = text.Split('\n');
            foreach (var item in table)
            {
                if (item.Length < 5)
                    continue;
                var keyVal = item.Split('/');
                if (keyVal.Length != 2)
                    throw new InvalidOperationException("ipv6 error ip range");
                var ipRange = new IpRange
                {
                    Address = keyVal[0],
                    PrefixLength = Convert.ToInt32(keyVal[1])
                };
                ipv6Ranges.Add(ipRange);
            }
        }
    }
}
