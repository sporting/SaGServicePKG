using SaGKernel.MajorClass;
using SaGKernel.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Settings
{
    /// <summary>
    /// 包埋盒列印卡匣取得的 API
    /// </summary>
    public class CassetteSettingsApi
    {
        //private static string CM1 = "卡匣1";
        //private static string CM2 = "卡匣2";
        //private static string CM3 = "卡匣3";
        //private static string CM4 = "卡匣4";
        //private static string CM5 = "卡匣5";
        //private static string CM6 = "卡匣6";
        //private static string CM7 = "卡匣7";
        //private static string CM8 = "卡匣8";
        //public static string[] CassetteMagazineNames = { CM1,CM2,CM3,CM4,CM5,CM6,CM7,CM8};

        //public static string GetDefaultCassetteMagazineName() {
        //    return CassetteMagazineNames[0];
        //}
        //public static string CassetteValue(CassetteSettings settings, string magazineName)
        //{
        //    if (magazineName.Equals(CM1))
        //    {
        //        return settings.CassetteName;
        //    }
        //    else if (magazineName.Equals(CM2))
        //    {
        //        return settings.CassetteName2;
        //    }
        //    else if (magazineName.Equals(CM3))
        //    {
        //        return settings.CassetteName3;
        //    }
        //    else if (magazineName.Equals(CM4))
        //    {
        //        return settings.CassetteName4;
        //    }
        //    else if (magazineName.Equals(CM5))
        //    {
        //        return settings.CassetteName5;
        //    }
        //    else if (magazineName.Equals(CM6))
        //    {
        //        return settings.CassetteName6;
        //    }
        //    else if (magazineName.Equals(CM7))
        //    {
        //        return settings.CassetteName7;
        //    }
        //    else if (magazineName.Equals(CM8))
        //    {
        //        return settings.CassetteName8;
        //    }

        //    return settings.CassetteName;
        //}

        //public static string CassetteValue(CassetteSettings settings, IMajorClass currentMajorClass)
        //{         
        //    var v = (from MajorCassetteSettings mcs in settings.MajorCassettes
        //            where mcs.MajorClassName == currentMajorClass.ClassName
        //             select mcs.CassetteMagazineName);

        //    string magazineName = settings.CassetteName;

        //    if (v.Count() > 0)
        //    {
        //        magazineName = v.First();
        //    }

        //    return CassetteValue(settings, magazineName);            
        //}

        //public static string CassetteMazagineName(CassetteSettings settings, IMajorClass currentMajorClass)
        //{
        //    var v = (from MajorCassetteSettings mcs in settings.MajorCassettes
        //             where mcs.MajorClassName == currentMajorClass.ClassName
        //             select mcs.CassetteMagazineName);

        //    if (v.Count() > 0)
        //    {
        //        return v.First();
        //    }

        //    return CM1;
        //}

            /// <summary>
            /// 依照檢體類別取得對應的檢體類別對應的卡匣名稱
            /// </summary>
            /// <param name="settings"></param>
            /// <param name="currentMajorClass"></param>
            /// <returns></returns>
        public static string CassetteMazagineName(CassetteSettings settings, IMajorClass currentMajorClass)
        {
            var v = (from MajorCassetteSettings mcs in settings.MajorCassettes
                     where mcs.MajorClassName == currentMajorClass.ClassName
                     select mcs.CassetteMagazineName);

            if (v.Count() > 0)
            {
                return v.First();
            }

            return CassetteColors.GetInstance().GetDefaultName();
        }
    }
}
