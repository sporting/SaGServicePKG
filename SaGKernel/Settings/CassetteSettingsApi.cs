using SaGKernel.MajorClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Settings
{
    public class CassetteSettingsApi
    {
        public static string[] CassetteMagazineNames = { "卡匣1", "卡匣2", "卡匣3", "卡匣4", "卡匣5", "卡匣6", "卡匣7", "卡匣8" };

        public static string GetDefaultCassetteMagazineName() {
            return CassetteMagazineNames[0];
        }
        public static string CassetteValue(CassetteSettings settings, IMajorClass currentMajorClass)
        {         
            string magazineName = (from MajorCassetteSettings mcs in settings.MajorCassettes
                    where mcs.MajorClassName == currentMajorClass.ClassName
                     select mcs.CassetteMagazineName).First();            

            if (magazineName.Equals("卡匣1"))
            {
                return settings.CassetteName;
            }
            else if (magazineName.Equals("卡匣2"))
            {
                return settings.CassetteName2;
            }
            else if (magazineName.Equals("卡匣3"))
            {
                return settings.CassetteName3;
            }
            else if (magazineName.Equals("卡匣4"))
            {
                return settings.CassetteName4;
            }
            else if (magazineName.Equals("卡匣5"))
            {
                return settings.CassetteName5;
            }
            else if (magazineName.Equals("卡匣6"))
            {
                return settings.CassetteName6;
            }
            else if (magazineName.Equals("卡匣7"))
            {
                return settings.CassetteName7;
            }
            else if (magazineName.Equals("卡匣8"))
            {
                return settings.CassetteName8;
            }

            return string.Empty;
        }
    }
}
