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
    public class OrderBarcodeLogBridge : BaseBridge<OrderBarcodeLogM>
    {
        protected override string Api
        {
            get
            {
                return "OrderBarcodeLog";
            }
        }

        public OrderBarcodeLogBridge(string token):base(token)
        {
            
        }

    }
}
