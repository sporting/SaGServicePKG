using SaGKernel.MajorClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Settings
{
    /// <summary>
    /// 玻片印製機輸出 Slot 取得的 API
    /// </summary>
    public class SlideSettingsApi
    {
        private static string SMN1 = "卡匣1";
        private static string SMN2 = "卡匣2";

        public static string[] SlideMagazineNames = { SMN1,SMN2};

        public static string GetDefaultSlideMagazineName() {
            return SlideMagazineNames[0];
        }
        public static int SlideMappingValue(SlideSettings settings, IMajorClass currentMajorClass)
        {
            var v = (from MajorSlideSettings mcs in settings.MajorSlides
                     where mcs.MajorClassName == currentMajorClass.ClassName
                     select mcs.SlideMagazineName);
            if (v.Count() > 0)
            {
                string magazineName = v.First();

                if (magazineName.Equals(SMN1))
                {
                    return 0;
                }
                else if (magazineName.Equals(SMN2))
                {
                    return 1;
                }
            }

            return 0;
        }

        public static string SlideName(int slot)
        {
            if (slot == 0)
            {
                return SMN1;
            }
            else if (slot == 1)
            {
                return SMN2;
            }

            return SMN1;
        }

        public static int SlideValue(string name)
        {
            if (name == SMN1)
            {
                return 0;
            }
            else if (name == SMN2)
            {
                return 1;
            }

            return 0;
        }
    }
}
