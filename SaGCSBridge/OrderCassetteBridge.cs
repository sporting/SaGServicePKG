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
    public class OrderCassetteBridge : BaseCSBridge<OrderCassetteM>
    {
        protected override string Api
        {
            get
            {
                return "OrderCassette";
            }
        }

        public OrderCassetteBridge(string token):base(token)
        {
            
        }
        public async Task<BridgeResult<OrderCassetteM>> AddCassette(OrderCassetteM log)
        {
            try
            {
                OrderCassette cassette = new OrderCassette();
                int newCassetteSeq = 1;
                if (cassette.Add(log, out newCassetteSeq))
                {
                    log.CassetteSequence = newCassetteSeq;

                    return await Task.FromResult(new BridgeResult<OrderCassetteM>
                    {
                        status = true,
                        message = string.Empty,
                        result = log
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<OrderCassetteM>
                    {
                        status = false,
                        message = "OrderCassette Add Error",
                        result = log
                    });
                }                
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderCassetteM>
                {
                    status = false,
                    message = ex.Message,
                    result = log
                });
            }
        }

        public async Task<BridgeResult<bool>> GetExist(string ordNo, int cassetteSeq)
        {
            try
            {
                OrderCassette order = new OrderCassette();
                
                return await Task.FromResult(new BridgeResult<bool>
                {
                    status = true,
                    message = string.Empty,
                    result = order.CassetteExist(ordNo, cassetteSeq)
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<bool>
                {
                    status = false,
                    message = ex.Message,
                    result = false
                });
            }
        }

        public async Task<BridgeResult<OrderCassetteM[]>> GetDetail(string type, string begDate, string endDate, string user)
        {
            try
            {
                OrderCassette order = new OrderCassette();

                if (type == "gross")
                {
                    return await Task.FromResult(new BridgeResult<OrderCassetteM[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = order.GetGrossDetail(begDate, endDate, user)
                    });
                }
                else if (type == "embed")
                {
                    return await Task.FromResult(new BridgeResult<OrderCassetteM[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = order.GetEmbedDetail(begDate, endDate, user)
                    });
                }
                
                return await Task.FromResult(new BridgeResult<OrderCassetteM[]>
                {
                    status = false,
                    message = "please give argument 'type' in ('gross' or 'embed')",
                    result = new OrderCassetteM[] { }
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<OrderCassetteM[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new OrderCassetteM[] { }
                });
            }
        }
    }
}
