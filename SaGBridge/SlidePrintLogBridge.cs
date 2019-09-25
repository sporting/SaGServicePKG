using Newtonsoft.Json;
using SaGModel;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGBridge
{
    public class SlidePrintLogBridge : BaseBridge<SlidePrintLogM>
    {
        protected override string Api
        {
            get
            {
                return "SlidePrintLog";
            }
        }

        public SlidePrintLogBridge(string token):base(token)
        {
            
        }

    }
}
