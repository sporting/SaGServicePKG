using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.System
{
    public class SaDate
    {
        public static DateTime Today()
        {
            return DateTime.Now;
        }

        public static string TodayYMD()
        {
            return Today().ToString("yyyyMMdd");
        }

        public static string TodayHMS()
        {
            return Today().ToString("HHmmss");
        }

        public static string TodayY()
        {
            return Today().ToString("yyyy");
        }
    }
}
