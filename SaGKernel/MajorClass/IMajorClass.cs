using SaGKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.MajorClass
{
    /// <summary>
    /// 定義 檢體類別 : 皮膚, 預設, 
    /// </summary>
    public interface IMajorClass
    {
        //major class name, ex: Skin
        string ClassName { get;  }
        //major class chinese name, ex: 皮膚
        string CHClassName { get; }
        MajorClassEnum MajorClass { get;}

        //輸入PathoQRCode 判斷是否為該 major class
        bool IsMe(CassetteSlideSplit css);
    }

    public enum MajorClassEnum
    {
        SkinMC, //皮膚
        DefaultMC //預設
    }
}
