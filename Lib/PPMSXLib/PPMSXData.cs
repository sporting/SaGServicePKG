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

        public bool SaveFile(string outputFolder)
        {
            string fileName = GetFileName(outputFolder);
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

            LogMan.Instance.Error("PPMSXData", $"{outputFolder} Directory not found");
            return string.Empty;
        }
    }
}
