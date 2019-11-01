using SaGModel;
using SaGLogic;
using SaGUtil.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGCSBridge
{
    public class OrderGrossLogBridge : BaseCSBridge<OrderGrossLogM>
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
        public async Task<BridgeResult<OrderGrossLogM>> AddGrossLog(OrderGrossLogM log)
        {
            try
            {
                OrderGrossLog grossLog = new OrderGrossLog();
                if (grossLog.AddLog(log))
                {
                    return await Task.FromResult(new BridgeResult<OrderGrossLogM>
                    {
                        status = true,
                        message = string.Empty,
                        result = log
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<OrderGrossLogM>
                    {
                        status = false,
                        message = "OrderGrossLog AddLog Error",
                        result = log
                    });
                }
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderGrossLogM>
                {
                    status = false,
                    message = ex.Message,
                    result = log
                });
            }
        }
    }
}
