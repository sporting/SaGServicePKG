using System.Configuration;
using SaGUtil.Configuration;

namespace SaGService.Utils
{
    public class AppSettings
    {
        public static string DefaultKey = "default";
        public static string TokenSecretKey()
        {
            return SaAppSettings.GetAppSetting<string>("TokenSecretKey");
        }
    }
}