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
    public class WorkLoadBridge : BaseBridge<WorkLoadMV[]>
    {
        protected override string Api
        {
            get
            {
                return "WorkLoad";
            }
        }

        public WorkLoadBridge(string token):base(token)
        {
            
        }

        public async Task<BridgeResult<WorkLoadMV[]>> GetWorkLoad(string type, string begDate, string endDate, string user)
        {
            string url = $"{ApiUrl}/?type={type}&begDate={begDate}&endDate={endDate}&user={user}";

            LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {type} {begDate} {endDate} {user}");

            HttpResponseMessage response = await Client.GetAsync(url);
            WorkLoadMV[] res = new WorkLoadMV[] { };
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<WorkLoadMV[]>(responseBody);
                }
                catch (Exception ex)
                {
                    return new BridgeResult<WorkLoadMV[]> { status = false, result = res, message = ex.Message };
                }

                return new BridgeResult<WorkLoadMV[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<WorkLoadMV[]> { status = false, result = res,message=ex.Message };
            }
        }

        public async Task<BridgeResult<SlideWorkLoadMV[]>> GetSlideWorkLoad(string type, string begDate, string endDate, string user)
        {
            string url = $"{ApiUrl}/?type={type}&begDate={begDate}&endDate={endDate}&user={user}";

            LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {type} {begDate} {endDate} {user}");

            HttpResponseMessage response = await Client.GetAsync(url);
            SlideWorkLoadMV[] res = new SlideWorkLoadMV[] { };
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<SlideWorkLoadMV[]>(responseBody);
                }
                catch (Exception ex)
                {
                    return new BridgeResult<SlideWorkLoadMV[]> { status = false, result = res, message = ex.Message };
                }

                return new BridgeResult<SlideWorkLoadMV[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<SlideWorkLoadMV[]> { status = false, result = res, message = ex.Message };
            }
        }
    }
}
