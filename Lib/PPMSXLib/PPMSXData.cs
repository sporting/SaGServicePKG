using SaGUtil.IO;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PPMSXLib
{

    public class PPMSXData
    {
        List<SlideFormat> _dataSections = new List<SlideFormat>();
        public void Clear()
        {
            _dataSections.Clear();
        }


        public void AddSection(SlideFormat ds)
        {
            _dataSections.Add(ds);
        }
        public void AddSections(SlideFormat[] dss)
        {
            _dataSections.AddRange(dss);
        }

        public bool SaveFile(string outputFolder)
        {
            string fileName = SaFile.GetTicksFileName(outputFolder, "CSV");
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter swWriter = new StreamWriter(fs, Encoding.GetEncoding(932))) //932 : Shift_JIS
                    {
                        foreach (SlideFormat data in _dataSections)
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
                LogMan.Instance.Error("PPMSXData.SaveFiles", $"{ex.Message}");
                return false;
            }
        }
     
    }
}
