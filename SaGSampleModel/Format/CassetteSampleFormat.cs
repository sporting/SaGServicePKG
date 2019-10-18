using SaGSampleModel;

namespace LCPMS15Lib.Format
{
    /// <summary>
    /// Sample 使用的 Cassette 範例格式
    /// </summary>
    public class CassetteSampleFormat:CassetteFormat
    {
        public CassetteEnvironment CSEnvironment;

        public SampleQRDataStruct Data;        
        //EMR 跟 Specimen 共用
        //把註記拉出來
        public bool IsEMR
        {
            get
            {
                return Data.SpecialRemark == ConstVal.EMR_WORD;
            }
        }

        //有特殊染色
        public bool HasSpecimen
        {
            get
            {
                return Data.SpecialRemark!=string.Empty && !IsEMR;
            }
        }


        private CassetteSampleFormat()
        {
            CSEnvironment = new CassetteEnvironment();
        }
        public CassetteSampleFormat(string QRCode):this()
        {
            Clear();

            CSEnvironment.TemplateName = string.Empty;
            CSEnvironment.CassetteName = string.Empty;

            Data = new SampleQRDataStruct(QRCode);
        }
        //same with SlideSampleFormat Constructor
        public CassetteSampleFormat(string templateName, string cassetteName, string pathoNo, int cassetteSeq, string specialRemark, string fieldA, string fieldB) : this()
        {
            Clear();

            CSEnvironment.TemplateName = templateName;
            CSEnvironment.CassetteName = cassetteName;

            Data = new SampleQRDataStruct(pathoNo, cassetteSeq, 0, specialRemark, fieldA, fieldB);
        }

        public CassetteSampleFormat(string templateName, string cassetteName, string pathoMajor, string pathoMajorTail, string pathoSequence, int cassetteSeq, string specialRemark, string fieldA, string fieldB) : this()
        {
            Clear();

            CSEnvironment.TemplateName = templateName;
            CSEnvironment.CassetteName = cassetteName;

            Data = new SampleQRDataStruct($"{pathoMajorTail}-{pathoSequence}", cassetteSeq, 0, specialRemark, fieldA, fieldB);
        }

        public CassetteSampleFormat(CassetteEnvironment ce, string pathoNo, int cassetteSeq, string specialRemark, string fieldA, string fieldB) : this()
        {
            Clear();

            CSEnvironment.TemplateName = ce.TemplateName;
            CSEnvironment.CassetteName = ce.CassetteName;

            Data = new SampleQRDataStruct(pathoNo, cassetteSeq, 0, specialRemark, fieldA, fieldB);
        }

        public CassetteSampleFormat(CassetteEnvironment ce, string pathoMajor, string pathoMajorTail, string pathoSequence, int cassetteSeq, string specialRemark, string fieldA, string fieldB) : this()
        {
            Clear();

            CSEnvironment.TemplateName = ce.TemplateName;
            CSEnvironment.CassetteName = ce.CassetteName;

            Data = new SampleQRDataStruct($"{pathoMajorTail}-{pathoSequence}", cassetteSeq, 0, specialRemark, fieldA, fieldB);
        }

        private void Clear()
        {
            CSEnvironment.TemplateName = string.Empty;
            CSEnvironment.CassetteName = string.Empty;
            if (Data != null)
            {
                Data.Clear();
            }
        }

        public override string Template {
            get
            {
                return CSEnvironment.TemplateName;
            }
            set
            {
                CSEnvironment.TemplateName = value;
            }
        }

        public override string Magazine
        {
            get
            {
                return CSEnvironment.CassetteName;
            }
            set
            {
                CSEnvironment.CassetteName = value;
            }
        }

        public override string[] LeftSide { get { return new string[] { }; } set { } }
        public override string[] RightSide { get { return new string[] { }; } set { } }
        public override string[] FrontSide
        {
            get
            {
                return new string[] 
                { Data.PathoQRText ,
                    Data.PathoNo,
                    Data.SpecialRemark,
                    Data.FieldA,
                    Data.FieldB
                };
            }       
            set            {            }     
        }

    }
}
