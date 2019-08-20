using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HeXuShi.Extensions
{

    public class IsChinaIp
    {
        internal class IpRange
        {
            public string Address { get; set; }
            public int PrefixLength { get; set; }
        }
        private static List<IpRange> ipv4Ranges = new List<IpRange>();
        private static List<IpRange> ipv6Ranges = new List<IpRange>();
        private static List<IpRange> ReadIpRange(string text)
        {
            return JsonConvert.DeserializeObject<List<IpRange>>(text);
        }
        public static void Setup()
        {
            var ipv4Text = System.Text.Encoding.UTF8.GetString(HeXuShi.Extensions.Properties.Resources.ipv4_cn_zone);
            var ipv6Text = System.Text.Encoding.UTF8.GetString(HeXuShi.Extensions.Properties.Resources.ipv6_cn_zone);
            ipv4Ranges = ReadIpRange(ipv4Text);
            ipv6Ranges = ReadIpRange(ipv6Text);
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
        public static bool VerifyIPv4(string address)
        {
            var addressByte = IPAddress.Parse(address).GetAddressBytes();
            foreach (var item in ipv4Ranges)
            {
                var rangeByte = IPAddress.Parse(item.Address).GetAddressBytes();
                if (CompareIp(rangeByte, item.PrefixLength, addressByte))
                    return true;
            }
            return false;
        }
        public static bool VerifyIPv6(string address)
        {
            var addressByte = IPAddress.Parse(address).GetAddressBytes();
            foreach (var item in ipv6Ranges)
            {
                var rangeByte = IPAddress.Parse(item.Address).GetAddressBytes();
                if (CompareIp(rangeByte, item.PrefixLength, addressByte))
                    return true;
            }
            return false;
        }
    }
}
