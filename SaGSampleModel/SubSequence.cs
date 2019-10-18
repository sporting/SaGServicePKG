using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGSampleModel
{
    /// <summary>
    /// 子編號的三個編碼子集合
    /// </summary>
    public class SubSequence
    {
        private static string[] FirstChar = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Fs" };
        private static string[] SecondChar = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static string[] ThirdChar = { "", "x", "y", "z" };
        private static string[] FourthChar = {"", "1", "2", "3", "4", "5", "6", "7", "8", "9","10","11","12","13",
            "14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32","33",
            "34","35","36","37","38","39","40","41","42","43","44","45","46","47","48","49","50","51","52","53",
            "54","55","56","57","58","59","60","61","62","63","64","65","66","67","68","69","70","71","72","73",
            "74","75","76","77","78","79","80","81","82","83","84","85","86","87","88","89","90","91","92","93",
            "94","95","96","97","98","99" };

        public static string[] GetFirstChar()
        {
            string[] ary1 = new string[FirstChar.Length];
            Array.Copy(FirstChar, 0, ary1, 0, FirstChar.Length);
            return ary1;
        }

        public static string[] GetSecondChar()
        {
            string[] ary1 = new string[SecondChar.Length];
            Array.Copy(SecondChar, 0, ary1, 0, SecondChar.Length);
            return ary1;
        }

        public static string[] GetThirdChar()
        {
            string[] ary1 = new string[ThirdChar.Length];
            Array.Copy(ThirdChar, 0, ary1, 0, ThirdChar.Length);
            return ary1;
        }
        public static string[] GetFourthChar()
        {
            string[] ary1 = new string[FourthChar.Length];
            Array.Copy(FourthChar, 0, ary1, 0, FourthChar.Length);
            return ary1;
        }
        public static string[] GetSequences(string start1Char, string end1Char, string start2Char, string end2Char, string start3Char, string end3Char, string start4Char, string end4Char)
        {
            int firstSIdx = Array.IndexOf(FirstChar, start1Char);
            int firstEIdx = Array.IndexOf(FirstChar, end1Char);

            int secondSIdx = Array.IndexOf(SecondChar, start2Char);
            int secondEIdx = Array.IndexOf(SecondChar, end2Char);

            int thirdSIdx = Array.IndexOf(ThirdChar, start3Char);
            int thirdEIdx = Array.IndexOf(ThirdChar, end3Char);

            int fourthSIdx = Array.IndexOf(FourthChar, start4Char);
            int fourthEIdx = Array.IndexOf(FourthChar, end4Char);

            List<string> res = new List<string>();

            for (int i1 = firstSIdx; i1 <= firstEIdx; i1++)
            {
                for (int i2 = secondSIdx; i2 <= secondEIdx; i2++)
                {
                    for (int i3 = thirdSIdx; i3 <= thirdEIdx; i3++)
                    {
                        for (int i4 = fourthSIdx; i4 <= fourthEIdx; i4++)
                        {
                            res.Add($"{FirstChar[i1]}{SecondChar[i2]}{ThirdChar[i3]}{FourthChar[i4]}");
                        }
                    }
                }
            }

            return res.ToArray();
        }

        //針對子編號做的解碼器
        //Fsax1 -> P1=Fs  P2=a  P3=x  P4=1
        //Fsa1  -> P1=Fs  P2=a  P3=   P4=1
        //Fsax  -> P1=Fs  P2=a  P3=x   P4=
        //Fsa   -> P1=Fs  P2=a  P3=   P4=
        //Fs1   -> P1=Fs  P2=   P3=   P4=1
        //Aa1   -> P1=A   P2=a  P3=   P4=1
        public static SubSequencePosition ValidateSubSequencePosition(string subSequence)
        {
            if (subSequence.Length > 1)
            {
                string p1 = subSequence.Substring(0, 2);
                if (p1 == "Fs")
                {
                    //冷凍切片
                    if (subSequence.Length == 5)
                    {//Fsax1
                        string c1 = subSequence.Substring(2, 1);
                        string c2 = subSequence.Substring(3, 1);
                        string c3 = subSequence.Substring(4, 1);
                        return new SubSequencePosition() { FullSubSequence = subSequence, P1 = p1, P2 = c1, P3 = c2, P4 = c3 };
                    }
                    else if (subSequence.Length == 4)
                    {//Fsa1 / Fsax
                        string c1 = subSequence.Substring(2, 1);
                        string c2 = subSequence.Substring(3, 1);
                        long num = 0;
                        bool canConvert = long.TryParse(c2, out num);
                        if (canConvert)
                        {
                            return new SubSequencePosition() { FullSubSequence = subSequence, P1 = p1, P2 = c1, P4 = c2 };
                        }
                        else
                        {
                            return new SubSequencePosition() { FullSubSequence = subSequence, P1 = p1, P2 = c1, P3 = c2 };
                        }
                    }
                    else if (subSequence.Length == 3)
                    {//Fsa or Fs1
                        string c = subSequence.Substring(2, 1);
                        long num = 0;
                        bool canConvert = long.TryParse(c, out num);
                        if (canConvert)
                        {
                            return new SubSequencePosition() { FullSubSequence = subSequence, P1=p1,P4 = c };
                        }
                        else
                        {
                            return new SubSequencePosition() { FullSubSequence = subSequence,P1=p1, P2 = c };
                        }
                    }
                    else if (subSequence.Length == 2)
                    {
                        return new SubSequencePosition() { FullSubSequence = subSequence, P1=p1 };
                    }
                }
                else
                {
                    if (subSequence.Length == 3)
                    {//Aa1
                        string c1 = subSequence.Substring(0, 1);
                        string c2 = subSequence.Substring(1, 1);
                        string c3 = subSequence.Substring(2, 1);
                        return new SubSequencePosition() { FullSubSequence = subSequence, P1 = c1, P2 = c2, P4 = c3 };
                    }
                    else if (subSequence.Length == 2)
                    {//Aa , A1, a1
                        string c1 = subSequence.Substring(0, 1);
                        string c2 = subSequence.Substring(1, 1);
                        long num = 0;
                        bool canConvert = long.TryParse(c2, out num);
                        if (canConvert)
                        {
                            if (Char.IsUpper(c1.ToCharArray()[0]))
                            {
                                return new SubSequencePosition() { FullSubSequence = subSequence, P1 = c1, P4 = c2 };
                            }
                            else
                            {
                                return new SubSequencePosition() { FullSubSequence = subSequence, P2 = c1, P4 = c2 };
                            }
                        }
                        else
                        {
                            return new SubSequencePosition() { FullSubSequence = subSequence, P1 = c1, P2 = c2 };
                        }
                    }
                }
            }
            else if (subSequence.Length == 1)
            {//A, a, 1
                string c = subSequence.Substring(0, 1);
                long num = 0;
                bool canConvert = long.TryParse(c, out num);
                if (canConvert)
                {
                    return new SubSequencePosition() { FullSubSequence = subSequence, P4 = c };
                }
                else
                {
                    if (Char.IsUpper(c.ToCharArray()[0]))
                    {
                        return new SubSequencePosition() { FullSubSequence = subSequence, P1 = c };
                    }
                    else
                    {
                        return new SubSequencePosition() { FullSubSequence = subSequence, P2 = c };
                    }
                }
            }

            return new SubSequencePosition() { FullSubSequence = subSequence };
        }
    }

    public class SubSequencePosition
    {
        public string FullSubSequence = string.Empty;
        public string P1 = string.Empty;
        public string P2 = string.Empty;
        public string P3 = string.Empty;
        public string P4 = string.Empty;
    }
}
