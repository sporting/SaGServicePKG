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
    public class OrderCassetteBridge : BaseBridge<OrderCassetteM>
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

        public async Task<BridgeResult<bool>> GetExist(string ordNo, int cassetteSeq)
        {
            string url = $"{ApiUrl}/?ordNo={ordNo}&cassetteSeq={cassetteSeq}";

            LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {ordNo} {cassetteSeq}");

            HttpResponseMessage response = await Client.GetAsync(url);
            bool res = false;
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<bool>(responseBody);
                }
                catch (Exception ex)
                {
                    return new BridgeResult<bool> { status = false, result = res, message = ex.Message };
                }

                return new BridgeResult<bool> { status = true, result = res };
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<bool> { status = false, result = res,message=ex.Message };
            }
        }

        public async Task<BridgeResult<OrderCassetteM[]>> Get(string type, string begDate, string endDate, string user)
        {
            string url = $"{ApiUrl}/?type={type}&begDate={begDate}&endDate={endDate}&user={user}";

            LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {type} {begDate} {endDate} {user}");

            HttpResponseMessage response = await Client.GetAsync(url);
            OrderCassetteM[] res = new OrderCassetteM[] { };
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<OrderCassetteM[]>(responseBody);
                }
                catch (Exception ex)
                {
                    return new BridgeResult<OrderCassetteM[]> { status = false, result = res, message = ex.Message };
                }

                return new BridgeResult<OrderCassetteM[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<OrderCassetteM[]> { status = false, result = res, message = ex.Message };
            }
        }
    }
}
