using SaGKernel.MajorClass;
using SaGUtil.Data;
using System;

namespace SaGKernel
{
    public abstract class QRDataStruct
    {
        //QR Code 內含的資訊 (ex: CS18-12345;1;1;FS;B;1)
        public virtual string PathoQRText
        {
            get
            {
                return $"{PathoMajorText}-{PathoSequence}${CassetteSequence}${SlideSequence}${SpecialRemark}${FieldA}${FieldB}";
            }
        }
        public virtual string PathoMajor { get; set; } // CS
        public virtual string PathoMajorTail { get; set; } //18
        public virtual string PathoMajorText //CS18
        {
            get
            {
                return string.Concat(PathoMajor, PathoMajorTail);
            }
        }

        public virtual string PathoSequence { get; set; } //12345
        public virtual string PathoNo
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

        public IMajorClass SpecimenMajorClass
        {
            get
            {
                return MajorClassify.GetInstance().Classify(this);
            }
        }

        protected QRDataStruct() { }

        public QRDataStruct(string pathoNo, int cassetteSeq, int slideSeq, string specialRemark, string fieldA, string fieldB):this($"{pathoNo}${cassetteSeq}${slideSeq}${specialRemark}${fieldA}${fieldB}")
        {

        }

        public QRDataStruct(string QRText)
        {
            try
            {
                string[] data = QRText.Split('$');

                if (data.Length ==6)
                {
                    //有六個參數在QRCode裡
                    //分別是 PathoNo,CassetteSequence,SlideSequence,SpecialRemark,FieldA,FieldB
                    SetQRFullParameter(data);
                }
                else if ((data.Length==3) || (data.Length==4))
                {
                    //有3~4個參數在QRCode裡
                    //分別是 PathoNo,SpecialRemark,FieldA,FieldB
                    SetQRPartialParameter(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("QRDataStruct: ", QRText, " ", ex.Message));
            }
        }

        protected virtual void SetQRPartialParameter(string[] data)
        {

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
                SpecialRemark = data[1];
            }

            if (data.Length > 2)
            {
                FieldA = data[2];
            }

            if (data.Length > 3)
            {
                FieldB = data[3];
            }

            CassetteSequence = 0;
            SlideSequence = 0;
        }

        protected virtual void SetQRFullParameter(string[] data)
        {
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
                CassetteSequence = SaConverter.ToInt(data[1], 1);
            }

            if (data.Length > 2)
            {
                SlideSequence = SaConverter.ToInt(data[2], 1);
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

        public virtual void Clear()
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
