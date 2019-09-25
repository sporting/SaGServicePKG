using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

    public class DataSection
    {
        string TemplateChar = "TE";
        string MagazineChar = "DT";
        string _template = string.Empty;
        string _magazine = string.Empty;
        string[] _leftSide = new string[] { "" };
        string[] _frontSide = new string[] { "" };
        string[] _rightSide = new string[] { "" };


        public void SetLeft(string[] strs)
        {
            _leftSide = strs;
        }
        public void SetFront(string[] strs)
        {
            _frontSide = strs;
        }
        public void SetRight(string[] strs)
        {
            _rightSide = strs;
        }
        public void Set(string[] leftStrs, string[] frontStrs, string[] rightStrs)
        {
            SetLeft(leftStrs);
            SetFront(frontStrs);
            SetRight(rightStrs);
        }
        public void SetTemplate(string template)
        {
            _template = template;
        }

        public void SetMagazine(string magazine)
        {
            _magazine = magazine;
        }
        private string ArrayText(string[] ss)
        {
            if (ss.Length > 0)
            {
                string s = string.Empty;
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].Trim().Length > 0)
                    {
                        s += string.Format("#{0}#{1};", i + 1, ss[i]);
                    }
                }
                return s;
            }

            return string.Empty;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string s = string.Empty;
            sb.AppendLine(string.Format("{0};{1};", TemplateChar, _template));
            sb.AppendLine(string.Format("{0};{1};", MagazineChar, _magazine));
            s = ArrayText(_leftSide);
            if (s.Length > 0) { sb.AppendLine(string.Format("{0};{1}", EnumVal.Value(CSSide.Left), s)); }
            s = ArrayText(_frontSide);
            if (s.Length > 0) { sb.AppendLine(string.Format("{0};{1}", EnumVal.Value(CSSide.Front), s)); }
            s = ArrayText(_rightSide);
            if (s.Length > 0) { sb.AppendLine(string.Format("{0};{1}", EnumVal.Value(CSSide.Right), s)); }
            return sb.ToString().Trim();
        }
    }

    public class LCPMS15Data
    {
        string NewCassetteChar = "NC";

        List<DataSection> _dataSections = new List<DataSection>();

        public void Clear()
        {
            _dataSections.Clear();
        }
        

        public void AddSection(DataSection ds)
        {
            _dataSections.Add(ds);
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
                    using (StreamWriter swWriter = new StreamWriter(fs, Encoding.UTF8))
                    {
                        for (int i = 0; i < _dataSections.Count; i++)
                        {
                            swWriter.WriteLine(_dataSections[i].ToString());
                            if (i < _dataSections.Count - 1)
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

        private string GetFileName(string outputFolder)
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            if (Directory.Exists(outputFolder))
            {
                string uniqueFileName = string.Format(@"{0}.txt", DateTime.Now.Ticks);
                string outputFileName = Path.Combine(outputFolder, uniqueFileName);

                return outputFileName;
            }

            //Console.WriteLine(string.Concat(outputFolder, " Directory not found"));
            LogMan.Instance.Error("LCPMS15Data", $"{outputFolder} Directory not found");
            return string.Empty;
        }
    }
}
