using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil
{
    public class Utility
    {
        public static DateTime Today()
        {
            return DateTime.Now;
        }

        public static string TodayYMD()
        {
            return Today().ToString("yyyyMMdd");
        }
    }
}
