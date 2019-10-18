using SaGKernel.Settings;
using SaGSampleModel;
using LCPMS15Lib.Format;
using SaGKernel.CSFilter;

namespace PPMSXLib.Format
{
    /// <summary>
    /// Demo 使用的玻片範例格式
    /// 欄位對照:
    /// Data.SpecialRemark = Specimen 檢體別
    /// Data.FieldA = 子編號 Aa1
    /// Data.FieldB = Staining 特殊染色劑
    /// </summary>
    public class SlideSampleFormat : SlideFormat
    {
        public SampleQRDataStruct Data;

        public SlideSampleFormat()
        {
            SeqPrintFlag = false;
            Sequence = 0;
            SubSeqPrintFlag = false;
            SubSeqStart = 1;
            SubSeqEnd = 1;
        }      

        //pathoQRText: PathoNo;Specimen;FieldABC; == QRCode
        public SlideSampleFormat(string pathoQRText):this()
        {
            Data = new SampleQRDataStruct(pathoQRText);

            Val0 = Data.PathoQRText;
            Val1 = Data.PathoMajorText;
            Val2 = Data.PathoSequence;
            Val3 = Data.SpecialRemark; //檢體別 specimen
            Val4 = Data.FieldA; //子編號 Aa1
            Val5 = Data.FieldB; //染色 staining
        }

        public SlideSampleFormat(SlideEnvironment env,string pathoQRText) : this(pathoQRText)
        {
            LayoutName = env.LayoutName;
            PrinterName = env.PrinterName;
            SlotId = env.SlotId;
        }

        public void SetVal(string pathoNo, int cassetteSeq, int slideSeq, string specialRemark, string fieldA, string fieldB)
        {
            Data = new SampleQRDataStruct(pathoNo, cassetteSeq, slideSeq, specialRemark, fieldA, fieldB);

            Val0 = Data.PathoQRText;
            Val1 = Data.PathoMajorText;
            Val2 = Data.PathoSequence;
            Val3 = Data.SpecialRemark;
            Val4 = Data.FieldA;
            Val5 = Data.FieldB;
        }

        public void SetVal(SlideEnvironment env, string pathoNo, int cassetteSeq, int slideSeq, string specialRemark, string fieldA, string fieldB)
        {
            SetVal(pathoNo, cassetteSeq, slideSeq, specialRemark, fieldA, fieldB);
            SetEnv(env);
        }

        public void SetEnv(string layoutName, string printerName, int slotId)
        {
            LayoutName = layoutName;
            PrinterName = printerName;
            SlotId = slotId;
        }

        public void SetEnv(SlideEnvironment env)
        {
            LayoutName = env.LayoutName;
            PrinterName = env.PrinterName;
            SlotId = env.SlotId;
        }
        
        private void Clear()
        {
            if (Data != null)
            {
                Data.Clear();
            }
        }

        public static SlideFormat[] GenerateSlideFormat(CSFilterClassify csFilterChooseser,SlideSettings settings, string pathoNo, string specimen, string subSequence)
        {
            SampleQRDataStruct css = new SampleQRDataStruct(pathoNo, 0, 0, specimen, subSequence, string.Empty);
            int slideMagazine = SlideSettingsApi.SlideValue(settings, css.SpecimenMajorClass);

            csFilterChooseser.SetSlideEnvironment(new SlideEnvironment(settings.LayoutName, settings.PrinterName, slideMagazine));
            SlideFormat[] smmhs = csFilterChooseser.MatchRule2GetSlides(new CassetteSampleFormat(string.Empty, string.Empty, pathoNo, 0, specimen, subSequence, string.Empty));

            return smmhs;
        }
    }
}
