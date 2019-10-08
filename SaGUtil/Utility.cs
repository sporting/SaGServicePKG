using System;
using System.Collections.Generic;
using System.Configuration;
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

        public static T GetAppSetting<T>(string key, T defaultValue)
        {
            object val = ConfigurationManager.AppSettings[key];
            if (val == null)
            {
                return defaultValue;
            }

            return (T)val;
        }

        public static T[] GetAppSettings<T>(string section)
        {
            object v= ConfigurationManager.GetSection(section);
            return null;
        }
    }
}
