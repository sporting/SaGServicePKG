﻿using Newtonsoft.Json;
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
    public class SysParamsBridge : BaseBridge<SysParamsM>
    {
        protected override string Api
        {
            get
            {
                return "SysParams";
            }
        }

        public SysParamsBridge(string token):base(token)
        {
            
        }

        public async Task<BridgeResult<SysParamsM[]>> GetValues(string name)
        {
            string url = $"{ApiUrl}/?name={name}";

            MyLog.Info(this, $"{Api}: {url}: {name}");
            //LogMan.Instance.Info(Api, $"{Api}: Get: {url}: {name}");

            HttpResponseMessage response = await Client.GetAsync(url);
            SysParamsM[] res=null;
            try
            {
                response.EnsureSuccessStatusCode();

                try
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<SysParamsM[]>(responseBody);
                }
                catch
                {
                }

                return new BridgeResult<SysParamsM[]> { status = true, result = res };
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{Api}: {url}: {ex.Message}");
                //LogMan.Instance.Error(Api, $"{Api}: {url}: {ex.Message}");
                return new BridgeResult<SysParamsM[]> { status = false, result = res };
            }
        }

    }
}
