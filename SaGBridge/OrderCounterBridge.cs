
using SaGBridge.Utils;
using SaGModel;
using SaGUtil.Data;
using SaGUtil.Json;
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

        public async Task<BridgeResult<bool>> GetExist(string headTag, string headYear, string tailSeq)
        {
            string url = $"{ApiUrl}/?headTag={headTag}&headYear={headYear}&tailSeq={tailSeq}";

            MyLog.Info(this, $"{Api}: {url}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {ordNo}");

            HttpResponseMessage response = await Client.GetAsync(url);
            bool res = false;
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = SaJson.DeserializeObject<bool>(responseBody);
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

        public async Task<BridgeResult<int>> Get(string head, string yyyy)
        {
            string url = $"{ApiUrl}/?head={head}&yyyy={yyyy}";

            MyLog.Info(this, $"{Api}: {url}: {head} {yyyy}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {head} {yyyy}");

            HttpResponseMessage response = await Client.GetAsync(url);
            string res = "0";
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = SaJson.DeserializeObject<string>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<int> { status = true, result = SaConverter.ToInt(res,0) };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<int> { status = false, result = SaConverter.ToInt(res, 0) };
            }
        }
    }
}
