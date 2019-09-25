using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PPMSXLib
{
    public class DataSection{
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
    public class PPMSXData
    {
        public string LayoutName { get; set; }
        public string PrinterName { get; set; }
        public int SlotId { get; set; }   //玻片槽號碼 0,1,2 ?
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

        public void AddData(string layoutName, string printerName, int slotId, DataSection ds)
        {           
                LayoutName = layoutName;
                PrinterName = printerName;
                SlotId = slotId;
                SeqPrintFlag = false;
                Sequence = 0;
                SubSeqPrintFlag = false;
                SubSeqStart = 1;
                SubSeqEnd = 1;
                Val0 = ds.Val0;
                Val1 = ds.Val1;
                Val2 = ds.Val2;
                Val3 = ds.Val3;
                Val4 = ds.Val4;
                Val5 = ds.Val5;
                Val6 = ds.Val6;
                Val7 = ds.Val7;
                Val8 = ds.Val8;
                Val9 = ds.Val9;
        }

        public bool SaveFile(string outputFolder)
        {
            string fileName = GetFileName(outputFolder);

            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter swWriter = new StreamWriter(fs, Encoding.GetEncoding(932))) //932 : Shift_JIS
                    {
                        string[] ss = new string[] { LayoutName, PrinterName, SlotId.ToString(), SeqPrintFlag ? "1" : "0", Sequence.ToString(), SubSeqPrintFlag ? "1" : "0", SubSeqStart.ToString(), SubSeqEnd.ToString(), Val0, Val1, Val2, Val3, Val4, Val5, Val6, Val7, Val8, Val9 };

                        swWriter.WriteLine(string.Join(",", ss));
                        //swWriter.Write(string.Join(",", ss));

                        swWriter.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error("PPMSXData", $"{fileName} SaveFile failed: {ex.Message}");
                //Console.WriteLine(string.Concat("PPMSXData.SaveFile",ex.Message));
                return false;
            }
        }

        private string GetFileName(string outputFolder)
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            if (Directory.Exists(outputFolder))
            {
                string uniqueFileName = string.Format(@"{0}.CSV", DateTime.Now.Ticks);
                string outputFileName = Path.Combine(outputFolder, uniqueFileName);

                return outputFileName;
            }

                //Console.WriteLine(string.Concat(outputFolder, " Directory not found"));
                LogMan.Instance.Error("PPMSXData", $"{outputFolder} Directory not found");
                return string.Empty;
        }
    }
}
