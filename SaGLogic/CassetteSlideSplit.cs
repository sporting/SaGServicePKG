using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGLogic
{
    public class CassetteSlideSplit
    {
        public string PathoQRText
        {
            get
            {
                return $"{PathoMajorText}-{PathoSequence}${CassetteSequence}${SlideSequence}${SpecialRemark}${FieldA}${FieldB}";
            }
        }//CS18-12345;1;1;FS;B;1
        public string PathoMajor; // CS
        public string PathoMajorTail; //18
        public string PathoMajorText //CS18
        {
            get
            {
                return string.Concat(PathoMajor, PathoMajorTail);
            }
        }

        public string PathoSequence; //12345
        public string PathoNo
        {
            get
            {
                return string.Format("{0}-{1}", PathoMajorText, PathoSequence);
            }
        }
        public string SpecialRemark; //FS 特殊註記
        public string FieldA; //欄位A
        public string FieldB; //欄位B
        public int CassetteSequence; // 同一病理編號多個 cassette 的流水號
        public int SlideSequence; //同一病理編號多個 cassette，同一個 cassette 多個玻片，作流水號使用 // cassette 的 SlideSequence=0

        public CassetteSlideSplit(string pathoNo, int cassetteSeq, int slideSeq, string specialRemark, string fieldA, string fieldB):this($"{pathoNo}${cassetteSeq}${slideSeq}${specialRemark}${fieldA}${fieldB}")
        {

        }

        public CassetteSlideSplit(string QRText)
        {
            try
            {
                string[] data = QRText.Split('$');
                if (data.Length > 0)
                {
                    string pathoNo = data[0];
                    string[] pp = pathoNo.Split('-');
                    if (pp.Length > 0)
                    {
                        string majorYear = pp[0];

                        if (majorYear.Length >= 3)
                        {
                            PathoMajor = majorYear.Substring(0, majorYear.Length - 2);
                            PathoMajorTail = majorYear.Substring(majorYear.Length - 2);
                        }
                        else
                        {
                            PathoMajor = majorYear;
                            PathoMajorTail = string.Empty;
                        }
                    }
                    if (pp.Length > 1)
                    {
                        PathoSequence = pp[1];
                    }
                }

                if (data.Length > 1)
                {
                    CassetteSequence = Converter.ToInt(data[1], 1);
                }

                if (data.Length > 2)
                {
                    SlideSequence = Converter.ToInt(data[2], 1);
                }

                if (data.Length > 3)
                {
                    SpecialRemark = data[3];
                }

                if (data.Length > 4)
                {
                    FieldA = data[4];
                }

                if (data.Length > 5)
                {
                    FieldB = data[5];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("CassetteSlideSplit: ", QRText, " ", ex.Message));
            }
        }

        public void Clear()
        {
            PathoMajor = string.Empty;
            PathoMajorTail = string.Empty;
            PathoSequence = string.Empty;
            SpecialRemark = string.Empty;
            FieldA = string.Empty;
            FieldB = string.Empty;
            CassetteSequence = 1;
        }
    }
}
