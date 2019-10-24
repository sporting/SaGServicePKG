using Newtonsoft.Json;
using SaGBridge.Utils;
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
    public class OrderSlideBridge : BaseBridge<OrderSlideM>
    {
        protected override string Api
        {
            get
            {
                return "OrderSlide";
            }
        }

        public OrderSlideBridge(string token) : base(token)
        {

        }

        public async Task<BridgeResult<OrderSlideM>> GetByOrdNoSeq(string ordNo, int cassetteSequence, int slideSequence)
        {
            string url = $"{ApiUrl}/?ordNo={ordNo}&cassetteSequence={cassetteSequence}&slideSequence={slideSequence}";

            MyLog.Info(this, $"{Api}: {url}: {ordNo} {cassetteSequence} {slideSequence}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {ordNo} {cassetteSequence} {slideSequence}");

            HttpResponseMessage response = await Client.GetAsync(url);
            OrderSlideM res = new OrderSlideM();
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<OrderSlideM>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<OrderSlideM> { status = true, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<OrderSlideM> { status = false, result = res };
            }
        }
        public async Task<BridgeResult<OrderSlideM[]>> GetByOpDate(string date)
        {
            string url = $"{ApiUrl}/?date={date}";

            MyLog.Info(this, $"{Api}: {url}: {date}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {date}");

            HttpResponseMessage response = await Client.GetAsync(url);
            OrderSlideM[] res = null;
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<OrderSlideM[]>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<OrderSlideM[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<OrderSlideM[]> { status = false, result = res };
            }
        }

        public async Task<BridgeResult<OrderSlideM[]>> GetByDoctorDate(string date)
        {
            string url = $"{ApiUrl}/?doctorDate={date}";

            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {date}");
            MyLog.Info(this, $"{Api}: {url}: {date}");

            HttpResponseMessage response = await Client.GetAsync(url);
            OrderSlideM[] res = null;
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<OrderSlideM[]>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<OrderSlideM[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<OrderSlideM[]> { status = false, result = res };
            }
        }

        public async Task<BridgeResult<bool>> GetExist(string ordNo, int cassetteSeq, int slideSeq)
        {
            string url = $"{ApiUrl}/?ordNo={ordNo}&cassetteSeq={cassetteSeq}&slideSeq={slideSeq}";

            MyLog.Info(this, $"{Api}: {url}: {ordNo} {cassetteSeq} {slideSeq}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {ordNo} {cassetteSeq} {slideSeq}");

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

        public async Task<BridgeResult<OrderSlideM[]>> Get(string type, string begDate, string endDate, string user)
        {
            string url = $"{ApiUrl}/?type={type}&begDate={begDate}&endDate={endDate}&user={user}";

            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {type} {begDate} {endDate} {user}");
            MyLog.Info(this, $"{Api}: {url}: {type} {begDate} {endDate} {user}");

            HttpResponseMessage response = await Client.GetAsync(url);
            OrderSlideM[] res = new OrderSlideM[] { };
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<OrderSlideM[]>(responseBody);
                }
                catch (Exception ex)
                {
                    return new BridgeResult<OrderSlideM[]> { status = false, result = res, message = ex.Message };
                }

                return new BridgeResult<OrderSlideM[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<OrderSlideM[]> { status = false, result = res, message = ex.Message };
            }
        }
    }
}
