using Newtonsoft.Json;
using SaGBridge.Utils;
using SaGModel;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGBridge
{
    public class DoctorSlideLogBridge : BaseBridge<DoctorSlideLogM>
    {
        protected override string Api
        {
            get
            {
                return "DoctorSlideLog";
            }
        }

        public DoctorSlideLogBridge(string token):base(token)
        {
            
        }


        //Post Data by id, data
        // http://host/api/[Api]/
        public async Task<BridgeResult<DoctorSlideLogM[]>> PostAll(DoctorSlideLogM[] data)
        {
            string js = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(js, Encoding.UTF8, "application/json");

            MyLog.Info(this, $"{Api}: {ApiUrl}: {js}");
            //LogMan.Instance.Info(Api, $"{Api}: Post: {ApiUrl}: {js}");

            HttpResponseMessage response = await Client.PostAsync(ApiUrl, content);
            DoctorSlideLogM[] res = new DoctorSlideLogM[] { };
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<DoctorSlideLogM[]>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<DoctorSlideLogM[]> { status = true, message = string.Empty, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {ApiUrl}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {ApiUrl}: {js}: {ex.Message}");
                return new BridgeResult<DoctorSlideLogM[]> { status = false, message = ex.Message, result = res };
            }
        }

        public async Task<BridgeResult<DoctorSlideLogM[]>> GetByOpDate(string date)
        {
            string url = $"{ApiUrl}/?date={date}";

            MyLog.Info(this, $"{Api}: {url}: {date}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {date}");
            HttpResponseMessage response = await Client.GetAsync(url);
            DoctorSlideLogM[] res = null;
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<DoctorSlideLogM[]>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<DoctorSlideLogM[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<DoctorSlideLogM[]> { status = false, result = res };
            }
        }
    }
}
