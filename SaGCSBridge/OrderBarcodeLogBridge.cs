using SaGLogic;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGCSBridge
{
    public class OrderBarcodeLogBridge : BaseCSBridge<OrderBarcodeLogM>
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

        public async Task<BridgeResult<OrderBarcodeLogM>> AddLog(OrderBarcodeLogM log)
        {
            try
            {
                OrderBarcodeLog orderCounterLog = new OrderBarcodeLog();
                if (orderCounterLog.Add(log))
                {
                    return await Task.FromResult(new BridgeResult<OrderBarcodeLogM>
                    {
                        status = true,
                        message = string.Empty,
                        result = log
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<OrderBarcodeLogM>
                    {
                        status = false,
                        message = "OrderBarcodeLog Add Error",
                        result = log
                    });
                }
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderBarcodeLogM>
                {
                    status = false,
                    message = ex.Message,
                    result = log
                });
            }
        }
    }
}
