using SaGKernel;

namespace SaGSampleModel
{
    public class SampleQRDataStruct : QRDataStruct
    {
        protected SampleQRDataStruct()
        {
        }

        public SampleQRDataStruct(string QRText) : base(QRText)
        {
        }

        public SampleQRDataStruct(string pathoNo, int cassetteSeq, int slideSeq, string specialRemark, string fieldA, string fieldB) : base(pathoNo, cassetteSeq, slideSeq, specialRemark, fieldA, fieldB)
        {
        }
    }
}
