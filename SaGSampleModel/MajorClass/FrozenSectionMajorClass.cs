using SaGSampleModel;

namespace SaGKernel.MajorClass
{
    /// <summary>
    /// 依 QR Code 內容決定是否為冷凍切片
    /// </summary>
    public class FrozenSectionMajorClass : IMajorClass
    {
        public string CHClassName
        {
            get
            {
                return "冷凍切片";
            }
        }

        public string ClassName
        {
            get
            {
                return "Fs";
            }
        }

        public MajorClassEnum MajorClass
        {
            get
            {
                return MajorClassEnum.FsMC;
            }
        }

        public bool IsMe(QRDataStruct css)
        {
            //馬偕 FieldA 放的是 SubSequence
            SubSequencePosition ssp = SubSequence.ValidateSubSequencePosition(css.FieldA);
            return ssp.P1.ToUpper() == "FS";
        }
        
    }
}
