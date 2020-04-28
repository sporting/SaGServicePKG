// *****************************************************
// * Copyright 2017, SportingApp, all rights reserved. *
// * Author: Shih Peiting                              *
// * mailto: sportingapp@gmail.com                     *
// *****************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaGUtil.Extensions
{
    /// <summary>
    ///     ''' for string type extension method
    ///     ''' </summary>
    ///     ''' <remarks></remarks>
    public static class SaStringExtension
    {
        public static string QuotedStr(this string S)
        {
            int I;
            string Result = S;
            for (I = Result.Length - 1; I >= 0; I += -1)
            {
                if (Result[I] == '\'')
                    Result = Result.Insert(I, "'");
            }
            Result = "'" + Result + "'";

            return Result;
        }

        /// <summary>
        ///         ''' True 表示式 : true, y, 1
        ///         ''' </summary>
        ///         ''' <param name="S"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static bool IsTrue(this string S)
        {
            return S.ToLower() == "true" || S.ToLower() == "y" || S.ToLower() == "1";
        }

        public static bool IsEmpty(this string S)
        {
            return S == null || S.Trim() == string.Empty;
        }

        public static bool IsInt(this string S)
        {
            int res;
            return int.TryParse(S, out res);
        }

        public static int ToInt(this string S)
        {
            int res;
            if (int.TryParse(S, out res))
                return res;
            return default(int);
        }

        public static int ToInt(this string S, int defaultInt)
        {
            int res;
            if (int.TryParse(S, out res))
                return res;
            return defaultInt;
        }

        public static bool IsDecimal(this string S)
        {
            decimal res;
            return decimal.TryParse(S, out res);
        }

        public static decimal ToDecimal(this string S)
        {
            decimal res;
            if (decimal.TryParse(S, out res))
                return res;
            return default(Decimal);
        }

        public static decimal ToDecimal(this string S, decimal defaultDecimal)
        {
            decimal res;
            if (decimal.TryParse(S, out res))
                return res;
            return defaultDecimal;
        }

        public static double IsDouble(this string S)
        {
            double res;
            if (double.TryParse(S, out res))
                return res;
            return default(double);
        }

        public static double ToDouble(this string S)
        {
            double res;
            if (double.TryParse(S, out res))
                return res;
            return default(Double);
        }

        public static double ToDouble(this string S, double defaultDouble)
        {
            double res;
            if (double.TryParse(S, out res))
                return res;
            return defaultDouble;
        }

        public static byte[] ParserHexToInt(this string S)
        {
            try
            {
                S = S.Trim();
                if (S.IsEmpty())
                    return null;

                string[] apdu_str = S.Split(' ');
                if (apdu_str.Length <= 1)
                    apdu_str = S.Split('-');
                var apdu_buffers = from t in apdu_str
                                   select Convert.ToByte(Convert.ToInt16(t.Trim(), 16));

                return apdu_buffers.ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///         ''' 80 CA 04 00 90 FE
        ///         ''' 80 09 CA 04 55 FB
        ///         ''' string transfer to byte
        ///         ''' </summary>
        ///         ''' <param name="S"></param>
        ///         ''' <returns></returns>
        public static byte[] StringToBytes(this string S)
        {
            string[] ls = S.Split('\n');
            List<byte> bs = new List<byte>();

            foreach (string l in ls)
                bs.AddRange(ParserHexToInt(l.Trim()));

            return bs.ToArray();
        }
    }
}

