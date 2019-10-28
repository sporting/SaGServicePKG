using Newtonsoft.Json;
using SaGBridge.Utils;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGBridge
{
    public class OrderCounterBridge : BaseBridge<string>
    {
        protected override string Api
        {
            get
            {
                return "OrderCounter";
            }
        }

        public OrderCounterBridge(string token):base(token)
        {
            
        }

        public async Task<BridgeResult<bool>> GetExist(string ordNo)
        {
            string url = $"{ApiUrl}/?ordNo={ordNo}";

            MyLog.Info(this, $"{Api}: {url}: {ordNo}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {ordNo}");

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
                catch
                {
                }

                return new BridgeResult<bool> { status = true, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<bool> { status = false, result = res };
            }
        }

        public async Task<BridgeResult<string>> Get(string head, string yyyy)
        {
            string url = $"{ApiUrl}/?head={head}&yyyy={yyyy}";

            MyLog.Info(this, $"{Api}: {url}: {head} {yyyy}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {head} {yyyy}");

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
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<string> { status = false, result = res };
            }
        }
    }
}
