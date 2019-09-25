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
    public class OrderListBridge : BaseBridge<OrderListM>
    {
        protected override string Api
        {
            get
            {
                return "OrderList";
            }
        }

        public OrderListBridge(string token):base(token)
        {
            
        }

        public async Task<BridgeResult<OrderListM[]>> GetByOrdNo(string ordNo)
        {
            string url = $"{ApiUrl}/?ordNo={ordNo}";

            LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {ordNo}");

            HttpResponseMessage response = await Client.GetAsync(url);
            OrderListM[] res=null;
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<OrderListM[]>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<OrderListM[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<OrderListM[]> { status = false, result = res };
            }
        }

        public async Task<BridgeResult<string>> Get(string head, string yyyy)
        {
            string url = $"{ApiUrl}/?head={head}&yyyy={yyyy}";

            LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {head} {yyyy}");

            HttpResponseMessage response = await Client.GetAsync(url);
            string res = string.Empty;
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<string>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<string> { status = true, result = res };
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<string> { status = false, result = res };
            }
        }
    }
}
