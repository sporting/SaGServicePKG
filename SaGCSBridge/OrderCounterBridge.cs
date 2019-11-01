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
    public class OrderCounterBridge : BaseCSBridge<string>
    {
        protected override string Api
        {
            get
            {
                return "OrderCounter";
            }
        }

        public OrderCounterBridge(string token) : base(token)
        {

        }

        public async Task<BridgeResult<bool>> GetExist(string headTag, string headYear, string tailSeq)
        {
            try
            {
                OrderCounter counter = new OrderCounter();
                return await Task.FromResult(new BridgeResult<bool>
                {
                    status = true,
                    message = string.Empty,
                    result = counter.GetExist(headTag, headYear, tailSeq)
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

        public async Task<BridgeResult<int>> GetNext(string head, string yyyy)
        {
            try
            {
                OrderCounter counter = new OrderCounter();
                int nextCount = counter.GetNextOrderCount(head, yyyy);

                return await Task.FromResult(new BridgeResult<int>
                {
                    status = true,
                    message = string.Empty,
                    result = nextCount
                });                
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<int>
                {
                    status = false,
                    message = ex.Message,
                    result = 0
                });
            }
        }
    }
}
