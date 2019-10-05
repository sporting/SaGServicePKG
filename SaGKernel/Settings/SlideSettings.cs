using SaGKernel.MajorClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaGKernel.Settings
{
   public class SlideSettings
    {
        public string LayoutName;
        public string PrinterName;
        public int SlotId=0; //卡匣1
        public int SlotId2=1; //卡匣2
        public string outputFolder;
        public MajorSlideSettingsCollection MajorSlides;
        public void InitialIfNull()
        {
            if (MajorSlides == null)
            {
                MajorSlides = new MajorSlideSettingsCollection();
            }
            if (MajorSlides.Count <= 0)
            {
                IMajorClass mcIns = MajorClassify.GetInstance().GetDefaultMajorClass();
                MajorSlideSettings mss = new MajorSlideSettings();
                mss.MajorClassName = mcIns.ClassName;
                mss.SlideMagazineName = SlideSettingsApi.GetDefaultSlideMagazineName();
                MajorSlides.Add(mss);
            }

            if (string.IsNullOrEmpty(LayoutName))
            {
                LayoutName = "LayoutSample";
            }

            if (string.IsNullOrEmpty(PrinterName))
            {
                PrinterName = "PRN0";
            }

            SlotId = 0;
            SlotId2 = 1;
            //if (string.IsNullOrEmpty(SlotId.ToString()))
            //{
            //    SlotId = 0;
            //}
            //if (string.IsNullOrEmpty(SlotId2.ToString()))
            //{
            //    SlotId2 = 0;
            //}
            if (string.IsNullOrEmpty(outputFolder))
            {
                outputFolder = "C:\\";
            }
        }
    }
}
