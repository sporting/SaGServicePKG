using SaGLogic;
using SaGUtil.System;
using System;

namespace SaGService.Models
{
    /// <summary>
    /// Demo 使用的玻片範例格式
    /// </summary>
    public class SlideSampleFormat
    {
        //private PPMSXData _ppmsxData;

        public CassetteSlideSplit Data;
        //pathoQRText: PathoNo$CassetteSequence$SlideSequence$SpecialRemark$FieldA$FieldB == QRCode
        public SlideSampleFormat(string QRText)
        {
            //PathoQRText = pathoQRText;
            try
            {
                Data = new CassetteSlideSplit(QRText);              
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error("SlideSampleFormat", $"{QRText} SaveFile failed:{ex.Message}");
                //Console.WriteLine(string.Concat("SlideSampleFormat: ", QRText, " ", ex.Message));
            }
        }

        public void SetSlideSequence(int seq)
        {
            Data.SlideSequence = seq;
        }

        //public bool CreateFile(string layoutName, string printerName, int slotId, string outputFolder)
        //{
        //    if (Directory.Exists(outputFolder))
        //    {
        //        _ppmsxData = new PPMSXData();
        //        _ppmsxData.LayoutName = layoutName;
        //        _ppmsxData.PrinterName = printerName;
        //        _ppmsxData.SlotId = slotId;
        //        _ppmsxData.SeqPrintFlag = false;
        //        _ppmsxData.Sequence = 0;
        //        _ppmsxData.SubSeqPrintFlag = false;
        //        _ppmsxData.SubSeqStart = 1;
        //        _ppmsxData.SubSeqEnd = 1;
        //        _ppmsxData.Val0 = Data.PathoQRText;
        //        _ppmsxData.Val1 = Data.PathoMajorText;
        //        _ppmsxData.Val2 = Data.PathoSequence;
        //        _ppmsxData.Val3 = Data.SpecialRemark;
        //        _ppmsxData.Val4 = Data.FieldA;
        //        _ppmsxData.Val5 = Data.FieldB;

        //        string uniqueFileName = string.Format(@"{0}_{1}.CSV", Data.PathoNo, DateTime.Now.Ticks);
        //        string outputFileName = Path.Combine(outputFolder, uniqueFileName);

        //        return _ppmsxData.SaveFile(outputFileName);
        //    }

        //    Console.WriteLine(string.Concat(outputFolder, " Directory not found"));
        //    return false;
        //}
    }
}
