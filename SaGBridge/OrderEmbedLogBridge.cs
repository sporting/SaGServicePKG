 
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
    public class OrderEmbedLogBridge : BaseBridge<OrderEmbedLogM>
    {
        protected override string Api
        {
            get
            {
                return "OrderEmbedLog";
            }
        }

        public OrderEmbedLogBridge(string token):base(token)
        {
            
        }

    }
}
