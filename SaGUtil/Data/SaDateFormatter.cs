using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.Data
{
    /// <summary>
    /// DateFormatter Function Collection
    /// </summary>
    public class SaDateFormatter
    {
        public static DateTime StrToDateTime(string s)
        {
            s = s.Replace("/", "");

            if ((s.Length == 6))
                // 西元年月
                return Convert.ToDateTime(s.Substring(0, 4) + "/" + s.Substring(4, 2) + "/" + "01");
            else if ((s.Length == 8))
                // 西元年月日
                return Convert.ToDateTime(s.Substring(0, 4) + "/" + s.Substring(4, 2) + "/" + s.Substring(6, 2));
            else if ((s.Length == 7))
                // 民國年月日
                return Convert.ToDateTime((Convert.ToInt16(s.Substring(0, 3)) + 1911).ToString() + "/" + s.Substring(3, 2) + "/" + s.Substring(5, 2));
            else if ((s.Length == 5))
                // 民國年月
                return Convert.ToDateTime((Convert.ToInt16(s.Substring(0, 3)) + 1911).ToString() + "/" + s.Substring(3, 2) + "/" + "01");
            else
                return DateTime.MinValue;
        }

        public static string DateFormat(string s, string delimiter = "/")
        {
            return StrToDateTime(s).ToString(string.Format("yyyy{0}MM{1}dd", delimiter, delimiter));
        }
        public static string TimeFormat(string s)
        {
            if ((s.Length == 6))
                return string.Concat(s.Substring(0, 2), ":", s.Substring(2, 2), ":", s.Substring(4, 2));
            else if ((s.Length == 4))
                return string.Concat(s.Substring(0, 2), ":", s.Substring(2, 2));
            return s;
        }

        public static string SubMonthDay(string s, string delimiter = "/")
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return StrToDateTime(s).ToString(string.Format("MM{0}dd", delimiter));
        }
        public static string SubHourMinute(string s)
        {
            if (s.Length >= 4)
            {
                return TimeFormat(s.Substring(0, 4));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
