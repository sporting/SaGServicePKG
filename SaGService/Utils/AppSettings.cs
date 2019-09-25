using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SaGService.Utils
{
    public class AppSettings
    {
        public static string DefaultKey = "default";
        public static string TokenSecretKey()
        {
            return ConfigurationManager.AppSettings["TokenSecretKey"].ToString();
        }
    }
}