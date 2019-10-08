using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaGKernel;

namespace SaGKernel.MajorClass
{
    public class DefaultMajorClass : IMajorClass
    {
        public string CHClassName
        {
            get
            {
                return "預設";
            }
        }

        public string ClassName
        {
            get
            {
                return "Default";
            }
        }

        public MajorClassEnum MajorClass
        {
            get
            {
                return MajorClassEnum.DefaultMC;
            }
        }

        public bool IsMe(QRDataStruct css)
        {
            return false;
        }
    }
}
