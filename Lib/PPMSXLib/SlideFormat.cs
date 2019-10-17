using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMSXLib
{
    /// <summary>
    /// Slide Printer
    /// PPMSX 每一個 Slide 對應欄位文字輸出
    /// </summary>
    public abstract class SlideFormat
    {
        public string LayoutName { get; set; }
        public string PrinterName { get; set; }
        public int SlotId { get; set; }   //玻片槽號碼 0,1
        public bool SeqPrintFlag { get; set; }//序列號是否列印
        public int Sequence { get; set; } //序列號
        public bool SubSeqPrintFlag { get; set; } //次編號是否列印
        public int SubSeqStart { get; set; } //次編號開始序號
        public int SubSeqEnd { get; set; } //次編號結束序號
        public string Val0 { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Val3 { get; set; }
        public string Val4 { get; set; }
        public string Val5 { get; set; }
        public string Val6 { get; set; }
        public string Val7 { get; set; }
        public string Val8 { get; set; }
        public string Val9 { get; set; }    
    }
}
