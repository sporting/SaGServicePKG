using SaGUtil.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.Data
{
    /// <summary>
    /// Converter Function Collection
    /// </summary>
    public class SaConverter
    {
        public static int ToInt(object obj, int defVal)
        {
            int val = defVal;
            if (int.TryParse(obj.ToString(), out val))
            {
                return val;
            }

            return defVal;
        }

        public static double ToDouble(string s, double defval)
        {
            try
            {
                return Convert.ToDouble(s);
            }
            catch
            {
                return defval;
            }
        }

        public static Int64 ToInt64(string s, Int64 defval)
        {
            try
            {
                return Convert.ToInt64(s);
            }
            catch
            {
                return defval;
            }
        }

        public static DateTime StrToDate(string s)
        {
            return StrToDate(s, "/");
        }

        public static string FormatDate(string s)
        {
            return StrToDate(s, "/").ToString("yyyy/MM/dd");
        }
        public static string FormatDayOfWeek(string s)
        {
            return StrToDate(s, "/").ToString("ddd");
        }

        public static string FormatTime(string s)
        {
            return FormatTime(s, ":");
        }

        public static DateTime StrToDate(string s, string delimiter)
        {
            s = s.Replace(delimiter, string.Empty);

            if (s.Length == 6)
            {
                //西元年月
                return Convert.ToDateTime(s.Substring(0, 4) + delimiter + s.Substring(4, 2) + delimiter + "01");
            }
            else if (s.Length == 8)
            {
                //西元年月日
                return Convert.ToDateTime(s.Substring(0, 4) + delimiter + s.Substring(4, 2) + delimiter + s.Substring(6, 2));
            }
            else if (s.Length == 7)
            {
                //民國年月日
                return Convert.ToDateTime((s.Substring(0, 3).ToInt() + 1911).ToString() + delimiter + s.Substring(3, 2) + delimiter + s.Substring(5, 2));
            }
            else if (s.Length == 5)
            {
                //民國年月
                return Convert.ToDateTime((s.Substring(0, 3).ToInt() + 1911).ToString() + delimiter + s.Substring(3, 2) + delimiter + "01");
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static string FormatTime(string s, string delimiter)
        {
            s = s.Replace(delimiter, string.Empty);

            if (s.Length == 4)
            {
                return (s.Substring(0, 2) + delimiter + s.Substring(2, 2) + delimiter + "00");
            }
            else if (s.Length == 6)
            {
                return (s.Substring(0, 2) + delimiter + s.Substring(2, 2) + delimiter + s.Substring(4, 2));
            }
            else
            {
                return s;
            }
        }
    }
}
