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
    public class BarcodeCassetteBridge : BaseCSBridge<BarcodeCassetteMV[]>
    {
        protected override string Api
        {
            get
            {
                return "BarcodeCassette";
            }
        }

        public BarcodeCassetteBridge(string token):base(token)
        {
            
        }

        public async Task<BridgeResult<BarcodeCassetteMV[]>> GetBarcodeCassetteMV(string begDate, string endDate)
        {
            try
            {
                BarcodeCassetteV work = new BarcodeCassetteV();
                BarcodeCassetteMV[] wv = work.Get(begDate, endDate);
                return await Task.FromResult(new BridgeResult<BarcodeCassetteMV[]>
                {
                    status = true,
                    message = string.Empty,
                    result = wv
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<BarcodeCassetteMV[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new BarcodeCassetteMV[] { }
                });
            }
        }

        public async Task<BridgeResult<BarcodeCassetteMV[]>> GetBarcodeCassetteMV(string ordNo)
        {
            try
            {
                BarcodeCassetteV work = new BarcodeCassetteV();
                BarcodeCassetteMV[] wv = work.Get(ordNo);
                return await Task.FromResult(new BridgeResult<BarcodeCassetteMV[]>
                {
                    status = true,
                    message = string.Empty,
                    result = wv
                });
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return await Task.FromResult(new BridgeResult<BarcodeCassetteMV[]>
                {
                    status = false,
                    message = ex.Message,
                    result = new BarcodeCassetteMV[] { }
                });
            }
        }
    }
}
