using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.Configuration
{
   public class SaAppSettings
    {
        public static T GetAppSetting<T>(string key, T defaultValue)
        {
            object val = ConfigurationManager.AppSettings[key];
            if (val == null)
            {
                return defaultValue;
            }

            return (T)val;
        }
        public static T GetAppSetting<T>(string key)
        {
            object val = ConfigurationManager.AppSettings[key];
            if (val == null)
            {
                return default(T);
            }

            return (T)val;
        }
        public static T GetAppSection<T>(string section)
        {
            object val = ConfigurationManager.GetSection(section);
            if (val == null)
            {
                return default(T);
            }
            return (T)val;
        }

        public static ConnectionStringSettings GetConnectionSettings(string name)
        {
            return ConfigurationManager.ConnectionStrings[name];
        }
    }
}
