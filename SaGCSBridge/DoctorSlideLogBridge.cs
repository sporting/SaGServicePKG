using SaGLogic;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaGCSBridge
{
    public class DoctorSlideLogBridge : BaseCSBridge<DoctorSlideLogM>
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


        public async Task<BridgeResult<DoctorSlideLogM[]>> AddDoctorSlideLog(DoctorSlideLogM[] logs)
        {
            try
            {
                DoctorSlideLog doctorSlideLog = new DoctorSlideLog();
                if (doctorSlideLog.AddLog(logs))
                {
                    return await Task.FromResult(new BridgeResult<DoctorSlideLogM[]>
                    {
                        status = true,
                        message = string.Empty,
                        result = logs
                    });
                }
                else
                {
                    return await Task.FromResult(new BridgeResult<DoctorSlideLogM[]>
                    {
                        status = false,
                        message = "DoctorSlideLog AddLog failed",
                        result = new DoctorSlideLogM[] { }
                    });
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BridgeResult<DoctorSlideLogM[]>
                {
                    status = true,
                    message = ex.Message,
                    result = new DoctorSlideLogM[] { }
                });
            }
        }
    }
}
