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
    public class OrderGrossLogBridge : BaseBridge<OrderGrossLogM>
    {
        protected override string Api
        {
            get
            {
                return "OrderGrossLog";
            }
        }

        public OrderGrossLogBridge(string token):base(token)
        {
            
        }

    }
}
