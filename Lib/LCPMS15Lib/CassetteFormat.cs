
using SaGUtil.System;
using System.Text;

namespace LCPMS15Lib
{
    /// <summary>
    /// Cassette Printer
    /// LCPMS15 每一個 Cassette 對應欄位文字輸出
    /// </summary>
    public abstract class CassetteFormat
    {
        string TemplateChar = "TE";
        string MagazineChar = "DT";
        public abstract string Template { get; set; }
        public abstract string Magazine { get; set; }
        public abstract string[] LeftSide { get; set; }
        public abstract string[] FrontSide { get; set; }
        public abstract string[] RightSide { get; set; }

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
            sb.AppendLine(string.Format("{0};{1};", TemplateChar, Template));
            sb.AppendLine(string.Format("{0};{1};", MagazineChar, Magazine));
            s = ArrayText(LeftSide);
            if (s.Length > 0) { sb.AppendLine(string.Format("{0};{1}", SaEnumVal.Value(CSSide.Left), s)); }
            s = ArrayText(FrontSide);
            if (s.Length > 0) { sb.AppendLine(string.Format("{0};{1}", SaEnumVal.Value(CSSide.Front), s)); }
            s = ArrayText(RightSide);
            if (s.Length > 0) { sb.AppendLine(string.Format("{0};{1}", SaEnumVal.Value(CSSide.Right), s)); }
            return sb.ToString().Trim();
        }
    }
}