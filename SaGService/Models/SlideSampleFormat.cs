using PPMSXLib;
using SaGKernel;
using SaGLogic;
using SaGUtil.System;
using System;

namespace SaGService.Models
{
    /// <summary>
    /// Demo 使用的玻片範例格式
    /// </summary>
    public class SlideSampleFormat:SlideFormat
    {
        public QRDataStruct Data;
        
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
            Data = new QRDataStruct(pathoQRText);

            Val0 = Data.PathoQRText;
            Val1 = Data.PathoMajorText;
            Val2 = Data.PathoSequence;
            Val3 = Data.SpecialRemark;
            Val4 = Data.FieldA;
            Val5 = Data.FieldB;
        }

        public void SetVal(string pathoNo,int cassetteSeq,int slideSeq, string specialRemark, string fieldA, string fieldB)
        {
            Data = new QRDataStruct(pathoNo, cassetteSeq, slideSeq, specialRemark, fieldA, fieldB);

            Val0 = Data.PathoQRText;
            Val1 = Data.PathoMajorText;
            Val2 = Data.PathoSequence;
            Val3 = Data.SpecialRemark;
            Val4 = Data.FieldA;
            Val5 = Data.FieldB;
        }
        public void SetEnv(string layoutName, string printerName, int slotId)
        {
            LayoutName = layoutName;
            PrinterName = printerName;
            SlotId = slotId;
        }

        private void Clear()
        {
            if (Data != null)
            {
                Data.Clear();
            }
        }

    }
}
