using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SaGBridge.Utils
{
    public class AppSettings
    {
        public static string SaGServiceUrl()
        {
            return ConfigurationManager.AppSettings["SaGServiceUrl"].ToString();
        }
    }
}
