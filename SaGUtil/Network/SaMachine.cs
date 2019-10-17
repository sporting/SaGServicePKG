using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.Network
{
    public struct MachineInfo
    {
        public string IP;
        public string MachineName;
    }
    public class SaMachine
    {
        public static MachineInfo Get()
        {
            // 取得本機名稱
            string strHostName = Dns.GetHostName();

            // 取得本機的IpHostEntry類別實體，MSDN建議新的用法
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            // 取得所有 IP 位址
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                // 只取得IP V4的Address
                if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return new MachineInfo() { IP = ipaddress.ToString(), MachineName = strHostName };
                }
            }

            return new MachineInfo() { };
        }
    }
}
