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
    public class OrderEmbedLogBridge : BaseCSBridge<OrderEmbedLogM>
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
        public async Task<BridgeResult<OrderEmbedLogM>> AddEmbedLog(OrderEmbedLogM log)
        {
            try
            {
                OrderEmbedLog embedLog = new OrderEmbedLog();
                if (embedLog.AddLog(log))
                {                   
                    return await Task.FromResult(new BridgeResult<OrderEmbedLogM>
                    {
                        status = true,
                        message = string.Empty,
                        result = log
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<OrderEmbedLogM>
                    {
                        status = false,
                        message = "OrderEmbedLog AddLog Error",
                        result = log
                    });
                }
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderEmbedLogM>
                {
                    status = false,
                    message = ex.Message,
                    result = log
                });
            }
        }
    }
}
