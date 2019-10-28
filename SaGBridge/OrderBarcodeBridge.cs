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
    public class OrderBarcodeBridge : BaseBridge<OrderBarcodeM>
    {
        protected override string Api
        {
            get
            {
                return "OrderBarcode";
            }
        }

        public OrderBarcodeBridge(string token):base(token)
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
    }
}
