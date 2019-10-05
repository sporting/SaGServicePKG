using SaGKernel.MajorClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Settings
{
    public class SlideSettingsApi
    {
        public static string[] SlideMagazineNames = { "卡匣1", "卡匣2"};

        public static string GetDefaultSlideMagazineName() {
            return SlideMagazineNames[0];
        }
        public static int SlideValue(SlideSettings settings, IMajorClass currentMajorClass)
        {         
            string magazineName = (from MajorSlideSettings mcs in settings.MajorSlides
                    where mcs.MajorClassName == currentMajorClass.ClassName
                     select mcs.SlideMagazineName).First();            

            if (magazineName.Equals("卡匣1"))
            {
                return settings.SlotId;
            }
            else if (magazineName.Equals("卡匣2"))
            {
                return settings.SlotId2;
            }

            return 0;
        }
    }
}
