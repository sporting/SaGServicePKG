using PPMSXLib.Format;
using SaGUtil.IO;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PPMSXLib
{
    /// <summary>
    /// Slide Printer
    /// PPMSX 文字檔內容
    /// </summary>
    public class PPMSXData
    {
        List<SlideFormat> _dataSlides = new List<SlideFormat>();
        public void Clear()
        {
            _dataSlides.Clear();
        }


        public void AddSlides(SlideFormat ds)
        {
            _dataSlides.Add(ds);
        }
        public void AddSlides(SlideFormat[] dss)
        {
            _dataSlides.AddRange(dss);
        }

        public bool SaveFile(string outputFolder)
        {
            string fileName = SaFile.GetTicksFileName(outputFolder, "CSV");
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
                {
                    using (StreamWriter swWriter = new StreamWriter(fs, Encoding.GetEncoding(932))) //932 : Shift_JIS
                    {
                        foreach (SlideFormat data in _dataSlides)
                        {
                            string[] ss = new string[] { data.LayoutName, data.PrinterName, data.SlotId.ToString(), data.SeqPrintFlag ? "1" : "0", data.Sequence.ToString(), data.SubSeqPrintFlag ? "1" : "0", data.SubSeqStart.ToString(), data.SubSeqEnd.ToString(), data.Val0, data.Val1, data.Val2, data.Val3, data.Val4, data.Val5, data.Val6, data.Val7, data.Val8, data.Val9 };
                            swWriter.WriteLine(string.Join(",", ss));
                        }

                        swWriter.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return false;
            }
        }
     
    }
}
