using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMSXLib.Format
{
    public class SlideEnvironment
    {
        public string LayoutName;
        public string PrinterName;
        public int SlotId;
        public SlideEnvironment()
        {

        }

        public SlideEnvironment(string layoutName, string printerName, int slotId):this()
        {
            LayoutName = layoutName;
            PrinterName = printerName;
            SlotId = slotId;
        }
    }
}
