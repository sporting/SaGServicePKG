using SaGUtil.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Utils
{
    public class AppSettings
    {
        public const string SCHEMA_NAME = "gross";
        public static string MyDBConnectionString
        {
            get
            {
                return SaAppSettings.GetConnectionSettings("db").ConnectionString;              
            }
        }

        public static string MyDBProviderName
        {
            get
            {
                return SaAppSettings.GetConnectionSettings("db").ProviderName;
            }
        }
    }
}
