using SaGUtil.Configuration;
using SaGUtil.System;
using SaGUtil.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Config
{
    public class CustomModelConfig
    {
        public static string[] CustomModelDll()
        {
            try
            {
                var config = SaAppSettings.GetAppSection<CustomModelSection>("CustomModelSection");
                if (config == null)
                {
                    MyLog.Info(typeof(CustomModelConfig), "CustomModelSection not load");
                    return new string[] { };
                }

                return (from CustomModelElement model in config.CustomModels
                        select model.Value).ToArray<string>();
            }
            catch (Exception ex)
            {
                MyLog.Fatal(typeof(CustomModelConfig), ex.Message);
                return new string[] { };
            }
        }
    }
}
