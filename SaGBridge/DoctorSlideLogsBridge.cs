using SaGModel;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGBridge
{
    public class DoctorSlideLogsBridge : BaseBridge<DoctorSlideLogM[]>
    {
        protected override string Api
        {
            get
            {
                return "DoctorSlideLogs";
            }
        }

        public DoctorSlideLogsBridge(string token):base(token)
        {
            
        }

    }
}
