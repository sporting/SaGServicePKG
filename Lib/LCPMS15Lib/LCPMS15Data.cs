using SaGUtil.IO;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Cassette Printer
/// LCPMS15 可接受的文字檔對應欄位
/// 
/// </summary>
namespace LCPMS15Lib
{
    public enum CSSide
    {
        [EnumVal("S1")]
        Left,
        [EnumVal("S2")]
        Front,
        [EnumVal("S3")]
        Right
    }


    public class LCPMS15Data
    {
        string NewCassetteChar = "NC";

        List<CassetteFormat> _dataCassettes = new List<CassetteFormat>();

        public void Clear()
        {
            _dataCassettes.Clear();
        }
        

        public void AddCassette(CassetteFormat ds)
        {
            _dataCassettes.Add(ds);
        }

        public bool SaveFile(string outputFolder)
        {
            string fileName = SaFile.GetTicksFileName(outputFolder, "txt");

            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter swWriter = new StreamWriter(fs, Encoding.UTF8))
                    {
                        for (int i = 0; i < _dataCassettes.Count; i++)
                        {
                            swWriter.WriteLine(_dataCassettes[i].ToString());
                            if (i < _dataCassettes.Count - 1)
                            {
                                swWriter.WriteLine(NewCassetteChar);
                            }
                        }

                        swWriter.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error("LCPMS15Data", $"{fileName} SaveFile failed:{ex.Message}");
                //Console.WriteLine(string.Concat("PPMSXData.SaveFile", ex.Message));
                return false;
            }
        }

    }
}
