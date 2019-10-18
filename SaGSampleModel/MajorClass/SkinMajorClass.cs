using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaGKernel;

namespace SaGKernel.MajorClass
{
    /// <summary>
    /// 依 QR Code 內容決定是否為 皮膚
    /// </summary>
    public class SkinMajorClass : IMajorClass
    {
        public string CHClassName
        {
            get
            {
                return "皮膚";
            }
        }

        public string ClassName
        {
            get
            {
                return "Skin";
            }
        }

        public MajorClassEnum MajorClass
        {
            get
            {
                return MajorClassEnum.SkinMC;
            }
        }

        public bool IsMe(QRDataStruct css)
        {
            //序號第一碼是8開頭的，表示是 Skin (皮膚)
            return css.PathoSequence.Substring(0, 1) == "8";
        }
    }
}
