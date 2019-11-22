using SaGKernel.MajorClass;
using SaGUtil.Data;
using System;

namespace SaGKernel
{
    /// <summary>
    /// QR Code 內容物，預定義為 CS19-12345$卡匣序$玻片序$特殊註記(檢體別/急)$FieldA(子編號)$FieldB
    /// 依不同的醫院，區分不同的 SaGSampleModel，例如馬偕 MMHModel
    /// 繼承 QRDataStruct，並實作符合該院的 QR Code 編碼規則
    /// </summary>
    public abstract class QRDataStruct
    {
        //QR Code 內含的資訊 (ex: CS18-12345;1;1;FS;B;1)
        public virtual string PathoQRText
        {
            get
            {
                return $"{PathoMajorText}-{PathoSequence}${CassetteSequence}${SlideSequence}${F1}${F2}${F3}${F4}${F5}${F6}${F7}${F8}${F9}${F10}${F11}${F12}${F13}${F14}${F15}${F16}${F17}${F18}${F19}${F20}";
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

        public IMajorClass SpecimenMajorClass
        {
            get
            {
                return MajorClassify.GetInstance().Classify(this);
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

        public string F1; //原SpecialRemark
        public string F2; //原FieldA
        public string F3; //原FieldB
        public string F4; 
        public string F5;
        public string F6;
        public string F7;
        public string F8;
        public string F9;
        public string F10;
        public string F11;
        public string F12;
        public string F13;
        public string F14;
        public string F15;
        public string F16;
        public string F17;
        public string F18;
        public string F19;
        public string F20;

        //public string SpecialRemark; //FS 特殊註記
        //public string FieldA; //欄位A
        //public string FieldB; //欄位B
        public int CassetteSequence; // 同一病理編號多個 cassette 的流水號
        public int SlideSequence; //同一病理編號多個 cassette，同一個 cassette 多個玻片，作流水號使用 // cassette 的 SlideSequence=0

        protected QRDataStruct() { }

        //public QRDataStruct(string pathoNo, int cassetteSeq, int slideSeq, string specialRemark, string fieldA, string fieldB):this($"{pathoNo}${cassetteSeq}${slideSeq}${specialRemark}${fieldA}${fieldB}")
        //{

        //}

        //public QRDataStruct(string QRText)
        //{
            //try
            //{
            //    string[] data = QRText.Split('$');

            //    if (data.Length ==6)
            //    {
            //        //有六個參數在QRCode裡
            //        //分別是 PathoNo,CassetteSequence,SlideSequence,SpecialRemark,FieldA,FieldB
            //        SetQRFullParameter(data);
            //    }
            //    else if ((data.Length==3) || (data.Length==4))
            //    {
            //        //有3~4個參數在QRCode裡
            //        //分別是 PathoNo,SpecialRemark,FieldA,FieldB
            //        SetQRPartialParameter(data);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(string.Concat("QRDataStruct: ", QRText, " ", ex.Message));
            //}
        //}

        protected virtual void SetQRPartialParameter(string[] data)
        {

            //if (data.Length > 0)
            //{
            //    string pathoNo = data[0];
            //    string[] pp = pathoNo.Split('-');
            //    if (pp.Length > 0)
            //    {
            //        string majorYear = pp[0];

            //        if (majorYear.Length >= 3)
            //        {
            //            PathoMajor = majorYear.Substring(0, majorYear.Length - 2);
            //            PathoMajorTail = majorYear.Substring(majorYear.Length - 2);
            //        }
            //        else
            //        {
            //            PathoMajor = majorYear;
            //            PathoMajorTail = string.Empty;
            //        }
            //    }
            //    if (pp.Length > 1)
            //    {
            //        PathoSequence = pp[1];
            //    }
            //}

            //if (data.Length > 1)
            //{
            //    SpecialRemark = data[1];
            //}

            //if (data.Length > 2)
            //{
            //    FieldA = data[2];
            //}

            //if (data.Length > 3)
            //{
            //    FieldB = data[3];
            //}

            //CassetteSequence = 0;
            //SlideSequence = 0;
        }

        protected virtual void SetQRFullParameter(string[] data)
        {
            //if (data.Length > 0)
            //{
            //    string pathoNo = data[0];
            //    string[] pp = pathoNo.Split('-');
            //    if (pp.Length > 0)
            //    {
            //        string majorYear = pp[0];

            //        if (majorYear.Length >= 3)
            //        {
            //            PathoMajor = majorYear.Substring(0, majorYear.Length - 2);
            //            PathoMajorTail = majorYear.Substring(majorYear.Length - 2);
            //        }
            //        else
            //        {
            //            PathoMajor = majorYear;
            //            PathoMajorTail = string.Empty;
            //        }
            //    }
            //    if (pp.Length > 1)
            //    {
            //        PathoSequence = pp[1];
            //    }
            //}

            //if (data.Length > 1)
            //{
            //    CassetteSequence = SaConverter.ToInt(data[1], 1);
            //}

            //if (data.Length > 2)
            //{
            //    SlideSequence = SaConverter.ToInt(data[2], 1);
            //}

            //if (data.Length > 3)
            //{
            //    SpecialRemark = data[3];
            //}

            //if (data.Length > 4)
            //{
            //    FieldA = data[4];
            //}

            //if (data.Length > 5)
            //{
            //    FieldB = data[5];
            //}
        }

        public virtual void Clear()
        {
            PathoMajor = string.Empty;
            PathoMajorTail = string.Empty;
            PathoSequence = string.Empty;
            F1 = string.Empty;
            F2 = string.Empty;
            F3 = string.Empty;
            F4 = string.Empty;
            F5 = string.Empty;
            F6 = string.Empty;
            F7 = string.Empty;
            F8 = string.Empty;
            F9 = string.Empty;
            F10 = string.Empty;
            F11 = string.Empty;
            F12 = string.Empty;
            F13 = string.Empty;
            F14 = string.Empty;
            F15 = string.Empty;
            F16 = string.Empty;
            F17 = string.Empty;
            F18 = string.Empty;
            F19 = string.Empty;
            F20 = string.Empty;
            CassetteSequence = 1;
        }


    }
}
