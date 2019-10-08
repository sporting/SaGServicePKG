using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Config
{
    public class MajorModelConfig
    {
        public static string[] MajorModelDll()
        {
            try
            {
                var config = (MajorModelSection)ConfigurationManager.GetSection("MajorModelSection");

                return (from MajorModelElement model in config.MajorModels
                        select model.Value).ToArray<string>();
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error("MajorModelConfig.MajorModelDll", ex.Message);
                return new string[] { };
            }
        }
    }
}
